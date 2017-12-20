using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ProjectCap : IProjectElement
    {
        private string title;

        public ProjectCap(string title)
        {
            this.title = title;
        }

        public string TypeName
        {
            get { return "ProjectCap"; }
        }

        public string ElementTitle
        {
            get { return this.title; }
            set { this.title = value; }
        }
    }
}
