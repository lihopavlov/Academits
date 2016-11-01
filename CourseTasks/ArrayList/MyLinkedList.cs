using System;
using System.CodeDom;
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

        private void ValidateNode(ListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Аргумент node равен null");
            }
            if (node.List != this)
            {
                throw new InvalidOperationException("Аргумент node не принадлежит текущему списку");
            }
        }

        public ListNode<T> First
        {
            get { return startNode; }
        }

        public ListNode<T> Last
        {
            get { return endNode; }
        }

        public void Add(T item)
        {
            if (IsEmpty())
            {
                startNode = new ListNode<T>(item, this);
                endNode = startNode;
            }
            else
            {
                ListNode<T> newNode = new ListNode<T>(item, this);
                newNode.PreviousItem = endNode;
                endNode = newNode;
                endNode.PreviousItem.NextItem = endNode;
            }
            itemsCount++;
            ModificationCode++;
        }

        private void ClearNodeLinks(ListNode<T> node)
        {
            if (node != null)
            {
                node.NextItem = null;
                node.PreviousItem = null;
                node.List = null;
            }
        }

        private void SelfListRemove(ListNode<T> node)
        {
            ValidateNode(node);
            itemsCount--;
            ModificationCode++;
            if (node.PreviousItem == null)
            {
                startNode = node.NextItem;
                startNode.PreviousItem = null;
            }
            else if (node.NextItem == null)
            {
                endNode = node.PreviousItem;
                endNode.NextItem = null;
            }
            else
            {
                node.PreviousItem.NextItem = node.NextItem;
                node.NextItem.PreviousItem = node.PreviousItem;
            }
            ClearNodeLinks(node);
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
                SelfListRemove(currentNode);
                return true;
            }
            return false;
        }

        public void Remove(ListNode<T> node)
        {
            ValidateNode(node);
            SelfListRemove(node);
        }

        public void RemoveFirst()
        {
            ValidateNode(startNode);
            SelfListRemove(startNode);
        }

        public void RemoveLast()
        {
            ValidateNode(endNode);
            SelfListRemove(endNode);
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
                ListNode<T> temp = currentNode;
                currentNode = currentNode.NextItem;
                ClearNodeLinks(temp);
            }
            startNode = null;
            endNode = null;
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

        private void SelfListAddAfter(ListNode<T> node, ListNode<T> newNode)
        {
            if (node.NextItem == null)
            {
                node.NextItem = newNode;
                newNode.PreviousItem = node;
                endNode = newNode;
            }
            else
            {
                newNode.NextItem = node.NextItem;
                node.NextItem.PreviousItem = newNode;
                node.NextItem = newNode;
                newNode.PreviousItem = node;
            }
            itemsCount++;
            ModificationCode++;
        }

        public ListNode<T> AddAfter(ListNode<T> node, T item)
        {
            ValidateNode(node);
            ListNode<T> result = new ListNode<T>(item, this);
            SelfListAddAfter(node, result);
            return result;
        }

        public void AddAfter(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            SelfListAddAfter(node, newNode);
        }

        public ListNode<T> AddLast(T item)
        {
            ListNode<T> result = new ListNode<T>(item, this);
            if (IsEmpty())
            {
                startNode = result;
                endNode = startNode;
                itemsCount++;
                ModificationCode++;
                return startNode;
            }
            SelfListAddAfter(endNode, result);
            return endNode;
        }

        public ListNode<T> AddLast(ListNode<T> newNode)
        {
            if (IsEmpty())
            {
                startNode = newNode;
                endNode = startNode;
                itemsCount++;
                ModificationCode++;
                return startNode;
            }
            SelfListAddAfter(endNode, newNode);
            return endNode;
        }

        private void SelfListAddBefore(ListNode<T> node, ListNode<T> newNode)
        {
            if (node.PreviousItem == null)
            {
                newNode.NextItem = node;
                node.PreviousItem = newNode;
                startNode = newNode;
            }
            else
            {
                node.PreviousItem.NextItem = newNode;
                newNode.PreviousItem = node.PreviousItem;
                newNode.NextItem = node;
                node.PreviousItem = newNode;
            }
            itemsCount++;
            ModificationCode++;
        }

        public ListNode<T> AddBefore(ListNode<T> node, T item)
        {
            ValidateNode(node);
            ListNode<T> result = new ListNode<T>(item, this);
            SelfListAddBefore(node, result);
            return result;
        }

        public void AddBefore(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            SelfListAddBefore(node, newNode);
        }

        public ListNode<T> AddFirst(T item)
        {
            ListNode<T> result = new ListNode<T>(item, this);
            if (IsEmpty())
            {
                startNode = result;
                endNode = startNode;
                itemsCount++;
                ModificationCode++;
                return startNode;
            }
            SelfListAddBefore(startNode, result);
            return startNode;
        }

        public void AddFirst(ListNode<T> newNode)
        {
            ValidateNode(newNode);
            if (IsEmpty())
            {
                startNode = newNode;
                endNode = startNode;
                itemsCount++;
                ModificationCode++;
                return;
            }
            SelfListAddBefore(startNode, newNode);
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
