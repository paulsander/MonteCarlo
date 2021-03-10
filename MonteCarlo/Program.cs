using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MonteCarlo.RandomObjects;


namespace MonteCarlo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const int numTrials = 100_000_000;

            Console.WriteLine("Starting Monte Carlo Tests...\n");

            Console.WriteLine("Problem 1:");
            Console.WriteLine("Roll 12 6-sided die. Let X be the sum of those dice.");
            Console.WriteLine("Calculate P(X = 60 | X > 58).\n");

            var dieSim = new DieSimulator();
            await dieSim.RunParallel(numTrials);
        }

    }
}
