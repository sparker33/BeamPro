using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeamPro
{
    public partial class BasicConditionInputsForm : ElementInputsForm
    {
        // Private objects
        private static int BasicConditionCount = 1;
        private const string ElementType = "Basic Condition ";

        // Public accessors
        //reserved

        // Class creation method
        public BasicConditionInputsForm() : base(ElementType + (BasicConditionCount++).ToString(), new BasicConditionInputsControl())
        {
            InitializeComponent();
        }

        // Method to apply the worksheet to a beam model
        public override void ApplyWorksheet(ref List<IElement> elementList, ref List<INode> nodeList)
        {
            BasicConditionInputsControl ip1 = elementInputsControl as BasicConditionInputsControl;
            if (nodeList.Count == 0)
            {
                nodeList.Add(new Basic2DNode(
                    (IEnumerable<double>)new double[] { 0.0d, 0.0d, 0.0d },
                    (IEnumerable<double>)new double[] { ip1.P, ip1.V, ip1.M }));
                nodeList[0].FixedDOF[0] = ip1.Xdof;
                nodeList[0].FixedDOF[1] = ip1.Ydof;
                nodeList[0].FixedDOF[2] = ip1.Qdof;
            }
            else
            {
                nodeList[nodeList.Count - 1].Force[0] += ip1.P;
                nodeList[nodeList.Count - 1].Force[1] += ip1.V;
                nodeList[nodeList.Count - 1].Force[2] += ip1.M;
                nodeList[nodeList.Count - 1].FixedDOF[0]
                    = (ip1.Xdof || nodeList[nodeList.Count - 1].FixedDOF[0]);
                nodeList[nodeList.Count - 1].FixedDOF[1]
                    = (ip1.Ydof || nodeList[nodeList.Count - 1].FixedDOF[1]);
                nodeList[nodeList.Count - 1].FixedDOF[2]
                    = (ip1.Qdof || nodeList[nodeList.Count - 1].FixedDOF[2]);
            }
        }
    }
}
