using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class AdjustementSize
    {
        #region Private Fields
        private EnumConstraint constraintWidth;
        private EnumConstraint constraintHeight;
        private uint width;
        private uint height;
        #endregion

        #region Default Constructor
        public AdjustementSize(EnumConstraint constraintWidth, EnumConstraint constraintHeight, uint width, uint height)
        {
            this.constraintWidth = constraintWidth;
            this.constraintHeight = constraintHeight;
            this.width = width;
            this.height = height;
        }
        #endregion

        #region Public Properties
        public EnumConstraint ConstraintWidth
        {
            get { return this.constraintWidth; }
            set { this.constraintWidth = value; }
        }

        public EnumConstraint ConstraintHeight
        {
            get { return this.constraintWidth; }
            set { this.constraintWidth = value; }
        }

        public uint Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public uint Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        #endregion
    }

    public static class SizeCompute
    {

        private static void CheckConstraints(AdjustementSize container, AdjustementSize content)
        {
            switch (container.ConstraintWidth)
            {
                case EnumConstraint.AUTO:
                    switch (content.ConstraintWidth)
                    {
                        case EnumConstraint.FIXED:
                            container.ConstraintWidth = EnumConstraint.FIXED;
                            container.Width = content.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            content.ConstraintWidth = EnumConstraint.AUTO;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (content.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            content.ConstraintWidth = EnumConstraint.FIXED;
                            if (container.Width < content.Width)
                            {
                                container.Width = content.Width;
                            }
                            break;
                        case EnumConstraint.FIXED:
                            if (container.Width < content.Width)
                            {
                                container.Width = content.Width;
                            }
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (content.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            container.ConstraintWidth = EnumConstraint.AUTO;
                            break;
                    }
                    break;
            }

            switch (container.ConstraintHeight)
            {
                case EnumConstraint.AUTO:
                    switch (content.ConstraintHeight)
                    {
                        case EnumConstraint.FIXED:
                            container.ConstraintHeight = EnumConstraint.FIXED;
                            container.Height = content.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            content.ConstraintHeight = EnumConstraint.AUTO;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (content.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            content.ConstraintHeight = EnumConstraint.FIXED;
                            if (container.Height < content.Height)
                            {
                                container.Height = content.Height;
                            }
                            break;
                        case EnumConstraint.FIXED:
                            if (container.Height < content.Height)
                            {
                                container.Height = content.Height;
                            }
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (content.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            container.ConstraintHeight = EnumConstraint.AUTO;
                            break;
                    }
                    break;
            }
        }

        private static AdjustementSize CheckConstraints(HorizontalZone hz)
        {
            IEnumerator<VerticalZone> e = hz.VerticalZones.GetEnumerator();
            int nCol = 0;
            bool aligned = true;
            if (e.MoveNext())
            {
                nCol = e.Current.CountLines;
                while (e.MoveNext())
                {
                    if (nCol != e.Current.CountLines)
                    {
                        aligned = false;
                        break;
                    }
                }
            }
            uint totalWidth = 0;
            uint maxHeight = 0;
            AdjustementSize container = new AdjustementSize(hz.ConstraintWidth, hz.ConstraintHeight, hz.Width, hz.Height);
            e.Reset();
            while (e.MoveNext())
            {
                int nonClientWidth = e.Current.CSS.Padding.Left + e.Current.CSS.Padding.Right + e.Current.CSS.Border.Left + e.Current.CSS.Border.Right + e.Current.CSS.Margin.Left + e.Current.CSS.Margin.Right;
                int nonClientHeight = e.Current.CSS.Padding.Top + e.Current.CSS.Padding.Bottom + e.Current.CSS.Border.Top + e.Current.CSS.Border.Bottom + e.Current.CSS.Margin.Top + e.Current.CSS.Margin.Bottom;
                uint width = e.Current.Width;
                if (nonClientWidth > 0) width += Convert.ToUInt32(nonClientWidth); else if (width > Convert.ToUInt32(nonClientWidth)) width -= Convert.ToUInt32(nonClientWidth);
                uint height = e.Current.Height;
                if (nonClientHeight > 0) height += Convert.ToUInt32(nonClientHeight); else if (height > Convert.ToUInt32(nonClientHeight)) height -= Convert.ToUInt32(nonClientHeight);

                // set information
                AdjustementSize content = new AdjustementSize(e.Current.ConstraintWidth, e.Current.ConstraintHeight, width, height);

                // compute
                SizeCompute.CheckConstraints(container, content);

                // update information
                switch (content.ConstraintWidth)
                {
                    case EnumConstraint.FIXED:
                        totalWidth += content.Width;
                        break;
                    case EnumConstraint.RELATIVE:
                        break;
                }

                switch (content.ConstraintHeight)
                {
                    case EnumConstraint.FIXED:
                        if (maxHeight < content.Height)
                            maxHeight = content.Height;
                        break;
                    case EnumConstraint.RELATIVE:
                        break;
                }

                e.Current.ConstraintWidth = content.ConstraintWidth;
                e.Current.ConstraintHeight = content.ConstraintHeight;
            }

            hz.ConstraintWidth = container.ConstraintWidth;
            hz.ConstraintHeight = container.ConstraintHeight;
            hz.Width = totalWidth;
            hz.Height = maxHeight;

            if (!aligned)
            {
                hz.ConstraintHeight = EnumConstraint.AUTO;
            }

            return new AdjustementSize(hz.ConstraintWidth, hz.ConstraintHeight, hz.Width, hz.Height);
        }

        public static AdjustementSize ComputeHTMLObject(Project p, HTMLObject obj)
        {
            if (obj.IsMasterObject)
            {
                MasterObject mo = p.MasterObjects.Find(a => a.Name == obj.MasterObjectName);
                if (mo != null)
                {
                    int nonClientWidth = mo.CSS.Padding.Left + mo.CSS.Padding.Right + mo.CSS.Border.Left + mo.CSS.Border.Right + mo.CSS.Margin.Left + mo.CSS.Margin.Right;
                    int nonClientHeight = mo.CSS.Padding.Top + mo.CSS.Padding.Bottom + mo.CSS.Border.Top + mo.CSS.Border.Bottom + mo.CSS.Margin.Top + mo.CSS.Margin.Bottom;
                    uint width = mo.Width;
                    if (nonClientWidth > 0) width += Convert.ToUInt32(nonClientWidth); else if (width > Convert.ToUInt32(nonClientWidth)) width -= Convert.ToUInt32(nonClientWidth);
                    uint height = mo.Height;
                    if (nonClientHeight > 0) height += Convert.ToUInt32(nonClientHeight); else if (height > Convert.ToUInt32(nonClientHeight)) height -= Convert.ToUInt32(nonClientHeight);

                    // set information
                    AdjustementSize container = new AdjustementSize(obj.ConstraintWidth, obj.ConstraintHeight, obj.Width, obj.Height);
                    AdjustementSize content = new AdjustementSize(mo.ConstraintWidth, mo.ConstraintHeight, width, height);

                    // compute
                    SizeCompute.CheckConstraints(container, content);

                    // update information
                    obj.ConstraintWidth = container.ConstraintWidth;
                    obj.ConstraintHeight = container.ConstraintHeight;
                    obj.Width = container.Width;
                    obj.Height = container.Height;

                    mo.ConstraintWidth = content.ConstraintWidth;
                    mo.ConstraintHeight = content.ConstraintHeight;

                }
            }
            else
            {
                int nonClientWidth = obj.Tool.CSS.Padding.Left + obj.Tool.CSS.Padding.Right + obj.Tool.CSS.Border.Left + obj.Tool.CSS.Border.Right + obj.Tool.CSS.Margin.Left + obj.Tool.CSS.Margin.Right;
                int nonClientHeight = obj.Tool.CSS.Padding.Top + obj.Tool.CSS.Padding.Bottom + obj.Tool.CSS.Border.Top + obj.Tool.CSS.Border.Bottom + obj.Tool.CSS.Margin.Top + obj.Tool.CSS.Margin.Bottom;
                uint width = obj.Tool.Width;
                if (nonClientWidth > 0) width += Convert.ToUInt32(nonClientWidth); else if (width > Convert.ToUInt32(nonClientWidth)) width -= Convert.ToUInt32(nonClientWidth);
                uint height = obj.Tool.Height;
                if (nonClientHeight > 0) height += Convert.ToUInt32(nonClientHeight); else if (height > Convert.ToUInt32(nonClientHeight)) height -= Convert.ToUInt32(nonClientHeight);

                // set information
                AdjustementSize container = new AdjustementSize(obj.ConstraintWidth, obj.ConstraintHeight, obj.Width, obj.Height);
                AdjustementSize content = new AdjustementSize(obj.Tool.ConstraintWidth, obj.Tool.ConstraintHeight, width, height);

                // compute
                SizeCompute.CheckConstraints(container, content);

                // update information
                obj.ConstraintWidth = container.ConstraintWidth;
                obj.ConstraintHeight = container.ConstraintHeight;
                obj.Width = container.Width;
                obj.Height = container.Height;

                obj.Tool.ConstraintWidth = content.ConstraintWidth;
                obj.Tool.ConstraintHeight = content.ConstraintHeight;

            }

            return new AdjustementSize(obj.ConstraintWidth, obj.ConstraintHeight, obj.Width, obj.Height);
        }

        public static AdjustementSize ComputeVerticalZones(Project p, VerticalZone vz, List<HTMLObject> objects)
        {
            HTMLObject found = objects.Find(a => a.Container == vz.Name);
            if (found != null)
            {
                int nonClientWidth = found.CSS.Padding.Left + found.CSS.Padding.Right + found.CSS.Border.Left + found.CSS.Border.Right + found.CSS.Margin.Left + found.CSS.Margin.Right;
                int nonClientHeight = found.CSS.Padding.Top + found.CSS.Padding.Bottom + found.CSS.Border.Top + found.CSS.Border.Bottom + found.CSS.Margin.Top + found.CSS.Margin.Bottom;
                uint width = found.Width;
                if (nonClientWidth > 0) width += Convert.ToUInt32(nonClientWidth); else if (width > Convert.ToUInt32(nonClientWidth)) width -= Convert.ToUInt32(nonClientWidth);
                uint height = found.Height;
                if (nonClientHeight > 0) height += Convert.ToUInt32(nonClientHeight); else if (height > Convert.ToUInt32(nonClientHeight)) height -= Convert.ToUInt32(nonClientHeight);

                // set information
                AdjustementSize container = new AdjustementSize(vz.ConstraintWidth, vz.ConstraintHeight, vz.Width, vz.Height);
                AdjustementSize content = new AdjustementSize(found.ConstraintWidth, found.ConstraintHeight, width, height);

                // compute
                SizeCompute.CheckConstraints(container, content);

                // update information
                vz.ConstraintWidth = container.ConstraintWidth;
                vz.ConstraintHeight = container.ConstraintHeight;
                vz.Width = container.Width;
                vz.Height = container.Height;

                found.ConstraintWidth = content.ConstraintWidth;
                found.ConstraintHeight = content.ConstraintHeight;

            }

            return new AdjustementSize(vz.ConstraintWidth, vz.ConstraintHeight, vz.Width, vz.Height);
        }

        public static AdjustementSize ComputeHorizontalZones(Project p, HorizontalZone hz, List<HTMLObject> objects)
        {
            SizeCompute.CheckConstraints(hz);
            foreach (VerticalZone vz in hz.VerticalZones)
            {
                AdjustementSize ads = SizeCompute.ComputeVerticalZones(p, vz, objects);
            }
            return new AdjustementSize(hz.ConstraintWidth, hz.ConstraintHeight, hz.Width, hz.Height);
        }

        public static AdjustementSize ComputeMasterObject(Project p, MasterObject mo)
        {
            uint maxWidth = 0;
            uint height = 0;
            EnumConstraint cWidth = EnumConstraint.FIXED, cHeight = EnumConstraint.FIXED;
            foreach (HorizontalZone hz in mo.HorizontalZones)
            {
                AdjustementSize ads = SizeCompute.ComputeHorizontalZones(p, hz, mo.Objects);
                if (ads.ConstraintWidth == EnumConstraint.FIXED)
                {
                    if (maxWidth < ads.Width)
                        maxWidth = ads.Width;
                } else {
                    cWidth = EnumConstraint.AUTO;
                }
                if (ads.ConstraintHeight == EnumConstraint.FIXED)
                {
                    height += ads.Height;
                } else {
                    cHeight = EnumConstraint.AUTO;
                }
            }

            // set information
            AdjustementSize container = new AdjustementSize(mo.ConstraintWidth, mo.ConstraintHeight, mo.Width, mo.Height);
            AdjustementSize content = new AdjustementSize(cWidth, cHeight, maxWidth, height);

            // compute
            SizeCompute.CheckConstraints(container, content);

            // update information
            mo.ConstraintWidth = container.ConstraintWidth;
            mo.ConstraintHeight = container.ConstraintHeight;
            mo.Width = container.Width;
            mo.Height = container.Height;

            return new AdjustementSize(mo.ConstraintWidth, mo.ConstraintHeight, mo.Width, mo.Height);
        }

        public static AdjustementSize ComputeMasterPage(Project p, MasterPage mp)
        {
            uint maxWidth = 0;
            uint height = 0;
            EnumConstraint cWidth = EnumConstraint.FIXED, cHeight = EnumConstraint.FIXED;
            foreach (HorizontalZone hz in mp.HorizontalZones)
            {
                AdjustementSize ads = SizeCompute.ComputeHorizontalZones(p, hz, mp.Objects);
                if (ads.ConstraintWidth == EnumConstraint.FIXED)
                {
                    if (maxWidth < ads.Width)
                        maxWidth = ads.Width;
                }
                else
                {
                    cWidth = EnumConstraint.AUTO;
                }
                if (ads.ConstraintHeight == EnumConstraint.FIXED)
                {
                    height += ads.Height;
                }
                else
                {
                    cHeight = EnumConstraint.AUTO;
                }
            }

            // set information
            AdjustementSize container = new AdjustementSize(mp.ConstraintWidth, mp.ConstraintHeight, mp.Width, mp.Height);
            AdjustementSize content = new AdjustementSize(cWidth, cHeight, maxWidth, height);

            // compute
            SizeCompute.CheckConstraints(container, content);

            // update information
            mp.ConstraintWidth = container.ConstraintWidth;
            mp.ConstraintHeight = container.ConstraintHeight;
            mp.Width = container.Width;
            mp.Height = container.Height;

            return new AdjustementSize(mp.ConstraintWidth, mp.ConstraintHeight, mp.Width, mp.Height);
        }

        public static AdjustementSize ComputePage(Project p, Page page)
        {
            MasterPage mp = p.MasterPages.Find(a => a.Name == page.MasterPageName);
            if (mp != null)
            {
                int nonClientWidth = mp.CSS.Padding.Left + mp.CSS.Padding.Right + mp.CSS.Border.Left + mp.CSS.Border.Right + mp.CSS.Margin.Left + mp.CSS.Margin.Right;
                int nonClientHeight = mp.CSS.Padding.Top + mp.CSS.Padding.Bottom + mp.CSS.Border.Top + mp.CSS.Border.Bottom + mp.CSS.Margin.Top + mp.CSS.Margin.Bottom;
                uint width = mp.Width;
                if (nonClientWidth > 0) width += Convert.ToUInt32(nonClientWidth); else if (width > Convert.ToUInt32(nonClientWidth)) width -= Convert.ToUInt32(nonClientWidth);
                uint height = mp.Height;
                if (nonClientHeight > 0) height += Convert.ToUInt32(nonClientHeight); else if (height > Convert.ToUInt32(nonClientHeight)) height -= Convert.ToUInt32(nonClientHeight);

                // set information
                AdjustementSize container = new AdjustementSize(page.ConstraintWidth, page.ConstraintHeight, page.Width, page.Height);
                AdjustementSize content = new AdjustementSize(mp.ConstraintWidth, mp.ConstraintHeight, width, height);

                // compute
                SizeCompute.CheckConstraints(container, content);

                // update information
                page.ConstraintWidth = container.ConstraintWidth;
                page.ConstraintHeight = container.ConstraintHeight;
                page.Width = container.Width;
                page.Height = container.Height;

                return new AdjustementSize(page.ConstraintWidth, page.ConstraintHeight, page.Width, page.Height);
            }
            else
            {
                return new AdjustementSize(page.ConstraintWidth, page.ConstraintHeight, page.Width, page.Height);
            }
        }
    }
}
