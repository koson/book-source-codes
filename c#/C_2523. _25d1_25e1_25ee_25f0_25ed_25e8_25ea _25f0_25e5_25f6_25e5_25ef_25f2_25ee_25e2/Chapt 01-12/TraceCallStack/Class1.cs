using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace TraceCallStack
{
	class Class1
	{
		static void Test1() 
		{
			Test2();	
		}

		static void Test2() 
		{
			Console.WriteLine(TraceCallStack());			
		}

		public static string TraceCallStack()
		{
			StringBuilder sb = new StringBuilder();

			StackTrace st = new StackTrace(1, true);
			StackFrame sf = st.GetFrame(StackTrace.METHODS_TO_SKIP);
			if (sf != null)
			{
				MethodBase mb = sf.GetMethod();
				sb.Append(mb.ReflectedType.Name);
				sb.Append(".");
				sb.Append(mb.Name);
				sb.Append("(");
				bool first = true;
				foreach (ParameterInfo pi in mb.GetParameters())
				{
					if (!first)
						sb.Append(", ");
					first = false;
					sb.Append(pi.ParameterType.Name);
					sb.Append(" ");
					sb.Append(pi.Name);
				}
				sb.Append(");");
				int n = StackTrace.METHODS_TO_SKIP+1;
				sf = st.GetFrame(n);
				if (sf != null)
				{
					sb.Append(" called from ");
					do
					{
						mb = sf.GetMethod();
						sb.Append(mb.ReflectedType.Name);
						sb.Append(".");
						sb.Append(mb.Name);
						sb.Append(":");
						sf = st.GetFrame(++n);
					}
					while (sf != null);
				}
			}
			return sb.ToString();
		}

		[STAThread]
		static void Main(string[] args)
		{
			Test1();
			Console.ReadLine();
		}
	}
}
