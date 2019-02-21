using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    /// <summary>
    /// Implements a project
    /// </summary>
    public class Project : Marshalling.MarshallingHash
    {

        /// <summary>
        /// Project
        /// </summary>
        /// <param name="title"></param>
        public Project(string title)
            : base(title)
        {
        }

        /// <summary>
        /// File name
        /// </summary>
        public string FileName
        {
            get
            {
                return this.GetValue("FileName", string.Empty);
            }
        }

        /// <summary>
        /// Master page list
        /// </summary>
        public IEnumerable<MasterPage> MasterPages
        {
            get
            {
                return this.TransformList<MasterPage>("MasterPages");
            }
        }

        /// <summary>
        /// Master object list
        /// </summary>
        public IEnumerable<MasterObject> MasterObjects
        {
            get
            {
                return this.TransformList<MasterObject>("MasterObjects");
            }
        }

        /// <summary>
        /// HTML object list
        /// </summary>
        public IEnumerable<HTMLObject> Objects
        {
            get
            {
                return this.TransformList<HTMLObject>("Objects");
            }
        }

        /// <summary>
        /// Tool list
        /// </summary>
        public IEnumerable<Tool> Tools
        {
            get
            {
                return this.TransformList<Tool>("Tools");
            }
        }

        /// <summary>
        /// File list
        /// </summary>
        public IEnumerable<File> Files
        {
            get
            {
                return this.TransformList<File>("Files");
            }
        }

        /// <summary>
        /// Page list
        /// </summary>
        public IEnumerable<Page> Pages
        {
            get
            {
                return this.TransformList<Page>("Pages");
            }
        }

        /// <summary>
        /// Export to a table
        /// </summary>
        /// <typeparam name="T">source</typeparam>
        /// <param name="name">name</param>
        /// <param name="columnCount">column count</param>
        /// <param name="f">function</param>
        /// <param name="source">source enumerable</param>
        /// <returns>ux table</returns>
        protected UXFramework.UXTable ExportToTable<T>(string name, uint columnCount, Func<T, UXFramework.UXRow> f, IEnumerable<T> source)
        {
            List<UXFramework.UXRow> rows = new List<UXFramework.UXRow>();
            foreach (T t in source)
            {
                rows.Add(f(t));
            }
            return UXFramework.Creation.CreateTable(name, columnCount, Convert.ToUInt32(rows.Count()), null, rows.ToArray());
        }

        /// <summary>
        /// Export file to table
        /// </summary>
        /// <returns>table</returns>
        public UXFramework.UXTable ExportFileToTable()
        {
            return ExportToTable<File>("files", 3, x =>
            {
                return x.ExportFileToRow();
            }, this.Files);
        }

        /// <summary>
        /// Export tool to table
        /// </summary>
        /// <returns>ux table</returns>
        public UXFramework.UXTable ExportToolToTable()
        {
            return ExportToTable<Tool>("tools", 4, x =>
            {
                return x.ExportToolToRow();
            }, this.Tools);
        }

        /// <summary>
        /// Export page to table
        /// </summary>
        /// <returns>ux table</returns>
        public UXFramework.UXTable ExportPageToTable()
        {
            return ExportToTable<Page>("pages", 7, x =>
            {
                return x.ExportPageToRow();
            }, this.Pages);
        }

        /// <summary>
        /// Export object to table
        /// </summary>
        /// <returns>ux table</returns>
        public UXFramework.UXTable ExportObjectToTable()
        {
            return ExportToTable<HTMLObject>("objects", 4, x =>
            {
                return x.ExportObjectToRow();
            }, this.Objects);
        }

        public UXFramework.UXTable ExportMasterObjectToTable()
        {
            return ExportToTable<MasterObject>("masterObjects", 4, x =>
            {
                x.ExportAreasToTable();
            }, this.MasterObjects);
        }
    }
}
