using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DedicatedServerUtilityGUI
{
    public partial class FrmMainWindow : Form
    {
        private static System.Timers.Timer PeriodicEventsTimer;
        private Process ServerProcess = new Process();
        private bool processRunning = false;
        private bool ManualStopTriggered = false;
        private Common.CommonFunctions CommonFunctions = new Common.CommonFunctions();
        private GlobalVariables GlobalVariables = new GlobalVariables();
        public FrmMainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            Startup();
        }

        private void Startup()
        {
            // If Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & Application.ProductName) Then
            // System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & Application.ProductName)
            // End If
            GlobalVariables.InitializeVariables(ref GlobalVariables);
            MainSettings.Show();
            HomeButton.Hide();
            if ((CommonFunctions.CheckProcessRunning(GlobalVariables.ServerID, GlobalVariables.ProcessName)))
            {
                ServerProcess = Process.GetProcessById(GlobalVariables.ServerID);
                ServerProcess.EnableRaisingEvents = true;
                ServerProcess.Exited += ServerProcess_Exited;
                PeriodicEventsTimer.Start();
                processRunning = true;
            }
            else if (GlobalVariables.AutoStart)
            {
                if (StartServerCommand())
                {
                    Console.WriteLine("Server Started");
                }
                else
                {
                    Console.WriteLine("Server Failed to Start");
                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServerWorker.CancelAsync();
        }


        private void PeriodicEvents(object source, System.Timers.ElapsedEventArgs e)
        {
            //TODO: Add periodic events. ie. check for updates.
            Console.WriteLine("Firing Periodic Events");
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (StartServerCommand())
            {
                Console.WriteLine("Server Started");
            }
            else
            {
                MessageBox.Show("Server Failed to Start. Check Logs");
            }         
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (StopServerWorker.IsBusy.Equals(false))
            {
                StopServerWorker.RunWorkerAsync();
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GlobalVariables.ServerPath + @"\" + GlobalVariables.ServerExe);
            MessageBox.Show(GlobalVariables.ProcessName);
            MessageBox.Show(GlobalVariables.ServerPath);
        }

        private void ServerProcess_Exited(object sender, EventArgs e)
        {
            PeriodicEventsTimer.Stop();
            ServerProcess.Exited -= ServerProcess_Exited;
            ServerProcess.Close();
            processRunning = false;
            if (ManualStopTriggered)
            {
                ManualStopTriggered = false;
                Console.WriteLine("EVENT: Server Stopped Manually");
            }
            else if (GlobalVariables.KeepAlive)
            {
                Console.WriteLine("EVENT: Server Failed! Attempting Restart!");
                if (StartServerCommand())
                {
                    Console.WriteLine("Server Started");
                }
                else
                {
                    Console.WriteLine("Server Failed to Start");
                }
            }
            else
            {
                Console.WriteLine("EVENT: Server Failed! Auto Restart Disabled!");
            }
        }

        private void InitializeTimer()
        {
            PeriodicEventsTimer = new System.Timers.Timer
            {
                Interval = 6000,
                Enabled = false
            };
            PeriodicEventsTimer.Elapsed += PeriodicEvents;
        }

        private bool StartServerCommand()
        {
            int refID = GlobalVariables.ServerID;
            if (processRunning)
            {
                return true;
            }
            else if (CommonFunctions.StartServer(ref ServerProcess, ref refID, GlobalVariables.ProcessName, GlobalVariables.ServerPath, GlobalVariables.ServerExe, GlobalVariables.ServerArgs))
            {
                ServerProcess.EnableRaisingEvents = true;
                ServerProcess.Exited += ServerProcess_Exited;
                PeriodicEventsTimer.Start();
                GlobalVariables.ServerID = refID;
                GlobalVariables.SavePID(GlobalVariables.ServerID);
                processRunning = true;
                return true;
            }
            else
            {
                GlobalVariables.ServerID = refID;
                GlobalVariables.SavePID(GlobalVariables.ServerID);
                return false;
            }
            
        }

        private void StopServerWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        { 
            PeriodicEventsTimer.Stop();
            ManualStopTriggered = true;
            DateTime now = DateTime.Now;
            DateTime failTime = now.Add(new TimeSpan(0, 0, 120));
            DateTime killTime = now.Add(new TimeSpan(0, 0, 60));
            DateTime nextTime = now.Add(new TimeSpan(0, 0, 10));
            DateTime next = now.Add(new TimeSpan(0, 0, 2));
            int refID = GlobalVariables.ServerID;
            var runLoop = false;
            if (CommonFunctions.CheckProcessRunning(refID, GlobalVariables.ProcessName).Equals(false))
            {
                Console.WriteLine("Server Not Running");
            }
            else if (CommonFunctions.StopServer(ref ServerProcess, ref refID, GlobalVariables.ProcessName, GlobalVariables.ServerStopCmd, false).Equals(true))
            {
                Console.WriteLine("Server Gracefully Stopped");
            }
            else
            {
                runLoop = true;
            }
            while (runLoop)
            {
                if (StopServerWorker.CancellationPending)
                {
                    CommonFunctions.StopServer(ref ServerProcess, ref refID, GlobalVariables.ProcessName, GlobalVariables.ServerStopCmd, true);
                    Console.WriteLine("Server Closed Forcefully");
                    break;
                }
                else if ((DateTime.Compare(DateTime.Now, next) > 0))
                {
                    Console.WriteLine("Waiting to Try Closing Again...");
                    if (DateTime.Compare(DateTime.Now, nextTime) > 0)
                    {
                        if (CommonFunctions.StopServer(ref ServerProcess, ref refID, GlobalVariables.ProcessName, GlobalVariables.ServerStopCmd, false))
                        {
                            Console.WriteLine("Server Closed Gracefully");
                            break;
                        }
                        else if (CommonFunctions.CheckProcessRunning(refID, GlobalVariables.ProcessName).Equals(false))
                        {
                            Console.WriteLine("Server Closed Gracefully");
                            break;
                        }
                        nextTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));
                    }
                    else if (DateTime.Compare(DateTime.Now, failTime) > 0)
                    {
                        Console.WriteLine("Server Failed to Close");
                        break;
                    }
                    else if (DateTime.Compare(DateTime.Now, killTime) > 0)
                    {
                        if (CommonFunctions.StopServer(ref ServerProcess, ref refID, GlobalVariables.ProcessName, GlobalVariables.ServerStopCmd, true))
                        {
                            Console.WriteLine("Server Closed Forcefully");
                            break;
                        }
                    }
                    GlobalVariables.ServerID = refID;
                    next = DateTime.Now.Add(new TimeSpan(0, 0, 2));
                }
                
            }
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIconGC.Visible = true;
            }
        }

        private void NotifyIconGC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIconGC.Visible = false;
        }

        private void MainSettings_Click(object sender, EventArgs e)
        {
            ChangeScreen("DedicatedServerUtilityGUI.Common.FrmMainSettings");
            MainSettings.Hide();
            HomeButton.Show();
        }

        private void ChangeScreen(string NewScreen)
        {
            Control ctl = null;
            Type ctlType;
            try
            {
                if (NewScreen.Contains("FrmMainSettings"))
                {
                    Text = "Main Settings";
                }
                if (NewScreen.Contains("FrmHome"))
                {
                    Text = "Home";
                }
                pnlCenter.Controls.Clear();
                ctlType = Type.GetType(NewScreen);
                ctl = (Control)Activator.CreateInstance(ctlType);

                pnlCenter.Controls.Add(ctl);
                ctl.Dock = DockStyle.Fill;
                ctl.Show();
                ctl.BringToFront();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChangeScreen: " + ex.Message);
            }


        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            
            ChangeScreen("DedicatedServerUtilityGUI.Common.FrmHome");
            HomeButton.Hide();
            MainSettings.Show();
            if ((CommonFunctions.CheckProcessRunning(GlobalVariables.ServerID, GlobalVariables.ProcessName).Equals(false)))
            {
                GlobalVariables.InitializeVariables(ref GlobalVariables);
            }

        }
    }

}

