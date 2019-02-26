using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class MasterObjectCreationPanel : UserControl
    {
        private Library.MasterObject mObject;

        public MasterObjectCreationPanel()
        {
            InitializeComponent();
            this.creationPanel1.Dock = DockStyle.Fill;
        }

        public MasterObjectCreationPanel(Library.MasterObject mObj)
        {
            this.mObject = mObj;
            InitializeComponent();
        }

        public Library.MasterObject MasterObject
        {
            get { return this.mObject; }
            set
            {
                this.mObject = value;
                if (this.mObject != null)
                {
                    this.creationPanel1.CountColumns = this.mObject.CountColumns;
                    this.creationPanel1.CountLines = this.mObject.CountLines;
                    this.creationPanel1.Start();
                    this.Refresh();
                }
            }
        }

        public void init()
        {
            this.creationPanel1.initialize_cases();
        }

        public void Confirm()
        {
            foreach (Library.AreaSizedRectangle r in this.creationPanel1.List)
            {
                Point p = this.creationPanel1.RevertCoordinates(new Point(r.Left, r.Top));
                r.StartWidth = p.X;
                r.StartHeight = p.Y;
            }
            this.mObject.MakeZones(this.creationPanel1.List);
        }
    }
}
