using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask
{
    static class RealNumberUtils
    {
        public static bool IsRealEquals(double arg1, double arg2)
        {
            double epsilon = 1.0e-10;
            return Math.Abs(arg1 - arg2) <= epsilon;
        }
    }
}
