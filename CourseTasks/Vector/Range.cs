using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class Range
    {
        private double beginRange;
        private double endRange;
        
        public Range()
        {
            beginRange = 0.0;
            endRange = 0.0;
        }

        public Range(double beginRange, double endRange)
        {
            this.beginRange = Math.Min(beginRange, endRange);
            this.endRange = Math.Max(beginRange, endRange);
        }

        static private bool IsDoubleEquals(double arg1, double arg2)
        {
            double epsilon = 1.0e-10;
            return Math.Abs(arg1 - arg2) <= epsilon;
        }

        public double Length
        {
            get
            {
                return endRange - beginRange;
            }
        }

        public double BeginRange
        {
            get
            {
                return beginRange;
            }
        }

        public double EndRange
        {
            get
            {
                return endRange;
            }
        }

        public Range[] GetDifference(Range range)
        {
            Range[] differenceRange = new Range[2];
            Range intersetcionRange = GetIntersection(range);
            if (intersetcionRange == null)
            {
                differenceRange[0] = this;
                differenceRange[1] = null;
            }
            else
            {
                if (!IsDoubleEquals(intersetcionRange.BeginRange, intersetcionRange.EndRange)) 
                {
                    differenceRange[0] = new Range(Math.Min(BeginRange, range.BeginRange), intersetcionRange.BeginRange);
                    differenceRange[1] = new Range(intersetcionRange.EndRange, Math.Max(EndRange, range.EndRange));
                }
                else
                {
                    differenceRange[0] = new Range(Math.Min(BeginRange, range.BeginRange), Math.Max(EndRange, range.EndRange));
                    differenceRange[1] = null;
                }
            }
            return differenceRange;
        }

        static public void ShowDifference(Range range1, Range range2)
        {
            Console.Write("Разность интервалов {0} - {1} и {2} - {3} равно ", range1.BeginRange, range1.EndRange,
            range2.BeginRange, range2.EndRange);
            Range[] differenceRange = range1.GetDifference(range2);
            if (differenceRange[0] != null)
            {
                Console.Write("{0} - {1}", differenceRange[0].BeginRange, differenceRange[0].EndRange);
                if (differenceRange[1] != null)
                {
                    Console.Write(", {0} - {1}", differenceRange[1].BeginRange, differenceRange[1].EndRange);
                }
            }
            Console.WriteLine();
        }

        public Range[] GetUnion(Range range)
        {
            Range[] unionRange = new Range[2];
            if (GetIntersection(range) == null)
            {
                unionRange[0] = this;
                unionRange[1] = range;
            }
            else
            {
                unionRange[0] = new Range(Math.Min(range.BeginRange, BeginRange), Math.Max(range.EndRange, EndRange));
                unionRange[1] = null;
            }
            return unionRange;
        }

        static public void ShowUnion(Range range1, Range range2)
        {
            Console.Write("Объединение интервалов {0} - {1} и {2} - {3} равно ", range1.BeginRange, range1.EndRange,
                    range2.BeginRange, range2.EndRange);
            Range[] unionRange = range1.GetUnion(range2);
            if (unionRange[0] != null)
            {
                Console.Write("{0} - {1}", unionRange[0].BeginRange, unionRange[0].EndRange);
                if (unionRange[1] != null)
                {
                    Console.Write(", {0} - {1}", unionRange[1].BeginRange, unionRange[1].EndRange);
                }
            }
            Console.WriteLine();
        }

        public Range GetIntersection(Range range)
        {
            if (IsInside(range.BeginRange) || IsInside(range.EndRange) ||
                range.IsInside(BeginRange))
            {
                return new Range(Math.Max(range.BeginRange, BeginRange), Math.Min(range.EndRange, EndRange));
            }
            else
            {
                return null;
            }
        }

        static public void ShowIntersection(Range range1, Range range2)
        {
            Range intersetcionRange = range1.GetIntersection(range2);
            if (intersetcionRange != null)
            {
                Console.WriteLine("Пересечение интервалов {0} - {1} и {2} - {3} равно {4} - {5}", range1.BeginRange, range1.EndRange,
                    range2.BeginRange, range2.EndRange, intersetcionRange.BeginRange, intersetcionRange.EndRange);
            }
            else
            {
                Console.WriteLine("Пересечение не найдено");
            }
        }

        public bool IsInside(double position)
        {
            return (position > beginRange && position < endRange ||
                (IsDoubleEquals(position, beginRange) || IsDoubleEquals(position, endRange)));
        }
    }
}
