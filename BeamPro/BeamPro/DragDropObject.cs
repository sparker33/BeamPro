using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BeamPro
{
    public class DragDropObject : Control
    {
        // Private and protected fields
        private int dragDropObjectType = 0;
        protected ElementInputsForm sectionInputs;
        private ObjectHolder parentObjectHolder;
        private MenuItem removeObjectMenuItem = new MenuItem();

        // Public accessors
        public int DragDropObjectType { get => dragDropObjectType; set => dragDropObjectType = value; }
        public ObjectHolder ParentObjectHolder { get => parentObjectHolder; set => parentObjectHolder = value; }
        public ElementInputsForm SectionInputs { get => sectionInputs; }

        // Default class constructor
        public DragDropObject()
        {
            Location = new Point(0, 0);
            Size = new Size(50, 50);
            BackgroundImage = new Bitmap(Properties.Resources.DefaultIcon, Size);
            AllowDrop = false;

            ContextMenu = new ContextMenu(new MenuItem[] { removeObjectMenuItem });
            // Remove Object menustripitem
            removeObjectMenuItem.Text = "Remove";
            removeObjectMenuItem.Click += new EventHandler(RemoveObject_Click);
        }

        // Default class constructor
        public DragDropObject(ObjectHolder parent)
        {
            ParentObjectHolder = parent;
            AllowDrop = true;
            Location = new Point(0, 0);
            Size = new Size(50, 50);
            BackgroundImage = new Bitmap(Properties.Resources.DefaultIcon, Size);

            ContextMenu = new ContextMenu(new MenuItem[] { removeObjectMenuItem });
            // Set up removeObjectMenuItem
            removeObjectMenuItem.Text = "Remove";
            removeObjectMenuItem.Click += new EventHandler(RemoveObject_Click);
        }

        // Method to update location to remain adjacent to correct other DragDropObjects
        public void UpdateLocation(DragDropObject rightNeighbor)
        {
            Location = new Point(rightNeighbor.Location.X + Size.Width, rightNeighbor.Location.Y);
        }

        // Drag enter event handler
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            drgevent.Effect = DragDropEffects.Copy;
        }

        // Add a new DragDropObject to teh chain
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (!Int32.TryParse(drgevent.Data.GetData(DataFormats.Text).ToString(), out int newDDObjectType))
            {
                return;
            }
            Point newDDObjectLocation = new Point(Location.X + Size.Width, Location.Y);
            DragDropObject newDDObject = ParentObjectHolder.GetDragDropItem(newDDObjectType, newDDObjectLocation, this.Size);
            ParentObjectHolder.AddDragDropObject(this, newDDObject);
        }

        // Method to display the section inputs form on double click
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            sectionInputs.ShowDialog();
        }

        // Method to remove this object from the ObjectHolder collection
        protected void RemoveObject_Click(object sender, EventArgs e)
        {
            parentObjectHolder.RemoveDragDropObject(this);
            this.Dispose();
        }
    }
}
