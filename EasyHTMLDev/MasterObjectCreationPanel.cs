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
        private Case[,] cases;
        private Library.MasterObject mObject;
        private bool select;
        private int startx;
        private int starty;
        private List<Rectangle> list = new List<Rectangle>();

        public MasterObjectCreationPanel()
        {
            InitializeComponent();
        }

        public Library.MasterObject MasterObject
        {
            get { return this.mObject; }
            set { this.mObject = value; }
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
            return new Point((int)(p.X * (int)this.mObject.CountColumns / (double)(this.Width - 3)), (int)(p.Y * (double)this.mObject.CountLines / (double)(this.Height - 3)));
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
            int deltax = (width - 3) / (int)this.mObject.CountColumns;
            int deltay = (height - 3) / (int)this.mObject.CountLines;

            if (deltax < 3 || deltay < 3)
                return;

            int pos_ligne = 3;
            int pos_colonne;
            for (int count_ligne = 0; count_ligne < this.mObject.CountLines; ++count_ligne)
            {
                pos_colonne = 3;
                for (int count_colonne = 0; count_colonne < this.mObject.CountColumns; ++count_colonne)
                {
                    resize_case(count_ligne, count_colonne, new Rectangle(pos_colonne+1, pos_ligne + 1, deltax, deltay));
                    pos_colonne += deltax;
                }
                pos_ligne += deltay;
            }
        }

        public void initialize_cases()
        {
            if (this.mObject != null)
            {
                this.cases = new Case[this.mObject.CountLines, this.mObject.CountColumns];
                for (int count_ligne = 0; count_ligne < this.mObject.CountLines; ++count_ligne)
                {
                    for (int count_colonne = 0; count_colonne < this.mObject.CountColumns; ++count_colonne)
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
            if (this.mObject != null)
            {
                this.calculeSize(width, height);
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.mObject != null)
            {
                e.Graphics.FillRectangle(Brushes.Gray, new RectangleF(e.Graphics.VisibleClipBounds.Location, e.Graphics.VisibleClipBounds.Size));
                e.Graphics.Clip = new Region(new RectangleF(e.Graphics.VisibleClipBounds.Location, e.Graphics.VisibleClipBounds.Size));
                for (int count_ligne = 0; count_ligne < this.mObject.CountLines; ++count_ligne)
                {
                    for (int count_colonne = 0; count_colonne < this.mObject.CountColumns; ++count_colonne)
                    {
                        this.cases[count_ligne, count_colonne].Draw(e.Graphics);
                    }
                }
            }
            foreach (Rectangle r in this.list)
            {
                Rectangle rectStart = this.getRectangle(r.Y, r.X);
                Rectangle rectEnd = this.getRectangle(r.Y + r.Height, r.X + r.Width);
                Rectangle zoneRect = new Rectangle(rectStart.X, rectStart.Y, rectEnd.Right - rectStart.X, rectEnd.Bottom - rectStart.Y);
                e.Graphics.FillRectangle(Brushes.Azure, zoneRect);
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


                    int deltax = (this.Width - 3) / (int)this.mObject.CountColumns;
                    int deltay = (this.Height - 3) / (int)this.mObject.CountLines;

                    if (deltax < 3 || deltay < 3)
                        return;

                    int pos_ligne = 3;
                    int pos_colonne;
                    for (int count_ligne = 0; count_ligne < this.mObject.CountLines; ++count_ligne)
                    {
                        pos_colonne = 3;
                        for (int count_colonne = 0; count_colonne < this.mObject.CountColumns; ++count_colonne)
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

                    if (start.X >= 0 && start.X < this.mObject.CountColumns && end.X >= 0 && end.X < this.mObject.CountColumns &&
                        start.Y >= 0 && start.Y < this.mObject.CountLines && end.Y >= 0 && end.Y < this.mObject.CountLines)
                        this.list.Add(new Rectangle(new Point(start.X, start.Y), new Size(end.X - start.X, end.Y - start.Y)));

                    for (int count_ligne = 0; count_ligne < this.mObject.CountLines; ++count_ligne)
                    {
                        for (int count_colonne = 0; count_colonne < this.mObject.CountColumns; ++count_colonne)
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
            if (this.mObject != null)
            {
                if (this.select)
                {
                    for (int count_ligne = 0; count_ligne < this.mObject.CountLines; ++count_ligne)
                    {
                        for (int count_colonne = 0; count_colonne < this.mObject.CountColumns; ++count_colonne)
                        {
                            this.cases[count_ligne, count_colonne].Unselect();
                        }
                    }
                    int deltax = (this.Width - 3) / (int)this.mObject.CountColumns;
                    int deltay = (this.Height - 3) / (int)this.mObject.CountLines;

                    if (deltax < 3 || deltay < 3)
                        return;

                    int pos_ligne = 3;
                    int pos_colonne;
                    for (int count_ligne = 0; count_ligne < this.mObject.CountLines; ++count_ligne)
                    {
                        pos_colonne = 3;
                        for (int count_colonne = 0; count_colonne < this.mObject.CountColumns; ++count_colonne)
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
            Nullable<Size>[,] indexes = new Nullable<Size>[this.mObject.CountLines, this.mObject.CountColumns];
            for (int index = 0; index < this.list.Count; ++index)
            {
                Rectangle current = this.list[index];
                indexes[current.Y, current.X] = new Size(current.Width, current.Height);
            }

            // ranger les données dans la master page
            for (int pos_ligne = 0; pos_ligne < this.mObject.CountLines; ++pos_ligne)
            {
                Nullable<int> minCountLines = null;
                Library.HorizontalZone hz = new Library.HorizontalZone();
                hz.ConstraintWidth = this.mObject.ConstraintWidth;
                hz.ConstraintHeight = this.mObject.ConstraintHeight;
                for (int pos_colonne = 0; pos_colonne < this.mObject.CountColumns; ++pos_colonne)
                {
                    if (indexes[pos_ligne, pos_colonne].HasValue)
                    {
                        Library.VerticalZone vz = new Library.VerticalZone();
                        vz.ConstraintWidth = this.mObject.ConstraintWidth;
                        vz.ConstraintHeight = this.mObject.ConstraintHeight;
                        vz.CountColumns = indexes[pos_ligne, pos_colonne].Value.Width + 1;
                        vz.CountLines = indexes[pos_ligne, pos_colonne].Value.Height + 1;
                        vz.Height = (uint)((vz.CountLines / (double)this.mObject.CountLines) * (double)this.mObject.Height);
                        vz.Width = (uint)((vz.CountColumns / (double)this.mObject.CountColumns) * (double)this.mObject.Width);
                        if (minCountLines.HasValue)
                        {
                            if (minCountLines.Value > vz.CountLines)
                            {
                                minCountLines = vz.CountLines;
                            }
                        }
                        else
                        {
                            minCountLines = vz.CountLines;
                        }
                        if (hz == null)
                            hz = new Library.HorizontalZone();
                        hz.VerticalZones.Add(vz);
                    }
                }
                if (hz != null)
                {
                    if (minCountLines.HasValue)
                        hz.CountLines = minCountLines.Value;
                    else
                        hz.CountLines = 0;
                    hz.Width = this.mObject.Width;
                    hz.Height = (uint)((hz.CountLines / (double)this.mObject.CountLines) * (double)this.mObject.Height);
                    // cette longueur et hauteur servira pour calculer le resize des zones verticales
                    hz.ConstraintWidth = Library.EnumConstraint.AUTO;
                    hz.ConstraintHeight = Library.EnumConstraint.AUTO;
                    this.mObject.HorizontalZones.Add(hz);
                }
            }

            /*            // compter le nombre de zones horizontales
                        uint countHoriz = (uint)this.mPage.HorizontalZones.Count;
                        // compter le nombre maximum de zones verticales
                        uint maxCountVert = 0;
                        foreach (Library.HorizontalZone horiz in this.mPage.HorizontalZones)
                        {
                            if (horiz.VerticalZones.Count > maxCountVert)
                            {
                                maxCountVert = (uint)horiz.VerticalZones.Count;
                            }
                        }
                        // calculer chaque colspan/rowspan pour chaque colonne
                        foreach (Library.HorizontalZone horiz in this.mPage.HorizontalZones)
                        {
                            Nullable<int> minCountLines = null;
                            foreach (Library.VerticalZone vert in horiz.VerticalZones)
                            {
                                vert.Width = (int)((vert.CountColumns / (double)this.mPage.CountColumns) * (double)this.mPage.Width);
                                vert.CountColumns = (int)Math.Ceiling((vert.CountColumns / (double)this.mPage.CountColumns) * (double)maxCountVert);
                                if (vert.CountColumns == 0) vert.CountColumns = 1;
                                vert.Height = (int)((vert.CountLines / (double)this.mPage.CountLines) * (double)this.mPage.Height);
                                vert.CountLines = (int)((vert.CountLines / (double)this.mPage.CountLines) * (double)countHoriz);
                                if (vert.CountLines == 0) vert.CountLines = 1;
                                if (minCountLines.HasValue)
                                {
                                    if (minCountLines > vert.CountLines)
                                    {
                                        minCountLines = vert.CountLines;
                                    }
                                }
                                else
                                {
                                    minCountLines = vert.CountLines;
                                }
                            }
                            horiz.CountLines = minCountLines.Value;
                        }
                        // positionner la taille totale
                        this.mPage.CountLines = countHoriz;
                        this.mPage.CountColumns = maxCountVert;
            */
        }
    }
}
