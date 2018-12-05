using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BeamPro
{
    class BasicConditionDragDropObject : DragDropObject
    {
        // Private objects
        private Bitmap standardBackgroundImage;
        private Bitmap dragEnterBackroungImage;

        // Public objects
		//reserved

        // Default class constructor
        public BasicConditionDragDropObject() : base()
        {
            standardBackgroundImage = new Bitmap(Properties.Resources.BasicConditionIcon, Size);
            dragEnterBackroungImage = new Bitmap(Properties.Resources.BasicConditionDragEnteredIcon, Size);
            Graphics g = Graphics.FromImage(standardBackgroundImage);
            g.DrawString(BasicConditionInputsForm.BasicConditionCount.ToString(),
                new Font("Arial", 8),
                new SolidBrush(Color.Black),
                ClientRectangle);
            g.Flush();
            g.Dispose();
            BackgroundImage = standardBackgroundImage;

            DragDropObjectType = 2;
        }

        // Class constructor with parent ObjectHolder argument
        public BasicConditionDragDropObject(ObjectHolder parent) : base(parent)
        {
            standardBackgroundImage = new Bitmap(Properties.Resources.BasicConditionIcon, Size);
            dragEnterBackroungImage = new Bitmap(Properties.Resources.BasicConditionDragEnteredIcon, Size);
            Graphics g = Graphics.FromImage(standardBackgroundImage);
            g.DrawString(BasicConditionInputsForm.BasicConditionCount.ToString(),
                new Font("Arial", 8),
                new SolidBrush(Color.Black),
                ClientRectangle);
            g.Flush();
            g.Dispose();
            BackgroundImage = standardBackgroundImage;

            DragDropObjectType = 2;
            DragLeave += new EventHandler(OnDragLeave);
            sectionInputs = new BasicConditionInputsForm();
        }

        // Drag enter event handler
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
            BackgroundImage = dragEnterBackroungImage;
        }

        // Reset Icon OnDragLeave
        public void OnDragLeave(object sender, EventArgs e)
        {
            BackgroundImage = standardBackgroundImage;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            BackgroundImage = standardBackgroundImage;
        }
    }
}
