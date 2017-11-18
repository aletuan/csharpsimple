using System;
using System.Threading;

namespace csharpsimple
{
    class Program
    {
        public static void ThreadMethod1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(0);
            }
        }


        public static void TheadMethod2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(1000);
            }
        }

        static void Main(string[] args)
        {
            /* 
            // Sample of starting multi-threading
            Thread t = new Thread(new ThreadStart(ThreadMethod1));
            t.Start();

            for (int i = 0; i < 4; i++) {
                Console.WriteLine("Main Thread: Do some work");
                Thread.Sleep(0);
            }
            

            t.Join();
            */

            Thread t = new Thread(new ThreadStart(TheadMethod2));
            t.IsBackground = false;
            t.Start();
        }
    }
}
