using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppEasy
{
    static class SelectProjectScreen
    {
        public static void SelectProject(WebBrowser web)
        {
            UXFramework.UXWindow win = new UXFramework.UXWindow();
            win.Name = "winOpen";
            UXFramework.UXTable t = new UXFramework.UXTable();
            t.Name = "table1";
            t.SetSize(3, 3, (o, e) =>
            {
                if (e.lineNumber == 0 && e.columnNumber == 0)
                {
                    e.isValid = true;
                    e.width = 600;
                    e.height = 200;
                    e.left = 0;
                    e.top = 0;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor("blue"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3);
                }
                else if (e.lineNumber == 1)
                {
                    if (e.columnNumber == 0)
                    {
                        e.isValid = true;
                        e.width = 200;
                        e.height = 600;
                        e.left = 0;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor("blue"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3);
                    }
                    else if (e.columnNumber == 1)
                    {
                        e.isValid = true;
                        e.width = 200;
                        e.height = 600;
                        e.left = 1;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor("blue"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3);
                    }
                    else if (e.columnNumber == 2)
                    {
                        e.isValid = true;
                        e.width = 200;
                        e.height = 600;
                        e.left = 2;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor("blue"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3);
                    }
                }
                else if (e.lineNumber == 2 && e.columnNumber == 0)
                {
                    e.isValid = true;
                    e.width = 600;
                    e.height = 200;
                    e.left = 0;
                    e.top = 2;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor("blue"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3);
                }
            });
            win.Add(t);
            win.Navigate(web);
        }

    }
}
