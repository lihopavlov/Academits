using ShapeTask.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask.Comparers
{
    class ShapeAreaComparer : IComparer<IShape>
    {
        public int Compare(IShape shape1, IShape shape2)
        {
            if (shape1.Area > shape2.Area)
            {
                return 1;
            }
            if (shape1.Area < shape2.Area)
            {
                return -1;
            }
            return 0;
        }
    }
}
