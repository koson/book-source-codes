using System;
using System.Windows.Forms;

namespace ImitatingFormDesigner
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.Run(new Form1());
    }
  }
}