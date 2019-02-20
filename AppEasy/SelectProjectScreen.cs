using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace AppEasy
{
    /// <summary>
    /// Screen to create or open a project
    /// </summary>
    static class SelectProjectScreen
    {
        public static void SelectProject(WebBrowser browser)
        {

            DirectoryInfo di = new DirectoryInfo(CommonDirectories.ConfigDirectories.GetDocumentsFolder());
            UXFramework.UXTable table = LibraryConverter.ListConverter.MakeUXProjectFiles(di);
            foreach (UXFramework.UXRow row in table.Children)
            {
                row.SetUpdate(x =>
                {
                    UXFramework.UXRow r = x as UXFramework.UXRow;
                    Project p = r.Get("project") as Library.Project;
                    LibraryConverter.ListConverter.MakeUXhierarchyProject(p, p.Hierarchy);
                });
            }
            UXFramework.UXWindow win = UXFramework.Creation.CreateWindow(null, 1320, 700, table);
            win.Add(() =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "BackColor", "Blue" },
                    { "ForeColor", "White" }
                };
            });

            win.Navigate(browser);

        }
    }
}

