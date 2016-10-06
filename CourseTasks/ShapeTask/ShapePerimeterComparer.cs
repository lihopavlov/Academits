using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask
{
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
}
