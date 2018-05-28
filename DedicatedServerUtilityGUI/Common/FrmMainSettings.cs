using System;
using System.Windows.Forms;

namespace DedicatedServerUtilityGUI.Common
{
    public partial class FrmMainSettings : UserControl
    {
        private GlobalVariables GlobalVariables = new GlobalVariables();
        
        public FrmMainSettings()
        {
            InitializeComponent();
            GlobalVariables.InitializeVariables(ref GlobalVariables);
            settingBox1.Text = GlobalVariables.ServerPath;
            
        }

        public bool SaveMainSettings()
        {
            GlobalVariables.ServerPath = settingBox1.Text.Trim();
            GlobalVariables.SaveSettings(ref GlobalVariables);
            GlobalVariables.InitializeVariables(ref GlobalVariables);
            return true;
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            SaveMainSettings();
        }
    }
}
