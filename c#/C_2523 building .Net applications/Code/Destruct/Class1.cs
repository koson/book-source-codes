using System;

namespace Destruct
{
	class Target
	{
		public int x;
		public Target()
		{
			x = 5;
		}
		~Target()
		{
		}
		public static void Main()
		{
			Console.WriteLine("Target is destroyed.");
		}
	}

}
