using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    static void Main()
    {
        var tasks = new List<Task<int>>();
        var source = new CancellationTokenSource();
        var token = source.Token;
        int completedIterations = 0;

        for (int n = 1; n <= 20; n++)
        {
            tasks.Add(Task.Run(() =>
            {
                int iterations = 0;
                try
                {
                    for (int ctr = 1; ctr <= 2_000_000; ctr++)
                    {
                        token.ThrowIfCancellationRequested();
                        iterations++;
                    }

                    // Chỉ tăng completed nếu hoàn thành hết vòng lặp
                    Interlocked.Increment(ref completedIterations);
                    if (completedIterations >= 10)
                        source.Cancel();
                }
                catch (OperationCanceledException)
                {
                    // Task bị hủy, không làm gì thêm
                }

                return iterations;
            }, token));
        }

        Console.WriteLine("Waiting for the first 10 tasks to complete...\n");

        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException ae)
        {
            Console.WriteLine("One or more tasks were canceled or faulted.\n");
        }

        Console.WriteLine("Status of tasks:\n");
        Console.WriteLine("{0,10} {1,20} {2,14}\n", "Task Id", "Status", "Iterations");

        foreach (var t in tasks)
        {
            string result = "n/a";

            if (t.Status == TaskStatus.RanToCompletion)
                result = t.Result.ToString("N0");

            Console.WriteLine("{0,10} {1,20} {2,14}", t.Id, t.Status, result);
        }

        Console.ReadLine();
    }
}
