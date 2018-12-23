using System;

namespace JScriptEvaluateTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			object result = Microsoft.JScript.Eval.JScriptEvaluate("-1", Microsoft.JScript.Vsa.VsaEngine.CreateEngine());
			Console.WriteLine(result);

			Console.ReadLine();

		}
	}
}
