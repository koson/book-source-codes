namespace GradientLabelTest00
{
    partial class GradientLabel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GradientLabel
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Size = new System.Drawing.Size(150, 24);
            this.Text = "Gradient Label";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GradientLabel_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
