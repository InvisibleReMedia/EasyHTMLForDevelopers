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
            UXTable table = ProjectViewer.CreateProjectTableHeaders();

            CommonProperties cp = new CommonProperties();
            cp.Width = 1320;
            cp.Height = 700;
            cp.BackColor = "White";
            cp.ForeColor = "Black";
            UXWindow win = Creation.NewUXWindow("splash", "Easy WEB for Developers", cp, table);
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
