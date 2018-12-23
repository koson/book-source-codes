using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoundPlayerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tinyNoiseMaker1_PlayStart(object sender, EventArgs e)
        {
            Text = "Started Play of: " + tinyNoiseMaker1.FileName;
        }

        private void tinyNoiseMaker1_PlayStop(object sender, EventArgs e)
        {
            Text = "Stopped Play of : " + tinyNoiseMaker1.FileName;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            tinyNoiseMaker1.FileName = "C:\\WINDOWS\\Media\\tada.wav";
            tinyNoiseMaker1.Play();
        }
    }
}