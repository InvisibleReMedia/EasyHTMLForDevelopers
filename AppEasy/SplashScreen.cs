using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UXFramework.BeamConnections;

namespace AppEasy
{
    static class SplashScreen
    {
        private static WebBrowser browser;
        private static Timer t;

        public static void Splash(WebBrowser web)
        {
            UXFramework.UXWindow win = new UXFramework.UXWindow();
            win.Beams.SetPropertyValue("Background", UXFramework.BeamConnections.Beam.Register("background-color", win, BrandIdentity.Current.Colors["fond_page"]));
            win.Beams.SetPropertyValue("Width", UXFramework.BeamConnections.Beam.Register("width", win, BrandIdentity.Current.Sizes["taille_page"].Width));
            win.Beams.SetPropertyValue("Height", UXFramework.BeamConnections.Beam.Register("height", win, BrandIdentity.Current.Sizes["taille_page"].Height));
            browser = web;
            win.Name = "splash";
            UXFramework.UXReadOnlyText text = new UXFramework.UXReadOnlyText("Easy HTML For Developers");
            win.Add(text);
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
