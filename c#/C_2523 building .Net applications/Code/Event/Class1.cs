using System;

namespace Event
{
	public delegate void Del();
	public interface Int
	{
		event Del TheEvent;
		void Start();
	}
	public class Class1: Int
	{
		public event Del TheEvent;
		public void Start()
		{
			if (TheEvent != null)
				TheEvent();
		}
	}
	public class Output
	{
		static private void Started()
		{
			Console.WriteLine("The event has started.");
		}
		static public void Main()
		{
			Int x = new Class1();
			x.TheEvent += new Del(Started);
			x.Start();
		}
	}
}
