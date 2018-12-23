using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddNumbers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double iResult;
            iResult = AddNumbers(Double.Parse(textBoxNumberOne.Text), Double.Parse(textBoxNumberTwo.Text));
            textBoxResult.Text = iResult.ToString();
        }

        private double AddNumbers(double iOne, double iTwo)
        {
            double iResult = 0;
            iResult = iOne + iTwo;
            return iResult;
        }
    }
}
