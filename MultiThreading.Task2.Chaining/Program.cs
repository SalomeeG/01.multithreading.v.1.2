/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code

            Task.Run(() =>
            {
                int[] arrayOfRandom = new int[10];
                for (int i = 0; i < arrayOfRandom.Length; i++)
                {
                    arrayOfRandom[i] = new Random().Next(10);
                }

                PrintResult("#1", arrayOfRandom);

                return arrayOfRandom;

            }).ContinueWith(task1 =>
            {
                for (int i = 0; i < task1.Result.Length; i++)
                {
                    task1.Result[i] *= new Random().Next(10);
                }

                PrintResult("#2", task1.Result);

                return task1.Result;

            }).ContinueWith(task2 =>
            {
                Array.Sort(task2.Result);

                PrintResult("#3", task2.Result);

                return task2.Result;

            }).ContinueWith(task3 =>
            {
                Console.WriteLine($"Task #4\nAverage: {task3.Result.Average(x => x)}");
            });

            Console.ReadLine();
        }

        static void PrintResult(string task, int[] array)
        {
            Console.WriteLine($"Task {task}");

            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
    }
}
