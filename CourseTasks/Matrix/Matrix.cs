using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Matrix
{
    using ShapeTask.Utils;
    using Vector;

    public class Matrix
    {
        private List<Vector> rows;
        private const double Precision = 0.0001;
        private const int FirstLineNumber = 0;

        public Matrix(List<List<double>> table)
        {
            rows = new List<Vector>();
            foreach (List<double> line in table)
            {
                rows.Add(new Vector(line));
            }
            AddZerosToLowLines();
        }

        public Matrix(Matrix matrix) : this(matrix.rows)
        {
        }

        public Matrix(List<Vector> rows)
        {
            this.rows = new List<Vector>(rows);
            AddZerosToLowLines();
        }

        public Matrix(int width, int height)
        {
            rows = new List<Vector>();
            for (int i = 0; i < height; i++)
            {
                rows.Add(new Vector(width));
            }
        }

        private void AddZerosToLowLines()
        {
            int maxLineLength = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                maxLineLength = Math.Max(maxLineLength, rows[i].Size);
            }
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i].Size < maxLineLength)
                {
                    rows[i].Addition(new Vector(maxLineLength));
                }
            }
        }

        public int Height
        {
            get { return rows.Count; }
        }

        public int Width
        {
            get
            {
                return rows[FirstLineNumber].Size;
            }
        }

        public Vector this[int i]
        {
            get
            {
                if (i < 0 || i >= Height)
                {
                    throw new ArgumentException("Ошибка. Индекс вне диапазона.");
                }
                return new Vector(rows[i]);
            }
            set
            {
                if (i < 0 || i >= Height)
                {
                    throw new ArgumentException("Ошибка. Индекс вне диапазона.");
                }
                rows[i] = value;

                if (rows[i].Size > rows[rows.Count - 1].Size)
                {
                    for (int j = 0; j < rows.Count; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }
                        rows[j].Addition(new Vector(rows[j].Size));
                    }
                }
                else
                {
                    rows[i].Addition(new Vector(rows[rows.Count - 1].Size));
                }
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Height || j < 0 || j >= Width)
                {
                    throw new ArgumentException("Ошибка. Индекс вне диапазона.");
                }
                return rows[i][j];
            }
            set
            {
                if (i < 0 || i >= Height || j < 0 || j >= Width)
                {
                    throw new ArgumentException("Ошибка. Индекс вне диапазона.");
                }
                rows[i][j] = value;
            }
        }

        public Vector GetColumn(int index)
        {
            if (index < 0 || index >= Width)
            {
                throw new ArgumentException("Ошибка. Индекс вне диапазона.");
            }
            List<double> column = new List<double>();
            for (int i = 0; i < Height; i++)
            {
                if (this[i].Size - 1 < index)
                {
                    column.Add(0.0);
                }
                else
                {
                    column.Add(this[i, index]);
                }
            }
            return new Vector(column);
        }

        public Matrix Transpose()
        {
            Matrix matrixCopy = new Matrix(this);
            rows.RemoveRange(0, rows.Count);
            for (int i = 0; i < matrixCopy.Width; i++)
            {
                rows.Add(matrixCopy.GetColumn(i));
            }
            return this;
        }

        public Matrix Multiplication(double scalar)
        {
            foreach (Vector line in rows)
            {
                for (int i = 0; i < line.Size; i++)
                {
                    line[i] *= scalar;
                }
            }
            return this;
        }

        public static Matrix Multiplication(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Width != matrix2.Height)
            {
                throw new ArgumentException("Ошибка. Ширина первого аргумента должна быть равна высоте второго.");
            }
            Matrix result = new Matrix(matrix2.Width, matrix1.Height);
            for (int i = 0; i < matrix1.Height; i++)
            {
                for (int j = 0; j < matrix2.Width; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < matrix2.Height; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }

        private Matrix GetMatrixWithoutRowCulumn(int row, int column)
        {
            List<List<double>> table = new List<List<double>>(Height);
            for (int i = 0; i < Height; i++)
            {
                List<double> line = new List<double>(Width);
                for (int j = 0; j < Width; j++)
                {
                    if (j == column)
                    {
                        continue;
                    }
                    line.Add(this[i, j]);
                }
                if (i == row)
                {
                    continue;
                }
                table.Add(line);
            }
            return new Matrix(table);
        }

        public double CalcDeterminant()
        {
            return Determinant(this);
        }

        private static double Determinant(Matrix matrix)
        {
            if (matrix.Width != matrix.Height)
            {
                throw new ArgumentException("Ошибка. Детерминант может быть рассчитан только для квадратной матрицы.");
            }

            if (matrix.Height == 1)
            {
                return matrix[0, 0];
            }

            if (matrix.Height == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            int sign = 1;
            double sum = 0;
            for (int i = 0; i < matrix.Height; i++)
            {
                sum += sign * matrix[i, 0] * Determinant(matrix.GetMatrixWithoutRowCulumn(i, 0));
                sign = -sign;
            }
            return sum;
        }

        public Vector MultiplicationToVector(Vector vector)
        {
            if (Width != vector.Size)
            {
                throw new ArgumentException("Ошибка. Неверная размерность вектора");
            }

            Vector result = new Vector(Height);

            for (int i = 0; i < Height; i++)
            {
                double sum = 0;
                for (int j = 0; j < Width; j++)
                {
                    sum += this[i, j] * vector[j];
                }
                result[i] = sum;
            }

            return result;
        }

        public Matrix Addition(Matrix matrix)
        {
            int minHeight = Math.Min(matrix.Height, Height);
            int maxHeight = Math.Max(matrix.Height, Height);
            int maxWidth = Math.Max(matrix.Width, Width);

            for (int i = 0; i < minHeight; i++)
            {
                this[i].Addition(matrix[i]);
            }

            if (Height >= matrix.Height)
            {
                for (int i = minHeight; i < maxHeight; i++)
                {
                    this[i].Addition(new Vector(maxWidth));
                }
                return this;
            }

            for (int i = minHeight; i < maxHeight; i++)
            {
                rows.Add(Vector.Addition(matrix[i], new Vector(maxWidth)));
            }
            return this;
        }

        public Matrix Subtraction(Matrix matrix)
        {
            int minHeight = Math.Min(matrix.Height, Height);
            int maxHeight = Math.Max(matrix.Height, Height);
            int maxWidth = Math.Max(matrix.Width, Width);

            for (int i = 0; i < minHeight; i++)
            {
                this[i].Subtraction(matrix[i]);
            }

            if (Height >= matrix.Height)
            {
                for (int i = minHeight; i < maxHeight; i++)
                {
                    this[i].Subtraction(new Vector(maxWidth));
                }
                return this;
            }

            for (int i = minHeight; i < maxHeight; i++)
            {
                rows.Add(Vector.Subtraction(matrix[i], new Vector(maxWidth)));
            }
            return this;
        }

        public static Matrix Addition(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix1Copy = new Matrix(matrix1);
            return matrix1Copy.Addition(matrix2);
        }

        public static Matrix Subtraction(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix1Copy = new Matrix(matrix1);
            return matrix1Copy.Addition(matrix2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                sb.Append(this[i])
                    .AppendLine();
            }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Matrix matrix = (Matrix)obj;
            if (matrix.Width != Width && matrix.Height != Height)
            {
                return false;
            }
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (!RealNumberUtils.IsRealEquals(matrix[i, j], this[i, j], Precision))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int startHash = 32;
            int hash = startHash;
            for (int i = 0; i < Height; i++)
            {
                hash ^= this[i].GetHashCode();
            }
            return hash;
        }

    }
}
