using System;
using System.Collections.Specialized;

namespace BitVector32Test
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			BitVector32 vector1 = new BitVector32( 0 );

			// ����� ��� ������ ���� �����
			int bit1 = BitVector32.CreateMask();
			int bit2 = BitVector32.CreateMask(bit1);
			int bit3 = BitVector32.CreateMask(bit2);
			int bit4 = BitVector32.CreateMask(bit3);
			int bit5 = BitVector32.CreateMask(bit4);

			// �������� BitVector32{00000000000000000000000000000000}
			Console.WriteLine(vector1.ToString());
			vector1[bit1] = true; // ���������� 1 ���
			// �������� BitVector32{00000000000000000000000000000001}
			Console.WriteLine(vector1.ToString());
			vector1[bit3] = true; // ���������� 3 ���
			// �������� BitVector32{00000000000000000000000000000101}
			Console.WriteLine(vector1.ToString());
			vector1[bit5] = true; // ���������� 5 ���
			// �������� BitVector32{00000000000000000000000000010101}
			Console.WriteLine(vector1.ToString());

			BitVector32 vector2 = new BitVector32( 0 );

			// ������� 4 ������. ������ ����� �������� �������� 6, �.� �������� 4 ����.
			// ������ - �������� 3 (�.�. ��� ����), ����� 1 ���, ����� 4 ����.
			BitVector32.Section sect1 = BitVector32.CreateSection( 6 );
			BitVector32.Section sect2 = BitVector32.CreateSection( 3, sect1 );
			BitVector32.Section sect3 = BitVector32.CreateSection( 1, sect2 );
			BitVector32.Section sect4 = BitVector32.CreateSection(15, sect3 );

			vector2[sect2] = 3;
			// ������� BitVector32{00000000000000000000000000011000}
			Console.WriteLine(vector2.ToString());
			vector2[sect4] = 1;
			// ������� BitVector32{00000000000000000000000001011000}
			Console.WriteLine(vector2.ToString());

			Console.ReadLine();
		}
	}
}
