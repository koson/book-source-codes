using System;

namespace SimpleDelegateExample
{
    // Declaration
    public delegate void SimpleDelegate();

    class TestDelegate
    {
        public static void MyFunc()
        {
            Console.WriteLine("I was called by delegate ...");
        }
        public static void Main()
        {
            // Instantiation
            SimpleDelegate simpleDelegate = new SimpleDelegate(MyFunc);

            // Invocation
            simpleDelegate();
        }
    }
}
