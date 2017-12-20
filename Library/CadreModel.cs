using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class CadreModel : INotifyPropertyChanged, ICloneable
    {
        #region Private Static Fields
        private static int counter = 0;
        private static int aimentedDistance = 20;
        #endregion

        #region Private Fields
        [NonSerialized]
        protected EventHandler<CadreIndexArgs> updated;
        [NonSerialized]
        protected PropertyChangedEventHandler propertyChanged;
        protected string name;
        protected List<CadreModelType> modelTypes;
        protected int selectedModelType;
        protected int L, H;
        protected int paddingL, paddingH;
        protected int positionL, positionH;
        protected int borderL, borderH;
        protected int? contactL, contactH;
        protected Color background, foreground, borderColor;
        protected int index;
        protected bool suspendBinding;
        protected bool isGrouped;
        protected int groupIndex;
        #endregion

        #region Public Constructor
        public CadreModel()
        {
            this.suspendBinding = false;
            this.modelTypes = new List<CadreModelType>();
            this.modelTypes.Add(new CadreModelType(CadreModelType.Image));
            this.modelTypes.Add(new CadreModelType(CadreModelType.Text));
            this.modelTypes.Add(new CadreModelType(CadreModelType.Tool));
            this.modelTypes.Add(new CadreModelType(CadreModelType.MasterObject));
            this.modelTypes.Add(new CadreModelType(CadreModelType.DynamicObject));
            this.selectedModelType = 4;
            this.L = 100; this.H = 100;
            this.paddingL = 0; this.paddingH = 0;
            this.positionL = 0; this.positionH = 0;
            this.borderL = 0; this.borderH = 0;
            this.background = Color.Black;
            this.foreground = Color.White;
            this.borderColor = Color.White;
            this.index = ++CadreModel.counter;
            this.isGrouped = false;
        }

        private CadreModel(CadreModel cm)
        {
            this.name = cm.name;
            this.suspendBinding = false;
            this.modelTypes = new List<CadreModelType>();
            foreach (CadreModelType t in cm.modelTypes)
            {
                this.modelTypes.Add(t.Clone() as CadreModelType);
            }
            this.selectedModelType = 4;
            this.L = cm.L; this.H = cm.H;
            this.paddingL = cm.paddingL; this.paddingH = cm.paddingH;
            this.positionL = cm.positionL; this.positionH = cm.positionH;
            this.borderL = cm.borderL; this.borderH = cm.borderH;
            this.background = cm.background;
            this.foreground = cm.foreground;
            this.borderColor = cm.borderColor;
            this.index = ++CadreModel.counter;
            this.isGrouped = cm.isGrouped;
        }
        #endregion

        #region Private Methods
        private void UpdateProperty(string name)
        {
            if (!this.suspendBinding)
            {
                if (this.propertyChanged != null)
                    this.propertyChanged(this, new PropertyChangedEventArgs(name));
            }
            if (this.updated != null)
            {
                this.contactH = this.contactL = null;
                this.updated(this, new CadreIndexArgs(this));
            }
        }
        #endregion

        #region Public Properties
        public bool SuspendBinding
        {
            get { return this.suspendBinding; }
            set { this.suspendBinding = value; }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public List<CadreModelType> ModelTypes
        {
            get { return this.modelTypes; }
        }

        public int SelectedModelTypeIndex
        {
            get { return this.selectedModelType; }
            set { this.selectedModelType = value; }
        }

        public CadreModelType SelectedModelTypeObject
        {
            get
            {
                if (this.selectedModelType != -1) return this.modelTypes[this.selectedModelType]; else return null;
            }
        }

        public int Largeur
        {
            get { return this.L; }
            set { this.L = value; this.UpdateProperty("Largeur"); }
        }

        public int Hauteur
        {
            get { return this.H; }
            set { this.H = value; this.UpdateProperty("Hauteur"); }
        }

        public int? HorizontalContact
        {
            get { return this.contactH; }
            set { this.contactH = value; }
        }

        public int? VerticalContact
        {
            get { return this.contactL; }
            set { this.contactL = value; }
        }

        public int VerticalPadding
        {
            get { return this.paddingL; }
            set { this.paddingL = value; this.UpdateProperty("VerticalPadding"); }
        }

        public int HorizontalPadding
        {
            get { return this.paddingH; }
            set { this.paddingH = value; this.UpdateProperty("HorizontalPadding"); }
        }

        public int VerticalPosition
        {
            get { return this.positionL; }
            set { this.positionL = value; this.UpdateProperty("VerticalPosition"); }
        }

        public int HorizontalPosition
        {
            get { return this.positionH; }
            set { this.positionH = value; this.UpdateProperty("HorizontalPosition"); }
        }

        public int VerticalBorder
        {
            get { return this.borderL; }
            set { this.borderL = value; this.UpdateProperty("VerticalBorder"); }
        }

        public int HorizontalBorder
        {
            get { return this.borderH; }
            set { this.borderH = value; this.UpdateProperty("HorizontalBorder"); }
        }

        public Color Foreground
        {
            get { return this.foreground; }
            set { this.foreground = value; this.UpdateProperty("Foreground"); }
        }

        public Color Background
        {
            get { return this.background; }
            set { this.background = value; this.UpdateProperty("Background"); }
        }

        public Color Border
        {
            get { return this.borderColor; }
            set { this.borderColor = value; this.UpdateProperty("Border"); }
        }

        public int Index
        {
            get { return this.index; }
        }
        #endregion

        #region Public Methods
        public void RaisePropertyChanged()
        {
            this.propertyChanged(this, new PropertyChangedEventArgs(""));
        }

        public void AimentedMove()
        {
            if (this.HorizontalContact.HasValue && this.HorizontalContact.Value > 0 && this.HorizontalContact.Value > CadreModel.aimentedDistance)
            {
                this.positionH += this.HorizontalContact.Value;
            }
            if (this.VerticalContact.HasValue && this.VerticalContact.Value > 0 && this.VerticalContact.Value > CadreModel.aimentedDistance)
            {
                this.positionL += this.VerticalContact.Value;
            }
            this.RaisePropertyChanged();
        }

        public void Reinit()
        {
            this.propertyChanged = null;
        }

        public void CopyProperties(HTMLObject obj)
        {
            obj.Title = this.name;
            if (this.Largeur >= 0)
            {
                obj.ConstraintWidth = EnumConstraint.FIXED;
                obj.Width = (uint)this.Largeur;
            }
            else
            {
                obj.ConstraintWidth = EnumConstraint.AUTO;
                obj.Width = 0;
            }
            if (this.Hauteur >= 0)
            {
                obj.ConstraintHeight = EnumConstraint.FIXED;
                obj.Height = (uint)this.Hauteur;
            }
            else
            {
                obj.ConstraintHeight = EnumConstraint.AUTO;
                obj.Height = 0;
            }
            obj.CSS.Padding = new Rectangle(this.HorizontalPadding, this.HorizontalPadding, this.VerticalPadding, this.VerticalPadding);
            obj.CSS.Border = new Rectangle(this.HorizontalBorder, this.HorizontalBorder, this.VerticalBorder, this.VerticalBorder);

            obj.CSS.BackgroundColor = new CSSColor(this.Background);
            obj.CSS.BorderBottomColor = obj.CSS.BorderLeftColor = obj.CSS.BorderRightColor = obj.CSS.BorderTopColor = new CSSColor(this.Border);
        }
        #endregion

        #region Public Static Methods
        public static void ReinitCounter(int value)
        {
            CadreModel.counter = value;
        }
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this.propertyChanged += new PropertyChangedEventHandler(value); }
            remove { this.propertyChanged -= new PropertyChangedEventHandler(value); }
        }

        public event EventHandler<CadreIndexArgs> Updated
        {
            add { this.updated += new EventHandler<CadreIndexArgs>(value); }
            remove { this.updated -= new EventHandler<CadreIndexArgs>(value); }
        }
        #endregion

        public object Clone()
        {
            CadreModel cm = new CadreModel(this);
            return cm;
        }
    }

    public class PlacementModel
    {
        #region Internal class
        /// <summary>
        /// Une classe MixedZone est un quadrillage
        /// des objets placés n'importe comment
        /// </summary>
        internal class MixedZone
        {
            #region Private Fields
            private int hPosition, vPosition;
            #endregion

            #region Public Constructor
            public MixedZone(int h, int v)
            {
                this.hPosition = h;
                this.vPosition = v;
            }
            #endregion

            #region Public Properties
            public int HorizontalPosition
            {
                get { return this.hPosition; }
            }

            public int VerticalPosition
            {
                get { return this.vPosition; }
            }
            #endregion

            #region Public Methods
            public void Move(int h, int v)
            {
                this.hPosition = h;
                this.vPosition = v;
            }
            #endregion
        }

        /// <summary>
        /// Un élément de placement contient un cadre
        /// </summary>
        internal class PlacementItem : IEqualityComparer<RectangleF>
        {
            #region Private Fields
            private RectangleF rect;
            #endregion

            #region Public Constructor
            public PlacementItem(CadreModel cm, float ratio)
            {
                this.rect = new RectangleF(cm.VerticalPosition / ratio, cm.HorizontalPosition / ratio, cm.Largeur / ratio, cm.Hauteur / ratio);
            }
            #endregion

            /// <summary>
            /// Retourne vrai si les deux rectangles s'intersectent
            /// </summary>
            /// <param name="x">rectangle 1</param>
            /// <param name="y">rectangle 2</param>
            /// <returns>vrai ou faux</returns>
            public bool Equals(RectangleF x, RectangleF y)
            {
                return !RectangleF.Intersect(x, y).IsEmpty;
            }

            public int GetHashCode(RectangleF obj)
            {
                return Convert.ToInt32(Math.Sqrt(Math.Pow(obj.Width, 2) + Math.Pow(obj.Height, 2)));
            }

            /// <summary>
            /// Compare deux éléments
            /// </summary>
            /// <param name="x">élément 1</param>
            /// <param name="y">élément 2</param>
            /// <returns>x=y 0, x>y 1 ou x<y -1</returns>
            public int CompareWidth(PlacementItem x, PlacementItem y)
            {
                if (this.Equals(x.rect, y.rect))
                {
                    return 0;
                }
                else
                {
                    float xwidth = x.rect.Left + x.rect.Width;
                    float ywidth = y.rect.Left + y.rect.Width;

                    if (xwidth > ywidth) return 1; else return -1;
                }
            }

            public int CompareHeight(PlacementItem x, PlacementItem y)
            {
                if (this.Equals(x.rect, y.rect))
                {
                    return 0;
                }
                else
                {
                    float xheight = x.rect.Top + x.rect.Height;
                    float yheight = y.rect.Top + y.rect.Height;

                    if (xheight > yheight) return 1; else return -1;
                }
            }

            /// <summary>
            /// Compare l'objet avec un autre
            /// </summary>
            /// <param name="other">autre</param>
            /// <returns>0, 1 ou -1</returns>
            public int CompareWidth(PlacementItem other)
            {
                return this.CompareWidth(this, other);
            }

            /// <summary>
            /// Compare l'objet avec un autre
            /// </summary>
            /// <param name="other">autre</param>
            /// <returns>0, 1 ou -1</returns>
            public int CompareHeight(PlacementItem other)
            {
                return this.CompareHeight(this, other);
            }
        }

        /// <summary>
        /// Indique l'ancienne et la nouvelle position
        /// lorsque la position d'un élément de placement change
        /// </summary>
        internal class PositionArgs : EventArgs
        {
            #region Private Fields
            private int oldValue;
            private int newValue;
            #endregion

            #region Public Constructor
            public PositionArgs(int oldValue, int newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }
            #endregion

            #region Public Properties
            public int OldPosition
            {
                get { return this.oldValue; }
            }

            public int NewPosition
            {
                get { return this.newValue; }
            }
            #endregion
        }

        /// <summary>
        /// Lignes horizontales des éléments
        /// </summary>
        internal class HorizontalPlacementItem
        {
            #region Private Fields
            private event EventHandler<PositionArgs> positionChanged;
            private int oldValue, newValue;
            private int position;
            private PlacementItem item;
            #endregion

            #region Public Constructor
            public HorizontalPlacementItem(PlacementItem item)
            {
                this.item = item;
                this.PositionChanged += action;
            }
            #endregion

            #region Private Methods
            private void action(object sender, PositionArgs e)
            {
                this.oldValue = e.OldPosition;
                this.newValue = e.NewPosition;
            }
            #endregion

            #region Public Properties
            public int Position
            {
                get { return this.position; }
                set
                {
                    if (this.position != value)
                    {
                        this.positionChanged(this, new PositionArgs(this.position, value));
                        this.position = value;
                    }
                }
            }

            public PlacementItem Item
            {
                get { return this.item; }
            }
            #endregion

            #region Events
            public event EventHandler<PositionArgs> PositionChanged
            {
                add { this.positionChanged += new EventHandler<PositionArgs>(value); }
                remove { this.positionChanged -= new EventHandler<PositionArgs>(value); }
            }
            #endregion
        }

        internal static class HorizontalComparer
        {
            #region Public Static Methods
            public static void Compare(LinkedList<HorizontalPlacementItem> list, HorizontalPlacementItem input)
            {
                LinkedListNode<HorizontalPlacementItem> positiveCurrent = list.First;
                LinkedListNode<HorizontalPlacementItem> negativeCurrent = list.Last;

                while (positiveCurrent != null && positiveCurrent != negativeCurrent)
                {
                    if (HorizontalComparer.Compare(positiveCurrent.Value, input) == 1)
                    {
                        list.AddAfter(positiveCurrent, input);
                        break;
                    }
                    else if (HorizontalComparer.Compare(negativeCurrent.Value, input) == -1)
                    {
                        list.AddBefore(negativeCurrent, input);
                        break;
                    }
                    positiveCurrent = positiveCurrent.Next;
                    negativeCurrent = negativeCurrent.Previous;
                }
                if (positiveCurrent == null || positiveCurrent == negativeCurrent)
                {
                    list.AddLast(input);
                }
            }

            public static int Compare(HorizontalPlacementItem x, HorizontalPlacementItem y)
            {
                return x.Item.CompareWidth(y.Item);
            }
            #endregion

        }

        /// <summary>
        /// Lignes verticales des éléments
        /// </summary>
        internal class VerticalPlacementItem : IComparer<VerticalPlacementItem>
        {
            #region Private Fields
            private event EventHandler<PositionArgs> positionChanged;
            private int oldValue, newValue;
            private int position;
            private LinkedList<HorizontalPlacementItem> list;
            #endregion

            #region Public Constructor
            public VerticalPlacementItem()
            {
                this.list = new LinkedList<HorizontalPlacementItem>();
                this.PositionChanged += action;
            }
            #endregion

            #region Private Methods
            private void action(object sender, PositionArgs e)
            {
                this.oldValue = e.OldPosition;
                this.newValue = e.NewPosition;
            }
            #endregion

            #region Public Properties
            public LinkedList<HorizontalPlacementItem> Items
            {
                get { return this.list; }
            }

            public int Position
            {
                get { return this.position; }
                set
                {
                    if (this.position != value)
                    {
                        this.positionChanged(this, new PositionArgs(this.position, value));
                        this.position = value;
                    }
                }
            }

            public PlacementItem LessPosition
            {
                get
                {
                    PlacementItem less = null;
                    LinkedListNode<HorizontalPlacementItem> positiveCurrent = list.First;
                    LinkedListNode<HorizontalPlacementItem> negativeCurrent = list.Last;

                    if (positiveCurrent != null)
                    {
                        PlacementItem first = positiveCurrent.Value.Item;
                        PlacementItem last = negativeCurrent.Value.Item;
                        while (positiveCurrent != null && positiveCurrent != negativeCurrent)
                        {
                            if (first.CompareHeight(positiveCurrent.Value.Item) == 1)
                            {
                                less = positiveCurrent.Value.Item;
                                break;
                            }
                            else if (last.CompareHeight(negativeCurrent.Value.Item) == -1)
                            {
                                less = negativeCurrent.Previous.Value.Item;
                                break;
                            }
                            positiveCurrent = positiveCurrent.Next;
                            negativeCurrent = negativeCurrent.Previous;
                        }
                    }
                    return less;
                }
            }
            #endregion

            #region Public Events
            public event EventHandler<PositionArgs> PositionChanged
            {
                add { this.positionChanged += new EventHandler<PositionArgs>(value); }
                remove { this.positionChanged -= new EventHandler<PositionArgs>(value); }
            }
            #endregion

            #region Public Methods
            public void Add(HorizontalPlacementItem h)
            {
                this.list.AddLast(h);
            }

            /// <summary>
            /// Compare deux objets
            /// </summary>
            /// <param name="x">objet 1</param>
            /// <param name="y">objet 2</param>
            /// <returns>vrai ou faux</returns>
            public int Compare(VerticalPlacementItem x, VerticalPlacementItem y)
            {
                PlacementItem p1 = x.LessPosition;
                PlacementItem p2 = y.LessPosition;
                if (p1 != null && p2 != null)
                    return p1.CompareHeight(p2);
                else
                    if (p1 != null) return -1;
                    else return 1;
            }

            public int Compare(VerticalPlacementItem other)
            {
                return this.Compare(this, other);
            }
            #endregion
        }

        internal static class VerticalComparer
        {
            #region Public Constructor
            public static void Compare(LinkedList<VerticalPlacementItem> list, PlacementItem p)
            {
                VerticalPlacementItem input = new VerticalPlacementItem();
                input.Add(new HorizontalPlacementItem(p));
                LinkedListNode<VerticalPlacementItem> positiveCurrent = list.First;
                LinkedListNode<VerticalPlacementItem> negativeCurrent = list.Last;

                while (positiveCurrent != null && positiveCurrent != negativeCurrent)
                {
                    if (VerticalComparer.Compare(positiveCurrent.Value, input) == 0)
                    {
                        HorizontalComparer.Compare(positiveCurrent.Value.Items, input.Items.First.Value);
                        break;
                    }
                    else if (VerticalComparer.Compare(negativeCurrent.Value, input) == 0)
                    {
                        HorizontalComparer.Compare(negativeCurrent.Value.Items, input.Items.First.Value);
                        break;
                    }
                    else if (VerticalComparer.Compare(positiveCurrent.Value, input) == 1)
                    {
                        list.AddAfter(positiveCurrent, input);
                        break;
                    } else if (VerticalComparer.Compare(negativeCurrent.Value, input) == -1)
                    {
                        list.AddBefore(negativeCurrent, input);
                        break;
                    }
                    positiveCurrent = positiveCurrent.Next;
                    negativeCurrent = negativeCurrent.Previous;
                }
                if (positiveCurrent == null || positiveCurrent == negativeCurrent)
                {
                    list.AddLast(input);
                }
            }
            #endregion

            #region Public Methods
            public static int Compare(VerticalPlacementItem x, VerticalPlacementItem y)
            {
                return y.Compare(x);
            }
            #endregion
        }

        #endregion

        #region Private Fields
        private LinkedList<VerticalPlacementItem> list;
        #endregion

        #region Public Constructor
        public PlacementModel()
        {
            this.list = new LinkedList<VerticalPlacementItem>();
        }
        #endregion

        #region Private Methods
        private void InspectQuadrille(PlacementItem p)
        {
            VerticalComparer.Compare(this.list, p);
        }
        #endregion

        #region Public Methods
        public void Add(CadreModel cm, float ratio)
        {
            PlacementItem p = new PlacementItem(cm, ratio);
            this.InspectQuadrille(p);
        }

        public void ComputeMixedZones()
        {
            List<List<PlacementItem>> zList = new List<List<PlacementItem>>();
            List<PlacementItem> hSavedItems = new List<PlacementItem>();
            foreach (VerticalPlacementItem v in this.list)
            {
                int index = 0;
                LinkedListNode<HorizontalPlacementItem> currentNext = v.Items.First;
                while (currentNext != null)
                {
                    if (index < hSavedItems.Count)
                    {
                        ++index;
                        if (hSavedItems[index].Equals(currentNext.Value.Item))
                        {
                            // les deux cadres s'intersectent
                        }
                    }
                    else
                    {
                        hSavedItems.Add(currentNext.Value.Item);
                    }
                    currentNext = currentNext.Next;
                }
            }
        }
        #endregion
    }
}
