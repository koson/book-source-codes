using System;

namespace Inherit
{
	public class Cat
	{
		private string name;
		private int year;
		public Cat()
		{
			name = "This cat does not exist.";
		}
		public Cat(string name, int year)
		{
			this.name = name;
			this.year = year; 
		}
		public void PrintCat ()
		{
			Console.WriteLine("{0}: Shots in {1}", name, year);
		}
	}
	public class Inherit : Cat
	{
		public static void Main(string[] args)
		{
			Cat One = new Cat("Mewsette", 1998);
			Cat Two = new Cat("Tigger", 2000);
			Console.WriteLine("Cat vaccination information:");
			One.PrintCat();
			Two.PrintCat();
		}
	}
}
