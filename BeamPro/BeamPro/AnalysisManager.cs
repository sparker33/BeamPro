using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // StreamWriter, StreamReader
using System.ComponentModel;	// BackgroundWorker

namespace BeamPro
{
    public class AnalysisManager
    {
        // Private fields
        private List<INode> _nodes = new List<INode>();
        private List<IElement> _elements = new List<IElement>();
        private List<double?> _displacements = new List<double?>();
        private List<double?> _forces = new List<double?>();
        private List<double> _stresses = new List<double>();
        private List<List<double>> _globalStiffness = new List<List<double>>();
        private double _error = Double.MaxValue;

        // Public properties
        public const double AllowableError = 0.00005d;
        public double Error { get { return _error; } }
        public List<INode> Nodes { get { return _nodes; } }
        public List<double?> Displacements { get { return _displacements; } }
        public List<double?> Forces { get { return _forces; } }
        public List<double> Stresses { get { return _stresses; } }

        // Class creation method
        public AnalysisManager(ObjectHolder holder)
        {
            _nodes.Clear();
            _elements.Clear();
            foreach (ElementInputsForm worksheet in holder.BeamWorksheets)
            {
                worksheet.ApplyWorksheet(ref _elements, ref _nodes);
            }
            foreach (INode node in _nodes)
            {
                foreach (bool dof in node.FixedDOF)
                {
                    _forces.Add(null);
                    _displacements.Add(null);
                }
            }
        }

        // Method to conduct an analysis with the current nodes and displacements
        public void RunAnalysis(BackgroundWorker worker)
        {
            int dofIndex;
            int loadSteps = 1;
            List<double> prevDisplacementsSum = new List<double>();
            List<double> displacementsSum = new List<double>();
            List<double> tempForces = new List<double>();
            List<double> forcesSum = new List<double>();
            for (int i = 0; i < _displacements.Count; i++)
            {
                prevDisplacementsSum.Add(0.0d);
                displacementsSum.Add(0.0d);
                tempForces.Add(0.0d);
                forcesSum.Add(0.0d);
            }
            do
            {
                dofIndex = 0;
                foreach (INode node in _nodes)
                {
                    for (int i = 0; i < node.Location.Count(); i++)
                    {
                        node.Location[i] -= displacementsSum[dofIndex];
                        displacementsSum[dofIndex] = 0.0d;
                        forcesSum[dofIndex] = 0.0d;
                        dofIndex++;
                    }
                }
                for (int i = 0; i < loadSteps; i++)
                {
                    dofIndex = 0;
                    foreach (INode node in _nodes)
                    {
                        foreach (bool dof in node.FixedDOF)
                        {
                            if (dof) //degree is fixed, force unkown
                            {
                                _displacements[dofIndex] = 0.0d;
                                _forces[dofIndex] = null;
                            }
                            else //degreee is free, force specified
                            {
                                _displacements[dofIndex] = null;
                                _forces[dofIndex] = node.Force[dofIndex % node.FixedDOF.Count()]
                                    / (double)loadSteps;
                            }
                            dofIndex++;
                        }
                    }
                    GenerateStiffnessMatrix(_elements, _nodes);
                    SolveDisplacements();
                    tempForces = CalculateReactions();
                    IEnumerator<double?> displacementsEnumerator = _displacements.GetEnumerator();
                    IEnumerator<double> forcesEnumerator = tempForces.GetEnumerator();
                    dofIndex = 0;
                    foreach (INode node in _nodes)
                    {
                        for (int j = 0; j < node.Location.Count(); j++)
                        {
                            displacementsEnumerator.MoveNext();
                            node.Location[j] += displacementsEnumerator.Current.Value;
                            displacementsSum[dofIndex] += displacementsEnumerator.Current.Value;
                            forcesEnumerator.MoveNext();
                            forcesSum[dofIndex] += forcesEnumerator.Current;
                            dofIndex++;
                        }
                    }
                }
                if (loadSteps > 1)
                {
                    _error = 0.0;
                    for (int i = 0; i < displacementsSum.Count; i++)
                    {
                        _error += (prevDisplacementsSum[i] - displacementsSum[i])
                            * (prevDisplacementsSum[i] - displacementsSum[i]);
                        prevDisplacementsSum[i] = displacementsSum[i];
                    }
                    _error = Math.Sqrt(_error / displacementsSum.Count);
                }
                worker.ReportProgress(100 * (int)Math.Min(1.0d, AllowableError / _error));
                if (loadSteps > 5000)
                {
                    System.Windows.Forms.MessageBox.Show("Analysis has exceeded 5000 load steps.");
                }
                loadSteps *= 2;
            } while (_error > AllowableError);

            for (int i = 0; i < _displacements.Count; i++)
            {
                _displacements[i] = displacementsSum[i];
            }
            _forces[0] = forcesSum[0];
            _forces[1] = forcesSum[1];
            for (int i = 3; i < _forces.Count; i += 3)
            {
                _forces[i - 1] = _elements[i / 3 - 1].GetInternalMoment();
                _forces[i] = _forces[i - 3] + forcesSum[i];
                _forces[i + 1] = _forces[i - 2] + forcesSum[i + 1];
            }
            _forces[_forces.Count - 1] = _forces[_forces.Count - 4];
            for (int i = 0; i < _forces.Count - 3; i += 3)
            {
                _stresses.Add(_forces[i].Value / _elements[i / 3].A);
                _stresses.Add(_forces[i + 1].Value / _elements[i / 3].A);
                _stresses.Add(_forces[i + 2].Value * _elements[i / 3].C / _elements[i / 3].I);
            }
            _stresses.Add(_stresses[_stresses.Count - 3]);
            _stresses.Add(_stresses[_stresses.Count - 3]);
            _stresses.Add(_stresses[_stresses.Count - 3]);
        }

