using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PieChartApp
{
    public partial class Form1 : Form
    {
        Color tempSliceColor;
        string tempSliceName;
        int tempSliceSize;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pieChart1.AddSlice(new Slice("Mozzarella", 55, Color.FromArgb(255, 128, 128)));
            pieChart1.AddSlice(new Slice("Gorgonzola", 15, Color.FromArgb(128, 255, 128)));
            pieChart1.AddSlice(new Slice("Parmigiano", 25, Color.FromArgb(128, 128, 255)));
            pieChart1.AddSlice(new Slice("Ricotta", 25, Color.FromArgb(255, 128, 255)));
            // the first unnamed slice will be automatically named "Slice 0"
            pieChart1.AddSlice(new Slice("", 25, Color.FromArgb(255, 255, 128)));
            // the seconf unnamed slice will be automatically named "Slice 1"
            pieChart1.AddSlice(new Slice("", 25, Color.FromArgb(128, 255, 255)));
            // get the "Ricotta" slice
            Slice tempSlice = pieChart1.GetSlice("Ricotta");
            // if the slice exists i.e. has the name different form ""
            if (tempSlice.GetSliceName() != "")
                // and name it Ricotta cheese"
                tempSlice.SetSliceName("Ricotta cheese");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pieChart1.Print(false);
        }

        private void pieChart1_Load(object sender, EventArgs e)
        {

        }

        private void addSliceColorButton_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            tempSliceColor = colorDialog1.Color;
            addSliceColorButton.BackColor = tempSliceColor;

        }

        private void addSliceButton_Click(object sender, EventArgs e)
        {
            if (addSliceNameTextBox.Text.ToString().Length <= 20)
            {
                tempSliceName = addSliceNameTextBox.Text;
            }
            else
            {
                MessageBox.Show("The entered name is not un up to 20 char string");
                return;
            }

            if (ParseNumber(addSliceSizeTextBox.Text))
            {
                if (addSliceSizeTextBox.Text != "")
                    tempSliceSize = Int32.Parse(addSliceSizeTextBox.Text.ToString());
                if (tempSliceSize == 0)
                    return;
            }
            else
            {
                MessageBox.Show("The entered size is not an up to 8 digit number!");
                return;
            }

            if (tempSliceColor.A == 0)
                tempSliceColor = Color.FromArgb(255, 255, 255, 255);
            pieChart1.AddSlice(new Slice(tempSliceName, tempSliceSize, tempSliceColor));
            pieChart1.Invalidate();
            // clean name, size and color
            addSliceNameTextBox.Text = "";
            addSliceSizeTextBox.Text = "";
            tempSliceName = "";
            tempSliceColor = Color.FromArgb(255, 255, 255);
            tempSliceSize = 0;
            colorDialog1.Color = Color.FromArgb(255, 255, 255, 255);
            addSliceColorButton.BackColor = Color.FromArgb(236, 233, 216);
        }

        private void removeSliceButton_Click(object sender, EventArgs e)
        {
            tempSliceName = removeSliceNameTextBox.Text;
            pieChart1.RemoveSlice(tempSliceName);

        }
        private bool ParseNumber(string userNumber)
        {
            //create our parser that will do all our work for us
            Regex parser = new Regex(@"^\d{0,8}$");
            //return the bool result of parser's findings
            return parser.IsMatch(userNumber);
        }



    }
}