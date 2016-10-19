using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{

    class CashMachine
    {
        private readonly List<Bill> pool;
        private int maxBillsCapacity;

        public CashMachine(int maxBillsCapacity, string location)
        {
            pool = new List<Bill>();
            this.maxBillsCapacity = maxBillsCapacity;
            Location = location;
        }

        public CashMachine(List<Bill> pool, int maxBillsCapacity, string location) : this (maxBillsCapacity, location)
        {
            if (GetTotalBillCount(pool) < maxBillsCapacity)
            {
                this.pool = pool;
            }
        }

        public CashMachine(CashMachine cashMashine)
        {
            this.Location = cashMashine.Location;
            this.pool = cashMashine.pool;
            this.maxBillsCapacity = cashMashine.maxBillsCapacity;
        }

        public string Location { get; }

        public int MaxBillsCapacity
        {
            get { return maxBillsCapacity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ошибка.Максимальное количество купюр не может быть меньше 0.");
                }
                maxBillsCapacity = value;
            }
        }

        public int TotalBillsCount
        {
            get
            {
                return GetTotalBillCount(pool);
            }
        }

        public int GetBillCount(Bills rating)
        {
            return GetBillCount(rating, pool);
        }

        private static int GetBillCount(Bills rating, List<Bill> pool)
        {
            int sum = 0;
            foreach (Bill bill in pool)
            {
                if (bill.Rating == rating)
                {
                    sum += bill.Count;
                }
            }
            return sum;
        }
        
        private static int GetTotalBillCount(List<Bill> pool)
        {
            int sum = 0;
            foreach (Bill bill in pool)
            {
                sum += bill.Count;
            }
            return sum;
        }
        
        public void AddBill(List<Bill> pool)
        {
            if (TotalBillsCount + GetTotalBillCount(pool) < MaxBillsCapacity)
            {
                this.pool.AddRange(pool);
            }
            else
            {
                foreach (Bill bill in pool)
                {
                    if (bill.Count + TotalBillsCount > MaxBillsCapacity)
                    {
                        this.pool.Add(new Bill(bill.Rating, MaxBillsCapacity - TotalBillsCount));
                        throw new ArgumentException("Превышено максимальное количество купюр.");
                    }
                    this.pool.Add(bill);
                }
            }
        }
        
        public void AddBill(Bills rating, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Ошибка. Количество купюр не может быть отрицательным.");
            }
            if (count + TotalBillsCount > MaxBillsCapacity)
            {
                pool.Add(new Bill(rating, MaxBillsCapacity - TotalBillsCount));
                throw new ArgumentException("Превышено максимальное количество купюр.");
            }
            pool.Add(new Bill(rating, count));
        }
        
        public Bill PayBill(Bills rating, int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Ошибка. Невозможно выдать отрицательное число купюр.");
            }
            int sum = 0;
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].Rating == rating)
                {
                    sum += pool[i].Count;
                    pool.Remove(pool[i]);
                    if (sum < capacity)
                    {
                        i--;
                        continue;
                    }
                    
                    if (sum != capacity)
                    {
                        pool.Add(new Bill(rating, sum - capacity));
                    }
                    return new Bill(rating, capacity);
                }
            }
            throw new ArgumentException(string.Format("В банкомате недостаточно купюр номинала {0}. Выдано {1}",
                rating, new Bill(rating, sum)));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{")
                .Append("  Расположение банкомата = ")
                .AppendLine(Location)
                .Append("  Максимальное количество купюр = ")
                .AppendLine(MaxBillsCapacity.ToString())
                .Append("  Общее количество купюр = ")
                .AppendLine(TotalBillsCount.ToString());
            foreach (Bills x in Enum.GetValues(typeof(Bills)))
            {
                sb.Append("  Купюр номинала ")
                    .Append(x)
                    .Append(" = ")
                    .AppendLine(GetBillCount(x).ToString());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
