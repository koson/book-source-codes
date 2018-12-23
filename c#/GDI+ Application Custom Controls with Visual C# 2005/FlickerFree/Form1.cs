using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlickerFree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbInterval_Scroll(object sender, EventArgs e)
        {
            scrollingText.ScrollTimeInterval = tbInterval.Value;
        }

        private void tbAmount_Scroll(object sender, EventArgs e)
        {
            scrollingText.ScrollPixelAmount = tbAmount.Value;
        }
    }
}