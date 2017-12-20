using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IProjectElement
    {
        string TypeName { get; }
        string ElementTitle { get; }
    }
}
