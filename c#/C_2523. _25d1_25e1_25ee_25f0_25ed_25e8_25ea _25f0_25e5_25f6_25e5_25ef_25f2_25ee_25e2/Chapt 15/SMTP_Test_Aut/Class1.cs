using System;
using System.Web.Mail;

namespace SMTP_Test_Aut
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			SmtpMail.SmtpServer = "smtp.mail.ru";

			MailMessage mailer  = new MailMessage();

			// авторизация SMTP 
			mailer.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1; // cdoBasic 
			mailer.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"    ] = "user"; 
			mailer.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"    ] = "password";
			mailer.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"       ] = 2; // cdoSendUsingPort

			mailer.From    = "???@mail.ru";
			mailer.Body    = "текст письма";
			mailer.Subject = "заголовок письма";
			mailer.To      = "=== кому ===";
			// mailer.Bcc = 
			// mailer.Cc  = 

			try
			{
				SmtpMail.Send(mailer);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}
	}
}
