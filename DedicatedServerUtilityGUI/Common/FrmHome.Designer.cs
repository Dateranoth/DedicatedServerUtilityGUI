namespace DedicatedServerUtilityGUI.Common
{
    partial class FrmHome
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
            this.HomeHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HomeHeader
            // 
            this.HomeHeader.AutoSize = true;
            this.HomeHeader.Location = new System.Drawing.Point(32, 28);
            this.HomeHeader.Name = "HomeHeader";
            this.HomeHeader.Size = new System.Drawing.Size(72, 13);
            this.HomeHeader.TabIndex = 1;
            this.HomeHeader.Text = "Home Screen";
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HomeHeader);
            this.Name = "FrmHome";
            this.Size = new System.Drawing.Size(968, 622);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HomeHeader;
    }
}
