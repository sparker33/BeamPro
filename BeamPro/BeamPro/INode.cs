using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamPro
{
    public interface INode
    {
        double[] Location { get; set; }  // 0: x, 1: y, 2: theta
        double[] Force { get; set; } // 0: x-force (P), 1: y-force (V), 2: z-torque (M)
        bool[] FixedDOF { get; set; } // 0: free, 1: fixed; length = 3: x, y, theta
    }
}
