using System;

namespace Structs
{
	public struct Coordinates
	{
		public int x;
		public int y;
		public int z;
		public Coordinates(int p1, int p2, int p3)
		{
			x = p1;
			y = p2;
			z = p3;
		}
	}
	public class Output
	{
		public static void Main()
		{
			Coordinates Start = new Coordinates(0, 0, 5);
			Coordinates End = new Coordinates(15, 12, 16);
			Console.WriteLine("Start at: {0},{1},{2}",Start.x,Start.y,Start.z);
			Console.WriteLine("End at: {0},{1},{2}",End.x,End.y,End.z);
		}
	}
}
