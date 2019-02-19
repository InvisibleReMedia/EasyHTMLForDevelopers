using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEasy
{
    public static class UXProject
    {

        public static UXFramework.UXTree CreateTreeView(uint index, Library.Project p, Library.Node<string, Library.Accessor> projectTree)
        {
            uint nodesIndex = 0;

            UXFramework.UXRow[] subtree = new UXFramework.UXRow[projectTree.SubNodes.Count() + projectTree.Elements.Count()];

            foreach (dynamic node in projectTree.Elements)
            {
                subtree[nodesIndex] = UXFramework.UXRow.CreateUXRow("index." + index.ToString() + "." + nodesIndex.ToString(), () =>
                {
                    return new Dictionary<string, dynamic>() {
                        { "ColumnCount", 3 },
                        { "children", UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            return new List<UXFramework.IUXObject>() {
                                UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + "." + nodesIndex.ToString(), () => {
                                    return new Dictionary<string, dynamic>() { 
                                        {
                                            "children",
                                            UXFramework.ChildCollection.CreateChildCollection("children", () => {
                                                return new List<UXFramework.IUXObject>() {
                                                    UXFramework.UXBox.CreateUXBox("box", () => {
                                                        return new Dictionary<string, dynamic>();
                                                    })
                                                };
                                            })
                                        }
                                    };

                                }),
                                UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + "." + nodesIndex.ToString(), () => {
                                    return new Dictionary<string, dynamic>() {
                                        { 
                                            "children",                           
                                            UXFramework.ChildCollection.CreateChildCollection("children", () => {
                                                return new List<UXFramework.IUXObject>() {
                                                    CreateTreeView(index + 1, p, node)
                                                };
                                            })
                                        }
                                    };
                                }),
                                UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + "." + nodesIndex.ToString(), () => {
                                    return new Dictionary<string, dynamic>() { 
                                        {
                                            "children",                           
                                            UXFramework.ChildCollection.CreateChildCollection("children", () => {
                                                return new List<UXFramework.IUXObject>() {
                                                    UXFramework.UXBox.CreateUXBox("box", () => {
                                                        return new Dictionary<string, dynamic>();
                                                    })
                                                };
                                            })
                                        }
                                    };
                                })

                            };
                        })
                    }
                };
                });

                ++nodesIndex;
            }

            foreach (Library.Node<string, Library.Accessor> sub in projectTree.SubNodes)
            {
                subtree[nodesIndex] = UXFramework.UXRow.CreateUXRow("index." + index.ToString() + "." + nodesIndex.ToString(), () =>
                {
                    return new Dictionary<string, dynamic>() {
                        { "ColumnCount", 3 },
                        { "children", UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            return new List<UXFramework.IUXObject>() {
                                UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + "." + nodesIndex.ToString(), () => {
                                    return new Dictionary<string, dynamic>() { 
                                        {
                                            "children",
                                            UXFramework.ChildCollection.CreateChildCollection("children", () => {
                                                return new List<UXFramework.IUXObject>() {
                                                    UXFramework.UXBox.CreateUXBox("box", () => {
                                                        return new Dictionary<string, dynamic>();
                                                    })
                                                };
                                            })
                                        }
                                    };

                                }),
                                UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + "." + nodesIndex.ToString(), () => {
                                    return new Dictionary<string, dynamic>() {
                                        { 
                                            "children",                           
                                            UXFramework.ChildCollection.CreateChildCollection("children", () => {
                                                return new List<UXFramework.IUXObject>() {
                                                    CreateTreeView(index + 1, p, sub)
                                                };
                                            })
                                        }
                                    };
                                }),
                                UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + "." + nodesIndex.ToString(), () => {
                                    return new Dictionary<string, dynamic>() { 
                                        {
                                            "children",                           
                                            UXFramework.ChildCollection.CreateChildCollection("children", () => {
                                                return new List<UXFramework.IUXObject>() {
                                                    UXFramework.UXBox.CreateUXBox("box", () => {
                                                        return new Dictionary<string, dynamic>();
                                                    })
                                                };
                                            })
                                        }
                                    };
                                })

                            };
                        })
                        }
                    };
                });

                ++nodesIndex;
            }

            UXFramework.UXRow firstRow;

            UXFramework.UXCell cell0 = UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + ".0", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 50 },
                    { "Height", 20 },
                    { "Border", "1px solid black" },
                    { "children", 
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            return new List<UXFramework.IUXObject>() {
                                UXFramework.UXImage.CreateUXImage("image." + index.ToString() + ".0", () =>
                                {
                                    return new Dictionary<string, dynamic>() {
                                        { "ImageWidth", 16 },
                                        { "ImageHeight", 16 },
                                        { "ImageFile", "plus.png" }
                                    };
                                })
                            };
                        })
                    }
                };
            });

            UXFramework.UXCell cell1 = UXFramework.UXCell.CreateUXCell("cell" + index.ToString() + ".1", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 150 },
                    { "overflow", "hidden" },
                    { 
                        "children",
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            return new List<UXFramework.IUXObject>()
                            {
                                UXFramework.UXReadOnlyText.CreateUXReadOnlyText("text" + index.ToString(), () =>
                                {
                                    return new Dictionary<string, dynamic>()
                                    {
                                        { "Id", "text" + index.ToString() },
                                        { "Text", projectTree.NodeValue }
                                    };
                                })
                            };
                        })
                    }
                };
            });

            UXFramework.UXCell cell2 = UXFramework.UXCell.CreateUXCell("cell." + index.ToString() + ".2", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 20 },
                    { "Height", 20 },
                    { "Border", "1px solid black" },
                    { "children",
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            return new List<UXFramework.IUXObject>()
                            {
                                UXFramework.UXImage.CreateUXImage("image." + index.ToString() + ".1", () =>
                                {
                                    return new Dictionary<string, dynamic>() {
                                        { "ImageWidth", 16 },
                                        { "ImageHeight", 16 },
                                        { "ImageFile", "go.png" }
                                    };
                                })
                            };
                        })
                    }
                };
            });

            firstRow = UXFramework.UXRow.CreateUXRow("row." + index.ToString() + ".0", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "ColumnCount", 3 },
                    { 
                        "children",
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            return new List<UXFramework.IUXObject>()
                            {
                                cell0, cell1, cell2
                            };
                        })
                    }
                };
            });


            UXFramework.UXTree tree = UXFramework.UXTree.CreateUXTree("tree", () => {
                return new Dictionary<string, dynamic>() {
                    { "ColumnCount", 3 },
                    { "LineCount", subtree.Count() + 1},
                    { 
                        "children", 
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            List<UXFramework.IUXObject> list = new List<UXFramework.IUXObject>();
                            list.Add(firstRow);
                            list = list.Concat(subtree).ToList();
                            return list;
                        })
                    }
                };
            });

            return tree;
        }

        public static void OpenProject(WebBrowser browser)
        {

            Library.Project p = new Library.Project();
            UXFramework.UXWindow win = UXFramework.UXWindow.CreateUXWindow("openProject", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", 1320 },
                    { "Height", 700 },
                    { "BackColor", "Blue" },
                    { "ForeColor", "White"},
                    { "children",
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {

                            List<UXFramework.IUXObject> children = new List<UXFramework.IUXObject>() {
                                CreateTreeView(0, p, p.Hierarchy)
                            };
                            return children;
                        })
                    }
                };

            });

            Console.WriteLine(win.ToString());

            win.Navigate(browser);

        }

    }
}
