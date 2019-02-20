using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConverter
{
    public class HashConverter : Marshalling.MarshallingHash
    {

        public HashConverter()
            : base("hash")
        {

        }

        public static HashConverter ProjectItems(Library.Project proj)
        {
            HashConverter hash = new HashConverter();
            return proj.Export<Library.Project, HashConverter>(hash, (x) =>
            {
                hash.Add(() =>
                {
                    Dictionary<string, dynamic> d = new Dictionary<string, dynamic>();
                    d.Add("Nom du projet", x.Title);
                    d.Add("Date de création", x.CreationDate);
                    d.Add("Date de modification", x.ModificationDate);
                    d.Add("Révision", x.Revision);
                    d.Add("Nombre de pages", x.Pages.Count);
                    d.Add("Nombre de master pages", x.MasterPages.Count);
                    d.Add("Nombre de master objets", x.MasterObjects.Count);
                    d.Add("Nombre d'outils", x.Tools.Count);
                    d.Add("Nombre de fichiers", x.Files.Count);
                    return d;
                });
                return hash;
            });
        }

        public static UXFramework.UXRow MakeUXHeaderProjectItems(HashConverter element)
        {
            uint index = 0;
            List<UXFramework.UXCell> cells = new List<UXFramework.UXCell>();
            foreach (string s in element.HashKeys)
            {
                dynamic d = s;
                cells.Add(UXFramework.UXCell.CreateUXCell(index.ToString(), () =>
                {
                    return new Dictionary<string, dynamic>() {
                        { "Border", "2px solid Blue" },
                        { "headerName", s },
                        { "children", UXFramework.Creation.CreateChildren(UXFramework.Creation.CreateReadOnlyText(null, "text", s)) }
                    };
                }));
                ++index;
            }
            UXFramework.UXRow row = UXFramework.Creation.CreateRow(Convert.ToUInt32(cells.Count()), null, cells.ToArray());
            row.Add(() =>
            {
                return new Dictionary<string, dynamic>() {
                    { "BackColor", "#1B63A5" },
                };
            });
            return row;
        }

        public static UXFramework.UXRow MakeUXProjectItems(HashConverter element)
        {
            uint index = 0;
            List<UXFramework.UXCell> cells = new List<UXFramework.UXCell>();
            foreach (string s in element.HashKeys)
            {
                dynamic d = element.Get(s).Value;
                cells.Add(UXFramework.UXCell.CreateUXCell(index.ToString(), () =>
                {
                    return new Dictionary<string, dynamic>() {
                        { "Border", "2px solid Blue" },
                        { s, d },
                        { "children", UXFramework.Creation.CreateChildren(UXFramework.Creation.CreateReadOnlyText(null, "text", d.ToString())) }
                    };
                }));
                ++index;
            }
            UXFramework.UXRow row = UXFramework.Creation.CreateRow(Convert.ToUInt32(cells.Count()), null, cells.ToArray());
            row.Add(() =>
            {
                return new Dictionary<string, dynamic>() {
                    { "BackColor", "#0B5395" },
                    { "IsSelectable", true },
                    { "Background-Selectable", "#1B63A5" },
                    { "IsClickable", true },
                    { "Background-Clickable", "#D9F4FB" },
                    { "FileName", element.Get("Nom du projet").Value + ".bin" }
                };
            });
            return row;
        }


        public static HashConverter HierarchyItems(Library.Project p, Library.Node<string, Library.Accessor> node)
        {
            HashConverter hash = new HashConverter();
            if (node.Elements.Count() > 0) {
                foreach (Library.Leaf<Library.Accessor> leaf in node.Elements)
                {
                    dynamic x = leaf.Object.GetObject(p);
                    hash.Set(x.Unique, x.Name);
                }
            }
            else if (node.SubNodes.Count() > 0)
            {
                uint index = 0;
                foreach (Library.Node<string, Library.Accessor> sub in node.SubNodes)
                {
                    hash.Set("index." + index.ToString(), sub.NodeValue);
                    ++index;
                }
            }
            return hash;
        }

    }
}
