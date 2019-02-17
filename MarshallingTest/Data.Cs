using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarshallingTest
{
    public class Data : Marshalling.PersistentDataObject
    {
        public Data()
        {
            this.Set("a", 1);
            this.Set("b", "2");
            this.Set("c", 0);
        }
    }
}
