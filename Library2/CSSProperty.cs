using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    public class CSSProperty : Marshalling.MarshallingHash
    {
        public CSSProperty()
            : base("CSSProperty")
        {
        }

        public string PropertyValue
        {
            get { return this.GetValue("PropertyValue", string.Empty); }
        }

        public string PropertyName
        {
            get { return this.GetValue("PropertyName", string.Empty); }
        }

    }
}
