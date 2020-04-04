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
            this._installDirectoryTextBox = new System.Windows.Forms.TextBox();
            this._installDirectoryLabel = new System.Windows.Forms.Label();
            this._acceptButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._locationGroupBox = new System.Windows.Forms.GroupBox();
            this._locationListCheckBox = new System.Windows.Forms.CheckBox();
            this._locationListLabel = new System.Windows.Forms.Label();
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
            this._installDirectoryButton = new System.Windows.Forms.Button();
            this._environmentGroupBox.SuspendLayout();
            this._locationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _environmentGroupBox
            // 
            this._environmentGroupBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._environmentGroupBox.Controls.Add(this._installDirectoryButton);
            this._environmentGroupBox.Controls.Add(this._installDirectoryTextBox);
            this._environmentGroupBox.Controls.Add(this._installDirectoryLabel);
            this._environmentGroupBox.Location = new System.Drawing.Point(12, 12);
            this._environmentGroupBox.Name = "_environmentGroupBox";
            this._environmentGroupBox.Size = new System.Drawing.Size(600, 57);
            this._environmentGroupBox.TabIndex = 0;
            this._environmentGroupBox.TabStop = false;
            this._environmentGroupBox.Text = "Environment";
            // 
            // _installDirectoryTextBox
            // 
            this._installDirectoryTextBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._installDirectoryTextBox.Location = new System.Drawing.Point(112, 22);
            this._installDirectoryTextBox.Name = "_installDirectoryTextBox";
            this._installDirectoryTextBox.Size = new System.Drawing.Size(436, 23);
            this._installDirectoryTextBox.TabIndex = 3;
            // 
            // _installDirectoryLabel
            // 
            this._installDirectoryLabel.AutoSize = true;
            this._installDirectoryLabel.Location = new System.Drawing.Point(6, 25);
            this._installDirectoryLabel.Name = "_installDirectoryLabel";
            this._installDirectoryLabel.Size = new System.Drawing.Size(91, 15);
            this._installDirectoryLabel.TabIndex = 2;
            this._installDirectoryLabel.Text = "Install directory:";
            // 
            // _acceptButton
            // 
            this._acceptButton.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Left)));
            this._acceptButton.Location = new System.Drawing.Point(12, 256);
            this._acceptButton.Name = "_acceptButton";
            this._acceptButton.Size = new System.Drawing.Size(100, 23);
            this._acceptButton.TabIndex = 9;
            this._acceptButton.Text = "Save";
            this._acceptButton.UseVisualStyleBackColor = true;
            this._acceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Left)));
            this._cancelButton.Location = new System.Drawing.Point(118, 256);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(100, 23);
            this._cancelButton.TabIndex = 10;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // _locationGroupBox
            // 
            this._locationGroupBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top |
                                                         System.Windows.Forms.AnchorStyles.Bottom) |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._locationGroupBox.Controls.Add(this._locationListCheckBox);
            this._locationGroupBox.Controls.Add(this._locationListLabel);
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
            this._locationGroupBox.Location = new System.Drawing.Point(12, 74);
            this._locationGroupBox.Name = "_locationGroupBox";
            this._locationGroupBox.Size = new System.Drawing.Size(600, 177);
            this._locationGroupBox.TabIndex = 11;
            this._locationGroupBox.TabStop = false;
            this._locationGroupBox.Text = "Locations";
            // 
            // _locationListCheckBox
            // 
            this._locationListCheckBox.Location = new System.Drawing.Point(318, 107);
            this._locationListCheckBox.Name = "_locationListCheckBox";
            this._locationListCheckBox.Size = new System.Drawing.Size(104, 24);
            this._locationListCheckBox.TabIndex = 16;
            this._locationListCheckBox.Text = "Allowed";
            this._locationListCheckBox.UseVisualStyleBackColor = true;
            // 
            // _locationListLabel
            // 
            this._locationListLabel.AutoSize = true;
            this._locationListLabel.Location = new System.Drawing.Point(212, 111);
            this._locationListLabel.Name = "_locationListLabel";
            this._locationListLabel.Size = new System.Drawing.Size(93, 15);
            this._locationListLabel.TabIndex = 15;
            this._locationListLabel.Text = "Directory listing:";
            // 
            // _locationAliasButton
            // 
            this._locationAliasButton.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._locationAliasButton.Location = new System.Drawing.Point(554, 77);
            this._locationAliasButton.Name = "_locationAliasButton";
            this._locationAliasButton.Size = new System.Drawing.Size(34, 23);
            this._locationAliasButton.TabIndex = 14;
            this._locationAliasButton.Text = "...";
            this._locationAliasButton.UseVisualStyleBackColor = true;
            this._locationAliasButton.Click += new System.EventHandler(this.LocationAliasButton_Click);
            // 
            // _locationDeleteButton
            // 
            this._locationDeleteButton.Location = new System.Drawing.Point(318, 138);
            this._locationDeleteButton.Name = "_locationDeleteButton";
            this._locationDeleteButton.Size = new System.Drawing.Size(100, 23);
            this._locationDeleteButton.TabIndex = 13;
            this._locationDeleteButton.Text = "Delete";
            this._locationDeleteButton.UseVisualStyleBackColor = true;
            this._locationDeleteButton.Click += new System.EventHandler(this.LocationDeleteButton_Click);
            // 
            // _locationUpdateButton
            // 
            this._locationUpdateButton.Location = new System.Drawing.Point(212, 138);
            this._locationUpdateButton.Name = "_locationUpdateButton";
            this._locationUpdateButton.Size = new System.Drawing.Size(100, 23);
            this._locationUpdateButton.TabIndex = 12;
            this._locationUpdateButton.Text = "Update";
            this._locationUpdateButton.UseVisualStyleBackColor = true;
            this._locationUpdateButton.Click += new System.EventHandler(this.LocationUpdateButton_Click);
            // 
            // _locationAliasTextBox
            // 
            this._locationAliasTextBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._locationAliasTextBox.Location = new System.Drawing.Point(318, 77);
            this._locationAliasTextBox.Name = "_locationAliasTextBox";
            this._locationAliasTextBox.Size = new System.Drawing.Size(230, 23);
            this._locationAliasTextBox.TabIndex = 8;
            // 
            // _locationAliasLabel
            // 
            this._locationAliasLabel.AutoSize = true;
            this._locationAliasLabel.Location = new System.Drawing.Point(212, 81);
            this._locationAliasLabel.Name = "_locationAliasLabel";
            this._locationAliasLabel.Size = new System.Drawing.Size(85, 15);
            this._locationAliasLabel.TabIndex = 7;
            this._locationAliasLabel.Text = "Root directory:";
            // 
            // _locationTypeLabel
            // 
            this._locationTypeLabel.AutoSize = true;
            this._locationTypeLabel.Location = new System.Drawing.Point(212, 51);
            this._locationTypeLabel.Name = "_locationTypeLabel";
            this._locationTypeLabel.Size = new System.Drawing.Size(34, 15);
            this._locationTypeLabel.TabIndex = 6;
            this._locationTypeLabel.Text = "Type:";
            // 
            // _locationTypeComboBox
            // 
            this._locationTypeComboBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._locationTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._locationTypeComboBox.FormattingEnabled = true;
            this._locationTypeComboBox.Location = new System.Drawing.Point(318, 48);
            this._locationTypeComboBox.Name = "_locationTypeComboBox";
            this._locationTypeComboBox.Size = new System.Drawing.Size(269, 23);
            this._locationTypeComboBox.TabIndex = 5;
            this._locationTypeComboBox.SelectedIndexChanged +=
                new System.EventHandler(this.LocationTypeComboBox_SelectedIndexChanged);
            // 
            // _locationBaseTextBox
            // 
            this._locationBaseTextBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._locationBaseTextBox.Location = new System.Drawing.Point(318, 18);
            this._locationBaseTextBox.Name = "_locationBaseTextBox";
            this._locationBaseTextBox.Size = new System.Drawing.Size(269, 23);
            this._locationBaseTextBox.TabIndex = 4;
            // 
            // _locationBaseLabel
            // 
            this._locationBaseLabel.AutoSize = true;
            this._locationBaseLabel.Location = new System.Drawing.Point(212, 21);
            this._locationBaseLabel.Name = "_locationBaseLabel";
            this._locationBaseLabel.Size = new System.Drawing.Size(58, 15);
            this._locationBaseLabel.TabIndex = 4;
            this._locationBaseLabel.Text = "Base URL:";
            // 
            // _locationListBox
            // 
            this._locationListBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Bottom) |
                                                       System.Windows.Forms.AnchorStyles.Left)));
            this._locationListBox.FormattingEnabled = true;
            this._locationListBox.IntegralHeight = false;
            this._locationListBox.ItemHeight = 15;
            this._locationListBox.Location = new System.Drawing.Point(6, 22);
            this._locationListBox.Name = "_locationListBox";
            this._locationListBox.Size = new System.Drawing.Size(200, 140);
            this._locationListBox.TabIndex = 0;
            this._locationListBox.SelectedIndexChanged +=
                new System.EventHandler(this.LocationListBox_SelectedIndexChanged);
            // 
            // _folderBrowserDialog
            // 
            this._folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // _installDirectoryButton
            // 
            this._installDirectoryButton.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._installDirectoryButton.Location = new System.Drawing.Point(553, 21);
            this._installDirectoryButton.Name = "_installDirectoryButton";
            this._installDirectoryButton.Size = new System.Drawing.Size(34, 23);
            this._installDirectoryButton.TabIndex = 17;
            this._installDirectoryButton.Text = "...";
            this._installDirectoryButton.UseVisualStyleBackColor = true;
            this._installDirectoryButton.Click += new System.EventHandler(this.InstallDirectoryButton_Click);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 291);
            this.Controls.Add(this._locationGroupBox);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._acceptButton);
            this.Controls.Add(this._environmentGroupBox);
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this._environmentGroupBox.ResumeLayout(false);
            this._environmentGroupBox.PerformLayout();
            this._locationGroupBox.ResumeLayout(false);
            this._locationGroupBox.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox _environmentGroupBox;
        private System.Windows.Forms.GroupBox _locationGroupBox;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.ListBox _locationListBox;
        private System.Windows.Forms.Label _locationBaseLabel;
        private System.Windows.Forms.TextBox _locationBaseTextBox;
        private System.Windows.Forms.ComboBox _locationTypeComboBox;
        private System.Windows.Forms.Label _locationTypeLabel;
        private System.Windows.Forms.Button _locationDeleteButton;
        private System.Windows.Forms.Button _acceptButton;
        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.Button _locationAliasButton;
        private System.Windows.Forms.Label _locationAliasLabel;
        private System.Windows.Forms.TextBox _locationAliasTextBox;
        private System.Windows.Forms.Label _locationListLabel;
        private System.Windows.Forms.CheckBox _locationListCheckBox;
        private System.Windows.Forms.Button _locationUpdateButton;
        private System.Windows.Forms.Button _installDirectoryButton;
        private System.Windows.Forms.TextBox _installDirectoryTextBox;
        private System.Windows.Forms.Label _installDirectoryLabel;
    }
}