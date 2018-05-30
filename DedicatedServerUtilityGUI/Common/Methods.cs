using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DedicatedServerUtilityGUI.Common
{
    public class Methods
        
    {
        public bool StartServer(ref Process ServerProcess, int ID, string Name, string Path, string EXE, string Args)
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
                    ServerProcess.WaitForInputIdle(1000);
                    if (ServerProcess.Responding)
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

        public int StopServer(ref Process ServerProcess, int ID, string Name, string Command, int killTime = 120000, bool force = false, bool kill = false)
        {
            // -1 = Error
            // 0 = Process was not running. 
            // 1 = Process Closed Gracefully.
            // 2 = Process Window Forced Closed.
            // 3 = Process Killed.
            try
            {
                if (CheckProcessRunning(ID, Name))
                {
                    int nextTime = (killTime / 4);
                    int forceTime = (killTime / 2);
                    int totalTime = 0;
                    ServerProcess = Process.GetProcessById(ID);
                    
                    TryAgain:
                    if (kill)
                    {
                        ServerProcess.Kill();    
                    }
                    else if (force)
                    {
                        ServerProcess.CloseMainWindow();
                    }
                    else
                    {
                        IntPtr hWnd = ServerProcess.MainWindowHandle;
                        NativeMethods.SetForegroundWindow(hWnd);
                        SendKeys.SendWait(Command);
                    }

                    ServerProcess.WaitForExit(nextTime);
                    totalTime += nextTime;
                    if (!(CheckProcessRunning(ID, Name)) || (kill))
                    {
                        if (kill)
                        {
                            return 3;
                        }
                        else if (force)
                        {
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else if (totalTime >= killTime)
                    {
                        kill = true;
                        goto TryAgain;
                    }
                    else if (totalTime >= forceTime)
                    {
                        force = true;
                        goto TryAgain;
                    }
                    else
                    {
                        goto TryAgain;
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("StopServer Error: " + ex.Message);
                return -1;
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