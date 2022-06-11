/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();


            var collection = new List<int>();

            var wpool = new Semaphore(0, 1);
            wpool.Release();

            var rpool = new Semaphore(0, 1);

            var thread1 = new Thread(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    wpool.WaitOne();
                    Console.WriteLine($"added {i}");
                    collection.Add(i);
                    rpool.Release();
                }
            });
             
            var thread2 = new Thread((arg) =>
            { 
                for (int i = 1; i <= 10; i++)
                {
                    rpool.WaitOne();
                    collection.ForEach(e => Console.WriteLine(e));
                    wpool.Release();
                }
            });

            thread1.Start();
            thread2.Start();
            // feel free to add your code

            Console.ReadLine();
        }
    }
}
