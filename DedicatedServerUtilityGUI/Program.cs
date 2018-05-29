﻿using System;
using System.Windows.Forms;

namespace DedicatedServerUtilityGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMainWindow());
        }
    }
}
