﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace csharpsimple
{
    class Program
    {
        static bool stopped = false;

        // using ThreadStatic variable to avoid sharing
        [ThreadStatic]
        static int _field;

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

        public static void ThreadMethod5()
        {
            for (int i = 0; i < 10; i++)
            {
                _field++;
                Console.WriteLine("Thread output value: {0}", _field);
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
            /*
            Thread t = new Thread(new ThreadStart(ThreadMethod4));
            t.Start();
            Console.WriteLine("Enter any key to stop thread");
            Console.ReadKey();

            stopped = true;
            t.Join();
            */

            // Each thread with separated copy of local variale
            /* 
            Thread a = new Thread(new ThreadStart(ThreadMethod5));
            Thread b = new Thread(new ThreadStart(ThreadMethod5));
            a.Start();
            b.Start();
            Console.ReadKey();
            */

            // Using thread pool to execute computation
            /* 
            ThreadPool.QueueUserWorkItem((s) => {
                Console.WriteLine("Working on a thread from thread pool");
            });
            Console.ReadLine();
            */

            // Using task schedule to control task (represent a computation)
            // schedule take availble thread from pool to execute task
            /* 
            Task<int> t = Task.Run(() => {
                int i = 0;
                for (; i < 100; i++)
                {
                    Console.WriteLine("*");
                }

                return i;
            });

            Task<int> k = t.ContinueWith((i) => {
                return (100 * i.Result);
            });

            Console.WriteLine(k.Result);

            k.Wait();
            */

            // Break task into small child task
            /* 
            Task<Int32[]> parent = Task.Run(() => {
                var results = new Int32[3];

                new Task(() => {
                    results[0] = 0;
                }, TaskCreationOptions.AttachedToParent).Start();

                new Task(() => {
                    results[1] = 1;
                }, TaskCreationOptions.AttachedToParent).Start();

                new Task(() => {
                    results[2] = 2;
                }, TaskCreationOptions.AttachedToParent).Start();

                return results;
            });
            */

            // can use with task factory instead
            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];
                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);
                tf.StartNew(() => results[0] = 0);
                tf.StartNew(() => results[1] = 1);
                tf.StartNew(() => results[2] = 2);
                return results;
            });

            var final = parent.ContinueWith(
                parentTask => {
                    foreach(int i in parentTask.Result)
                    {
                        Console.WriteLine(i);
                    }
                }
            );

            final.Wait();
            

            // Using async
            /* 
            string result = DownloadContent().Result;
            Console.WriteLine("From the main thread application");
            Console.WriteLine(result);
            */
        }
    }
}
