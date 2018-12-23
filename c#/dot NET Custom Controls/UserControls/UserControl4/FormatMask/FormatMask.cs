using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Akadia
{
	namespace FormatMask
	{
		// Extended User Control to implement an Edit Mask Text Box
		public class EditMask : System.Windows.Forms.TextBox
		{
			// Fields
			private string _mask;

			// Properties
			public string Mask
			{
				get { return _mask; }
				set 
				{ 
					_mask = value; 
					this.Text = "";
				}
			}

			// To use the masked control, the application programmer chooses
			// a mask and applies it to the Mask property of the control.
			// The number sign (#) represents any number, and the period (.)
			// represents any letter. All other characters in the mask
			// are treated as fixed characters, and are inserted automatically
			// when needed. For example, in the phone number mask (###) ###-####
			// the first bracket is inserted automatically when the user types
			// the first number.
			protected override void OnKeyPress(KeyPressEventArgs e)
			{
				if (Mask != "")
				{
					// Suppress the typed character.
					e.Handled = true;

					string newText = this.Text;

					// Loop through the mask, adding fixed characters as needed.
					// If the next allowed character matches what the user has
					// typed in (a number or letter), that is added to the end.
					bool finished = false;
					for (int i = this.SelectionStart; i < _mask.Length; i++)
					{ 
						switch (_mask[i].ToString())
						{
							case "#" :
								// Allow the keypress as long as it is a number.
								if (Char.IsDigit(e.KeyChar))
								{
									newText += e.KeyChar.ToString();
									finished = true;
									break;
								}
								else
								{
									// Invalid entry; exit and don't change the text.
									return;
								}
							case "." :
								// Allow the keypress as long as it is a letter.
								if (Char.IsLetter(e.KeyChar))
								{
									newText += e.KeyChar.ToString();
									finished = true;
									break;
								}
								else
								{
									// Invalid entry; exit and don't change the text.
									return;
								}
							default :
								// Insert the mask character.
								newText += _mask[i];
								break;
						}
						if (finished)
						{ break; }
					}

					// Update the text.
					this.Text = newText;
					this.SelectionStart = this.Text.Length;
				}
				// base.OnKeyPress(e);
			}    
  
			// Stop special characters.
			protected override void OnKeyDown(KeyEventArgs e)
			{
				e.Handled = true;
			}
		}
	}
}
