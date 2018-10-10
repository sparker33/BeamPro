using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BeamPro
{
    class BasicSectionDragDropObject : DragDropObject
    {
        // Private objects
        private Bitmap standardBackgroundImage;
        private Bitmap dragEnterBackroungImage;

        // Public objects
        public static int BasicSectionNumber = 0;

        // Default class constructor
        public BasicSectionDragDropObject() : base()
        {
            standardBackgroundImage = new Bitmap(Properties.Resources.BasicSectionIcon, Size);
            dragEnterBackroungImage = new Bitmap(Properties.Resources.BasicSectionDragEnteredIcon, Size);
            Graphics g = Graphics.FromImage(standardBackgroundImage);
            g.DrawString((BasicSectionNumber++).ToString(),
                new Font("Arial", 8),
                new SolidBrush(Color.Black),
                ClientRectangle);
            g.Flush();
            g.Dispose();
            BackgroundImage = standardBackgroundImage;

            DragDropObjectType = 1;
        }

        // Class constructor with parent ObjectHolder argument
        public BasicSectionDragDropObject(ObjectHolder parent) : base(parent)
        {
            standardBackgroundImage = new Bitmap(Properties.Resources.BasicSectionIcon, Size);
            dragEnterBackroungImage = new Bitmap(Properties.Resources.BasicSectionDragEnteredIcon, Size);
            Graphics g = Graphics.FromImage(standardBackgroundImage);
            g.DrawString((BasicSectionNumber++).ToString(),
                new Font("Arial", 8),
                new SolidBrush(Color.Black),
                ClientRectangle);
            g.Flush();
            g.Dispose();
            BackgroundImage = standardBackgroundImage;

            DragDropObjectType = 1;
            DragLeave += new EventHandler(OnDragLeave);
            sectionInputs = new BasicSectionInputsForm();
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
