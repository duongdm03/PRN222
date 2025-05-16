using System;
using System.Threading;
using System.Threading.Tasks;
static double DoComputations(double start)
{
    double sum = 0;
    for (var value = start; value < start + 10; value += .1)
    {
        sum += value;
    }
    return sum;
}

Task<Double>[] taskArray = {Task <double>.Factory.StartNew(() => DoComputations(1.0)),
                            Task <double>.Factory.StartNew(() => DoComputations(100.0)),
                            Task <double>.Factory.StartNew(() => DoComputations(1000.0)),
                            };
var results = new double[taskArray.Length];
double sum = 0;
for (int i = 0; i < taskArray.Length; i++)
{
    results[i] = taskArray[i].Result;
    Console.Write("{0:N1} {1}", results[1], i == taskArray.Length - 1 ? "=" : "+ ");
    sum += results[i];
}
Console.WriteLine("{0:N1}", sum);