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
        private long modificationCode;

        public MyLinkedList()
        {
            First = null;
            Last = null;
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

        private bool IsEmpty()
        {
            return First == null;
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

        private static void ValidateNewNode(ListNode<T> newNode)
        {
            if (newNode == null)
            {
                throw new ArgumentNullException("Аргумент newNode равен null");
            }
            if (newNode.List != null)
            {
                throw new InvalidOperationException("Аргумент newNode принадлежит некоторому списку");
            }
        }

        public ListNode<T> First { get; private set; }

        public ListNode<T> Last { get; private set; }

        private void SelfListRemove(ListNode<T> node)
        {
            if (IsEmpty())
            {
                return;
            }
            ValidateNode(node);
            Count--;
            ModificationCode++;
            if (node.PreviousItem == null && node.NextItem == null)
            {
                First = null;
                Last = null;
            }
            else if (node.PreviousItem == null)
            {
                First = node.NextItem;
                First.PreviousItem = null;
            }
            else if (node.NextItem == null)
            {
                Last = node.PreviousItem;
                Last.NextItem = null;
            }
            else
            {
                node.PreviousItem.NextItem = node.NextItem;
                node.NextItem.PreviousItem = node.PreviousItem;
            }
            node.Invalidate();
        }

        public bool Remove(T item)
        {
            if (IsEmpty())
            {
                return false;
            }
            ListNode<T> currentNode = First;
            for (; currentNode != null; currentNode = currentNode.NextItem)
            {
                if (currentNode.Value != null  )
                {
                    if (currentNode.Value.Equals(item))
                    {
                        break;
                    }
                }
                else if (item == null)
                {
                    break;
                }
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
            SelfListRemove(First);
        }

        public void RemoveLast()
        {
            SelfListRemove(Last);
        }

        public int Count { get; private set; }

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
            ListNode<T> currentNode = First;
            while (currentNode != null)
            {
                ListNode<T> temp = currentNode;
                currentNode = currentNode.NextItem;
                temp.Invalidate();
            }
            First = null;
            Last = null;
            Count = 0;
            ModificationCode++;
        }

        public bool Contains(T item)
        {
            if (IsEmpty())
            {
                return false;
            }
            foreach (T x in this)
            {
                if (x != null)
                {
                    if (x.Equals(item))
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
            if (IsEmpty())
            {
                return;
            }
            int startIndex = index;
            foreach (T x in this)
            {
                array[startIndex] = x;
                startIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            long startModificationCode = ModificationCode;
            for (ListNode<T> currentNode = First; currentNode != null; currentNode = currentNode.NextItem)
            {
                if (startModificationCode != ModificationCode)
                {
                    throw new InvalidOperationException("Коллекция изменилась");
                }
                yield return currentNode.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ListNode<T> AddAfter(ListNode<T> node, T item)
        {
            ValidateNode(node);
            ListNode<T> result = new ListNode<T>(item);
            SelfListAddAfter(node, result);
            return result;
        }

        public void AddAfter(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNode(node);
            SelfListAddAfter(node, newNode);
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        private void AddNodeToEmptyList(ListNode<T> newNode)
        {
            First = newNode;
            Last = First;
        }

        public ListNode<T> AddLast(T item)
        {
            ListNode<T> newNode = new ListNode<T>(item);
            SelfListAddAfter(Last, newNode);
            return Last;
        }

        public void AddLast(ListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            AddLast(newNode.Value);
        }

        private void SelfListAddAfter(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            newNode.List = this;
            Count++;
            ModificationCode++;
            if (IsEmpty())
            {
                AddNodeToEmptyList(newNode);
                return;
            }
            if (node.NextItem == null)
            {
                node.NextItem = newNode;
                newNode.PreviousItem = node;
                Last = newNode;
            }
            else
            {
                newNode.NextItem = node.NextItem;
                node.NextItem.PreviousItem = newNode;
                node.NextItem = newNode;
                newNode.PreviousItem = node;
            }
        }

        private void SelfListAddBefore(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            newNode.List = this;
            Count++;
            ModificationCode++;
            if (IsEmpty())
            {
                AddNodeToEmptyList(newNode);
                return;
            }
            if (node.PreviousItem == null)
            {
                newNode.NextItem = node;
                node.PreviousItem = newNode;
                First = newNode;
            }
            else
            {
                node.PreviousItem.NextItem = newNode;
                newNode.PreviousItem = node.PreviousItem;
                newNode.NextItem = node;
                node.PreviousItem = newNode;
            }
        }

        public ListNode<T> AddBefore(ListNode<T> node, T item)
        {
            ValidateNode(node);
            ListNode<T> result = new ListNode<T>(item);
            SelfListAddBefore(node, result);
            return result;
        }

        public void AddBefore(ListNode<T> node, ListNode<T> newNode)
        {
            ValidateNode(node);
            SelfListAddBefore(node, newNode);
        }

        public ListNode<T> AddFirst(T item)
        {
            ListNode<T> result = new ListNode<T>(item);
            SelfListAddBefore(First, result);
            return result;
        }

        public void AddFirst(ListNode<T> newNode)
        {
            SelfListAddBefore(First, newNode);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ListNode<T> currentNode = First;
            sb.Append("{ ");
            while (currentNode != null)
            {
                sb.Append(currentNode.Value)
                    .Append(", ");
                currentNode = currentNode.NextItem;
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
