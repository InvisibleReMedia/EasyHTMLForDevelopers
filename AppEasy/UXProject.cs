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
            Marshalling.MarshallingHash hash;
            UXFramework.UXRow[] subtree = new UXFramework.UXRow[projectTree.SubNodes.Count() + projectTree.Elements.Count()];

            foreach (dynamic node in projectTree.Elements)
            {
                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "ImageWidth", 16 },
                        { "ImageHeight", 16 }
                    };
                });
                UXFramework.UXBox box1 = UXFramework.Creation.CreateBox(hash, 16, 16);
                UXFramework.UXImage im2 = UXFramework.Creation.CreateImage(hash, "image", "go.png");
                UXFramework.UXReadOnlyText ro = UXFramework.Creation.CreateReadOnlyText(null, "text", node.NodeValue);

                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Width", 32 },
                        { "Height", 32 },
                        { "Border", "1px solid black" }
                    };
                });
                UXFramework.UXCell cLeaf1 = UXFramework.Creation.CreateCell(hash, box1);
                UXFramework.UXCell cLeaf3 = UXFramework.Creation.CreateCell(hash, im2);
                UXFramework.UXCell cLeaf2 = UXFramework.Creation.CreateCell(null, ro);
                UXFramework.UXRow row = UXFramework.Creation.CreateRow(3, null, cLeaf1, cLeaf2, cLeaf3);

                subtree[nodesIndex] = row;
                ++nodesIndex;
            }

            foreach (Library.Node<string, Library.Accessor> sub in projectTree.SubNodes)
            {
                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "ImageWidth", 16 },
                        { "ImageHeight", 16 },
                    };
                });
                UXFramework.UXImage im1 = UXFramework.Creation.CreateImage(hash, "image", "ehd_plus.png");
                UXFramework.UXImage im2 = UXFramework.Creation.CreateImage(hash, "image", "go.png");
                UXFramework.UXReadOnlyText ro = UXFramework.Creation.CreateReadOnlyText(null, "text", sub.NodeValue);
                ro.Add(() => {
                    return new Dictionary<string,dynamic>() {
                    };
                });

                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Width", 32 },
                        { "Height", 32 },
                        { "Border", "1px solid black" }
                    };
                });
                UXFramework.UXCell cNode1 = UXFramework.Creation.CreateCell(hash, im1);
                UXFramework.UXCell cNode2 = UXFramework.Creation.CreateCell(hash, ro);
                /// HACK TO INSERT the Width
                cNode2.Add(() =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Width", 150 }
                    };
                });
                UXFramework.UXCell cNode3 = UXFramework.Creation.CreateCell(hash, im2);
                UXFramework.UXRow row = UXFramework.Creation.CreateRow(3, null, cNode1, cNode2, cNode3);

                subtree[nodesIndex] = row;
                ++nodesIndex;
            }


            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "ImageWidth", 16 },
                    { "ImageHeight", 16 }
                };
            });
            UXFramework.UXImage image1 = UXFramework.Creation.CreateImage(hash, "image", "ehd_plus.png");
            UXFramework.UXImage image2 = UXFramework.Creation.CreateImage(hash, "image", "go.png");

            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 32 },
                    { "Height", 32 },
                    { "Border", "1px solid black" }
                };
            });
            UXFramework.UXCell c3 = UXFramework.Creation.CreateCell(hash, image1);
            UXFramework.UXCell c4 = UXFramework.Creation.CreateCell(hash, image2);
            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 150 },
                    { "overflow", "hidden" },
                    { "Height", 32 },
                    { "Disposition", "LEFT_TOP" },
                };
            });
            UXFramework.UXReadOnlyText roText = UXFramework.Creation.CreateReadOnlyText(hash, "text", projectTree.NodeValue);
            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Height", 32 },
                    { "Border", "1px solid black" }
                };
            });
            UXFramework.UXCell c5 = UXFramework.Creation.CreateCell(hash, roText);

            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () => {
                return new Dictionary<string, dynamic>() {
                    { "Width", 214 },
                    { "ColumnCount", 3 },
                    { "LineCount", subtree.Count() + 1 },
                };
            });

            UXFramework.UXRow first = UXFramework.Creation.CreateRow(3, null, c3, c5, c4);
            List<UXFramework.UXRow> list = new List<UXFramework.UXRow>();
            UXFramework.UXTree tree = UXFramework.Creation.CreateTree(hash, "tree", first, list.Concat(subtree).ToArray());
            
            return tree;
        }

        public static void OpenProject(WebBrowser browser)
        {

            Library.Project p = new Library.Project();
            UXFramework.UXWindow win = UXFramework.UXWindow.CreateUXWindow("openProject", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", 220 },
                    { "Height", 700 },
                    { "BackColor", "Blue" },
                    { "ForeColor", "White"},
                    { "children",
                        UXFramework.ChildCollection.CreateChildCollection("children", () => {
                            List<UXFramework.IUXObject> children = new List<UXFramework.IUXObject>();
                            UXFramework.UXTree t = CreateTreeView(0, p, p.Hierarchy);
                            t.Add(() =>
                            {
                                return new Dictionary<string, dynamic>()
                                {
                                    { "IsRoot", true }
                                };
                            });
                            children.Add(t);
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
