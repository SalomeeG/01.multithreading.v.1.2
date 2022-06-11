/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            var initialTask = new Task(() =>
            {
                throw new Exception();

                tokenSource2.Cancel();
                ct.ThrowIfCancellationRequested();

            }, ct);

            var taskA = initialTask.ContinueWith(x =>
            {
                Console.WriteLine("Task A - Executes always");
            });

            var taskB = initialTask.ContinueWith(x =>
            {
                Console.WriteLine("Task B - Executes on non-completion.");
            }, TaskContinuationOptions.NotOnRanToCompletion);
            
            var taskC = initialTask.ContinueWith(x =>
            {
                Console.WriteLine("Task C - Executes on fault & tries to use parent task's thead.");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            var taskD = initialTask.ContinueWith(x =>
            {
                Console.WriteLine("Task D - Executes when task is canceled & runs outside the threadpool.");
            },
            CancellationToken.None,
            TaskContinuationOptions.None, //OnlyOnCancel
            TaskScheduler.Current);

            initialTask.Start();
            Console.ReadLine();
        }
    }
}
