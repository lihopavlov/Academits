using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask
{
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
            Circle circle = (Circle)obj;
            return RealNumberUtils.IsRealEquals(circle.Width, Width) && RealNumberUtils.IsRealEquals(circle.Height, Height);
        }
    }
}
