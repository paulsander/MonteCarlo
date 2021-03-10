using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public abstract class Simulator<T>
    {
        protected abstract T TestProcess(int numTrials);

        protected abstract T Combine(IEnumerable<T> subResults);

        protected abstract void Report(int numTrials, T result);

        public void RunSerial(int numTrials)
        {
            Console.WriteLine("Test starting...");

            var watch = Stopwatch.StartNew();
            var result = TestProcess(numTrials);
            watch.Stop();
            RunReport(numTrials, result, watch.ElapsedMilliseconds);
        }
        
        public async Task RunParallel(int numTrials)
        {
            var logicalProcessors = Environment.ProcessorCount;
            var splitCount = numTrials / logicalProcessors;
            var tasks = new List<Task<T>>();
            var remaining = numTrials;

            Console.WriteLine("Test starting...");
            
            var watch = Stopwatch.StartNew();
            for (var i = 0; i < logicalProcessors - 1; i++)
            {
                tasks.Add(Task.Run(() => TestProcess(splitCount)));
                remaining -= splitCount;
            }

            tasks.Add(Task.Run(() => TestProcess(remaining)));

            var subResults = await Task.WhenAll(tasks);
            var result = Combine(subResults);
            watch.Stop();
            
            RunReport(numTrials, result, watch.ElapsedMilliseconds);
        }

        private void RunReport(int numTrials, T result, long elapsedTime)
        {
            Console.WriteLine($"Test finished - {numTrials:n0} trials in {elapsedTime:n0} ms");
            
            Report(numTrials, result);
        }
    }
}
