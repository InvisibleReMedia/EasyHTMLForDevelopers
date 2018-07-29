using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppEasy
{
    static class SplashScreen
    {
        private static WebBrowser browser;
        private static Timer t;

        public static void Splash(WebBrowser web)
        {
            UXFramework.UXWindow win = new UXFramework.UXWindow();
            browser = web;
            win.Name = "splash";
            UXFramework.UXReadOnlyText text = new UXFramework.UXReadOnlyText("Easy HTML For Developers");
            win.Children.Add(text);
            win.Navigate(web);

            t = new Timer();
            t.Interval = 10000;
            t.Tick += T_Tick;
            t.Start();
        }

        private static void T_Tick(object sender, EventArgs e)
        {
            SelectProjectScreen.SelectProject(browser);
            t.Stop();
        }
    }
}
