using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Winp.Services;

namespace Winp.Forms
{
    public partial class ServiceForm : Form
    {
        private static readonly string ConfigurationPath =
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".json");

        private readonly IReadOnlyList<Instance> _instances = new[]
            {new Instance(new MariaDbService()), new Instance(new NginxService()), new Instance(new PhpService())};

        private Configuration.ApplicationConfig _configuration;

        public ServiceForm()
        {
            InitializeComponent();

            _configuration = ConfigurationLoad();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ExecuteStop().Wait();
        }

        private static Configuration.ApplicationConfig ConfigurationLoad()
        {
            if (!File.Exists(ConfigurationPath))
                return default;

            using var stream = new FileStream(ConfigurationPath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream, Encoding.UTF8);

            return JsonConvert.DeserializeObject<Configuration.ApplicationConfig>(reader.ReadToEnd());
        }

        private static void ConfigurationSave(Configuration.ApplicationConfig configuration)
        {
            using var stream = new FileStream(ConfigurationPath, FileMode.Create, FileAccess.Write);
            using var writer = new StreamWriter(stream, Encoding.UTF8);

            writer.Write(JsonConvert.SerializeObject(configuration, Formatting.Indented));
        }

        private void ConfigureButton_Click(object sender, EventArgs e)
        {
            var form = new ConfigurationForm(_configuration, configuration =>
            {
                ConfigurationSave(configuration);

                _configuration = configuration;
            });

            form.Show(this);
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            var imageList = _statusImageList;
            var label = _installStatusLabel;

            SetStatusLabel(label, imageList, Status.Loading, "Downloading and configuring services...");

            InstallExecute().ContinueWith(async run =>
            {
                if (run.IsFaulted && run.Exception != null)
                {
                    SetStatusLabel(label, imageList, Status.Failure, "Installation failed: " + run.Exception.Message);

                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
                else if (run.Result != null)
                {
                    SetStatusLabel(label, imageList, Status.Failure, "Installation failed: " + run.Result);

                    await Task.Delay(TimeSpan.FromSeconds(3));
                }

                await InstallRefresh();
            });
        }

        private void ServiceForm_Shown(object sender, EventArgs e)
        {
            Task.Run(InstallRefresh);
            Task.Run(ExecuteRefresh);
        }

        private void ExecuteStartButton_Click(object sender, EventArgs e)
        {
            Task.Run(ExecuteStart);
        }

        private void ExecuteStopButton_Click(object sender, EventArgs e)
        {
            Task.Run(ExecuteStop);
        }

        private async Task<string?> InstallExecute()
        {
            if (_instances.Any(instance => instance.IsRunning))
                return "stop services before installing";

            foreach (var instance in _instances)
            {
                var message = await instance.Service.Install(_configuration);

                if (message != null)
                    return $"{instance.Service.Name}: {message}";
            }

            return null;
        }

        private async Task InstallRefresh()
        {
            var missing = new List<string>();

            foreach (var instance in _instances)
            {
                if (!await instance.Service.IsReady(_configuration))
                    missing.Add(instance.Service.Name);
            }

            if (missing.Count > 0)
            {
                var message = "Missing services: " + string.Join(", ", missing) + $" (click '{_installButton.Text}')";

                SetStatusLabel(_installStatusLabel, _statusImageList, Status.Notice, message);
            }
            else
                SetStatusLabel(_installStatusLabel, _statusImageList, Status.Success, "Services installed");
        }

        private Task ExecuteRefresh()
        {
            var allExited = true;
            var anyExited = false;

            foreach (var instance in _instances)
            {
                allExited = allExited && !instance.IsRunning;
                anyExited = anyExited || !instance.IsRunning;
            }

            if (allExited)
                SetStatusLabel(_executeStatusLabel, _statusImageList, Status.Notice, "Services stopped");
            else if (anyExited)
                SetStatusLabel(_executeStatusLabel, _statusImageList, Status.Failure, "Process faulted");
            else
                SetStatusLabel(_executeStatusLabel, _statusImageList, Status.Success, "Services running");

            return Task.CompletedTask;
        }

        private async Task ExecuteStart()
        {
            SetStatusLabel(_executeStatusLabel, _statusImageList, Status.Loading, "Starting services...");

            for (var i = 0; i < _instances.Count; ++i)
            {
                var instance = _instances[i];

                if (await instance.Start(_configuration, () => Task.Run(ExecuteRefresh)))
                    continue;

                for (var j = 0; j < i; ++j)
                    await _instances[j].Stop(_configuration);

                SetStatusLabel(_executeStatusLabel, _statusImageList, Status.Failure,
                    $"Failed starting process {instance.Service.Name}");

                return;
            }

            await ExecuteRefresh();
        }

        private async Task ExecuteStop()
        {
            SetStatusLabel(_executeStatusLabel, _statusImageList, Status.Loading, "Stopping services...");

            foreach (var instance in _instances)
                await instance.Stop(_configuration);

            await ExecuteRefresh();
        }

        private void SetStatusLabel(Label label, ImageList imageList, Status? status, string text)
        {
            const int space = 4;

            Invoke(new MethodInvoker(() =>
            {
                if (status.HasValue)
                {
                    label.ImageIndex = (int) status.Value;
                    label.Text = text;

                    label.AutoSize = true;

                    var width = label.Width;

                    label.AutoSize = false;

                    label.Width = imageList.ImageSize.Width + space + width;
                    label.Visible = true;
                }
                else
                    label.Visible = false;
            }));
        }
    }
}