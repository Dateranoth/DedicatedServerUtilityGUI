using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerUtilityGUI
{
    
    public class GlobalVariables
    {
        //private Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
        private int serverID;
        private string serverPath;
        private string serverEXE;
        private string processName;
        private string serverArgs;
        private string serverStopCmd;
        private bool autoStart;
        public int ServerID
        {
            get
            {
                return serverID;
            }
            set
            {
                serverID = value;
            }
        }

        public string ServerPath
        {
            get
            {
                return serverPath;
            }
            set
            {
                serverPath = value;
            }
        }

        public string ServerEXE
        {
            get
            {
                return serverEXE;
            }
            set
            {
                serverEXE = value;
            }
        }

        public string ProcessName
        {
            get
            {
                return processName;
            }
            set
            {
                processName = value;
            }
        }

        public string ServerArgs
        {
            get
            {
                return serverArgs;
            }
            set
            {
                serverArgs = value;
            }
        }

        public string ServerStopCmd
        {
            get
            {
                return serverStopCmd;
            }
            set
            {
                serverStopCmd = value;
            }
        }

        public bool AutoStart
        {
            get
            {
                return autoStart;
            }
            set
            {
                autoStart = value;
            }
        }
        public bool InitializeVariables(ref GlobalVariables myVars)
        {
            Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
            myVars.ServerID = Properties.Settings.Default.LastProcessID;
            myVars.ServerPath = System.Text.RegularExpressions.Regex.Match(Properties.Settings.Default.ServerPath.Trim(), @"^(.*[^\\])").Value;
            myVars.ServerEXE = Properties.Settings.Default.ServerEXE.Trim();
            myVars.ProcessName = CommonFunctions.GetProcessName(myVars.ServerEXE);
            myVars.ServerArgs = Properties.Settings.Default.ServerStartArguments.Trim();
            myVars.ServerStopCmd = Properties.Settings.Default.ServerStopString.Trim();
            myVars.AutoStart = Properties.Settings.Default.AutoStart;
            return true;
        }

        public bool SaveSettings(ref GlobalVariables myVars)
        {
            Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
            Properties.Settings.Default.LastProcessID = myVars.ServerID;
            Properties.Settings.Default.ServerPath = System.Text.RegularExpressions.Regex.Match(myVars.ServerPath.Trim(), @"^(.*[^\\])").Value;
            Properties.Settings.Default.ServerEXE = myVars.ServerEXE.Trim();
            Properties.Settings.Default.ServerStartArguments = myVars.ServerArgs.Trim();
            Properties.Settings.Default.ServerStopString = myVars.ServerStopCmd.Trim();
            Properties.Settings.Default.AutoStart = myVars.AutoStart;
            Properties.Settings.Default.Save();
            return true;
        }

        public bool SavePID(ref GlobalVariables myVars)
        {
            Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
            Properties.Settings.Default.LastProcessID = myVars.ServerID;
            Properties.Settings.Default.Save();
            return true;
        }
    }


    

}
