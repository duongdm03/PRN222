using System;
using System.Threading;
using System.Threading.Tasks;


static void PrintNumbers(string message)
{
    for (int i = 1; i <= 5; i++)
    {
        Console.WriteLine($"{message}:{i}");
        Thread.Sleep(1000);
    }
}

Thread.CurrentThread.Name = "Main";
Task task01 = new Task(() => PrintNumbers("Task 01"));
task01.Start();
Task task02 = Task.Run(delegate { PrintNumbers("Task 02"); });
Task task03 = new Task(new Action(() => {
    PrintNumbers("Task 03");
}));
task03.Start();
Console.WriteLine($"Thread '{Thread.CurrentThread.Name}'");
Console.ReadKey();

