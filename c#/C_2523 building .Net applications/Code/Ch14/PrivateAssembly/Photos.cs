namespace PhotoAlbum
{
	using System;
	using System.Collections;

	public class Photos : IEnumerable
	{
		private ArrayList phtList;
	
		public Photos()
		{
			phtList = new ArrayList();
		}
	
		public void Add(Photo pht)
		{ 
			phtList.Add(pht);
		}
	
		public void Remove(int phtRemove) 
		{ 
			phtList.RemoveAt(phtRemove);
		}
	
		public int Count
		{ 
			get{ return phtList.Count;} 
		}

		public IEnumerator GetEnumerator()
		{ 
			return phtList.GetEnumerator();
		}
	}
}

