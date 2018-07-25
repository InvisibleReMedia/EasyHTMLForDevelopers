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
            mp.Name = "mp1";
            mp.Width = 1000;
            mp.Height = 500;
            mp.CountColumns = 15;
            mp.CountLines = 20;
            List<SizedRectangle> rects = new List<SizedRectangle>();
            SizedRectangle sz = new SizedRectangle(5, 20, 5, 10, 5, 20);
            rects.Add(sz);
            sz = new SizedRectangle(5, 5, 5, 10, 5, 10);
            rects.Add(sz);
            sz = new SizedRectangle(5, 5, 5, 10, 10, 15);
            rects.Add(sz);
            sz = new SizedRectangle(5, 5, 5, 10, 15, 20);
            rects.Add(sz);
            sz = new SizedRectangle(5, 20, 10, 15, 5, 20);
            rects.Add(sz);

            mp.MakeZones(rects);

            p1.MasterPageName = "mp1";
            p.MasterPages.Add(mp);
            p.Pages.Add(p1);

            OutputHTML o = p1.GenerateDesign();
            UXWindow w = new UXWindow();
            UXReadOnlyText u = new UXReadOnlyText(o.HTML.ToString());
            w.Add(u);

            return w;
        }
    }
}
