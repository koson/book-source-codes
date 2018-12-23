using System;
using System.IO;
using System.Text;

namespace CRC32
{
	class Class1
	{
		// Вычисление CRC32
		public static uint Calculate(Stream stream) 
		{ 
			const int BUFFERSIZE = 1024; 
			const uint POLYNOMIAL = 0xEDB88320; 
 
			uint result = 0xFFFFFFFF; 
			uint Crc32; 

			byte[] buffer = new byte[BUFFERSIZE]; 
			uint[] Crc32Table = new uint[256]; 

			// Инициалиазация таблицы
			unchecked 
			{ 
				for (int i = 0; i < 256; i++) 
				{ 
					Crc32 = (uint)i; 
 
					for (int j = 8; j > 0; j--) 
					{ 
						if ((Crc32 & 1)==1) 
							Crc32 = (Crc32 >> 1) ^ POLYNOMIAL; 
						else 
							Crc32 >>= 1; 
					} 
 
					Crc32Table[i] = Crc32; 
				} 
 
				// Чтение буфера
				int count = stream.Read(buffer, 0, BUFFERSIZE); 

				// Вычисление CRC
				while (count > 0)  
				{ 
					for (int i = 0; i < count; i++) 
					{ 
						result = ((result) >> 8) ^ Crc32Table[(buffer[i]) ^ ((result) & 0x000000FF)]; 
					} 
 
					count = stream.Read(buffer, 0, BUFFERSIZE); 
				} 
			} 
 
			return ~result; 
		} 


		[STAThread]
		static void Main(string[] args)
		{
			// Читаем файл test1
			FileStream stream1 = File.OpenRead("test1");
			Console.WriteLine(string.Format("{0:X}", Calculate(stream1)));

			// Читаем файл test2
			FileStream stream2 = File.OpenRead("test2");
			Console.WriteLine(string.Format("{0:X}", Calculate(stream2)));

			Console.ReadLine();

		}
	}
}
