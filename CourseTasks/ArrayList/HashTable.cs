using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    public class HashTable<T> : ICollection<T>
    {
        private const int InternalArrayCollisionsTreshold = 5;
        private const double InternalArrayIncreaseFactor = 1.35;
        private const int InitSizeInternalArray = 15;
        private long modificationCode;

        private List<T>[] internalArray;

        public HashTable()
        {
            internalArray = new List<T>[InitSizeInternalArray];
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

        private static int GetInternalHashCode(T item, int arrayLength)
        {
            if (item == null)
            {
                return 0;
            }
            return Math.Abs(item.GetHashCode()) % arrayLength;
        }

        private static int InternalArrayAdd(T item, List<T>[] internalArray)
        {
            var internalArrayIndex = GetInternalHashCode(item, internalArray.Length);
            List<T> currentList = internalArray[internalArrayIndex];
            if (currentList == null)
            {
                internalArray[internalArrayIndex] = new List<T> { item };
                return 1;
            }
            currentList.Add(item);
            return currentList.Count;
        }

        private void ResizeInternalArray()
        {
            List<T>[] tempInternalArray = new List<T>[Math.Min((int)(internalArray.Length * InternalArrayIncreaseFactor), int.MaxValue)];
            foreach (var item in this)
            {
                InternalArrayAdd(item, tempInternalArray);
            }
            internalArray = tempInternalArray;
        }

        public void Add(T item)
        {
            ModificationCode++;
            Count++;
            var internalArrayAddResult = InternalArrayAdd(item, internalArray);
            if (internalArrayAddResult > InternalArrayCollisionsTreshold)
            {
                ResizeInternalArray();
            }
        }

        public void Clear()
        {
            internalArray = new List<T>[internalArray.Length];
            Count = 0;
            ModificationCode++;
        }

        public bool Contains(T item)
        {
            var internalArrayIndex = GetInternalHashCode(item, internalArray.Length);
            List<T> currentList = internalArray[internalArrayIndex];
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
            var startIndex = index;
            foreach (T x in this)
            {
                array[startIndex] = x;
                startIndex++;
            }
        }

        public bool Remove(T item)
        {
            var internalArrayIndex = GetInternalHashCode(item, internalArray.Length);
            List<T> currentList = internalArray[internalArrayIndex];
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
            for (var i = 0; i < internalArray.Length; i++)
            {
                if (internalArray[i] == null)
                {
                    continue;
                }
                foreach (var x in internalArray[i])
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
