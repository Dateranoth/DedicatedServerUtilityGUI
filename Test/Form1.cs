using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
//using System.Timers

namespace Test
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer Timer1;
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindow(null, "Conan Server Utility Server1 - Conan Exiles - press Ctrl + C to shutdown");
            MessageBox.Show(hWnd.ToString());
           
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at" + e.SignalTime);
        }

        private void InitializeTimer()
        {
            Timer1 = new System.Timers.Timer
            {
                Interval = 100000,
                Enabled = true
            };
            Timer1.Elapsed += OnTimedEvent;
            Timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            TimeTest();
        }

        private void TimeTest()
        {
            DateTime today = System.DateTime.Now;
            TimeSpan duration = new System.TimeSpan(0, 0, 10);
            DateTime answer = today.Add(duration);
            DateTime next = today.Add(new TimeSpan(0, 0, 1));
            DateTime remaining = answer;
            while ((DateTime.Compare(System.DateTime.Now, answer) < 0))
            {
                if (backgroundWorker1.CancellationPending)
                {
                    System.Console.WriteLine("Operation Aborted");
                    break;
                }
                else
                {
                    //System.Windows.Forms.Application.DoEvents();
                    if ((DateTime.Compare(System.DateTime.Now, next) > 0))
                    {
                        remaining = remaining.Subtract(new TimeSpan(0, 0, 1));
                        Console.WriteLine("This is how long remains: {0:ss}", remaining);
                        next = DateTime.Now.Add(new TimeSpan(0, 0, 1));
                    }
                }
            }


            System.Console.WriteLine("The Time is Up");
        }
    }
}
