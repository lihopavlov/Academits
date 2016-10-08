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
            Vector zeroVector = new Vector(5);
            Vector zeroVector2 = new Vector(zeroVector);
            Vector vector1 = new Vector(new List<double> { 12.0, 2.0, 14.0 });
            Vector vector2 = new Vector(new List<double> { 4.0, 8.0 });
            Vector vector3 = new Vector(new List<double> { 8.0, 1.0, 5.0, 6.0 });
            Vector vector4 = new Vector(new List<double> { 12.0, 2.0, 14.0, 11.0, 10.0, 18.0 });
            try
            {
                Vector someVector = new Vector(0, new List<double> { 1.0, 2.0, 3.0 });
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка при создании объекта. Неверный размер.");
            }

            try
            {
                Vector vector5 = new Vector(3, new List<double> { 1.0, 2.0, 3.0 });
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка при создании объекта. Неверный размер.");
            }

            Console.Write("Сложение векторов {0} и {1}", vector1, vector2);
            vector1.Addition(vector2);
            Console.WriteLine(" равно {0}", vector1);
            Console.Write("Сложение векторов {0} и {1}", vector2, vector3);
            Console.WriteLine(" равно {0}", vector2.Addition(vector3));

            Console.Write("Вычитание векторов {0} и {1}", vector2, vector3);
            Console.WriteLine(" равно {0}", vector2.Subtraction(vector3));
            Console.Write("Вычитание векторов {0} и {1}", vector1, vector2);
            Console.WriteLine(" равно {0}", vector1.Subtraction(vector2));

            const int scalar = 2;
            Console.Write("Умножение вкетора {0} на скаляр {1} ", vector1, scalar);
            Console.WriteLine("равно {0}", vector1.Multiplication(scalar));

            Console.WriteLine("Длина вкетора {0} равна {1:0.00}", vector1, vector1.VectorLength);

            Console.WriteLine("Сложение векторов {0} и {1} равно {2}", vector1, vector2, new Vector(Vector.Addition(vector1, vector2)));
            Console.WriteLine("Вычитание векторов {0} и {1} равно {2}", vector1, vector2, new Vector(Vector.Subtraction(vector1, vector2)));
            Console.WriteLine("Умножение векторов {0} и {1} равно {2:0.00}", vector1, vector2, Vector.Multiplication(vector1, vector2));
        }
    }
}
