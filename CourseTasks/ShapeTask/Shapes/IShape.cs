using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTask.Shapes
{
    interface IShape
    {
        double Width
        {
            get;
        }

        double Height
        {
            get;
        }

        double Area
        {
            get;
        }

        double Perimeter
        {
            get;
        }
    }
}
