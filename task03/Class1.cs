using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace task03
{
    public class CustomCollection<T> : IEnumerable<T>
    {
        private readonly List<T> _items = new List<T>();

        public void Add(T item) => _items.Add(item);
        public bool Remove(T item) => _items.Remove(item);
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static IEnumerable<T> GetReverseEnumerator() => _items.AsEnumerable().Reverse();

        public static IEnumerable<int> GenerateSequence(int start, int count) => Enumerable.Range(0, count).Select(i => start + i);

        public IEnumerable<T> FilterAndSort(Func<T, bool> predicate, Func<T, IComparable> keySelector)
        {
            return from item in _items
                   where predicate(item)
                   orderby keySelector(item)
                   select item;
        }
    }
}
