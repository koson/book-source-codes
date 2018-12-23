using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImitatingFormDesigner
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      textBox2.Text = textBox1.Text;
    }
  }
}