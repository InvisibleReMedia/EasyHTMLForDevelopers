using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Border constraint
    /// A border is a space with a colored pen that changes
    /// the size of the content or container
    /// </summary>
    public class BorderConstraint
    {

        #region Fields

        /// <summary>
        /// Parent (container)
        /// </summary>
        BorderConstraint parent;
        /// <summary>
        /// quotient
        /// </summary>
        uint quotientWidth, quotientHeight;
        /// <summary>
        /// Counter
        /// </summary>
        uint counterWidth, counterHeight;
        /// <summary>
        /// Previous
        /// </summary>
        bool alreadyComputedWidth, alreadyComputedHeight;
        /// <summary>
        /// Relative to a master page or a master object
        /// </summary>
        uint masterBorderWidth, masterBorderHeight;
        /// <summary>
        /// Count of lines and columns
        /// </summary>
        uint totalCountLines, totalCountColumns;
        /// <summary>
        /// Horizontal area
        /// </summary>
        uint horizBorderWidth, horizBorderHeight;
        /// <summary>
        /// Count of lines
        /// </summary>
        uint countLines;
        /// <summary>
        /// Vertical area
        /// </summary>
        uint vertBorderWidth, vertBorderHeight;
        /// <summary>
        /// Count of columns
        /// </summary>
        uint countColumns;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with the size of border
        /// </summary>
        /// <param name="borderWidth">width size</param>
        /// <param name="borderHeight">height size</param>
        /// <param name="countLines">count of lines</param>
        /// <param name="countColumns">count of columns</param>
        public BorderConstraint(uint borderWidth, uint borderHeight, uint countLines, uint countColumns)
        {
            this.parent = null;
            this.quotientWidth = this.quotientHeight = 0;
            this.counterWidth = this.counterHeight = 0;
            this.masterBorderWidth = borderWidth;
            this.masterBorderHeight = borderHeight;
            this.totalCountLines = countLines;
            this.totalCountColumns = countColumns;
            this.horizBorderWidth = 0;
            this.horizBorderHeight = 0;
            this.countLines = 0;
            this.vertBorderWidth = 0;
            this.vertBorderHeight = 0;
            this.countColumns = 0;
            this.alreadyComputedHeight = false;
            this.alreadyComputedWidth = false;
        }

        /// <summary>
        /// Constructor with a border constraint parent and a new horizontal border
        /// </summary>
        /// <param name="parent">parent</param>
        /// <param name="borderWidth">width size</param>
        /// <param name="borderHeight">height size</param>
        /// <param name="countLines">count of lines</param>
        public BorderConstraint(BorderConstraint parent, uint borderWidth, uint borderHeight, uint countLines)
        {
            this.parent = parent;
            this.quotientWidth = this.quotientHeight = 0;
            this.masterBorderWidth = parent.masterBorderWidth;
            this.masterBorderHeight = parent.masterBorderHeight;
            this.totalCountLines = parent.totalCountLines;
            this.totalCountColumns = parent.totalCountColumns;
            this.horizBorderWidth = borderWidth;
            this.horizBorderHeight = borderHeight;
            this.countLines = countLines;
            this.vertBorderWidth = 0;
            this.vertBorderHeight = 0;
            this.countColumns = 0;
            this.alreadyComputedHeight = false;
            this.alreadyComputedWidth = false;
        }

        /// <summary>
        /// Constructor with a border constraint parent and a new horizontal and vertical border
        /// </summary>
        /// <param name="parent">parent</param>
        /// <param name="borderWidth">width size</param>
        /// <param name="borderHeight">height size</param>
        /// <param name="countLines">count of lines</param>
        /// <param name="countColumns">count of columns</param>
        public BorderConstraint(BorderConstraint parent, uint borderWidth, uint borderHeight, uint countLines, uint countColumns)
        {
            this.parent = parent;
            this.quotientWidth = this.quotientHeight = 0;
            this.masterBorderWidth = parent.masterBorderWidth;
            this.masterBorderHeight = parent.masterBorderHeight;
            this.totalCountLines = parent.totalCountLines;
            this.totalCountColumns = parent.totalCountColumns;
            this.horizBorderWidth = parent.horizBorderWidth;
            this.horizBorderHeight = parent.horizBorderHeight;
            this.countLines = countLines;
            this.vertBorderWidth = borderWidth;
            this.vertBorderHeight = borderHeight;
            this.countColumns = countColumns;
            this.alreadyComputedHeight = false;
            this.alreadyComputedWidth = false;
        }

        /// <summary>
        /// Create a border constraint with an
        /// existing parent constraint
        /// </summary>
        /// <param name="parent">parent</param>
        public BorderConstraint(BorderConstraint parent)
        {
            this.parent = parent;
            this.quotientWidth = this.quotientHeight = 0;
            this.masterBorderWidth = parent.masterBorderWidth;
            this.masterBorderHeight = parent.masterBorderHeight;
            this.totalCountLines = parent.totalCountLines;
            this.totalCountColumns = parent.totalCountColumns;
            this.horizBorderWidth = parent.horizBorderWidth;
            this.horizBorderHeight = parent.horizBorderHeight;
            this.countLines = parent.countLines;
            this.vertBorderWidth = parent.vertBorderWidth;
            this.vertBorderHeight = parent.vertBorderHeight;
            this.countColumns = parent.countColumns;
            this.alreadyComputedHeight = false;
            this.alreadyComputedWidth = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add counter width to the parent constraint
        /// </summary>
        /// <param name="timing">move</param>
        private void AugmentCounterWidth(uint timing)
        {
            this.parent.counterWidth += timing;
        }

        /// <summary>
        /// Add counter width to the parent constraint
        /// </summary>
        /// <param name="timing">move</param>
        private void AugmentCounterHeight(uint timing)
        {
            this.parent.counterHeight += timing;
        }

        /// <summary>
        /// Compare the parent counter height with this maximum value
        /// </summary>
        /// <param name="maximum">maximum value</param>
        /// <returns>true if lesser</returns>
        private bool CompareCounterHeight(uint maximum)
        {
            return this.parent.counterHeight < maximum;
        }

        /// <summary>
        /// Compare the parent counter width with this maximum value
        /// </summary>
        /// <param name="maximum">maximum value</param>
        /// <returns>true if lesser</returns>
        private bool CompareCounterWidth(uint maximum)
        {
            return this.parent.counterWidth < maximum;
        }

        /// <summary>
        /// Lorsqu'il faut augmenter la taille du bord
        /// pour que l'ensemble reste homogène
        /// </summary>
        /// <param name="timing">move value</param>
        /// <param name="maximum">maximum value admitted</param>
        /// <returns>grown size</returns>
        private uint LimitedCounterWidth(uint timing, uint maximum)
        {
            // si il y en a déjà assez
            if (this.CompareCounterWidth(maximum))
            {
                // si on en ajoute
                if (this.parent.counterWidth + timing <= maximum)
                {
                    this.AugmentCounterWidth(timing);
                    return timing;
                }
                else
                {
                    // on ajoute le reste
                    uint left = maximum - this.parent.counterWidth;
                    this.AugmentCounterWidth(left);
                    return left;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Lorsqu'il faut augmenter la taille du bord
        /// pour que l'ensemble reste homogène
        /// </summary>
        /// <param name="timing">move value</param>
        /// <param name="maximum">maximum value admitted</param>
        /// <returns>grown size</returns>
        private uint LimitedCounterHeight(uint timing, uint maximum)
        {
            // si il y en a déjà assez
            if (this.CompareCounterHeight(maximum))
            {
                // si on en ajoute
                if (this.parent.counterHeight + timing <= maximum)
                {
                    this.AugmentCounterHeight(timing);
                    return timing;
                }
                else
                {
                    // on ajoute le reste
                    uint left = maximum - this.parent.counterHeight;
                    this.AugmentCounterHeight(left);
                    return left;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Replace total count columns
        /// </summary>
        /// <param name="totalCountColumns">new value</param>
        public void ChangeTotalCountColumns(uint totalCountColumns)
        {
            this.totalCountColumns = totalCountColumns;
        }

        /// <summary>
        /// Border remaining between master and this width
        /// </summary>
        /// <param name="width">width</param>
        /// <returns>the remainer width</returns>
        public uint ComputeBorderWidthForMaster(uint width)
        {
            return width - this.masterBorderWidth;
        }

        /// <summary>
        /// Remove this border width and leave all to master
        /// </summary>
        /// <param name="width">width</param>
        /// <returns>new border size</returns>
        public uint RemoveBorderWidthForMaster(uint width)
        {
            return width + this.masterBorderWidth;
        }

        /// <summary>
        /// Border remaining between master and this height
        /// </summary>
        /// <param name="height">height</param>
        /// <returns>the remainer height</returns>
        public uint ComputeBorderHeightForMaster(uint height)
        {
            return height - this.masterBorderHeight;
        }

        /// <summary>
        /// Remove this border height and leave all to master
        /// </summary>
        /// <param name="height">height</param>
        /// <returns>new border size</returns>
        public uint RemoveBorderHeightForMaster(uint height)
        {
            return height + this.masterBorderHeight;
        }

        /// <summary>
        /// Border remaining between master, horizontal area and this width
        /// </summary>
        /// <param name="width">width</param>
        /// <returns>the remainer width</returns>
        public uint ComputeBorderWidthForHorizontalZone(uint width)
        {
            return width - (this.masterBorderWidth + this.horizBorderWidth);
        }

        /// <summary>
        /// Border remaining between master, horizontal area and this height
        /// </summary>
        /// <param name="height">height</param>
        /// <returns>new size value</returns>
        public uint ComputeBorderHeightForHorizontalZone(uint height)
        {
            uint newHeight = height;
            uint quotient = this.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight;
            return newHeight - quotient;
        }

        /// <summary>
        /// Remove this border width and leave all to master
        /// </summary>
        /// <param name="width">width</param>
        /// <returns>new border size</returns>
        public uint RemoveBorderWidthForHorizontalZone(uint width)
        {
            return width + (this.masterBorderWidth + this.horizBorderWidth);
        }

        /// <summary>
        /// Remove this border with and leave all to master
        /// </summary>
        /// <param name="height">height</param>
        /// <returns>new border size</returns>
        public uint RemoveBorderHeightForHorizontalZone(uint height)
        {
            return height + this.masterBorderHeight + this.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight;
        }

        /// <summary>
        /// Computes quotient for horizontal area
        /// </summary>
        /// <param name="countLines">count of lines</param>
        /// <returns>quotient</returns>
        private uint ComputeQuotientForHorizontalZone(uint countLines)
        {
            if (!this.alreadyComputedHeight || this.countLines != countLines)
            {
                this.alreadyComputedHeight = true;
                if (this.masterBorderHeight > 0)
                {
                    if ((this.masterBorderHeight) >= this.totalCountLines)
                    {
                        // on compte le nombre de pixels nécessaire et suffisant pour une ligne de taille 1
                        uint countPixelPerLine = (this.masterBorderHeight) / this.totalCountLines;
                        uint leftPart = (this.masterBorderHeight) % this.totalCountLines;
                        countPixelPerLine += leftPart;
                        // cette ligne peut être plus grande que 1
                        uint quotient = countLines * countPixelPerLine;
                        if (this.countLines == countLines)
                        {
                            // il ne faut pas que le nombre de pixels total soit dépassé
                            this.quotientHeight = this.LimitedCounterHeight(quotient, this.masterBorderHeight);
                            return this.quotientHeight;
                        }
                        else
                        {
                            quotient = this.LimitedCounterHeight(quotient, this.masterBorderHeight);
                            return quotient;
                        }
                    }
                    else
                    {
                        // on compte le nombre de pixels nécessaire et suffisant pour une ligne de taille 1
                        uint countLinesPerPixel = this.totalCountLines / (this.masterBorderHeight);
                        uint leftPart = this.totalCountLines % (this.masterBorderHeight);
                        //countLinesPerPixel += leftPart;
                        // cette ligne peut être plus grande que 1
                        uint quotient = countLines / countLinesPerPixel;
                        uint left = countLines % countLinesPerPixel;
                        quotient += left;
                        // il ne faut pas que le nombre de pixels total soit dépassé
                        if (this.countLines == countLines)
                        {
                            // il ne faut pas que le nombre de pixels total soit dépassé
                            this.quotientHeight = this.LimitedCounterHeight(quotient, this.masterBorderHeight);
                            return this.quotientHeight;
                        }
                        else
                        {
                            quotient = this.LimitedCounterHeight(quotient, this.masterBorderHeight);
                            return quotient;
                        }
                    }
                }
                else
                {
                    this.quotientHeight = 0;
                    return 0;
                }
            }
            else
            {
                return this.quotientHeight;
            }
        }

        /// <summary>
        /// Border remaining between master, vertical area and this height
        /// </summary>
        /// <param name="height">height</param>
        /// <returns>new size value</returns>
        public uint ComputeBorderHeightForVerticalZone(uint height)
        {
            uint newHeight = height;
            newHeight -= this.parent.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight + this.vertBorderHeight;
            return newHeight;
        }

        /// <summary>
        /// Border remaining between master, vertical area and this width
        /// </summary>
        /// <param name="width">width</param>
        /// <returns>new size value</returns>
        public uint ComputerBorderWidthForVerticalZone(uint width)
        {
            uint newWidth = width;
            uint quotient = this.ComputeQuotientForVerticalZone() + this.vertBorderWidth;
            return newWidth - (uint)quotient;
        }

        /// <summary>
        /// Compute the final border width for content
        /// </summary>
        /// <param name="width">origin width</param>
        /// <returns>new width</returns>
        public uint ComputeBorderWidthForObject(uint width)
        {
            uint newWidth = width;
            uint quotient = this.parent.ComputeQuotientForVerticalZone() + this.vertBorderWidth;
            return newWidth - (uint)quotient;
        }

        /// <summary>
        /// Compute the final border height for content
        /// </summary>
        /// <param name="height">origin height</param>
        /// <returns>new height</returns>
        public uint ComputerBorderHeightForObject(uint height)
        {
            uint newHeight = height;
            uint quotient = this.parent.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight + this.vertBorderHeight;
            return newHeight - (uint)quotient;
        }

        /// <summary>
        /// Computes quotient for vertical area
        /// </summary>
        /// <returns>quotient</returns>
        private uint ComputeQuotientForVerticalZone()
        {
            if (!this.alreadyComputedWidth)
            {
                this.alreadyComputedWidth = true;
                if (this.masterBorderWidth + this.horizBorderWidth > 0 && this.parent.countLines == this.countLines)
                {
                    if ((this.masterBorderWidth + this.horizBorderWidth) >= this.totalCountColumns)
                    {
                        // on compte le nombre de pixels nécessaire et suffisant pour une colonne de taille 1
                        uint countPixelPerColumn = (this.masterBorderWidth + this.horizBorderWidth) / this.totalCountColumns;
                        uint leftPart = (this.masterBorderWidth + this.horizBorderWidth) % this.totalCountColumns;
                        countPixelPerColumn += leftPart;
                        // cette colonne peut être plus grande que 1
                        uint quotient = this.countColumns * countPixelPerColumn;
                        // il ne faut pas que le nombre de pixels total soit dépassé
                        this.quotientWidth = this.LimitedCounterWidth(quotient, this.masterBorderWidth + this.horizBorderWidth);
                        return this.quotientWidth;
                    }
                    else
                    {
                        // on compte le nombre de pixels nécessaire et suffisant pour une colonne de taille 1
                        uint countColumnsPerPixel = this.totalCountColumns / (this.masterBorderWidth + this.horizBorderWidth);
                        uint leftPart = this.totalCountColumns % (this.masterBorderWidth + this.horizBorderWidth);
                        //countColumnsPerPixel += leftPart;
                        // cette colonne peut être plus grande que 1
                        uint quotient = this.countColumns / countColumnsPerPixel;
                        uint left = this.countColumns % countColumnsPerPixel;
                        quotient += left;
                        // il ne faut pas que le nombre de pixels total soit dépassé
                        this.quotientWidth = this.LimitedCounterWidth(quotient, this.masterBorderWidth + this.horizBorderWidth);
                        return this.quotientWidth;
                    }
                }
                else
                {
                    this.quotientWidth = 0;
                    return 0;
                }
            }
            else
            {
                return this.quotientWidth;
            }
        }

        /// <summary>
        /// Static constructor from a css, the total number of lines and columns
        /// </summary>
        /// <param name="css">css</param>
        /// <param name="totalLines">total number of lines</param>
        /// <param name="totalColumns">total number of columns</param>
        /// <returns>Border constraint</returns>
        public static BorderConstraint CreateBorderConstraint(CodeCSS css, uint totalLines, uint totalColumns)
        {
            uint borderWidth = (uint)(css.Border.Left + css.Border.Right + css.Padding.Left + css.Padding.Right + css.Margin.Left + css.Margin.Right);
            uint borderHeight = (uint)(css.Border.Top + css.Border.Bottom + css.Padding.Top + css.Padding.Bottom + css.Margin.Top + css.Margin.Bottom);
            return new BorderConstraint(borderWidth, borderHeight, totalLines, totalColumns);
        }

        /// <summary>
        /// Static constructor from a parent border constraint, a css, the number of lines
        /// </summary>
        /// <param name="parent">parent constraint</param>
        /// <param name="css">css</param>
        /// <param name="countLines">number of lines</param>
        /// <returns>Border constraint</returns>
        public static BorderConstraint CreateBorderConstraint(BorderConstraint parent, CodeCSS css, uint countLines)
        {
            uint borderWidth = (uint)(css.Border.Left + css.Border.Right + css.Padding.Left + css.Padding.Right + css.Margin.Left + css.Margin.Right);
            uint borderHeight = (uint)(css.Border.Top + css.Border.Bottom + css.Padding.Top + css.Padding.Bottom + css.Margin.Top + css.Margin.Bottom);
            return new BorderConstraint(parent, borderWidth, borderHeight, countLines);
        }

        /// <summary>
        /// Static constructor from a parent border constraint, a css, the number of lines and columns
        /// </summary>
        /// <param name="parent">parent constraint</param>
        /// <param name="css">css</param>
        /// <param name="countLines">number of lines</param>
        /// <param name="countColumns">number of columns</param>
        /// <returns>Border constraint</returns>
        public static BorderConstraint CreateBorderConstraint(BorderConstraint parent, CodeCSS css, uint countLines, uint countColumns)
        {
            uint borderWidth = (uint)(css.Border.Left + css.Border.Right + css.Padding.Left + css.Padding.Right + css.Margin.Left + css.Margin.Right);
            uint borderHeight = (uint)(css.Border.Top + css.Border.Bottom + css.Padding.Top + css.Padding.Bottom + css.Margin.Top + css.Margin.Bottom);
            return new BorderConstraint(parent, borderWidth, borderHeight, countLines, countColumns);
        }

        /// <summary>
        /// Border Constraint from a parent
        /// </summary>
        /// <param name="parent">parent</param>
        /// <returns>new border constraint</returns>
        public static BorderConstraint CreateBorderConstraint(BorderConstraint parent)
        {
            return new BorderConstraint(parent);
        }

        #endregion

    }
}
