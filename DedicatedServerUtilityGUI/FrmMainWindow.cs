using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DedicatedServerUtilityGUI
{
    public partial class FrmMainWindow : Form
    {

        #region Local Variables.
        private static System.Timers.Timer PeriodicEventsTimer;
        private Process ServerProcess = new Process();
        private bool processRunning = false;
        private bool manualStopTriggered = false;
        private Common.Methods CommonMethods = new Common.Methods();
        private Common.Settings GlobalVariables = new Common.Settings();
        #endregion Local Variables.

        public FrmMainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            Startup();
        }

        #region Misc Events: Timers, Processes, Etc.
        private void PeriodicEvents(object source, System.Timers.ElapsedEventArgs e)
        {
            //TODO: Add periodic events. ie. check for updates.
            Console.WriteLine("Firing Periodic Events");
        }

        private void ServerProcess_Exited(object sender, EventArgs e)
        {
            PeriodicEventsTimer.Stop();
            ServerProcess.Exited -= ServerProcess_Exited;
            ServerProcess.Close();
            processRunning = false;
            if (manualStopTriggered)
            {
                manualStopTriggered = false;
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
        # endregion Misc Events: Timers, Processes, Etc.

        #region Form Built In Events.
        private void StopServerWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        { 
            PeriodicEventsTimer.Stop();
            manualStopTriggered = true;
            int closedStatus;
            while (true)
            {
                if (StopServerWorker.CancellationPending)
                {
                    CommonMethods.StopServer(ref ServerProcess, GlobalVariables.ServerID, GlobalVariables.ProcessName, GlobalVariables.ServerStopCmd, 0, false, true);
                    Console.WriteLine("Server Closed Forcefully");
                    break;
                }
                else
                {
                    closedStatus = CommonMethods.StopServer(ref ServerProcess, GlobalVariables.ServerID, GlobalVariables.ProcessName, GlobalVariables.ServerStopCmd);
                    e.Result = closedStatus;
                }
                break;              
                
            }
        }

        private void StopServerWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                switch (e.Result)
                // -1 = Error
                // 0 = Process was not running. 
                // 1 = Process Closed Gracefully.
                // 2 = Process Window Forced Closed.
                // 3 = Process Killed.
                {
                    case -1:
                        Console.WriteLine("Something Went Wrong. Check Logs.");
                        break;
                    case 0:
                        Console.WriteLine("Could not find Process to Close.");
                        break;
                    case 1:
                        Console.WriteLine("Process Closed Gracefully.");
                        statusLabel1.Text = "Process Closed Gracefully.";
                        break;
                    case 2:
                        Console.WriteLine("Process Window was Forced Closed.");
                        break;
                    case 3:
                        Console.WriteLine("Process was Killed.");
                        break;
                    default:
                        Console.WriteLine("No Idea. This shouldn't happen.");
                        break;

                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServerWorker.CancelAsync();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIconGC.Visible = true;
            }
        }
        #endregion Form Built In Events.

        #region Form Button Events.


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

        private void HomeButton_Click(object sender, EventArgs e)
        {

            ChangeScreen("DedicatedServerUtilityGUI.Common.FrmHome");
            HomeButton.Hide();
            MainSettings.Show();
            if ((CommonMethods.CheckProcessRunning(GlobalVariables.ServerID, GlobalVariables.ProcessName).Equals(false)))
            {
                GlobalVariables.InitializeVariables(ref GlobalVariables);
            }

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

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Exit Now?", "Exit", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                Application.Exit();
            }           
        }
        #endregion Form Button Events.

        #region Local Methods.

        private void Startup()
        {
            // If Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & Application.ProductName) Then
            // System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & Application.ProductName)
            // End If
            statusLabel1.Visible = false;
            GlobalVariables.InitializeVariables(ref GlobalVariables);
            MainSettings.Show();
            HomeButton.Hide();
            if (GlobalVariables.AutoStart)
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
            else if ((CommonMethods.CheckProcessRunning(GlobalVariables.ServerID, GlobalVariables.ProcessName)))
            {
                ServerProcess = Process.GetProcessById(GlobalVariables.ServerID);
                ServerProcess.EnableRaisingEvents = true;
                ServerProcess.Exited += ServerProcess_Exited;
                PeriodicEventsTimer.Start();
                processRunning = true;
                Console.WriteLine("Attached to Running Server Process");
            }
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
            if (processRunning)
            {
                return true;
            }
            else if (CommonMethods.StartServer(ref ServerProcess, GlobalVariables.ServerID, GlobalVariables.ProcessName, GlobalVariables.ServerPath, GlobalVariables.ServerExe, GlobalVariables.ServerArgs))
            {
                ServerProcess.EnableRaisingEvents = true;
                ServerProcess.Exited += ServerProcess_Exited;
                PeriodicEventsTimer.Start();
                GlobalVariables.ServerID = ServerProcess.Id;
                GlobalVariables.SavePID(GlobalVariables.ServerID);
                processRunning = true;
                statusLabel1.Visible = true;
                statusLabel1.Text = "Server Running.";
                return true;
            }
            else
            {
                GlobalVariables.ServerID = 0;
                GlobalVariables.SavePID(GlobalVariables.ServerID);
                processRunning = false;
                return false;
            }

        }

        #endregion Local Methods.

    }

}

