using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    class MyArrayList<T> : IList<T>
    {
        private T[] selfArray;
        private int itemsCount;

        public MyArrayList()
        {
            selfArray = new T[10];
            itemsCount = 0;
        }

        private bool IsEnoughCapacity()
        {
            return itemsCount + 1 < selfArray.Length;
        }

        private void ChangeSizeSelfArray(int currentDataLength)
        {
            T[] temp = new T[(int)(currentDataLength * 3.0 / 2 + 1)];
            for (int i = 0; i < itemsCount; i++)
            {
                temp[i] = selfArray[i];
            }
            selfArray = temp;
        }

        private void MoveTailToBegin(int startTailIndex)
        {
            if (startTailIndex < 0 || startTailIndex > itemsCount - 1)
            {
                return;
            }
            for (int i = startTailIndex; i < itemsCount - 1; i++)
            {
                selfArray[i] = selfArray[i + 1];
            }
            itemsCount--;
        }

        private void MoveTailToEnd(int startTailIndex)
        {
            if (!IsEnoughCapacity())
            {
                ChangeSizeSelfArray(selfArray.Length);
            }
            for (int i = itemsCount - 1; i >= startTailIndex; i--)
            {
                selfArray[i + 1] = selfArray[i];
            }
            itemsCount++;
        }

        public void Add(T item)
        {
            if (!IsEnoughCapacity())
            {
                ChangeSizeSelfArray(selfArray.Length);                
            }
            selfArray[itemsCount++] = item;
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentException("Невозможно выполнить поиск по null");
            }
            for (int i = 0; i < itemsCount; i++)
            {
                if (selfArray[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentException("Невозможно выполнить поиск по null");
            }
            if (itemsCount * 2 < selfArray.Length)
            {
                ChangeSizeSelfArray(itemsCount);
            }
            for (int i = 0; i < itemsCount; i++)
            {
                if (selfArray[i].Equals(item))
                {
                    MoveTailToBegin(i);
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            selfArray = new T[10];
            itemsCount = 0;
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentException("Массив не существует");
            }
            if (index < 0 || index > itemsCount - 1)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            int minLength = Math.Min(itemsCount - index, array.Length);
            for (int i = 0; i < minLength; i++)
            {
                array[i] = selfArray[i + index];
            }
        }

        public int Count
        {
            get { return itemsCount; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(T item)
        {
            if (item == null)
            {
                throw new ArgumentException("Невозможно выполнить поиск по null");
            }
            for (int i = 0; i < itemsCount; i++)
            {
                if (selfArray[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > itemsCount - 1)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            MoveTailToEnd(index);
            selfArray[index] = item;
        }
        
        public void RemoveAt(int index)
        {
            if (index < 0 || index > itemsCount - 1)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            MoveTailToBegin(index);
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > itemsCount - 1)
                {
                    throw new ArgumentException("Индекс вне диапазона");
                }
                return selfArray[index];
            }
            set
            {
                if (index < 0 || index > itemsCount - 1)
                {
                    throw new ArgumentException("Индекс вне диапазона");
                }
                selfArray[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < itemsCount; i++)
            {
                yield return selfArray[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            for (int i = 0; i < itemsCount; i++)
            {
                sb.Append(selfArray[i])
                    .Append(", ");
            }
            if (itemsCount > 0)
            {
                sb.Remove(sb.Length - 1 - 1, 1);
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
