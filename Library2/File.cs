using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    public class File : Marshalling.MarshallingHash
    {

        /// <summary>
        /// Empty constructor
        /// </summary>
        public File()
            : base("File")
        {

        }

        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="name">name</param>
        public File(string name)
            : base(name)
        {

        }

        /// <summary>
        /// Gets extension
        /// </summary>
        public string Extension
        {
            get
            {
                if (this.Exists("Extension"))
                {
                    return this.Get("Extension").Value;
                }
                else
                {
                    return "html";
                }
            }
        }

        /// <summary>
        /// Gets file name
        /// </summary>
        public string FileName
        {
            get
            {
                if (this.Exists("FileName"))
                {
                    return this.Get("FileName").Value;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets path
        /// </summary>
        public StringBuilder Path
        {
            get
            {
                if (this.Exists("Path"))
                {
                    return this.Get("Path").Value;
                }
                else
                {
                    return new StringBuilder();
                }
            }
        }

        /// <summary>
        /// Export extension
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportExtension()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.Extension));
        }

        /// <summary>
        /// Export file name
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportFileName()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.FileName));
        }

        /// <summary>
        /// Export path
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportPath()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.Path.ToString()));
        }

        /// <summary>
        /// Export to row
        /// </summary>
        /// <returns>ux row</returns>
        public UXFramework.UXRow ExportFileToRow()
        {
            return UXFramework.Creation.CreateRow(3, null, ExportPath(), ExportFileName(), ExportExtension());
        }

    }
}
