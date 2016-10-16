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
        FiveThousnad = 5000
    }

    class CashMashine
    {
        private static readonly int TenBillsMaxCapacity;
        private static readonly int FiftyBillsMaxCapacity;
        private static readonly int HundredBillsMaxCapacity;
        private static readonly int FiveHundredBillsMaxCapacity;
        private static readonly int ThousandBillsMaxCapacity;
        private static readonly int FiveThousandBillsMaxCapacity;

        private int tenCapacity;
        private int fiftyCapacity;
        private int hundredCapacity;
        private int fiveHundredCapacity;
        private int thousandCapacity;
        private int fiveThousandCapacity;

        static CashMashine()
        {
            TenBillsMaxCapacity = 1000;
            FiftyBillsMaxCapacity = 1000;
            HundredBillsMaxCapacity = 1000;
            FiveHundredBillsMaxCapacity = 1000;
            ThousandBillsMaxCapacity = 100;
            FiveThousandBillsMaxCapacity = 100;
        }

        public CashMashine() : this(0, 0, 0, 0, 0, 0)
        {
        }

        public CashMashine(int tenCapacity, int fiftyCapacity, int hundredCapacity, int fiveHundredCapacity,
            int thousandCapacity, int fiveThousandCapacity)
        {
            this.tenCapacity = tenCapacity;
            this.fiftyCapacity = fiftyCapacity;
            this.hundredCapacity = hundredCapacity;
            this.fiveHundredCapacity = fiveHundredCapacity;
            this.thousandCapacity = thousandCapacity;
            this.fiveThousandCapacity = fiveThousandCapacity;
        }

        public CashMashine(CashMashine cashMashine) : this(cashMashine.TenCapacity, cashMashine.FiftyCapacity,
            cashMashine.HundredCapacity, cashMashine.FiveHundredCapacity, cashMashine.ThousandCapacity, 
            cashMashine.FiveThousandCapacity)
        {
        }

        public int TenCapacity
        {
            get { return tenCapacity; }
            set
            {
                if (tenCapacity < 0)
                {
                    throw new ArgumentException();
                }
                if (tenCapacity + value > TenBillsMaxCapacity)
                {
                    int billsToBack = value - (TenBillsMaxCapacity - tenCapacity);
                    tenCapacity = TenBillsMaxCapacity;
                    throw new ArgumentException(string.Format("Слишком много купюр номинала {0}. Возвращено {1} купюр.", Bills.Ten, billsToBack));
                }
                tenCapacity = value;
            }
        }

        public int FiftyCapacity
        {
            get { return fiftyCapacity; }
            set
            {
                if (fiftyCapacity < 0)
                {
                    throw new ArgumentException();
                }
                if (fiftyCapacity + value > FiftyBillsMaxCapacity)
                {
                    int billsToBack = value - (FiftyBillsMaxCapacity - fiftyCapacity);
                    fiftyCapacity = FiftyBillsMaxCapacity;
                    throw new ArgumentException(string.Format("Слишком много купюр. Возвращено {0} купюр.", billsToBack));
                }
                fiftyCapacity = value;
            }
        }

        public int HundredCapacity
        {
            get { return hundredCapacity; }
            set
            {
                if (hundredCapacity < 0)
                {
                    throw new ArgumentException();
                }
                if (hundredCapacity + value > HundredBillsMaxCapacity)
                {
                    int billsToBack = value - (HundredBillsMaxCapacity - hundredCapacity);
                    hundredCapacity = HundredBillsMaxCapacity;
                    throw new ArgumentException(string.Format("Слишком много купюр. Возвращено {0} купюр.", billsToBack));
                }
                hundredCapacity = value;
            }
        }

        public int FiveHundredCapacity
        {
            get { return fiveHundredCapacity; }
            set
            {
                if (fiveHundredCapacity < 0)
                {
                    throw new ArgumentException();
                }
                if (fiveHundredCapacity + value > FiveHundredBillsMaxCapacity)
                {
                    int billsToBack = value - (FiveHundredBillsMaxCapacity - fiveHundredCapacity);
                    fiveHundredCapacity = FiveHundredBillsMaxCapacity;
                    throw new ArgumentException(string.Format("Слишком много купюр. Возвращено {0} купюр.", billsToBack));
                }
                fiveHundredCapacity = value;
            }
        }

        public int ThousandCapacity
        {
            get { return thousandCapacity; }
            set
            {
                if (thousandCapacity < 0)
                {
                    throw new ArgumentException();
                }
                if (thousandCapacity + value > ThousandBillsMaxCapacity)
                {
                    int billsToBack = value - (ThousandBillsMaxCapacity - thousandCapacity);
                    thousandCapacity = ThousandBillsMaxCapacity;
                    throw new ArgumentException(string.Format("Слишком много купюр. Возвращено {0} купюр.", billsToBack));
                }
                thousandCapacity = value;
            }
        }

        public int FiveThousandCapacity
        {
            get { return fiveThousandCapacity; }
            set
            {
                if (fiveThousandCapacity < 0)
                {
                    throw new ArgumentException();
                }
                if (fiveThousandCapacity + value > FiveThousandBillsMaxCapacity)
                {
                    int billsToBack = value - (FiveThousandBillsMaxCapacity - fiveThousandCapacity);
                    fiveThousandCapacity = FiveThousandBillsMaxCapacity;
                    throw new ArgumentException(string.Format("Слишком много купюр. Возвращено {0} купюр.", billsToBack));
                }
                fiveThousandCapacity = value;
            }
        }

        public string PushBills(int tenCapacity, int fiftyCapacity, int hundredCapacity, int fiveHundredCapacity,
            int thousandCapacity, int fiveThousandCapacity)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                TenCapacity += tenCapacity;
            }
            catch (ArgumentException e)
            {
                sb.AppendLine(e.Message);
            }
        }
    }
}
