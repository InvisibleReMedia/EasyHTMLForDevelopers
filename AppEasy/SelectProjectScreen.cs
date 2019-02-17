using System;
using System.Collections.Generic;
using System.IO;
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
        public static void SelectProject(WebBrowser browser)
        {
            Marshalling.MarshallingHash uiPage = Marshalling.MarshallingHash.CreateMarshalling("splash.ui", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", 1320 },
                    { "Height", 700 },
                    { "BackColor", "Blue" },
                    { "ForeColor", "White"}
                };
            });

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "title", "Nom du Projet" },
                { "dateCreation", "Date de création" },
                { "dateModification", "Date de modification"},
                { "revision", "Révision" },
                { "pageCount", "Nombre de pages" },
                { "masterPageCount", "Nombre de master page"},
                { "masterObjectCount", "Nombre de master objet"},
                { "toolCount", "Nombre d'outils" }
            };

            Marshalling.MarshallingHash[] dataHeaders = new Marshalling.MarshallingHash[headers.Count];
            UXFramework.UXReadOnlyText[] textHeaders = new UXFramework.UXReadOnlyText[headers.Count];
            int index = 0;
            foreach (KeyValuePair<string, string> kv in headers)
            {
                dataHeaders[index] = Marshalling.MarshallingHash.CreateMarshalling("header." + index.ToString(), () =>
                {
                    return new Dictionary<string, dynamic>() {
                        { "Id", kv.Key },
                        { "Text", kv.Value }
                    };
                });
                textHeaders[index] = UXFramework.UXReadOnlyText.CreateUXReadOnlyText(dataHeaders[index], new Marshalling.MarshallingHash("ui"));
                ++index;
            }


        }
    }
}
