using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class Program
    {
        static void Main(string[] args)
        {
            Range range1 = new Range(3.0, 10.0);
            Range range2 = new Range(1.0, 5.0);
            Range range3 = new Range(8.0, 11.0);
            Range range4 = new Range(4.0, 9.0);
            Range range5 = new Range(0.5, 11.0);
            Range range6 = new Range(11.0, 15.0);
            Range range7 = new Range(10.0, 15.0);
            Range range8 = new Range(2.0, 3.0);

            Range.ShowIntersection(range1, range2);
            Range.ShowIntersection(range1, range3);
            Range.ShowIntersection(range1, range4);
            Range.ShowIntersection(range1, range5);
            Range.ShowIntersection(range1, range6);
            Console.WriteLine();

            Range.ShowUnion(range1, range2);
            Range.ShowUnion(range1, range3);
            Range.ShowUnion(range1, range4);
            Range.ShowUnion(range1, range5);
            Range.ShowUnion(range1, range6);
            Range.ShowUnion(range1, range7);
            Console.WriteLine();

            Range.ShowDifference(range1, range2);
            Range.ShowDifference(range1, range3);
            Range.ShowDifference(range1, range4);
            Range.ShowDifference(range1, range5);
            Range.ShowDifference(range1, range6);
            Range.ShowDifference(range1, range7);
            Range.ShowDifference(range1, range8);
            Console.WriteLine();

        }
    }
}
