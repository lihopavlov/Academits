using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask
{
    class Square : IShape
    {
        private const string shapeName = "КВАДРАТ";
        private const int startHash = 14;

        public Square(double sideLength)
        {
            //Я правильно понял?
            SideLength = Math.Max(SideLength, sideLength);
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
            Square square = (Square)obj;
            return RealNumberUtils.IsRealEquals(square.Width, Width);
        }
    }
}
