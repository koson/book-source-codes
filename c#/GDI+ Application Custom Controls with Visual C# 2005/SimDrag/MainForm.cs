using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimDrag
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void dragArea1_LocationChanged(LocationChangedEventArgs e)
        {
            Text = "X: " + e.X + ", Y: " + e.Y;
        }
    }
}