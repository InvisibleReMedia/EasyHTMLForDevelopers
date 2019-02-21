using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    /// <summary>
    /// A web page
    /// </summary>
    public class Page : Tool
    {

        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="name">name</param>
        public Page(string name)
            : base(name)
        {

        }

        public string MasterPageName
        {
            get
            {
                return this.GetValue("MasterPageName", string.Empty);
            }
        }

        public UXFramework.UXRow ExportPageToRow()
        {
            return UXFramework.Creation.CreateRow(7, null, ExportPath(), ExportFileName(), ExportExtension(),
                                                           ExportHTML(), ExportCSS(), ExportJavascript(),
                                                           ExportJavascriptOnLoad());
        }
    }
}
