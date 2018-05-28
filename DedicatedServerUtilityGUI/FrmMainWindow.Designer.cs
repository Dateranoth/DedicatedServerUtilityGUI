namespace DedicatedServerUtilityGUI
{
    partial class FrmMainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainWindow));
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.StopServerWorker = new System.ComponentModel.BackgroundWorker();
            this.notifyIconGC = new System.Windows.Forms.NotifyIcon(this.components);
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.MainSettings = new System.Windows.Forms.Button();
            this.HomeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(717, 668);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(124, 49);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start Server";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(860, 668);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(124, 49);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop Server";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(151, 694);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 2;
            this.TestButton.Text = "Test Stuff";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // StopServerWorker
            // 
            this.StopServerWorker.WorkerSupportsCancellation = true;
            this.StopServerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StopServerWorker_DoWork);
            // 
            // notifyIconGC
            // 
            this.notifyIconGC.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconGC.Icon")));
            this.notifyIconGC.Text = "DedicatedServerUtilityGUI";
            this.notifyIconGC.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconGC_MouseDoubleClick);
            // 
            // pnlCenter
            // 
            this.pnlCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCenter.BackColor = System.Drawing.Color.Transparent;
            this.pnlCenter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCenter.Location = new System.Drawing.Point(15, 31);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(968, 622);
            this.pnlCenter.TabIndex = 3;
            // 
            // MainSettings
            // 
            this.MainSettings.Location = new System.Drawing.Point(15, 668);
            this.MainSettings.Name = "MainSettings";
            this.MainSettings.Size = new System.Drawing.Size(130, 48);
            this.MainSettings.TabIndex = 4;
            this.MainSettings.Text = "Open Main Settings";
            this.MainSettings.UseVisualStyleBackColor = true;
            this.MainSettings.Click += new System.EventHandler(this.MainSettings_Click);
            // 
            // HomeButton
            // 
            this.HomeButton.Location = new System.Drawing.Point(15, 669);
            this.HomeButton.Name = "HomeButton";
            this.HomeButton.Size = new System.Drawing.Size(130, 48);
            this.HomeButton.TabIndex = 5;
            this.HomeButton.Text = "Home";
            this.HomeButton.UseVisualStyleBackColor = true;
            this.HomeButton.Click += new System.EventHandler(this.HomeButton_Click);
            // 
            // FrmMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.HomeButton);
            this.Controls.Add(this.MainSettings);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMainWindow";
            this.Text = "DedicatedServerUtilityGUI";
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button TestButton;
        private System.ComponentModel.BackgroundWorker StopServerWorker;
        private System.Windows.Forms.NotifyIcon notifyIconGC;
        public System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Button MainSettings;
        private System.Windows.Forms.Button HomeButton;
    }
}