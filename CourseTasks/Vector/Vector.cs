using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTask.Utils;

namespace Vector
{
    public class Vector
    {
        private List<double> coordinates;
        private const double precision = 0.0001;

        public Vector(int size)
        {
            coordinates = new List<double>();
            for (int i = 0; i < size; i++)
            {
                coordinates.Add(0.0);
            }
        }

        public Vector(Vector vector) : this(vector.coordinates)
        {
        }

        public Vector(List<double> coordinates) 
        {
            this.coordinates = new List<double>(coordinates);
        }

        public Vector(int size, List<double> coordinates) : this(coordinates)
        {
            if (size <= 0)
            {
                throw new ArgumentException();
            }
            for (int i = coordinates.Count; i < size; i++)
            {
                this.coordinates.Add(0.0);
            }
        }

        public int Size
        {
            get
            {
                return coordinates.Count;
            }
        }

        public Vector Addition(Vector vector)
        {
            int minSize = Math.Min(Size, vector.Size);
            int maxSize = Math.Max(Size, vector.Size);
            for (int i = 0; i < minSize; i++)
            {
                this[i] += vector[i];
            }
            if (Size > vector.Size)
            {
                return this;
            }
            for (int i = minSize; i < maxSize; i++)
            {
                coordinates.Add(vector[i]);
            }
            return this;
        }

        public Vector Subtraction(Vector vector)
        {
            int minSize = Math.Min(Size, vector.Size);
            int maxSize = Math.Max(Size, vector.Size);
            for (int i = 0; i < minSize; i++)
            {
                this[i] -= vector[i];
            }
            if (Size > vector.Size)
            {
                return this;
            }
            for (int i = minSize; i < maxSize; i++)
            {
                coordinates.Add(0.0 - vector[i]);
            }
            return this;
        }

        public Vector Multiplication(double scalar)
        {
            for (int i = 0; i < Size; i++)
            {
                this[i] *= scalar;
            }
            return this;
        }

        public Vector Spread()
        {
            return Multiplication(-1.0);
        }

        public double VectorLength
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < Size; i++)
                {
                    sum += Math.Pow(coordinates[i], 2);
                }
                return Math.Sqrt(sum);
            }
        }

        public double this[int i]
        {
            get
            {
                if (i < 0 || i >= Size)
                {
                    throw new IndexOutOfRangeException();
                }
                return coordinates[i];
            }
            set
            {
                if (i < 0 || i >= Size)
                {
                    throw new IndexOutOfRangeException();
                }
                coordinates[i] = value;
            }
        }

        public static Vector Addition(Vector vector1, Vector vector2)
        {
            Vector vector1Copy = new Vector(vector1);
            return new Vector(vector1Copy.Addition(vector2));
        }

        public static Vector Subtraction(Vector vector1, Vector vector2)
        {
            Vector vector1Copy = new Vector(vector1);
            return new Vector(vector1Copy.Subtraction(vector2));
        }

        public static double Multiplication(Vector vector1, Vector vector2)
        {
            double result = 0;
            double minSize = Math.Min(vector1.Size, vector2.Size);
            for (int i = 0; i < minSize; i++)
            {
                result += vector1[i] * vector2[i];
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Vector vector = (Vector)obj;
            if (vector.Size != Size)
            {
                return false;
            }
            for (int i = 0; i < Size; i++)
            {
                if (!RealNumberUtils.IsRealEquals(vector[i], this[i], precision))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int startHash = 28;
            int hash = startHash;
            for (int i = 0; i < Size; i++)
            {
                hash ^= this[i].GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            for (int i = 0; i < Size; i++)
            {
                sb.Append(this[i])
                  .Append(", ");
            }
            const int endForRemoveLength = 1;
            sb.Remove(sb.Length - endForRemoveLength - 1, endForRemoveLength);
            sb.Append("}");
            return sb.ToString();
        }
    }
}
