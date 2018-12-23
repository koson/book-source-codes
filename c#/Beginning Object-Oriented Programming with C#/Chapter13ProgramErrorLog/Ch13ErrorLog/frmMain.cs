using System;
using System.Windows.Forms;
using System.IO;

public class frmMain : Form
{
    string err;

    private RadioButton rbNumericOverflow;
    private RadioButton rbFileNotFound;
    private RadioButton rbDivideBy0;
    private Button btnThrow;
    private Button btnClose;
    private TextBox txtErrorMsgs;
    private GroupBox groupBox1;
    #region Windows code
    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.rbNumericOverflow = new System.Windows.Forms.RadioButton();
        this.rbFileNotFound = new System.Windows.Forms.RadioButton();
        this.rbDivideBy0 = new System.Windows.Forms.RadioButton();
        this.btnThrow = new System.Windows.Forms.Button();
        this.btnClose = new System.Windows.Forms.Button();
        this.txtErrorMsgs = new System.Windows.Forms.TextBox();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.rbNumericOverflow);
        this.groupBox1.Controls.Add(this.rbFileNotFound);
        this.groupBox1.Controls.Add(this.rbDivideBy0);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(310, 60);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Exception:";
        // 
        // rbNumericOverflow
        // 
        this.rbNumericOverflow.AutoSize = true;
        this.rbNumericOverflow.Location = new System.Drawing.Point(197, 19);
        this.rbNumericOverflow.Name = "rbNumericOverflow";
        this.rbNumericOverflow.Size = new System.Drawing.Size(109, 17);
        this.rbNumericOverflow.TabIndex = 2;
        this.rbNumericOverflow.TabStop = true;
        this.rbNumericOverflow.Text = "Numeric Overflow";
        this.rbNumericOverflow.UseVisualStyleBackColor = true;
        // 
        // rbFileNotFound
        // 
        this.rbFileNotFound.AutoSize = true;
        this.rbFileNotFound.Location = new System.Drawing.Point(100, 19);
        this.rbFileNotFound.Name = "rbFileNotFound";
        this.rbFileNotFound.Size = new System.Drawing.Size(91, 17);
        this.rbFileNotFound.TabIndex = 1;
        this.rbFileNotFound.TabStop = true;
        this.rbFileNotFound.Text = "File Not found";
        this.rbFileNotFound.UseVisualStyleBackColor = true;
        // 
        // rbDivideBy0
        // 
        this.rbDivideBy0.AutoSize = true;
        this.rbDivideBy0.Location = new System.Drawing.Point(16, 19);
        this.rbDivideBy0.Name = "rbDivideBy0";
        this.rbDivideBy0.Size = new System.Drawing.Size(78, 17);
        this.rbDivideBy0.TabIndex = 0;
        this.rbDivideBy0.TabStop = true;
        this.rbDivideBy0.Text = "Divide by 0";
        this.rbDivideBy0.UseVisualStyleBackColor = true;
        // 
        // btnThrow
        // 
        this.btnThrow.Location = new System.Drawing.Point(340, 12);
        this.btnThrow.Name = "btnThrow";
        this.btnThrow.Size = new System.Drawing.Size(75, 23);
        this.btnThrow.TabIndex = 1;
        this.btnThrow.Text = "&Throw";
        this.btnThrow.UseVisualStyleBackColor = true;
        this.btnThrow.Click += new System.EventHandler(this.btnThrow_Click);
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(340, 49);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "&Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // txtErrorMsgs
        // 
        this.txtErrorMsgs.Location = new System.Drawing.Point(12, 93);
        this.txtErrorMsgs.Multiline = true;
        this.txtErrorMsgs.Name = "txtErrorMsgs";
        this.txtErrorMsgs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtErrorMsgs.Size = new System.Drawing.Size(403, 170);
        this.txtErrorMsgs.TabIndex = 3;
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(432, 279);
        this.Controls.Add(this.txtErrorMsgs);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.btnThrow);
        this.Controls.Add(this.groupBox1);
        this.Name = "frmMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Error Logger";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
        rbDivideBy0.Checked = true;
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    private void btnThrow_Click(object sender, EventArgs e)
    {

        try
        {
            // To use general catch, uncomment the next line
            // throw new System.OutOfMemoryException();

            if (rbDivideBy0.Checked == true)
            {
                throw new System.DivideByZeroException();
            }
            else
            {
                if (rbFileNotFound.Checked == true)
                {
                    throw new System.IO.FileNotFoundException();
                }
                else
                {
                    throw new System.OverflowException();
                }
            }
        }
        catch (DivideByZeroException ex)
        {
            MessageBox.Show("DivideByZeroException thrown.", "Exception Error");
            err = "DivideByZeroException: " + ex.StackTrace;
        }
        catch (FileNotFoundException ex)
        {
            MessageBox.Show("FileNotFoundException thrown.", "Exception Error");
            err = "FileNotFoundException" + ex.StackTrace;
        }
        catch (OverflowException ex)
        {
            MessageBox.Show("OverflowException thrown.", "Exception Error");
            err = "OverflowException" + ex.StackTrace;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.StackTrace + "", "Exception Error");
            err = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            clsErrorLog myErrLog = new clsErrorLog(err);
            myErrLog.PathName = Application.StartupPath;

            myErrLog.WriteErrorLog();
            txtErrorMsgs.Text = myErrLog.ReadErrorLog();
        }

    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}