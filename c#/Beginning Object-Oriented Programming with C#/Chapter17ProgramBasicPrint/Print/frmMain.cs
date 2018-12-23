using System;
using System.Windows.Forms;

public class frmMain : Form
{
    private System.Windows.Forms.Button btnSelectPrintFile;
    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.Button btnPrint;
    private System.Windows.Forms.TextBox txtSampleOutput;
    private PrintDialog prdMyPrintDialog;
    private Label lblSample;

    const int LINESTOPRINT = 6;
    private PrintPreviewDialog printPreviewDialog1;     // Let them peek at this many lines
    string inFile = null;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnSelectPrintFile = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtSampleOutput = new System.Windows.Forms.TextBox();
            this.prdMyPrintDialog = new System.Windows.Forms.PrintDialog();
            this.lblSample = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
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
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(627, 405);
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
            if (inFile == null)
            {
                MessageBox.Show("Select a file to print first", "Input File Name Error");
                return;
            }
            DialogResult result = prdMyPrintDialog.ShowDialog();
            clsPrint doPrint = new clsPrint(inFile);

            // If the result is OK then print the document.

            if (result == DialogResult.OK)
            {
                doPrint.Print();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
            return;
        }
    }



}

