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
    public partial class ElementInputsControl : UserControl, ISaveableControl
    {
        public ElementInputsControl()
        {
            InitializeComponent();
        }

        // Method to provide enumerable of parameters needed to describe this element
        public virtual IEnumerable<string> GetSaveParams()
        {
            string[] saveParams = { };
            return saveParams;
        }

        // Method to retreive enumerable of parameters needed to describe this element
        public virtual void LoadParams(IEnumerable<string> values)
        {

        }
    }
}
