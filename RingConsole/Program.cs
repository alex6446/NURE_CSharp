using System;
using RingLib;

namespace RingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Ring<int> a = new Ring<int>();
            int n;
            n = a < 3;
            n = a < 4;
            n = a < 5;
            a++;
            a--;
            n = a > n;
            Console.WriteLine(n);

            Ring<int> b = new Ring<int>(a);
            n = a > n;
            n = b > n;
            Console.WriteLine(n);

            Ring<int> c = new Ring<int>(new int[6]{1, 2, 3, 4, 5, 6});
            n = c > n;
            Console.WriteLine(n);

            Ring<int> d = new Ring<int>();
            d.Input();
            Console.WriteLine(d);

            Ring<int> e = new Ring<int>(d);
            Console.WriteLine(e);
            Console.WriteLine(e == d);
            Console.WriteLine(a != e);

            if (e)
                Console.WriteLine("E is full");
            else
                Console.WriteLine("E is empty");

            e[1] = 148;
            for (int i = 0; i < e.Size(); i++)
                Console.Write("{0} ", e[i]);
            Console.WriteLine();

            Console.WriteLine("size of e is {0}", e.Size());

            int[] arr = (int[])e;
            foreach (var i in arr)
                Console.Write("{0} ", i);
            Console.WriteLine(" ");

            int[] arr2 = {13, 12, 16};
            Console.WriteLine((Ring<int>)arr2);

        }
    }
}
