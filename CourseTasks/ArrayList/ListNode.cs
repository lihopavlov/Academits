using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    sealed class ListNode<T>
    {
        public T Value { get; set; }
        internal ListNode<T> nextItem;
        internal ListNode<T> previousItem;
        internal MyLinkedList<T> list;

        public ListNode(T value) : this(value, null)
        {
        }

        internal ListNode(T value, MyLinkedList<T> list)
        {
            Value = value;
            this.list = list;
        }

        public ListNode<T> NextItem
        {
            get { return nextItem; }
        }

        public ListNode<T> PreviousItem
        {
            get { return previousItem; }
        }

        public MyLinkedList<T> List
        {
            get { return list; }
        }

        internal void Invalidate()
        {
            previousItem = null;
            nextItem = null;
            list = null;
        }
    }
}
