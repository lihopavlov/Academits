using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class Program
    {

        private static IShape FindMaxArea(List<IShape> shapes)
        {
            ShapeAreaComparer areaComparer = new ShapeAreaComparer();
            shapes.Sort(areaComparer);
            return shapes[shapes.Count - 1];
        }

        private static IShape FindNMaxPerimeter(List<IShape> shapes, int nIndex)
        {
            ShapePerimeterComparer perimeterComparer = new ShapePerimeterComparer();
            shapes.Sort(perimeterComparer);

            if (nIndex < 1 || nIndex > shapes.Count)
            {
                return null;
            }
            return shapes[shapes.Count - nIndex];
        }

        static void Main(string[] args)
        {
            const int secondMaxPerimeter = 2;

            List<IShape> shapes = new List<IShape>();
            shapes.Add(new Square(5.0));
            shapes.Add(new Square(4.0));
            shapes.Add(new Rectangle(2.0, 6.0));
            shapes.Add(new Rectangle(3.0, 4.0));
            shapes.Add(new Triangle(5.0, 1.0, 7.0, 4.0, 2.0, 5.0));
            shapes.Add(new Triangle(5.0, 1.0, 8.0, 3.0, 2.5, 5.5));
            shapes.Add(new Circle(5.0));
            shapes.Add(new Circle(5.5));

            ShapeAreaComparer areaComparer = new ShapeAreaComparer();
            shapes.Sort(areaComparer);
            Console.WriteLine("По возрастанию площади:");
            foreach (IShape shape in shapes)
            {
                Console.WriteLine(shape);
            }

            ShapePerimeterComparer perimeterComparer = new ShapePerimeterComparer();
            shapes.Sort(perimeterComparer);
            Console.WriteLine("По возрастанию периметра:");
            foreach (IShape shape in shapes)
            {
                Console.WriteLine(shape);
            }

            Console.WriteLine("Фигура с максимальной площадью ->");
            Console.WriteLine(FindMaxArea(shapes));
            Console.WriteLine("Фигура со вторым по величине периметром ->");
            Console.WriteLine(FindNMaxPerimeter(shapes, secondMaxPerimeter));

        }
    }
}
