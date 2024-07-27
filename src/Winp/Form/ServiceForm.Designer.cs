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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm));
            this._statusImageList = new System.Windows.Forms.ImageList(this.components);
            this._controlStopButton = new System.Windows.Forms.Button();
            this._controlStartButton = new System.Windows.Forms.Button();
            this._controlConfigureButton = new System.Windows.Forms.Button();
            this._packageGroupBox = new System.Windows.Forms.GroupBox();
            this._packagePhpMyAdminStatusLabel = new System.Windows.Forms.Label();
            this._packagePhpMyAdminVariantComboBox = new System.Windows.Forms.ComboBox();
            this._packagePhpMyAdminLabel = new System.Windows.Forms.Label();
            this._packagePhpStatusLabel = new System.Windows.Forms.Label();
            this._packagePhpVariantComboBox = new System.Windows.Forms.ComboBox();
            this._packagePhpLabel = new System.Windows.Forms.Label();
            this._packageMariaDbStatusLabel = new System.Windows.Forms.Label();
            this._packageMariaDbVariantComboBox = new System.Windows.Forms.ComboBox();
            this._packageMariaDbLabel = new System.Windows.Forms.Label();
            this._packageNginxStatusLabel = new System.Windows.Forms.Label();
            this._packageNginxVariantComboBox = new System.Windows.Forms.ComboBox();
            this._packageNginxLabel = new System.Windows.Forms.Label();
            this._controlGroupBox = new System.Windows.Forms.GroupBox();
            this._packageGroupBox.SuspendLayout();
            this._controlGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _statusImageList
            // 
            this._statusImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._statusImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_statusImageList.ImageStream")));
            this._statusImageList.TransparentColor = System.Drawing.Color.Transparent;
            this._statusImageList.Images.SetKeyName(0, "error.png");
            this._statusImageList.Images.SetKeyName(1, "information.png");
            this._statusImageList.Images.SetKeyName(2, "accept.png");
            this._statusImageList.Images.SetKeyName(3, "hourglass.png");
            // 
            // _controlStopButton
            // 
            this._controlStopButton.Location = new System.Drawing.Point(261, 19);
            this._controlStopButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._controlStopButton.Name = "_controlStopButton";
            this._controlStopButton.Size = new System.Drawing.Size(120, 23);
            this._controlStopButton.TabIndex = 3;
            this._controlStopButton.Text = "Stop services";
            this._controlStopButton.UseVisualStyleBackColor = true;
            this._controlStopButton.Click += new System.EventHandler(this.ControlStopButton_Click);
            // 
            // _controlStartButton
            // 
            this._controlStartButton.Location = new System.Drawing.Point(133, 19);
            this._controlStartButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._controlStartButton.Name = "_controlStartButton";
            this._controlStartButton.Size = new System.Drawing.Size(120, 23);
            this._controlStartButton.TabIndex = 2;
            this._controlStartButton.Text = "Start services";
            this._controlStartButton.UseVisualStyleBackColor = true;
            this._controlStartButton.Click += new System.EventHandler(this.ControlStartButton_Click);
            // 
            // _controlConfigureButton
            // 
            this._controlConfigureButton.Location = new System.Drawing.Point(5, 19);
            this._controlConfigureButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._controlConfigureButton.Name = "_controlConfigureButton";
            this._controlConfigureButton.Size = new System.Drawing.Size(120, 23);
            this._controlConfigureButton.TabIndex = 0;
            this._controlConfigureButton.Text = "Configure";
            this._controlConfigureButton.UseVisualStyleBackColor = true;
            this._controlConfigureButton.Click += new System.EventHandler(this.ControlConfigureButton_Click);
            // 
            // _packageGroupBox
            // 
            this._packageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._packageGroupBox.Controls.Add(this._packagePhpMyAdminStatusLabel);
            this._packageGroupBox.Controls.Add(this._packagePhpMyAdminVariantComboBox);
            this._packageGroupBox.Controls.Add(this._packagePhpMyAdminLabel);
            this._packageGroupBox.Controls.Add(this._packagePhpStatusLabel);
            this._packageGroupBox.Controls.Add(this._packagePhpVariantComboBox);
            this._packageGroupBox.Controls.Add(this._packagePhpLabel);
            this._packageGroupBox.Controls.Add(this._packageMariaDbStatusLabel);
            this._packageGroupBox.Controls.Add(this._packageMariaDbVariantComboBox);
            this._packageGroupBox.Controls.Add(this._packageMariaDbLabel);
            this._packageGroupBox.Controls.Add(this._packageNginxStatusLabel);
            this._packageGroupBox.Controls.Add(this._packageNginxVariantComboBox);
            this._packageGroupBox.Controls.Add(this._packageNginxLabel);
            this._packageGroupBox.Location = new System.Drawing.Point(9, 7);
            this._packageGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._packageGroupBox.Name = "_packageGroupBox";
            this._packageGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._packageGroupBox.Size = new System.Drawing.Size(435, 126);
            this._packageGroupBox.TabIndex = 2;
            this._packageGroupBox.TabStop = false;
            this._packageGroupBox.Text = "Packages";
            // 
            // _packagePhpMyAdminStatusLabel
            // 
            this._packagePhpMyAdminStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._packagePhpMyAdminStatusLabel.ImageIndex = 0;
            this._packagePhpMyAdminStatusLabel.ImageList = this._statusImageList;
            this._packagePhpMyAdminStatusLabel.Location = new System.Drawing.Point(241, 97);
            this._packagePhpMyAdminStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packagePhpMyAdminStatusLabel.Name = "_packagePhpMyAdminStatusLabel";
            this._packagePhpMyAdminStatusLabel.Size = new System.Drawing.Size(162, 15);
            this._packagePhpMyAdminStatusLabel.TabIndex = 11;
            this._packagePhpMyAdminStatusLabel.Text = "Dummy";
            this._packagePhpMyAdminStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _packagePhpMyAdminVariantComboBox
            // 
            this._packagePhpMyAdminVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._packagePhpMyAdminVariantComboBox.FormattingEnabled = true;
            this._packagePhpMyAdminVariantComboBox.Location = new System.Drawing.Point(97, 94);
            this._packagePhpMyAdminVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._packagePhpMyAdminVariantComboBox.Name = "_packagePhpMyAdminVariantComboBox";
            this._packagePhpMyAdminVariantComboBox.Size = new System.Drawing.Size(140, 23);
            this._packagePhpMyAdminVariantComboBox.TabIndex = 10;
            // 
            // _packagePhpMyAdminLabel
            // 
            this._packagePhpMyAdminLabel.AutoSize = true;
            this._packagePhpMyAdminLabel.Location = new System.Drawing.Point(6, 97);
            this._packagePhpMyAdminLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packagePhpMyAdminLabel.Name = "_packagePhpMyAdminLabel";
            this._packagePhpMyAdminLabel.Size = new System.Drawing.Size(81, 15);
            this._packagePhpMyAdminLabel.TabIndex = 9;
            this._packagePhpMyAdminLabel.Text = "phpMyAdmin";
            // 
            // _packagePhpStatusLabel
            // 
            this._packagePhpStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._packagePhpStatusLabel.ImageIndex = 0;
            this._packagePhpStatusLabel.ImageList = this._statusImageList;
            this._packagePhpStatusLabel.Location = new System.Drawing.Point(241, 72);
            this._packagePhpStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packagePhpStatusLabel.Name = "_packagePhpStatusLabel";
            this._packagePhpStatusLabel.Size = new System.Drawing.Size(162, 15);
            this._packagePhpStatusLabel.TabIndex = 8;
            this._packagePhpStatusLabel.Text = "Dummy";
            this._packagePhpStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _packagePhpVariantComboBox
            // 
            this._packagePhpVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._packagePhpVariantComboBox.FormattingEnabled = true;
            this._packagePhpVariantComboBox.Location = new System.Drawing.Point(97, 69);
            this._packagePhpVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._packagePhpVariantComboBox.Name = "_packagePhpVariantComboBox";
            this._packagePhpVariantComboBox.Size = new System.Drawing.Size(140, 23);
            this._packagePhpVariantComboBox.TabIndex = 7;
            // 
            // _packagePhpLabel
            // 
            this._packagePhpLabel.AutoSize = true;
            this._packagePhpLabel.Location = new System.Drawing.Point(6, 72);
            this._packagePhpLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packagePhpLabel.Name = "_packagePhpLabel";
            this._packagePhpLabel.Size = new System.Drawing.Size(30, 15);
            this._packagePhpLabel.TabIndex = 6;
            this._packagePhpLabel.Text = "PHP";
            // 
            // _packageMariaDbStatusLabel
            // 
            this._packageMariaDbStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._packageMariaDbStatusLabel.ImageIndex = 0;
            this._packageMariaDbStatusLabel.ImageList = this._statusImageList;
            this._packageMariaDbStatusLabel.Location = new System.Drawing.Point(241, 22);
            this._packageMariaDbStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packageMariaDbStatusLabel.Name = "_packageMariaDbStatusLabel";
            this._packageMariaDbStatusLabel.Size = new System.Drawing.Size(162, 15);
            this._packageMariaDbStatusLabel.TabIndex = 5;
            this._packageMariaDbStatusLabel.Text = "Dummy";
            this._packageMariaDbStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _packageMariaDbVariantComboBox
            // 
            this._packageMariaDbVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._packageMariaDbVariantComboBox.FormattingEnabled = true;
            this._packageMariaDbVariantComboBox.Location = new System.Drawing.Point(97, 19);
            this._packageMariaDbVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._packageMariaDbVariantComboBox.Name = "_packageMariaDbVariantComboBox";
            this._packageMariaDbVariantComboBox.Size = new System.Drawing.Size(140, 23);
            this._packageMariaDbVariantComboBox.TabIndex = 4;
            // 
            // _packageMariaDbLabel
            // 
            this._packageMariaDbLabel.AutoSize = true;
            this._packageMariaDbLabel.Location = new System.Drawing.Point(6, 22);
            this._packageMariaDbLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packageMariaDbLabel.Name = "_packageMariaDbLabel";
            this._packageMariaDbLabel.Size = new System.Drawing.Size(52, 15);
            this._packageMariaDbLabel.TabIndex = 3;
            this._packageMariaDbLabel.Text = "MariaDB";
            // 
            // _packageNginxStatusLabel
            // 
            this._packageNginxStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._packageNginxStatusLabel.ImageIndex = 0;
            this._packageNginxStatusLabel.ImageList = this._statusImageList;
            this._packageNginxStatusLabel.Location = new System.Drawing.Point(241, 47);
            this._packageNginxStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packageNginxStatusLabel.Name = "_packageNginxStatusLabel";
            this._packageNginxStatusLabel.Size = new System.Drawing.Size(162, 15);
            this._packageNginxStatusLabel.TabIndex = 2;
            this._packageNginxStatusLabel.Text = "Dummy";
            this._packageNginxStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _packageNginxVariantComboBox
            // 
            this._packageNginxVariantComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._packageNginxVariantComboBox.FormattingEnabled = true;
            this._packageNginxVariantComboBox.Location = new System.Drawing.Point(97, 44);
            this._packageNginxVariantComboBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._packageNginxVariantComboBox.Name = "_packageNginxVariantComboBox";
            this._packageNginxVariantComboBox.Size = new System.Drawing.Size(140, 23);
            this._packageNginxVariantComboBox.TabIndex = 1;
            // 
            // _packageNginxLabel
            // 
            this._packageNginxLabel.AutoSize = true;
            this._packageNginxLabel.Location = new System.Drawing.Point(6, 47);
            this._packageNginxLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._packageNginxLabel.Name = "_packageNginxLabel";
            this._packageNginxLabel.Size = new System.Drawing.Size(39, 15);
            this._packageNginxLabel.TabIndex = 0;
            this._packageNginxLabel.Text = "Nginx";
            // 
            // _controlGroupBox
            // 
            this._controlGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._controlGroupBox.Controls.Add(this._controlStartButton);
            this._controlGroupBox.Controls.Add(this._controlStopButton);
            this._controlGroupBox.Controls.Add(this._controlConfigureButton);
            this._controlGroupBox.Location = new System.Drawing.Point(9, 137);
            this._controlGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._controlGroupBox.Name = "_controlGroupBox";
            this._controlGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this._controlGroupBox.Size = new System.Drawing.Size(435, 49);
            this._controlGroupBox.TabIndex = 8;
            this._controlGroupBox.TabStop = false;
            this._controlGroupBox.Text = "Controls";
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 195);
            this.Controls.Add(this._controlGroupBox);
            this.Controls.Add(this._packageGroupBox);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Winp Service";
            this.Shown += new System.EventHandler(this.ServiceForm_Shown);
            this._packageGroupBox.ResumeLayout(false);
            this._packageGroupBox.PerformLayout();
            this._controlGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button _controlConfigureButton;
        private System.Windows.Forms.Button _controlStartButton;
        private System.Windows.Forms.Button _controlStopButton;
        private System.Windows.Forms.ImageList _statusImageList;

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
    }
}