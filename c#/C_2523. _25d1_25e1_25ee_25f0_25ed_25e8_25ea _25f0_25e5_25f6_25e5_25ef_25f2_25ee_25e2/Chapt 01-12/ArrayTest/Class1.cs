using System;

namespace ArrayTest
{
        class Class1
        {
                [STAThread]
                static void Main(string[] args)
                {
                        int [,] myArray = new int[3,2];
                        myArray[0, 0] = 100;
                        myArray[1, 0] = 200;
                        myArray[1, 1] = 201;
                        myArray[2, 0] = 300;
                        myArray[2, 1] = 301;

                        Console.WriteLine("Rank={0}",myArray.Rank);
                        Console.WriteLine("Length={0}",myArray.Length);
                        Console.WriteLine("GetLength(0)={0}",myArray.GetLength(0));
                        Console.WriteLine("GetLength(1)={0}",myArray.GetLength(1));
/*                      
                        int [][] myArray = new int[3][];
                        myArray[0] = new int[1];
                        myArray[1] = new int[2];
                        myArray[2] = new int[3];

                        myArray[0][0] = 100;

                        myArray[1][0] = 200;
                        myArray[1][1] = 201;

                        myArray[2][0] = 300;
                        myArray[2][1] = 301;
                        myArray[2][2] = 302;

                        Console.WriteLine("Rank={0}",myArray.Rank);
                        Console.WriteLine("Length={0}",myArray.Length);
                        Console.WriteLine("GetLength(0)={0}",myArray[0].GetLength(0));
                        Console.WriteLine("GetLength(1)={0}",myArray[1].GetLength(0));
*/

                        Console.ReadLine();
                }

                public static Array Redim(Array origArray,Int32 desiredSize)
                {
                        //Определяем тип элемента массива.
                        Type t =origArray.GetType().GetElementType();
                        //Создаем новый массив с требуемым числом элементов.
                        //Тип массива должен совпадать с типом исходного массива.
                        Array newArray = Array.CreateInstance(t,desiredSize);
                        //Копируем элементы из исходного массива в новый массив.
                        Array.Copy(origArray,0,
                                newArray,0,Math.Min(origArray.Length,desiredSize));
                        //Возвращаем новый массив,
                        return newArray;
                }

        }
}
