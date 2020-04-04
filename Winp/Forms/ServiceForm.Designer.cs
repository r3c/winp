using System.ComponentModel;

namespace Winp.Forms
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm));
            this._executionGroupBox = new System.Windows.Forms.GroupBox();
            this._executeStatusLabel = new System.Windows.Forms.Label();
            this._statusImageList = new System.Windows.Forms.ImageList(this.components);
            this._executeStopButton = new System.Windows.Forms.Button();
            this._executeStartButton = new System.Windows.Forms.Button();
            this._configurationGroupBox = new System.Windows.Forms.GroupBox();
            this._configureButton = new System.Windows.Forms.Button();
            this._installStatusLabel = new System.Windows.Forms.Label();
            this._installButton = new System.Windows.Forms.Button();
            this._executionGroupBox.SuspendLayout();
            this._configurationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _executionGroupBox
            // 
            this._executionGroupBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._executionGroupBox.Controls.Add(this._executeStatusLabel);
            this._executionGroupBox.Controls.Add(this._executeStopButton);
            this._executionGroupBox.Controls.Add(this._executeStartButton);
            this._executionGroupBox.Location = new System.Drawing.Point(12, 74);
            this._executionGroupBox.Name = "_executionGroupBox";
            this._executionGroupBox.Size = new System.Drawing.Size(598, 57);
            this._executionGroupBox.TabIndex = 3;
            this._executionGroupBox.TabStop = false;
            this._executionGroupBox.Text = "Execution";
            // 
            // _executeStatusLabel
            // 
            this._executeStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._executeStatusLabel.ImageIndex = 0;
            this._executeStatusLabel.ImageList = this._statusImageList;
            this._executeStatusLabel.Location = new System.Drawing.Point(218, 22);
            this._executeStatusLabel.Name = "_executeStatusLabel";
            this._executeStatusLabel.Size = new System.Drawing.Size(374, 23);
            this._executeStatusLabel.TabIndex = 7;
            this._executeStatusLabel.Text = "serviceStatusLabel";
            this._executeStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._executeStatusLabel.Visible = false;
            // 
            // _statusImageList
            // 
            this._statusImageList.ImageStream =
                ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("_statusImageList.ImageStream")));
            this._statusImageList.TransparentColor = System.Drawing.Color.Transparent;
            this._statusImageList.Images.SetKeyName(0, "error.png");
            this._statusImageList.Images.SetKeyName(1, "information.png");
            this._statusImageList.Images.SetKeyName(2, "accept.png");
            this._statusImageList.Images.SetKeyName(3, "hourglass.png");
            // 
            // _executeStopButton
            // 
            this._executeStopButton.Location = new System.Drawing.Point(112, 22);
            this._executeStopButton.Name = "_executeStopButton";
            this._executeStopButton.Size = new System.Drawing.Size(100, 23);
            this._executeStopButton.TabIndex = 3;
            this._executeStopButton.Text = "Stop services";
            this._executeStopButton.UseVisualStyleBackColor = true;
            this._executeStopButton.Click += new System.EventHandler(this.ExecuteStopButton_Click);
            // 
            // _executeStartButton
            // 
            this._executeStartButton.Location = new System.Drawing.Point(6, 22);
            this._executeStartButton.Name = "_executeStartButton";
            this._executeStartButton.Size = new System.Drawing.Size(100, 23);
            this._executeStartButton.TabIndex = 2;
            this._executeStartButton.Text = "Start services";
            this._executeStartButton.UseVisualStyleBackColor = true;
            this._executeStartButton.Click += new System.EventHandler(this.ExecuteStartButton_Click);
            // 
            // _configurationGroupBox
            // 
            this._configurationGroupBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this._configurationGroupBox.Controls.Add(this._configureButton);
            this._configurationGroupBox.Controls.Add(this._installStatusLabel);
            this._configurationGroupBox.Controls.Add(this._installButton);
            this._configurationGroupBox.Location = new System.Drawing.Point(12, 12);
            this._configurationGroupBox.Name = "_configurationGroupBox";
            this._configurationGroupBox.Size = new System.Drawing.Size(598, 57);
            this._configurationGroupBox.TabIndex = 2;
            this._configurationGroupBox.TabStop = false;
            this._configurationGroupBox.Text = "Configuration";
            // 
            // _configureButton
            // 
            this._configureButton.Location = new System.Drawing.Point(6, 22);
            this._configureButton.Name = "_configureButton";
            this._configureButton.Size = new System.Drawing.Size(100, 23);
            this._configureButton.TabIndex = 0;
            this._configureButton.Text = "Configure";
            this._configureButton.UseVisualStyleBackColor = true;
            this._configureButton.Click += new System.EventHandler(this.ConfigureButton_Click);
            // 
            // _installStatusLabel
            // 
            this._installStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._installStatusLabel.ImageIndex = 0;
            this._installStatusLabel.ImageList = this._statusImageList;
            this._installStatusLabel.Location = new System.Drawing.Point(218, 22);
            this._installStatusLabel.Name = "_installStatusLabel";
            this._installStatusLabel.Size = new System.Drawing.Size(374, 23);
            this._installStatusLabel.TabIndex = 5;
            this._installStatusLabel.Text = "installStatusLabel";
            this._installStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._installStatusLabel.Visible = false;
            // 
            // _installButton
            // 
            this._installButton.Location = new System.Drawing.Point(112, 22);
            this._installButton.Name = "_installButton";
            this._installButton.Size = new System.Drawing.Size(100, 23);
            this._installButton.TabIndex = 1;
            this._installButton.Text = "Install";
            this._installButton.UseVisualStyleBackColor = true;
            this._installButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 147);
            this.Controls.Add(this._executionGroupBox);
            this.Controls.Add(this._configurationGroupBox);
            this.Name = "ServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Winp Service";
            this.Shown += new System.EventHandler(this.ServiceForm_Shown);
            this._executionGroupBox.ResumeLayout(false);
            this._configurationGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button _installButton;
        private System.Windows.Forms.GroupBox _configurationGroupBox;
        private System.Windows.Forms.GroupBox _executionGroupBox;
        private System.Windows.Forms.ImageList _statusImageList;
        private System.Windows.Forms.Label _installStatusLabel;
        private System.Windows.Forms.Button _configureButton;
        private System.Windows.Forms.Label _executeStatusLabel;
        private System.Windows.Forms.Button _executeStopButton;
        private System.Windows.Forms.Button _executeStartButton;
    }
}