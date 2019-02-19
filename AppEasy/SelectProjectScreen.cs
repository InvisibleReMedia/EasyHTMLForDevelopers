using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace AppEasy
{
    /// <summary>
    /// Screen to create or open a project
    /// </summary>
    static class SelectProjectScreen
    {
        public static void SelectProject(WebBrowser browser)
        {
            Marshalling.MarshallingHash uiHeader = Marshalling.MarshallingHash.CreateMarshalling("uiItem", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "BackColor", "#0B5395" },
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
            UXFramework.UXCell[] cellHeaders = new UXFramework.UXCell[headers.Count];

            int index = 0;
            foreach (KeyValuePair<string, string> kv in headers)
            {
                dataHeaders[index] = Marshalling.MarshallingHash.CreateMarshalling("content", () =>
                {
                    return new Dictionary<string, dynamic>() {
                        { "Id", kv.Key },
                        { "Text", kv.Value },
                    };
                });
                textHeaders[index] = UXFramework.UXReadOnlyText.CreateUXReadOnlyText(dataHeaders[index], new Marshalling.MarshallingHash("uiItem"));

                cellHeaders[index] = UXFramework.UXCell.CreateUXCell("header." + index.ToString(), () =>
                {
                    return new Dictionary<string, dynamic>() {
                        { "Border", "2px solid Blue" },
                        { "children", Marshalling.MarshallingList.CreateMarshalling("children", () => {
                                        return new List<UXFramework.IUXObject>() { textHeaders[index] };
                          }) },
                    };
                });

                ++index;
            }

            UXFramework.UXRow rowHeader = UXFramework.UXRow.CreateUXRow("rowHeader", () =>
            {
                return new Dictionary<string, dynamic>() 
                {   
                    { "Index", "0" },
                    { "BackColor", "#0B5395" },
                    { "ForeColor", "White"},
                    { "ColumnCount", headers.Count },
                    { "children",
                        Marshalling.MarshallingList.CreateMarshalling("children", () => {
                        
                            List<UXFramework.IUXObject> objList = new List<UXFramework.IUXObject>();
                            for(index = 0; index < headers.Count; ++index) {
                                objList.Add(cellHeaders[index]);
                            }

                            return objList;
                        })
                    }
                };
            });


            DirectoryInfo di = new DirectoryInfo(CommonDirectories.ConfigDirectories.GetDocumentsFolder());
            FileInfo[] files = di.GetFiles("*.bin");
            Dictionary<string, dynamic>[] items = new Dictionary<string, dynamic>[files.Count()];
            UXFramework.UXRow[] rowItems = new UXFramework.UXRow[files.Count()];

            index = 0;
            foreach (FileInfo fi in files)
            {
                Marshalling.PersistentDataObject p;
                if (Project.Load(fi, out p))
                {
                    Project proj = p as Project;
                    items[index] = new Dictionary<string, dynamic>();
                    items[index].Add("title", proj.Title);
                    items[index].Add("dateCreation", proj.CreationDate.ToShortDateString());
                    items[index].Add("dateModification", proj.ModificationDate.ToShortDateString());
                    items[index].Add("revision", proj.Revision.ToString());
                    items[index].Add("pageCount", proj.Pages.Count.ToString());
                    items[index].Add("masterPageCount", proj.MasterPages.Count.ToString());
                    items[index].Add("masterObjectCount", proj.MasterObjects.Count.ToString());
                    items[index].Add("toolCount", proj.Tools.Count.ToString());

                    rowItems[index] = UXFramework.UXRow.CreateUXRow("rowItem." + index.ToString(), () =>
                    {
                        return new Dictionary<string, dynamic>()
                        {
                            { "Id", "rowItem." + index.ToString() },
                            { "Index", index },
                            { "BackColor", "#C8E3FB" },
                            { "ForeColor", "Black"},
                            { "IsSelectable", true },
                            { "Background-Selectable", "#D9F4FC" },
                            { "Background-Clickable", "#96B1C8" },
                            { "IsClickable", true },
                            { "ColumnCount", headers.Count },
                            { "children", 
                                Marshalling.MarshallingList.CreateMarshalling("children", () => {
                                    
                                    List<UXFramework.IUXObject> objList = new List<UXFramework.IUXObject>();

                                    foreach(string key in items[index].Keys) {

                                        Marshalling.MarshallingHash dataItem = Marshalling.MarshallingHash.CreateMarshalling("Content", () =>
                                        {
                                            return new Dictionary<string, dynamic>() {
                                                { "Id", key },
                                                { "Text", items[index][key] }
                                            };
                                        });
                                        UXFramework.UXReadOnlyText textItem = UXFramework.UXReadOnlyText.CreateUXReadOnlyText(dataItem, new Marshalling.MarshallingHash("uiCell" + index.ToString()));

                                        UXFramework.UXCell cellItem = UXFramework.UXCell.CreateUXCell("cell." + index.ToString(), () => {
                                            return new Dictionary<string, dynamic>() {
                                                { "Border", "2px solid Blue" },
                                                { "children", Marshalling.MarshallingList.CreateMarshalling("children", () => {
                                                    return new List<UXFramework.IUXObject>() { textItem };
                                                }) },
                                            };
                                        });

                                        objList.Add(cellItem);
                                    }
                                    return objList;
                                })
                            }
                        };
                    });

                    rowItems[index].SetUpdate((o) =>
                    {
                        UXFramework.UXRow r = o as UXFramework.UXRow;
                        string selectedId = r.Id;
                        UXProject.OpenProject(browser);
                    });


                    ++index;
                }
            }

            UXFramework.UXWindow win = UXFramework.UXWindow.CreateUXWindow("selectProject", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", 1320 },
                    { "Height", 700 },
                    { "BackColor", "Blue" },
                    { "ForeColor", "White"},
                    { "children", Marshalling.MarshallingList.CreateMarshalling("children", () => {

                            UXFramework.UXTable table = UXFramework.UXTable.CreateUXTable("table", () => {
                                return new Dictionary<string, dynamic>() {
                                    { "ColumnCount", headers.Count },
                                    { "LineCount", files.Count() + 1 },
                                    {
                                        "children", 
                                        Marshalling.MarshallingList.CreateMarshalling("children", () => {

                                            List<UXFramework.IUXObject> objList = new List<UXFramework.IUXObject>();
                        
                                            objList.Add(rowHeader);
                                            for(index = 0; index < files.Count(); ++index) {
                                                objList.Add(rowItems[index]);
                                            }

                                            return objList;

                                        })
                                    }
                                };
                            });
                            
                            return new List<UXFramework.IUXObject>() { table };

                        })

                    }
                        
                };

            });

            win.Navigate(browser);

        }
    }
}

