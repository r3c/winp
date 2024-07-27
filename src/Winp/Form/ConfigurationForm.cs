using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Winp.Configuration;

namespace Winp.Form;

public partial class ConfigurationForm : System.Windows.Forms.Form
{
    private ApplicationConfig _application;
    private readonly List<LocationConfig> _locations;
    private readonly Action<ApplicationConfig> _save;

    public ConfigurationForm(ApplicationConfig application, Action<ApplicationConfig> save)
    {
        InitializeComponent();

        var descriptions = Enum.GetNames(typeof(LocationType))
            .Select(name => typeof(LocationType).GetField(name)?.GetCustomAttribute(typeof(DescriptionAttribute)))
            .Cast<DescriptionAttribute>().Select(a => a.Description);
        var environment = application.Environment;
        var package = application.Package;

        _application = application;
        _installDirectoryTextBox.Text = environment.InstallDirectory.AbsolutePath;
        _locations = application.Locations.ToList();
        _locationTypeComboBox.Items.AddRange(descriptions.Cast<object>().ToArray());
        _locationTypeComboBox.SelectedIndex = 0;
        _save = save;
        _serverAddressTextBox.Text = package.Nginx.ServerAddress;
        _serverPortTextBox.Text = package.Nginx.ServerPort.ToString(CultureInfo.InvariantCulture);

        LocationRefresh();
    }

    private void AcceptButton_Click(object sender, EventArgs e)
    {
        if (!Uri.TryCreate(_installDirectoryTextBox.Text, UriKind.Absolute, out var installDirectory))
        {
            MessageBox.Show(this, "Install directory is not a valid path", "Error", MessageBoxButtons.OK);

            return;
        }

        if (!IPAddress.TryParse(_serverAddressTextBox.Text, out var serverAddress))
        {
            MessageBox.Show(this, "Server address is not a valid IP", "Error", MessageBoxButtons.OK);

            return;
        }

        if (!int.TryParse(_serverPortTextBox.Text, out var serverPort) || serverPort < 1 || serverPort > 65535)
        {
            MessageBox.Show(this, "Server port is not a valid integer between 1 and 65535", "Error", MessageBoxButtons.OK);

            return;
        }

        // Move every root path pointing to previous install directory to new one
        var previousInstallDirectory = _application.Environment.InstallDirectory;

        if (installDirectory != previousInstallDirectory)
        {
            _locations.ForEach(location =>
            {
                if (location.Root.AbsolutePath.StartsWith(previousInstallDirectory.AbsolutePath) &&
                    Uri.TryCreate(
                        installDirectory.AbsolutePath +
                        location.Root.AbsolutePath[previousInstallDirectory.AbsolutePath.Length..],
                        UriKind.Absolute,
                        out var relocatedRoot))
                {
                    location.Root = relocatedRoot;
                }
            });
        }

        _application.Environment.InstallDirectory = installDirectory;
        _application.Locations = _locations;
        _application.Package.Nginx.ServerAddress = serverAddress.ToString();
        _application.Package.Nginx.ServerPort = serverPort;

        _save(_application);

        Close();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InstallDirectoryButton_Click(object sender, EventArgs e)
    {
        if (_folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            _installDirectoryTextBox.Text = _folderBrowserDialog.SelectedPath;
    }

    private void LocationDeleteButton_Click(object sender, EventArgs e)
    {
        if (_locationListBox.SelectedItem is LocationItem item)
        {
            _locations.RemoveAt(item.Index);

            LocationRefresh();
        }
    }

    private void LocationUpdateButton_Click(object sender, EventArgs e)
    {
        if (!Uri.TryCreate(_locationRootTextBox.Text, UriKind.Absolute, out var locationRoot))
        {
            MessageBox.Show(this, "Root directory is not a valid path", "Error", MessageBoxButtons.OK);

            return;
        }

        var location = new LocationConfig
        {
            Base = _locationBaseTextBox.Text,
            Index = _locationIndexCheckBox.Checked,
            List = _locationListCheckBox.Checked,
            Root = locationRoot,
            Type = (LocationType)_locationTypeComboBox.SelectedIndex
        };

        if (_locationListBox.SelectedItem is LocationItem item)
        {
            _locations[item.Index] = location;

            LocationRefresh();

            _locationListBox.SelectedIndex = item.Index;
        }
        else
        {
            _locations.Add(location);

            LocationRefresh();

            _locationListBox.SelectedIndex = _locations.Count - 1;
        }
    }

    private void LocationListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_locationListBox.SelectedItem is LocationItem item)
        {
            var location = _locations[item.Index];

            _locationBaseTextBox.Text = location.Base;
            _locationIndexCheckBox.Checked = location.Index;
            _locationListCheckBox.Checked = location.List;
            _locationRootTextBox.Text = location.Root.IsAbsoluteUri
                ? location.Root.AbsolutePath
                : string.Empty;
            _locationTypeComboBox.SelectedIndex = (int)location.Type;

            _locationDeleteButton.Enabled = true;
            _locationUpdateButton.Text = @"Update";
        }
        else
        {
            _locationBaseTextBox.Text = string.Empty;
            _locationIndexCheckBox.Checked = false;
            _locationListCheckBox.Checked = false;
            _locationRootTextBox.Text = string.Empty;
            _locationTypeComboBox.SelectedIndex = default;

            _locationDeleteButton.Enabled = false;
            _locationUpdateButton.Text = @"Insert";
        }
    }

    private void LocationRootButton_Click(object sender, EventArgs e)
    {
        if (_folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            _locationRootTextBox.Text = _folderBrowserDialog.SelectedPath;
    }

    private void LocationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch ((LocationType)_locationTypeComboBox.SelectedIndex)
        {
            case LocationType.PhpFileName:
            case LocationType.Static:
                _locationRootButton.Visible = true;
                _locationRootLabel.Visible = true;
                _locationRootTextBox.Visible = true;
                _locationIndexCheckBox.Visible = true;
                _locationIndexLabel.Visible = true;
                _locationListCheckBox.Visible = true;

                break;

            case LocationType.PhpOnly:
                _locationRootButton.Visible = true;
                _locationRootLabel.Visible = true;
                _locationRootTextBox.Visible = true;
                _locationIndexCheckBox.Visible = false;
                _locationIndexLabel.Visible = false;
                _locationListCheckBox.Visible = false;

                break;

            default:
                _locationRootButton.Visible = false;
                _locationRootLabel.Visible = false;
                _locationRootTextBox.Visible = false;
                _locationIndexCheckBox.Visible = false;
                _locationIndexLabel.Visible = false;
                _locationListCheckBox.Visible = false;

                break;
        }
    }

    private void LocationRefresh()
    {
        _locationListBox.Items.Clear();
        _locationListBox.Items.AddRange(_locations
            .Select((location, index) => new LocationItem(index, location.Base))
            .Cast<object>()
            .ToArray());
        _locationListBox.Items.Add("<new location>");
    }

    private class LocationItem
    {
        public readonly int Index;

        private readonly string _label;

        public LocationItem(int index, string label)
        {
            Index = index;
            _label = label;
        }

        public override string ToString()
        {
            return _label;
        }
    }
}