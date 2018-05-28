namespace DedicatedServerUtilityGUI.Common
{
    partial class FrmMainSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainSettingsHeader = new System.Windows.Forms.Label();
            this.AutoStart = new System.Windows.Forms.CheckBox();
            this.settingBox1 = new System.Windows.Forms.TextBox();
            this.settingLabel1 = new System.Windows.Forms.Label();
            this.SaveSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainSettingsHeader
            // 
            this.MainSettingsHeader.AutoSize = true;
            this.MainSettingsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainSettingsHeader.Location = new System.Drawing.Point(29, 20);
            this.MainSettingsHeader.Name = "MainSettingsHeader";
            this.MainSettingsHeader.Size = new System.Drawing.Size(143, 25);
            this.MainSettingsHeader.TabIndex = 0;
            this.MainSettingsHeader.Text = "Main Settings";
            // 
            // AutoStart
            // 
            this.AutoStart.AutoSize = true;
            this.AutoStart.Location = new System.Drawing.Point(765, 567);
            this.AutoStart.Name = "AutoStart";
            this.AutoStart.Size = new System.Drawing.Size(177, 17);
            this.AutoStart.TabIndex = 1;
            this.AutoStart.Text = "Start Server on Application Start";
            this.AutoStart.UseVisualStyleBackColor = true;
            // 
            // settingBox1
            // 
            this.settingBox1.Location = new System.Drawing.Point(217, 85);
            this.settingBox1.Name = "settingBox1";
            this.settingBox1.Size = new System.Drawing.Size(725, 20);
            this.settingBox1.TabIndex = 2;
            // 
            // settingLabel1
            // 
            this.settingLabel1.AutoSize = true;
            this.settingLabel1.Location = new System.Drawing.Point(214, 69);
            this.settingLabel1.Name = "settingLabel1";
            this.settingLabel1.Size = new System.Drawing.Size(75, 13);
            this.settingLabel1.TabIndex = 3;
            this.settingLabel1.Text = "Path to Server";
            // 
            // SaveSettings
            // 
            this.SaveSettings.Location = new System.Drawing.Point(3, 567);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new System.Drawing.Size(124, 49);
            this.SaveSettings.TabIndex = 7;
            this.SaveSettings.Text = "Save Settings";
            this.SaveSettings.UseVisualStyleBackColor = true;
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // FrmMainSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.settingLabel1);
            this.Controls.Add(this.settingBox1);
            this.Controls.Add(this.AutoStart);
            this.Controls.Add(this.MainSettingsHeader);
            this.Name = "FrmMainSettings";
            this.Size = new System.Drawing.Size(968, 622);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MainSettingsHeader;
        private System.Windows.Forms.CheckBox AutoStart;
        private System.Windows.Forms.TextBox settingBox1;
        private System.Windows.Forms.Label settingLabel1;
        private System.Windows.Forms.Button SaveSettings;
    }
}
