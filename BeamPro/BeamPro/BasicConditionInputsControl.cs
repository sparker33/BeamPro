using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeamPro
{
    public partial class BasicConditionInputsControl : ElementInputsControl
    {
        // Public accessors to input boxes
        public double P
        {
            get
            {
                try
                {
                    return Double.Parse(xForceBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("X-Force input must be a number.");
                }
            }
        }
        public double V
        {
            get
            {
                try
                {
                    return Double.Parse(yForceBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Y-Force input must be a number.");
                }
            }
        }
        public double M
        {
            get
            {
                try
                {
                    return Double.Parse(qMomentBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Z-Moment input must be a number.");
                }
            }
        }
        public bool Xdof
        {
            get
            {
                switch (xDofBox.SelectedIndex)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    default:
                        throw new Exception("Invalid X degrees of freedom dropdown entry");
                }
            }
        }
        public bool Ydof
        {
            get
            {
                switch (yDofBox.SelectedIndex)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    default:
                        throw new Exception("Invalid Y degrees of freedom dropdown entry");
                }
            }
        }
        public bool Qdof
        {
            get
            {
                switch (qDofBox.SelectedIndex)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    default:
                        throw new Exception("Invalid Q degrees of freedom dropdown entry");
                }
            }
        }

        // Default class constructor
        public BasicConditionInputsControl() : base()
        {
            InitializeComponent();
            xDofBox.Items.AddRange(new object[] {"Free",
                "Fixed"});
            xDofBox.SelectedIndex = 0;
            yDofBox.Items.AddRange(new object[] {"Free",
                "Fixed"});
            yDofBox.SelectedIndex = 0;
            qDofBox.Items.AddRange(new object[] {"Free",
                "Fixed"});
            qDofBox.SelectedIndex = 0;
        }

        // Method to provide enumerable of parameters needed to describe this element
        public override IEnumerable<string> GetSaveParams()
        {
            string[] saveParams = { xForceBox.Text,
                                    yForceBox.Text,
                                    qMomentBox.Text,
                                    xDofBox.SelectedIndex.ToString(),
                                    yDofBox.SelectedIndex.ToString(),
                                    qDofBox.SelectedIndex.ToString() };
            return saveParams;
        }

        // Method to retreive enumerable of parameters needed to describe this element
        public override void LoadParams(IEnumerable<string> values)
        {
            IEnumerator<string> valuesEnumerator = values.GetEnumerator();
            valuesEnumerator.MoveNext();
            xForceBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            yForceBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            qMomentBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            xDofBox.SelectedIndex = Int32.Parse(valuesEnumerator.Current);

            valuesEnumerator.MoveNext();
            yDofBox.SelectedIndex = Int32.Parse(valuesEnumerator.Current);

            valuesEnumerator.MoveNext();
            qDofBox.SelectedIndex = Int32.Parse(valuesEnumerator.Current);
        }
    }
}
