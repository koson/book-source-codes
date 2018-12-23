using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RegexApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ParseSerialNumber(textBox1.Text))
                MessageBox.Show("Serial Number is valid!", "Regex results:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No match!", "Regex results:", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        }
        private bool ParseSerialNumber(string userData)
        {
            //create our parser that will do all our work for us
            Regex parser = new Regex(@"^[A-Z]{3}\d{4,7}[AB]\d$");
            //return the bool result of parser's findings
            return parser.IsMatch(userData);
        }

    }
}