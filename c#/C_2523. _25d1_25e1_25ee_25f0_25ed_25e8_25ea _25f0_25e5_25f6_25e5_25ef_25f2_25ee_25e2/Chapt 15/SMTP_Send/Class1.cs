using System;
using System.Web.Mail;

namespace SMTP_Send
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			SmtpMail.SmtpServer = "smtp.mail.ru";

			MailMessage mailer  = new MailMessage();
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
