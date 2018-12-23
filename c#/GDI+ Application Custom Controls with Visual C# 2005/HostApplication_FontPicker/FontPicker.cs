using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HostApplication
{
    /// <remarks>
    /// The Font Picker user control
    /// </remarks>

    public partial class FontPicker : UserControl
    {
        Color selectedColor;
        FontFamily selectedFont;
        List<string> colors;
        List<FontFamily> fontFamilies;

        /// <summary>
        /// The FontPicker creates a generic list with known colors
        /// and populates the combobox Items collection.
        /// </summary>

        public FontPicker()
        {
            // prepare the fields
            selectedColor = Color.Black;
            selectedFont = new FontFamily("Arial");
            colors = new List<string>();
            fontFamilies = new List<FontFamily>();
            // intialize the list of colors
            foreach (string knownColor in Enum.GetNames(typeof(KnownColor)))
            {
                if (!Color.FromName(knownColor).IsSystemColor)
                {
                    colors.Add(knownColor);
                }
            }
            // initialize the list of font families
            foreach (FontFamily fontFamily in FontFamily.Families)
            {
                if (fontFamily.IsStyleAvailable(FontStyle.Regular))
                {
                    fontFamilies.Add(fontFamily);
                }
            }
            // perform control initialization tasks
            InitializeComponent();
        }

        private void colorCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                bool hasFocus = false;
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    hasFocus = true;
                    DrawColorBoxHighlight(hasFocus, e.Graphics, e.Bounds);
                }
                else
                {
                    DrawColorBoxHighlight(hasFocus, e.Graphics, e.Bounds);
                }
                ColorItem(e.Graphics, e.Bounds, e.Index, hasFocus);
            }

        }
        void ColorItem(Graphics graphics, Rectangle rectangle, int p, bool hasFocus)
        {
            SolidBrush text = new SolidBrush(Color.FromKnownColor(KnownColor.MenuText));
            SolidBrush color = new SolidBrush(Color.FromName(colors[p]));
            Pen pen = new Pen(Color.Black, 1);
            if (hasFocus)
            {
                text.Color = Color.FromKnownColor(KnownColor.HighlightText);
            }
            graphics.FillRectangle(color, rectangle.Left + 2, rectangle.Top + 2, 20, rectangle.Height - 4);
            graphics.DrawRectangle(pen, new Rectangle(rectangle.Left + 1, rectangle.Top + 1, 21, rectangle.Height - 3));
            graphics.DrawString(colors[p], colorCombo.Font, text, rectangle.Left + 28, rectangle.Top);
        }

        void DrawColorBoxHighlight(bool isSelected, Graphics graphics, Rectangle rectangle)
        {
            if (!isSelected)
            {
                graphics.FillRectangle(new SolidBrush(SystemColors.Window), rectangle);
            }
            else
            {
                Pen borderPen = new Pen(Color.FromKnownColor(KnownColor.Highlight));
                SolidBrush backgroundBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
                graphics.FillRectangle(backgroundBrush, rectangle); graphics.DrawRectangle(borderPen, rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);
                borderPen.Dispose();
                backgroundBrush.Dispose();
            }
        }

        private void fontCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                bool hasFocus = false;
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    hasFocus = true;
                    DrawFontBoxHighlight(hasFocus, e.Graphics, e.Bounds);
                }
                else
                {
                    DrawFontBoxHighlight(hasFocus, e.Graphics, e.Bounds);
                }
                DrawFontItem(e.Graphics, e.Bounds, e.Index, hasFocus);
            }

        }
        private void DrawFontBoxHighlight(bool isSelected, Graphics graphics, Rectangle rectangle)
        {
            if (!isSelected)
            {
                graphics.FillRectangle(new SolidBrush(SystemColors.Window), rectangle);
            }
            else
            {
                Pen borderPen = new Pen(Color.FromKnownColor(KnownColor.Highlight));
                SolidBrush backgroundBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
                graphics.FillRectangle(backgroundBrush, rectangle); graphics.DrawRectangle(borderPen, rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);
                borderPen.Dispose();
                backgroundBrush.Dispose();
            }
        }

        void DrawFontItem(Graphics graphics, Rectangle rectangle, int p, bool hasFocus)
        {
            float fontSize = 10;
            Font font = new Font(fontFamilies[p], fontSize);
            SolidBrush text = new SolidBrush(SystemColors.ControlText);
            SolidBrush color = new SolidBrush(SystemColors.ControlText);
            Pen pen = new Pen(Color.Black, 1);
            if (hasFocus)
            {
                text.Color = Color.FromKnownColor(KnownColor.HighlightText);
            }
            graphics.DrawString(fontFamilies[p].Name, font, text, rectangle);
        }

        private void FontPicker_Load(object sender, EventArgs e)
        {
            foreach (string item in colors)
            {
                colorCombo.Items.Add(item);
            }
            foreach (FontFamily font in fontFamilies)
            {
                fontCombo.Items.Add(font.Name);
            }

        }
        /// <summary>
        /// Gets the selected color from the color dropdown list.
        /// </summary>
        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
        }

        /// <summary>
        /// Gets the selected font from the font dropdown list.
        /// </summary>
        public FontFamily SelectedFont
        {
            get
            {
                return selectedFont;
            }
        }
        /// <summary>
        /// The SelectedColorChanged event is fired when the selected color changes.
        /// </summary>
        public event SelectedControlChangedEvent SelectedControlChanged;

        private void OnSelectedControlChanged(ControlChangedEventArgs controlEventArgs)
        {
            if (SelectedControlChanged != null)
            {
                SelectedControlChanged(controlEventArgs);
            }
        }

        private void colorCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedColor = Color.FromName(colorCombo.Text);
            ControlChangedEventArgs controlEventArgs =
              new ControlChangedEventArgs(selectedColor, selectedFont);
            OnSelectedControlChanged(controlEventArgs);

        }

        private void fontCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFont = fontFamilies[fontCombo.SelectedIndex];
            ControlChangedEventArgs controlEventArgs =
              new ControlChangedEventArgs(selectedColor, selectedFont);
            OnSelectedControlChanged(controlEventArgs);

        }
    }

   
    /// <remarks >
    /// The SelectedColorChangedEvent delegate
    /// </remarks>
    public delegate void SelectedControlChangedEvent(ControlChangedEventArgs e);

    /// <remarks>
    /// The custom event args for the SelectedColorChanged event
    /// </remarks>
    public class ControlChangedEventArgs : EventArgs
    {
        private Color changedColor;
        private FontFamily changedFont;

        /// <summary>
        /// The new color.
        /// </summary>
        public Color ChangedColor
        {
            get
            {
                return changedColor;
            }
        }

        /// <summary>
        /// The new font family.
        /// </summary>
        public FontFamily ChangedFont
        {
            get
            {
                return changedFont;
            }
        }

        /// <summary>
        /// The ColorChangedEventArgs are custom event args
        /// for the SelectedControlChanged event.
        /// </summary>
        /// <param name="changedColor">The new color.</param>
        /// <param name="changedFont">The new font.</param>
        internal ControlChangedEventArgs(Color changedColor, FontFamily changedFont)
        {
            this.changedColor = changedColor;
            this.changedFont = changedFont;
        }
    }
}



