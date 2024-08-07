using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Winp.Configuration;
using Winp.Package;
using Winp.Properties;

namespace Winp.Form;

public partial class ServiceForm : System.Windows.Forms.Form
{
    private static readonly string ConfigurationPath =
        Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".json");

    private ApplicationConfig _configuration;

    private readonly TaskScheduler _scheduler;
    private readonly IReadOnlyList<ServiceReference> _services;

    public ServiceForm()
    {
        InitializeComponent();

        ApplicationConfig configuration;

        try
        {
            configuration = ApplicationConfig.Load(ConfigurationPath);
        }
        catch (JsonException exception)
        {
            MessageBox.Show(
                this,
                $@"Could not load configuration file '{ConfigurationPath}', defaults will be used.{Environment.NewLine}{Environment.NewLine}Error was: {exception.Message}",
                @"Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            configuration = new ApplicationConfig();
        }

        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        var mariaDbPackage = new MariaDbPackage();
        var mariaDbInstance = new ServiceRunner(mariaDbPackage);
        var mariaDbService = CreateServiceReference(mariaDbPackage, mariaDbInstance,
            configuration.Package.MariaDb.Variants, _packageMariaDbVariantComboBox, _packageMariaDbStatusLabel);
        var nginxPackage = new NginxPackage();
        var nginxInstance = new ServiceRunner(nginxPackage);
        var nginxService = CreateServiceReference(nginxPackage, nginxInstance, configuration.Package.Nginx.Variants,
            _packageNginxVariantComboBox, _packageNginxStatusLabel);
        var phpPackage = new PhpPackage();
        var phpInstance = new ServiceRunner(phpPackage);
        var phpService = CreateServiceReference(phpPackage, phpInstance, configuration.Package.Php.Variants,
            _packagePhpVariantComboBox, _packagePhpStatusLabel);
        var phpMyAdminPackage = new PhpMyAdminPackage();
        var phpMyAdminService = CreateServiceReference(phpMyAdminPackage, null,
            configuration.Package.PhpMyAdmin.Variants, _packagePhpMyAdminVariantComboBox,
            _packagePhpMyAdminStatusLabel);

        var statusImageList = new ImageList();

        statusImageList.Images.Add(Resources.statusError);
        statusImageList.Images.Add(Resources.statusInformation);
        statusImageList.Images.Add(Resources.statusReady);
        statusImageList.Images.Add(Resources.statusLoading);

        _configuration = configuration;
        _packageMariaDbStatusLabel.ImageList = statusImageList;
        _packageNginxStatusLabel.ImageList = statusImageList;
        _packagePhpStatusLabel.ImageList = statusImageList;
        _packagePhpMyAdminStatusLabel.ImageList = statusImageList;
        _services = new[] { mariaDbService, nginxService, phpService, phpMyAdminService };
        _scheduler = scheduler;
    }

    private void ServiceForm_Shown(object sender, EventArgs e)
    {
        _ = PackageRefreshAll();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        // Allow closing form is all services are stopped
        if (!_services.Any(package => package.Runner?.IsRunning ?? false))
            return;

        // Otherwise stop them before closing form
        ControlStopExecute().ContinueWith(_ => Close(), _scheduler);

        e.Cancel = true;
    }

    private void ControlConfigureButton_Click(object sender, EventArgs e)
    {
        var form = new ConfigurationForm(_configuration, configuration =>
        {
            ApplicationConfig.Save(ConfigurationPath, configuration);

            _configuration = configuration;

            _ = PackageRefreshAll();
        });

        form.Show(this);
    }

    private void ControlStartButton_Click(object sender, EventArgs e)
    {
        RunBackgroundAction(ControlStartExecute);
    }

    private void ControlStopButton_Click(object sender, EventArgs e)
    {
        RunBackgroundAction(ControlStopExecute);
    }

    private async Task ControlStartExecute()
    {
        for (var i = 0; i < _services.Count; ++i)
        {
            var started = await PackageStart(_services[i]);

            if (started)
                continue;

            for (var j = 0; j < i; ++j)
                await PackageStop(_services[j]);

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
        foreach (var service in _services)
            await PackageStop(service);
    }

    private ServiceReference CreateServiceReference(IPackage package, ServiceRunner? runner,
        IReadOnlyList<PackageVariantConfig> variants, ComboBox variantComboBox, Label statusLabel)
    {
        var service = new ServiceReference(
            package,
            runner,
            () =>
            {
                var task = new Task<PackageVariantConfig?>(() => variantComboBox.SelectedItem as PackageVariantConfig);

                task.Start(_scheduler);

                return task;
            },
            enabled => RunUiAction(() =>
            {
                variantComboBox.Enabled = enabled;
            }),
            status => RunUiAction(() =>
            {
                const int space = 4;

                statusLabel.ImageIndex = (int)status.Level;
                statusLabel.Text = status.Message;

                // Set text and left-aligned icon on underlying label component.
                statusLabel.AutoSize = true;

                var imageWidth = statusLabel.ImageList is not null ? statusLabel.ImageList.ImageSize.Width : 0;
                var labelWidth = statusLabel.Width;

                statusLabel.AutoSize = false;
                statusLabel.Width = imageWidth + space + labelWidth;
            }));

        foreach (var variant in variants)
            variantComboBox.Items.Add(variant);

        if (variants.Count > 0)
            variantComboBox.SelectedIndex = 0;

        variantComboBox.SelectedIndexChanged += (_, _) => _ = PackageRefresh(service);

        return service;
    }

    private async Task PackageRefresh(ServiceReference service)
    {
        var variant = await service.GetVariant();

        Status status;

        if (variant is null)
            status = new Status(StatusLevel.Failure, "No selection");
        else if (!service.Package.IsInstalled(_configuration, variant))
            status = new Status(StatusLevel.Notice, "Not installed");
        else if (service.Runner is null || service.Runner.IsRunning)
            status = new Status(StatusLevel.Success, "Ready");
        else
            status = new Status(StatusLevel.Notice, "Not running");

        service.SetStatus(status);
    }

    private async Task PackageRefreshAll()
    {
        foreach (var service in _services)
            await PackageRefresh(service);
    }

    private async Task<bool> PackageStart(ServiceReference service)
    {
        await PackageStop(service);

        var variant = await service.GetVariant();

        if (variant is null)
        {
            service.SetStatus(new Status(StatusLevel.Failure, "Nothing to start"));

            return false;
        }

        if (!service.Package.IsInstalled(_configuration, variant))
        {
            service.SetStatus(new Status(StatusLevel.Loading, "Installing package..."));

            var installMessage = await Task.Run(() => service.Package.Install(_configuration, variant));

            if (installMessage != null)
            {
                service.SetStatus(new Status(StatusLevel.Failure, installMessage));

                return false;
            }
        }

        service.SetStatus(new Status(StatusLevel.Loading, "Configuring package..."));

        var configureMessage = await Task.Run(() => service.Package.Configure(_configuration, variant));

        if (configureMessage != null)
        {
            service.SetStatus(new Status(StatusLevel.Failure, $"Error: {configureMessage}"));

            return false;
        }

        var instance = service.Runner;

        if (instance is not null)
        {
            service.SetStatus(new Status(StatusLevel.Loading, "Starting..."));

            var success = await Task.Run(() => instance.Start(_configuration, variant.Identifier,
                () => _ = PackageRefresh(service)));

            if (!success)
            {
                service.SetStatus(new Status(StatusLevel.Failure, "Could not start"));

                return false;
            }
        }

        await PackageRefresh(service);

        service.SetEnabled(false);

        return true;
    }

    private async Task PackageStop(ServiceReference service)
    {
        service.SetEnabled(true);

        var runner = service.Runner;
        var variant = await service.GetVariant();

        if (runner is null || variant is null)
            return;

        service.SetStatus(new Status(StatusLevel.Loading, "Stopping..."));

        await Task.Run(() => runner.Stop(_configuration, variant.Identifier));
        await PackageRefresh(service);
    }

    private void RunBackgroundAction(Func<Task> action)
    {
        Task.Run(action).ContinueWith((task, _) =>
        {
            var exception = task.Exception;

            if (exception is not null)
            {
                MessageBox.Show(
                    this,
                    $@"Unknown error was caught, you should probably restart the application.{Environment.NewLine}{Environment.NewLine}Error was: {exception.Message}",
                    @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }, null, TaskContinuationOptions.OnlyOnFaulted);
    }

    private void RunUiAction(Action action)
    {
        var task = new Task(action);

        task.Start(_scheduler);
    }
}