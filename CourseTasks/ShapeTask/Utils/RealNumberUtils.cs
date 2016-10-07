using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask.Utils
{
    public static class RealNumberUtils
    {
        public static bool IsRealEquals(double arg1, double arg2)
        {
            double epsilon = 1.0e-10;
            return Math.Abs(arg1 - arg2) <= epsilon;
        }

        public static bool IsRealEquals(double arg1, double arg2, double precision)
        {
            return Math.Abs(arg1 - arg2) <= precision;
        }

    }
}
