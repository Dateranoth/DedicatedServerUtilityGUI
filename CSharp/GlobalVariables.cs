using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    public class GlobalVariables
    {
        public static int ServerID;
        public static string ServerPath;
        public static string ServerEXE;
        public static string ProcessName;
        public static string ServerArgs;
        public static string ServerStopCmd;
        public static bool AutoStart;

        public static bool UpdateVariables()
            {
            ServerID = Properties.Settings.Default.LastProcessID;
            ServerPath = System.Text.RegularExpressions.Regex.Match(Properties.Settings.Default.ServerPath.Trim(), @"^(.*[^\\])").Value;
            ServerEXE = Properties.Settings.Default.ServerEXE.Trim();
            ProcessName = GlobalFunctions.GetProcessName(ServerEXE);
            ServerArgs = Properties.Settings.Default.ServerStartArguments.Trim();
            ServerStopCmd = Properties.Settings.Default.ServerStopString.Trim();
            AutoStart = Properties.Settings.Default.AutoStart;
            return true;
            }
    }


}
