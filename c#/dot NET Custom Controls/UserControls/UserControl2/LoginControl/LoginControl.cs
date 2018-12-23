using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Akadia
{
	namespace LoginControl
	{
		// Implementation of a Login User Control
		public class LoginControl : System.Windows.Forms.UserControl
		{
			private System.Windows.Forms.Label lblUserName;
			private System.Windows.Forms.Label lblPassword;
			private System.Windows.Forms.TextBox txtUserName;
			private System.Windows.Forms.TextBox txtPassword;
			private System.Windows.Forms.Button btnLogin;
			private System.Windows.Forms.ErrorProvider erpLoginError;
			private System.Windows.Forms.StatusBar stbMessage;
			private System.ComponentModel.Container components = null;

			// Here we use the predefined System.EventHandler delegate. 
			// This delegate is useful if you want to define an event
			// that has no additional data. The event will be passed an
			// empty System.EcentArgs parameter instead. This is the
			// delegate used by many of the Windows Forms.
			public delegate void EventHandler(Object sender, EventArgs e);
			public event EventHandler LoginSuccess;
			public event EventHandler LoginFailed;

			// Constructor
			public LoginControl()
			{
				InitializeComponent();
			}

			// Clean up any resources being used.
			protected override void Dispose( bool disposing )
			{
				if( disposing )
				{
					if( components != null )
						components.Dispose();
				}
				base.Dispose( disposing );
			}

		#region Component Designer generated code
			/// <summary>
			/// Required method for Designer support - do not modify 
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.lblUserName = new System.Windows.Forms.Label();
				this.lblPassword = new System.Windows.Forms.Label();
				this.txtUserName = new System.Windows.Forms.TextBox();
				this.txtPassword = new System.Windows.Forms.TextBox();
				this.btnLogin = new System.Windows.Forms.Button();
				this.erpLoginError = new System.Windows.Forms.ErrorProvider();
				this.stbMessage = new System.Windows.Forms.StatusBar();
				this.SuspendLayout();
				// 
				// lblUserName
				// 
				this.lblUserName.Location = new System.Drawing.Point(40, 20);
				this.lblUserName.Name = "lblUserName";
				this.lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
				this.lblUserName.Size = new System.Drawing.Size(37, 16);
				this.lblUserName.TabIndex = 0;
				this.lblUserName.Text = "Name";
				// 
				// lblPassword
				// 
				this.lblPassword.Location = new System.Drawing.Point(18, 51);
				this.lblPassword.Name = "lblPassword";
				this.lblPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
				this.lblPassword.Size = new System.Drawing.Size(61, 15);
				this.lblPassword.TabIndex = 1;
				this.lblPassword.Text = "Password";
				// 
				// txtUserName
				// 
				this.txtUserName.Location = new System.Drawing.Point(83, 16);
				this.txtUserName.Name = "txtUserName";
				this.txtUserName.Size = new System.Drawing.Size(151, 20);
				this.txtUserName.TabIndex = 1;
				this.txtUserName.Text = "";
				// 
				// txtPassword
				// 
				this.txtPassword.Location = new System.Drawing.Point(83, 48);
				this.txtPassword.Name = "txtPassword";
				this.txtPassword.PasswordChar = '*';
				this.txtPassword.Size = new System.Drawing.Size(151, 20);
				this.txtPassword.TabIndex = 2;
				this.txtPassword.Text = "";
				// 
				// btnLogin
				// 
				this.btnLogin.Location = new System.Drawing.Point(95, 104);
				this.btnLogin.Name = "btnLogin";
				this.btnLogin.TabIndex = 3;
				this.btnLogin.Text = "Login";
				this.btnLogin.Click += new System.EventHandler(this.loginButtonClicked);
				// 
				// erpLoginError
				// 
				this.erpLoginError.DataMember = null;
				// 
				// stbMessage
				// 
				this.stbMessage.Location = new System.Drawing.Point(0, 137);
				this.stbMessage.Name = "stbMessage";
				this.stbMessage.Size = new System.Drawing.Size(265, 35);
				this.stbMessage.SizingGrip = false;
				this.stbMessage.TabIndex = 5;
				// 
				// LoginControl
				// 
				this.Controls.AddRange(new System.Windows.Forms.Control[] {
																			  this.stbMessage,
																			  this.btnLogin,
																			  this.txtPassword,
																			  this.txtUserName,
																			  this.lblPassword,
																			  this.lblUserName});
				this.Name = "LoginControl";
				this.Size = new System.Drawing.Size(265, 172);
				this.ResumeLayout(false);

			}
		#endregion

			// This is the very simple Login Check Validation
			// The Password mus be ... "secret" .....
			private bool LoginCheck(string pName, string pPassword)
			{
				return pPassword.Equals("secret");
			}

			// Validate Login, in any case call the LoginSuccess or
			// LoginFailed event, which will notify the Application's
			// Event Handlers.
			private void loginButtonClicked(object sender, System.EventArgs e)
			{
				// User Name Validation
				if (txtUserName.Text.Length == 0) 
				{
					erpLoginError.SetError(txtUserName,"Please enter a user name");
					stbMessage.Text = "Please enter a user name";
					return;
				}
				else 
				{
					erpLoginError.SetError(txtUserName,"");
					stbMessage.Text = "";
				}

				// Password Validation
				if (txtPassword.Text.Length == 0) 
				{
					erpLoginError.SetError(txtPassword,"Please enter a password");
					stbMessage.Text = "Please enter a password";
					return;
				}
				else 
				{
					erpLoginError.SetError(txtPassword,"");
					stbMessage.Text = "";
				}

				// Check Password
				if (LoginCheck(txtUserName.Text, txtPassword.Text))
				{
					// If there any Subscribers for the LoginSuccess
					// Event, notify them ...
					if (LoginSuccess != null)
					{
						LoginSuccess(this, new System.EventArgs());
					}
				}
				else
				{
					// If there any Subscribers for the LoginFailed
					// Event, notify them ...
					if (LoginFailed != null)
					{
						LoginFailed(this, new System.EventArgs());
					}
				}
			}

			// Read-Write Property for User Name Label
			public string LabelName
			{
				get
				{
					return lblUserName.Text;
				}
				set
				{
					lblUserName.Text = value;
				}
			}

			// Read-Write Property for User Name Password
			public string LabelPassword
			{
				get
				{
					return lblPassword.Text;
				}
				set
				{
					lblPassword.Text = value;
				}
			}

			// Read-Write Property for Login Button Text
			public string LoginButtonText
			{
				get
				{
					return btnLogin.Text;
				}
				set
				{
					btnLogin.Text = value;
				}
			}

			// Read-Only Property for User Name
			[Browsable(false)]
			public string UserName
			{
				set 
				{
					txtUserName.Text = value;
				}
			}

			// Read-Only Property for Password
			[Browsable(false)]
			public string Password
			{
				set 
				{
					txtPassword.Text = value;
				}
			}

		}
	}
}