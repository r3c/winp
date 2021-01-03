using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Winp.Configuration;

namespace Winp.Forms
{
    public partial class ConfigurationForm : Form
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
            _installDirectoryTextBox.Text = environment.InstallDirectoryOrDefault.AbsolutePath;
            _locations = application.LocationsOrDefault.ToList();
            _locationTypeComboBox.Items.AddRange(descriptions.Cast<object>().ToArray());
            _locationTypeComboBox.SelectedIndex = 0;
            _save = save;
            _serverAddressTextBox.Text = package.Nginx.ServerAddressOrDefault;
            _serverPortTextBox.Text = package.Nginx.ServerPortOrDefault.ToString(CultureInfo.InvariantCulture);

            LocationRefresh();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            _application.Environment.InstallDirectory = new Uri(_installDirectoryTextBox.Text);
            _application.Locations = _locations.ToArray();
            _application.Package.Nginx.ServerAddress =
                _serverAddressTextBox.Text.Length > 0 ? _serverAddressTextBox.Text : null;
            _application.Package.Nginx.ServerPort = int.TryParse(_serverPortTextBox.Text, out var serverPort)
                ? (int?) serverPort
                : null;

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
            var location = new LocationConfig
            {
                Alias = Uri.TryCreate(_locationAliasTextBox.Text, UriKind.RelativeOrAbsolute, out var aliasDirectory)
                    ? aliasDirectory
                    : null,
                Base = _locationBaseTextBox.Text,
                Index = _locationIndexCheckBox.Checked,
                List = _locationListCheckBox.Checked,
                Type = (LocationType) _locationTypeComboBox.SelectedIndex
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

                _locationAliasTextBox.Text = location.AliasOrDefault.IsAbsoluteUri
                    ? location.AliasOrDefault.AbsolutePath
                    : string.Empty;
                _locationBaseTextBox.Text = location.BaseOrDefault;
                _locationIndexCheckBox.Checked = location.Index;
                _locationListCheckBox.Checked = location.List;
                _locationTypeComboBox.SelectedIndex = (int) location.Type;

                _locationDeleteButton.Enabled = true;
                _locationUpdateButton.Text = @"Update";
            }
            else
            {
                _locationAliasTextBox.Text = string.Empty;
                _locationBaseTextBox.Text = string.Empty;
                _locationIndexCheckBox.Checked = false;
                _locationListCheckBox.Checked = false;
                _locationTypeComboBox.SelectedIndex = default;

                _locationDeleteButton.Enabled = false;
                _locationUpdateButton.Text = @"Insert";
            }
        }

        private void LocationAliasButton_Click(object sender, EventArgs e)
        {
            if (_folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                _locationAliasTextBox.Text = _folderBrowserDialog.SelectedPath;
        }

        private void LocationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((LocationType) _locationTypeComboBox.SelectedIndex)
            {
                case LocationType.PhpFileName:
                case LocationType.Static:
                    _locationAliasButton.Visible = true;
                    _locationAliasLabel.Visible = true;
                    _locationAliasTextBox.Visible = true;
                    _locationIndexCheckBox.Visible = true;
                    _locationIndexLabel.Visible = true;
                    _locationListCheckBox.Visible = true;

                    break;

                case LocationType.PhpOnly:
                    _locationAliasButton.Visible = true;
                    _locationAliasLabel.Visible = true;
                    _locationAliasTextBox.Visible = true;
                    _locationIndexCheckBox.Visible = false;
                    _locationIndexLabel.Visible = false;
                    _locationListCheckBox.Visible = false;

                    break;

                default:
                    _locationAliasButton.Visible = false;
                    _locationAliasLabel.Visible = false;
                    _locationAliasTextBox.Visible = false;
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
                .Select((location, index) => new LocationItem(index, location.BaseOrDefault))
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
}