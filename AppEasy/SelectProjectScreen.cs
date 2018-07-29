using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppEasy
{
    /// <summary>
    /// Screen to create or open a project
    /// </summary>
    static class SelectProjectScreen
    {
        /// <summary>
        /// Function SetSize can have multiple changes
        /// so, form values it can move the view as different manners
        /// one of them is to set 0 to height
        /// </summary>
        /// <param name="web"></param>
        public static void SelectProject(WebBrowser web)
        {
            UXFramework.UXWindow win = new UXFramework.UXWindow();
            win.Name = "winOpen";
            UXFramework.UXTable t = new UXFramework.UXTable();
            t.Name = "table1";

            t.SetHorizontal(3, (o, e) =>
            {
                if (e.lineNumber == 0)
                {
                    e.isValid = true;
                    e.height = 100;
                    e.left = 0;
                    e.top = 0;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("#2BAED0"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, null);
                }
                else if (e.lineNumber == 1)
                {
                    e.isValid = true;
                    e.height = 100;
                    e.left = 0;
                    e.top = 1;
                    e.columnSize = 1;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("#99D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, null);
                }
                else if (e.lineNumber == 2)
                {
                    e.isValid = true;
                    e.height = 50;
                    e.left = 0;
                    e.top = 2;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#EDF9FC"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, null);
                }
            });

            t.SetVertical(3, 3, (o, e) =>
            {
                if (e.lineNumber == 0 && e.columnNumber == 0)
                {
                    e.isValid = true;
                    e.height = 50;
                    e.left = 0;
                    e.top = 0;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    UXFramework.UXClickableText b = new UXFramework.UXClickableText("bonjour!");
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#2BAED0"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, b);
                }
                else if (e.lineNumber == 1)
                {
                    if (e.columnNumber == 0)
                    {
                        e.isValid = true;
                        e.height = 300;
                        e.left = 0;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXBox b = new UXFramework.UXBox();
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#99D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, b);
                    }
                    else if (e.columnNumber == 1)
                    {
                        e.isValid = true;
                        e.width = 800;
                        e.left = 1;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXViewDataTable view = new UXFramework.UXViewDataTable("projectList");
                        view.Bind((x) =>
                        {

                            string folder = CommonDirectories.ConfigDirectories.GetDocumentsFolder();
                            DirectoryInfo di = new DirectoryInfo(folder);
                            int index = 0;
                            foreach (FileInfo fi in di.GetFiles("*.bin"))
                            {
                                Marshalling.PersistentDataObject obj;
                                if (Library.Project.Load(fi, out obj))
                                {

                                    Library.Project p = (Library.Project)obj;

                                    if (p != null)
                                    {

                                        Marshalling.IMarshalling m;
                                        m = new Marshalling.MarshallingHash(p.Title, new Dictionary<string, dynamic>() {
                                            { "Title", p.Title},
                                            { "CreationDate", p.CreationDate.ToShortDateString() },
                                            { "ModificationDate",  p.ModificationDate.ToShortDateString() },
                                            { "MasterPages count", p.MasterPages.Count },
                                            { "MasterObjects count", p.MasterObjects.Count },
                                            { "Pages count", p.Pages.Count },
                                            { "Tools count", p.Tools.Count },
                                            { "Revision", p.Revision }
                                        });
                                        x[index] = m;
                                        ++index;
                                    }

                                }
                            }

                        });
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.AUTO, new Library.CSSColor("#47D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, view);
                    }
                    else if (e.columnNumber == 2)
                    {
                        e.isValid = true;
                        e.height = 300;
                        e.left = 2;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXBox b = new UXFramework.UXBox();
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#99D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, b);
                    }
                }
                else if (e.lineNumber == 2 && e.columnNumber == 0)
                {
                    e.isValid = true;
                    e.height = 50;
                    e.left = 0;
                    e.top = 2;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    UXFramework.UXReadOnlyText b = new UXFramework.UXReadOnlyText("Business Forward Technology @copyright 2018 - Contact us :business.forward.technology@gmail.com");
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#2BAED0"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, b);
                }
            });
            win.Add(t);
            win.Navigate(web);
        }

    }
}
