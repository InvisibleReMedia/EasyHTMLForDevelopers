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
        private static System.Drawing.Rectangle? granneRect;
        private static uint granneX, granneY; // taille d'une case en # pixels
        private static System.Drawing.Size countCells; // total de cases en x et y
        #endregion

        #region Private Fields
        private CadreModel model;
        private uint indexX, indexY; // numeros de case en x et en y (de 1 à N)
        private uint sizeX, sizeY; // nombre de cases en x et en y
        private uint shiftLeft, shiftRight, shiftTop, shiftBottom; // reste division entiere (en nombres positifs ou nuls)
        #endregion

        #region Public Constructor
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
        public void InsertModel(List<CadreModel> modelList)
        {
            modelList.Add(this.model);
        }

        public void InsertIntoArray(Granne[,] tab)
        {
            tab[this.indexX, this.indexY] = this;
        }

        public RefObject CreateRefObject(Project proj)
        {
            HTMLObject obj = Project.InstanciateSculptureTool(proj, this.model);
            if (obj != null)
                return new RefObject(RefObject.ToolInstance, obj.Name, obj);
            else
                return null;
        }

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

        internal SizedRectangle ConvertToDrawingRectangle()
        {
            int width = (int)(this.sizeX * (uint)Granne.granneX);
            int height = (int)(this.sizeY * (uint)Granne.granneY);
            return new SizedRectangle(width, height, (int)this.indexX, (int)(this.indexX + this.sizeX), (int)this.indexY, (int)(this.indexY + this.sizeY));
        }


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
        public static void Init()
        {
            Granne.granneRect = null;
            Granne.granneX = 0; Granne.granneY = 0;
            Granne.countCells = new System.Drawing.Size(0, 0);
        }

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

        public static void SetMinGranne(int minX, int minY)
        {
            Granne.granneX = (uint)minX;
            Granne.granneY = (uint)minY;
        }

        public static void ComputeGranne(List<CadreModel> list)
        {
            Granne.SetMinGranne(list.Min(a => a.Largeur), list.Min(a => a.Hauteur));
        }

        public static int HorizontalComparer(Granne g1, Granne g2)
        {
            if (g1.indexX >= g2.indexX) return 1;
            else return -1;
        }

        public static int VerticalComparer(Granne g1, Granne g2)
        {
            if (g1.indexY >= g2.indexY) return 1;
            else return -1;
        }

        public static System.Drawing.Size Size
        {
            get { return Granne.countCells; }
        }

        public static System.Drawing.Rectangle TrueRectangle
        {
            get { return (Granne.granneRect.HasValue) ? Granne.granneRect.Value : new System.Drawing.Rectangle(); }
        }

        internal static System.Drawing.Size UnitySize
        {
            get
            {
                return new System.Drawing.Size((int)Granne.granneX, (int)Granne.granneY);
            }
        }
        #endregion

        public object Clone()
        {
            Granne newObject = new Granne(this);
            return newObject;
        }
    }

    internal class HorizontalGranneComparer : IComparer<Granne>
    {
        public int Compare(Granne x, Granne y)
        {
            return Granne.HorizontalComparer(x, y);
        }
    }

    internal class VerticalGranneComparer : IComparer<Granne>
    {
        public int Compare(Granne x, Granne y)
        {
            return Granne.VerticalComparer(x, y);
        }
    }

}
