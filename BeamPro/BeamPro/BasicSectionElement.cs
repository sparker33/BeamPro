using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamPro
{
    class BasicSectionElement : IElement
    {
        // Private fields
        private double _E;
        private double _A;
        private double _I;
        private double _C;
        private List<INode> _nodes = new List<INode>();
        private List<SquareMatrix> _stiffness = new List<SquareMatrix>();

        // Public Properties
        public double L
        {
            get
            {
                return (Math.Sqrt((_nodes[1].Location[0] - _nodes[0].Location[0]) * (_nodes[1].Location[0] - _nodes[0].Location[0])
                    + (_nodes[1].Location[1] - _nodes[0].Location[1]) * (_nodes[1].Location[1] - _nodes[0].Location[1])));
            }
        }
        public double E { get { return _E; } }
        public double A { get { return _A; } }
        public double I { get { return _I; } }
        public double C { get { return _C; } }
        public IList<INode> Nodes { get { return _nodes; } set { _nodes = (List<INode>)value; } }
        public SquareMatrix TransformMatrix
        {
            get
            {
                SquareMatrix transformMatrix = new SquareMatrix(3);
                double theta = Math.Atan((_nodes[1].Location[1] - _nodes[0].Location[1]) / (_nodes[1].Location[0] - _nodes[0].Location[0]));
                double cos = Math.Cos(theta);
                double sin = Math.Sin(theta);
                transformMatrix[0][0] = cos;
                transformMatrix[0][1] = -sin;
                transformMatrix[0][2] = 0.0d;
                transformMatrix[1][0] = sin;
                transformMatrix[1][1] = cos;
                transformMatrix[1][2] = 0.0d;
                transformMatrix[2][0] = 0.0d;
                transformMatrix[2][1] = 0.0d;
                transformMatrix[2][2] = 1.0d;
                return transformMatrix;
            }
        }

        // Methods for IElement
        public SquareMatrix GetStiffness(int index)
        {
            return _stiffness[index];
		}

		/// <summary>
		/// Class creation method from BasicSectinInputsControl
		/// </summary>
		/// <param name="startNode"> Left node terminus. </param>
		/// <param name="endNode"> Right node terminus. </param>
		/// <param name="ip"> InputsControl with section properties. </param>
		public BasicSectionElement(INode startNode, INode endNode, BasicSectionInputsControl ip)
		{
			// should consolidate all inputs into a single BasicSectionInputsPanel class
			_nodes.Add(startNode);
			_nodes.Add(endNode);
			_E = ip.Modulus;
			_A = ip.Area;
			_I = ip.Inertia;
			_C = ip.MaxFiberDistance;
			double l = L;
			for (int i = 0; i < 4; i++)
			{
				_stiffness.Add(new SquareMatrix(3));

				_stiffness.Last()[0][0] = _A * _E / l;
				_stiffness.Last()[0][1] = 0.0d;
				_stiffness.Last()[0][2] = 0.0d;
				_stiffness.Last()[1][0] = 0.0d;
				_stiffness.Last()[2][0] = 0.0d;
				double lcf = 2.0d * _E * _I / l;
				_stiffness.Last()[1][1] = lcf * 6.0d / (l * l);
				_stiffness.Last()[1][2] = lcf * 3.0d / l;
				_stiffness.Last()[2][1] = _stiffness.Last()[1][2];
				_stiffness.Last()[2][2] = lcf * 2.0d;
			}
			_stiffness[1][0][0] *= -1.0d;
			_stiffness[1][1][1] *= -1.0d;
			_stiffness[1][2][1] *= -1.0d;
			_stiffness[1][2][2] *= 0.5d;
			_stiffness[2][0][0] *= -1.0d;
			_stiffness[2][1][1] *= -1.0d;
			_stiffness[2][1][2] *= -1.0d;
			_stiffness[2][2][2] *= 0.5d;
			_stiffness[3][1][2] *= -1.0d;
			_stiffness[3][2][1] *= -1.0d;
		}

		/// <summary>
		/// Class constructor from manually enumerated inputs
		/// </summary>
		/// <param name="startNode"> Left node terminus. </param>
		/// <param name="endNode"> Right node terminus. </param>
		/// <param name="modulus"> Material modulus. </param>
		/// <param name="area"> Cross-sectional area. </param>
		/// <param name="inertia"> Inertia (second moment of area). </param>
		/// <param name="maxFiber"> Extreme fiber distance from neutral axis. </param>
		public BasicSectionElement(INode startNode, INode endNode, double modulus, double area, double inertia, double maxFiber)
		{
			// should consolidate all inputs into a single BasicSectionInputsPanel class
			_nodes.Add(startNode);
			_nodes.Add(endNode);
			_E = modulus;
			_A = area;
			_I = inertia;
			_C = maxFiber;
			double l = L;
			for (int i = 0; i < 4; i++)
			{
				_stiffness.Add(new SquareMatrix(3));

				_stiffness.Last()[0][0] = _A * _E / l;
				_stiffness.Last()[0][1] = 0.0d;
				_stiffness.Last()[0][2] = 0.0d;
				_stiffness.Last()[1][0] = 0.0d;
				_stiffness.Last()[2][0] = 0.0d;
				double lcf = 2.0d * _E * _I / l;
				_stiffness.Last()[1][1] = lcf * 6.0d / (l * l);
				_stiffness.Last()[1][2] = lcf * 3.0d / l;
				_stiffness.Last()[2][1] = _stiffness.Last()[1][2];
				_stiffness.Last()[2][2] = lcf * 2.0d;
			}
			_stiffness[1][0][0] *= -1.0d;
			_stiffness[1][1][1] *= -1.0d;
			_stiffness[1][2][1] *= -1.0d;
			_stiffness[1][2][2] *= 0.5d;
			_stiffness[2][0][0] *= -1.0d;
			_stiffness[2][1][1] *= -1.0d;
			_stiffness[2][1][2] *= -1.0d;
			_stiffness[2][2][2] *= 0.5d;
			_stiffness[3][1][2] *= -1.0d;
			_stiffness[3][2][1] *= -1.0d;
		}

		// Method to determine element internal moment
		public double GetInternalMoment()
        {
            double moment = -_E * _I * (_nodes[1].Location[2] - _nodes[0].Location[2])
                / (_nodes[1].Location[0] - _nodes[0].Location[0]);
            return moment;
        }
    }
}
