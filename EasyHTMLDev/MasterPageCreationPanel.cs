using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EasyHTMLDev
{
    public partial class MasterPageCreationPanel : UserControl
    {
        private Library.MasterPage mPage;

        public MasterPageCreationPanel()
        {
            InitializeComponent();
            this.creationPanel1.Dock = DockStyle.Fill;
        }

        public MasterPageCreationPanel(Library.MasterPage mPage)
        {
            this.mPage = mPage;
            InitializeComponent();
        }

        public Library.MasterPage MasterPage
        {
            get { return this.mPage; }
            set
            {
                this.mPage = value;
                if (this.mPage != null)
                {
                    this.creationPanel1.CountColumns = this.mPage.CountColumns;
                    this.creationPanel1.CountLines = this.mPage.CountLines;
                    this.creationPanel1.Start();
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
            this.mPage.MakeZones(this.creationPanel1.List);
        }
    }
}
