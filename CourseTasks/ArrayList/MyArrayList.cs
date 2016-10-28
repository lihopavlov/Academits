using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    class MyArrayList<T> : IList<T>
    {
        private const double IncrementCoeff = 1.5;
        private const double DecrementCoeff = 2.0;
        private const double CapacityThreshold = 0.9;
        private long modificationCode;

        private T[] selfArray;
        private int itemsCount;

        public MyArrayList()
        {
            selfArray = new T[10];
            itemsCount = 0;
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

        private bool IsEnoughCapacity()
        {
            return itemsCount < selfArray.Length;
        }

        private bool IsEnoughCapacity(int additionRange)
        {
            return itemsCount + additionRange <= selfArray.Length;
        }


        private void ChangeSizeSelfArray(int currentDataLength, double incrementCoeff)
        {
            T[] temp = new T[(int)(currentDataLength * incrementCoeff + 1)];
            for (int i = 0; i < itemsCount; i++)
            {
                temp[i] = selfArray[i];
            }
            selfArray = temp;
        }

        private void MoveTailToBegin(int startTailIndex)
        {
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
                ChangeSizeSelfArray(selfArray.Length, IncrementCoeff);
            }
            for (int i = itemsCount - 1; i >= startTailIndex; i--)
            {
                selfArray[i + 1] = selfArray[i];
            }
            itemsCount++;
        }

        public virtual int Capacity
        {
            get { return selfArray.Length; }
            set
            {
                if (value < itemsCount)
                {
                    throw new ArgumentException("Значение свойтсва Capacity не может быть меньше Count");
                }
                ChangeSizeSelfArray(value - 1, 1.0);
            }
        }

        public virtual void TrimExcess()
        {
            if ((double)itemsCount / Capacity < CapacityThreshold)
            {
                Capacity = itemsCount;
            }
        }

        public virtual void AddRange(ICollection<T> c)
        {
            if (c == null)
            {
                throw new ArgumentException("Коллекция не существует");
            }
            if (!IsEnoughCapacity(c.Count))
            {
                ChangeSizeSelfArray(itemsCount + c.Count, IncrementCoeff);
            }
            int i = itemsCount;
            foreach (T item in c)
            {
                selfArray[i] = item;
                i++;
            }
            itemsCount += c.Count;
            ++ModificationCode;
        }

        public void Add(T item)
        {
            if (!IsEnoughCapacity())
            {
                ChangeSizeSelfArray(selfArray.Length, IncrementCoeff);                
            }
            selfArray[itemsCount++] = item;
            ++ModificationCode;
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
            if (itemsCount * DecrementCoeff < selfArray.Length)
            {
                ChangeSizeSelfArray(itemsCount, IncrementCoeff);
            }
            for (int i = 0; i < itemsCount; i++)
            {
                if (selfArray[i].Equals(item))
                {
                    MoveTailToBegin(i);
                    ++ModificationCode;
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            selfArray = new T[itemsCount];
            itemsCount = 0;
            ++ModificationCode;
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentException("Массив не существует");
            }
            if (index < 0 || index >= itemsCount)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            if (array.Length < itemsCount - index)
            {
                throw new ArgumentException("Недостаточно длины массива");
            }
            for (int i = 0; i < itemsCount - index; i++)
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
            if (index < 0 || index >= itemsCount)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            MoveTailToEnd(index);
            selfArray[index] = item;
            ++ModificationCode;
        }
        
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= itemsCount)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            MoveTailToBegin(index);
            ++ModificationCode;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= itemsCount)
                {
                    throw new ArgumentException("Индекс вне диапазона");
                }
                return selfArray[index];
            }
            set
            {
                if (index < 0 || index >= itemsCount)
                {
                    throw new ArgumentException("Индекс вне диапазона");
                }
                selfArray[index] = value;
                ++ModificationCode;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            long startModificationCode = ModificationCode;
            for (int i = 0; i < itemsCount; i++)
            {
                if (startModificationCode != ModificationCode)
                {
                    throw new InvalidOperationException("Коллекция изменилась");
                }
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
