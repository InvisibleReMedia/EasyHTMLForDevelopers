using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    /// <summary>
    /// Master object
    /// </summary>
    public class MasterObject : Tool
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name"></param>
        public MasterObject(string name)
            : base(name)
        {

        }

        /// <summary>
        /// Gets the container
        /// </summary>
        public string Container
        {
            get
            {
                return this.GetValue("Container", string.Empty);
            }
        }

        /// <summary>
        /// Gets column count
        /// </summary>
        public uint ColumnCount
        {
            get
            {
                return this.GetValue("ColumnCount", 0u);
            }
        }

        /// <summary>
        /// Gets line count
        /// </summary>
        public uint LineCount
        {
            get
            {
                return this.GetValue("LineCount", 0u);
            }
        }

        /// <summary>
        /// List of lines
        /// </summary>
        public IEnumerable<MasterObject> Horizontally
        {
            get
            {
                return this.TransformList<MasterObject>("Horizontally");
            }
        }

        /// <summary>
        /// List of columns
        /// </summary>
        public IEnumerable<MasterObject> Vertically
        {
            get
            {
                return this.TransformList<MasterObject>("Vertically");
            }
        }


        /// <summary>
        /// Objects instance
        /// </summary>
        public IEnumerable<HTMLObject> Objects
        {
            get
            {
                return this.TransformList<HTMLObject>("Objects");
            }
        }

        /// <summary>
        /// Export objects
        /// Create a table with all objects information
        /// </summary>
        /// <returns>ux table</returns>
        public UXFramework.UXTable ExportObjectsToTable()
        {
            UXFramework.UXRow[] rows = new UXFramework.UXRow[this.Objects.Count()];
            uint index = 0;
            foreach (HTMLObject h in this.Objects)
            {
                UXFramework.UXReadOnlyText text = UXFramework.Creation.CreateReadOnlyText(null, "HtmlObject." + index.ToString(), h.ToString());
                UXFramework.UXCell cell = UXFramework.Creation.CreateCell(null, text);
                rows[index] = UXFramework.Creation.CreateRow(1, null, cell);
                ++index;
            }
            return UXFramework.Creation.CreateTable("Objects", 1, Convert.ToUInt32(this.Objects.Count()), null, rows);
        }

        /// <summary>
        /// List of each area
        /// Creates a table with all horizontally and vertically
        /// </summary>
        /// <returns>ux table</returns>
        public UXFramework.UXTable ExportAreasToTable()
        {
            UXFramework.UXRow[] rows = new UXFramework.UXRow[this.Horizontally.Count()];
            uint indexHorizontally = 0;
            foreach (MasterObject ho in this.Horizontally)
            {
                UXFramework.UXCell[] cells = new UXFramework.UXCell[ho.Vertically.Count()];
                uint indexVertically = 0;
                foreach (MasterObject vo in ho.Vertically)
                {
                    UXFramework.UXReadOnlyText text = UXFramework.Creation.CreateReadOnlyText(null, "obj." + indexHorizontally.ToString() + "." + indexVertically.ToString(), vo.ToString());
                    cells[indexVertically] = UXFramework.Creation.CreateCell(null, text);
                    ++indexVertically;
                }
                rows[indexHorizontally] = UXFramework.Creation.CreateRow(Convert.ToUInt32(cells.Count()), null, cells);
            }
            return UXFramework.Creation.CreateTable("Areas", 1, Convert.ToUInt32(rows.Count()), null, rows);
        }

        /// <summary>
        /// Output areas
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder OutputAreas()
        {
            StringBuilder output = new StringBuilder();
            UXFramework.UXRow[] rows = new UXFramework.UXRow[this.Horizontally.Count()];
            output.Append("<table>");
            foreach (MasterObject ho in this.Horizontally)
            {
                output.AppendFormat("<tr rowspan='{0}'>", ho.LineCount);
                foreach (MasterObject vo in ho.Vertically)
                {
                    output.AppendFormat("<td colspan='{0}'>", vo.ColumnCount);
                    foreach (HTMLObject obj in this.Objects)
                    {
                        if (obj.HookContainer == vo.Container)
                        {
                            output.Append(obj.Output().ToString());
                        }
                    }
                    foreach (HTMLObject obj in vo.Objects)
                    {
                        if (obj.HookContainer == vo.Container)
                        {
                            output.Append(obj.Output().ToString());
                        }
                    }
                    output.Append("</td>");
                }
                output.Append("</tr>");
            }
            output.Append("</table>");
            return output;
        }

    }
}
