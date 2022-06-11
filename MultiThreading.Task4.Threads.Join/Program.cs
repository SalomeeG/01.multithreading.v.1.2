/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task4.Threads.Join
{

    class Program
    {
        static object _locker = new object();
        static Semaphore _pool;

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
            //Console.WriteLine("Recursive With Join");
            //RecursiveWithJoin(10);


            _pool = new Semaphore(0, 1);
            _pool.Release(1);

            RecursiveWithThreadPoolSemaphore(10);
            //RecursiveWithJoin(10);
            //RecursiveWithThreadPoolSemaphore(10);
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"VAMUSHAVEBT :{i}");
            //    _pool.WaitOne();
            //    var z = i;
            //    Task.Run(
            //        () =>
            //            {

            //                Console.WriteLine(z);
            //                _pool.Release();
            //            }
            //       );

            //}

            //Console.WriteLine("Recursive With Semaphore");
            //RecursiveWithThreadPoolSemaphore(10);

            Console.ReadLine();
        }

        static void RecursiveWithJoin(int counter)
        {
            if (counter == 0)
                return;

            Thread thread = new Thread(() =>
            {
                counter--;
                RecursiveWithJoin(counter);
            });
            thread.Start();
            thread.Join();
            Console.WriteLine($"counter: {counter}");
        }

        static void RecursiveWithThreadPoolSemaphore(int counter)
        {
            if (counter == 0)
                return;
            _pool.WaitOne();

            ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                int count = (int)state - 1;
                RecursiveWithThreadPoolSemaphore(count);
                _pool.Release();

            }, counter);

            Console.WriteLine($"counter: {counter}");
            

        }
    }
}
