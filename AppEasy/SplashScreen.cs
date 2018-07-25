using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppEasy
{
    static class SplashScreen
    {
        public static void Splash(WebBrowser web)
        {
            UXFramework.UXWindow win = new UXFramework.UXWindow();
            win.Name = "splash";
            UXFramework.UXReadOnlyText text = new UXFramework.UXReadOnlyText("Ceci est un texte.");
            win.Children.Add(text);
            win.Navigate(web);
        }
    }
}
