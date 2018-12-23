using System;
using System.Text;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool flag;
        decimal amount;
        decimal interestRate;
        decimal months;
        decimal payment;
        decimal temp;

        flag = decimal.TryParse(txtAmount.Text, out amount);
        if (flag == false)
            payment = 0.0M;     // Bad input

        flag = decimal.TryParse(txtInterestRate.Text, out interestRate);
        if (flag == false)
            payment = 0.0M;     // Bad input

        flag = decimal.TryParse(txtMonths.Text, out months);
        if (flag == false)
            payment = 0.0M;     // Bad input

        interestRate /= 1200;   // Because we need it as a decimal fraction by month
        temp = (decimal) Math.Pow(1.0 + (double) interestRate, (double)months);
        payment = interestRate * amount * (temp / (temp - 1.0M));


        StringBuilder myPayment = new StringBuilder();
        myPayment.AppendFormat("${0:F2}", payment);

        lblPayment.Visible = true;
        txtPayment.Visible = true;
        txtPayment.Text = myPayment.ToString();

    }
}