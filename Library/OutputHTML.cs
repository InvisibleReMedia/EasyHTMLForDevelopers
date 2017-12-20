using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class OutputHTML
    {
        private StringBuilder html = new StringBuilder();
        private StringBuilder css = new StringBuilder();
        private StringBuilder javascript = new StringBuilder();
        private StringBuilder javascriptOnLoad = new StringBuilder();

        public StringBuilder HTML
        {
            get { return this.html; }
        }

        public StringBuilder CSS
        {
            get { return this.css; }
        }

        public void AppendCSS(List<CodeCSS> cssAdditional)
        {
            cssAdditional.ForEach(a => { this.CSS.Append(a.GenerateCSS(false, true, true) + Environment.NewLine); });
        }

        public StringBuilder JavaScript
        {
            get { return this.javascript; }
        }

        public StringBuilder JavaScriptOnLoad
        {
            get { return this.javascriptOnLoad; }
        }
    }
}
