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
            var service = application.Service;

            _application = application;
            _installDirectoryTextBox.Text = environment.InstallDirectoryOrDefault.AbsolutePath;
            _locations = application.LocationsOrDefault.ToList();
            _locationTypeComboBox.Items.AddRange(descriptions.Cast<object>().ToArray());
            _locationTypeComboBox.SelectedIndex = 0;
            _save = save;
            _serverAddressTextBox.Text = service.Nginx.ServerAddressOrDefault;
            _serverPortTextBox.Text = service.Nginx.ServerPortOrDefault.ToString(CultureInfo.InvariantCulture);

            LocationRefresh();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            _application.Environment.InstallDirectory = new Uri(_installDirectoryTextBox.Text);
            _application.Locations = _locations.ToArray();
            _application.Service.Nginx.ServerAddress =
                _serverAddressTextBox.Text.Length > 0 ? _serverAddressTextBox.Text : null;
            _application.Service.Nginx.ServerPort = int.TryParse(_serverPortTextBox.Text, out var serverPort)
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
            var index = _locations.FindIndex(l => l.Base == _locationBaseTextBox.Text);

            if (index < 0)
                return;

            _locations.RemoveAt(index);

            LocationRefresh();
        }

        private void LocationUpdateButton_Click(object sender, EventArgs e)
        {
            var location = new LocationConfig
            {
                Alias = Uri.TryCreate(_locationAliasTextBox.Text, UriKind.RelativeOrAbsolute, out var aliasDirectory)
                    ? aliasDirectory
                    : null,
                Base = _locationBaseTextBox.Text,
                List = _locationListCheckBox.Checked,
                Type = (LocationType) _locationTypeComboBox.SelectedIndex
            };

            var replace = _locations.FindIndex(l => l.Base == location.Base);

            if (replace < 0)
                _locations.Add(location);
            else
                _locations[replace] = location;

            LocationRefresh();

            _locationListBox.SelectedIndex = replace < 0 ? _locations.Count - 1 : replace;
        }

        private void LocationListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(_locationListBox.SelectedItem is string locationBase))
                locationBase = string.Empty;

            var location = _locations.FirstOrDefault(l => l.Base == locationBase);

            _locationAliasTextBox.Text = location.AliasOrDefault.AbsolutePath;
            _locationBaseTextBox.Text = location.BaseOrDefault;
            _locationListCheckBox.Checked = location.List;
            _locationTypeComboBox.SelectedIndex = (int) location.Type;
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
                    _locationListCheckBox.Visible = true;
                    _locationListLabel.Visible = true;

                    break;

                case LocationType.PhpOnly:
                    _locationAliasButton.Visible = true;
                    _locationAliasLabel.Visible = true;
                    _locationAliasTextBox.Visible = true;
                    _locationListCheckBox.Visible = false;
                    _locationListLabel.Visible = false;

                    break;

                default:
                    _locationAliasButton.Visible = false;
                    _locationAliasLabel.Visible = false;
                    _locationAliasTextBox.Visible = false;
                    _locationListCheckBox.Visible = false;
                    _locationListLabel.Visible = false;

                    break;
            }
        }

        private void LocationRefresh()
        {
            _locationListBox.Items.Clear();
            _locationListBox.Items.AddRange(_locations.Select(l => l.BaseOrDefault as object).ToArray());
        }
    }
}