using System;

namespace IntProp
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	interface Point
		{
			int x 
			{
				get; 
				set; 
			}

			int y 
			{
				get; 
				set; 
			}

		/// <summary>
		/// Interface property
		/// </summary>
		int IntProperty
		{
			get;
			set;
		}
		}

	class Class1
	{
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
		}
	}
}
