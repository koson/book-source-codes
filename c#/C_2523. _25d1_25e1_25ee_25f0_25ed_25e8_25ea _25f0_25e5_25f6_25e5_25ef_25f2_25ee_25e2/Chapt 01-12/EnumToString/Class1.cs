using System;

namespace Enum_example
{
	class Class1
	{
		enum Button
		{
			Start,
			Stop ,
			Play ,
			Next ,
			Prev
		}


		[STAThread]
		static void Main(string[] args)
		{
			Button b = Button.Stop;
			Console.WriteLine(b.ToString()); // отобразит "Stop"
			Console.WriteLine(b.ToString("G")); // отобразит "Stop"
			Console.WriteLine(b.ToString("D")); // отобразит "1"

			Console.WriteLine(Button.Next.ToString()); // отобразит "Next"

			// отобразит "Prev"
			Console.WriteLine(Enum.GetName(typeof(Button), 4).ToString()); 
			// отобразит "Stop"
			Console.WriteLine(Enum.GetName(typeof(Button), b).ToString()); 

			// отобразит "Prev"
            Button c =(Button) Enum.Parse(typeof(Button),"Prev");
			Console.WriteLine(c.ToString()); 

			Console.WriteLine("================"); 
			Button [] Buttons = (Button[]) Enum.GetValues(typeof(Button));
			foreach (Button btn in Buttons)
			{
				Console.WriteLine(btn.ToString("D") + " = " + btn.ToString("G")); 
			}
			Console.WriteLine("================"); 

			if (Enum.IsDefined(typeof(Button), "Next"))
				Console.WriteLine("YES");

			if (Enum.IsDefined(typeof(Button), 55))
				Console.WriteLine("YES");

			Console.ReadLine();
		}
	}
}
