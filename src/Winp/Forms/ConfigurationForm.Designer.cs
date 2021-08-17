using System.ComponentModel;

namespace Winp.Forms
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._environmentGroupBox = new System.Windows.Forms.GroupBox();
            this._serverPortTextBox = new System.Windows.Forms.TextBox();
            this._serverPortLabel = new System.Windows.Forms.Label();
            this._serverAddressLabel = new System.Windows.Forms.Label();
            this._serverAddressTextBox = new System.Windows.Forms.TextBox();
            this._installDirectoryButton = new System.Windows.Forms.Button();
            this._installDirectoryTextBox = new System.Windows.Forms.TextBox();
            this._installDirectoryLabel = new System.Windows.Forms.Label();
            this._acceptButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._locationGroupBox = new System.Windows.Forms.GroupBox();
            this._locationIndexLabel = new System.Windows.Forms.Label();
            this._locationListCheckBox = new System.Windows.Forms.CheckBox();
            this._locationAliasButton = new System.Windows.Forms.Button();
            this._locationDeleteButton = new System.Windows.Forms.Button();
            this._locationUpdateButton = new System.Windows.Forms.Button();
            this._locationAliasTextBox = new System.Windows.Forms.TextBox();
            this._locationAliasLabel = new System.Windows.Forms.Label();
            this._locationTypeLabel = new System.Windows.Forms.Label();
            this._locationTypeComboBox = new System.Windows.Forms.ComboBox();
            this._locationBaseTextBox = new System.Windows.Forms.TextBox();
            this._locationBaseLabel = new System.Windows.Forms.Label();
            this._locationListBox = new System.Windows.Forms.ListBox();
            this._folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._locationIndexCheckBox = new System.Windows.Forms.CheckBox();
            this._environmentGroupBox.SuspendLayout();
            this._locationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _environmentGroupBox
            // 
            this._environmentGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._environmentGroupBox.Controls.Add(this._serverPortTextBox);
            this._environmentGroupBox.Controls.Add(this._serverPortLabel);
            this._environmentGroupBox.Controls.Add(this._serverAddressLabel);
            this._environmentGroupBox.Controls.Add(this._serverAddressTextBox);
            this._environmentGroupBox.Controls.Add(this._installDirectoryButton);
            this._environmentGroupBox.Controls.Add(this._installDirectoryTextBox);
            this._environmentGroupBox.Controls.Add(this._installDirectoryLabel);
            this._environmentGroupBox.Location = new System.Drawing.Point(10, 10);
            this._environmentGroupBox.Name = "_environmentGroupBox";
            this._environmentGroupBox.Size = new System.Drawing.Size(514, 75);
            this._environmentGroupBox.TabIndex = 0;
            this._environmentGroupBox.TabStop = false;
            this._environmentGroupBox.Text = "Environment";
            // 
            // _serverPortTextBox
            // 
            this._serverPortTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._serverPortTextBox.Location = new System.Drawing.Point(445, 44);
            this._serverPortTextBox.Name = "_serverPortTextBox";
            this._serverPortTextBox.Size = new System.Drawing.Size(59, 20);
            this._serverPortTextBox.TabIndex = 21;
            // 
            // _serverPortLabel
            // 
            this._serverPortLabel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._serverPortLabel.AutoSize = true;
            this._serverPortLabel.Location = new System.Drawing.Point(412, 47);
            this._serverPortLabel.Name = "_serverPortLabel";
            this._serverPortLabel.Size = new System.Drawing.Size(29, 13);
            this._serverPortLabel.TabIndex = 20;
            this._serverPortLabel.Text = "Port:";
            // 
            // _serverAddressLabel
            // 
            this._serverAddressLabel.AutoSize = true;
            this._serverAddressLabel.Location = new System.Drawing.Point(5, 47);
            this._serverAddressLabel.Name = "_serverAddressLabel";
            this._serverAddressLabel.Size = new System.Drawing.Size(81, 13);
            this._serverAddressLabel.TabIndex = 19;
            this._serverAddressLabel.Text = "Server address:";
            // 
            // _serverAddressTextBox
            // 
            this._serverAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._serverAddressTextBox.Location = new System.Drawing.Point(96, 44);
            this._serverAddressTextBox.Name = "_serverAddressTextBox";
            this._serverAddressTextBox.Size = new System.Drawing.Size(312, 20);
            this._serverAddressTextBox.TabIndex = 18;
            // 
            // _installDirectoryButton
            // 
            this._installDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._installDirectoryButton.Location = new System.Drawing.Point(474, 18);
            this._installDirectoryButton.Name = "_installDirectoryButton";
            this._installDirectoryButton.Size = new System.Drawing.Size(29, 20);
            this._installDirectoryButton.TabIndex = 17;
            this._installDirectoryButton.Text = "...";
            this._installDirectoryButton.UseVisualStyleBackColor = true;
            this._installDirectoryButton.Click += new System.EventHandler(this.InstallDirectoryButton_Click);
            // 
            // _installDirectoryTextBox
            // 
            this._installDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._installDirectoryTextBox.Location = new System.Drawing.Point(96, 19);
            this._installDirectoryTextBox.Name = "_installDirectoryTextBox";
            this._installDirectoryTextBox.Size = new System.Drawing.Size(374, 20);
            this._installDirectoryTextBox.TabIndex = 3;
            // 
            // _installDirectoryLabel
            // 
            this._installDirectoryLabel.AutoSize = true;
            this._installDirectoryLabel.Location = new System.Drawing.Point(5, 22);
            this._installDirectoryLabel.Name = "_installDirectoryLabel";
            this._installDirectoryLabel.Size = new System.Drawing.Size(80, 13);
            this._installDirectoryLabel.TabIndex = 2;
            this._installDirectoryLabel.Text = "Install directory:";
            // 
            // _acceptButton
            // 
            this._acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._acceptButton.Location = new System.Drawing.Point(10, 242);
            this._acceptButton.Name = "_acceptButton";
            this._acceptButton.Size = new System.Drawing.Size(86, 20);
            this._acceptButton.TabIndex = 9;
            this._acceptButton.Text = "Save";
            this._acceptButton.UseVisualStyleBackColor = true;
            this._acceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cancelButton.Location = new System.Drawing.Point(101, 242);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(86, 20);
            this._cancelButton.TabIndex = 10;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // _locationGroupBox
            // 
            this._locationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._locationGroupBox.Controls.Add(this._locationIndexCheckBox);
            this._locationGroupBox.Controls.Add(this._locationIndexLabel);
            this._locationGroupBox.Controls.Add(this._locationListCheckBox);
            this._locationGroupBox.Controls.Add(this._locationAliasButton);
            this._locationGroupBox.Controls.Add(this._locationDeleteButton);
            this._locationGroupBox.Controls.Add(this._locationUpdateButton);
            this._locationGroupBox.Controls.Add(this._locationAliasTextBox);
            this._locationGroupBox.Controls.Add(this._locationAliasLabel);
            this._locationGroupBox.Controls.Add(this._locationTypeLabel);
            this._locationGroupBox.Controls.Add(this._locationTypeComboBox);
            this._locationGroupBox.Controls.Add(this._locationBaseTextBox);
            this._locationGroupBox.Controls.Add(this._locationBaseLabel);
            this._locationGroupBox.Controls.Add(this._locationListBox);
            this._locationGroupBox.Location = new System.Drawing.Point(10, 89);
            this._locationGroupBox.Name = "_locationGroupBox";
            this._locationGroupBox.Size = new System.Drawing.Size(514, 147);
            this._locationGroupBox.TabIndex = 11;
            this._locationGroupBox.TabStop = false;
            this._locationGroupBox.Text = "Locations";
            // 
            // _locationIndexLabel
            // 
            this._locationIndexLabel.AutoSize = true;
            this._locationIndexLabel.Location = new System.Drawing.Point(182, 96);
            this._locationIndexLabel.Name = "_locationIndexLabel";
            this._locationIndexLabel.Size = new System.Drawing.Size(89, 13);
            this._locationIndexLabel.TabIndex = 17;
            this._locationIndexLabel.Text = "Directory options:";
            // 
            // _locationListCheckBox
            // 
            this._locationListCheckBox.Location = new System.Drawing.Point(273, 93);
            this._locationListCheckBox.Name = "_locationListCheckBox";
            this._locationListCheckBox.Size = new System.Drawing.Size(89, 21);
            this._locationListCheckBox.TabIndex = 16;
            this._locationListCheckBox.Text = "Allow listing";
            this._locationListCheckBox.UseVisualStyleBackColor = true;
            // 
            // _locationAliasButton
            // 
            this._locationAliasButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._locationAliasButton.Location = new System.Drawing.Point(475, 67);
            this._locationAliasButton.Name = "_locationAliasButton";
            this._locationAliasButton.Size = new System.Drawing.Size(29, 20);
            this._locationAliasButton.TabIndex = 14;
            this._locationAliasButton.Text = "...";
            this._locationAliasButton.UseVisualStyleBackColor = true;
            this._locationAliasButton.Click += new System.EventHandler(this.LocationAliasButton_Click);
            // 
            // _locationDeleteButton
            // 
            this._locationDeleteButton.Location = new System.Drawing.Point(273, 120);
            this._locationDeleteButton.Name = "_locationDeleteButton";
            this._locationDeleteButton.Size = new System.Drawing.Size(86, 20);
            this._locationDeleteButton.TabIndex = 13;
            this._locationDeleteButton.Text = "Delete";
            this._locationDeleteButton.UseVisualStyleBackColor = true;
            this._locationDeleteButton.Click += new System.EventHandler(this.LocationDeleteButton_Click);
            // 
            // _locationUpdateButton
            // 
            this._locationUpdateButton.Location = new System.Drawing.Point(182, 120);
            this._locationUpdateButton.Name = "_locationUpdateButton";
            this._locationUpdateButton.Size = new System.Drawing.Size(86, 20);
            this._locationUpdateButton.TabIndex = 12;
            this._locationUpdateButton.Text = "Update";
            this._locationUpdateButton.UseVisualStyleBackColor = true;
            this._locationUpdateButton.Click += new System.EventHandler(this.LocationUpdateButton_Click);
            // 
            // _locationAliasTextBox
            // 
            this._locationAliasTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._locationAliasTextBox.Location = new System.Drawing.Point(273, 67);
            this._locationAliasTextBox.Name = "_locationAliasTextBox";
            this._locationAliasTextBox.Size = new System.Drawing.Size(198, 20);
            this._locationAliasTextBox.TabIndex = 8;
            // 
            // _locationAliasLabel
            // 
            this._locationAliasLabel.AutoSize = true;
            this._locationAliasLabel.Location = new System.Drawing.Point(182, 70);
            this._locationAliasLabel.Name = "_locationAliasLabel";
            this._locationAliasLabel.Size = new System.Drawing.Size(76, 13);
            this._locationAliasLabel.TabIndex = 7;
            this._locationAliasLabel.Text = "Root directory:";
            // 
            // _locationTypeLabel
            // 
            this._locationTypeLabel.AutoSize = true;
            this._locationTypeLabel.Location = new System.Drawing.Point(182, 44);
            this._locationTypeLabel.Name = "_locationTypeLabel";
            this._locationTypeLabel.Size = new System.Drawing.Size(34, 13);
            this._locationTypeLabel.TabIndex = 6;
            this._locationTypeLabel.Text = "Type:";
            // 
            // _locationTypeComboBox
            // 
            this._locationTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._locationTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._locationTypeComboBox.FormattingEnabled = true;
            this._locationTypeComboBox.Location = new System.Drawing.Point(273, 42);
            this._locationTypeComboBox.Name = "_locationTypeComboBox";
            this._locationTypeComboBox.Size = new System.Drawing.Size(231, 21);
            this._locationTypeComboBox.TabIndex = 5;
            this._locationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.LocationTypeComboBox_SelectedIndexChanged);
            // 
            // _locationBaseTextBox
            // 
            this._locationBaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._locationBaseTextBox.Location = new System.Drawing.Point(273, 16);
            this._locationBaseTextBox.Name = "_locationBaseTextBox";
            this._locationBaseTextBox.Size = new System.Drawing.Size(231, 20);
            this._locationBaseTextBox.TabIndex = 4;
            // 
            // _locationBaseLabel
            // 
            this._locationBaseLabel.AutoSize = true;
            this._locationBaseLabel.Location = new System.Drawing.Point(182, 18);
            this._locationBaseLabel.Name = "_locationBaseLabel";
            this._locationBaseLabel.Size = new System.Drawing.Size(59, 13);
            this._locationBaseLabel.TabIndex = 4;
            this._locationBaseLabel.Text = "Base URL:";
            // 
            // _locationListBox
            // 
            this._locationListBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this._locationListBox.FormattingEnabled = true;
            this._locationListBox.IntegralHeight = false;
            this._locationListBox.Location = new System.Drawing.Point(5, 19);
            this._locationListBox.Name = "_locationListBox";
            this._locationListBox.Size = new System.Drawing.Size(172, 121);
            this._locationListBox.TabIndex = 0;
            this._locationListBox.SelectedIndexChanged += new System.EventHandler(this.LocationListBox_SelectedIndexChanged);
            // 
            // _folderBrowserDialog
            // 
            this._folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // _locationIndexCheckBox
            // 
            this._locationIndexCheckBox.Location = new System.Drawing.Point(352, 93);
            this._locationIndexCheckBox.Name = "_locationIndexCheckBox";
            this._locationIndexCheckBox.Size = new System.Drawing.Size(89, 21);
            this._locationIndexCheckBox.TabIndex = 18;
            this._locationIndexCheckBox.Text = "Use index";
            this._locationIndexCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 272);
            this.Controls.Add(this._locationGroupBox);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._acceptButton);
            this.Controls.Add(this._environmentGroupBox);
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Winp Configuration";
            this._environmentGroupBox.ResumeLayout(false);
            this._environmentGroupBox.PerformLayout();
            this._locationGroupBox.ResumeLayout(false);
            this._locationGroupBox.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button _acceptButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.GroupBox _environmentGroupBox;
        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.Button _installDirectoryButton;
        private System.Windows.Forms.Label _installDirectoryLabel;
        private System.Windows.Forms.TextBox _installDirectoryTextBox;
        private System.Windows.Forms.Button _locationAliasButton;
        private System.Windows.Forms.Label _locationAliasLabel;
        private System.Windows.Forms.TextBox _locationAliasTextBox;
        private System.Windows.Forms.Label _locationBaseLabel;
        private System.Windows.Forms.TextBox _locationBaseTextBox;
        private System.Windows.Forms.Button _locationDeleteButton;
        private System.Windows.Forms.GroupBox _locationGroupBox;
        private System.Windows.Forms.CheckBox _locationIndexCheckBox;
        private System.Windows.Forms.Label _locationIndexLabel;
        private System.Windows.Forms.ListBox _locationListBox;
        private System.Windows.Forms.CheckBox _locationListCheckBox;
        private System.Windows.Forms.ComboBox _locationTypeComboBox;
        private System.Windows.Forms.Label _locationTypeLabel;
        private System.Windows.Forms.Button _locationUpdateButton;
        private System.Windows.Forms.Label _serverAddressLabel;
        private System.Windows.Forms.TextBox _serverAddressTextBox;
        private System.Windows.Forms.Label _serverPortLabel;
        private System.Windows.Forms.TextBox _serverPortTextBox;

        #endregion
    }
}