using System;
using System.Threading; 

namespace task14
{
    

class DefiniteIntegral
{
  public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
    {
        double result = 0;
        object block = new object();
        Thread[] threads = new Thread[threadsNumber];

        double segment = (b - a) / threadsNumber; 

        for (int i = 0; i < threadsNumber; i++)
        {
            double start = a + i * segment;
            double end = (i == threadsNumber - 1) ? b : start + segment;

            threads[i] = new Thread(() => 
            {
                double part = CalcIntegrel(start, end, function, step);
                
                lock (block)
                {
                    result += part;
                }
            });
            
            threads[i].Start();
        }
        foreach (var thread in threads)
        {
            thread.Join();
        }

        return result;
    
    static double CalcIntegrel(double a, double b, Func<double, double> function, double step)
    {
        double sum = 0;
        double x = a;
        
        while (x < b)
        {
            double y = Math.Min(x + step, b);
            sum += (function(x) + function(y)) * (y - x) / 2;
            x = y;
        }
        
        return sum;
    }
}
}
}
