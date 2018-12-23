// FormsYourWay - code for a Windows Forms application that ignores the 
//               Visual Studio forms designer but still includes the essentials
//               This file also includes Main()
// just these using statements
using System;
using System.Windows.Forms;
namespace FormsYourWay
{
  class Form1 : Form
  {
    // declare controls on form here:
    private TextBox textBox1;
    private TextBox textBox2;
    private Button button1;
    // don’t need the ComponentModel.Container stuff
    // form’s constructor:
    public Form1()
    {
      // don’t need the form designer stuff here
      // set form properties here, e.g. form’s Text property:
      // tinker with these and other properties at will
      this.ClientSize = new System.Drawing.Size(300, 200);
      this.Text = "Text Copy Application";
      // instantiate control objects here:
      textBox1 = new TextBox();
      textBox2 = new TextBox();
      button1 = new Button();
      this.SuspendLayout();  // reduces flicker when form loads
      // set control properties here, e.g. 
      // tinker with location coordinates until satisfied
      // textBox1
      textBox1.Location = new System.Drawing.Point(40, 40);
      textBox1.Size = new System.Drawing.Size(180, 0);
      textBox1.Name = "textBox1";   // change this if you like
      textBox1.TabIndex = 0;
      textBox1.Text = "I'm typing away over here";
      // textBox2
      textBox2.Location = new System.Drawing.Point(40, 80);
      textBox2.Size = new System.Drawing.Size(180, 0);
      textBox2.Name = "textBox2";
      textBox2.ReadOnly = true;
      textBox2.Text = "Program copies text into here";
      // button1
      button1.Location = new System.Drawing.Point(90, 120);
      button1.Name = "button1";
      button1.TabIndex = 1;
      button1.Text = "Copy";
      // now add an event handler for the button click event:
      // button1_Click is your name for the handler method (below)
      button1.Click += new System.EventHandler(this.button1_Click);
      // finally, add each control to form’s Controls collection:
      this.Controls.Add(textBox1);
      this.Controls.Add(textBox2);
      this.Controls.Add(button1);
      this.ResumeLayout(false);
      // any other stuff you might need
    }
    // don’t require the #region/#endregion directives: use if desired
    // don’t need the InitializeComponent stuff
    // start control event handler methods here:
    private void button1_Click(object sender, System.EventArgs e)
    {
      // do something here to carry out button click, e.g.
      textBox2.Text = textBox1.Text;
    }
    // Main() could be in a separate Program class if you prefer
    static void Main()
    {
      Application.Run(new Form1());
    }

    //}
  } // end of form
} // end of namespace
