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

            Marshalling.IMarshalling ui = Marshalling.MarshallingHash.CreateMarshalling("0", () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Width", 1320),
                    new KeyValuePair<string, dynamic>("Height", 700),
                    new KeyValuePair<string, dynamic>("BackColor", "#FF0000"),
                    new KeyValuePair<string, dynamic>("ForeColor", "White")
                }.AsEnumerable();
            });
            Marshalling.IMarshalling uiChilds = Marshalling.MarshallingList.CreateMarshalling("uiChilds", () =>
            {
                return new List<Marshalling.IMarshalling>() { ui };
            });


            Marshalling.IMarshalling hash = Marshalling.MarshallingHash.CreateMarshalling("0", () =>
            {
                return new List<KeyValuePair<string, dynamic>>()
                {
                    new KeyValuePair<string, dynamic>("type", "UXReadOnlyText"),
                    new KeyValuePair<string, dynamic>("name", "splash"),
                    new KeyValuePair<string, dynamic>("Text", "Easy HTML For Developers")
                }.AsEnumerable();
            });
            Marshalling.IMarshalling childs = Marshalling.MarshallingList.CreateMarshalling("childs", () =>
            {
                return new List<Marshalling.IMarshalling>() { hash };
            });
            UXFramework.UXWindow win = UXFramework.UXWindow.CreateUXWindow("splash", childs as Marshalling.MarshallingList, uiChilds as Marshalling.MarshallingList);
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
