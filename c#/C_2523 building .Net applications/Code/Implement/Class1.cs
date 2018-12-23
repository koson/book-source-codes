using System;

namespace Implement
{
	public class Group 
	{
		int[] primes;
		public Group() 
		{
			primes = new int[5] {1,2,3,5,7};
		}
		public Enumerator GetEnumerator() {
			return new Enumerator (this);
		}
		public class Enumerator 
		{
			int nIndex;
			Group collection;
			public Enumerator(Group coll) 
			{
				collection = coll;
				nIndex = -1;
			}
			public bool MoveNext() {
				nIndex++;
				return(nIndex < collection.primes.GetLength(0));
			}
			public int Current {
				get 
				{
					return(collection.primes[nIndex]);
				}
			}
		}
		public class MainClass 
		{
			public static void Main() 
			{
				Group col = new Group();
				Console.WriteLine("Single-digit primes include:");
				foreach (int i in col) 
				{
					Console.WriteLine(i);
				}
			}
		}
	}
}
