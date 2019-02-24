using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    public class CSSProperties : Marshalling.MarshallingHash
    {

        public CSSProperties()
            : base("CSSProperties")
        {

        }

        public IEnumerable<CSSProperty> Properties
        {
            get { return this.ConversionToList<CSSProperty>("Properties"); }
        }
    }
}
