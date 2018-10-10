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
using System.IO;

namespace BeamPro
{
    public partial class ObjectHolder : Panel
    {
        // Private objects
        private List<DragDropObject> dragDropObjects = new List<DragDropObject>();

        // Public Accessors
        public List<ElementInputsForm> BeamWorksheets
        {
            get
            {
                List<ElementInputsForm> worksheets = new List<ElementInputsForm>();
                foreach (DragDropObject ddObj in dragDropObjects)
                {
                    worksheets.Add(ddObj.SectionInputs);
                }

                return worksheets;
            }
        }
        public ObjectHolder()
        {
            InitializeComponent();

            AllowDrop = true;
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (dragDropObjects.Count() != 0)
            {
                drgevent.Effect = DragDropEffects.None;
            }
            else
            {
                drgevent.Effect = DragDropEffects.Copy;
            }
        }

        // Method to generate new DragDrop elemnts
        public DragDropObject GetDragDropItem(int type, Point location, Size size)
        {
            DragDropObject newDDObject = new DragDropObject();
            switch (type)
            {
                case (1):
                    newDDObject = new BasicSectionDragDropObject(this);
                    break;
                case (2):
                    newDDObject = new BasicConditionDragDropObject(this);
                    break;
                default:
                    newDDObject = new DragDropObject();
                    break;
            }
            newDDObject.Location = location;
            newDDObject.Size = size;

            return newDDObject;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (Int32.TryParse(drgevent.Data.GetData(DataFormats.Text).ToString(), out int newDDObjectType))
            {
                if (newDDObjectType > 2) { return; }
            }
            else { return; }
            Point newObjLocation = this.PointToClient(new Point(drgevent.X, drgevent.Y));
            Size newObjSize = new Size(50, 50);
            DragDropObject newDDObject = GetDragDropItem(newDDObjectType, newObjLocation, newObjSize);
            this.Controls.Add(newDDObject);
            dragDropObjects.Add(newDDObject);

            Invalidate();
        }

        // Method enabling client DragDropObjects to add a new member
        public void AddDragDropObject(DragDropObject senderDDObj, DragDropObject newDDObj)
        {
            this.Controls.Add(newDDObj);
            dragDropObjects.Insert(dragDropObjects.IndexOf(senderDDObj) + 1, newDDObj);
            for (int i = dragDropObjects.IndexOf(newDDObj) + 1; i < dragDropObjects.Count(); i++)
            {
                dragDropObjects[i].UpdateLocation(dragDropObjects[i - 1]);
            }
        }

        // Method enabling client DragDropObjects to delete themselves
        public void RemoveDragDropObject(DragDropObject senderDDObj)
        {
            dragDropObjects[dragDropObjects.IndexOf(senderDDObj)].Location = new Point(
                dragDropObjects[dragDropObjects.IndexOf(senderDDObj)].Location.X - dragDropObjects[dragDropObjects.IndexOf(senderDDObj)].Size.Width,
                dragDropObjects[dragDropObjects.IndexOf(senderDDObj)].Location.Y);
            for (int i = dragDropObjects.IndexOf(senderDDObj) + 1; i < dragDropObjects.Count(); i++)
            {
                dragDropObjects[i].UpdateLocation(dragDropObjects[i - 1]);
            }
            this.Controls.Remove(senderDDObj);
            dragDropObjects.Remove(senderDDObj);
        }

        // Method to center elements in the view
        public void CenterView()
        {
            dragDropObjects[0].Location = new Point(
                ClientSize.Width / 2 - dragDropObjects.Count() * dragDropObjects[0].Width / 2,
                ClientSize.Height / 2);
            for (int i = 1; i < dragDropObjects.Count(); i++)
            {
                dragDropObjects[i].UpdateLocation(dragDropObjects[i - 1]);
            }
        }

        // Method to save an analysis
        public void SaveAnalysis(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string line;
                foreach (DragDropObject ddObj in dragDropObjects)
                {
                    line = String.Format("{0}, {1}", ddObj.SectionInputs.ElementName, ddObj.DragDropObjectType.ToString());
                    writer.WriteLine(line);
                    line = ddObj.SectionInputs.SaveElement();
                    writer.WriteLine(line);
                }
            }
        }

        // Method to load a saved analysis
        public void LoadAnalysis(string filePath)
        {
            foreach (DragDropObject ddObj in dragDropObjects)
            {
                this.Controls.Remove(ddObj);
            }
            dragDropObjects.Clear();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                dragDropObjects.Add(GetDragDropItem(Int32.Parse(values[1]), new Point(0, 0), new Size(50, 50)));
                dragDropObjects[0].SectionInputs.ElementName = values[0];
                line = reader.ReadLine();
                dragDropObjects[0].SectionInputs.LoadElement(line);
                this.Controls.Add(dragDropObjects.Last());
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    values = line.Split(',');
                    dragDropObjects.Add(GetDragDropItem(Int32.Parse(values[1]),
                        new Point(dragDropObjects.Last().Location.X + dragDropObjects.Last().Width, dragDropObjects.Last().Location.Y),
                        dragDropObjects.Last().Size));
                    dragDropObjects.Last().SectionInputs.ElementName = values[0];
                    line = reader.ReadLine();
                    dragDropObjects.Last().SectionInputs.LoadElement(line);
                    this.Controls.Add(dragDropObjects.Last());
                }
            }
            CenterView();
        }
    }
}
