using System;
using System.Runtime.InteropServices;

namespace DedicatedServerUtilityGUI.Common
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.DLL", CharSet = CharSet.Unicode)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}