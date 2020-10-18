using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextStrip
{
    public partial class frmSimple : Form
    {
        public frmSimple()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string src = txtOriginal.Text;
                string output = "";

                foreach (string s in src.Split('\n'))
                {
                    // Remove everything before ':', including that character
                    string temp = Regex.Replace(s, "^[^:]*:", "", RegexOptions.IgnorePatternWhitespace);
                    temp = temp.Replace(": ", "");

                    // Strip texts of special characters inside the bracket
                    Regex pattern = new Regex("[;,!?.]");
                    temp = pattern.Replace(temp, "");

                    // Strip texts of: Double whitespace
                    pattern = new Regex("[ ]{2}");
                    temp = pattern.Replace(temp, " ");

                    // Lowercase
                    temp = temp.ToLower();

                    output += temp;
                }

                // Trim whitespace
                output = output.Trim();

                txtOutput.Text = output;
            }
            catch (Exception ex)
            {
                // 
                MessageBox.Show($"Error occured!\n{ex.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
