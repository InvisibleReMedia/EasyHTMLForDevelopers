using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Sculpture object definition
    /// </summary>
    [Serializable]
    public class SculptureObject : Marshalling.PersistentDataObject, IProjectElement, ICloneable
    {

        #region Protected Fields

        /// <summary>
        /// Index name for unique id
        /// </summary>
        protected static readonly string uniqueName = "unique";
        /// <summary>
        /// Index name for title
        /// </summary>
        protected static readonly string titleName = "title";
        /// <summary>
        /// Index name for cadres
        /// </summary>
        protected static readonly string cadreListName = "cadres";
        /// <summary>
        /// Index name for generated sculptures
        /// </summary>
        protected static readonly string generatedSculptureListName = "generated";
        /// <summary>
        /// Index name for distances
        /// </summary>
        protected static readonly string distancesName = "distances";

        #endregion

        #region Default Constructor

        /// <summary>
        /// Empty constructor
        /// </summary>
        public SculptureObject() { }
        
        #endregion

        #region Copy Constructor

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="obj">object source to copy</param>
        private SculptureObject(SculptureObject obj)
        {
            this.Title = ExtensionMethods.CloneThis(obj.Title);
            foreach (CadreModel cm in obj.Cadres)
            {
                this.Cadres.Add(cm.Clone() as CadreModel);
            }
            foreach (GeneratedSculpture gs in obj.Generated)
            {
                this.Generated.Add(gs.Clone() as GeneratedSculpture);
            }
            foreach (DistanceCadreModel d in obj.Distances)
            {
                this.Distances.Add(d.Clone() as DistanceCadreModel);
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique id
        /// </summary>
        public string Unique
        {
            get { return this.Get(uniqueName); }
            set { this.Set(uniqueName, value); }
        }

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string TypeName
        {
            get { return "SculptureObject"; }
        }

        /// <summary>
        /// Gets the element title
        /// </summary>
        public string ElementTitle
        {
            get { return this.Title; }
        }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title
        {
            get { return this.Get(titleName, ""); }
            set { this.Set(titleName, value); }
        }

        /// <summary>
        /// Gets cadres
        /// </summary>
        public List<CadreModel> Cadres
        {
            get { return this.Get(cadreListName, new List<CadreModel>()); }
        }

        /// <summary>
        /// Gets generated sculptures
        /// </summary>
        public List<GeneratedSculpture> Generated
        {
            get { return this.Get(generatedSculptureListName, new List<GeneratedSculpture>()); }
        }

        /// <summary>
        /// Gets the distances
        /// </summary>
        internal List<DistanceCadreModel> Distances
        {
            get
            {
                return this.Get(distancesName, new List<DistanceCadreModel>());
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Cadre model updater
        /// </summary>
        /// <param name="sender">source sender</param>
        /// <param name="e">arguments</param>
        private void cm_Updated(object sender, CadreIndexArgs e)
        {
            DistanceCadreModel.UpdateDistance(this.Distances, this.Cadres, e.CadreModel);
        }

        /// <summary>
        /// Generate grannes
        /// </summary>
        /// <returns>hash set of granne</returns>
        private HashSet<Granne> GenerateGrannes()
        {

            Granne.Init();
            Granne.ComputeGranne(this.Cadres);
            HashSet<Granne> grannes = new HashSet<Granne>();
            foreach (CadreModel c in this.Cadres)
            {
                Granne.SetTrueRect(c.WidthPosition, c.HeightPosition, c.WidthPosition + c.Width, c.HeightPosition + c.Height);
            }
            foreach (CadreModel c in this.Cadres)
            {
                grannes.Add(new Granne(c, c.WidthPosition, c.HeightPosition, c.Width, c.Height));
            }
            return grannes;
        }

        /// <summary>
        /// Sorting grannes
        /// </summary>
        /// <param name="hs">hash set</param>
        /// <returns>sorted grannes</returns>
        private SortedSet<Granne> SortGranne(HashSet<Granne> hs)
        {
            SortedSet<Granne> sortedV = new SortedSet<Granne>(hs, new VerticalGranneComparer());
            return new SortedSet<Granne>(sortedV, new HorizontalGranneComparer());
        }

        /// <summary>
        /// Fill objects
        /// </summary>
        /// <param name="proj">project to edit</param>
        /// <param name="gs">generated sculpture</param>
        /// <param name="tab">two dimensional tabular granne</param>
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

        /// <summary>
        /// Gets rectangle cross list
        /// </summary>
        /// <param name="cm">cadre model</param>
        /// <returns>list of rectangle</returns>
        public List<Rectangle> GetCrossRectList(CadreModel cm)
        {
            return DistanceCadreModel.CrossRect(this.Cadres, this.Distances, cm);
        }

        /// <summary>
        /// Get the previous generation
        /// </summary>
        /// <param name="type">type of generation</param>
        /// <returns>list of generated sculpture</returns>
        public List<GeneratedSculpture> GetPreviousGeneration(string type)
        {
            return (from GeneratedSculpture g in this.Generated where g.Type == type select g).ToList();
        }

        /// <summary>
        /// Reinitialize
        /// </summary>
        public void Reinit()
        {
            foreach (CadreModel cm in this.Cadres)
            {
                cm.Updated += cm_Updated;
            }
        }

        /// <summary>
        /// Add a new cadre model
        /// </summary>
        /// <param name="cm">cadre model to add</param>
        public void AddNewCadreModel(CadreModel cm)
        {
            DistanceCadreModel.AddNewCadre(this.Distances, this.Cadres, cm);
            cm.Updated += cm_Updated;
            this.Cadres.Add(cm);
        }

        /// <summary>
        /// Create all objects for sculpture generation on a project
        /// </summary>
        /// <param name="proj">project to edit</param>
        /// <param name="gs">generated sculpture</param>
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

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new SculptureObject(this);
        }

        #endregion

    }

    /// <summary>
    /// Distance computed for a cadre model
    /// object that can be compared by equality
    /// </summary>
    [Serializable]
    internal class DistanceCadreModel : Marshalling.PersistentDataObject, IEqualityComparer<DistanceCadreModel>, ICloneable
    {

        #region Private Fields

        // distance x et y entre deux coins supérieurs gauche de 2 rectangles
        /// <summary>
        /// Index name for distance on x axis
        /// </summary>
        protected static readonly string distanceXName = "distX";
        /// <summary>
        /// Index name for distance on y axis
        /// </summary>
        protected static readonly string distanceYName = "distY";

        // point complémentaire d'un des deux rectangles
        /// <summary>
        /// Index name for position on x axis
        /// </summary>
        protected static readonly string positionXName = "posX";
        /// <summary>
        /// Index name for position on y axis
        /// </summary>
        protected static readonly string positionYName = "posY";
        // distance séparant le premier rectangle du second
        /// <summary>
        /// Index name for cross on x axis
        /// </summary>
        protected static readonly string crossXName = "crossX";
        /// <summary>
        /// Index name for cross on y axis
        /// </summary>
        protected static readonly string crossYName = "crossY";
        /// <summary>
        /// Index name for positive index
        /// </summary>
        protected static readonly string refCadreModelPositiveIndexName = "refCadreModelPositiveIndex";
        /// <summary>
        /// Index name for negative index
        /// </summary>
        protected static readonly string refCadreModelNegativeIndexName = "refCadreModelNegativeIndex";

        #endregion

        #region Public Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public DistanceCadreModel()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dx">distance x</param>
        /// <param name="dy">distance y</param>
        /// <param name="px">position x</param>
        /// <param name="py">position y</param>
        /// <param name="cx">cross x</param>
        /// <param name="cy">cross y</param>
        /// <param name="positiveIndex">positive index</param>
        /// <param name="negativeIndex">negative index</param>
        public DistanceCadreModel(int dx, int dy, int px, int py, int cx, int cy, int positiveIndex, int negativeIndex)
        {
            this.Set(distanceXName, dx);
            this.Set(distanceYName, dy);
            this.Set(positionXName, px);
            this.Set(positionYName, py);
            this.Set(crossXName, cx);
            this.Set(crossYName, cy);
            this.Set(refCadreModelPositiveIndexName, positiveIndex);
            this.Set(refCadreModelNegativeIndexName, negativeIndex);
        }

        /// <summary>
        /// Constructor with two cadre model
        /// </summary>
        /// <param name="cm1">cadre model one</param>
        /// <param name="cm2">cadre model two</param>
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

        /// <summary>
        /// Uniformizer
        /// </summary>
        /// <param name="cm2">cadre model one</param>
        /// <param name="cm1">cadre model two</param>
        private void Uniform(CadreModel cm2, CadreModel cm1)
        {
            int dx = cm2.WidthPosition - cm1.WidthPosition;
            int dy = cm2.HeightPosition - cm1.HeightPosition;
            int px = 0, py = 0;
            int cx, cy;
            px = cm1.Width;
            if (dx > 0)
            {// cm2 est plus loin que cm1
                cx = cm1.WidthPosition + cm1.Width - cm2.WidthPosition; // si cx > 0 alors il y a un chevauchement
            }
            else if (dx < 0)
            {   // cm1 est de l'autre côté de cm2
                cx = cm2.WidthPosition + cm2.Width - cm1.WidthPosition; // si cx > 0 alors cm chevauche cmNew
            }
            else
            { // cm1 et cm2 commencent en même temps
                cx = cm2.Width - cm1.Width;
            }
            py = cm1.Height;
            if (dy > 0)
            {
                cy = cm1.HeightPosition + cm1.Height - cm2.HeightPosition; // si cy > 0 alors cmNew chevauche cm
                if (cy < 0 && (!cm2.HeightContact.HasValue || cm2.HeightContact.Value < cy))
                {
                    cm2.HeightContact = cy;
                }
            }
            else if (dy < 0)
            {
                cy = cm2.HeightPosition + cm2.Height - cm1.HeightPosition;
                if (cy < 0 && (!cm2.HeightContact.HasValue || cm2.HeightContact.Value < cy))
                {
                    cm2.HeightContact = cy;
                }
            }
            else
            {
                cy = cm2.Height - cm1.Height;
                if (cy < 0 && (!cm2.HeightContact.HasValue || cm2.HeightContact.Value < cy))
                {
                    cm2.HeightContact = cy;
                }
            }
            this.Set(distanceXName, dx);
            this.Set(distanceYName, dy);
            this.Set(positionXName, px);
            this.Set(positionYName, py);
            this.Set(crossXName, cx);
            this.Set(crossYName, cy);
            this.Set(refCadreModelPositiveIndexName, cm2.Index);
            this.Set(refCadreModelNegativeIndexName, cm1.Index);
        }

        /// <summary>
        /// Computes negative intersection
        /// </summary>
        /// <param name="dist">distance</param>
        /// <param name="pos">position</param>
        /// <param name="cross">croisement</param>
        /// <param name="before">avant</param>
        /// <param name="after">après</param>
        /// <returns>point</returns>
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

        /// <summary>
        /// Aimented contact between two cadre mode
        /// </summary>
        /// <param name="cm2">cadre model two</param>
        /// <param name="cm1">cadre model one</param>
        private void AimentedContact(CadreModel cm2, CadreModel cm1)
        {
            if (this.Get(crossXName, 0) < 0 && (!cm2.WidthContact.HasValue || cm2.WidthContact.Value < this.Get(crossXName, 0)))
            {
                cm2.WidthContact = this.Get(crossXName, 0);
            }
            if (this.Get(crossYName, 0) < 0 && (!cm2.HeightContact.HasValue || cm2.HeightContact.Value < this.Get(crossYName, 0)))
            {
                cm2.HeightContact = this.Get(crossYName, 0);
            }
        } 
        #endregion

        #region Public Methods

        /// <summary>
        /// Test if an intersection exists
        /// </summary>
        /// <param name="src">cadre model list</param>
        /// <param name="cm">cadre model to test</param>
        /// <returns>rectangle of intersection</returns>
        public Rectangle HasIntersection(List<CadreModel> src, CadreModel cm)
        {
            if (cm.Index == this.Get(refCadreModelPositiveIndexName, 0))
            {
                System.Drawing.Point p1 = this.NegativeIntersectionBy(this.Get(distanceXName, 0), this.Get(positionXName, 0), this.Get(crossXName, 0), cm.WidthPosition, cm.WidthPosition + cm.Width);
                System.Drawing.Point p2 = this.NegativeIntersectionBy(this.Get(distanceYName, 0), this.Get(positionYName, 0), this.Get(crossYName, 0), cm.HeightPosition, cm.HeightPosition + cm.Height);
                return new Rectangle(p1.X, p1.Y, p2.X, p2.Y);
            }
            else if (cm.Index == this.Get(refCadreModelNegativeIndexName, 0))
            {
                CadreModel cm2 = DistanceCadreModel.Find(src, this.Get(refCadreModelPositiveIndexName, 0));
                return this.HasIntersection(src, cm2);
            }
            return new Rectangle();
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new DistanceCadreModel(this.Get(distanceXName, 0), this.Get(distanceYName, 0),
                                          this.Get(positionXName, 0), this.Get(positionYName, 0),
                                          this.Get(crossXName, 0), this.Get(crossYName, 0),
                                          this.Get(refCadreModelPositiveIndexName, 0), this.Get(refCadreModelNegativeIndexName, 0));
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Dumping list
        /// </summary>
        /// <param name="list">list of distance between cadre model list</param>
        /// <returns>string dump</returns>
        public static string Dump(List<DistanceCadreModel> list)
        {
            string output = String.Empty;
            foreach (DistanceCadreModel d in list)
            {
                output += d.ToString() + Environment.NewLine;
            }
            return output;
        }

        /// <summary>
        /// Gets the crossed list of rectangle
        /// </summary>
        /// <param name="src">cadre model list</param>
        /// <param name="list">distance cadre model list</param>
        /// <param name="cm">specific cadre model</param>
        /// <returns>a list of rectangle</returns>
        public static List<Rectangle> CrossRect(List<CadreModel> src, List<DistanceCadreModel> list, CadreModel cm)
        {
            return DistanceCadreModel.FindIntersect(list, src, cm);
        }

        /// <summary>
        /// Find an intersection between cadre model
        /// </summary>
        /// <param name="list">list of distance cadre model</param>
        /// <param name="src">list of cadre model</param>
        /// <param name="cm">specific cadre model</param>
        /// <returns>a list of rectangle</returns>
        public static List<Rectangle> FindIntersect(List<DistanceCadreModel> list, List<CadreModel> src, CadreModel cm)
        {
            return list.Select(a =>
            {

                if (a.Get(refCadreModelPositiveIndexName, 0) == cm.Index && a.Get(refCadreModelNegativeIndexName, 0) > cm.Index)
                {
                    Rectangle r = a.HasIntersection(src, cm);
                    if (!r.IsEmpty()) return r;
                }
                else if (a.Get(refCadreModelNegativeIndexName, 0) == cm.Index && a.Get(refCadreModelPositiveIndexName, 0) > cm.Index)
                {
                    Rectangle r = a.HasIntersection(src, cm);
                    if (!r.IsEmpty()) return r;
                }
                return new Rectangle();

            }).ToList();
        }

        /// <summary>
        /// Find a specific cadre model from a list
        /// </summary>
        /// <param name="src">source cadre model list</param>
        /// <param name="index">specific index</param>
        /// <returns>cadre model that has the same index</returns>
        public static CadreModel Find(List<CadreModel> src, int index)
        {
            return src.Single(a => { return a.Index == index; });
        }

        /// <summary>
        /// Update distance
        /// </summary>
        /// <param name="list">list of distance cadre model</param>
        /// <param name="src">list of cadre model</param>
        /// <param name="cmChanged">cadre model that has changed</param>
        public static void UpdateDistance(List<DistanceCadreModel> list, List<CadreModel> src, CadreModel cmChanged)
        {
            list.FindAll(a =>
            {
                if (a.Get(refCadreModelPositiveIndexName, 0) == cmChanged.Index || a.Get(refCadreModelNegativeIndexName, 0) == cmChanged.Index) return true; else return false;
            }).ForEach(a =>
            {
                if (a.Get(refCadreModelPositiveIndexName, 0) == cmChanged.Index)
                {
                    CadreModel cm = DistanceCadreModel.Find(src, a.Get(refCadreModelNegativeIndexName, 0));
                    a.Uniform(cmChanged, cm);
                    a.AimentedContact(cmChanged, cm);
                    cmChanged.AimentedMove();
                }
                else
                {
                    CadreModel cm = DistanceCadreModel.Find(src, a.Get(refCadreModelPositiveIndexName, 0));
                    a.Uniform(cm, cmChanged);
                    a.AimentedContact(cm, cmChanged);
                    cm.AimentedMove();
                }
            });
        }

        /// <summary>
        /// Add a new cadre model
        /// </summary>
        /// <param name="list">list of distance cadre model</param>
        /// <param name="src">cadre model source list</param>
        /// <param name="cmNew">new cadre model</param>
        public static void AddNewCadre(List<DistanceCadreModel> list, List<CadreModel> src, CadreModel cmNew)
        {
            foreach (CadreModel cm in src)
            {
                list.Add(new DistanceCadreModel(cm, cmNew));
            }
        }

        /// <summary>
        /// Remove cadre model
        /// </summary>
        /// <param name="list">list of distance cadre model</param>
        /// <param name="cmToRemove">cadre model to remove</param>
        public static void RemoveCadre(List<DistanceCadreModel> list, CadreModel cmToRemove)
        {
            IEnumerable<DistanceCadreModel> toRemove = list.TakeWhile(a => { return a.Get(refCadreModelPositiveIndexName, 0) == cmToRemove.Index || a.Get(refCadreModelNegativeIndexName, 0) == cmToRemove.Index; });
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

        /// <summary>
        /// Test if two object are equals or not
        /// </summary>
        /// <param name="x">object one</param>
        /// <param name="y">object two</param>
        /// <returns>true if equals</returns>
        public bool Equals(DistanceCadreModel x, DistanceCadreModel y)
        {
            return (x.Get(refCadreModelPositiveIndexName, 0) == y.Get(refCadreModelPositiveIndexName, 0) && x.Get(refCadreModelNegativeIndexName, 0) == y.Get(refCadreModelNegativeIndexName, 0));
        }

        public int GetHashCode(DistanceCadreModel obj)
        {
            try
            {
                double d = Math.Round(Math.Sqrt(Math.Pow(obj.Get(distanceXName, 0), 2) + Math.Pow(obj.Get(distanceYName, 0), 2)), 5);
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
            string output = "(" + this.Get(refCadreModelPositiveIndexName, 0) + "," + this.Get(refCadreModelNegativeIndexName, 0) + ") = ";
            output += "{" + "dist.x=" + this.Get(distanceXName, 0) + ", dist.y=" +
                      this.Get(distanceYName, 0) + ", pos.x=" + this.Get(positionXName, 0) + ", pos.y=" +
                      this.Get(positionYName, 0) + ", cross.x=" + this.Get(crossXName, 0) + ", cross.y=" + this.Get(crossYName, 0) + "}";
            return output;
        }

        #endregion

    }

    /// <summary>
    /// Cadre index argument
    /// </summary>
    public class CadreIndexArgs : EventArgs
    {

        #region Private Fields

        private CadreModel cm;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="cm">cadre model</param>
        public CadreIndexArgs(CadreModel cm)
        {
            this.cm = cm;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets cadre model transmission
        /// </summary>
        public CadreModel CadreModel
        {
            get { return this.cm; }
        }

        #endregion
    }

    /// <summary>
    /// Cadre index argument during paint
    /// </summary>
    public class CadreIndexPaintArgs : EventArgs
    {
        #region Private Fields

        private CadreModel cm;
        private System.Windows.Forms.PaintEventArgs p;
        
        #endregion

        #region Public Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="cm">cadre model</param>
        /// <param name="p">paint event argument</param>
        public CadreIndexPaintArgs(CadreModel cm, System.Windows.Forms.PaintEventArgs p)
        {
            this.cm = cm;
            this.p = p;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets cadre model transmission
        /// </summary>
        public CadreModel CadreModel
        {
            get { return this.cm; }
        }

        /// <summary>
        /// Gets paint event argument
        /// </summary>
        public System.Windows.Forms.PaintEventArgs Paint
        {
            get { return this.p; }
        }

        #endregion
    }
}
