using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{
    class Bill
    {
        private int count;

        public Bill(Bills rating, int count)
        {
            if (!Enum.IsDefined(typeof(Bills), rating) || rating == Bills.Blank)
            {
                throw new ArgumentException("Ошибка создания объекта. Заданный номинал не существует.");
            }
            if (count < 0)
            {
                throw new ArgumentException("Ошибка создания объекта. Число купюр не может быть отрицательным.");
            }
            Rating = rating;
            this.count = count;
        }

        public Bills Rating
        {
            get;
        }

        public int Count
        {
            get { return count; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ошибка. Число купюр не может быть отрицательным.");
                }
                count = value;
            }
        }

        public override string ToString()
        {
            return string.Format("[ Номинал = {0}, Количество = {1} ]", Rating.ToString("d"), Count);
        }
    }
}
