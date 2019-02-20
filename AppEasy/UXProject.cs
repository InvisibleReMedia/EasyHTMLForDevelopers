using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEasy
{
    public class UXProject
    {

        public UXProject(WebBrowser web)
        {
            this.web = web;
            currentProject = new Library.Project();
            currentNode = currentProject.Hierarchy;
            this.OpenProject();
        }

        private Library.Node<string, Library.Accessor> currentNode;
        private Library.Project currentProject;
        private WebBrowser web;

        public UXFramework.UXTree CreateTreeView(uint index, Library.Project p, Library.Node<string, Library.Accessor> projectTree)
        {
            Marshalling.MarshallingHash hash;
            List<UXFramework.UXRow> subtree = new List<UXFramework.UXRow>();

            foreach (dynamic node in projectTree.Elements)
            {
                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Width", 20 },
                        { "Height", 25 },
                        { "Constraint-Width", "FIXED" },
                        { "Constraint-Height", "FIXED" },
                        { "ImageWidth", 16 },
                        { "ImageHeight", 16 }
                    };
                });
                UXFramework.UXBox box1 = UXFramework.Creation.CreateBox(null, 16, 16);
                UXFramework.UXImage im2 = UXFramework.Creation.CreateImage(hash, "image", "left.png");
                UXFramework.UXReadOnlyText ro = UXFramework.Creation.CreateReadOnlyText(null, "text", node.NodeValue);

                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Width", 32 },
                        { "Height", 25 },
                        { "Constraint-Width", "FIXED" },
                        { "Constraint-Height", "FIXED" },
                        { "align", "left" },
                        { "Border", "2px solid Blue" }
                    };
                });
                UXFramework.UXCell cLeaf1 = UXFramework.Creation.CreateCell(hash, box1);
                UXFramework.UXCell cLeaf3 = UXFramework.Creation.CreateCell(hash, im2);
                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Height", 25 },
                        { "Constraint-Height", "FIXED" },
                        { "align", "left" },
                    };
                });
                UXFramework.UXCell cLeaf2 = UXFramework.Creation.CreateCell(hash, ro);
                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "IsSelectable", true },
                        { "Background-Selectable", "#0B5395" },
                        { "Height", 25 },
                        { "Constraint-Height", "FIXED" },
                    };
                });

                UXFramework.UXRow row = UXFramework.Creation.CreateRow(3, hash, cLeaf1, cLeaf2, cLeaf3);
                row.SetUpdate(x =>
                {
                    Library.Node<string, Library.Accessor> n = x.GetProperty("ProjectRef").Value;
                    n.IsSelected = true;
                    currentNode = n;
                    this.OpenProject();

                });
                subtree.Add(row);
            }

            if (projectTree.IsSelected)
            {
                foreach (Library.Node<string, Library.Accessor> sub in projectTree.SubNodes)
                {
                    hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                    {
                        return new Dictionary<string, dynamic>()
                    {
                        { "Width", 20 },
                        { "Height", 25 },
                        { "Constraint-Width", "FIXED" },
                        { "Constraint-Height", "FIXED" },
                        { "ImageWidth", 16 },
                        { "ImageHeight", 16 }
                    };
                    });
                    UXFramework.UXReadOnlyText ro1 = UXFramework.Creation.CreateReadOnlyText(hash, "text", "+");
                    UXFramework.UXImage im2 = UXFramework.Creation.CreateImage(hash, "image", "left.png");
                    UXFramework.UXReadOnlyText ro = UXFramework.Creation.CreateReadOnlyText(null, "text", sub.NodeValue);

                    hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                    {
                        return new Dictionary<string, dynamic>()
                    {
                        { "Width", 32 },
                        { "Height", 25 },
                        { "Constraint-Width", "FIXED" },
                        { "Constraint-Height", "FIXED" },
                        { "Border", "2px solid Blue" }
                    };
                    });
                    UXFramework.UXCell cNode1 = UXFramework.Creation.CreateCell(hash, ro1);
                    UXFramework.UXCell cNode3 = UXFramework.Creation.CreateCell(hash, im2);
                    hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                    {
                        return new Dictionary<string, dynamic>()
                    {
                        { "Height", 25 },
                        { "Constraint-Height", "FIXED" },
                        { "Disposition", "LEFT_MIDDLE" },
                        { "Border", "2px solid Blue" }
                    };
                    });
                    UXFramework.UXCell cNode2 = UXFramework.Creation.CreateCell(hash, ro);
                    hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                    {
                        return new Dictionary<string, dynamic>()
                    {
                        { "IsSelectable", true },
                        { "Background-Selectable", "#0B5395" },
                        { "Height", 25 },
                        { "Constraint-Height", "FIXED" },
                    };
                    });
                    UXFramework.UXRow row = UXFramework.Creation.CreateRow(3, hash, cNode1, cNode2, cNode3);

                    subtree.Add(row);
                }
            }

            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 20 },
                    { "Height", 25 },
                    { "Constraint-Width", "FIXED" },
                    { "Constraint-Height", "FIXED" },
                };
            });

            UXFramework.UXReadOnlyText ro2;
            if (projectTree.SubNodes.Count() > 0)
            {
                if (projectTree.IsSelected)
                {
                    ro2 = UXFramework.Creation.CreateReadOnlyText(hash, "text", "-");
                }
                else
                {
                    ro2 = UXFramework.Creation.CreateReadOnlyText(hash, "text", "+");
                }
            }
            else
            {
                ro2 = UXFramework.Creation.CreateReadOnlyText(hash, "text", "&nbsp;");
            }
            UXFramework.UXImage image2 = UXFramework.Creation.CreateImage(hash, "image", "left.png");

            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 32 },
                    { "Height", 25 },
                    { "Constraint-Width", "FIXED" },
                    { "Constraint-Height", "FIXED" },
                    { "Border", "2px solid Blue" }
                };
            });
            UXFramework.UXCell c3 = UXFramework.Creation.CreateCell(hash, ro2);
            UXFramework.UXCell c4 = UXFramework.Creation.CreateCell(hash, image2);
            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Height", 25 },
                    { "Constraint-Height", "FIXED" },
                };
            });
            UXFramework.UXReadOnlyText roText = UXFramework.Creation.CreateReadOnlyText(hash, "text", projectTree.NodeValue);
            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Height", 25 },
                    { "Constraint-Height", "FIXED" },
                    { "Border", "2px solid Blue" },
                    { "Disposition", "LEFT_MIDDLE" },
                };
            });
            UXFramework.UXCell c5 = UXFramework.Creation.CreateCell(hash, roText);

            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                    {
                        { "Id", "row" + index.ToString() },
                        { "IsSelectable", true },
                        { "IsClickable", true },
                        { "ProjectRef", projectTree },
                        { "Background-Selectable", "#0B5395" },
                        { "Background-Clickable", "#C8E3FB" },
                        { "Height", 25 },
                        { "Constraint-Height", "FIXED" },
                        { "Disposition", "LEFT_TOP" },
                        { "align", "left" },
                        { "valign", "top" }
                    };
            });

            UXFramework.UXRow first = UXFramework.Creation.CreateRow(3, hash, c3, c5, c4);
            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Disposition", "LEFT_TOP" },
                    { "align", "left" },
                    { "valign", "top" },
                    { "Width", 220 },
                    { "Constraint-Width", "FIXED" },
                    { "ColumnCount", 3 },
                    { "LineCount", subtree.Count() + 1 },
                };
            });

            UXFramework.UXTree tree = UXFramework.Creation.CreateTree(hash, "tree", first, subtree.ToArray());
            
            return tree;
        }

        public void OpenProject()
        {


            UXFramework.UXTree tree = CreateTreeView(0, currentProject, currentNode);
            UXFramework.UXBox box = UXFramework.Creation.CreateBox(null, 1120, 0);
            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling("left", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 220 },
                    { "Constraint-Width", "FIXED" },
                };
            });
            UXFramework.UXCell c1 = UXFramework.Creation.CreateCell(hash, tree);
            hash = Marshalling.MarshallingHash.CreateMarshalling("right", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 1100 },
                    { "Constraint-Width", "FIXED" },
                };
            });
            UXFramework.UXCell c2 = UXFramework.Creation.CreateCell(hash, box);

            hash = Marshalling.MarshallingHash.CreateMarshalling("row", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Disposition", "LEFT_TOP"},
                    { "align", "left" },
                    { "valign", "top" }
                };
            });
            UXFramework.UXRow row = UXFramework.Creation.CreateRow(2, hash, c1, c2);
            UXFramework.UXTable table = UXFramework.Creation.CreateTable("boxes", 2, 1, null, row);

            UXFramework.UXWindow win = UXFramework.UXWindow.CreateUXWindow("openProject", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", 1320 },
                    { "Height", 700 },
                    { "Constraint-Width", "FIXED" },
                    { "Constraint-Height", "FIXED" },
                    { "BackColor", "Blue" },
                    { "ForeColor", "White"},
                    { "children",
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            List<UXFramework.IUXObject> children = new List<UXFramework.IUXObject>();
                            children.Add(table);
                            return children;
                        })
                    }
                };

            });

            win.Navigate(this.web);

        }

    }
}
