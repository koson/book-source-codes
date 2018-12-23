using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HostApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fontPicker1_Load(object sender, EventArgs e)
        {

        }

        private void fontPicker1_SelectedControlChanged(ControlChangedEventArgs e)
        {
            Text = e.ChangedFont.ToString() + ", " + e.ChangedColor.ToString();
        }
        

    }
}