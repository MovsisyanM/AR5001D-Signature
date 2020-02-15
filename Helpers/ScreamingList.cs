/*
 Made by Movsisyan.
 Find me on GitHub.
 Contact me at movsisyan@protonmail.com for future endeavors.
 Գտիր ինձ ԳիթՀաբ-ում:
 Գրիր ինձ movsisyan@protonmail.com հասցեյով հետագա առաջարկների համար:
 2019
*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace AR5001D
{
    /// <summary>
    /// This type encapsulates a List and implements notifications.
    /// </summary>
    /// <typeparam name="T">Type of values stored in the list</typeparam>
    public sealed class ScreamerList<T> : ICloneable, IEnumerable<T>, ICollection<T>, IList<T>
    {
        private List<T> items = new List<T>();

        public int Count
        {
            get
            {
                return ((ICollection<T>)items).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<T>)items).IsReadOnly;
            }
        }

        public T this[int index]
        {
            get
            {
                return ((IList<T>)items)[index];
            }

            set
            {
                ((IList<T>)items)[index] = value;
                ItemChanged?.Invoke();
            }
        }

        public delegate void ListChangedHandler();

        public event ListChangedHandler ItemAdded;
        public event ListChangedHandler ItemChanged;
        public event ListChangedHandler ItemRemoved;

        /// <summary>
        /// Duplicates the collection.
        /// </summary>
        /// <returns>A copy of this instance</returns>
        public object Clone()
        {
            var list = new ScreamerList<T>();
            foreach (T item in items)
            {
                list.Add(item);
            }
            return list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)items).GetEnumerator();
        }

        public void Add(T item)
        {
            ((ICollection<T>)items).Add(item);
            ItemAdded?.Invoke();
        }

        public void Clear()
        {
            items.Clear();
            ItemRemoved?.Invoke();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)items).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)items).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            ItemRemoved?.Invoke();
            return ((ICollection<T>)items).Remove(item);
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)items).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)items).Insert(index, item);
            ItemChanged?.Invoke();
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)items).RemoveAt(index);
            ItemRemoved?.Invoke();
        }

        public List<T> ToList()
        {
            List<T> eutput = new List<T>();
            foreach (T item in items)
            {
                eutput.Add(item);
            }
            return eutput;
        }
    }
}