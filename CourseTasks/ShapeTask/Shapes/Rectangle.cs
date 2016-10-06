using ShapeTask.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask.Shapes
{
    class Rectangle : IShape
    {
        private const string shapeName = "ПРЯМОУГЛЬНИК";
        private const int startHash = 16;

        public Rectangle(double width, double height)
        {
            Width = Math.Max(width, 0);
            Height = Math.Max(height, 0);
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
            Rectangle rectangle = (Rectangle)obj;
            return RealNumberUtils.IsRealEquals(rectangle.Width, Width) && RealNumberUtils.IsRealEquals(rectangle.Height, Height);
        }
    }
}
