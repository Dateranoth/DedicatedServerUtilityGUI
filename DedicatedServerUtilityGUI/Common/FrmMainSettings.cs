using System;
using System.Windows.Forms;

namespace DedicatedServerUtilityGUI.Common
{
    public partial class FrmMainSettings : UserControl
    {
        //TODO: Determine if more base settings are required.
        //TODO: Add Upgrade Button and Reset to Default Button.
        private GlobalVariables mySettings = new GlobalVariables();
        
        public FrmMainSettings()
        {
            InitializeComponent();
            mySettings.InitializeVariables(ref mySettings);
            settingBox1.Text = mySettings.InstallDirectory;
            settingBox2.Text = mySettings.RelativeExePath;
            settingBox3.Text = mySettings.ServerExe;
            settingBox4.Text = mySettings.ServerArgs;
            settingBox5.Text = mySettings.ServerStopCmd;
            checkBox1.Checked = mySettings.AutoStart;
            checkBox2.Checked = mySettings.KeepAlive;
            
        }

        private void SaveMainSettings()
        {
            mySettings.InstallDirectory = settingBox1.Text;
            mySettings.RelativeExePath = settingBox2.Text;
            mySettings.ServerExe = settingBox3.Text;
            mySettings.ServerArgs = settingBox4.Text;
            mySettings.ServerStopCmd = settingBox5.Text;
            mySettings.AutoStart = checkBox1.Checked;
            mySettings.KeepAlive = checkBox2.Checked;
            mySettings.SaveSettings(ref mySettings);
            mySettings.InitializeVariables(ref mySettings);
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            SaveMainSettings();
        }
    }
}
