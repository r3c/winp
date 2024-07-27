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
using Winp.Package;

namespace Winp.Forms;

public partial class ServiceForm : Form
{
    private static readonly string ConfigurationPath =
        Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".json");

    private ApplicationConfig _configuration;

    private readonly IReadOnlyList<ServiceContainer> _packages;
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
        var mariaDbInstance = new ServiceRunner(mariaDbPackage);
        var mariaDbService = new ServiceContainer(mariaDbPackage, mariaDbInstance, scheduler, _packageMariaDbVariantComboBox, _packageMariaDbStatusLabel, configuration.Package.MariaDb.Variants, PackageRefresh);
        var nginxPackage = new NginxPackage();
        var nginxInstance = new ServiceRunner(nginxPackage);
        var nginxService = new ServiceContainer(nginxPackage, nginxInstance, scheduler, _packageNginxVariantComboBox, _packageNginxStatusLabel, configuration.Package.Nginx.Variants, PackageRefresh);
        var phpPackage = new PhpPackage();
        var phpInstance = new ServiceRunner(phpPackage);
        var phpService = new ServiceContainer(phpPackage, phpInstance, scheduler, _packagePhpVariantComboBox, _packagePhpStatusLabel, configuration.Package.Php.Variants, PackageRefresh);
        var phpMyAdminPackage = new PhpMyAdminPackage();
        var phpMyAdminService = new ServiceContainer(phpMyAdminPackage, null, scheduler, _packagePhpMyAdminVariantComboBox, _packagePhpMyAdminStatusLabel, configuration.Package.PhpMyAdmin.Variants, PackageRefresh);

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
        if (!_packages.Any(package => package.Runner?.IsRunning ?? false))
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

    private async Task<bool> PackageInstall(ServiceContainer service)
    {
        service.SetText(Status.Loading, "Downloading and installing...");

        if (service.Variant is null)
        {
            PackageRefresh(service);

            return false;
        }

        var message = await Task.Run(() => service.Package.Install(_configuration, service.Variant));

        if (message != null)
        {
            service.SetText(Status.Failure, message);

            return false;
        }

        PackageRefresh(service);

        return true;
    }

    private void PackageRefresh(ServiceContainer service)
    {
        if (service.Variant is null)
            service.SetText(Status.Failure, "No selection");
        else if (!service.Package.IsInstalled(_configuration, service.Variant))
            service.SetText(Status.Notice, "Not installed");
        else if (service.Runner is null || service.Runner.IsRunning)
            service.SetText(Status.Success, "Ready");
        else
            service.SetText(Status.Notice, "Not running");
    }

    private void PackageRefreshAll()
    {
        foreach (var service in _packages)
            PackageRefresh(service);
    }

    private async Task<bool> PackageStart(ServiceContainer service)
    {
        await PackageStop(service);

        service.SetText(Status.Loading, "Configuring package...");

        var variant = service.Variant;

        if (variant is null)
        {
            service.SetText(Status.Failure, "Nothing to start");

            return false;
        }

        var message = await Task.Run(() => service.Package.Configure(_configuration, variant));

        if (message != null)
        {
            service.SetText(Status.Failure, $"Error: {message}");

            return false;
        }

        var instance = service.Runner;

        if (instance is not null)
        {
            service.SetText(Status.Loading, "Starting...");

            var success = await Task.Run(() => instance.Start(_configuration, variant.Identifier, () => PackageRefresh(service)));

            if (!success)
            {
                service.SetText(Status.Failure, "Could not start");

                return false;
            }
        }

        PackageRefresh(service);

        service.VersionLock();

        return true;
    }

    private async Task PackageStop(ServiceContainer service)
    {
        service.VersionUnlock();

        var instance = service.Runner;
        var variant = service.Variant;

        if (instance is null || variant is null)
            return;

        service.SetText(Status.Loading, "Stopping...");

        await Task.Run(() => instance.Stop(_configuration, variant.Identifier));

        PackageRefresh(service);
    }
}