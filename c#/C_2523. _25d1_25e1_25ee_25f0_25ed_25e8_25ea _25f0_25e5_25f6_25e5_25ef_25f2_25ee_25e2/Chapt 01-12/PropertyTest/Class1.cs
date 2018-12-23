using System;

namespace PropertyTest
{
	class Test
	{
		public int Var1;

		private int _Var2;
		public int Var2
		{
			get {return(_Var2);}
			set {_Var2 = value;}
		}
	}

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
		}
	}
}
