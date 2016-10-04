using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class Range
    {
        
        public Range()
        {
            BeginRange = 0.0;
            EndRange = 0.0;
        }

        public Range(double BeginRange, double EndRange)
        {
            this.BeginRange = Math.Min(BeginRange, EndRange);
            this.EndRange = Math.Max(BeginRange, EndRange);
        }

        public Range(Range range)
        {
            BeginRange = range.BeginRange;
            EndRange = range.EndRange;
        }

        private static bool IsDoubleEquals(double arg1, double arg2)
        {
            double epsilon = 1.0e-10;
            return Math.Abs(arg1 - arg2) <= epsilon;
        }

        public double Length
        {
            get
            {
                return EndRange - BeginRange;
            }
        }

        public double BeginRange
        {
            get;
        }

        public double EndRange
        {
            get;
        }

        public Range[] GetDifference(Range range)
        {
            if (IsDoubleEquals(range.BeginRange, BeginRange) && IsDoubleEquals(range.EndRange, EndRange))
            {
                return new Range[0];
            }

            Range intersectionRange = GetIntersection(range);
            if (intersectionRange == null)
            {
                return new Range[] { new Range(this) };
            }

            if (!IsDoubleEquals(intersectionRange.BeginRange, intersectionRange.EndRange)) 
            {
                return new Range[] { new Range(Math.Min(BeginRange, range.BeginRange), intersectionRange.BeginRange),
                    new Range(intersectionRange.EndRange, Math.Max(EndRange, range.EndRange)) };
            }

            return new Range[] { new Range(Math.Min(BeginRange, range.BeginRange), Math.Max(EndRange, range.EndRange)) };
        }

        public static void ShowDifference(Range range1, Range range2)
        {
            Console.Write("Разность интервалов {0} - {1} и {2} - {3} равно ", range1.BeginRange, range1.EndRange,
            range2.BeginRange, range2.EndRange);
            Range[] differenceRange = range1.GetDifference(range2);
            if (differenceRange.Length == 0)
            {
                Console.WriteLine("0");
                return;
            }
            Console.Write("{0} - {1}", differenceRange[0].BeginRange, differenceRange[0].EndRange);
            if (differenceRange.Length > 1)
            {
                Console.Write(", {0} - {1}", differenceRange[1].BeginRange, differenceRange[1].EndRange);
            }
            Console.WriteLine();
        }

        public Range[] GetUnion(Range range)
        {
            if (GetIntersection(range) == null)
            {
                return new Range[] { new Range(this), range };
            }
            return new Range[] { new Range(Math.Min(range.BeginRange, BeginRange), Math.Max(range.EndRange, EndRange)) };
        }

        public static void ShowUnion(Range range1, Range range2)
        {
            Console.Write("Объединение интервалов {0} - {1} и {2} - {3} равно ", range1.BeginRange, range1.EndRange,
                    range2.BeginRange, range2.EndRange);
            Range[] unionRange = range1.GetUnion(range2);
            Console.Write("{0} - {1}", unionRange[0].BeginRange, unionRange[0].EndRange);
            if (unionRange.Length > 1)
            {
                Console.Write(", {0} - {1}", unionRange[1].BeginRange, unionRange[1].EndRange);
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
            return null;
        }

        public static void ShowIntersection(Range range1, Range range2)
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
            return (position > BeginRange && position < EndRange ||
                (IsDoubleEquals(position, BeginRange) || IsDoubleEquals(position, EndRange)));
        }
    }
}
