using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMashine
{

    enum Bills
    {
        Ten = 10,
        Fifty = 50,
        Hundred = 100,
        FiveHundred = 500,
        Thousand = 1000,
        FiveThousand = 5000
    }


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
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ")
                .Append(Rating)
                .Append(", ")
                .Append(Count)
                .Append(" }");
            return sb.ToString();
        }
    }
}
