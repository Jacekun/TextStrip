using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextStrip
{
    public partial class frmMain : Form
    {
        public static string PATH_ROOT = Application.StartupPath + "\\";
        public static string PATH_LOG = PATH_ROOT + "AppLog.log";
        public static string PATH_LOG_FULL = PATH_ROOT + "AppLogFull.log";

        public static BackgroundWorker bgw = new BackgroundWorker();

        public int COUNT_MAX = 0;
        public int COUNT_CURRENT = 0;

        public frmMain()
        {
            InitializeComponent();

            Text = Application.ProductName;

            txtLog.Text = "";
            txtSrc.Text = "";

            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            bgw.WorkerReportsProgress = true;
        }
        //################################################################################## Functions
        private void LogToFile(string filepath, string text)
        {
            try
            {
                using (StreamWriter r = File.AppendText(filepath))
                {
                    r.WriteLine(text);
                    r.Close();
                }
            }
            catch { }
        }
        private void Log(string filepath, string text)
        {
            try
            {
                string toWrite = $"[{DateTime.Now.ToString("T")}]: {text}";
                using (StreamWriter r = File.AppendText(filepath))
                {
                    r.WriteLine(toWrite);
                    r.Close();
                }
                txtLog.AppendText(toWrite);
                txtLog.AppendText(Environment.NewLine);
            }
            catch { }
        }
        private void Log(string text)
        {
            Log(PATH_LOG, text);
        }
        private void LogInvoke(string filepath, string text)
        {
            // Don't log anything related to logFile
            if (text.Contains(PATH_LOG))
            {
                return;
            }
            try
            {
                string toWrite = $"[{DateTime.Now.ToString("T")}]: {text}";
                using (StreamWriter r = File.AppendText(filepath))
                {
                    r.WriteLine(toWrite);
                    r.Close();
                }
                this.Invoke(new MethodInvoker(delegate
                {
                    txtLog.AppendText(toWrite);
                    txtLog.AppendText(Environment.NewLine);
                }));
            }
            catch { }
        }
        private void LogInvoke(string text)
        {
            LogInvoke(PATH_LOG, text);
        }
        private bool OutputLog(string filepath, string text)
        {
            try
            {
                using (StreamWriter r = File.AppendText(filepath))
                {
                    r.WriteLine(text);
                    r.Close();
                }
                LogInvoke("Succesfully written to file.");
                return true;
            }
            catch (Exception ex)
            {
                // Error
                txtLog.AppendText(ex.ToString());
                txtLog.AppendText(Environment.NewLine);
                return false;
            }
        }
        // Show messages
        private DialogResult ShowMsg(string msg, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //
            return MessageBox.Show(this, msg, caption, buttons, icon);
        }
        private void ShowWarning(string msg, string caption = "")
        {
            string cap = String.IsNullOrWhiteSpace(caption) ? Application.ProductName : caption;
            ShowMsg(msg, cap, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        // Delete previous logs at startup
        private void DeleteLogs()
        {
            try
            {
                File.Delete(PATH_LOG);
                File.Delete(PATH_LOG_FULL);
            }
            catch (Exception ex)
            {
                //
                ShowWarning("Cannot delete prev Logs!");
            }
        }
        //################################################################################## CUSTOM Events
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            try
            {
                // Reset and Create vars
                COUNT_CURRENT = 0;
                string output = "";
                string fileOutput = txtSrc.Text + "_output.txt";
                bool writeIt = false; // write the output to file?

                LogInvoke("Creating list...");
                List<string> content = new List<string>();

                LogInvoke("Populating list from source file...");
                // Read from File
                using (StreamReader r = new StreamReader(txtSrc.Text))
                {
                    content = r.ReadToEnd().Split('\n').ToList<string>();
                    r.Close();
                }

                COUNT_MAX = content.Count;
                LogInvoke($"List saved! Length: {COUNT_MAX.ToString()}");

                LogInvoke("Starting process...");
                TimeSpan LoadingStart = DateTime.Now.TimeOfDay;

                foreach (string s in content)
                {
                    // Iterate through the list
                    output = "";
                    COUNT_CURRENT += 1;
                    int percents = (COUNT_CURRENT * 100) / COUNT_MAX;

                    LogInvoke($"Processing item {COUNT_CURRENT} of {COUNT_MAX}...");
                    LogInvoke($"Original: {s}");

                    // Check if string is whitespace or not
                    if (String.IsNullOrWhiteSpace(s))
                    {
                        // If yes, assign output as it is
                        output = s;
                        writeIt = true;
                    }
                    else
                    {
                        // Remove everything before ':', including that character
                        string temp = Regex.Replace(s, "^[^:]*:", string.Empty, RegexOptions.IgnorePatternWhitespace);
                        temp = temp.TrimStart(':', ' ');
                        temp = temp.TrimStart();

                        // Strip texts of special characters inside the bracket
                        Regex pattern = new Regex("[;,!?.~*\"]");
                        temp = pattern.Replace(temp, string.Empty);

                        if (string.IsNullOrWhiteSpace(temp) == false)
                        {
                            // Strip texts of: Double whitespace
                            pattern = new Regex("[ ]{2}");
                            while (temp.Contains("  "))
                            {
                                temp = pattern.Replace(temp, " ");
                            }

                            // Replace additional characters
                            temp = temp.Replace("[", string.Empty);
                            temp = temp.Replace("]", string.Empty);

                            // Lowercase
                            temp = temp.ToLower();

                            // Trim whitespace
                            output = temp.Trim();

                            writeIt = (!String.IsNullOrWhiteSpace(output));
                        }
                        else
                        {
                            output = String.Empty;
                            writeIt = false;
                        }
                    }
                    LogInvoke($"Result: {output}");

                    if (writeIt)
                    {
                        LogInvoke($"Writing to file.....");
                        OutputLog(fileOutput, output);
                    }

                    bgw.ReportProgress(percents, COUNT_CURRENT.ToString());
                }
                TimeSpan LoadingEnd = DateTime.Now.TimeOfDay;
                TimeSpan duration = LoadingEnd.Subtract(LoadingStart);
                double TimeMS = Convert.ToDouble(duration.TotalMilliseconds);
                double TimeSec = TimeMS / 1000;
                string n = Environment.NewLine;
                string TimeitTook = $"Took {TimeMS} milliseconds to finish processing!{n}In seconds : {TimeSec}{n}In minutes : {duration.ToString("g")}";
                string TimeStartEnd = $"{n}Time Start: {LoadingStart.ToString()}{n}Time End: {LoadingEnd.ToString()}";
                LogInvoke($"End process... {n}{TimeitTook}{n}{TimeStartEnd}");

                e.Result = txtSrc.Text + "_output.txt";
            }
            catch (Exception ex)
            {
                LogInvoke(ex.ToString());
                e.Result = null;
            }
        }
        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change status
            pBar.Value = e.ProgressPercentage;
            string proc = $"Finished item {e.UserState} of {COUNT_MAX}. - {e.ProgressPercentage.ToString()}% done...";
            lblStatus.Text = proc;
            Log(proc);
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Text = "Writing Full Log to file.....";
            Log(lblStatus.Text);
            LogToFile(PATH_LOG_FULL, txtLog.Text);
            btnStart.Enabled = true;
            lblStatus.Text = "DONE!";
            Log("DONE!");

            if (e.Result != null)
            {
                string output = e.Result as string;
                btnOutput.Tag = output;
            }
        }
        //################################################################################## Events
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtSrc.Text))
            { 
                btnStart.Enabled = false;
                pBar.Value = 0;
                Log("Start Process...");
                bgw.RunWorkerAsync();
            }
            else
            {
                ShowWarning("Source file is invalid!");
                txtSrc.Focus();
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            // Open output, saved in Tag
            try
            {
                string file = btnOutput.Tag.ToString();
                Process.Start(file);
            }
            catch (Exception ex)
            {
                // Log error
                ShowWarning("Cannot open output file!");
                Log("Cannot open output file!");
                Log(ex.ToString());
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DeleteLogs();
        }
    }
}
