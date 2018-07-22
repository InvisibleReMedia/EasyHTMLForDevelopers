using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Cadre model form to create a sculpture
    /// and generate a complete or partial form of an html page, master page, master object or a tool
    /// </summary>
    [Serializable]
    public class CadreModel : Marshalling.PersistentDataObject, INotifyPropertyChanged, ICloneable
    {

        #region Private Static Fields

        /// <summary>
        /// Static Counter
        /// </summary>
        private static int counter = 0;
        /// <summary>
        /// Distance minimum under two objects are collapsed
        /// </summary>
        private static int aimentedDistance = 20;

        #endregion

        #region Protected Static Fields

        /// <summary>
        /// Index name for name
        /// </summary>
        protected static readonly string nameName = "name";
        /// <summary>
        /// Index name for existing standard model
        /// </summary>
        protected static readonly string modelTypesName = "modelTypeList";
        /// <summary>
        /// Index name for select the desired model type
        /// </summary>
        protected static readonly string selectedModelTypeName = "selectedModelType";
        /// <summary>
        /// Index name for width
        /// </summary>
        protected static readonly string widthName = "width";
        /// <summary>
        /// Index name for height
        /// </summary>
        protected static readonly string heightName = "height";
        /// <summary>
        /// Index name for padding width
        /// </summary>
        protected static readonly string paddingWidthName = "paddingWidth";
        /// <summary>
        /// Index name for padding height
        /// </summary>
        protected static readonly string paddingHeightName = "paddingHeight";
        /// <summary>
        /// Index name for position width
        /// </summary>
        protected static readonly string positionWidthName = "positionWidth";
        /// <summary>
        /// Index name for position height
        /// </summary>
        protected static readonly string positionHeightName = "positionHeight";
        /// <summary>
        /// Index name for border width
        /// </summary>
        protected static readonly string borderWidthName = "borderWidth";
        /// <summary>
        /// Index name for border height
        /// </summary>
        protected static readonly string borderHeightName = "borderHeight";
        /// <summary>
        /// Index name for contact width
        /// </summary>
        protected static readonly string contactWidthName = "contactWidth";
        /// <summary>
        /// Index name for contact height
        /// </summary>
        protected static readonly string contactHeightName = "contactHeight";
        /// <summary>
        /// Index name for background color
        /// </summary>
        protected static readonly string backgroundColorName = "backgroundColor";
        /// <summary>
        /// Index name for foreground color
        /// </summary>
        protected static readonly string foregroundColorName = "foregroundColor";
        /// <summary>
        /// Index name for border color
        /// </summary>
        protected static readonly string borderColorName = "borderColor";
        /// <summary>
        /// Index name for index position of this object from the list
        /// </summary>
        protected static readonly string indexName = "indexPosition";
        /// <summary>
        /// Index name for suspending binding switch
        /// </summary>
        protected static readonly string suspendBindingName = "suspendBinding";
        /// <summary>
        /// Index name for group switch
        /// </summary>
        protected static readonly string isGroupName = "isGroup";
        /// <summary>
        /// Index name for group index
        /// </summary>
        protected static readonly string groupIndexName = "groupIndex";


        #endregion

        #region Events

        /// <summary>
        /// Event handler when a cadre model was updated
        /// </summary>
        [NonSerialized]
        protected EventHandler<CadreIndexArgs> updated;
        /// <summary>
        /// Event handler when a property of a cadre model has changed
        /// </summary>
        [NonSerialized]
        protected PropertyChangedEventHandler propertyChanged;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CadreModel()
        {
            List<CadreModelType> modelTypes = new List<CadreModelType>();
            modelTypes.Add(new CadreModelType(CadreModelType.Image));
            modelTypes.Add(new CadreModelType(CadreModelType.Text));
            modelTypes.Add(new CadreModelType(CadreModelType.Tool));
            modelTypes.Add(new CadreModelType(CadreModelType.MasterObject));
            modelTypes.Add(new CadreModelType(CadreModelType.DynamicObject));
            this.Set(modelTypesName, modelTypes);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="cm">object source</param>
        private CadreModel(CadreModel cm)
        {
            this.Name = cm.Name;
            this.SuspendBinding = false;
            List<CadreModelType> modelTypes = new List<CadreModelType>();
            foreach(CadreModelType cmt in cm.ModelTypes)
            {
                modelTypes.Add(cmt.Clone() as CadreModelType);
            }
            this.Set(modelTypesName, modelTypes);
            this.SelectedModelTypeIndex = 4;
            this.Width = cm.Width; this.Height = cm.Height;
            this.WidthPadding = cm.WidthPadding; this.HeightPadding = cm.HeightPadding;
            this.WidthPosition = cm.WidthPosition; this.HeightPosition = cm.HeightPosition;
            this.WidthBorder = cm.WidthBorder; this.HeightBorder = cm.HeightBorder;
            this.Background = cm.Background;
            this.Foreground = cm.Foreground;
            this.Border = cm.Border;
            this.IsGroup = cm.IsGroup;
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets Suspending binding switch
        /// </summary>
        public bool SuspendBinding
        {
            get { return this.Get(suspendBindingName, false); }
            set { this.Set(suspendBindingName, value); }
        }

        /// <summary>
        /// Gets or sets the name of this cadre model
        /// </summary>
        public string Name
        {
            get
            {
                return this.Get(nameName, "new");
            }
            set
            {
                this.Set(nameName, value);
            }
        }

        /// <summary>
        /// List of available model types
        /// </summary>
        public List<CadreModelType> ModelTypes
        {
            get { return this.Get(modelTypesName); }
        }

        /// <summary>
        /// Gets or sets the selected model type
        /// </summary>
        public int SelectedModelTypeIndex
        {
            get { return this.Get(selectedModelTypeName, 4); }
            set { this.Get(selectedModelTypeName, value); }
        }

        /// <summary>
        /// Gets or sets the selected model object
        /// </summary>
        public CadreModelType SelectedModelTypeObject
        {
            get
            {
                if (this.SelectedModelTypeIndex != -1) return this.ModelTypes[this.SelectedModelTypeIndex]; else return null;
            }
        }

        /// <summary>
        /// Gets or sets the width value
        /// </summary>
        public int Width
        {
            get { return this.Get(widthName, 100); }
            set { this.Set(widthName, value); this.UpdateProperty("Width"); }
        }

        /// <summary>
        /// Gets or sets the height value
        /// </summary>
        public int Height
        {
            get { return this.Get(heightName, 100); }
            set { this.Set(heightName, value); this.UpdateProperty("Height"); }
        }

        /// <summary>
        /// Gets or sets the width contact
        /// </summary>
        public int? WidthContact
        {
            get { return this.Get(contactWidthName, new Nullable<int>()); }
            set { this.Set(contactWidthName, value); }
        }

        /// <summary>
        /// Gets or sets the height contact
        /// </summary>
        public int? HeightContact
        {
            get { return this.Get(contactHeightName, new Nullable<int>()); }
            set { this.Set(contactHeightName, value); }
        }

        /// <summary>
        /// Gets or sets the width padding
        /// </summary>
        public int WidthPadding
        {
            get { return this.Get(paddingWidthName, 0); }
            set { this.Set(paddingWidthName, value); this.UpdateProperty("WidthPadding"); }
        }

        /// <summary>
        /// Gets or sets the height padding
        /// </summary>
        public int HeightPadding
        {
            get { return this.Get(paddingHeightName, 0); }
            set { this.Set(paddingHeightName, value); this.UpdateProperty("HeightPadding"); }
        }

        /// <summary>
        /// Gets or sets the width position
        /// </summary>
        public int WidthPosition
        {
            get { return this.Get(positionWidthName, 0); }
            set { this.Set(positionWidthName, value); this.UpdateProperty("WidthPosition"); }
        }

        /// <summary>
        /// Gets or sets the height position
        /// </summary>
        public int HeightPosition
        {
            get { return this.Get(positionHeightName, 0); }
            set { this.Set(positionHeightName, value); this.UpdateProperty("HeightPosition"); }
        }

        /// <summary>
        /// Gets or sets the width border
        /// </summary>
        public int WidthBorder
        {
            get { return this.Get(borderWidthName, 0); }
            set { this.Set(borderWidthName, value); this.UpdateProperty("WidthBorder"); }
        }

        /// <summary>
        /// Gets or sets the height border
        /// </summary>
        public int HeightBorder
        {
            get { return this.Get(borderHeightName, 0); }
            set { this.Set(borderHeightName, value); this.UpdateProperty("HeightBorder"); }
        }

        /// <summary>
        /// Gets or sets the foreground color
        /// </summary>
        public Color Foreground
        {
            get { return this.Get(foregroundColorName, Color.White); }
            set { this.Set(foregroundColorName, value); this.UpdateProperty("Foreground"); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color Background
        {
            get { return this.Get(backgroundColorName, Color.Black); }
            set { this.Set(backgroundColorName, value); this.UpdateProperty("Background"); }
        }

        /// <summary>
        /// Gets or sets the border color
        /// </summary>
        public Color Border
        {
            get { return this.Get(borderColorName, Color.Chocolate); }
            set { this.Set(borderColorName, value); this.UpdateProperty("Border"); }
        }

        /// <summary>
        /// Gets the index
        /// </summary>
        public int Index
        {
            get {
                if (!this.Exists(indexName))
                {
                    int c = counter++;
                    this.Set(indexName, c);
                }
                return this.Get(indexName);
            }
        }

        /// <summary>
        /// Gets or sets the group switch
        /// </summary>
        public bool IsGroup
        {
            get { return this.Get(isGroupName, false); }
            set { this.Set(isGroupName, value); }
        }

        /// <summary>
        /// Gets or sets the index group
        /// </summary>
        public int? IndexGroup
        {
            get { return this.Get(groupIndexName, new Nullable<int>()); }
            set { this.Set(groupIndexName, value); }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Update property
        /// </summary>
        /// <param name="name">property name</param>
        private void UpdateProperty(string name)
        {
            if (!this.SuspendBinding)
            {
                if (this.propertyChanged != null)
                    this.propertyChanged(this, new PropertyChangedEventArgs(name));
            }
            if (this.updated != null)
            {
                this.HeightContact = this.WidthContact = null;
                this.updated(this, new CadreIndexArgs(this));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Raise property changed event
        /// </summary>
        public void RaisePropertyChanged()
        {
            this.propertyChanged(this, new PropertyChangedEventArgs(""));
        }

        /// <summary>
        /// Gets aimented move
        /// </summary>
        public void AimentedMove()
        {
            if (this.WidthContact.HasValue && this.WidthContact.Value > 0 && this.WidthContact.Value > CadreModel.aimentedDistance)
            {
                this.WidthPosition += this.WidthContact.Value;
            }
            if (this.HeightContact.HasValue && this.HeightContact.Value > 0 && this.HeightContact.Value > CadreModel.aimentedDistance)
            {
                this.HeightPosition += this.HeightContact.Value;
            }
            this.RaisePropertyChanged();
        }

        /// <summary>
        /// Clear events
        /// </summary>
        public void ClearEvents()
        {
            this.propertyChanged = null;
        }

        /// <summary>
        /// Copy properties
        /// </summary>
        /// <param name="obj">html object</param>
        public void CopyProperties(HTMLObject obj)
        {
            obj.Title = this.Name;
            if (this.Width >= 0)
            {
                obj.ConstraintWidth = EnumConstraint.FIXED;
                obj.Width = (uint)this.Width;
            }
            else
            {
                obj.ConstraintWidth = EnumConstraint.AUTO;
                obj.Width = 0;
            }
            if (this.Height >= 0)
            {
                obj.ConstraintHeight = EnumConstraint.FIXED;
                obj.Height = (uint)this.Height;
            }
            else
            {
                obj.ConstraintHeight = EnumConstraint.AUTO;
                obj.Height = 0;
            }
            obj.CSS.Padding = new Rectangle(this.WidthPadding, this.WidthPadding, this.HeightPadding, this.HeightPadding);
            obj.CSS.Border = new Rectangle(this.WidthBorder, this.WidthBorder, this.HeightBorder, this.HeightBorder);

            obj.CSS.BackgroundColor = new CSSColor(this.Background);
            obj.CSS.BorderBottomColor = obj.CSS.BorderLeftColor = obj.CSS.BorderRightColor = obj.CSS.BorderTopColor = new CSSColor(this.Border);
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            CadreModel cm = new CadreModel(this);
            return cm;
        }
        
        #endregion

        #region Public Static Methods

        /// <summary>
        /// Reinitialize counter
        /// Used just after an opened project
        /// </summary>
        /// <param name="value">value to set</param>
        public static void ReinitCounter(int value)
        {
            CadreModel.counter = value;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Event property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this.propertyChanged += new PropertyChangedEventHandler(value); }
            remove { this.propertyChanged -= new PropertyChangedEventHandler(value); }
        }

        /// <summary>
        /// Event after update
        /// </summary>
        public event EventHandler<CadreIndexArgs> Updated
        {
            add { this.updated += new EventHandler<CadreIndexArgs>(value); }
            remove { this.updated -= new EventHandler<CadreIndexArgs>(value); }
        }

        #endregion

    }

    /// <summary>
    /// Placement Method
    /// </summary>
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

            /// <summary>
            /// Position in pixel
            /// </summary>
            private int hPosition, vPosition;

            #endregion

            #region Public Constructor

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="h">width</param>
            /// <param name="v">height</param>
            public MixedZone(int h, int v)
            {
                this.hPosition = h;
                this.vPosition = v;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the width position
            /// </summary>
            public int HorizontalPosition
            {
                get { return this.hPosition; }
            }

            /// <summary>
            /// Get the height position
            /// </summary>
            public int VerticalPosition
            {
                get { return this.vPosition; }
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Move to new position
            /// </summary>
            /// <param name="h">new width</param>
            /// <param name="v">new height</param>
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

            /// <summary>
            /// A rectangular area
            /// </summary>
            private RectangleF rect;
            
            #endregion

            #region Public Constructor

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="cm">cadre model</param>
            /// <param name="ratio">ratio</param>
            public PlacementItem(CadreModel cm, float ratio)
            {
                this.rect = new RectangleF(cm.WidthPosition / ratio, cm.HeightPosition / ratio, cm.Width / ratio, cm.Height / ratio);
            }

            #endregion

            #region Methods

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

            /// <summary>
            /// Computes an hash code that reduces sort algorithm
            /// </summary>
            /// <param name="obj">input</param>
            /// <returns>hash code</returns>
            public int GetHashCode(RectangleF obj)
            {
                return Convert.ToInt32(Math.Sqrt(Math.Pow(obj.Width, 2) + Math.Pow(obj.Height, 2)));
            }

            /// <summary>
            /// Compare deux éléments en largeur
            /// </summary>
            /// <param name="x">élément 1</param>
            /// <param name="y">élément 2</param>
            /// <returns>x=y 0, x &gt; y 1 ou x &lt; y -1</returns>
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

            /// <summary>
            /// Compare deux éléments en hauteur
            /// </summary>
            /// <param name="x">élément 1</param>
            /// <param name="y">élément 2</param>
            /// <returns>x=y 0, x &gt; y 1 ou x &lt; y -1</returns>
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

            #endregion

        }

        /// <summary>
        /// Indique l'ancienne et la nouvelle position
        /// lorsque la position d'un élément de placement change
        /// </summary>
        internal class PositionArgs : EventArgs
        {
            #region Private Fields

            /// <summary>
            /// old value
            /// </summary>
            private int oldValue;
            /// <summary>
            /// new value
            /// </summary>
            private int newValue;
            #endregion

            #region Public Constructor

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="oldValue">old value</param>
            /// <param name="newValue">new value</param>
            public PositionArgs(int oldValue, int newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }
            #endregion

            #region Public Properties

            /// <summary>
            /// Gets old position
            /// </summary>
            public int OldPosition
            {
                get { return this.oldValue; }
            }

            /// <summary>
            /// Gets new position
            /// </summary>
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

            /// <summary>
            /// Event when position has changed
            /// </summary>
            private event EventHandler<PositionArgs> positionChanged;
            /// <summary>
            /// Old and new position
            /// </summary>
            private int oldValue, newValue;
            /// <summary>
            /// Position
            /// </summary>
            private int position;
            /// <summary>
            /// Element à placer
            /// </summary>
            private PlacementItem item;

            #endregion

            #region Public Constructor

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="item">élement à placer</param>
            public HorizontalPlacementItem(PlacementItem item)
            {
                this.item = item;
                this.PositionChanged += action;
            }
            #endregion

            #region Private Methods

            /// <summary>
            /// Action position changed
            /// </summary>
            /// <param name="sender">origin</param>
            /// <param name="e">args</param>
            private void action(object sender, PositionArgs e)
            {
                this.oldValue = e.OldPosition;
                this.newValue = e.NewPosition;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets position
            /// </summary>
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

            /// <summary>
            /// Gets item
            /// </summary>
            public PlacementItem Item
            {
                get { return this.item; }
            }

            #endregion

            #region Events

            /// <summary>
            /// Add or remove event Position Changed
            /// </summary>
            public event EventHandler<PositionArgs> PositionChanged
            {
                add { this.positionChanged += new EventHandler<PositionArgs>(value); }
                remove { this.positionChanged -= new EventHandler<PositionArgs>(value); }
            }

            #endregion

        }

        /// <summary>
        /// Comparer for horizontal item
        /// </summary>
        internal static class HorizontalComparer
        {

            #region Public Static Methods

            /// <summary>
            /// Insert a new horizontal placement from a linked list
            /// based on the comparison of the horizontal position
            /// </summary>
            /// <param name="list">list d'éléments horizontaux successifs</param>
            /// <param name="input">élément horizontal</param>
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

            /// <summary>
            /// Compare two horizontal elements of placement
            /// </summary>
            /// <param name="x">placement item 1</param>
            /// <param name="y">placement item 2</param>
            /// <returns>1 or -1</returns>
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

            /// <summary>
            /// Event when position has changed
            /// </summary>
            private event EventHandler<PositionArgs> positionChanged;
            /// <summary>
            /// Old and new position
            /// </summary>
            private int oldValue, newValue;
            /// <summary>
            /// Position
            /// </summary>
            private int position;
            /// <summary>
            /// Linked list of horizontal element
            /// </summary>
            private LinkedList<HorizontalPlacementItem> list;

            #endregion

            #region Public Constructor

            /// <summary>
            /// Default constructor
            /// </summary>
            public VerticalPlacementItem()
            {
                this.list = new LinkedList<HorizontalPlacementItem>();
                this.PositionChanged += action;
            }

            #endregion

            #region Private Methods

            /// <summary>
            /// Action position changed
            /// </summary>
            /// <param name="sender">origin</param>
            /// <param name="e">args</param>
            private void action(object sender, PositionArgs e)
            {
                this.oldValue = e.OldPosition;
                this.newValue = e.NewPosition;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the linked list of horizontal item
            /// </summary>
            public LinkedList<HorizontalPlacementItem> Items
            {
                get { return this.list; }
            }

            /// <summary>
            /// Gets or sets position
            /// </summary>
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

            /// <summary>
            /// Compute the shortest position
            /// </summary>
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

            /// <summary>
            /// Add or remove event Position Changed
            /// </summary>
            public event EventHandler<PositionArgs> PositionChanged
            {
                add { this.positionChanged += new EventHandler<PositionArgs>(value); }
                remove { this.positionChanged -= new EventHandler<PositionArgs>(value); }
            }
            #endregion

            #region Public Methods

            /// <summary>
            /// Add a new horizontal item into this vertical item
            /// </summary>
            /// <param name="h">horizontal item</param>
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

            /// <summary>
            /// Compare this with one another vertical item
            /// </summary>
            /// <param name="other">vertical item to compare</param>
            /// <returns>1 or -1</returns>
            public int Compare(VerticalPlacementItem other)
            {
                return this.Compare(this, other);
            }

            #endregion

        }

        /// <summary>
        /// Class for the vertical comparer
        /// </summary>
        internal static class VerticalComparer
        {

            #region Public Constructor

            /// <summary>
            /// Insert a placement item into the right horizontal placement item
            /// </summary>
            /// <param name="list">vertical placement</param>
            /// <param name="p">element to place</param>
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

            /// <summary>
            /// Compare two vertical elements of placement
            /// </summary>
            /// <param name="x">placement item 1</param>
            /// <param name="y">placement item 2</param>
            /// <returns>1 or -1</returns>
            public static int Compare(VerticalPlacementItem x, VerticalPlacementItem y)
            {
                return y.Compare(x);
            }

            #endregion
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Linked list of vertical placement
        /// </summary>
        private LinkedList<VerticalPlacementItem> list;
        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlacementModel()
        {
            this.list = new LinkedList<VerticalPlacementItem>();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Inspect content
        /// </summary>
        /// <param name="p">placement</param>
        private void InspectQuadrille(PlacementItem p)
        {
            VerticalComparer.Compare(this.list, p);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Add a new element
        /// Create a new placement item, set update position changed event
        /// and place item
        /// </summary>
        /// <param name="cm">new cadre model</param>
        /// <param name="ratio">ratio</param>
        public void Add(CadreModel cm, float ratio)
        {
            PlacementItem p = new PlacementItem(cm, ratio);
            this.InspectQuadrille(p);
        }

        /// <summary>
        /// Computes all areas
        /// This function is not terminated.
        /// When two elements intersects each other, it takes a zIndex (3D)
        /// </summary>
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
