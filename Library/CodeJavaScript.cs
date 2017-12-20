using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class CodeJavaScript : ICloneable
    {
        private string code;

        public string Code
        {
            get { return this.code; }
            set { this.code = value; string result = this.GeneratedCode; }
        }

        public string GeneratedCode
        {
            get { return Project.CurrentProject.Configuration.Replace(this.code); }
        }

        public object Clone()
        {
            CodeJavaScript cj = new CodeJavaScript();
            this.code = ExtensionMethods.CloneThis(cj.code);
            return cj;
        }
    }
}
