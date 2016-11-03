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

        public ListNode(T value) : this(value, null)
        {
        }

        internal ListNode(T value, MyLinkedList<T> list)
        {
            Value = value;
            List = list;
        }

        public ListNode<T> NextItem
        {
            get; internal set; }

        public ListNode<T> PreviousItem { get; internal set; }

        public MyLinkedList<T> List { get; internal set; }

        internal void Invalidate()
        {
            PreviousItem = null;
            NextItem = null;
            List = null;
        }

        public override string ToString()
        {
            return string.Format("[ {0} ]", Value);
        }
    }
}
