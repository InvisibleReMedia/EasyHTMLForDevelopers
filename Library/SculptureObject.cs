using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class SculptureObject : IProjectElement, ICloneable
    {
        #region Private Fields
        private string title = String.Empty;
        private List<CadreModel> cadres = new List<CadreModel>();
        private List<GeneratedSculpture> generated = new List<GeneratedSculpture>();
        private List<DistanceCadreModel> distances = new List<DistanceCadreModel>();
        #endregion

        #region Default Constructor
        public SculptureObject() { }
        #endregion

        #region Copy Constructor
        private SculptureObject(SculptureObject obj)
        {
            this.title = ExtensionMethods.CloneThis(obj.title);
            foreach (CadreModel cm in obj.cadres)
            {
                this.cadres.Add(cm.Clone() as CadreModel);
            }
            foreach (GeneratedSculpture gs in obj.generated)
            {
                this.generated.Add(gs.Clone() as GeneratedSculpture);
            }
            foreach (DistanceCadreModel d in obj.distances)
            {
                this.distances.Add(d.Clone() as DistanceCadreModel);
            }
        }
        #endregion

        #region Public Properties
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public List<CadreModel> Cadres
        {
            get { return this.cadres; }
        }

        public List<GeneratedSculpture> Generated
        {
            get { return this.generated; }
        }
        #endregion

        #region Private Methods
        private void cm_Updated(object sender, CadreIndexArgs e)
        {
            DistanceCadreModel.UpdateDistance(this.distances, this.cadres, e.CadreModel);
        }

        private HashSet<Granne> GenerateGrannes()
        {
            Granne.Init();
            Granne.ComputeGranne(this.cadres);
            HashSet<Granne> grannes = new HashSet<Granne>();
            foreach (CadreModel c in this.cadres)
            {
                Granne.SetTrueRect(c.HorizontalPosition, c.VerticalPosition, c.HorizontalPosition + c.Largeur, c.VerticalPosition + c.Hauteur);
            }
            foreach (CadreModel c in this.cadres)
            {
                grannes.Add(new Granne(c, c.HorizontalPosition, c.VerticalPosition, c.Largeur, c.Hauteur));
            }
            return grannes;
        }

        private SortedSet<Granne> SortGranne(HashSet<Granne> hs)
        {
            SortedSet<Granne> sortedV = new SortedSet<Granne>(hs, new VerticalGranneComparer());
            return new SortedSet<Granne>(sortedV, new HorizontalGranneComparer());
        }

        private void FillObjects(Project proj, GeneratedSculpture gs, Granne[,] tab)
        {
            List<SizedRectangle> list = new List<SizedRectangle>();
            gs.RemainerModels.Clear();
            gs.Objects.Clear();
            for (int y = 0; y < tab.GetLength(1); ++y)
            {
                for (int x = 0; x < tab.GetLength(0); ++x)
                {
                    if (tab[x, y] != null)
                    {
                        gs.GenerateContent(list, tab[x, y]);
                        tab[x, y].InsertModel(gs.RemainerModels);
                        gs.Objects.Add(tab[x, y].CreateRefObject(proj));
                    }
                }
            }
            gs.GenerateContent(list);
        }
        #endregion

        #region Public Methods
        public List<Rectangle> GetCrossRectList(CadreModel cm)
        {
            return DistanceCadreModel.CrossRect(this.cadres, this.distances, cm);
        }

        public List<GeneratedSculpture> GetPreviousGeneration(string type)
        {
            return (from GeneratedSculpture g in this.generated where g.Type == type select g).ToList();
        }

        public void Reinit()
        {
            foreach (CadreModel cm in this.cadres)
            {
                cm.Updated += cm_Updated;
            }
        }

        public void AddNewCadreModel(CadreModel cm)
        {
            DistanceCadreModel.AddNewCadre(this.distances, this.cadres, cm);
            cm.Updated += cm_Updated;
            this.cadres.Add(cm);
        }

        public void CreateProjectObject(Project proj, GeneratedSculpture gs)
        {
            Granne.Init();
            SortedSet<Granne> set = this.SortGranne(this.GenerateGrannes());
            uint trueWidth = 0, trueHeight = 0;
            uint countX = 0, countY = 0;
            if (Granne.TrueRectangle.Width > 0) trueWidth = (uint)Granne.TrueRectangle.Width;
            if (Granne.TrueRectangle.Height > 0) trueHeight = (uint)Granne.TrueRectangle.Height;
            if (Granne.Size.Width > 0) countX = (uint)Granne.Size.Width;
            if (Granne.Size.Height > 0) countY = (uint)Granne.Size.Height;
            Granne[,] tab2D = new Granne[countX, countY];
            foreach (Granne g in set)
            {
                g.InsertIntoArray(tab2D);
            }
            gs.CreateDestination(proj, trueWidth, trueHeight, countX, countY);
            this.FillObjects(proj, gs, tab2D);
        }
        #endregion

        public string TypeName
        {
            get { return "SculptureObject"; }
        }

        public string ElementTitle
        {
            get { return this.title; }
        }

        public object Clone()
        {
            return new SculptureObject(this);
        }
    }

    [Serializable]
    internal class DistanceCadreModel : IEqualityComparer<DistanceCadreModel>, ICloneable
    {
        #region Private Fields
        private int distX, distY; // distance x et y entre deux coins supérieurs gauche de 2 rectangles
        private int posX, posY; // point complémentaire d'un des deux rectangles
        private int crossX, crossY; // distance séparant le premier rectangle du second
        private int refCadreModelIndexPositive;
        private int refCadreModelIndexNegative;
        #endregion

        #region Public Constructors
        public DistanceCadreModel() : this(0, 0, 0, 0, 0, 0, 0, 0)
        {
        }

        public DistanceCadreModel(int dx, int dy, int px, int py, int cx, int cy, int positiveIndex, int negativeIndex)
        {
            this.distX = dx;
            this.distY = dy;
            this.posX = px;
            this.posY = py;
            this.crossX = cx;
            this.crossY = cy;
            this.refCadreModelIndexPositive = positiveIndex;
            this.refCadreModelIndexNegative = negativeIndex;
        }

        public DistanceCadreModel(CadreModel cm1, CadreModel cm2)
        {
            if (cm1.Index > cm2.Index)
            {
                this.Uniform(cm2, cm1);
                this.AimentedContact(cm2, cm1);
                cm2.AimentedMove();
            }
            else
            {
                this.Uniform(cm1, cm2);
                this.AimentedContact(cm1, cm2);
                cm1.AimentedMove();
            }
        }
        #endregion

        #region Private Methods
        private void Uniform(CadreModel cm2, CadreModel cm1)
        {
            int dx = cm2.HorizontalPosition - cm1.HorizontalPosition;
            int dy = cm2.VerticalPosition - cm1.VerticalPosition;
            int px = 0, py = 0;
            int cx, cy;
            px = cm1.Largeur;
            if (dx > 0)
            {// cm2 est plus loin que cm1
                cx = cm1.HorizontalPosition + cm1.Largeur - cm2.HorizontalPosition; // si cx > 0 alors il y a un chevauchement
            }
            else if (dx < 0)
            {   // cm1 est de l'autre côté de cm2
                cx = cm2.HorizontalPosition + cm2.Largeur - cm1.HorizontalPosition; // si cx > 0 alors cm chevauche cmNew
            }
            else
            { // cm1 et cm2 commencent en même temps
                cx = cm2.Largeur - cm1.Largeur;
            }
            py = cm1.Hauteur;
            if (dy > 0)
            {
                cy = cm1.VerticalPosition + cm1.Hauteur - cm2.VerticalPosition; // si cy > 0 alors cmNew chevauche cm
                if (cy < 0 && (!cm2.VerticalContact.HasValue || cm2.VerticalContact.Value < cy))
                {
                    cm2.VerticalContact = cy;
                }
            }
            else if (dy < 0)
            {
                cy = cm2.VerticalPosition + cm2.Hauteur - cm1.VerticalPosition;
                if (cy < 0 && (!cm2.VerticalContact.HasValue || cm2.VerticalContact.Value < cy))
                {
                    cm2.VerticalContact = cy;
                }
            }
            else
            {
                cy = cm2.Hauteur - cm1.Hauteur;
                if (cy < 0 && (!cm2.VerticalContact.HasValue || cm2.VerticalContact.Value < cy))
                {
                    cm2.VerticalContact = cy;
                }
            }
            this.distX = dx;
            this.distY = dy;
            this.posX = px;
            this.posY = py;
            this.crossX = cx;
            this.crossY = cy;
            this.refCadreModelIndexPositive = cm2.Index;
            this.refCadreModelIndexNegative = cm1.Index;
        }

        private System.Drawing.Point NegativeIntersectionBy(int dist, int pos, int cross, int before, int after)
        {
            int start = 0;
            int end = 0;
            if (dist > 0) // z - cm > 0
            {
                if (cross > 0) // chevauchement de cm sur z
                {
                    start = before - dist;
                    end = before + pos - dist;
                }
            }
            else if (dist < 0) // cm - z > 0
            {
                if (cross >= 0) // chevauchement de z sur cm
                {
                    start = before - dist;
                    end = before + pos - dist;
                }
            }
            else
            {
                start = before;
                end = after;
            }
            return new System.Drawing.Point(start - before, end - before);
        }

        private void AimentedContact(CadreModel cm2, CadreModel cm1)
        {
            if (this.crossX < 0 && (!cm2.HorizontalContact.HasValue || cm2.HorizontalContact.Value < this.crossX))
            {
                cm2.HorizontalContact = this.crossX;
            }
            if (this.crossY < 0 && (!cm2.VerticalContact.HasValue || cm2.VerticalContact.Value < this.crossY))
            {
                cm2.VerticalContact = this.crossY;
            }
        } 
        #endregion

        #region Public Methods
        public Rectangle HasIntersection(List<CadreModel> src, CadreModel cm)
        {
            if (cm.Index == this.refCadreModelIndexPositive)
            {
                System.Drawing.Point p1 = this.NegativeIntersectionBy(this.distX, this.posX, this.crossX, cm.HorizontalPosition, cm.HorizontalPosition + cm.Largeur);
                System.Drawing.Point p2 = this.NegativeIntersectionBy(this.distY, this.posY, this.crossY, cm.VerticalPosition, cm.VerticalPosition + cm.Hauteur);
                return new Rectangle(p1.X, p1.Y, p2.X, p2.Y);
            }
            else if (cm.Index == this.refCadreModelIndexNegative)
            {
                CadreModel cm2 = DistanceCadreModel.Find(src, this.refCadreModelIndexPositive);
                return this.HasIntersection(src, cm2);
            }
            return new Rectangle();
        }
        #endregion

        #region Public Static Methods
        public static string Dump(List<DistanceCadreModel> list)
        {
            string output = String.Empty;
            foreach (DistanceCadreModel d in list)
            {
                output += d.ToString() + Environment.NewLine;
            }
            return output;
        }

        public static List<Rectangle> CrossRect(List<CadreModel> src, List<DistanceCadreModel> list, CadreModel cm)
        {
            return DistanceCadreModel.FindIntersect(list, src, cm);
        }

        public static List<Rectangle> FindIntersect(List<DistanceCadreModel> list, List<CadreModel> src, CadreModel cm)
        {
            return list.Select(a =>
            {
                if (a.refCadreModelIndexPositive == cm.Index && a.refCadreModelIndexNegative > cm.Index)
                {
                    Rectangle r = a.HasIntersection(src, cm);
                    if (!r.IsEmpty()) return r;
                }
                else if (a.refCadreModelIndexNegative == cm.Index && a.refCadreModelIndexPositive > cm.Index)
                {
                    Rectangle r = a.HasIntersection(src, cm);
                    if (!r.IsEmpty()) return r;
                }
                return new Rectangle();
            }).ToList();
        }

        public static CadreModel Find(List<CadreModel> src, int index)
        {
            return src.Single(a => { return a.Index == index; });
        }

        public static void UpdateDistance(List<DistanceCadreModel> list, List<CadreModel> src, CadreModel cmChanged)
        {
            list.FindAll(a =>
            {
                if (a.refCadreModelIndexPositive == cmChanged.Index || a.refCadreModelIndexNegative == cmChanged.Index) return true; else return false;
            }).ForEach(a =>
            {
                if (a.refCadreModelIndexPositive == cmChanged.Index)
                {
                    CadreModel cm = DistanceCadreModel.Find(src, a.refCadreModelIndexNegative);
                    a.Uniform(cmChanged, cm);
                    a.AimentedContact(cmChanged, cm);
                    cmChanged.AimentedMove();
                }
                else
                {
                    CadreModel cm = DistanceCadreModel.Find(src, a.refCadreModelIndexPositive);
                    a.Uniform(cm, cmChanged);
                    a.AimentedContact(cm, cmChanged);
                    cm.AimentedMove();
                }
            });
        }

        public static void AddNewCadre(List<DistanceCadreModel> list, List<CadreModel> src, CadreModel cmNew)
        {
            foreach (CadreModel cm in src)
            {
                list.Add(new DistanceCadreModel(cm, cmNew));
            }
        }

        public static void RemoveCadre(List<DistanceCadreModel> list, CadreModel cmToRemove)
        {
            IEnumerable<DistanceCadreModel> toRemove = list.TakeWhile(a => { return a.refCadreModelIndexPositive == cmToRemove.Index || a.refCadreModelIndexNegative == cmToRemove.Index; });
            for (int index = list.Count - 1; index >= 0; --index)
            {
                DistanceCadreModel d = list[index];
                if (toRemove.Contains(d, new DistanceCadreModel()))
                {
                    list.Remove(d);
                    --index;
                }
            }
        }
        #endregion

        #region IEqualityComparer Members
        public bool Equals(DistanceCadreModel x, DistanceCadreModel y)
        {
            return (x.refCadreModelIndexPositive == y.refCadreModelIndexPositive && x.refCadreModelIndexNegative == y.refCadreModelIndexNegative);
        }

        public int GetHashCode(DistanceCadreModel obj)
        {
            try
            {
                double d = Math.Round(Math.Sqrt(Math.Pow(obj.distX, 2) + Math.Pow(obj.distY, 2)), 5);
                return Convert.ToInt32(d * Math.Pow(10, 5));
            }
            catch (OverflowException)
            {
                return Int32.MaxValue;
            }
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            string output = "(" + this.refCadreModelIndexPositive + "," + this.refCadreModelIndexNegative + ") = ";
            output += "{" + "dist.x=" + this.distX + ", dist.y=" + this.distY + ", pos.x=" + this.posX + ", pos.y=" + this.posY + ", cross.x=" + this.crossX + ", cross.y=" + this.crossY + "}";
            return output;
        }
        #endregion

        public object Clone()
        {
            return new DistanceCadreModel(this.distX, this.distY, this.posX, this.posY, this.crossX, this.crossY, this.refCadreModelIndexPositive, this.refCadreModelIndexNegative);
        }
    }

    public class CadreIndexArgs : EventArgs
    {
        #region Private Fields
        private CadreModel cm;
        #endregion

        #region Public Constructor
        public CadreIndexArgs(CadreModel cm)
        {
            this.cm = cm;
        }
        #endregion

        #region Public Properties
        public CadreModel CadreModel
        { get { return this.cm; } }
        #endregion
    }

    public class CadreIndexPaintArgs : EventArgs
    {
        #region Private Fields
        private CadreModel cm;
        private System.Windows.Forms.PaintEventArgs p;
        #endregion

        #region Public Constructor
        public CadreIndexPaintArgs(CadreModel cm, System.Windows.Forms.PaintEventArgs p)
        {
            this.cm = cm;
            this.p = p;
        }
        #endregion

        #region Public Properties
        public CadreModel CadreModel
        {
            get { return this.cm; }
        }

        public System.Windows.Forms.PaintEventArgs Paint
        {
            get { return this.p; }
        }
        #endregion
    }
}
