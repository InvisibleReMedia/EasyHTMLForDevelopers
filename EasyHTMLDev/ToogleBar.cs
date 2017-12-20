using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EasyHTMLDev
{
    public partial class ToogleBar : UserControl
    {
        private bool isUp;
        private UserControl content;

        public ToogleBar()
        {
            InitializeComponent();
            this.isUp = false;
        }

        public bool IsUp
        {
            get { return this.isUp; }
            set
            {
                if (value != this.isUp)
                {
                    if (this.content != null)
                    {
                        if (this.isUp)
                            this.content.Visible = false;
                        if (this.Parent != null)
                        {
                            if (this.isUp)
                            {
                                this.Height = this.Height - this.content.Height;
                                this.Parent.Height = this.Parent.Height - this.content.Height;
                            }
                            else
                            {
                                this.Height = this.Height + this.content.Height;
                                this.Parent.Height = this.Parent.Height + this.content.Height;
                            }
                        }
                        if (!this.isUp)
                            this.content.Visible = true;
                    }
                    this.isUp = value;
                    this.Invalidate();
                }
            }
        }

        public UserControl Content
        {
            get { return this.content; }
            set
            {
                if (this.content != null)
                    this.Controls.Remove(this.content);
                if (value != null)
                {
                    value.Left = 0;
                    value.Top = 20;
                    value.Visible = this.isUp;
                    this.Controls.Add(value);
                }
                this.content = value;
            }
        }

        private GraphicsPath DrawArrow(float angle, int tx, int ty)
        {
            GraphicsPath p = new GraphicsPath();
            p.AddPolygon(new Point[] { new Point(0, 0), new Point(-5, 5), new Point(5, 5) });
            p.CloseAllFigures();
            Matrix m = new Matrix();
            m.Translate(tx, ty);
            m.Rotate(angle);
            p.Transform(m);
            return p;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            GraphicsPath gp = new GraphicsPath(FillMode.Alternate);
            RectangleF r = e.Graphics.VisibleClipBounds;
            r.Inflate(new SizeF(0.9f, 0.9f));
            gp.AddRectangle(r);
            e.Graphics.DrawPath(Pens.Black, gp);
            gp.Dispose();
            if (!this.isUp)
                gp = this.DrawArrow(0, 10, 10);
            else
                gp = this.DrawArrow(-180, 10, 10);
            e.Graphics.DrawPath(Pens.Black, gp);
        }

        private void ToogleBar_Click(object sender, EventArgs e)
        {
            this.IsUp = !this.IsUp;
        }
    }
}
