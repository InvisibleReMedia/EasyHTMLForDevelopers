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
            UXFramework.UXTable t = new UXFramework.UXTable(1, 3, "table1");

            Marshalling.MarshallingList lines = new Marshalling.MarshallingList("lines");

            UXFramework.UXTable tbt = new UXFramework.UXTable(4, 1, "table-buttons");

            Marshalling.MarshallingList buttons = new Marshalling.MarshallingList("buttons");
            {

                UXFramework.UXButton bNew = new UXFramework.UXButton("buttonNew", "New");
                bNew.RollBackColor = "purple";
                bNew.RollColor = "white";
                bNew.ClickBorderColor = "black";
                bNew.SetUpdate(new Action(() =>
                {
                    ViewProject(web, new Library.Project());
                }));
                buttons[0] = bNew;

                UXFramework.UXButton bOpen = new UXFramework.UXButton("buttonOpen", "Open");
                bOpen.RollBackColor = "purple";
                bOpen.RollColor = "white";
                bOpen.ClickBorderColor = "black";
                bOpen.SetUpdate(new Action(() =>
                {
                    ViewProject(web, new Library.Project());
                }));
                buttons[1] = bOpen;

                UXFramework.UXButton bDel = new UXFramework.UXButton("buttonDel", "Delete");
                bDel.RollBackColor = "purple";
                bDel.RollColor = "white";
                bDel.ClickBorderColor = "black";
                buttons[2] = bDel;

                UXFramework.UXButton bQuickView = new UXFramework.UXButton("buttonQView", "Quick view");
                bQuickView.RollBackColor = "purple";
                bQuickView.RollColor = "white";
                bQuickView.ClickBorderColor = "black";
                buttons[3] = bQuickView;

            }

            Marshalling.MarshallingList btnLine = new Marshalling.MarshallingList("btn-line");
            btnLine[0] = buttons;

            //tbt.Bind(btnLine);

            lines[0] = tbt;

            UXFramework.UXViewSelectableDataTable view = new UXFramework.UXViewSelectableDataTable(8, 10, "projectList");

            Marshalling.MarshallingList x = new Marshalling.MarshallingList("projectList");
            {
                string folder = CommonDirectories.ConfigDirectories.GetDocumentsFolder();
                DirectoryInfo di = new DirectoryInfo(folder);
                int index = 0;
                Marshalling.PersistentDataObject obj;
                for (index = 1; index <= 30; ++index)
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
            }

            //view.Bind(x);

            lines[1] = view;

            UXFramework.UXReadOnlyText b = new UXFramework.UXReadOnlyText("Business Forward Technology copyright @ 2018 - Contact us : business.forward.technology@gmail.com");
            lines[2] = b;

            //t.Bind(lines);

            win.Add(t);
            win.Navigate(web);
        }

        private static Marshalling.MarshallingList DisplayTree(Library.Project p, Library.Node<string, Library.Accessor> t)
        {
            Marshalling.MarshallingList lines = new Marshalling.MarshallingList("lines");

            int index = 0;
            foreach (Library.Node<string, Library.Accessor> r in t.SubNodes)
            {
                Marshalling.MarshallingHash h = new Marshalling.MarshallingHash("hash", new List<Marshalling.IMarshalling>() { new Marshalling.MarshallingRegexValue(t.NodeValue + "-v", r.NodeValue, "^*$"), DisplayTree(p, r) });
                lines[index++] = h;
            }
            foreach (Library.Leaf<Library.Accessor> r in t.Elements)
            {
                Marshalling.MarshallingHash h = new Marshalling.MarshallingHash("hash", new List<Marshalling.IMarshalling>() { new Marshalling.MarshallingRegexValue(t.NodeValue + "-v" + index.ToString(), r.Object.ToString(), "^*$") });
                lines[index++] = h;
            }
            return lines;
        }

        public static void ViewProject(WebBrowser web, Library.Project p)
        {

            UXFramework.UXWindow win = new UXFramework.UXWindow();
            win.Name = "viewProject";
            win.Disposition = Library.Disposition.CENTER_TOP;

            Marshalling.MarshallingList x = DisplayTree(p, p.Hierarchy);

            UXFramework.UXTree view = new UXFramework.UXTree("idt");

            view.Bind(x);
            win.Add(view);
            win.Navigate(web);
        }

    }
}
