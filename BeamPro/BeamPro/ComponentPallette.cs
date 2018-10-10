using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace BeamPro
{
    public partial class ComponentPallette : Control
    {
        // Private fields
        private List<DragDropObject> dragDropObjects = new List<DragDropObject>();

        // Public accessors
        public int IconSize = 1;

        // Class construction method
        public ComponentPallette()
        {
            InitializeComponent();
        }

        #region BaseClassOverrides
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), ClientRectangle);

            foreach (DragDropObject ddobj in dragDropObjects)
            {
                Rectangle rect = GetIconRectangle(ddobj);

                pe.Graphics.DrawImage(ddobj.BackgroundImage, rect);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            foreach (DragDropObject ddobj in dragDropObjects)
            {
                Rectangle rect = GetIconRectangle(ddobj);

                if (rect.Contains(e.Location))
                {
                    // Begin drag-drop
                    DoDragDrop(ddobj.DragDropObjectType.ToString(), DragDropEffects.Copy);

                    return;
                }
            }
        }
        #endregion

        // Method to add colors to the pallete
        public void AddElementType(DragDropObject ddobj)
        {
            dragDropObjects.Add(ddobj);

            Invalidate();
        }

        // Method to generate the rectangle to draw into
        public Rectangle GetIconRectangle(DragDropObject ddobj)
        {
            int index = dragDropObjects.IndexOf(ddobj);
            if (index == -1)
            {
                return new Rectangle(0, 0, 0, 0);
            }

            IconSize = ClientRectangle.Height;
            int numDDObjectsX = ClientRectangle.Width / IconSize;

            if (numDDObjectsX == 0)
            {
                numDDObjectsX = 1;
            }
            int ddObjectsX = index % numDDObjectsX;
            int ddObjectsY = index / numDDObjectsX;

            Rectangle rect = new Rectangle(ddObjectsX * IconSize, ddObjectsY * IconSize, IconSize, IconSize);
            rect.Inflate(-IconSize / 10, -IconSize / 10);

            return rect;
        }
    }
}
