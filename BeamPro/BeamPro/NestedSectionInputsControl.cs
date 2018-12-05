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
	public partial class NestedSectionInputsControl : ElementInputsControl
	{
		// Public accessors to input boxes
		public double Modulus1
		{
			get
			{
				try
				{
					return Double.Parse(modulusBox1.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Modulus input must be a positive number.");
				}
			}
		}
		public double Modulus2
		{
			get
			{
				try
				{
					return Double.Parse(modulusBox2.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Modulus input must be a positive number.");
				}
			}
		}
		public double MaxFiberDistance1
		{
			get
			{
				try
				{
					return Double.Parse(maxFiberBox1.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Max Fiber Distance input must be a positive number.");
				}
			}
		}
		public double MaxFiberDistance2
		{
			get
			{
				try
				{
					return Double.Parse(maxFiberBox2.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Max Fiber Distance input must be a positive number.");
				}
			}
		}
		public double Area1
		{
			get
			{
				try
				{
					return Double.Parse(areaBox1.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Area input must be a positive number.");
				}
			}
		}
		public double Area2
		{
			get
			{
				try
				{
					return Double.Parse(areaBox2.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Area input must be a positive number.");
				}
			}
		}
		public double Inertia1
		{
			get
			{
				try
				{
					return Double.Parse(inertiaBox1.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Inertia input must be a positive number.");
				}
			}
		}
		public double Inertia2
		{
			get
			{
				try
				{
					return Double.Parse(inertiaBox2.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Inertia input must be a positive number.");
				}
			}
		}
		public int Subsections1
		{
			get
			{
				try
				{
					return Int32.Parse(subSectionsBox1.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Subsections input must be a positive integer.");
				}
			}
		}
		public int Subsections2
		{
			get
			{
				try
				{
					return Int32.Parse(subSectionsBox2.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Subsections input must be a positive integer.");
				}
			}
		}
		public double RelAngle1
		{
			get
			{
				try
				{
					return Double.Parse(angleBox1.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Relative Angle input must be a positive integer.");
				}
			}
		}
		public double RelAngle2
		{
			get
			{
				try
				{
					return Double.Parse(angleBox2.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Relative Angle input must be a positive integer.");
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
		public double LeftRadialTieStiffness
		{
			get
			{
				try
				{
					return Double.Parse(leftRadialStiffnessBox.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Tie stiffness input must be a positive number.");
				}
			}
		}
		public double RightRadialTieStiffness
		{
			get
			{
				try
				{
					return Double.Parse(rightRadialStiffnessBox.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Tie stiffness input must be a positive number.");
				}
			}
		}
		public double LeftAxialTieStiffness
		{
			get
			{
				try
				{
					return Double.Parse(leftAxialStiffnessBox.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Tie stiffness input must be a positive number.");
				}
			}
		}
		public double RightAxialTieStiffness
		{
			get
			{
				try
				{
					return Double.Parse(rightAxialStiffnessBox.Text);
				}
				catch (FormatException)
				{
					throw new ArgumentException("Tie stiffness input must be a positive number.");
				}
			}
		}
		//public int LeftAxialTieDestination
		//{
		//	get
		//	{
		//		return leftTieComboBox.SelectedIndex; /* ??USE STRING NAME OF SECTION TO DETERMINE CORRECT NODE IN NESTEDSECTIONINPUTSFORM.APPLYWORKSHEET METHOD?? */
		//	}
		//}
		//public int RightAxialTieDestination
		//{
		//	get
		//	{
		//		return rightTieComboBox.SelectedIndex;
		//	}
		//}
		//public int RadialTieDirectionDefiningNode
		//{
		//	get
		//	{
		//		return radialDefiningBeamDropDown.SelectedIndex;
		//	}
		//}

		// Class constructor
		public NestedSectionInputsControl() : base()
		{
			InitializeComponent();
			// Populate and initialize selected index for comboboxes
			//code here
			radialDefiningBeamDropDown.SelectedIndex = 0;
		}

		// Method to provide enumerable of parameters needed to describe this element
		public override IEnumerable<string> GetSaveParams()
		{
			string[] saveParams = { modulusBox1.Text,
									maxFiberBox1.Text,
									areaBox1.Text,
									inertiaBox1.Text,
									subSectionsBox1.Text,
									angleBox1.Text,
									modulusBox2.Text,
									maxFiberBox2.Text,
									areaBox2.Text,
									inertiaBox2.Text,
									subSectionsBox2.Text,
									angleBox2.Text,
									lengthBox.Text,
									leftRadialStiffnessBox.Text,
									rightRadialStiffnessBox.Text,
									leftAxialStiffnessBox.Text,
									rightAxialStiffnessBox.Text,
									leftTieComboBox.SelectedIndex.ToString(),
									rightTieComboBox.SelectedIndex.ToString(),
									radialDefiningBeamDropDown.SelectedIndex.ToString() };
			return saveParams;
		}

		// Method to retreive enumerable of parameters needed to describe this element
		public override void LoadParams(IEnumerable<string> values)
		{
			IEnumerator<string> valuesEnumerator = values.GetEnumerator();
			valuesEnumerator.MoveNext();
			modulusBox1.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			maxFiberBox1.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			areaBox1.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			inertiaBox1.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			subSectionsBox1.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			angleBox1.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			modulusBox2.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			maxFiberBox2.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			areaBox2.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			inertiaBox2.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			subSectionsBox2.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			angleBox2.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			lengthBox.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			leftRadialStiffnessBox.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			rightRadialStiffnessBox.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			leftAxialStiffnessBox.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			rightAxialStiffnessBox.Text = valuesEnumerator.Current;

			valuesEnumerator.MoveNext();
			leftTieComboBox.SelectedIndex = Int32.Parse(valuesEnumerator.Current);

			valuesEnumerator.MoveNext();
			rightTieComboBox.SelectedIndex = Int32.Parse(valuesEnumerator.Current);

			valuesEnumerator.MoveNext();
			radialDefiningBeamDropDown.SelectedIndex = Int32.Parse(valuesEnumerator.Current);
		}
	}
}
