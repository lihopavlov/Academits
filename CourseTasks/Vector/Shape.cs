using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    interface IShape
    {
        double Width
        {
            get;
        }

        double Height
        {
            get;
        }

        double Area
        {
            get;
        }

        double Perimeter
        {
            get;
        }
    }

    class ShapeAreaComparer : IComparer<IShape>
    {
        public int Compare(IShape shape1, IShape shape2)
        {
            if (shape1.Area > shape2.Area)
            {
                return 1;
            }
            if (shape1.Area < shape2.Area)
            {
                return -1;
            }
            return 0;
        }
    }

    class ShapePerimeterComparer : IComparer<IShape>
    {
        public int Compare(IShape shape1, IShape shape2)
        {
            if (shape1.Perimeter > shape2.Perimeter)
            {
                return 1;
            }
            if (shape1.Perimeter < shape2.Perimeter)
            {
                return -1;
            }
            return 0;
        }
    }

    class Square : IShape
    {
        private const string shapeName = "КВАДРАТ";
        private const int startHash = 14;

        public Square(double sideLength)
        {
            if (sideLength >= 0)
            {
                SideLength = sideLength;
            }
            else
            {
                SideLength = 0.0;
            }
        }

        public double SideLength
        {
            get;
        }

        public double Width
        {
            get
            {
                return SideLength;
            }
        }

        public double Height
        {
            get
            {
                return SideLength;
            }
        }

        public double Area
        {
            get
            {
                return Math.Pow(SideLength, 2);
            }
        }

        public double Perimeter
        {
            get
            {
                return 4 * SideLength;
            }
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
            hash ^= SideLength.GetHashCode();
            return hash ^ shapeName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Square square = obj as Square;
            return square.Width == Width;
        }
    }

    class Triangle : IShape
    {
        private const string shapeName = "ТРЕУГОЛЬНИК";

        private double sideALength;
        private double sideBLength;
        private double sideCLength;
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

            if (!IsSameLine(X1, Y1, X2, Y2, X3, Y3))
            {
                sideALength = LenghthOfSegment(X1, Y1, X2, Y2);
                sideBLength = LenghthOfSegment(X1, Y1, X3, Y3);
                sideCLength = LenghthOfSegment(X2, Y2, X3, Y3);
            }
            else
            {
                sideALength = 0.0;
                sideBLength = 0.0;
                sideCLength = 0.0;
            }
            Perimeter = (sideALength + sideBLength + sideCLength);
        }

        private static double LenghthOfSegment(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y2 - y1, 2));
        }

        private static bool IsDoubleEquals(double arg1, double arg2)
        {
            double epsilon = 1.0e-10;
            return Math.Abs(arg1 - arg2) <= epsilon;
        }

        private static bool IsSameLine(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            if (IsDoubleEquals(x1, x2) && IsDoubleEquals(x1, x3) || IsDoubleEquals(y1, y2) && IsDoubleEquals(y1, y3))
            {
                return true;
            }

            double exprRightPart = x3 * (y2 - y1) / (x2 - x1) + y1 - x1 * (y2 - y1) / (x2 - x1);
            return IsDoubleEquals(y3, exprRightPart);
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
                double halfPerimeter = Perimeter / 2;
                return Math.Sqrt(halfPerimeter * (halfPerimeter - sideALength) * (halfPerimeter - sideBLength) *
                    (halfPerimeter - sideCLength));
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
            Triangle triangle = obj as Triangle;
            return triangle.X1 == X1 && triangle.Y1 == Y1 && triangle.X2 == X2 && 
                triangle.Y2 == Y2 && triangle.X3 == X3 && triangle.Y3 == Y3;
        }
    }

    class Rectangle : IShape
    {
        private const string shapeName = "ПРЯМОУГЛЬНИК";
        private const int startHash = 16;

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width 
        {
            get;
        }

        public double Height
        {
            get;
        }
            
        public double Area
        {
            get
            {
                return Width * Height;
            }
        }

        public double Perimeter
        {
            get
            {
                return (Width + Height) * 2;
            }
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
            hash ^= Width.GetHashCode();
            hash ^= Height.GetHashCode();
            return hash ^ shapeName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Rectangle rectangle = obj as Rectangle;
            return rectangle.Width == Width && rectangle.Height == Height;
        }
    }

    class Circle : IShape
    {
        private const string shapeName = "КРУГ";
        private const int startHash = 17;

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double Radius
        {
            get;
        }

        public double Width
        {
            get
            {
                return Radius * 2;
            }
        }
            
        public double Height
        {
            get
            {
                return Radius * 2;
            }
        }

        public double Area
        {
            get
            {
                return Math.PI * Math.Pow(Radius, 2);
            }
        }

        public double Perimeter
        {
            get
            {
                return 2 * Math.PI * Radius;
            }
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
            hash ^= Radius.GetHashCode();
            return hash ^ shapeName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Circle circle = obj as Circle;
            return circle.Width == Width && circle.Height == Height;
        }
    }
}
