using System;

namespace Delegate
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	delegate void Del(int x);

	class class1
	{
		public static void Main()
		{
			SubmitDel(new Del(Function));
		}

		public static void SubmitDel(Del DelFunction)
		{
			DelFunction(500);
		}
   
		public static void Function(int x)
		{
			Console.WriteLine("Now calling delegate {0}.", x);
		}
	}
}
