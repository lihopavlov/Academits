using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    class MyArrayList<T> : ICollection<T>
    {
        private T[] selfArray;
        private int counter;
        private int index;

        public MyArrayList(bool isReadOnly)
        {
            selfArray = new T[10];
            counter = 0;
            index = -1;
            IsReadOnly = isReadOnly;
        }

        private bool IsEnoughCapacity()
        {
            return counter + 1 < selfArray.Length;
        }

        private void ChangeSizeSelfArray(int currentDataLength)
        {
            T[] temp = new T[(int)(currentDataLength * 3.0 / 2 + 1)];
            for (int i = 0; i < counter; i++)
            {
                temp[i] = selfArray[i];
            }
            selfArray = temp;
        }

        private void MoveTailToBegin(int startTailIndex)
        {
            if (startTailIndex < 1 || startTailIndex > counter - 1)
            {
                return;
            }
            for (int i = startTailIndex; i < counter - 1; i++)
            {
                selfArray[i] = selfArray[i + 1];
            }
            counter--;
        }

        public void Add(T item)
        {
            if (IsReadOnly)
            {
                return;
            }
            if (!IsEnoughCapacity())
            {
                ChangeSizeSelfArray(selfArray.Length);                
            }
            selfArray[counter++] = item;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < counter; i++)
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
            if (IsReadOnly)
            {
                return false;
            }
            if (counter * 2 < selfArray.Length)
            {
                ChangeSizeSelfArray(counter);
            }
            for (int i = 0; i < counter; i++)
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
            if (IsReadOnly)
            {
                return;
            }
            selfArray = new T[10];
            counter = 0;
        }

        public void CopyTo(T[] array, int index)
        {
            if (index < 0 || index > counter - 1)
            {
                throw new ArgumentException("Индекс вне диапазона");
            }
            int minLength = Math.Min(counter - index, array.Length);
            for (int i = 0; i < minLength; i++)
            {
                array[i] = selfArray[i + index];
            }
        }

        public int Count
        {
            get { return counter; }
        }

        public bool IsReadOnly { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < counter; i++)
            {
                if (selfArray[i] == null)
                {
                    break;
                }
                yield return selfArray[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            if (index == counter - 1)
            {
                Reset();
                return false;
            }
            index++;
            return true;
        }

        public void Reset()
        {
            index = -1;
        }

        public T Current
        {
            get { return selfArray[index]; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            for (int i = 0; i < counter; i++)
            {
                sb.Append(selfArray[i])
                    .Append(", ");
            }
            if (counter > 0)
            {
                sb.Remove(sb.Length - 1 - 1, 1);
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
