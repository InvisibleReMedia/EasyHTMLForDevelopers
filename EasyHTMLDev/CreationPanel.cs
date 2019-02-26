using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class CreationPanel : UserControl
    {
        uint countColumns, countLines;
        private Case[,] cases;
        private bool select;
        private int startx;
        private int starty;
        protected bool started = false;
        protected List<Library.AreaSizedRectangle> list = new List<Library.AreaSizedRectangle>();

        public CreationPanel()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public CreationPanel(uint countColumns, uint countLines)
        {
            this.countColumns = countColumns;
            this.countLines = countLines;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public uint CountColumns
        {
            get
            {
                return this.countColumns;
            }
            set
            {
                this.countColumns = value;
            }
        }

        public uint CountLines
        {
            get
            {
                return this.countLines;
            }
            set
            {
                this.countLines = value;
            }
        }

        public List<Library.AreaSizedRectangle> List
        {
            get { return this.list; }
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
            return new Point((int)(p.X * (double)this.CountColumns / (double)(this.Width - 3)), (int)(p.Y * (double)this.CountLines / (double)(this.Height - 3)));
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
            int deltax = (width - 3) / (int)this.CountColumns;
            int deltay = (height - 3) / (int)this.CountLines;

            if (deltax < 3 || deltay < 3)
                return;

            int pos_ligne = 3;
            int pos_colonne;
            for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
            {
                pos_colonne = 3;
                for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                {
                    resize_case(count_ligne, count_colonne, new Rectangle(pos_colonne + 1, pos_ligne + 1, deltax, deltay));
                    pos_colonne += deltax;
                }
                pos_ligne += deltay;
            }
        }

        public void initialize_cases()
        {
            this.cases = new Case[this.CountLines, this.CountColumns];
            for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
            {
                for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                {
                    this.cases[count_ligne, count_colonne] = new Case(count_ligne, count_colonne);
                }
            }
            this.calculeSize(this.Width, this.Height);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            if (started)
            {
                this.calculeSize(width, height);
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (started)
            {
                base.OnPaint(e);
                e.Graphics.FillRectangle(Brushes.DarkGray, new RectangleF(e.Graphics.VisibleClipBounds.Location, e.Graphics.VisibleClipBounds.Size));
                e.Graphics.Clip = new Region(new RectangleF(e.Graphics.VisibleClipBounds.Location, e.Graphics.VisibleClipBounds.Size));
                for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                {
                    for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                    {
                        this.cases[count_ligne, count_colonne].Draw(e.Graphics);
                    }
                }

                foreach (Library.AreaSizedRectangle r in this.list)
                {
                    Rectangle rect = new Rectangle(r.Left, r.Top, r.Width, r.Height);
                    e.Graphics.FillRectangle(Brushes.BlueViolet, rect);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                }
            }
        }

        private void RunMouseDown(object sender, MouseEventArgs e)
        {
            if (started)
            {

                this.Capture = true;
                this.select = true;
                this.startx = this.PointToClient(Control.MousePosition).X;
                this.starty = this.PointToClient(Control.MousePosition).Y;
            }
        }

        private void RunMouseUp(object sender, MouseEventArgs e)
        {
            if (started)
            {

                try
                {
                    if (select)
                    {
                        this.select = false;


                        int deltax = (this.Width - 3) / (int)this.CountColumns;
                        int deltay = (this.Height - 3) / (int)this.CountLines;

                        if (deltax < 3 || deltay < 3)
                            return;

                        bool cancel = false;
                        int pos_ligne = 3;
                        int pos_colonne;
                        for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                        {
                            pos_colonne = 3;
                            for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                            {
                                if (((pos_colonne + deltax) >= this.startx && pos_colonne < this.PointToClient(Control.MousePosition).X) &&
                                    ((pos_ligne + deltay) >= this.starty && pos_ligne < this.PointToClient(Control.MousePosition).Y))
                                {
                                    if (!this.cases[count_ligne, count_colonne].IsDisabled)
                                        this.cases[count_ligne, count_colonne].Disable();
                                    else
                                        cancel = true;
                                }
                                pos_colonne += deltax;
                            }
                            pos_ligne += deltay;
                        }

                        Point start = this.RevertCoordinates(new Point(this.startx, this.starty));
                        Point end = this.RevertCoordinates(this.PointToClient(Control.MousePosition));

                        if (!cancel)
                        {
                            if (start.X >= 0 && start.X < this.CountColumns && end.X >= 0 && end.X < this.CountColumns &&
                                start.Y >= 0 && start.Y < this.CountLines && end.Y >= 0 && end.Y < this.CountLines)
                                this.list.Add(new Library.AreaSizedRectangle(deltax * (end.X - start.X + 1), deltay * (end.Y - start.Y + 1), end.X - start.X + 1, end.Y - start.Y + 1, deltax * start.X, deltay * start.Y));
                        }

                        for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                        {
                            for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
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
        }

        private void RunMouseMove(object sender, MouseEventArgs e)
        {
            if (started)
            {
                if (this.select)
                {
                    for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                    {
                        for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                        {
                            if (!this.cases[count_ligne, count_colonne].IsDisabled)
                                this.cases[count_ligne, count_colonne].Unselect();
                        }
                    }
                    int deltax = (this.Width - 3) / (int)this.CountColumns;
                    int deltay = (this.Height - 3) / (int)this.CountLines;

                    if (deltax < 3 || deltay < 3)
                        return;

                    int pos_ligne = 3;
                    int pos_colonne;
                    for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                    {
                        pos_colonne = 3;
                        for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                        {
                            if (((pos_colonne + deltax) >= this.startx && pos_colonne < this.PointToClient(Control.MousePosition).X) &&
                                ((pos_ligne + deltay) >= this.starty && pos_ligne < this.PointToClient(Control.MousePosition).Y))
                                if (!this.cases[count_ligne, count_colonne].IsDisabled)
                                    this.cases[count_ligne, count_colonne].Select();
                            pos_colonne += deltax;
                        }
                        pos_ligne += deltay;
                    }
                    this.Invalidate();
                }
            }
        }



        internal void Start()
        {
            this.started = true;
        }
    }
}
