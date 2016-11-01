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
        public MyLinkedList<T> List { get; set; }

        public ListNode(T value, MyLinkedList<T> list) : this(value, list, null, null)
        {
        }

        public ListNode(T value) : this(value, null, null, null)
        {
        }

        public ListNode(T value, MyLinkedList<T> list, ListNode<T> previousItem, ListNode<T> nextItem)
        {
            Value = value;
            List = list;
            PreviousItem = previousItem;
            NextItem = nextItem;
        }

        public ListNode(T value, MyLinkedList<T> list, ListNode<T> previosItem) : this(value, list, previosItem, null)
        {
        }
    }
}
