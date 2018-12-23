using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SendMail_Socket
{
	class Class1
	{
		static void Main()
		{
			string subject		= "Test mail";
			string to_name	    = "Pasha";
			string to_mail		= "<???@???>";
			string from_name    = "PVA";
			string from_mail	= "<???@???>";
			string smtpServer	= "smtp.mail.ru";
			int	   smtpPort		= 25;

			// generate an RFC compliant email

			// Заголовок
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Subject: {0}", subject);
			sb.Append(Environment.NewLine);
			sb.AppendFormat("To: {0}{1}", to_name, to_mail);
			sb.Append(Environment.NewLine);
			sb.AppendFormat("From: {0}{1}", from_name, from_mail);
			sb.Append(Environment.NewLine);
			sb.AppendFormat("Date: {0}", DateTime.Now.ToString("ddd, d MMM yyyy H:m:s zz00"));
			sb.Append(Environment.NewLine);
			sb.AppendFormat("Content-Type: text/plain");
			sb.Append(Environment.NewLine);
			sb.AppendFormat("X-Mailer: C# Mailer Example (http://www.aspemporium.com/)");
			sb.Append(Environment.NewLine);
			// Тело письма
			sb.Append("Test");
			sb.Append(Environment.NewLine);

			string email = sb.ToString();

			// Отправка письма с использованием сокета
			///////////////////////////////////////////

			TcpClient client = null;
            
			try
			{
				// Установка соединения
				client = new TcpClient(smtpServer, smtpPort);
				NetworkStream ns = client.GetStream();
				StreamReader stdIn  = new StreamReader(ns);
				StreamWriter stdOut = new StreamWriter(ns);

				// Ждем ответа сервера
				int responseCode = GetResponse(stdIn);
				if (responseCode != 220)
					throw new Exception("no smtp server at specified address or smtp server not ready");

				// Посылаем команду HELO
				stdOut.WriteLine("HELO " + Dns.GetHostName());
				stdOut.Flush();
				responseCode = GetResponse(stdIn);
				if (responseCode != 250)
					throw new Exception("helo fails. code="+responseCode);

				// Команда MAIL
				stdOut.WriteLine("MAIL FROM:"+from_mail);
				stdOut.Flush();
				responseCode = GetResponse(stdIn);
				if (responseCode != 250)
					throw new Exception("FROM email considered bad by server. code="+responseCode);

				// Команда RCPT 
				stdOut.WriteLine("RCPT TO:"+to_mail);
				stdOut.Flush();
				responseCode = GetResponse(stdIn);
				switch(responseCode)
				{
					case 250:
					case 251:
						break;
					default:
						throw new Exception("TO email considered bad by server. code="+responseCode);
				}

				// Команда DATA 
				stdOut.WriteLine("DATA");
				stdOut.Flush();
				responseCode = GetResponse(stdIn);
				if (responseCode != 354)
					throw new Exception("data command not accepted. code="+responseCode);

				// Отправка
				stdOut.WriteLine(email);
				stdOut.Flush();

				// Отправка одиночной точки означает завершение отправки
				stdOut.WriteLine(".");
				stdOut.Flush();
				responseCode = GetResponse(stdIn);
				if (responseCode != 250)
					throw new Exception("email not accepted. code="+responseCode);

				// Команда QUIT
				stdOut.WriteLine("QUIT");
				stdOut.Flush();
				responseCode = GetResponse(stdIn);
				if (responseCode != 221)
				{
					//who cares
				}

				Console.WriteLine("Все успешно отправлено");

			}
			catch(Exception ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			finally
			{
				// Закрываем соединение
				if (client != null)
					client.Close();
				client = null;
			}

			Console.ReadLine();
		}

		static int GetResponse(StreamReader stdIn)
		{
			try
			{
				string response = string.Empty;
				// Читаем ответ сервера
				do
				{
					response += stdIn.ReadLine()+"\r\n";
				}
				while(stdIn.Peek() != -1);

				// Выводим на консоль
				Console.WriteLine(response);

				// Получаем код ответа (первые три символа)
				return Convert.ToInt32(response.Substring(0, 3));
			}
			catch
			{
				// Если ошибка
				return 0;
			}
		}
	}
}
