using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    /// <summary>
    /// Instance of master object and tool
    /// </summary>
    public class HTMLObject : Marshalling.MarshallingHash
    {

        /// <summary>
        /// Constructor friendly
        /// </summary>
        protected HTMLObject()
            : base("HTMLObject")
        {

        }

        /// <summary>
        /// Creates an HTMLObject from a master object
        /// </summary>
        /// <param name="mo">Master object</param>
        public HTMLObject(MasterObject mo) : base("MasterObject")
        {
            this.Add(new Func<IDictionary<string,dynamic>>(() => {
                return new Dictionary<string, dynamic>() {
                    { "MasterObject", mo.Clone() }
                };
            }));
        }

        /// <summary>
        /// Creates an HTMLObject from a tool
        /// </summary>
        /// <param name="t">tool</param>
        public HTMLObject(Tool t) : base("Tool")
        {
            this.Add(new Func<IDictionary<string, dynamic>>(() =>
            {
                return new Dictionary<string, dynamic>() {
                    { "Tool", t.Clone() }
                };
            }));
        }

        /// <summary>
        /// Hook container
        /// </summary>
        public string HookContainer
        {
            get
            {
                return this.Get("HookContainer").Value;
            }
        }

        /// <summary>
        /// Make output
        /// </summary>
        /// <param name="f1">function for master object</param>
        /// <param name="f2">function for tool</param>
        /// <returns>output</returns>
        public StringBuilder Output(Func<MasterObject, string> f1, Func<Tool, string> f2)
        {
            StringBuilder output = new StringBuilder();
            if (this.Exists("MasterObject"))
            {
                MasterObject obj = this.Get("MasterObject") as MasterObject;
                output.Append(f1(obj));
            }
            else if (this.Exists("Tool"))
            {
                Tool obj = this.Get("Tool") as Tool;
                output.Append(f2(obj));
            }
            return output;
        }

        /// <summary>
        /// Returns output
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder Output()
        {
            return this.Output(obj =>
            {
                return obj.OutputAreas().ToString();
            }, obj =>
            {
                return obj.Output().ToString();
            });
        }

        /// <summary>
        /// Output javascript
        /// </summary>
        /// <returns>output javascript</returns>
        public StringBuilder OutputJavascript()
        {
            return this.Output(obj =>
            {
                return obj.Javascript.ToString();
            },
            obj =>
            {
                return obj.Javascript.ToString();
            });
        }

        /// <summary>
        /// Javascript on load
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder OutputJavascriptOnLoad()
        {
            return Output(obj =>
            {
                return obj.JavascriptOnLoad.ToString();
            },
            obj =>
            {
                return obj.JavascriptOnLoad.ToString();
            });
        }

        /// <summary>
        /// Output CSS
        /// </summary>
        /// <returns>output</returns>
        public StringBuilder OutputCSS()
        {
            return Output(obj =>
            {
                return obj.CSS.ToString();
            },
            obj =>
            {
                return obj.CSS.ToString();
            });
        }

        /// <summary>
        /// Export master object container
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportMasterObjectContainer()
        {
            UXFramework.UXReadOnlyText text = UXFramework.Creation.CreateReadOnlyText(null, "MasterObject", string.Empty);
            this.Get("MasterObject", (x, y) => {
                text = UXFramework.Creation.CreateReadOnlyText(null, x, y.Value.Container);
            });
            return UXFramework.Creation.CreateCell(null, text);
        }

        /// <summary>
        /// Export path and name
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportToolPathName()
        {
            UXFramework.UXReadOnlyText text = UXFramework.Creation.CreateReadOnlyText(null, "Tool", string.Empty);
            this.Get("Tool", (x, y) =>
            {
                text = UXFramework.Creation.CreateReadOnlyText(null, x, y.Value.Path.ToString() + "/" + y.Value.FileName + "." + y.Value.Extension);
            });
            return UXFramework.Creation.CreateCell(null, text);
        }

        /// <summary>
        /// Export object to row
        /// </summary>
        /// <returns>ux row</returns>
        public UXFramework.UXRow ExportObjectToRow()
        {
            return UXFramework.Creation.CreateRow(2, null, ExportMasterObjectContainer(), ExportToolPathName());
        }

    }
}
