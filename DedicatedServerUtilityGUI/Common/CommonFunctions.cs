using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DedicatedServerUtilityGUI.Common
{
    public class CommonFunctions
        
    {
        public bool StartServer(ref Process ServerProcess, ref int ID, string Name, string Path, string EXE, string Args)
        {
            
            try
            {
                
                if (!System.IO.File.Exists(Path + @"\" + EXE))
                {
                    throw new Exception("File Does Not Exist at " + Path + @"\" + EXE);
                }

                if (CheckProcessRunning(ID, Name))
                {
                    // Process Exists
                    ServerProcess = Process.GetProcessById(ID);
                    Console.WriteLine("Process Already Running");
                    return true;
                }
                else
                {
                    ServerProcess.StartInfo.UseShellExecute = true;
                    ServerProcess.StartInfo.CreateNoWindow = false;
                    ServerProcess.StartInfo.RedirectStandardInput = false;
                    ServerProcess.StartInfo.FileName = Path + @"\" + EXE;
                    ServerProcess.StartInfo.Arguments = Args;
                    ServerProcess.StartInfo.WorkingDirectory = Path;
                    ServerProcess.Start();
                    ServerProcess.WaitForInputIdle();
                    ID = ServerProcess.Id;
                    if (ServerProcess.WaitForInputIdle(10000))
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Process Failed to Start after 10 Seconds");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("StartServer Error: " + ex.Message);
                return false;
            }
        }

        public bool StopServer(ref Process ServerProcess, ref int ID, string Name, string Command, bool kill = false)
        {
            try
            {
                if (CheckProcessRunning(ID, Name))
                {
                    // Process Exists
                    ServerProcess = Process.GetProcessById(ID);
                    IntPtr hWnd = ServerProcess.MainWindowHandle;
                    if (kill)
                    {
                        ServerProcess.Kill();    
                    }
                    else
                    {
                        NativeMethods.SetForegroundWindow(hWnd);
                        SendKeys.SendWait(Command);
                    }
                    ServerProcess.WaitForExit(1000);
                    if (CheckProcessRunning(ID, Name))
                    {
                        return false;              
                    }
                    else
                    {
                        ID = 0;
                        return true;
                    }
                }
                else
                {
                    ID = 0;
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("StopServer Error: " + ex.Message);
                return false;
            }
        }


        public bool CheckProcessRunning(int Id, string Name)
        {
            try
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if (proc.Id == Id & proc.ProcessName == Name)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CheckProcessRunning Error: " + ex.Message);
                return false;
            }
        }

        public string GetProcessName(string EXE)
        {
            try
            {
                string TempString = EXE;
                int TempLength;
                TempString = TempString.Trim();
                TempLength = TempString.Length;
                TempString = TempString.Substring(0, (TempLength - 4));
                return TempString;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetProcessName Error: " + ex.Message);
                return "";
            }
        }

    }
}