using System;
using System.Collections.Generic;
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
                    e.width = 600;
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
                    e.width = 200;
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
                    e.width = 600;
                    e.height = 100;
                    e.left = 0;
                    e.top = 2;
                    e.columnSize = 3;
                    e.lineSize = 1;
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.AUTO, new Library.CSSColor("#EDF9FC"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, null);
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
                        e.height = 500;
                        e.left = 1;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXButton but = new UXFramework.UXButton();
                        but.ButtonText = "Click";
                        but.Id = "b1";
                        but.RollBackColor = "#35F4FD";
                        but.RollColor = "white";
                        but.ClickBorderColor = "black";
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#D5F0F7"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, but);
                    }
                    else if (e.columnNumber == 2)
                    {
                        e.isValid = true;
                        e.height = 300;
                        e.left = 2;
                        e.top = 1;
                        e.columnSize = 1;
                        e.lineSize = 1;
                        UXFramework.UXImage image = new UXFramework.UXImage("im1", "capture8.png");
                        e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#99D9EA"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, image);
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
                    UXFramework.UXBox b = new UXFramework.UXBox();
                    e.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#EDF9FC"), new Library.CSSColor("white"), new Library.CSSColor("black"), 3, b);
                }
            });
            win.Add(t);
            win.Navigate(web);
        }

    }
}
