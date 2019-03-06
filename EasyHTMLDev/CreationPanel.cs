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
        }

        public CreationPanel(uint countColumns, uint countLines)
        {
            this.countColumns = countColumns;
            this.countLines = countLines;
            InitializeComponent();
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

        /// <summary>
        /// Gets inner width pour avoir une zone franche
        /// </summary>
        public int InnerWidth
        {
            get
            {
                return this.Width - (this.Width % (int)this.CountColumns);
            }
        }

        /// <summary>
        /// Gets innner height pour avoir une zone franche
        /// </summary>
        public int InnerHeight
        {
            get
            {
                return this.Height - (this.Height % (int)this.CountLines);
            }
        }

        public List<Library.AreaSizedRectangle> List
        {
            get { return this.list; }
        }

        /// <summary>
        /// methode pour retrouver le numéro de case
        /// en fonction de la position sur l'écran du coin inférieur gauche de la case
        /// </summary>
        /// <param name="p">point de coordonnées écran</param>
        /// <returns>point de coordonnées dans la grille</returns>
        public Point RevertCoordinates(Point p)
        {
            return new Point((int)(p.X * (double)this.CountColumns / (double)this.InnerWidth), (int)(p.Y * (double)this.CountLines / (double)this.InnerHeight));
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

        internal void calculeSize(int width, int height)
        {
            int deltax = width / (int)this.CountColumns;
            int deltay = height / (int)this.CountLines;

            if (deltax < 3 || deltay < 3)
                return;

            int pos_ligne = 0;
            int pos_colonne;
            for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
            {
                pos_colonne = 0;
                for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                {
                    resize_case(count_ligne, count_colonne, new Rectangle(pos_colonne, pos_ligne, deltax, deltay));
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
            this.calculeSize(this.InnerWidth, this.InnerHeight);
        }

        public void resize()
        {
            if (started)
            {
                this.calculeSize(this.InnerWidth, this.InnerHeight);
                foreach (Library.AreaSizedRectangle a in this.list)
                {
                    a.Resize(this.InnerWidth / (double)this.CountColumns, this.InnerHeight / (double)this.CountLines);
                }
                this.Invalidate();
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (this.list.Count > 0)
                {
                    this.list.RemoveAt(this.list.Count - 1);
                    for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                    {
                        for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
                        {
                            this.cases[count_ligne, count_colonne].Activate();
                        }
                    }
                    this.Invalidate();
                }
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


                        int deltax = InnerWidth / (int)this.CountColumns;
                        int deltay = InnerHeight / (int)this.CountLines;

                        if (deltax < 3 || deltay < 3)
                            return;

                        bool cancel = false;
                        int pos_ligne = 0;
                        int pos_colonne;
                        for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                        {
                            pos_colonne = 0;
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
                            {
                                Library.AreaSizedRectangle a = new Library.AreaSizedRectangle(deltax * (end.X - start.X + 1), deltay * (end.Y - start.Y + 1), end.X - start.X + 1, end.Y - start.Y + 1, deltax * start.X, deltay * start.Y);
                                a.StartWidth = start.X;
                                a.StartHeight = start.Y;
                                this.list.Add(a);
                            }
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
                            this.cases[count_ligne, count_colonne].Unselect();
                        }
                    }
                    int deltax = this.InnerWidth / (int)this.CountColumns;
                    int deltay = this.InnerHeight / (int)this.CountLines;

                    if (deltax < 3 || deltay < 3)
                        return;

                    int pos_ligne = 0;
                    int pos_colonne;
                    for (int count_ligne = 0; count_ligne < this.CountLines; ++count_ligne)
                    {
                        pos_colonne = 0;
                        for (int count_colonne = 0; count_colonne < this.CountColumns; ++count_colonne)
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



        internal void Start()
        {
            this.started = true;
        }
    }
}
