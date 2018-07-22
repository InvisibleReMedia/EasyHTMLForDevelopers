using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Granne : morceau dont la taille est modulo granneX et granneY
    /// </summary>
    internal class Granne : ICloneable
    {

        #region Private Static Fields

        /// <summary>
        /// rectangle
        /// </summary>
        private static System.Drawing.Rectangle? granneRect;
        /// <summary>
        /// taille d'une case en nombre de pixels
        /// pour toutes les instances
        /// </summary>
        private static uint granneX, granneY;
        /// <summary>
        /// total de cases en x et y pour toutes les instances
        /// </summary>
        private static System.Drawing.Size countCells;

        #endregion

        #region Private Fields

        /// <summary>
        /// model
        /// </summary>
        private CadreModel model;

        /// <summary>
        /// numeros de case en x et en y (de 1 à N)
        /// </summary>
        private uint indexX, indexY;
        /// <summary>
        /// nombre de cases en x et en y
        /// </summary>
        private uint sizeX, sizeY;
        /// <summary>
        /// reste de la division entiere (en nombres positifs ou nuls)
        /// </summary>
        private uint shiftLeft, shiftRight, shiftTop, shiftBottom; 
        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="c">cadre model appartenant à ce granne</param>
        /// <param name="left">left position in pixels</param>
        /// <param name="top">top position in pixels</param>
        /// <param name="width">width position in pixels</param>
        /// <param name="height">height position in pixels</param>
        public Granne(CadreModel c, int left, int top, int width, int height)
        {
            int remainder = 0, quotient = 0, granne;

            this.model = c;

            #region Calcul coin supérieur gauche

            granne = (int)Granne.granneX;
            quotient = Math.DivRem(left - Granne.TrueRectangle.Left, granne, out remainder);
            // le quotient peut être nul, indexX est strictement positif
            this.indexX = (uint)quotient;
            if (this.indexX > Granne.countCells.Width) Granne.countCells.Width = (int)this.indexX;
            // le reste de la division euclidienne par quotient
            this.shiftLeft = (uint)(this.indexX * Granne.granneX - remainder);
            granne = (int)Granne.granneY;
            quotient = Math.DivRem(top - Granne.TrueRectangle.Top, granne, out remainder);
            this.indexY = (uint)quotient;
            if (this.indexY > Granne.countCells.Height) Granne.countCells.Height = (int)this.indexY;
            this.shiftTop = (uint)(this.indexY * Granne.granneY - remainder);

            #endregion

            #region Calcul coin inférieur droit

            granne = (int)Granne.granneX;
            quotient = Math.DivRem(left + width - Granne.TrueRectangle.Left, granne, out remainder);
            // le quotient ne peut pas être nul car granneX est la plus petite largeur de tous les rectangles
            uint posX = (uint)quotient;
            this.sizeX = posX - this.indexX;
            if (this.indexX + this.sizeX > Granne.countCells.Width) Granne.countCells.Width = (int)(this.indexX + this.sizeX);
            // le reste de la division euclidienne par quotient
            this.shiftRight = (uint)remainder;
            // le quotient ne peut pas être nul car granneY est la plus petite hauteur de tous les rectangles
            granne = (int)Granne.granneY;
            quotient = Math.DivRem(top + height - Granne.TrueRectangle.Top, granne, out remainder);
            uint posY = (uint)quotient;
            this.sizeY = posY - this.indexY;
            if (this.indexY + this.sizeY > Granne.countCells.Height) Granne.countCells.Height = (int)(this.indexY + this.sizeY);
            this.shiftBottom = (uint)remainder;

            #endregion

        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="g">granne source</param>
        private Granne(Granne g)
        {
            this.model = g.model.Clone() as CadreModel;
            this.indexX = g.indexX; this.indexY = g.indexY;
            this.sizeX = g.sizeX; this.sizeY = g.sizeY;
            this.shiftLeft = g.shiftLeft; this.shiftRight = g.shiftRight;
            this.shiftTop = g.shiftTop; this.shiftBottom = g.shiftBottom;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Computes a list of cadre model
        /// </summary>
        /// <param name="modelList">list to host cadre model instances</param>
        public void InsertModel(List<CadreModel> modelList)
        {
            modelList.Add(this.model);
        }

        /// <summary>
        /// Insert a granne into a two-dimensional array
        /// </summary>
        /// <param name="tab">two-dimensional array of granne</param>
        public void InsertIntoArray(Granne[,] tab)
        {
            tab[this.indexX, this.indexY] = this;
        }

        /// <summary>
        /// Create a RefObject that contains what is your construction
        /// for this granne
        /// </summary>
        /// <param name="proj">project to use</param>
        /// <returns>a RefObject class</returns>
        public RefObject CreateRefObject(Project proj)
        {
            HTMLObject obj = Project.InstanciateSculptureTool(proj, this.model);
            if (obj != null)
                return new RefObject(RefObject.Tool, obj.Name, obj);
            else
                return null;
        }

        /// <summary>
        /// Insert vertical areas
        /// </summary>
        /// <param name="vZones">vertical area list</param>
        /// <param name="content">content</param>
        public void InsertVerticalZones(List<VerticalZone> vZones, RefObject content)
        {
            bool added = false;
            for (int index = 0; index < vZones.Count; ++index)
            {
                if (index > this.indexX)
                {
                    VerticalZone newV = new VerticalZone();
                    newV.ConstraintHeight = EnumConstraint.FIXED;
                    newV.ConstraintWidth = EnumConstraint.FIXED;
                    newV.Width = (this.sizeX * (uint)Granne.TrueRectangle.Width + this.shiftRight - this.shiftLeft);
                    newV.Height = (this.sizeY * (uint)Granne.TrueRectangle.Height + this.shiftBottom - this.shiftTop);
                    content.DirectObject.Container = newV.Name;
                    vZones.Insert(index, newV);
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                VerticalZone newV = new VerticalZone();
                newV.ConstraintHeight = EnumConstraint.FIXED;
                newV.ConstraintWidth = EnumConstraint.FIXED;
                newV.Width = (this.sizeX * (uint)Granne.TrueRectangle.Width + this.shiftRight - this.shiftLeft);
                newV.Height = (this.sizeY * (uint)Granne.TrueRectangle.Height + this.shiftBottom - this.shiftTop);
                if (content != null)
                    content.DirectObject.Container = newV.Name;
                vZones.Add(newV);
            }
        }

        /// <summary>
        /// Insert horizontal areas
        /// </summary>
        /// <param name="hZones">horizontal area list</param>
        /// <param name="content">content</param>
        public void InsertHorizontalZones(List<HorizontalZone> hZones, RefObject content)
        {
            bool added = false;
            for(int index = 0; index < hZones.Count; ++index)
            {
                if (index > this.indexX)
                {
                    HorizontalZone newH = new HorizontalZone();
                    newH.ConstraintHeight = EnumConstraint.FIXED;
                    newH.ConstraintWidth = EnumConstraint.FIXED;
                    newH.Width = (this.sizeX * (uint)Granne.TrueRectangle.Width + this.shiftRight - this.shiftLeft);
                    newH.Height = (this.sizeY * (uint)Granne.TrueRectangle.Height + this.shiftBottom - this.shiftTop);
                    this.InsertVerticalZones(newH.VerticalZones, content);
                    hZones.Insert(index, newH);
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                HorizontalZone newH = new HorizontalZone();
                newH.ConstraintHeight = EnumConstraint.FIXED;
                newH.ConstraintWidth = EnumConstraint.FIXED;
                newH.Width = (this.sizeX * (uint)Granne.TrueRectangle.Width + this.shiftRight - this.shiftLeft);
                newH.Height = (this.sizeY * (uint)Granne.TrueRectangle.Height + this.shiftBottom - this.shiftTop);
                this.InsertVerticalZones(newH.VerticalZones, content);
                hZones.Add(newH);
            }
        }

        /// <summary>
        /// Convert granne to drawing rectangle
        /// </summary>
        /// <returns>sized rectangle</returns>
        internal SizedRectangle ConvertToDrawingRectangle()
        {
            int width = (int)(this.sizeX * (uint)Granne.granneX);
            int height = (int)(this.sizeY * (uint)Granne.granneY);
            return new SizedRectangle(width, height, (int)this.indexX, (int)(this.indexX + this.sizeX), (int)this.indexY, (int)(this.indexY + this.sizeY));
        }


        /// <summary>
        /// Computes the true size
        /// </summary>
        internal System.Drawing.Size TrueSize
        {
            get
            {
                System.Drawing.Size s = new System.Drawing.Size();
                s.Width = (int)(this.indexX * (uint)Granne.Size.Width + this.shiftRight - this.shiftLeft);
                s.Height = (int)(this.indexX * (uint)Granne.Size.Height + this.shiftBottom - this.shiftTop);
                return s;
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Initialize Granne sequence construction
        /// </summary>
        public static void Init()
        {
            Granne.granneRect = null;
            Granne.granneX = 0; Granne.granneY = 0;
            Granne.countCells = new System.Drawing.Size(0, 0);
        }

        /// <summary>
        /// Set the global true rectangle
        /// </summary>
        /// <param name="left">current granne left</param>
        /// <param name="top">current granne top</param>
        /// <param name="right">current granne right</param>
        /// <param name="bottom">current granne bottom</param>
        public static void SetTrueRect(int left, int top, int right, int bottom)
        {
            if (Granne.granneRect.HasValue)
            {
                int minX = 0, minY = 0, maxX = 0, maxY = 0;
                if (Granne.granneRect.Value.Left > left) minX = left; else minX = Granne.granneRect.Value.Left;
                if (Granne.granneRect.Value.Top > top) minY = top; else minY = Granne.granneRect.Value.Top;
                if (Granne.granneRect.Value.Right < right) maxX = right; else maxX = Granne.granneRect.Value.Right;
                if (Granne.granneRect.Value.Bottom < bottom) maxY = bottom; else maxY = Granne.granneRect.Value.Bottom;
                Granne.granneRect = new System.Drawing.Rectangle(minX, minY, maxX - minX, maxY - minY);
            }
            else
            {
                Granne.granneRect = new System.Drawing.Rectangle(left, top, right - left, bottom - top);
            }
        }

        /// <summary>
        /// Set the minimum size for all grannes
        /// </summary>
        /// <param name="minX">minimum size x</param>
        /// <param name="minY">minimum size y</param>
        public static void SetMinGranne(int minX, int minY)
        {
            Granne.granneX = (uint)minX;
            Granne.granneY = (uint)minY;
        }

        /// <summary>
        /// Compute for all cadre model
        /// to select the minimum size cadre model
        /// </summary>
        /// <param name="list">list of cadre model</param>
        public static void ComputeGranne(List<CadreModel> list)
        {
            Granne.SetMinGranne(list.Min(a => a.Width), list.Min(a => a.Height));
        }

        /// <summary>
        /// Compares two grannes by horizontal index
        /// return 1 if g1 >= g2 else -1
        /// </summary>
        /// <param name="g1">granne 1</param>
        /// <param name="g2">granne 2</param>
        /// <returns>1 or -1</returns>
        public static int HorizontalComparer(Granne g1, Granne g2)
        {
            if (g1.indexX >= g2.indexX) return 1;
            else return -1;
        }

        /// <summary>
        /// Compares two grannes by vertical index
        /// return 1 if g1 >= g2 else -1
        /// </summary>
        /// <param name="g1"></param>
        /// <param name="g2"></param>
        /// <returns></returns>
        public static int VerticalComparer(Granne g1, Granne g2)
        {
            if (g1.indexY >= g2.indexY) return 1;
            else return -1;
        }

        /// <summary>
        /// Gets the total size for all cadre model list
        /// </summary>
        public static System.Drawing.Size Size
        {
            get { return Granne.countCells; }
        }

        /// <summary>
        /// Gets the true rectangle
        /// </summary>
        public static System.Drawing.Rectangle TrueRectangle
        {
            get { return (Granne.granneRect.HasValue) ? Granne.granneRect.Value : new System.Drawing.Rectangle(); }
        }

        /// <summary>
        /// Gets the unity size
        /// </summary>
        internal static System.Drawing.Size UnitySize
        {
            get
            {
                return new System.Drawing.Size((int)Granne.granneX, (int)Granne.granneY);
            }
        }
        #endregion

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            Granne newObject = new Granne(this);
            return newObject;
        }
    }

    /// <summary>
    /// Horizontal granne comparer
    /// </summary>
    internal class HorizontalGranneComparer : IComparer<Granne>
    {
        /// <summary>
        /// Compares two granne by horizontal index
        /// </summary>
        /// <param name="x">granne 1</param>
        /// <param name="y">granne 2</param>
        /// <returns>1 or -1</returns>
        public int Compare(Granne x, Granne y)
        {
            return Granne.HorizontalComparer(x, y);
        }
    }

    /// <summary>
    /// Vertical granne comparer
    /// </summary>
    internal class VerticalGranneComparer : IComparer<Granne>
    {
        /// <summary>
        /// Compares two granne by vertical index
        /// </summary>
        /// <param name="x">granne 1</param>
        /// <param name="y">granne 2</param>
        /// <returns>1 or -1</returns>
        public int Compare(Granne x, Granne y)
        {
            return Granne.VerticalComparer(x, y);
        }
    }

}
