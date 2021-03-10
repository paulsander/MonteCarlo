using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonteCarlo.RandomObjects;

namespace MonteCarlo
{
    public class DieSimulator : Simulator<(int gte58, int equals60)>
    {
        protected override (int gte58, int equals60) TestProcess(int numTrials)
        {
            var cup = new DiceCup<int>((12, new FairDie(6, new Random())));

            var gte58 = 0;
            var equals60 = 0;

            for (var i = 0; i < numTrials; i++)
            {
                var total = cup.RollAll().Sum();

                if (total >= 58)
                    gte58++;
                if (total == 60)
                    equals60++;
            }

            return (gte58, equals60);
        }

        protected override (int gte58, int equals60) Combine(IEnumerable<(int gte58, int equals60)> subResults)
        {
            var gte58Cumulative = 0;
            var equals60Cumulative = 0;

            foreach (var (gte58, equals60) in subResults)
            {
                gte58Cumulative += gte58;
                equals60Cumulative += equals60;
            }

            return (gte58Cumulative, equals60Cumulative);
        }

        protected override void Report(int numTrials, (int gte58, int equals60) result)
        {
            var p = result.gte58 == 0 ? 0.0 : result.equals60 / (double)result.gte58;

            Console.WriteLine($"Totals greater than or equal to 58: {result.gte58}");
            Console.WriteLine($"Totals equal to exactly 60: {result.equals60}\n");

            Console.WriteLine($"P( X = 60 | X >= 58) = {p}\n");
        }
    }
}
