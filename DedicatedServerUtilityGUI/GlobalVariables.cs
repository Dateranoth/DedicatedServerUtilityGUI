using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerUtilityGUI
{
    
    public class GlobalVariables
    {
        // For Program Usage - Saves last Process ID.
        public int ServerID { get; set; }
        // User Options

        public string InstallDirectory { get; set; }
        public string RelativeExePath { get; set; }
        public string ServerExe { get; set; }
        public string ServerArgs { get; set; }
        public string ServerStopCmd { get; set; }
        public bool AutoStart { get; set; }
        public bool KeepAlive { get; set; }

        //Generated from User Options.
        public string ServerPath { get; set; }
        public string ProcessName { get; set; }

        public bool InitializeVariables(ref GlobalVariables myVars)
        {
            Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
            // For Program Usage - Retrieves Last Process ID.
            myVars.ServerID = Properties.Settings.Default.LastProcessID;

            // User Options.
            myVars.InstallDirectory = System.Text.RegularExpressions.Regex.Match(Properties.Settings.Default.InstallDirectory.Trim(), @"([^\\].*[^\\])").Value;
            myVars.RelativeExePath = System.Text.RegularExpressions.Regex.Match(Properties.Settings.Default.RelativeExePath.Trim(), @"([^\\].*[^\\])").Value;
            myVars.ServerExe = Properties.Settings.Default.ServerExe.Trim();
            myVars.ServerArgs = Properties.Settings.Default.ServerStartArguments.Trim();
            myVars.ServerStopCmd = Properties.Settings.Default.ServerStopString.Trim();
            myVars.AutoStart = Properties.Settings.Default.AutoStart;
            myVars.KeepAlive = Properties.Settings.Default.KeepAlive;

            // Generated from User Options.
            myVars.ServerPath = myVars.InstallDirectory + @"\" + myVars.RelativeExePath;
            myVars.ProcessName = CommonFunctions.GetProcessName(myVars.ServerExe);
            return true;
        }

        public bool SaveSettings(ref GlobalVariables myVars)
        {
            Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
            // For Program Usage - Saves Last Process ID.
            Properties.Settings.Default.LastProcessID = myVars.ServerID;

            // Save User Options
            Properties.Settings.Default.InstallDirectory = System.Text.RegularExpressions.Regex.Match(myVars.InstallDirectory.Trim(), @"([^\\].*[^\\])").Value;
            Properties.Settings.Default.RelativeExePath = System.Text.RegularExpressions.Regex.Match(myVars.RelativeExePath.Trim(), @"([^\\].*[^\\])").Value;
            Properties.Settings.Default.ServerExe = myVars.ServerExe.Trim();
            Properties.Settings.Default.ServerStartArguments = myVars.ServerArgs.Trim();
            Properties.Settings.Default.ServerStopString = myVars.ServerStopCmd.Trim();
            Properties.Settings.Default.AutoStart = myVars.AutoStart;
            Properties.Settings.Default.KeepAlive = myVars.KeepAlive;
            Properties.Settings.Default.Save();
            return true;
        }

        public bool SavePID(int myID)
        {
            Properties.Settings.Default.LastProcessID = myID;
            Properties.Settings.Default.Save();
            return true;
        }
    }


    

}
