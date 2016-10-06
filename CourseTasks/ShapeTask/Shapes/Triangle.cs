using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask
{
    class Triangle : IShape
    {
        private const string shapeName = "ТРЕУГОЛЬНИК";

        private readonly double sideALength;
        private readonly double sideBLength;
        private readonly double sideCLength;
        private const int startHash = 15;

        public double X1
        {
            get;
        }

        public double Y1
        {
            get;
        }

        public double X2
        {
            get;
        }

        public double Y2
        {
            get;
        }


        public double X3
        {
            get;
        }

        public double Y3
        {
            get;
        }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;

            sideALength = LenghthOfSegment(X1, Y1, X2, Y2);
            sideBLength = LenghthOfSegment(X1, Y1, X3, Y3);
            sideCLength = LenghthOfSegment(X2, Y2, X3, Y3);

            Perimeter = (sideALength + sideBLength + sideCLength);
        }

        private static double LenghthOfSegment(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y2 - y1, 2));
        }

        private static bool IsSameLine(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            if (RealNumberUtils.IsRealEquals(x1, x2) && RealNumberUtils.IsRealEquals(x1, x3) || 
                RealNumberUtils.IsRealEquals(y1, y2) && RealNumberUtils.IsRealEquals(y1, y3))
            {
                return true;
            }

            double exprRightPart = x3 * (y2 - y1) / (x2 - x1) + y1 - x1 * (y2 - y1) / (x2 - x1);
            return RealNumberUtils.IsRealEquals(y3, exprRightPart);
        }

        public double Width
        {
            get
            {
                return Math.Max(Math.Max(X1, X2), X3) - Math.Min(Math.Min(X1, X2), X3);
            }
        }

        public double Height
        {
            get
            {
                return Math.Max(Math.Max(Y1, Y2), Y3) - Math.Min(Math.Min(Y1, Y2), Y3);
            }
        }

        public double Area
        {
            get
            {
                if (!IsSameLine(X1, Y1, X2, Y2, X3, Y3))
                {
                    double halfPerimeter = Perimeter / 2;
                    return Math.Sqrt(halfPerimeter * (halfPerimeter - sideALength) * (halfPerimeter - sideBLength) *
                        (halfPerimeter - sideCLength));
                }
                return 0.0;
            }
        }

        public double Perimeter
        {
            get;
        }

        public override string ToString()
        {
            return string.Format("Фигура: {0} (ширина = {1}; высота = {2}; площадь = {3}; периметр = {4})",
                shapeName.PadLeft(14), Width.ToString("0.0").PadLeft(5), Height.ToString("0.0").PadLeft(5),
                Area.ToString("0.00").PadLeft(5), Perimeter.ToString("0.00").PadLeft(5));
        }

        public override int GetHashCode()
        {
            int hash = startHash;
            hash ^= X1.GetHashCode();
            hash ^= Y1.GetHashCode();
            hash ^= X2.GetHashCode();
            hash ^= Y2.GetHashCode();
            hash ^= X3.GetHashCode();
            hash ^= Y3.GetHashCode();
            return hash ^ shapeName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Triangle triangle = (Triangle)obj;
            return RealNumberUtils.IsRealEquals(triangle.X1, X1) && RealNumberUtils.IsRealEquals(triangle.Y1, Y1) && 
                RealNumberUtils.IsRealEquals(triangle.X2, X2) && RealNumberUtils.IsRealEquals(triangle.Y2, Y2) && 
                RealNumberUtils.IsRealEquals(triangle.X3, X3) && RealNumberUtils.IsRealEquals(triangle.Y3, Y3);
        }
    }
}
