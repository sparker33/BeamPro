using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamPro
{
    public class Basic2DNode : INode
    {
        // Private properties
        private double[] _location = new double[3];
        private double[] _force = new double[3];
        private bool[] _dof = new bool[] { false, false, false };

        // Public fields
        public double[] Location { get { return _location; } set { _location = value; } }
        public double[] Force { get { return _force; } set { _force = value; } }
        public bool[] FixedDOF { get { return _dof; } set { _dof = value; } } // 0: free, 1: fixed; length = 3: x, y, theta

        // Class creation method
        public Basic2DNode(IEnumerable<double> location, IEnumerable<double> force)
        {
            try
            {
                IEnumerator<double> locationEnumerator = location.GetEnumerator();
                for (int i = 0; locationEnumerator.MoveNext(); i++)
                {
                    _location[i] = locationEnumerator.Current;
                }
                IEnumerator<double> forceEnumerator = force.GetEnumerator();
                for (int i = 0; forceEnumerator.MoveNext(); i++)
                {
                    _force[i] = forceEnumerator.Current;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
