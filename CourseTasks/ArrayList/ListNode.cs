using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    class ListNode<T>
    {
        public T Value { get; set; }
        public ListNode<T> NextItem { get; set; }
        public ListNode<T> PreviousItem { get; set; }

        public ListNode(T value) : this(null, null)
        {
            Value = value;
        }

        public ListNode() : this(null, null)
        {
        }

        public ListNode(ListNode<T> previousItem, ListNode<T> nextItem)
        {
            PreviousItem = previousItem;
            NextItem = nextItem;
        }

        public ListNode(ListNode<T> previosItem) : this(previosItem, null)
        {
        }
    }
}
