using System;

namespace MonteCarlo.RandomObjects
{
    public class FairDie : RandomObjectBase, IDie<int>
    {
        private readonly Random _random;

        private readonly int _numSides;

        public FairDie(int numSides) : this(numSides, Random) { }

        public FairDie(int numSides, Random random)
        {
            _random = random;
            _numSides = numSides;
        }

        public int Roll() => _random.Next(1, _numSides + 1);
    }
}
