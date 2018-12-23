using System;
using System.Reflection;
using Microsoft.Office.Interop.Outlook;

namespace OutlookCalendar
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			ApplicationClass application = new ApplicationClass();
			
			NameSpace nspace = application.GetNamespace("MAPI");

			nspace.Logon(Missing.Value, Missing.Value, false, false);    
            

			object objappointment = application.CreateItem(OlItemType.olAppointmentItem);

			if (objappointment != null)
			{
				AppointmentItem appointment = (AppointmentItem) objappointment;

				appointment.Importance = OlImportance.olImportanceHigh;
				appointment.Subject = "watch tv";
				appointment.Body = "Не забудь посмотреть телевизор!";
				appointment.Start = DateTime.Parse("1-1-2006 11:00 am");
				appointment.End = DateTime.Parse("1-1-2006 1:00 pm");
				appointment.Location = "Диван";
				appointment.ReminderSet = true;
				appointment.ReminderMinutesBeforeStart = 60;
				appointment.AllDayEvent = false;
				appointment.Save();

				appointment = null;
			}

			nspace.Logoff();
			nspace = null;

			application.Quit();
			application = null;

			Console.ReadLine();
		}
	}
}
