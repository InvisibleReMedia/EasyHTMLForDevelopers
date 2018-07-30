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
            win.Disposition = Library.Disposition.CENTER_TOP;
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
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("#2BAED0"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, null);
                }
                else if (e.lineNumber == 1)
                {
                    e.isValid = true;
                    e.left = 0;
                    e.top = 1;
                    e.columnSize = 1;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("#99D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, null);
                }
                else if (e.lineNumber == 2)
                {
                    e.isValid = true;
                    e.height = 50;
                    e.left = 0;
                    e.top = 2;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#EDF9FC"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, null);
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
                    UXFramework.UXTable reg = new UXFramework.UXTable();
                    reg.Name = "buttonRegion";
                    reg.SetHorizontal(1, (obj, m) =>
                    {
                        m.isValid = true;
                        m.left = 0;
                        m.top = 0;
                        m.columnSize = 4;
                        m.lineSize = 1;
                        m.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("transparent"), new Library.CSSColor("white"), new Library.CSSColor("black"), 2, 3, null);
                    });
                    reg.SetVertical(4, 1, (obj, m) =>
                    {
                        if (m.columnNumber == 0)
                        {
                            m.isValid = true;
                            m.left = 0;
                            m.top = 0;
                            m.columnSize = 1;
                            m.lineSize = 1;
                            UXFramework.UXButton bNew = new UXFramework.UXButton("buttonNew", "New");
                            bNew.RollBackColor = "purple";
                            bNew.RollColor = "white";
                            bNew.ClickBorderColor = "black";
                            m.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("transparent"), new Library.CSSColor("white"), new Library.CSSColor("black"), 0, 0, bNew);
                        }
                        else if (m.columnNumber == 1)
                        {
                            m.isValid = true;
                            m.left = 1;
                            m.top = 0;
                            m.columnSize = 1;
                            m.lineSize = 1;
                            UXFramework.UXButton bOpen = new UXFramework.UXButton("buttonOpen", "Open");
                            bOpen.RollBackColor = "purple";
                            bOpen.RollColor = "white";
                            bOpen.ClickBorderColor = "black";
                            m.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("transparent"), new Library.CSSColor("white"), new Library.CSSColor("black"), 0, 0, bOpen);
                        }
                        else if (m.columnNumber == 2)
                        {
                            m.isValid = true;
                            m.left = 2;
                            m.top = 0;
                            m.columnSize = 1;
                            m.lineSize = 1;
                            UXFramework.UXButton bDel = new UXFramework.UXButton("buttonDel", "Delete");
                            bDel.RollBackColor = "purple";
                            bDel.RollColor = "white";
                            bDel.ClickBorderColor = "black";
                            m.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("transparent"), new Library.CSSColor("white"), new Library.CSSColor("black"), 0, 0, bDel);
                        }
                        else if (m.columnNumber == 3)
                        {
                            m.isValid = true;
                            m.left = 3;
                            m.top = 0;
                            m.columnSize = 1;
                            m.lineSize = 1;
                            UXFramework.UXButton bQuickView = new UXFramework.UXButton("buttonQView", "Quick view");
                            bQuickView.RollBackColor = "purple";
                            bQuickView.RollColor = "white";
                            bQuickView.ClickBorderColor = "black";
                            m.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("transparent"), new Library.CSSColor("white"), new Library.CSSColor("black"), 0, 0, bQuickView);
                        }
                    });
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#2BAED0"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, reg);
                }
                else if (e.lineNumber == 1)
                {
                    if (e.columnNumber == 0)
                    {
                        e.isValid = true;
                        e.left = 0;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXBox b = new UXFramework.UXBox();
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("#99D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, b);
                    }
                    else if (e.columnNumber == 1)
                    {
                        e.isValid = true;
                        e.width = 800;
                        e.left = 1;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXViewSelectableDataTable view = new UXFramework.UXViewSelectableDataTable("projectList");
                        view.Bind((x) =>
                        {
                            string folder = CommonDirectories.ConfigDirectories.GetDocumentsFolder();
                            DirectoryInfo di = new DirectoryInfo(folder);
                            int index = 0;
                            Marshalling.PersistentDataObject obj;
                            for (index = 1; index <= 10; ++index)
                            {
                                FileInfo newFile = new FileInfo(Path.Combine(di.FullName, "project" + index.ToString() + ".bin"));
                                if (!Library.Project.Load(newFile, out obj))
                                {
                                    Library.Project p = new Library.Project();
                                    p.CreationDate = DateTime.Now;
                                    p.Title = "Project " + index.ToString();
                                    p.Revision = 1;
                                    Library.Project.Save(p, newFile.DirectoryName, newFile.Name);
                                }
                            }
                            index = 0;
                            foreach (FileInfo fi in di.GetFiles("*.bin"))
                            {
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
                        e.Options(Library.Disposition.CENTER_TOP, Library.EnumConstraint.FIXED, Library.EnumConstraint.AUTO, new Library.CSSColor("#47D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, view);
                    }
                    else if (e.columnNumber == 2)
                    {
                        e.isValid = true;
                        e.left = 2;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXBox b = new UXFramework.UXBox();
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("#99D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, b);
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
                    UXFramework.UXReadOnlyText b = new UXFramework.UXReadOnlyText("Business Forward Technology copyright @ 2018 - Contact us : business.forward.technology@gmail.com");
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#2BAED0"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, 3, b);
                }
            });
            win.Add(t);
            win.Navigate(web);
        }

    }
}
