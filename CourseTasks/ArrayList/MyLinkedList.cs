using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    class MyLinkedList<T> : ICollection<T>
    {
        private ListNode<T> startNode;
        private ListNode<T> endNode;
        private int itemsCount;
        private long modificationCode;

        public MyLinkedList()
        {
            startNode = null;
            endNode = null;
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

        private bool IsEmpty()
        {
            return startNode == null;
        }

        public void Add(T item)
        {
            if (IsEmpty())
            {
                startNode = new ListNode<T>(item);
                endNode = startNode;
            }
            else
            {
                ListNode<T> newNode = new ListNode<T>(item);
                newNode.PreviousItem = endNode;
                endNode = newNode;
                endNode.PreviousItem.NextItem = endNode;
            }
            itemsCount++;
            ModificationCode++;
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentException("Невозможно выполнить поиск по null");
            }
            if (IsEmpty())
            {
                return false;
            }
            ListNode<T> currentNode = startNode;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    break;
                }
                currentNode = currentNode.NextItem;
            }
            if (currentNode != null)
            {
                itemsCount--;
                ModificationCode++;
                if (currentNode.PreviousItem == null)
                {
                    startNode = currentNode.NextItem;
                    startNode.PreviousItem = null;
                    return true;
                }
                if (currentNode.NextItem == null)
                {
                    endNode = currentNode.PreviousItem;
                    endNode.NextItem = null;
                    return true;
                }
                currentNode.PreviousItem.NextItem = currentNode.NextItem;
                currentNode.NextItem.PreviousItem = currentNode.PreviousItem;
                return true;
            }
            return false;
        }

        public int Count
        {
            get { return itemsCount; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Clear()
        {
            if (IsEmpty())
            {
                return;
            }
            ListNode<T> currentNode = startNode;
            while (currentNode != null)
            {
                currentNode.PreviousItem = null;
                if (currentNode.NextItem != null)
                {
                    currentNode.NextItem.PreviousItem = null;
                }
                currentNode = currentNode.NextItem;
            }
            startNode = null;
            itemsCount = 0;
            ModificationCode++;
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentException("Невозможно выполнить поиск по null");
            }
            if (IsEmpty())
            {
                return false;
            }
            ListNode<T> currentNode = startNode;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    return true;
                }
                currentNode = currentNode.NextItem;
            }
            return false;
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
            if (IsEmpty())
            {
                return;
            }
            ListNode<T> currentNode = startNode;
            int startIndex = 0;
            while (currentNode != null)
            {
                if (startIndex >= index)
                {
                    array[startIndex - index] = currentNode.Value;
                }
                currentNode = currentNode.NextItem;
                startIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            long startModificationCode = ModificationCode;
            ListNode<T> currentNode = startNode;
            while (currentNode != null)
            {
                if (startModificationCode != ModificationCode)
                {
                    throw new InvalidOperationException("Коллекция изменилась");
                }
                yield return currentNode.Value;
                currentNode = currentNode.NextItem;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ListNode<T> currentNode = startNode;
            sb.Append("{ ");
            while (currentNode != null)
            {
                sb.Append(currentNode.Value)
                    .Append(", ");
                currentNode = currentNode.NextItem;
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
