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
                UXFramework.UXImage im2 = UXFramework.Creation.CreateImage(hash, "image", "left.png");
                UXFramework.UXReadOnlyText ro = UXFramework.Creation.CreateReadOnlyText(null, "text", node.NodeValue);

                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Width", 32 },
                        { "Height", 20 },
                        { "align", "left" },
                        { "Border", "2px solid Blue" }
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
                UXFramework.UXImage im2 = UXFramework.Creation.CreateImage(hash, "image", "left.png");
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
                        { "Height", 20 },
                        { "Border", "2px solid Blue" }
                    };
                });
                UXFramework.UXCell cNode1 = UXFramework.Creation.CreateCell(hash, im1);
                UXFramework.UXCell cNode3 = UXFramework.Creation.CreateCell(hash, im2);
                hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
                {
                    return new Dictionary<string, dynamic>()
                    {
                        { "Width", 150 },
                        { "Height", 20 },
                        { "Disposition", "LEFT_MIDDLE" },
                        { "Border", "2px solid Blue" }
                    };
                });
                UXFramework.UXCell cNode2 = UXFramework.Creation.CreateCell(hash, ro);
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
            UXFramework.UXImage image2 = UXFramework.Creation.CreateImage(hash, "image", "left.png");

            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 32 },
                    { "Height", 20 },
                    { "Border", "2px solid Blue" }
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
                    { "Height", 20 },
                };
            });
            UXFramework.UXReadOnlyText roText = UXFramework.Creation.CreateReadOnlyText(hash, "text", projectTree.NodeValue);
            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Height", 20 },
                    { "Border", "2px solid Blue" },
                    { "Disposition", "LEFT_MIDDLE" },
                };
            });
            UXFramework.UXCell c5 = UXFramework.Creation.CreateCell(hash, roText);

            hash = Marshalling.MarshallingHash.CreateMarshalling("hash", () => {
                return new Dictionary<string, dynamic>() {
                    { "BackColor", "#0B5395" },
                    { "ForeColor", "White"},
                    { "Disposition", "LEFT_TOP" },
                    { "Width", 214 },
                    { "Height-Minimum", 250 },
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

            UXFramework.UXTree tree = CreateTreeView(0, p, p.Hierarchy);
            tree.Add(() =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "IsRoot", true }
                };
            });
            UXFramework.UXBox box = UXFramework.Creation.CreateBox(null, 1120, 700);
            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling("left", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 214 },
                    { "Height-Minimum", 250 },
                    { "Constraint-Width", "FIXED" }
                };
            });
            UXFramework.UXCell c1 = UXFramework.Creation.CreateCell(hash, tree);
            hash = Marshalling.MarshallingHash.CreateMarshalling("right", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "Width", 1120 },
                    { "Height", 700 },
                    { "Constraint-Width", "FIXED" }
                };
            });
            UXFramework.UXCell c2 = UXFramework.Creation.CreateCell(hash, box);

            UXFramework.UXRow row = UXFramework.Creation.CreateRow(2, null, c1, c2);
            UXFramework.UXTable table = UXFramework.Creation.CreateTable("boxes", 2, 1, null, row);

            UXFramework.UXWindow win = UXFramework.UXWindow.CreateUXWindow("openProject", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", 1320 },
                    { "Height", 700 },
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

            Console.WriteLine(win.ToString());

            win.Navigate(browser);

        }

    }
}
