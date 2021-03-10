using System;
using System.Collections.Generic;

namespace MonteCarlo.RandomObjects
{
    public class Bag<T> : RandomObjectBase
    {
        private readonly Random _random;
        private readonly List<T> _items = new List<T>();

        private int _index = 0;

        public Bag(IEnumerable<T> items) : this(items, Random) { }

        public Bag(IEnumerable<T> items, Random random)
        {
            _random = random;
            foreach (var item in items)
                _items.Add(item);

            Shuffle();
        }

        public Bag(params (T item, int count)[] items) : this(Random, items) { }

        public Bag(Random random, params (T item, int count)[] items)
        {
            _random = random;
            foreach (var (item, count) in items)
                for (var i = 0; i < count; i++)
                    _items.Add(item);

            Shuffle();
        }

        public bool IsEmpty => _index == _items.Count;

        public int Remaining => _items.Count - _index;

        public T Draw()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Bag is empty!");

            var res = _items[_index];
            _index++;
            return res;
        }

        public IEnumerable<T> Draw(int num)
        {
            if (IsEmpty)
                throw new InvalidOperationException("Bag is empty!");
            if (num <= 0)
                throw new InvalidOperationException(
                    $"You must draw a positive number of items! (Attempted to draw {num}.)");
            if (num > Remaining)
                throw new InvalidOperationException($"You tried to draw {num} items but only {Remaining} were left!");

            var res = new List<T>();

            for (var i = 0; i < num; i++)
            {
                res.Add(Draw());
            }

            return res;
        }

        public IEnumerable<T> DrawAll()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Bag is empty!");

            return Draw(Remaining);
        }

        public void Reset()
        {
            _index = 0;
            Shuffle();
        }

        public void Print()
        {
            Console.WriteLine(string.Join(',', _items));
        }

        private void Shuffle()
        {
            for (var i = _items.Count - 1; i > 0; i--)
            {
                var j = _random.Next(0, i + 1);
                (_items[i], _items[j]) = (_items[j], _items[i]);
            }
        }
    }
}
