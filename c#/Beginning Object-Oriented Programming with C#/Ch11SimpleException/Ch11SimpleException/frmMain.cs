#define DEBUG
//#undef DEBUG
using System;
using System.Windows.Forms;

public class frmMain : Form
{

    private Button btnCalc;
    #region Windows code
    private void InitializeComponent()
    {
        this.btnCalc = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // btnCalc
        // 
        this.btnCalc.Location = new System.Drawing.Point(12, 12);
        this.btnCalc.Name = "btnCalc";
        this.btnCalc.Size = new System.Drawing.Size(75, 23);
        this.btnCalc.TabIndex = 0;
        this.btnCalc.Text = "Ca&lculate";
        this.btnCalc.UseVisualStyleBackColor = true;
        this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
        // 
        // frmMain
        // 
        this.ClientSize = new System.Drawing.Size(292, 266);
        this.Controls.Add(this.btnCalc);
        this.Name = "frmMain";
        this.Text = "Simple Exception";
        this.ResumeLayout(false);

    }
    #endregion

    public frmMain()
    {
        InitializeComponent();
    }

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }


    private void btnCalc_Click(object sender, EventArgs e)
    {
        int exp1 = 0;
        int exp2 = 5;
        int result;

#if DEBUG
        MessageBox.Show("exp1 = " + exp1.ToString());

#endif

        try
        {
            throw new ArgumentOutOfRangeException();
            result = exp2 / exp1;
        }
        catch (DivideByZeroException)
        {
            MessageBox.Show("Divide by zero error.", "Exception Thrown");
            return;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.StackTrace, "Exception Thrown");
        }
        finally
        {
            MessageBox.Show("In finally");
        }

    }
}