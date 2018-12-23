using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;


public class frmMain : Form
{
    private System.Windows.Forms.Button btnSelectPrintFile;
    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.Button btnPrint;
    private System.Windows.Forms.TextBox txtSampleOutput;
    private PrintDialog prdMyPrintDialog;
    private Label lblSample;

    const int LINESTOPRINT = 6;    // Let them peek at this many lines

    private System.ComponentModel.BackgroundWorker myBackgroundWorker;
    private Label lblJobDone; 
    string inFile = null;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.btnSelectPrintFile = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtSampleOutput = new System.Windows.Forms.TextBox();
            this.prdMyPrintDialog = new System.Windows.Forms.PrintDialog();
            this.lblSample = new System.Windows.Forms.Label();
            this.myBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblJobDone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectPrintFile
            // 
            this.btnSelectPrintFile.Location = new System.Drawing.Point(12, 370);
            this.btnSelectPrintFile.Name = "btnSelectPrintFile";
            this.btnSelectPrintFile.Size = new System.Drawing.Size(111, 23);
            this.btnSelectPrintFile.TabIndex = 0;
            this.btnSelectPrintFile.Text = "Select Print &File";
            this.btnSelectPrintFile.UseVisualStyleBackColor = true;
            this.btnSelectPrintFile.Click += new System.EventHandler(this.btnSelectPrintFile_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(504, 370);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(111, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(149, 370);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(111, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "&Print File";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtSampleOutput
            // 
            this.txtSampleOutput.Location = new System.Drawing.Point(12, 12);
            this.txtSampleOutput.Multiline = true;
            this.txtSampleOutput.Name = "txtSampleOutput";
            this.txtSampleOutput.Size = new System.Drawing.Size(603, 315);
            this.txtSampleOutput.TabIndex = 3;
            // 
            // prdMyPrintDialog
            // 
            this.prdMyPrintDialog.UseEXDialog = true;
            // 
            // lblSample
            // 
            this.lblSample.Location = new System.Drawing.Point(12, 330);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(603, 20);
            this.lblSample.TabIndex = 4;
            this.lblSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSample.Visible = false;
            // 
            // myBackgroundWorker
            // 
            this.myBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.myBackgroundWorker_DoWork);
            this.myBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.myBackgroundWorker_RunWorkerCompleted);
            // 
            // lblJobDone
            // 
            this.lblJobDone.ForeColor = System.Drawing.Color.Green;
            this.lblJobDone.Location = new System.Drawing.Point(266, 373);
            this.lblJobDone.Name = "lblJobDone";
            this.lblJobDone.Size = new System.Drawing.Size(225, 20);
            this.lblJobDone.TabIndex = 5;
            this.lblJobDone.Text = "Print job finished.";
            this.lblJobDone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblJobDone.Visible = false;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(627, 405);
            this.Controls.Add(this.lblJobDone);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.txtSampleOutput);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSelectPrintFile);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select and Print a Text File";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    public frmMain()
    {
        InitializeComponent();
    }


    [STAThread]
    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnSelectPrintFile_Click(object sender, EventArgs e)
    {
        clsSelectPrintFile myFile = new clsSelectPrintFile();
        myFile.SelectPrintFile();
        inFile = myFile.FileName;
        myFile.ReadSampleFromFile(inFile, LINESTOPRINT);
        txtSampleOutput.Text = myFile.GetBuffer;
        lblSample.Text = "First " + LINESTOPRINT.ToString() + " lines of output from:  " + inFile;
        lblSample.Visible = true;
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            lblJobDone.Visible = false;     // Make sure "job done" message is hidden
            if (inFile == null)
            {
                MessageBox.Show("Select a file to print first", "Input File Name Error");
                return;
            }
            clsPrint doPrint = new clsPrint(inFile);
            DialogResult result = prdMyPrintDialog.ShowDialog();

            // If the result is OK then print the document.

            if (result == DialogResult.OK)
            {
                myBackgroundWorker.RunWorkerAsync(doPrint);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
            return;
        }
    }

    private void myBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        try
        {
            clsPrint myPrinterJob = e.Argument as clsPrint;
            // If you want to test this without wasting paper, uncomment the 
            // next line and comment out the call to myPrinterJob.Print();
            Thread.Sleep(10000);
            //myPrinterJob.Print();   // Comment out this line to avoid prinnting

            e.Result = myPrinterJob;
        } catch (Exception ex) {
            MessageBox.Show("Error in DoWork: " + ex.Message);
        }

    }

    private void myBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // Let them know the job's done...
        lblJobDone.Visible = true;
    }

   


}

