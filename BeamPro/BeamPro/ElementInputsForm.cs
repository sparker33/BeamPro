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
    public partial class ElementInputsForm : Form
    {
        // Private/Protected fields
        protected ElementInputsControl elementInputsControl;

        // Public properties
        public event EventHandler OkButtonClicked;
        public string ElementName { get => nameBox.Text; set => nameBox.Text = value; }

        // Default class constructor method
        public ElementInputsForm() : base()
        {
            InitializeComponent();
        }

        // Class constructor method with name and input control inputs
        public ElementInputsForm(string name, ElementInputsControl controls)
        {
            elementInputsControl = controls;
            InitializeComponent();

            this.nameBox.Text = name;
        }

        // Handler for okButton
        private void OkButton_Click(object sender, EventArgs e)
        {
            OkButtonClicked?.Invoke(this, new EventArgs());
            this.Visible = false;
        }

        // Method for updating name field when it's changed on parent InputPanel
        public void NameChangedFromParent(string newName)
        {
            nameBox.Text = newName;
        }

        // Method to apply the worksheet to a beam model
        public virtual void ApplyWorksheet(ref List<IElement> elementList, ref List<INode> nodeList)
        {

        }

        // Method to save element 
        public string SaveElement()
        {
            ISaveableControl componentInputsPanel = elementInputsControl as ISaveableControl;
            string values = "";
            string[] parameters = componentInputsPanel.GetSaveParams().ToArray();
            foreach (string saveParam in parameters)
            {
                values = String.Format("{0}, {1}", values, saveParam);
            }
            return values.Remove(0, 2);
        }

        // Method to load element from saved analysis
        public void LoadElement(string values)
        {
            ISaveableControl componentInputsPanel = elementInputsControl as ISaveableControl;
            string[] savedParams = values.Split(',');
            componentInputsPanel.LoadParams(savedParams);
        }
    }
}
