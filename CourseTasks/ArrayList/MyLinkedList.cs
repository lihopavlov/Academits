using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    class MyLinkedList<T>// : ICollection<T>
    {
        private ListNode<T> startNode;
        private ListNode<T> endNode;
        private int itemsCount;

        public MyLinkedList()
        {
            startNode = null;
            endNode = null;
            itemsCount = 0;
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
        }

        public bool Remove(T item)
        {
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
