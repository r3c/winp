using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Winp.Configuration;
using Winp.Packages;

namespace Winp.Forms
{
    public partial class ServiceForm : Form
    {
        private static readonly string ConfigurationPath =
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".json");

        private ApplicationConfig _configuration;

        private readonly IReadOnlyList<Package> _packages;
        private readonly TaskScheduler _scheduler;

        public ServiceForm()
        {
            InitializeComponent();

            ApplicationConfig configuration;

            try
            {
                configuration = ConfigurationLoad();
            }
            catch (JsonException exception)
            {
                MessageBox.Show(this, $"Could not load configuration file '{ConfigurationPath}', defaults will be used." + Environment.NewLine + Environment.NewLine + "Error was: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                configuration = new ApplicationConfig();
            }

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            var mariaDbPackage = new MariaDbPackage();
            var mariaDbInstance = new Instance(mariaDbPackage);
            var mariaDbService = new Package(mariaDbPackage, mariaDbInstance, scheduler, _packageMariaDbVariantComboBox, _packageMariaDbStatusLabel, configuration.Package.MariaDb.Variants, PackageRefresh);
            var nginxPackage = new NginxPackage();
            var nginxInstance = new Instance(nginxPackage);
            var nginxService = new Package(nginxPackage, nginxInstance, scheduler, _packageNginxVariantComboBox, _packageNginxStatusLabel, configuration.Package.Nginx.Variants, PackageRefresh);
            var phpPackage = new PhpPackage();
            var phpInstance = new Instance(phpPackage);
            var phpService = new Package(phpPackage, phpInstance, scheduler, _packagePhpVariantComboBox, _packagePhpStatusLabel, configuration.Package.Php.Variants, PackageRefresh);
            var phpMyAdminPackage = new PhpMyAdminPackage();
            var phpMyAdminService = new Package(phpMyAdminPackage, null, scheduler, _packagePhpMyAdminVariantComboBox, _packagePhpMyAdminStatusLabel, configuration.Package.PhpMyAdmin.Variants, PackageRefresh);

            _configuration = configuration;
            _packages = new[] { mariaDbService, nginxService, phpService, phpMyAdminService };
            _scheduler = scheduler;

            mariaDbService.Initialize();
            nginxService.Initialize();
            phpService.Initialize();
            phpMyAdminService.Initialize();
        }

        private void ServiceForm_Shown(object sender, EventArgs e)
        {
            PackageRefreshAll();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Allow closing form is all services are stopped
            if (!_packages.Any(package => package.Instance?.IsRunning ?? false))
                return;

            // Otherwise stop them before closing form
            ControlStopExecute().ContinueWith(task => Close(), _scheduler);

            e.Cancel = true;
        }

        private static ApplicationConfig ConfigurationLoad()
        {
            if (!File.Exists(ConfigurationPath))
                return new ApplicationConfig();

            using var stream = new FileStream(ConfigurationPath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream, Encoding.UTF8);

            return JsonConvert.DeserializeObject<ApplicationConfig>(reader.ReadToEnd())!;
        }

        private static void ConfigurationSave(ApplicationConfig configuration)
        {
            using var stream = new FileStream(ConfigurationPath, FileMode.Create, FileAccess.Write);
            using var writer = new StreamWriter(stream, Encoding.UTF8);

            writer.Write(JsonConvert.SerializeObject(configuration, Formatting.Indented));
        }

        private void ControlConfigureButton_Click(object sender, EventArgs e)
        {
            var form = new ConfigurationForm(_configuration, configuration =>
            {
                ConfigurationSave(configuration);

                _configuration = configuration;

                PackageRefreshAll();
            });

            form.Show(this);
        }

        private void ControlInstallButton_Click(object sender, EventArgs e)
        {
            Task.Run(ControlInstallExecute);
        }

        private void ControlStartButton_Click(object sender, EventArgs e)
        {
            Task.Run(ControlStartExecute);
        }

        private void ControlStopButton_Click(object sender, EventArgs e)
        {
            Task.Run(ControlStopExecute);
        }

        private async Task ControlInstallExecute()
        {
            await ControlStopExecute();

            foreach (var package in _packages)
                await PackageInstall(package);
        }

        private async Task ControlStartExecute()
        {
            for (var i = 0; i < _packages.Count; ++i)
            {
                var started = await PackageStart(_packages[i]);

                if (started)
                    continue;

                for (var j = 0; j < i; ++j)
                    await PackageStop(_packages[j]);

                return;
            }

            if (_configuration.Locations.Count > 0)
            {
                var nginx = _configuration.Package.Nginx;

                Process.Start(new ProcessStartInfo
                {
                    FileName = $"http://{nginx.ServerAddress}:{nginx.ServerPort}{_configuration.Locations[0].Base}",
                    UseShellExecute = true
                });
            }
        }

        private async Task ControlStopExecute()
        {
            foreach (var package in _packages)
                await PackageStop(package);
        }

        private async Task<bool> PackageInstall(Package package)
        {
            package.SetText(Status.Loading, "Downloading and installing...");

            if (package.Variant is null)
            {
                PackageRefresh(package);

                return false;
            }

            var message = await Task.Run(() => package.Installable.Install(_configuration, package.Variant));

            if (message != null)
            {
                package.SetText(Status.Failure, message);

                return false;
            }

            PackageRefresh(package);

            return true;
        }

        private void PackageRefresh(Package package)
        {
            if (package.Variant is null)
                package.SetText(Status.Notice, "No selection");
            else if (!package.Installable.IsInstalled(_configuration, package.Variant))
                package.SetText(Status.Notice, "Not installed");
            else if (package.Instance is null)
                package.SetText(Status.Success, "Ready");
            else if (package.Instance.IsRunning)
                package.SetText(Status.Success, "Running");
            else
                package.SetText(Status.Notice, "Not running");
        }

        private void PackageRefreshAll()
        {
            foreach (var service in _packages)
                PackageRefresh(service);
        }

        private async Task<bool> PackageStart(Package package)
        {
            await PackageStop(package);

            package.SetText(Status.Loading, "Configuring package...");

            var variant = package.Variant;

            if (variant is null)
            {
                package.SetText(Status.Notice, "No selection");

                return false;
            }

            var message = await Task.Run(() => package.Installable.Configure(_configuration, variant));

            if (message != null)
            {
                package.SetText(Status.Failure, message);

                return false;
            }

            var instance = package.Instance;

            if (instance is not null)
            {
                package.SetText(Status.Failure, "Starting...");

                var success = await Task.Run(() => instance.Start(_configuration, variant.Identifier, () => PackageRefresh(package)));

                if (!success)
                {
                    package.SetText(Status.Failure, "Could not start");

                    return false;
                }
            }

            PackageRefresh(package);

            package.VersionLock();

            return true;
        }

        private async Task PackageStop(Package package)
        {
            package.VersionUnlock();

            var instance = package.Instance;
            var variant = package.Variant;

            if (instance is null || variant is null)
                return;

            package.SetText(Status.Failure, "Stopping...");

            await Task.Run(() => instance.Stop(_configuration, variant.Identifier));

            PackageRefresh(package);
        }
    }
}