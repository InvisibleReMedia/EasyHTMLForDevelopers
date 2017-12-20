using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class File : IProjectElement
    {
        private string fileName;

        public File(string f)
        {
            this.fileName = f;
        }

        public string TypeName
        {
            get { return "File"; }
        }

        public string ElementTitle
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }
    }
}
