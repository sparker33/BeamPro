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
    public partial class BasicSectionInputsControl : ElementInputsControl
    {
        // Public accessors to input boxes
        public double Modulus
        {
            get
            {
                try
                {
                    return Double.Parse(modulusBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Modulus input must be a positive number.");
                }
            }
        }
        public double MaxFiberDistance
        {
            get
            {
                try
                {
                    return Double.Parse(maxFiberBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Max Fiber Distance input must be a positive number.");
                }
            }
        }
        public double Area
        {
            get
            {
                try
                {
                    return Double.Parse(areaBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Area input must be a positive number.");
                }
            }
        }
        public double Inertia
        {
            get
            {
                try
                {
                    return Double.Parse(inertiaBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Inertia input must be a positive number.");
                }
            }
        }
        public double Length
        {
            get
            {
                try
                {
                    return Double.Parse(lengthBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Length input must be a positive number.");
                }
            }
        }
        public int Subsections
        {
            get
            {
                try
                {
                    return Int32.Parse(subSectionsBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Subsections input must be a positive integer.");
                }
            }
        }
        public double RelAngle
        {
            get
            {
                try
                {
                    return Double.Parse(angleBox.Text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Relative Angle input must be a positive integer.");
                }
            }
        }

        // Default class constructor
        public BasicSectionInputsControl() : base()
        {
            InitializeComponent();
        }

        // Method to provide enumerable of parameters needed to describe this element
        public override IEnumerable<string> GetSaveParams()
        {
            string[] saveParams = { modulusBox.Text,
                                    maxFiberBox.Text,
                                    areaBox.Text,
                                    inertiaBox.Text,
                                    lengthBox.Text,
                                    subSectionsBox.Text,
                                    angleBox.Text };
            return saveParams;
        }

        // Method to retreive enumerable of parameters needed to describe this element
        public override void LoadParams(IEnumerable<string> values)
        {
            IEnumerator<string> valuesEnumerator = values.GetEnumerator();
            valuesEnumerator.MoveNext();
            modulusBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            maxFiberBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            areaBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            inertiaBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            lengthBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            subSectionsBox.Text = valuesEnumerator.Current;

            valuesEnumerator.MoveNext();
            angleBox.Text = valuesEnumerator.Current;
        }
    }
}
