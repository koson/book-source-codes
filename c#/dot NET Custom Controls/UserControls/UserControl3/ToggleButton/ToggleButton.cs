using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Akadia
{
	namespace ToggleButton
	{
		// The ToggleButton class is inherited from the
		// System.Windows.Forms.CheckBox Class
		public class ToggleButton : System.Windows.Forms.CheckBox
		{
			// Fields
			private string _checkedText;
			private string _uncheckedText;
			private Color _checkedColor;
			private Color _uncheckedColor;

			// Constructor
			public ToggleButton()
			{
				// If Appearance value is set to Appearance.Normal,
				// the check box has a typical appearance.
				// If the value is set to Button, the check box appears
				// like a toggle button, which may be toggled to
				// an up or down state.
				this.Appearance = Appearance.Button;

				// Set Default toggled Text
				this._checkedText = "Checked";
				this._uncheckedText = "Unchecked";

				// Set Default toggled Color
				this._checkedColor = Color.Gray;
				this._uncheckedColor = this.BackColor;
			}

			// Public Properties, can be accessed in Property Panel
			public string CheckedText
			{
				get { return this._checkedText; }
				set { this._checkedText = value; }
			}

			public string UncheckedText
			{
				get { return this._uncheckedText; }
				set { this._uncheckedText = value; }
			}

			public Color CheckedColor
			{
				get { return this._checkedColor; }
				set { this._checkedColor = value; }
			}

			public Color UncheckedColor
			{
				get { return this._uncheckedColor; }
				set { this._uncheckedColor = value; }
			}

			// When the user clicks a toggle Button, the Text and
			// BackColor properties should be set according to the Checked
			// state of the button. The natural place to do this is
			// the Click event. However, keep in mind that you only
			// want to extend the default Click event supplied with
			// the CheckBox class rather than replacing is. In the .NET
			// Framework documentation, you will be notice that controls
			// typically have a protected OnXXX method that raises each
			// event (where XXX is the name of the event) - for example
			// the Click event is raised by the OnClick method. The Control
			// call these methods when an event occurs. If you want to
			// extend the Click event, the Trick is therefore to override
			// the OnClick method.
			protected override void OnClick(EventArgs e)
			{
				base.OnClick(e); // Call the CheckBox Baseclass

				// Set Text and Color according to the
				// current state
				if (this.Checked)
				{
					this.Text = this._checkedText;
					this.BackColor = this._checkedColor;
				}
				else
				{
					this.Text = this._uncheckedText;
					this.BackColor = this._uncheckedColor;
				}
			}
		}
	}
}
