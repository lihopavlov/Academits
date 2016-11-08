using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    class HashTable<T> : ICollection<T>
    {
        private const int SelfArrayCollisionsTreshold = 5;
        private const double SelfArrayIncreaseFactor = 1.35;
        private const int InitSizeSelfArray = 15;
        private long modificationCode;

        private List<T>[] selfArray;

        public HashTable()
        {
            selfArray = new List<T>[InitSizeSelfArray];
            Count = 0;
            modificationCode = 0L;
        }

        private long ModificationCode
        {
            get { return modificationCode; }
            set
            {
                if (value + modificationCode >= long.MaxValue)
                {
                    modificationCode = 0L;
                }
                else
                {
                    modificationCode = value;
                }
            }
        }

        private static int GetSelfHashCode(T item)
        {
            if (item == null)
            {
                return 0;
            }
            return Math.Abs(item.GetHashCode());
        }

        private static int SelfArrayAdd(T item, List<T>[] selfArray)
        {
            int hashCode = GetSelfHashCode(item);
            int selfArrayIndex = hashCode % selfArray.Length;
            List<T> currentList = selfArray[selfArrayIndex];
            if (currentList == null)
            {
                selfArray[selfArrayIndex] = new List<T> { item };
                return 1;
            }
            foreach (T x in currentList)
            {
                if (x != null)
                {
                    if (item != null && item.GetHashCode() == x.GetHashCode() && x.Equals(item))
                    {
                        return 0;
                    }
                }
                else if (item == null)
                {
                    return 0;
                }
            }
            currentList.Add(item);
            return currentList.Count;
        }

        private void ResizeSelfArray()
        {
            List<T>[] tempSelfArray = new List<T>[Math.Min((int)(selfArray.Length * SelfArrayIncreaseFactor), int.MaxValue)];
            foreach (var item in this)
            {
                SelfArrayAdd(item, tempSelfArray);
            }
            selfArray = tempSelfArray;
        }

        public void Add(T item)
        {
            int selfArrayAddResult = SelfArrayAdd(item, selfArray);
            if (selfArrayAddResult > 0)
            {
                ModificationCode++;
                Count++;
                if (selfArrayAddResult > SelfArrayCollisionsTreshold)
                {
                    ResizeSelfArray();
                }
            }
        }

        public void Clear()
        {
            selfArray = new List<T>[selfArray.Length];
            Count = 0;
            ModificationCode++;
        }

        public bool Contains(T item)
        {
            int selfArrayIndex = GetSelfHashCode(item) % selfArray.Length;
            List<T> currentList = selfArray[selfArrayIndex];
            foreach (var x in currentList)
            {
                if (x != null)
                {
                    if (item != null && item.GetHashCode() == x.GetHashCode() && x.Equals(item))
                    {
                        return true;
                    }
                }
                else if (item == null)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentException("Массив не существует");
            }
            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            if (array.Length - index < Count)
            {
                throw new ArgumentException("Недостаточно длины массива");
            }
            int startIndex = index;
            foreach (T x in this)
            {
                array[startIndex] = x;
                startIndex++;
            }
        }

        public bool Remove(T item)
        {
            int selfArrayIndex = GetSelfHashCode(item) % selfArray.Length;
            List<T> currentList = selfArray[selfArrayIndex];
            foreach (var x in currentList)
            {
                if (x != null)
                {
                    if (item != null && item.GetHashCode() == x.GetHashCode() && x.Equals(item))
                    {
                        currentList.Remove(x);
                        Count--;
                        ModificationCode++;
                        return true;
                    }
                }
                else if (item == null)
                {
                    currentList.Remove(x);
                    Count--;
                    ModificationCode++;
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            long startModificationCode = ModificationCode;
            for (int i = 0; i < selfArray.Length; i++)
            {
                if (selfArray[i] == null)
                {
                    continue;
                }
                foreach (var x in selfArray[i])
                {
                    if (startModificationCode != ModificationCode)
                    {
                        throw new InvalidOperationException("Коллекция изменилась");
                    }
                    yield return x;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; private set; }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            foreach (var item in this)
            {
                sb.Append(item)
                    .Append(", ");
            }
            if (Count > 0)
            {
                sb.Remove(sb.Length - 1 - 1, 1);
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
