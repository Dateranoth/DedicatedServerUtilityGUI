using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp.Common
{
    public partial class FrmMainSettings : UserControl
    {
        public FrmMainSettings()
        {
            InitializeComponent();
            this.settingBox1.Text = GlobalVariables.ServerPath;
        }

        public bool SaveMainSettings()
        {
            Properties.Settings.Default.ServerPath = settingBox1.Text.Trim();
            Properties.Settings.Default.Save();
            GlobalVariables.UpdateVariables();
            return true;
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            SaveMainSettings();
        }
    }
}
