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

        public ListNode(T value)
        {
            Value = value;
            NextItem = null;
            PreviousItem = null;
        }

        public ListNode(T value, ListNode<T> previousItem, ListNode<T> nextItem)
        {
            Value = value;
            NextItem = nextItem;
            PreviousItem = previousItem;
        }

        public ListNode(T value, ListNode<T> previosItem)
        {
            Value = value;
            NextItem = null;
            PreviousItem = previosItem;
        }
    }
}
