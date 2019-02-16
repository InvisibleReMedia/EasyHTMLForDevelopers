using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UXFramework;
using UXFramework.BeamConnections;

namespace AppEasy
{
    static class SplashScreen
    {
        private static WebBrowser browser;
        private static Timer t;

        public static void Splash(WebBrowser web)
        {
            UXReadOnlyText u = Creation.NewUXReadOnlyText("text1", "Easy WEB For Developers", new Dictionary<string, dynamic>()
            {
                { "Width", 100 },
                { "Height", 20 },
                { "BackColor", "White" },
                { "ForeColor", "Black" },
                { "Border", "1px solid blue" },
                { "Margin", "0,0,0,0"  },
                { "Padding", "0,0,0,0" }
            });

            UXWindow win = Creation.NewUXWindow("splash", "Easy WEB for Developers", new Dictionary<string, dynamic>()
            {
                { "Width", 1320 },
                { "Height", 700 },
                { "BackColor", "White" },
                { "ForeColor", "Black" },
                { "Border", "" },
                { "Margin", "5,5,5,5"  },
                { "Padding", "2,2,2,2" }
            }, u);
            browser = web;
            win.Navigate(web);

            t = new Timer();
            t.Interval = 3000;
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
