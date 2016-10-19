using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{
    class Bill
    {
        public Bill(Bills rating, int count)
        {
            Rating = rating;
            Count = count;
        }

        public Bills Rating
        {
            get;
        }

        public int Count
        {
            get;
        }

        public override string ToString()
        {
            return string.Format("[ {0}, {1} ]", Rating, Count);
        }
    }
}
