using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Example
{
	public class ErrorHandlingModule : IHttpModule
	{
		private HttpApplication _application;

		// Инициализация
		void IHttpModule.Init(HttpApplication application) 
		{
			_application = application;
			_application.Error += new EventHandler(ErrorHandler);
		}

		// Обработчик
		private void ErrorHandler(Object sender, EventArgs e) 
		{
			bool Handled = true;

			// Получаем ошибку
			Exception ex = _application.Server.GetLastError();

			// Формируем сообщение
			StringBuilder msg = new StringBuilder();
			if (ex.InnerException != null)
			{
				msg.Append(ex.InnerException.Message);
				if (ex.InnerException.InnerException != null)
				{
					msg.AppendFormat(" {0}", ex.InnerException.InnerException.Message);
					Handled  =  false;
				}
			}

			// Передаем сообщение на страницу отображения ошибки
			if (Handled)
			{
				// Чистим ошибки
				_application.Server.ClearError();
				msg = msg.Replace("\r\n", " ");
				_application.Context.Response.Redirect(string.Format("./ErrorHandling.aspx?msg={0}", msg));
			}
		}

		public void Dispose()
		{
		}
	}
}
