using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamPro
{
    public class SquareMatrix
    {
        // Private fields
        private int _order;
        private double[][] _matrix; // _matrix[rowIndex][columnIndex]

        // Public properties
        public int Order { get { return _order; } }
        public double[] this[int index1]
        {
            get
            {
                return _matrix[index1];
            }
            set
            {
                _matrix[index1] = value;
            }
        }
        public double this[int index1, int index2]
        {
            get
            {
                return _matrix[index1][index2];
            }
            set
            {
                _matrix[index1][index2] = value;
            }
        }
        public SquareMatrix Transpose
        {
            get
            {
                SquareMatrix transpose = new SquareMatrix(_order);
                for (int i = 0; i < _order; i++)
                {
                    for (int j = 0; j < _order; j++)
                    {
                        transpose[i][j] = _matrix[j][i];
                    }
                }
                return transpose;
            }
        }

        // Class creation method for empty matrix of specified order
        public SquareMatrix(int order)
        {
            _order = order;
            _matrix = new double[_order][];
            for (int i = 0; i < _order; i++)
            {
                _matrix[i] = new double[_order];
            }
        }

        // Class creation method for matrix from square IEnumerable<double>
        public SquareMatrix(IEnumerable<IEnumerable<double>> matrix)
        {
            IEnumerator<IEnumerable<double>> matrixRowsEnumerator = matrix.GetEnumerator();
            if (!matrixRowsEnumerator.MoveNext())
            {
                throw new Exception("SquareMatrix cannot be created from empty IEnumerable");
            }
            if (matrix.Count() != matrixRowsEnumerator.Current.Count())
            {
                throw new Exception(String.Format("SquareMatrix(IEnumerable<IEnumerable<double>> matrix) " +
                    "input is not square."));
            }

            _order = matrix.Count();
            _matrix = new double[_order][];
            for (int i = 0; i < _order; i++)
            {
                IEnumerator<double> matrixElementsEnumerator = matrixRowsEnumerator.Current.GetEnumerator();
                _matrix[i] = new double[_order];
                for (int j = 0; j < _order; j++)
                {
                    matrixElementsEnumerator.MoveNext();
                    _matrix[i][j] = matrixElementsEnumerator.Current;
                }
                matrixRowsEnumerator.MoveNext();
            }
        }

        // Method to conduct product of this _matrix with a vector
        // returns vector result
        public IEnumerable<double> VectorProduct(IEnumerable<double> vector)
        {
            if (vector.Count() != _order)
            {
                throw new Exception(String.Format("VectorProduct input is of incorrect size." +
                    "\r\n" + "Is: {0}" + "\r\n" + "Should be: {1}",
                    new string[] { vector.Count().ToString(), _order.ToString() }));
            }

            double[] solution = new double[_order];
            IEnumerator<double> vectorEnumerator = vector.GetEnumerator();
            for (int i = 0; i < _order; i++)
            {
                solution[i] = 0.0d;
                for (int j = 0; j < _order; j++)
                {
                    vectorEnumerator.MoveNext();
                    solution[i] += _matrix[i][j] * vectorEnumerator.Current;
                }
                vectorEnumerator.Reset();
            }
            return solution;
        }

        // Method to conduct product of this _matrix (left) with an equally sized
        // SquareMatrix (right) returns SquareMatrix result
        public SquareMatrix MatrixProduct(SquareMatrix rightMatrix)
        {
            if (rightMatrix.Order != _order)
            {
                throw new Exception(String.Format("MatrixProduct input is of incorrect size." +
                    "\r\n" + "Is: {0}" + "\r\n" + "Should be: {1}",
                    new string[] { rightMatrix.Order.ToString(), _order.ToString() }));
            }

            SquareMatrix solution = new SquareMatrix(_order);
            for (int i = 0; i < _order; i++)
            {
                for (int j = 0; j < _order; j++)
                {
                    solution[j][i] = 0.0d;
                    for (int k = 0; k < _order; k++)
                    {
                        solution[j][i] += _matrix[j][k] * rightMatrix[k][i];
                    }
                }
            }
            return solution;
        }

        // Method to solve the solution for an input vector problem
        public IEnumerable<double> SolveFor(IEnumerable<double> vector)
        {
            if (vector.Count() != _order)
            {
                throw new Exception(String.Format("SolveFor input is of incorrect size." +
                    "\r\n" + "Is: {0}" + "\r\n" + "Should be: {1}",
                    new string[] { vector.Count().ToString(), _order.ToString() }));
            }

            double[] solution = new double[_order];
            double[][] choleskyDecomp = new double[_order][];
            for (int i = 0; i < _order; i++)
            {
                choleskyDecomp[i] = new double[i + 1];
            }
            choleskyDecomp = CholeskyDecomp();
            try
            {
                IEnumerator<double> vectorEnumerator = vector.GetEnumerator();
                for (int i = 0; i < _order; i++)
                {
                    vectorEnumerator.MoveNext();
                    solution[i] = vectorEnumerator.Current;
                    for (int j = 0; j < i; j++)
                    {
                        solution[i] -= choleskyDecomp[i][j] * solution[j];
                    }
                    solution[i] /= choleskyDecomp[i][i];
                }
                for (int i = _order - 1; i > -1; i--)
                {
                    for (int j = i + 1; j < _order; j++)
                    {
                        solution[i] -= choleskyDecomp[j][i] * solution[j];
                    }
                    solution[i] /= choleskyDecomp[i][i];
                }
            }
            catch (System.DivideByZeroException)
            {
                throw new DivideByZeroException("Division by zero in SolveFor()");
            }
            return solution;
        }

        // Method to determine Cholesky decomposition of this _matrix
        // Returned matrix is Lower Triangular (jagged array);
        // Upper Triangular corresponding component is the (conjugate) transpose
        private double[][] CholeskyDecomp()
        {
            double[][] decomp = new double[_order][];
            for (int i = 0; i < _order; i++)
            {
                decomp[i] = new double[i + 1];
            }
            try
            {
                for (int i = 0; i < _order; i++)
                {
                    decomp[i][i] = _matrix[i][i];
                    for (int k = 0; k < i; k++)
                    {
                        decomp[i][i] -= decomp[i][k] * decomp[i][k];
                    }
                    decomp[i][i] = Math.Sqrt(decomp[i][i]);
                    for (int j = i + 1; j < _order; j++)
                    {
                        decomp[j][i] = _matrix[i][j];
                        for (int k = 0; k < i; k++)
                        {
                            decomp[j][i] -= decomp[i][k] * decomp[j][k];
                        }
                        decomp[j][i] /= decomp[i][i];
                    }
                }
            }
            catch (System.DivideByZeroException)
            {
                throw new DivideByZeroException("Division by zero in CholeskyDecomp()");
            }
            return decomp;
        }
    }
}
