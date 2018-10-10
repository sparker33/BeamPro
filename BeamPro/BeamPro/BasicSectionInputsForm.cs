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
    public partial class BasicSectionInputsForm : ElementInputsForm
    {
        // Private objects
        private static int BasicSectionCount = 1;
        private const string ElementType = "Basic Section ";

        // Public accessors
        //reserved

        // Class creation method
        public BasicSectionInputsForm() : base(ElementType + (BasicSectionCount++).ToString(), new BasicSectionInputsControl())
        {
            InitializeComponent();
        }

        // Method to apply the worksheet to a beam model
        public override void ApplyWorksheet(ref List<IElement> elementList, ref List<INode> nodeList)
        {
            if (nodeList.Count == 0)
            {
                nodeList.Add(new Basic2DNode(
                    (IEnumerable<double>)new double[] { 0.0d, 0.0d, 0.0d },
                    (IEnumerable<double>)new double[] { 0.0d, 0.0d, 0.0d }));
                nodeList[0].FixedDOF[0] = false;
                nodeList[0].FixedDOF[1] = false;
                nodeList[0].FixedDOF[2] = false;
            }

            BasicSectionInputsControl ip0 = elementInputsControl as BasicSectionInputsControl;
            double subElementLength = ip0.Length / (double)ip0.Subsections;
            double[] endNodeLocation = new double[3];
            nodeList[nodeList.Count - 1].Location[2] += ip0.RelAngle;
            for (int i = 0; i < ip0.Subsections; i++)
            {
                endNodeLocation[0] = nodeList[nodeList.Count - 1].Location[0]
                    + subElementLength * Math.Cos(nodeList[nodeList.Count - 1].Location[2]);
                endNodeLocation[1] = nodeList[nodeList.Count - 1].Location[1]
                    + subElementLength * Math.Sin(nodeList[nodeList.Count - 1].Location[2]);
                endNodeLocation[2] = nodeList[nodeList.Count - 1].Location[2];
                nodeList.Add(new Basic2DNode((IEnumerable<double>)endNodeLocation,
                    (IEnumerable<double>)new double[] { 0.0d, 0.0d, 0.0d }));
                elementList.Add(new BasicSectionElement(nodeList[nodeList.Count - 2],
                    nodeList[nodeList.Count - 1], ip0));
            }
        }
    }
}
