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

        public Matrix(int columnCount, int rowCount)
        {
            rows = new List<Vector>();
            for (int i = 0; i < rowCount; i++)
            {
                rows.Add(new Vector(columnCount));
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

        public int RowCount
        {
            get { return rows.Count; }
        }

        public int ColumnCount
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
                if (i >= RowCount)
                {
                    throw new ArgumentException("Ошибка. Индекс больше максимального предела.");
                }
                if (i < 0)
                {
                    throw new ArgumentException("Ошибка. Индекс меньше минимального предела.");
                }
                return new Vector(rows[i]);
            }
            set
            {
                if (i >= RowCount)
                {
                    throw new ArgumentException("Ошибка. Индекс больше максимального предела.");
                }
                if (i < 0)
                {
                    throw new ArgumentException("Ошибка. Индекс меньше минимального предела.");
                }
                int matrixColumnCount = ColumnCount;
                rows[i] = value;
                
                if (rows[i].Size > matrixColumnCount)
                {
                    for (int j = 0; j < rows.Count; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }
                        rows[j].Addition(new Vector(rows[i].Size));
                    }
                }
                else if (rows[i].Size < matrixColumnCount)
                {
                    rows[i].Addition(new Vector(matrixColumnCount));
                }
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i >= RowCount)
                {
                    throw new ArgumentException("Ошибка. Индекс строки больше максимального предела.");
                }
                if (i < 0)
                {
                    throw new ArgumentException("Ошибка. Индекс строки меньше минимального предела.");
                }

                if (j >= ColumnCount)
                {
                    throw new ArgumentException("Ошибка. Индекс столбца больше максимального предела.");
                }
                if (j < 0)
                {
                    throw new ArgumentException("Ошибка. Индекс столбца меньше минимального предела.");
                }
                return rows[i][j];
            }
            set
            {
                if (i >= RowCount)
                {
                    throw new ArgumentException("Ошибка. Индекс строки больше максимального предела.");
                }
                if (i < 0)
                {
                    throw new ArgumentException("Ошибка. Индекс строки меньше минимального предела.");
                }

                if (j >= ColumnCount)
                {
                    throw new ArgumentException("Ошибка. Индекс столбца больше максимального предела.");
                }
                if (j < 0)
                {
                    throw new ArgumentException("Ошибка. Индекс столбца меньше минимального предела.");
                }
                rows[i][j] = value;
            }
        }

        public Vector GetColumn(int index)
        {
            if (index >= ColumnCount)
            {
                throw new ArgumentException("Ошибка. Индекс больше максимального предела.");
            }
            if (index < 0)
            {
                throw new ArgumentException("Ошибка. Индекс меньше минимального предела.");
            }
            List<double> column = new List<double>();
            for (int i = 0; i < RowCount; i++)
            {
                column.Add(this[i, index]);
            }
            return new Vector(column);
        }

        public Matrix Transpose()
        {
            Matrix matrixCopy = new Matrix(this);
            rows.Clear();
            for (int i = 0; i < matrixCopy.ColumnCount; i++)
            {
                rows.Add(matrixCopy.GetColumn(i));
            }
            return this;
        }

        public Matrix Multiplication(double scalar)
        {
            foreach (Vector line in rows)
            {
                line.Multiplication(scalar);
            }
            return this;
        }

        public static Matrix Multiplication(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.ColumnCount != matrix2.RowCount)
            {
                throw new ArgumentException("Ошибка. Число столбцов первого аргумента должна быть равно числу строк второго.");
            }
            Matrix result = new Matrix(matrix2.ColumnCount, matrix1.RowCount);
            for (int i = 0; i < matrix1.RowCount; i++)
            {
                for (int j = 0; j < matrix2.ColumnCount; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < matrix2.RowCount; k++)
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
            List<List<double>> table = new List<List<double>>(RowCount);
            for (int i = 0; i < RowCount; i++)
            {
                List<double> line = new List<double>(ColumnCount);
                for (int j = 0; j < ColumnCount; j++)
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
            if (matrix.ColumnCount != matrix.RowCount)
            {
                throw new ArgumentException("Ошибка. Детерминант может быть рассчитан только для квадратной матрицы.");
            }

            if (matrix.RowCount == 1)
            {
                return matrix[0, 0];
            }

            if (matrix.RowCount == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            int sign = 1;
            double sum = 0;
            for (int i = 0; i < matrix.RowCount; i++)
            {
                sum += sign * matrix[i, 0] * Determinant(matrix.GetMatrixWithoutRowCulumn(i, 0));
                sign = -sign;
            }
            return sum;
        }

        public Vector Multiplication(Vector vector)
        {
            if (ColumnCount != vector.Size)
            {
                throw new ArgumentException("Ошибка. Неверная размерность вектора. Длина вектора должна быть равна числу" +
                                            "столбцов матрицы");
            }

            Vector result = new Vector(RowCount);

            for (int i = 0; i < RowCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < ColumnCount; j++)
                {
                    sum += this[i, j] * vector[j];
                }
                result[i] = sum;
            }

            return result;
        }

        public Matrix Addition(Matrix matrix)
        {
            int minRowCount = Math.Min(matrix.RowCount, RowCount);
            int maxRowCount = Math.Max(matrix.RowCount, RowCount);
            int maxColumnCount = Math.Max(matrix.ColumnCount, ColumnCount);
            

            for (int i = 0; i < minRowCount; i++)
            {
                this[i] = this[i].Addition(matrix[i]);
            }

            if (RowCount >= matrix.RowCount)
            {
                for (int i = minRowCount; i < maxRowCount; i++)
                {
                    this[i] = this[i].Addition(new Vector(maxColumnCount));
                }
                return this;
            }

            for (int i = minRowCount; i < maxRowCount; i++)
            {
                rows.Add(Vector.Addition(matrix[i], new Vector(maxColumnCount)));
            }
            return this;
        }

        public Matrix Subtraction(Matrix matrix)
        {
            int minRowCount = Math.Min(matrix.RowCount, RowCount);
            int maxRowCount = Math.Max(matrix.RowCount, RowCount);
            int maxColumnCount = Math.Max(matrix.ColumnCount, ColumnCount);

            for (int i = 0; i < minRowCount; i++)
            {
                this[i] = this[i].Subtraction(matrix[i]);
            }

            if (RowCount >= matrix.RowCount)
            {
                for (int i = minRowCount; i < maxRowCount; i++)
                {
                    this[i] = this[i].Subtraction(new Vector(maxColumnCount));
                }
                return this;
            }

            for (int i = minRowCount; i < maxRowCount; i++)
            {
                rows.Add(Vector.Subtraction(new Vector(maxColumnCount), matrix[i]));
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
            return matrix1Copy.Subtraction(matrix2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < RowCount; i++)
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
            if (ReferenceEquals(this, matrix))
            {
                return true;
            }
            if (matrix.ColumnCount != ColumnCount || matrix.RowCount != RowCount)
            {
                return false;
            }
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
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
            for (int i = 0; i < RowCount; i++)
            {
                hash ^= this[i].GetHashCode();
            }
            return hash;
        }

    }
}
