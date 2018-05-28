using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    public class GlobalVariables
    {
        public static int ServerID = Properties.Settings.Default.LastProcessID;
        public static string ServerPath = System.Text.RegularExpressions.Regex.Match(Properties.Settings.Default.ServerPath.Trim(), @"^(.*[^\\])").Value;
        public static string ServerEXE = Properties.Settings.Default.ServerEXE.Trim();
        public static string ProcessName = GlobalFunctions.GetProcessName(ServerEXE);
        public static string ServerArgs = Properties.Settings.Default.ServerStartArguments.Trim();
        public static string ServerStopCmd = Properties.Settings.Default.ServerStopString.Trim();
        public static bool AutoStart = Properties.Settings.Default.AutoStart;
    }

}