        // Method to generate the global stiffness matrix
        private void GenerateStiffnessMatrix(List<IElement> elements, List<INode> nodes)
        {
            SquareMatrix stiffnessMatrix = new SquareMatrix(_displacements.Count);
            foreach (IElement element in elements)
            {
                int[][] nodesOrders = new int[4][];
                nodesOrders[0] = new int[] { nodes.IndexOf(element.Nodes[0]), nodes.IndexOf(element.Nodes[0]) };
                nodesOrders[1] = new int[] { nodes.IndexOf(element.Nodes[0]), nodes.IndexOf(element.Nodes[1]) };
                nodesOrders[2] = new int[] { nodes.IndexOf(element.Nodes[1]), nodes.IndexOf(element.Nodes[0]) };
                nodesOrders[3] = new int[] { nodes.IndexOf(element.Nodes[1]), nodes.IndexOf(element.Nodes[1]) };
                SquareMatrix transformMatrix = element.TransformMatrix;
                SquareMatrix tempMatrix;
                for (int i = 0; i < 4; i++)
                {
                    tempMatrix = transformMatrix.MatrixProduct(element.GetStiffness(i).MatrixProduct(transformMatrix.Transpose));
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            stiffnessMatrix[3 * nodesOrders[i][0] + j][3 * nodesOrders[i][1] + k] += tempMatrix[j][k];
                        }
                    }
                }
            }
            _globalStiffness.Clear();
            for (int i = 0; i < _displacements.Count; i++)
            {
                _globalStiffness.Add(new List<double>());
                for (int j = 0; j < _displacements.Count; j++)
                {
                    _globalStiffness.Last().Add(stiffnessMatrix[i][j]);
                }
            }
        }

        // Method to solve for unknown forces and displacements
        private void SolveDisplacements()
        {
            List<int> knownForcesOrderKey = new List<int>();
            List<double> knownForces = new List<double>();
            List<List<double>> dispSolveStiffnesses = new List<List<double>>();
            for (int i = 0; i < _displacements.Count; i++)
            {
                if (_forces[i].HasValue)
                {
                    knownForces.Add(_forces[i].Value);
                    knownForcesOrderKey.Add(i);
                    dispSolveStiffnesses.Add(new List<double>());
                    for (int j = 0; j < knownForcesOrderKey.Count - 1; j++)
                    {
                        dispSolveStiffnesses[j].Add(_globalStiffness[knownForcesOrderKey[j]][i]);
                    }
                    for (int j = 0; j < knownForcesOrderKey.Count; j++)
                    {
                        dispSolveStiffnesses.Last().Add(_globalStiffness[i][knownForcesOrderKey[j]]);
                    }
                    for (int j = 0; j < _displacements.Count; j++)
                    {
                        if (_displacements[j].HasValue)
                        {
                            knownForces[knownForces.Count - 1] -= _globalStiffness[i][j] * _displacements[j].Value;
                        }
                    }
                }
            }
            SquareMatrix stiffnessMatrix = new SquareMatrix(dispSolveStiffnesses);
            List<double> solvedDisplacements = new List<double>(stiffnessMatrix.SolveFor(knownForces));
            for (int i = 0; i < solvedDisplacements.Count; i++)
            {
                _displacements[knownForcesOrderKey[i]] = solvedDisplacements[i];
            }
        }

        // Method to solve for unknown forces and displacements
        private List<double> CalculateReactions()
        {
            SquareMatrix stiffnessMatrix = new SquareMatrix(_globalStiffness);
            List<double> nonNullableDisplacements = new List<double>();
            foreach (double? displacement in _displacements)
            {
                nonNullableDisplacements.Add(displacement.Value);
            }
            List<double> reactions = new List<double>(stiffnessMatrix.VectorProduct((IEnumerable<double>)nonNullableDisplacements));
            return reactions;
        }
    }
}
