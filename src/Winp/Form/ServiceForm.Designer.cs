using System.ComponentModel;

namespace Winp.Form
{
    partial class ServiceForm
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
            _controlStopButton = new System.Windows.Forms.Button();
            _controlStartButton = new System.Windows.Forms.Button();
            _controlConfigureButton = new System.Windows.Forms.Button();
            _controlMinimizeToTrayButton = new System.Windows.Forms.Button();
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _packageGroupBox = new System.Windows.Forms.GroupBox();
            _packagePhpMyAdminStatusLabel = new System.Windows.Forms.Label();
            _packagePhpMyAdminVariantComboBox = new System.Windows.Forms.ComboBox();
            _packagePhpMyAdminLabel = new System.Windows.Forms.Label();
            _packagePhpStatusLabel = new System.Windows.Forms.Label();
            _packagePhpVariantComboBox = new System.Windows.Forms.ComboBox();
            _packagePhpLabel = new System.Windows.Forms.Label();
            _packageMariaDbStatusLabel = new System.Windows.Forms.Label();
            _packageMariaDbVariantComboBox = new System.Windows.Forms.ComboBox();
            _packageMariaDbLabel = new System.Windows.Forms.Label();
            _packageNginxStatusLabel = new System.Windows.Forms.Label();
            _packageNginxVariantComboBox = new System.Windows.Forms.ComboBox();
            _packageNginxLabel = new System.Windows.Forms.Label();
            _controlGroupBox = new System.Windows.Forms.GroupBox();
            _controlBrowserButton = new System.Windows.Forms.Button();
            _packageGroupBox.SuspendLayout();
            _controlGroupBox.SuspendLayout();
            SuspendLayout();
            //
            // _controlStopButton
            //
            _controlStopButton.Location = new System.Drawing.Point(222, 19);
            _controlStopButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _controlStopButton.Name = "_controlStopButton";
            _controlStopButton.Size = new System.Drawing.Size(100, 23);
            _controlStopButton.TabIndex = 3;
            _controlStopButton.Text = "Stop services";
            _controlStopButton.UseVisualStyleBackColor = true;
            _controlStopButton.Click += ControlStopButton_Click;
            //
            // _controlStartButton
            //
            _controlStartButton.Location = new System.Drawing.Point(114, 19);
            _controlStartButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _controlStartButton.Name = "_controlStartButton";
            _controlStartButton.Size = new System.Drawing.Size(100, 23);
            _controlStartButton.TabIndex = 2;
            _controlStartButton.Text = "Start services";
            _controlStartButton.UseVisualStyleBackColor = true;
            _controlStartButton.Click += ControlStartButton_Click;
            //
            // _controlConfigureButton
            //
            _controlConfigureButton.Location = new System.Drawing.Point(6, 19);
            _controlConfigureButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _controlConfigureButton.Name = "_controlConfigureButton";
            _controlConfigureButton.Size = new System.Drawing.Size(100, 23);
            _controlConfigureButton.TabIndex = 0;
            _controlConfigureButton.Text = "Configure";
            _controlConfigureButton.UseVisualStyleBackColor = true;
            _controlConfigureButton.Click += ControlConfigureButton_Click;
            //
            // _controlMinimizeToTrayButton
            //
            _controlMinimizeToTrayButton.Location = new System.Drawing.Point(438, 19);
            _controlMinimizeToTrayButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _controlMinimizeToTrayButton.Name = "_controlMinimizeToTrayButton";
            _controlMinimizeToTrayButton.Size = new System.Drawing.Size(100, 23);
            _controlMinimizeToTrayButton.TabIndex = 5;
            _controlMinimizeToTrayButton.Text = "Minimize to tray";
            _controlMinimizeToTrayButton.UseVisualStyleBackColor = true;
            _controlMinimizeToTrayButton.Click += ControlMinimizeToTrayButton_Click;
            //
            // _notifyIcon
            //
            _notifyIcon.Text = "Winp Service";
            _notifyIcon.Visible = false;
            _notifyIcon.Click += NotifyIcon_Click;
            //
            // _packageGroupBox
            //
            _packageGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _packageGroupBox.Controls.Add(_packagePhpMyAdminStatusLabel);
            _packageGroupBox.Controls.Add(_packagePhpMyAdminVariantComboBox);
            _packageGroupBox.Controls.Add(_packagePhpMyAdminLabel);
            _packageGroupBox.Controls.Add(_packagePhpStatusLabel);
            _packageGroupBox.Controls.Add(_packagePhpVariantComboBox);
            _packageGroupBox.Controls.Add(_packagePhpLabel);
            _packageGroupBox.Controls.Add(_packageMariaDbStatusLabel);
            _packageGroupBox.Controls.Add(_packageMariaDbVariantComboBox);
            _packageGroupBox.Controls.Add(_packageMariaDbLabel);
            _packageGroupBox.Controls.Add(_packageNginxStatusLabel);
            _packageGroupBox.Controls.Add(_packageNginxVariantComboBox);
            _packageGroupBox.Controls.Add(_packageNginxLabel);
            _packageGroupBox.Location = new System.Drawing.Point(9, 7);
            _packageGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _packageGroupBox.Name = "_packageGroupBox";
            _packageGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _packageGroupBox.Size = new System.Drawing.Size(435, 126);
            _packageGroupBox.TabIndex = 2;
            _packageGroupBox.TabStop = false;
            _packageGroupBox.Text = "Packages";
            //
            // _packagePhpMyAdminStatusLabel
            //
            _packagePhpMyAdminStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _packagePhpMyAdminStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            _packagePhpMyAdminStatusLabel.ImageIndex = 0;
            _packagePhpMyAdminStatusLabel.Location = new System.Drawing.Point(265, 97);
            _packagePhpMyAdminStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packagePhpMyAdminStatusLabel.Name = "_packagePhpMyAdminStatusLabel";
            _packagePhpMyAdminStatusLabel.Size = new System.Drawing.Size(162, 15);
            _packagePhpMyAdminStatusLabel.TabIndex = 11;
            _packagePhpMyAdminStatusLabel.Text = "Dummy";
            _packagePhpMyAdminStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // _packagePhpMyAdminVariantComboBox
            //
            _packagePhpMyAdminVariantComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            _packagePhpMyAdminVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _packagePhpMyAdminVariantComboBox.FormattingEnabled = true;
            _packagePhpMyAdminVariantComboBox.Location = new System.Drawing.Point(97, 96);
            _packagePhpMyAdminVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            _packagePhpMyAdminVariantComboBox.Name = "_packagePhpMyAdminVariantComboBox";
            _packagePhpMyAdminVariantComboBox.Size = new System.Drawing.Size(166, 23);
            _packagePhpMyAdminVariantComboBox.TabIndex = 10;
            //
            // _packagePhpMyAdminLabel
            //
            _packagePhpMyAdminLabel.AutoSize = true;
            _packagePhpMyAdminLabel.Location = new System.Drawing.Point(6, 97);
            _packagePhpMyAdminLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packagePhpMyAdminLabel.Name = "_packagePhpMyAdminLabel";
            _packagePhpMyAdminLabel.Size = new System.Drawing.Size(81, 15);
            _packagePhpMyAdminLabel.TabIndex = 9;
            _packagePhpMyAdminLabel.Text = "phpMyAdmin";
            //
            // _packagePhpStatusLabel
            //
            _packagePhpStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _packagePhpStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            _packagePhpStatusLabel.ImageIndex = 0;
            _packagePhpStatusLabel.Location = new System.Drawing.Point(265, 72);
            _packagePhpStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packagePhpStatusLabel.Name = "_packagePhpStatusLabel";
            _packagePhpStatusLabel.Size = new System.Drawing.Size(162, 15);
            _packagePhpStatusLabel.TabIndex = 8;
            _packagePhpStatusLabel.Text = "Dummy";
            _packagePhpStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // _packagePhpVariantComboBox
            //
            _packagePhpVariantComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            _packagePhpVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _packagePhpVariantComboBox.FormattingEnabled = true;
            _packagePhpVariantComboBox.Location = new System.Drawing.Point(97, 71);
            _packagePhpVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            _packagePhpVariantComboBox.Name = "_packagePhpVariantComboBox";
            _packagePhpVariantComboBox.Size = new System.Drawing.Size(166, 23);
            _packagePhpVariantComboBox.TabIndex = 7;
            //
            // _packagePhpLabel
            //
            _packagePhpLabel.AutoSize = true;
            _packagePhpLabel.Location = new System.Drawing.Point(6, 72);
            _packagePhpLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packagePhpLabel.Name = "_packagePhpLabel";
            _packagePhpLabel.Size = new System.Drawing.Size(30, 15);
            _packagePhpLabel.TabIndex = 6;
            _packagePhpLabel.Text = "PHP";
            //
            // _packageMariaDbStatusLabel
            //
            _packageMariaDbStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _packageMariaDbStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            _packageMariaDbStatusLabel.ImageIndex = 0;
            _packageMariaDbStatusLabel.Location = new System.Drawing.Point(265, 22);
            _packageMariaDbStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packageMariaDbStatusLabel.Name = "_packageMariaDbStatusLabel";
            _packageMariaDbStatusLabel.Size = new System.Drawing.Size(162, 15);
            _packageMariaDbStatusLabel.TabIndex = 5;
            _packageMariaDbStatusLabel.Text = "Dummy";
            _packageMariaDbStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // _packageMariaDbVariantComboBox
            //
            _packageMariaDbVariantComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            _packageMariaDbVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _packageMariaDbVariantComboBox.FormattingEnabled = true;
            _packageMariaDbVariantComboBox.Location = new System.Drawing.Point(97, 21);
            _packageMariaDbVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            _packageMariaDbVariantComboBox.Name = "_packageMariaDbVariantComboBox";
            _packageMariaDbVariantComboBox.Size = new System.Drawing.Size(166, 23);
            _packageMariaDbVariantComboBox.TabIndex = 4;
            //
            // _packageMariaDbLabel
            //
            _packageMariaDbLabel.AutoSize = true;
            _packageMariaDbLabel.Location = new System.Drawing.Point(6, 22);
            _packageMariaDbLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packageMariaDbLabel.Name = "_packageMariaDbLabel";
            _packageMariaDbLabel.Size = new System.Drawing.Size(52, 15);
            _packageMariaDbLabel.TabIndex = 3;
            _packageMariaDbLabel.Text = "MariaDB";
            //
            // _packageNginxStatusLabel
            //
            _packageNginxStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _packageNginxStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            _packageNginxStatusLabel.ImageIndex = 0;
            _packageNginxStatusLabel.Location = new System.Drawing.Point(265, 47);
            _packageNginxStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packageNginxStatusLabel.Name = "_packageNginxStatusLabel";
            _packageNginxStatusLabel.Size = new System.Drawing.Size(162, 15);
            _packageNginxStatusLabel.TabIndex = 2;
            _packageNginxStatusLabel.Text = "Dummy";
            _packageNginxStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // _packageNginxVariantComboBox
            //
            _packageNginxVariantComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            _packageNginxVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _packageNginxVariantComboBox.FormattingEnabled = true;
            _packageNginxVariantComboBox.Location = new System.Drawing.Point(97, 45);
            _packageNginxVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            _packageNginxVariantComboBox.Name = "_packageNginxVariantComboBox";
            _packageNginxVariantComboBox.Size = new System.Drawing.Size(166, 23);
            _packageNginxVariantComboBox.TabIndex = 1;
            //
            // _packageNginxLabel
            //
            _packageNginxLabel.AutoSize = true;
            _packageNginxLabel.Location = new System.Drawing.Point(6, 47);
            _packageNginxLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            _packageNginxLabel.Name = "_packageNginxLabel";
            _packageNginxLabel.Size = new System.Drawing.Size(39, 15);
            _packageNginxLabel.TabIndex = 0;
            _packageNginxLabel.Text = "Nginx";
            //
            // _controlGroupBox
            //
            _controlGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _controlGroupBox.Controls.Add(_controlMinimizeToTrayButton);
            _controlGroupBox.Controls.Add(_controlBrowserButton);
            _controlGroupBox.Controls.Add(_controlStartButton);
            _controlGroupBox.Controls.Add(_controlStopButton);
            _controlGroupBox.Controls.Add(_controlConfigureButton);
            _controlGroupBox.Location = new System.Drawing.Point(9, 137);
            _controlGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            _controlGroupBox.Name = "_controlGroupBox";
            _controlGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            _controlGroupBox.Size = new System.Drawing.Size(552, 49);
            _controlGroupBox.TabIndex = 8;
            _controlGroupBox.TabStop = false;
            _controlGroupBox.Text = "Controls";
            //
            // _controlBrowserButton
            //
            _controlBrowserButton.Location = new System.Drawing.Point(330, 19);
            _controlBrowserButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            _controlBrowserButton.Name = "_controlBrowserButton";
            _controlBrowserButton.Size = new System.Drawing.Size(100, 23);
            _controlBrowserButton.TabIndex = 4;
            _controlBrowserButton.Text = "Open browser";
            _controlBrowserButton.UseVisualStyleBackColor = true;
            _controlBrowserButton.Click += ControlBrowserButton_Click;
            //
            // ServiceForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(552, 195);
            Controls.Add(_controlGroupBox);
            Controls.Add(_packageGroupBox);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ServiceForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Winp Service";
            Shown += ServiceForm_Shown;
            _packageGroupBox.ResumeLayout(false);
            _packageGroupBox.PerformLayout();
            _controlGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Button _controlConfigureButton;
        private System.Windows.Forms.Button _controlStartButton;
        private System.Windows.Forms.Button _controlStopButton;
        private System.Windows.Forms.Button _controlMinimizeToTrayButton;
        private System.Windows.Forms.NotifyIcon _notifyIcon;

        #endregion

        private System.Windows.Forms.GroupBox _packageGroupBox;
        private System.Windows.Forms.Label _packageNginxStatusLabel;
        private System.Windows.Forms.ComboBox _packageNginxVariantComboBox;
        private System.Windows.Forms.Label _packageNginxLabel;
        private System.Windows.Forms.Label _packagePhpMyAdminStatusLabel;
        private System.Windows.Forms.ComboBox _packagePhpMyAdminVariantComboBox;
        private System.Windows.Forms.Label _packagePhpMyAdminLabel;
        private System.Windows.Forms.Label _packagePhpStatusLabel;
        private System.Windows.Forms.ComboBox _packagePhpVariantComboBox;
        private System.Windows.Forms.Label _packagePhpLabel;
        private System.Windows.Forms.Label _packageMariaDbStatusLabel;
        private System.Windows.Forms.ComboBox _packageMariaDbVariantComboBox;
        private System.Windows.Forms.Label _packageMariaDbLabel;
        private System.Windows.Forms.GroupBox _controlGroupBox;
        private System.Windows.Forms.Button _controlBrowserButton;
    }
}