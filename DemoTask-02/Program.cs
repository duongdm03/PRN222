
Task[] task = new Task[5];
String taskData = "Hello";
for (int i = 0; i < 5; i++)
{
    task[i] = Task.Run(() =>
    {
        Console.WriteLine($"Task={Task.CurrentId},obj={taskData}," + $"Thread={Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(1000);

    });
}
try
{
    Task.WaitAll(task);
}
catch (AggregateException ae)
{
    Console.WriteLine("One or more exceptions occurred: ");
    foreach (var ex in ae.Flatten().InnerExceptions)
        Console.WriteLine("    {0}", ex.Message);
}
Console.WriteLine("Status of completed task:");
foreach (var t in task)
{
    Console.WriteLine("    Task  #{0}: {1}", t.Id, t.Status);
}

