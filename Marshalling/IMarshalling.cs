using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marshalling
{
    public interface IMarshalling : ICloneable
    {
        string Name { get; set; }
        dynamic Value { get; set; }
    }
}
