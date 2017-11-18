using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace csharpsimple
{
    class Program
    {
        static bool stopped = false;

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

        public static void ThreadMethod3(object o)
        {
            for(int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(0);
            }
        }

        public static void ThreadMethod4() 
        {
            while(!stopped) 
            {
                Console.WriteLine("Running in thread...");
                Thread.Sleep(1000);
            }
        }

        public static async Task<string> DownloadContent() 
        {
            using(HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync("http://www.microsoft.com");
                Console.WriteLine("From the asynchronous code");
                return result;
            }
        }

        static void Main(string[] args)
        {
            // Sample of starting multi-threading
            /* 
            Thread t = new Thread(new ThreadStart(ThreadMethod1));
            t.Start();

            for (int i = 0; i < 4; i++) {
                Console.WriteLine("Main Thread: Do some work");
                Thread.Sleep(0);
            }
            

            t.Join();
            */

            // Setting background or foreground thread
            /* 
            Thread t = new Thread(new ThreadStart(TheadMethod2));
            t.IsBackground = false;
            t.Start();
            */

            // Sending data to thread
            /* 
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod3));
            t.Start(10);
            t.Join();
            */

            // Stop a thread using shared variable
            Thread t = new Thread(new ThreadStart(ThreadMethod4));
            t.Start();
            Console.WriteLine("Enter any key to stop thread");
            Console.ReadKey();

            stopped = true;
            t.Join();

            // Using async
            /* 
            string result = DownloadContent().Result;
            Console.WriteLine("From the main thread application");
            Console.WriteLine(result);
            */
        }
    }
}
