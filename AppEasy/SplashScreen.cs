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
            Marshalling.MarshallingHash ui = Marshalling.MarshallingHash.CreateMarshalling("splash.ui", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", 1320 },
                    { "Height", 700 },
                    { "BackColor", "Blue" },
                    { "ForeColor", "White"}
                };
            });

            Marshalling.MarshallingHash data = Marshalling.MarshallingHash.CreateMarshalling("splash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Id", "splash" },
                    { "Text", "Easy WEB For Developers" }
                };
            });

            UXReadOnlyText ro = UXReadOnlyText.CreateUXReadOnlyText(data, new Marshalling.MarshallingHash("ui"));

            Marshalling.MarshallingHash top = Marshalling.MarshallingHash.CreateMarshalling("splash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    {
                        "children", Marshalling.MarshallingList.CreateMarshalling("children", () => {
                            return new List<IUXObject>() { ro };
                        })
                    }
                };
            });

            UXWindow win = UXWindow.CreateUXWindow("splash", top, ui);
            browser = web;
            win.Navigate(web);

            t = new Timer();
            t.Interval = 3000;
            t.Tick += T_Tick;
            t.Start();
        }

        private static void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
            SelectProjectScreen.SelectProject(browser);
        }
    }
}
