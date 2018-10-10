using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamPro
{
    public interface IElement
    {
        double L { get; }
        double E { get; }
        double A { get; }
        double I { get; }
        double C { get; }
        IList<INode> Nodes { get; set; } // must always be Count = 2
        SquareMatrix TransformMatrix { get; } // gets the transform matrix for this element's stiffness

        SquareMatrix GetStiffness(int nodeIndex); // index can be [0,3]; returned SquareMatrix has Order = 3

        double GetInternalMoment();
    }
}
