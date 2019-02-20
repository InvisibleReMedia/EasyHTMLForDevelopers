using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    public static class Creation
    {

        /// <summary>
        /// Create children
        /// </summary>
        /// <param name="controls">children to add</param>
        /// <returns>a ChildCollection</returns>
        public static ChildCollection CreateChildren(params UXControl[] controls)
        {
            return ChildCollection.CreateChildCollection("children", () =>
            {
                return controls.ToList();
            });
        }

        /// <summary>
        /// Create a table
        /// </summary>
        /// <param name="name">name of table</param>
        /// <param name="ColumnCount">column count</param>
        /// <param name="LineCount">line count</param>
        /// <param name="properties">props</param>
        /// <param name="rows">lines</param>
        /// <returns>ux table</returns>
        public static UXTable CreateTable(string name, uint ColumnCount, uint LineCount, Marshalling.MarshallingHash properties, params UXRow[] rows)
        {
            UXTable t = UXTable.CreateUXTable(name, () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "ColumnCount", ColumnCount },
                    { "LineCount", LineCount },
                    { "children", UXFramework.Creation.CreateChildren(rows) }
                };
            });
            if (properties != null)
                t.Bind(properties);
            return t;
        }

        /// <summary>
        /// Create a row
        /// </summary>
        /// <param name="ColumnCount">column count</param>
        /// <param name="properties">props</param>
        /// <param name="cells">columns</param>
        /// <returns>ux row</returns>
        public static UXRow CreateRow(uint ColumnCount, Marshalling.MarshallingHash properties, params UXCell[] cells)
        {
            UXRow row = UXRow.CreateUXRow("row", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "ColumnCount", ColumnCount },
                    { "children", UXFramework.Creation.CreateChildren(cells) }
                };
            });
            if (properties != null)
                row.Bind(properties);
            return row;
        }

        /// <summary>
        /// Create a cell
        /// </summary>
        /// <param name="properties">props</param>
        /// <param name="controls">controls</param>
        /// <returns>ux cell</returns>
        public static UXCell CreateCell(Marshalling.MarshallingHash properties, params UXControl[] controls)
        {
            UXCell cell = UXCell.CreateUXCell("cell", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "children", UXFramework.Creation.CreateChildren(controls) }
                };
            });
            if (properties != null)
                cell.Bind(properties);
            return cell;
        }

        /// <summary>
        /// Create a read only text
        /// </summary>
        /// <param name="properties">props</param>
        /// <param name="id">ux id</param>
        /// <param name="text">ux text</param>
        /// <returns>ux read only text</returns>
        public static UXReadOnlyText CreateReadOnlyText(Marshalling.MarshallingHash properties, string id, string text)
        {
            UXReadOnlyText t = UXReadOnlyText.CreateUXReadOnlyText("text", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Id", id },
                    { "Text", text }
                };
            });
            if (properties != null)
                t.Bind(properties);
            return t;
        }

        /// <summary>
        /// Create an image
        /// </summary>
        /// <param name="properties">props</param>
        /// <param name="id">ux id</param>
        /// <param name="text">ux text</param>
        /// <returns>ux read only text</returns>
        public static UXImage CreateImage(Marshalling.MarshallingHash properties, string id, string fileName)
        {
            UXImage im = UXImage.CreateUXImage("image", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Id", id },
                    { "ImageFile", fileName }
                };
            });
            if (properties != null)
                im.Bind(properties);
            return im;
        }

        /// <summary>
        /// Create a box
        /// </summary>
        /// <param name="properties">props</param>
        /// <returns>ux read only text</returns>
        public static UXBox CreateBox(Marshalling.MarshallingHash properties, int width, int height)
        {
            UXBox im = UXBox.CreateUXBox("box", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", width },
                    { "Height", height },
                    { "Constraint-Width", "FIXED" },
                    { "Constraint-Height", "FIXED" }
                };
            });
            if (properties != null)
                im.Bind(properties);
            return im;
        }


        /// <summary>
        /// Create a window
        /// </summary>
        /// <param name="properties">props</param>
        /// <returns>ux window</returns>
        public static UXWindow CreateWindow(Marshalling.MarshallingHash properties, int width, int height, params UXControl[] controls)
        {
            UXWindow win = UXWindow.CreateUXWindow("win", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Width", width },
                    { "Height", height },
                    { "Constraint-Width", "FIXED" },
                    { "Constraint-Height", "FIXED" },
                    { 
                        "children",
                        ChildCollection.CreateChildCollection("children", () => {
                            return controls.ToList();
                        })
                    }
                };
            });
            if (properties != null)
                win.Bind(properties);
            return win;
        }



        /// <summary>
        /// Create tree
        /// </summary>
        /// <param name="properties">props</param>
        /// <param name="id">id</param>
        /// <param name="first">first row</param>
        /// <param name="nexts">next rows</param>
        /// <returns>ux tree</returns>
        public static UXTree CreateTree(Marshalling.MarshallingHash properties, string id, UXRow first, params UXRow[] nexts)
        {
            UXTree t = UXTree.CreateUXTree("tree", () =>
            {
                List<UXRow> rows = new List<UXRow>();
                rows.Add(first);
                rows = rows.Concat(nexts).ToList();
                return new Dictionary<string, dynamic>()
                {
                    { "Id", id },
                    { "children", rows }
                };
            });
            if (properties != null)
                t.Bind(properties);
            return t;
        }


    }
}
