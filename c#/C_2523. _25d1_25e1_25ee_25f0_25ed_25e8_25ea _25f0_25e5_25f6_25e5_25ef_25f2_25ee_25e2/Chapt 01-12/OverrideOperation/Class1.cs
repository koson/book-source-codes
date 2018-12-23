using System;

namespace OverrideOperation
{

	class Fraction
	{
		private int numerator;
		private int denominator;

		// Конструктор
		public Fraction(int numerator, int denominator)
		{
			this.numerator  = numerator;
			this.denominator= denominator;
		}

		// Сумма
		public static Fraction operator+(Fraction lhs, Fraction rhs)
		{
			// Если делитель одинаковый - можно просто сложить
			if (lhs.denominator == rhs.denominator)
			{
				return new Fraction(
					lhs.numerator+rhs.numerator,
					lhs.denominator
					);
			}

			// Вычисляем по правилам школьной арифметики
			// 1/2 + 3/4 == (1*4) + (3*2) / (2*4) == 10/8
			int firstProduct  = lhs.numerator * rhs.denominator;
			int secondProduct = rhs.numerator * lhs.denominator;
			return new Fraction(
				firstProduct    + secondProduct,
				lhs.denominator * rhs.denominator
				);
		}

		// Сравнение на равенство
		public static bool operator==(Fraction lhs, Fraction rhs)
		{
			// Если надо проверить на null, то нельзя использовать
			// запись (lsh == null), т.к. это вызовет рекурсию.
			// Для проверки на null следует использовать метод Equals

			return 
				(lhs.denominator == rhs.denominator) &&
				(lhs.numerator   == rhs.numerator  );
		}
		// Не равно реализуем через равно
		public static bool operator !=(Fraction lhs, Fraction rhs)
		{
			return !(lhs==rhs);
		}

		// Метод сравнения. Проверяет еще и тип аргумента.
		public override bool Equals(object o)
		{
			if (! (o is Fraction) )
			{
				return false;
			}
			return this == (Fraction) o;
		}

		// Метод преобразования в строку
		public override string ToString()
		{
			return string.Format("{0}/{1}", numerator, denominator);
		}

		// Перекрытие == требует реализации GetHashCode
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public class OperatorOverrideClass
	{
		static void Main()
		{
			Fraction f1 = new Fraction(3, 4);
			Console.WriteLine("f1: {0}", f1.ToString());

			Fraction f2 = new Fraction(2, 4);
			Console.WriteLine("f2: {0}", f2.ToString());

			Fraction f3 = f1 + f2;
			Console.WriteLine("f1 + f2 = f3: {0}", f3.ToString());

			Fraction f4 = new Fraction(5, 4);
			if (f4 == f3)
			{
				Console.WriteLine("f4: {0} == F3: {1}",
					f4.ToString(), f3.ToString());
			}

			if (f4 != f2)
			{
				Console.WriteLine("f4: {0} != F2: {1}",
					f4.ToString(), f2.ToString());
			}


			if (f4.Equals(f3))
			{
				Console.WriteLine("{0}.Equals({1})",
					f4.ToString(), f3.ToString());
			}

			Console.ReadLine();
		}
	}  
} 
 
