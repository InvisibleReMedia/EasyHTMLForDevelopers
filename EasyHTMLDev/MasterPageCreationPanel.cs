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
        private Case[,] cases;
        private Library.MasterPage mPage;
        private bool select;
        private int startx;
        private int starty;
        private List<Library.SizedRectangle> list = new List<Library.SizedRectangle>();

        public MasterPageCreationPanel()
        {
            InitializeComponent();
        }

        public Library.MasterPage MasterPage
        {
            get { return this.mPage; }
            set { this.mPage = value; }
        }

        /// <summary>
        /// int deltax = (width - 3) / (int)this.mPage.CountColumns;
        /// est le pas sur l'écran par rapport à la grille
        /// l * deltax = la position sur l'écran du coin supérieur gauche d'un rectangle de la grille
        /// width - 3 => (int)this.mPage.CountColumns
        ///          => ?
        /// C'est une règle de trois.
        /// 
        /// </summary>
        /// <param name="p">point de coordonnées écran</param>
        /// <returns>point de coordonnées dans la grille</returns>
        public Point RevertCoordinates(Point p)
        {
            return new Point((int)(p.X * (int)this.mPage.CountColumns / (double)(this.Width - 3)), (int)(p.Y * (double)this.mPage.CountLines / (double)(this.Height - 3)));
        }

        public Point getCoordinates(int l, int c)
        {
            return getRectangle(l, c).Location;
        }

        public Rectangle getRectangle(int l, int c)
        {
            return this.cases[l, c].Rectangle;
        }

        private void resize_case(int l, int c, Rectangle r)
        {
            this.cases[l, c].resise(r);
        }

        private void calculeSize(int width, int height)
        {
            int deltax = (width - 3) / (int)this.mPage.CountColumns;
            int deltay = (height - 3) / (int)this.mPage.CountLines;

            if (deltax < 3 || deltay < 3)
                return;

            int pos_ligne = 3;
            int pos_colonne;
            for (int count_ligne = 0; count_ligne < this.mPage.CountLines; ++count_ligne)
            {
                pos_colonne = 3;
                for (int count_colonne = 0; count_colonne < this.mPage.CountColumns; ++count_colonne)
                {
                    resize_case(count_ligne, count_colonne, new Rectangle(pos_colonne+1, pos_ligne + 1, deltax, deltay));
                    pos_colonne += deltax;
                }
                pos_ligne += deltay;
            }
        }

        public void initialize_cases()
        {
            if (this.mPage != null)
            {
                this.cases = new Case[this.mPage.CountLines, this.mPage.CountColumns];
                for (int count_ligne = 0; count_ligne < this.mPage.CountLines; ++count_ligne)
                {
                    for (int count_colonne = 0; count_colonne < this.mPage.CountColumns; ++count_colonne)
                    {
                        this.cases[count_ligne, count_colonne] = new Case(count_ligne, count_colonne);
                    }
                }
                this.calculeSize(this.Width, this.Height);
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            if (this.mPage != null)
            {
                this.calculeSize(width, height);
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.mPage != null)
            {
                e.Graphics.FillRectangle(Brushes.DarkGray, new RectangleF(e.Graphics.VisibleClipBounds.Location, e.Graphics.VisibleClipBounds.Size));
                e.Graphics.Clip = new Region(new RectangleF(e.Graphics.VisibleClipBounds.Location, e.Graphics.VisibleClipBounds.Size));
                for (int count_ligne = 0; count_ligne < this.mPage.CountLines; ++count_ligne)
                {
                    for (int count_colonne = 0; count_colonne < this.mPage.CountColumns; ++count_colonne)
                    {
                        this.cases[count_ligne, count_colonne].Draw(e.Graphics);
                    }
                }
            }
            foreach (Library.SizedRectangle r in this.list)
            {
                Rectangle rectStart = this.getRectangle(r.Top, r.Left);
                Rectangle rectEnd = this.getRectangle(r.Bottom, r.Right);
                Rectangle zoneRect = new Rectangle(rectStart.X, rectStart.Y, rectEnd.Right - rectStart.X, rectEnd.Bottom - rectStart.Y);
                e.Graphics.FillRectangle(Brushes.Aquamarine, zoneRect);
                e.Graphics.DrawRectangle(Pens.Black, zoneRect);
            }
        }

        private void MasterPagePanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = true;
            this.select = true;
            this.startx = this.PointToClient(Control.MousePosition).X;
            this.starty = this.PointToClient(Control.MousePosition).Y;
        }

        private void MasterPagePanel_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (select)
                {
                    this.select = false;


                    int deltax = (this.Width - 3) / (int)this.mPage.CountColumns;
                    int deltay = (this.Height - 3) / (int)this.mPage.CountLines;

                    if (deltax < 3 || deltay < 3)
                        return;

                    int pos_ligne = 3;
                    int pos_colonne;
                    for (int count_ligne = 0; count_ligne < this.mPage.CountLines; ++count_ligne)
                    {
                        pos_colonne = 3;
                        for (int count_colonne = 0; count_colonne < this.mPage.CountColumns; ++count_colonne)
                        {
                            if (((pos_colonne + deltax) >= this.startx && pos_colonne < this.PointToClient(Control.MousePosition).X) &&
                                ((pos_ligne + deltay) >= this.starty && pos_ligne < this.PointToClient(Control.MousePosition).Y))
                            {
                                this.cases[count_ligne, count_colonne].Disable();
                            }
                            pos_colonne += deltax;
                        }
                        pos_ligne += deltay;
                    }

                    Point start = this.RevertCoordinates(new Point(this.startx, this.starty));
                    Point end = this.RevertCoordinates(this.PointToClient(Control.MousePosition));

                    if (start.X >= 0 && start.X < this.mPage.CountColumns && end.X >= 0 && end.X < this.mPage.CountColumns &&
                        start.Y >= 0 && start.Y < this.mPage.CountLines && end.Y >= 0 && end.Y < this.mPage.CountLines)
                        this.list.Add(new Library.SizedRectangle(deltax * (end.X - start.X + 1), deltay * (end.Y - start.Y + 1), start.X, end.X, start.Y, end.Y));

                    for (int count_ligne = 0; count_ligne < this.mPage.CountLines; ++count_ligne)
                    {
                        for (int count_colonne = 0; count_colonne < this.mPage.CountColumns; ++count_colonne)
                        {
                            this.cases[count_ligne, count_colonne].Unselect();
                        }
                    }
                    this.Invalidate();
                    this.Capture = false;
                }
            }
            catch { }
        }

        private void MasterPagePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mPage != null)
            {
                if (this.select)
                {
                    for (int count_ligne = 0; count_ligne < this.mPage.CountLines; ++count_ligne)
                    {
                        for (int count_colonne = 0; count_colonne < this.mPage.CountColumns; ++count_colonne)
                        {
                            this.cases[count_ligne, count_colonne].Unselect();
                        }
                    }
                    int deltax = (this.Width - 3) / (int)this.mPage.CountColumns;
                    int deltay = (this.Height - 3) / (int)this.mPage.CountLines;

                    if (deltax < 3 || deltay < 3)
                        return;

                    int pos_ligne = 3;
                    int pos_colonne;
                    for (int count_ligne = 0; count_ligne < this.mPage.CountLines; ++count_ligne)
                    {
                        pos_colonne = 3;
                        for (int count_colonne = 0; count_colonne < this.mPage.CountColumns; ++count_colonne)
                        {
                            if (((pos_colonne + deltax) >= this.startx && pos_colonne < this.PointToClient(Control.MousePosition).X) &&
                                ((pos_ligne + deltay) >= this.starty && pos_ligne < this.PointToClient(Control.MousePosition).Y))
                                this.cases[count_ligne, count_colonne].Select();
                            pos_colonne += deltax;
                        }
                        pos_ligne += deltay;
                    }
                    this.Invalidate();
                }
            }
        }

        public void Confirm()
        {
            this.mPage.MakeZones(this.list);
        }
    }
}
