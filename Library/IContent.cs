using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IContent
    {
        string Container { get; set; }
        bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found);
    }
}
