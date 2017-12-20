using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class BorderConstraint
    {
        BorderConstraint parent;
        uint quotientWidth, quotientHeight;
        uint counterWidth, counterHeight;
        bool alreadyComputedWidth, alreadyComputedHeight;
        uint masterBorderWidth, masterBorderHeight;
        uint totalCountLines, totalCountColumns;
        uint horizBorderWidth, horizBorderHeight;
        uint countLines;
        uint vertBorderWidth, vertBorderHeight;
        uint countColumns;

        private void AugmentCounterWidth(uint timing)
        {
            this.parent.counterWidth += timing;
        }

        private void AugmentCounterHeight(uint timing)
        {
            this.parent.counterHeight += timing;
        }

        private bool CompareCounterHeight(uint maximum)
        {
            return this.parent.counterHeight < maximum;
        }

        private bool CompareCounterWidth(uint maximum)
        {
            return this.parent.counterWidth < maximum;
        }

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

        public void ChangeTotalCountColumns(uint totalCountColumns)
        {
            this.totalCountColumns = totalCountColumns;
        }

        public uint ComputeBorderWidthForMaster(uint width)
        {
            return width - this.masterBorderWidth;
        }

        public uint RemoveBorderWidthForMaster(uint width)
        {
            return width + this.masterBorderWidth;
        }

        public uint ComputeBorderHeightForMaster(uint height)
        {
            return height - this.masterBorderHeight;
        }

        public uint RemoveBorderHeightForMaster(uint height)
        {
            return height + this.masterBorderHeight;
        }

        public uint ComputeBorderWidthForHorizontalZone(uint width)
        {
            return width - (this.masterBorderWidth + this.horizBorderWidth);
        }

        public uint RemoveBorderWidthForHorizontalZone(uint width)
        {
            return width + (this.masterBorderWidth + this.horizBorderWidth);
        }

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

        public uint ComputeBorderHeightForHorizontalZone(uint height)
        {
            uint newHeight = height;
            uint quotient = this.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight;
            return newHeight - quotient;
        }

        public uint RemoveBorderHeightForHorizontalZone(uint height)
        {
            return height + this.masterBorderHeight + this.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight;
        }

        public uint ComputeBorderHeightForVerticalZone(uint height)
        {
            uint newHeight = height;
            newHeight -= this.parent.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight + this.vertBorderHeight;
            return newHeight;
        }

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

        public uint ComputerBorderWidthForVerticalZone(uint width)
        {
            uint newWidth = width;
            uint quotient = this.ComputeQuotientForVerticalZone() + this.vertBorderWidth;
            return newWidth - (uint)quotient;
        }

        public uint ComputeBorderWidthForObject(uint width)
        {
            uint newWidth = width;
            uint quotient = this.parent.ComputeQuotientForVerticalZone() + this.vertBorderWidth;
            return newWidth - (uint)quotient;
        }

        public uint ComputerBorderHeightForObject(uint height)
        {
            uint newHeight = height;
            uint quotient = this.parent.ComputeQuotientForHorizontalZone(this.countLines) + this.horizBorderHeight + this.vertBorderHeight;
            return newHeight - (uint)quotient;
        }

        public static BorderConstraint CreateBorderConstraint(CodeCSS css, uint totalLines, uint totalColumns)
        {
            uint borderWidth = (uint)(css.Border.Left + css.Border.Right + css.Padding.Left + css.Padding.Right + css.Margin.Left + css.Margin.Right);
            uint borderHeight = (uint)(css.Border.Top + css.Border.Bottom + css.Padding.Top + css.Padding.Bottom + css.Margin.Top + css.Margin.Bottom);
            return new BorderConstraint(borderWidth, borderHeight, totalLines, totalColumns);
        }

        public static BorderConstraint CreateBorderConstraint(BorderConstraint parent, CodeCSS css, uint countLines)
        {
            uint borderWidth = (uint)(css.Border.Left + css.Border.Right + css.Padding.Left + css.Padding.Right + css.Margin.Left + css.Margin.Right);
            uint borderHeight = (uint)(css.Border.Top + css.Border.Bottom + css.Padding.Top + css.Padding.Bottom + css.Margin.Top + css.Margin.Bottom);
            return new BorderConstraint(parent, borderWidth, borderHeight, countLines);
        }

        public static BorderConstraint CreateBorderConstraint(BorderConstraint parent, CodeCSS css, uint countLines, uint countColumns)
        {
            uint borderWidth = (uint)(css.Border.Left + css.Border.Right + css.Padding.Left + css.Padding.Right + css.Margin.Left + css.Margin.Right);
            uint borderHeight = (uint)(css.Border.Top + css.Border.Bottom + css.Padding.Top + css.Padding.Bottom + css.Margin.Top + css.Margin.Bottom);
            return new BorderConstraint(parent, borderWidth, borderHeight, countLines, countColumns);
        }

        public static BorderConstraint CreateBorderConstraint(BorderConstraint parent)
        {
            return new BorderConstraint(parent);
        }
    }
}
