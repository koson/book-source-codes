using System;
using System.Collections;

namespace Comparer
{
	class Class1
	{

		class Person
		{
			private string name;
			public string Name
			{
				get
				{
					return Name;
				}
			}

			// Конструктор
			public Person(string name)
			{
				this.name  = name;
			}
		}

		public class PersonComparer : IComparer
		{
			int IComparer.Compare(Object x, Object y)
			{
				string nx = (x as Person).Name;
				string ny = (y as Person).Name;
				return string.Compare(nx, ny);
			}
		}


		[STAThread]
		static void Main(string[] args)
		{
			ArrayList a = new ArrayList();
			a.Add(new Person("AAA"));
			a.Add(new Person("CCC"));
			a.Add(new Person("BBB"));

			PersonComparer comp = new PersonComparer();
			a.Sort(comp);  
            
			foreach (Person p in a)
			{
				Console.WriteLine(p.Name);
			}
			Console.ReadLine();
		}
	}
}
