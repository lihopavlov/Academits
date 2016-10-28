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

        public CashMachine(int maxBillsCapacity, string location) : this(new List<Bill>
                                                                        {
                                                                            new Bill(Bills.Ten, 0),
                                                                            new Bill(Bills.Fifty, 0),
                                                                            new Bill(Bills.Hundred, 0),
                                                                            new Bill(Bills.FiveHundred, 0),
                                                                            new Bill(Bills.Thousand, 0),
                                                                            new Bill(Bills.FiveThousand, 0)
                                                                        }, maxBillsCapacity, location)
        {
        }

        public CashMachine(List<Bill> pool, int maxBillsCapacity, string location)
        {
            if (GetTotalBillCount(pool) > maxBillsCapacity)
            {
                throw new ArgumentException("Ошибка создания объекта. Превышено допустимое количество купюр.");
            }
            this.pool = RemoveDuplicate(pool);
            MaxBillsCapacity = maxBillsCapacity;
            Location = location;
        }

        public CashMachine(CashMachine cashMashine)
        {
            Location = cashMashine.Location;
            pool = cashMashine.pool;
            maxBillsCapacity = cashMashine.maxBillsCapacity;
        }

        private static List<Bill> RemoveDuplicate(List<Bill> pool)
        {
            List<Bill> result = new List<Bill>(pool);
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (result[i].Rating == result[j].Rating)
                    {
                        result[i].Count += result[j].Count;
                        result.RemoveAt(j);
                        j--;
                    }
                }
            }
            return result;
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
                if (TotalBillsCount > value)
                {
                    throw new ArgumentException("Ошибка. Невозможно установить заданное количество купюр. " +
                                                "Банкомат уже содержит большее число купюр.");
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

        public decimal TotalCashVolume
        {
            get
            {
                decimal sum = 0.0m;
                foreach (Bill bill in pool)
                {
                    sum += bill.Count * (int)bill.Rating;
                }
                return sum;
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
            if (TotalBillsCount + GetTotalBillCount(pool) > MaxBillsCapacity)
            {
                throw new ArgumentException("Ошибка. Превышено максимальное количество купюр. Средства не приняты.");
            }
            foreach (Bill bill in RemoveDuplicate(pool))
            {
                AddBill(bill.Rating, bill.Count);
            }
        }
        
        public void AddBill(Bills rating, int count)
        {
            if (!Enum.IsDefined(typeof(Bills), rating) || rating == Bills.Blank)
            {
                throw new ArgumentException("Ошибка. Заданный номинал не существует.");
            }
            if (count < 0)
            {
                throw new ArgumentException("Ошибка. Количество купюр не может быть отрицательным.");
            }
            if (count + TotalBillsCount > MaxBillsCapacity)
            {
                throw new ArgumentException("Превышено максимальное количество купюр. Средства не приняты.");
            }
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].Rating == rating)
                {
                    pool[i].Count += count;
                    return;
                }
            }
            pool.Add(new Bill(rating, count));
        }
        
        public Bill PayBill(Bills rating, int capacity)
        {
            if (!Enum.IsDefined(typeof(Bills), rating) || rating == Bills.Blank)
            {
                throw new ArgumentException("Ошибка. Заданный номинал не существует.");
            }
            if (capacity < 0)
            {
                throw new ArgumentException("Ошибка. Невозможно выдать отрицательное число купюр.");
            }
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].Rating == rating)
                {
                    if (capacity > pool[i].Count)
                    {
                        throw new ArgumentException(string.Format("Ошибка. В банкомате недостаточно купюр номинала {0}",
                            rating.ToString("d")));
                    }
                    pool[i].Count -= capacity;
                    return new Bill(rating, capacity);
                }
            }
            return new Bill(rating, 0);
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
                .AppendLine(TotalBillsCount.ToString())
                .Append("  Общее количество средств (руб.) = ")
                .AppendLine(TotalCashVolume.ToString());
            foreach (Bills x in Enum.GetValues(typeof(Bills)))
            {
                if (x != Bills.Blank)
                {
                    sb.Append("  Купюр номинала ");
                    sb.Append(x.ToString("d"))
                        .Append(" = ")
                        .AppendLine(GetBillCount(x).ToString());
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
