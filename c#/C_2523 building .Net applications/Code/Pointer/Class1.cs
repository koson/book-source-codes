using System;

namespace Pointer
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		unsafe static void Main(string[] args)
		{
			int a = 500;
			byte* pb = (byte*)&a;
			for (int i = 0; i < sizeof(double); ++i)
				Console.WriteLine(" {0,2:X}", (uint)(*pb++));
		}	
	}
}
