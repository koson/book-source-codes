using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageWarperApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            imageWarperControl1.ImageAngle = Double.Parse(angleBox.Text) * System.Math.PI / 180;
            imageWarperControl1.ImageScale = Double.Parse(scaleBox.Text) / 100;
            imageWarperControl1.ImageSkew = new SizeF(float.Parse(skewHorizontalBox.Text), float.Parse(skewVerticalBox.Text));

        }

        
    }
}