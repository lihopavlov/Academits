using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    using Vector;

    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix(new List<List<double>>
            {
                new List<double> { 5.0, 7.0, 1.0 },
                new List<double> { -4.0, 1.0, 0.0 },
                new List<double> { 2.0, 0.0, 3.0 },
            });

            Matrix matrix8 = new Matrix(new List<List<double>>
            {
                new List<double> { 5.0, 7.0, 1.0, 4.0, 3.0 },
                new List<double> { -4.0, 1.0, 0.0, -1.0, 12.0 }
            });

            try
            {
                Console.WriteLine(matrix.CalcDeterminant());
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(matrix8.Transpose());

            Matrix matrix1 = new Matrix(new List<List<double>>
            {
                new List<double> { 1.0, 2.0 },
                new List<double> { -2.0, 5.0 },
                new List<double> { 4.0, 7.0},
                new List<double> { 5.0, 4.0 }
            });

            Matrix matrix2 = new Matrix(new List<List<double>>
            {
                new List<double> { 1.0, 2.0, 3.0, 7.0 },
                new List<double> { -2.0, 5.0, 8.0, 9.0 },
            });

            Matrix matrix3 = new Matrix(new List<List<double>>
            {
                new List<double> { 1.0, 2.0, 3.0, 7.0 },
                new List<double> { -2.0, 5.0 },
            });

            Matrix matrix4 = new Matrix(new List<List<double>>
            {
                new List<double> { 1.0, 2.0 },
                new List<double> { -2.0, 5.0 },
                new List<double> { 4.0, 7.0},
                new List<double> { 5.0, 4.0 }
            });

            Matrix matrix5 = new Matrix(new List<List<double>>
            {
                new List<double> { 1.0, 2.0, 3.0, 7.0 },
                new List<double> { -2.0, 5.0 },
            });

            Console.WriteLine("-------ADDITION---------");
            Console.WriteLine(matrix1.Addition(matrix2));
            Console.WriteLine(matrix5.Addition(matrix4));
            Console.WriteLine(matrix3.Subtraction(matrix4));

            try
            {
                Console.WriteLine(matrix3.Multiplication(new Vector(new List<double> {1, 2, 3, 4})));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();

            Matrix matrix6 = new Matrix(new List<List<double>>
            {
                new List<double> { 1.0, 3.0, 2.0 },
                new List<double> { 0.0, 4.0, -1.0 },
            });
            Matrix matrix7 = new Matrix(new List<List<double>>
            {
                new List<double> { 2.0, 0.0, -1.0, -1.0 },
                new List<double> { 3.0, -2.0, 1.0, 2.0 },
                new List<double> { 0.0, 1.0, 2.0, 3.0 },
            });

            try
            {
                Console.WriteLine(Matrix.Multiplication(matrix6, matrix7));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();

            Matrix matrix9 = new Matrix(new List<List<double>>
            {
                new List<double> { 2.0, 0.0, -1.0, -1.0 },
                new List<double> { 3.0, -2.0, 1.0, 2.0 },
                new List<double> { 0.0, 1.0, 2.0, 3.0 },
            });

            try
            {
                matrix9[matrix9.RowCount - 1] = new Vector(new List<double> { 18.0, 19.0 });
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(matrix9);
            Console.WriteLine();

            try
            {
                matrix9[matrix9.RowCount - 1] = new Vector(new List<double> { 18.0, 19.0, 20.0, 21.0, 22.0, 24.0 });
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(matrix9);
            Console.WriteLine();

            try
            {
                matrix9[0] = new Vector(new List<double> { 18.0, 19.0, 20.0, 21.0, 22.0, 24.0, 1.0 });
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(matrix9);
            Console.WriteLine();

            matrix9.Multiplication(30.0);
            Console.WriteLine(matrix9);
            Console.WriteLine();

            Matrix matrix10 = new Matrix(new List<List<double>>
            {
                new List<double> { 2, 1 },
                new List<double> { 3, 2 },
            });
            Matrix matrix11 = new Matrix(new List<List<double>>
            {
                new List<double> { 2, 1 },
                new List<double> { 3, 2 },
                new List<double> { 1, 1 },
            });

            matrix10.Subtraction(matrix11);
            Console.WriteLine(matrix10);
            Console.WriteLine();
        }
    }
}
