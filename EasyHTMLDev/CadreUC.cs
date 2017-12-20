using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class CadreUC : UserControl
    {
        #region Internal types
        protected enum CardinalEnum
        {
            NONE,
            TOP,
            BOTTOM,
            LEFT,
            RIGHT
        }
        #endregion

        #region Private Fields
        protected Color borderColor;
        protected Library.CadreModel cm;
        protected SculpturePanel parent;
        protected bool buttonDown;
        protected CardinalEnum action;
        protected Rectangle hold;
        protected event EventHandler<Library.CadreIndexPaintArgs> crossRectanglePaint;
        protected event EventHandler<MouseEventArgs> move, resize;
        protected event EventHandler select;
        #endregion

        #region Public Constructor
        public CadreUC(SculpturePanel panel)
        {
            InitializeComponent();
            this.buttonDown = false;
            this.cm = new Library.CadreModel();
            this.BackColor = this.cm.Background;
            this.ForeColor = this.cm.Foreground;
            this.BorderColor = this.cm.Border;
            base.Left = this.cm.HorizontalPosition;
            base.Top = this.cm.VerticalPosition;
            this.Width = this.cm.Largeur;
            this.Height = this.cm.Hauteur;
            this.cm.PropertyChanged += cm_PropertyChanged;
            this.parent = panel;
            this.parent.RatioChanged += parent_RatioChanged;
            this.Size = new Size(this.cm.Largeur, this.cm.Hauteur);
        }

        public CadreUC(SculpturePanel panel, Library.CadreModel cm)
        {
            InitializeComponent();
            this.buttonDown = false;
            this.cm = cm;
            this.BackColor = this.cm.Background;
            this.ForeColor = this.cm.Foreground;
            this.BorderColor = this.cm.Border;
            base.Left = this.cm.HorizontalPosition;
            base.Top = this.cm.VerticalPosition;
            this.Width = this.cm.Largeur;
            this.Height = this.cm.Hauteur;
            this.cm.PropertyChanged += cm_PropertyChanged;
            this.parent = panel;
            this.parent.RatioChanged += parent_RatioChanged;
            base.Left = Convert.ToInt32(Math.Floor(this.cm.HorizontalPosition / this.Ratio));
            base.Top = Convert.ToInt32(Math.Floor(this.cm.VerticalPosition / this.Ratio));
            this.Size = new Size(this.cm.Largeur, this.cm.Hauteur);
        }
        #endregion

        #region Public Events
        public event EventHandler<MouseEventArgs> Moved
        {
            add { this.move += new EventHandler<MouseEventArgs>(value); }
            remove { this.move -= new EventHandler<MouseEventArgs>(value); }
        }

        public event EventHandler<MouseEventArgs> Resized
        {
            add { this.resize += new EventHandler<MouseEventArgs>(value); }
            remove { this.resize -= new EventHandler<MouseEventArgs>(value); }
        }

        public event EventHandler<Library.CadreIndexPaintArgs> CrossRectanglePaint
        {
            add { this.crossRectanglePaint += new EventHandler<Library.CadreIndexPaintArgs>(value); }
            remove { this.crossRectanglePaint -= new EventHandler<Library.CadreIndexPaintArgs>(value); }
        }

        public event EventHandler Selected
        {
            add { this.select += new EventHandler(value); }
            remove { this.select -= new EventHandler(value); }
        }

        #endregion

        #region Public Properties
        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }

        public Library.CadreModel Properties
        {
            get { return this.cm; }
        }

        public float Ratio
        {
            get { return this.parent.Ratio; }
        }

        public Point Position
        {
            get
            {
                return new Point(base.Left, base.Top);
            }
            set
            {
                int h = this.parent.AutoScrollOffset.X;
                int v = this.parent.AutoScrollOffset.Y;
                this.cm.HorizontalPosition = Convert.ToInt32(Math.Floor((value.X - h) * this.Ratio));
                base.Left = value.X;
                this.cm.VerticalPosition = Convert.ToInt32(Math.Floor((value.Y - v) * this.Ratio));
                base.Top = value.Y;
            }
        }

        public new int Left
        {
            get
            {
                return Convert.ToInt32(Math.Floor(this.cm.HorizontalPosition / this.Ratio));
            }
            set
            {
            }
        }

        public new int Top
        {
            get
            {
                return Convert.ToInt32(Math.Floor(this.cm.VerticalPosition / this.Ratio));
            }
            set
            {
            }
        }

        public new int TabIndex
        {
            get { return this.cm.Index; }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Private Methods
        private CardinalEnum HitTest(int x, int y)
        {
            Point p = new Point(x, y);
            if (p.X < 10 && Math.Abs(p.Y - (this.Height >> 1)) < 10) return CardinalEnum.LEFT;
            else if (this.Width - 10 < p.X && Math.Abs(p.Y - (this.Height >> 1)) < 10) return CardinalEnum.RIGHT;
            else if (p.Y < 10 && Math.Abs(p.X - (this.Width >> 1)) < 10) return CardinalEnum.TOP;
            else if (this.Height - 10 < p.Y && Math.Abs(p.X - (this.Width >> 1)) < 10) return CardinalEnum.BOTTOM;
            else return CardinalEnum.NONE;
        }

        private GraphicsPath DrawArrow(float angle, float tx, float ty)
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

        private GraphicsPath DrawMeter(float angle, float tx, float ty, int size)
        {
            GraphicsPath p = new GraphicsPath();
            p.AddString(String.Format("{0}px", size), SystemFonts.DefaultFont.FontFamily, 0, 10, new Point(0, 5), StringFormat.GenericDefault);
            Matrix m = new Matrix();
            m.Translate(tx, ty);
            m.Rotate(angle);
            p.Transform(m);
            return p;
        }

        private void parent_RatioChanged(object sender, EventArgs e)
        {
        }

        private void cm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.BindingContext[this.cm].SuspendBinding();
            this.BackColor = this.cm.Background;
            this.ForeColor = this.cm.Foreground;
            this.BorderColor = this.cm.Border;
            this.Width = this.cm.Largeur;
            this.Height = this.cm.Hauteur;
            base.Left = this.cm.HorizontalPosition;
            base.Top = this.cm.VerticalPosition;
            this.BindingContext[this.cm].ResumeBinding();
            this.Parent.Invalidate(true);
        }
        #endregion

        #region Override Methods
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.Capture = false;
            this.Cursor = Cursors.Default;
            this.buttonDown = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.select(this, new EventArgs());
            this.action = this.HitTest(e.X, e.Y);
            if (this.action != CardinalEnum.NONE)
            {
                switch (this.action)
                {
                    case CardinalEnum.LEFT:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case CardinalEnum.RIGHT:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case CardinalEnum.TOP:
                        this.Cursor = Cursors.SizeNS;
                        break;
                    case CardinalEnum.BOTTOM:
                        this.Cursor = Cursors.SizeNS;
                        break;
                }
                this.Capture = true;
            }
            else
            {
                this.Cursor = Cursors.SizeAll;
            }
            this.buttonDown = true;
            int h = this.parent.AutoScrollOffset.X;
            int v = this.parent.AutoScrollOffset.Y;
            this.hold = new Rectangle(e.X + h, e.Y + v, this.Width, this.Height);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.buttonDown)
            {
                this.Parent.SuspendLayout();
                if (this.action == CardinalEnum.NONE)
                {
                    int h = this.parent.AutoScrollOffset.X;
                    int v = this.parent.AutoScrollOffset.Y;
                    MouseEventArgs e2 = new MouseEventArgs(e.Button, e.Clicks, e.X + h - this.hold.X, e.Y + v - this.hold.Y, e.Delta);
                    this.move(this, e2);
                }
                else
                {
                    switch (this.action)
                    {
                        case CardinalEnum.LEFT:
                            {
                                MouseEventArgs e2 = new MouseEventArgs(e.Button, e.Clicks, this.hold.Width - (e.X - this.hold.X), this.hold.Height, e.Delta);
                                this.resize(this, e2);
                            }
                            break;
                        case CardinalEnum.TOP:
                            {
                                MouseEventArgs e2 = new MouseEventArgs(e.Button, e.Clicks, this.hold.Width, this.hold.Height - (e.Y - this.hold.Y), e.Delta);
                                this.resize(this, e2);
                            }
                            break;
                        case CardinalEnum.RIGHT:
                            {
                                MouseEventArgs e2 = new MouseEventArgs(e.Button, e.Clicks, this.hold.Width + (e.X - this.hold.X), this.hold.Height, e.Delta);
                                this.resize(this, e2);
                            }
                            break;
                        case CardinalEnum.BOTTOM:
                            {
                                MouseEventArgs e2 = new MouseEventArgs(e.Button, e.Clicks, this.hold.Width, this.hold.Height + (e.Y - this.hold.Y), e.Delta);
                                this.resize(this, e2);
                            }
                            break;
                    }
                    this.cm.SuspendBinding = true;
                    this.cm.Largeur = Convert.ToInt32(Math.Floor(this.Width * this.Ratio));
                    this.cm.Hauteur = Convert.ToInt32(Math.Floor(this.Height * this.Ratio));
                    this.cm.SuspendBinding = false;
                    this.cm.RaisePropertyChanged();
                }
                this.Parent.ResumeLayout();
                this.Parent.Invalidate(true);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Capture = false;
            this.buttonDown = false;
            this.Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            try
            {
                ControlPaint.DrawBorder(e.Graphics,
                                        this.ClientRectangle,
                                        this.BorderColor, this.Properties.HorizontalBorder, ButtonBorderStyle.Solid,
                                        this.BorderColor, this.Properties.VerticalBorder, ButtonBorderStyle.Solid,
                                        this.BorderColor, this.Properties.HorizontalBorder, ButtonBorderStyle.Solid,
                                        this.borderColor, this.Properties.VerticalBorder, ButtonBorderStyle.Solid);
                RectangleF innerRect = new RectangleF(this.ClientRectangle.Left + ((this.Properties.HorizontalBorder + this.Properties.HorizontalPadding) / this.Ratio),
                                                    this.ClientRectangle.Top + ((this.Properties.VerticalBorder + this.Properties.VerticalPadding) / this.Ratio),
                                                    this.ClientRectangle.Width - (2 * (this.Properties.HorizontalBorder + this.Properties.HorizontalPadding) / this.Ratio),
                                                    this.ClientRectangle.Height - (2 * (this.Properties.VerticalBorder + this.Properties.VerticalPadding) / this.Ratio));
                Pen penForeColor = new Pen(this.ForeColor);
                if (this.crossRectanglePaint != null)
                    this.crossRectanglePaint(this, new Library.CadreIndexPaintArgs(this.Properties, e));
                e.Graphics.DrawLine(penForeColor, new PointF(innerRect.Left + innerRect.Width / 2f, innerRect.Top), new PointF(innerRect.Left + innerRect.Width / 2f, innerRect.Top + innerRect.Height));
                e.Graphics.DrawLine(penForeColor, new PointF(innerRect.Left, innerRect.Top + innerRect.Height / 2f), new PointF(innerRect.Left + innerRect.Width, innerRect.Top + innerRect.Height / 2f));
                GraphicsPath p;
                p = this.DrawMeter(0.0f, innerRect.Left + innerRect.Width / 2f, innerRect.Top + innerRect.Height / 2f, this.Properties.Largeur - this.Properties.HorizontalPadding - this.Properties.HorizontalBorder);
                e.Graphics.DrawPath(penForeColor, p);
                p.Dispose();
                p = this.DrawArrow(0.0f, innerRect.Left + innerRect.Width / 2f, innerRect.Top);
                e.Graphics.DrawPath(penForeColor, p);
                p.Dispose();
                p = this.DrawArrow(-180.0f, innerRect.Left + innerRect.Width / 2f, innerRect.Top + innerRect.Height - 1);
                e.Graphics.DrawPath(penForeColor, p);
                p.Dispose();

                p = this.DrawMeter(-90.0f, innerRect.Left + innerRect.Width / 2f, innerRect.Top + innerRect.Height / 2f, this.Properties.Hauteur - this.Properties.VerticalPadding - this.Properties.VerticalBorder);
                e.Graphics.DrawPath(penForeColor, p);
                p.Dispose();
                p = this.DrawArrow(-90.0f, innerRect.Left, innerRect.Top + innerRect.Height / 2f);
                e.Graphics.DrawPath(penForeColor, p);
                p.Dispose();
                p = this.DrawArrow(-270.0f, innerRect.Left + innerRect.Width - 1, innerRect.Top + innerRect.Height / 2f);
                e.Graphics.DrawPath(penForeColor, p);
                p.Dispose();
                penForeColor.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        #endregion
    }
}
