using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UXFramework;
using Library;


namespace AppEasy
{
    public class MLData : IMLData
    {

        /// <summary>
        /// Delegate to export
        /// </summary>
        /// <returns>ux object</returns>
        public event EventHandler ExportEvent;
        /// <summary>
        /// Export ux from data
        /// </summary>
        /// <returns>ux instance</returns>
        public IUXObject Export()
        {
            Project p = new Project();
            p.CreationDate = DateTime.Now;
            p.Title = "Ouverture fichier";
            p.Revision = 1;
            Project.CurrentProject = p;
            Page p1 = new Page();
            MasterPage mp = new MasterPage();
            mp.ConstraintWidth = EnumConstraint.FORCED;
            mp.ConstraintHeight = EnumConstraint.FORCED;

            mp.Name = "mp1";
            mp.CountColumns = 3;
            mp.CountLines = 3;
            List<SizedRectangle> rects = new List<SizedRectangle>();
            SizedRectangle sz = new SizedRectangle(250, 100, 1, 3, 0, 0);
            rects.Add(sz);
            sz = new SizedRectangle(500, 100, 1, 1, 1, 0);
            rects.Add(sz);
            sz = new SizedRectangle(500, 500, 1, 1, 1, 1);
            rects.Add(sz);
            sz = new SizedRectangle(500, 100, 1, 1, 1, 2);
            rects.Add(sz);
            sz = new SizedRectangle(250, 100, 1, 3, 2, 0);
            rects.Add(sz);

            mp.MakeZones(rects);
            foreach(HorizontalZone h in mp.HorizontalZones)
            {
                foreach(VerticalZone v in h.VerticalZones)
                {
                    v.CSS.BackgroundColor = CSSColor.ParseColor("Red");
                    v.CSS.Border = new Rectangle("2,2,2,2");
                    v.CSS.BorderBottomColor = v.CSS.BorderLeftColor = v.CSS.BorderRightColor = v.CSS.BorderTopColor = CSSColor.ParseColor("white");
                }
            }

            p1.MasterPageName = "mp1";
            p.MasterPages.Add(mp);
            p.Pages.Add(p1);

            OutputHTML o = p1.GenerateProduction();
            UXWindow w = new UXWindow();
            UXReadOnlyText u = new UXReadOnlyText(o.HTML.ToString());
            w.Add(u);

            return w;
        }
    }
}
