using System.Collections.Generic;

namespace MonteCarlo.RandomObjects
{
    public class DiceCup<T>
    {
        private readonly List<(int count, IDie<T> die)> _dice = new List<(int count, IDie<T> die)>();
        
        public DiceCup(IEnumerable<IDie<T>> dice)
        {
            foreach (var die in dice) _dice.Add((1, die));
        }

        public DiceCup(params (int count, IDie<T> die)[] dice)
        {
            foreach (var (count, die) in dice) _dice.Add((count, die));
        }

        public List<T> RollAll()
        {
            var results = new List<T>();

            lock (_dice)
            {
                foreach (var (count, die) in _dice)
                    for (var i = 0; i < count; i++)
                        results.Add(die.Roll());
            }

            return results;
        }
    }
}
