using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace SoundPlayerTest
{
    public partial class TinyNoiseMaker : UserControl
    {
        public event EventHandler PlayStart;
        public event EventHandler PlayStop;

        protected virtual void OnPlayStart(EventArgs e)
        {
            if (PlayStart != null)
            {
                PlayStart(this, e);
            }
        }

        protected virtual void OnPlayStop(EventArgs e)
        {
            if (PlayStop != null)
            {
                PlayStop(this, e);
            }
        }
        SoundPlayer soundPlayer;
        public TinyNoiseMaker()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
            }
                
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }
        public void Play()
        {
            soundPlayer.Play();
            OnPlayStart(EventArgs.Empty);
        }
        public void Stop()
        {
            soundPlayer.Stop();
            OnPlayStop(EventArgs.Empty);
        }
        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    soundPlayer.Stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                }
            }
        }



    }
}
