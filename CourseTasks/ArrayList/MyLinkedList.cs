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

        private static void ValidateNode(ListNode<T> node, MyLinkedList<T> list)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Аргумент node равен null");
            }
            if (node.List != list)
            {
                throw new InvalidOperationException("Аргумент node не принадлежит текущему списку");
            }
        }

        private static void ValidateNewNode(ListNode<T> newNode)
        {
            if (newNode == null)
            {
                throw new ArgumentNullException("Аргумент newNode равен null");
            }
            if (newNode.list != null)
            {
                throw new InvalidOperationException("Аргумент newNode уже принадлежит другому списку");
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

        private void SelfListRemove(ListNode<T> node)
        {
            if (IsEmpty())
            {
                return;
            }
            ValidateNode(node, this);
            itemsCount--;
            ModificationCode++;
            if (node.PreviousItem == null)
            {
                startNode = node.NextItem;
                startNode.previousItem = null;
            }
            else if (node.NextItem == null)
            {
                endNode = node.PreviousItem;
                endNode.nextItem = null;
            }
            else
            {
                node.PreviousItem.nextItem = node.NextItem;
                node.NextItem.previousItem = node.PreviousItem;
            }
            node.Invalidate();
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
            currentNode.list = this;
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
            SelfListRemove(node);
        }

        public void RemoveFirst()
        {
            SelfListRemove(startNode);
        }

        public void RemoveLast()
        {
            SelfListRemove(endNode);
        }

        public int Count
        {
            get { return itemsCount; }
        }

        bool ICollection<T>.IsReadOnly
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
                temp.Invalidate();
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

        public ListNode<T> AddAfter(ListNode<T> node, T item)
        {
            ListNode<T> result = new ListNode<T>(item);
            SelfListAddAfter(node, result);
            return result;
        }

        public void AddAfter(ListNode<T> node, ListNode<T> newNode)
        {
            SelfListAddAfter(node, newNode);
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        private void AddNodeToEmptyList(ListNode<T> newNode)
        {
            startNode = newNode;
            endNode = startNode;
        }

        public ListNode<T> AddLast(T item)
        {
            ListNode<T> newNode = new ListNode<T>(item);
            SelfListAddAfter(endNode, newNode);
            return endNode;
        }

        public void AddLast(ListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            AddLast(newNode.Value);
        }

        private void SelfListAddAfter(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            newNode.list = this;
            itemsCount++;
            ModificationCode++;
            if (IsEmpty())
            {
                AddNodeToEmptyList(newNode);
                return;
            }
            ValidateNode(node, this);
            if (node.NextItem == null)
            {
                node.nextItem = newNode;
                newNode.previousItem = node;
                endNode = newNode;
            }
            else
            {
                newNode.nextItem = node.NextItem;
                node.NextItem.previousItem = newNode;
                node.nextItem = newNode;
                newNode.previousItem = node;
            }
        }


        private void SelfListAddBefore(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            newNode.list = this;
            itemsCount++;
            ModificationCode++;
            if (IsEmpty())
            {
                AddNodeToEmptyList(newNode);
                return;
            }
            ValidateNode(node, this);
            if (node.PreviousItem == null)
            {
                newNode.nextItem = node;
                node.previousItem = newNode;
                startNode = newNode;
            }
            else
            {
                node.PreviousItem.nextItem = newNode;
                newNode.previousItem = node.PreviousItem;
                newNode.nextItem = node;
                node.previousItem = newNode;
            }
        }

        public ListNode<T> AddBefore(ListNode<T> node, T item)
        {
            ListNode<T> result = new ListNode<T>(item);
            SelfListAddBefore(node, result);
            return result;
        }

        public void AddBefore(ListNode<T> node, ListNode<T> newNode)
        {
            SelfListAddBefore(node, newNode);
        }

        public ListNode<T> AddFirst(T item)
        {
            ListNode<T> result = new ListNode<T>(item);
            SelfListAddBefore(startNode, result);
            return result;
        }

        public void AddFirst(ListNode<T> newNode)
        {
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
