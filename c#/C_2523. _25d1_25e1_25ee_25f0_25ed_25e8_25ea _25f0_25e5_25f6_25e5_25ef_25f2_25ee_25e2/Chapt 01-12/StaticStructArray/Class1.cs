using System;

namespace StaticStructArray
{
        public struct MyStruct {
                public MyStruct(int x, int y) {
                        a = x;
                        b = y;
                }
                int a;
                public int A { get {return a;}}
                int b;
                public int B { get {return B;}}
        };
    

        class Class1
        {
                static MyStruct [] s = {new MyStruct(5,5), new MyStruct(10,10)};

                [STAThread]
                static void Main(string[] args)
                {
                        Console.WriteLine(s[0].A);

                        Console.ReadLine();
                }
        }
}
