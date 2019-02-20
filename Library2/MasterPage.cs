using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    /// <summary>
    /// Implements a master page
    /// </summary>
    public class MasterPage : MasterObject
    {

        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="name">name</param>
        public MasterPage(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets Meta
        /// </summary>
        public StringBuilder Meta
        {
            get
            {
                if (this.Exists("Meta"))
                {
                    return this.Get("Meta").Value;
                }
                else
                {
                    return new StringBuilder();
                }
            }
        }

        /// <summary>
        /// Output meta
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder OutputMeta()
        {
            return this.Meta;
        }

        /// <summary>
        /// Output html source
        /// </summary>
        /// <returns>output</returns>
        public new StringBuilder Output()
        {
            StringBuilder output = new StringBuilder();
            output.Append("<html>");
            output.Append(this.OutputMeta().ToString());
            output.Append("<style>");
            output.Append(this.OutputCSS().ToString());
            output.Append("</style>");
            output.Append("<script language='JavaScript' type='text/javascript'>");
            output.Append(this.OutputJavascript().ToString());
            output.Append("</script>");
            output.Append("<script language='JavaScript' type='text/javascript'>" + Environment.NewLine);
            output.Append("function initialize() {" + Environment.NewLine);
            output.Append(this.OutputJavascriptOnLoad().ToString());
            output.Append("}" + Environment.NewLine);
            output.Append("</script>");
            output.Append("<body onload='javascript:initialize();'>");
            output.Append(this.OutputAreas().ToString());
            output.Append("</body>");
            output.Append("</html>");
            return output;
        }

        /// <summary>
        /// Output with a function
        /// </summary>
        /// <param name="f">function</param>
        /// <returns>output</returns>
        public StringBuilder Output(Func<HTMLObject, string> f) {

            StringBuilder output = new StringBuilder();
            foreach (HTMLObject obj in this.Objects)
            {
                if (obj.HookContainer == this.Container)
                {
                    output.Append(f(obj));
                }
            }
            return output;

        }

        /// <summary>
        /// Output all javascript code
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder OutputJavascript()
        {
            return Output(obj =>
            {
                return obj.OutputJavascript().ToString();
            });
        }

        /// <summary>
        /// Output all javascript on load code
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder OutputJavascriptOnLoad()
        {
            return Output(obj =>
            {
                return obj.OutputJavascriptOnLoad().ToString();
            });
        }

        /// <summary>
        /// Output all CSS code
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder OutputCSS()
        {
            return Output(obj =>
            {
                return obj.OutputCSS().ToString();
            });
        }
    }
}
