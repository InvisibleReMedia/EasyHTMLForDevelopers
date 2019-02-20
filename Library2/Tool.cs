using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    /// <summary>
    /// A tool
    /// </summary>
    public class Tool : File
    {

        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="name">name</param>
        public Tool(string name)
            : base(name)
        {

        }

        /// <summary>
        /// HTML data
        /// </summary>
        public StringBuilder HTML
        {
            get
            {
                if (this.Exists("HTML"))
                {
                    return this.Get("HTML").Value;
                }
                else
                {
                    return new StringBuilder();
                }
            }
        }

        /// <summary>
        /// CSS data
        /// </summary>
        public StringBuilder CSS
        {
            get
            {
                if (this.Exists("CSS"))
                {
                    return this.Get("CSS").Value;
                }
                else
                {
                    return new StringBuilder();
                }
            }
        }

        /// <summary>
        /// Javascript data
        /// </summary>
        public StringBuilder Javascript
        {
            get
            {
                if (this.Exists("Javascript"))
                {
                    return this.Get("Javascript").Value;
                }
                else
                {
                    return new StringBuilder();
                }
            }
        }

        /// <summary>
        /// Javascript on load data
        /// </summary>
        public StringBuilder JavascriptOnLoad
        {
            get
            {
                if (this.Exists("JavascriptOnLoad"))
                {
                    return this.Get("JavascriptOnLoad").Value;
                }
                else
                {
                    return new StringBuilder();
                }
            }
        }

        /// <summary>
        /// Output tool
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder Output()
        {
            return new StringBuilder(this.HTML.ToString());
        }

        /// <summary>
        /// Export HTML
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportHTML()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.HTML.ToString()));
        }

        /// <summary>
        /// Export CSS
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportCSS()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.CSS.ToString()));
        }

        /// <summary>
        /// Export javascript
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportJavascript()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.Javascript.ToString()));
        }

        /// <summary>
        /// Export javascript on load
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportJavascriptOnLoad()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.JavascriptOnLoad.ToString()));
        }

        /// <summary>
        /// Export to a row
        /// </summary>
        /// <returns>ux row</returns>
        public UXFramework.UXRow ExportToolToRow()
        {
            return UXFramework.Creation.CreateRow(4, null, ExportHTML(), ExportCSS(), ExportJavascript(), ExportJavascriptOnLoad());
        }

    }
}
