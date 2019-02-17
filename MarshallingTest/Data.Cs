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

    public class Data2 : Marshalling.PersistentDataObject
    {

        public Data2()
        {
            this.Set("x", 1);
            this.Set("y", 2);
        }
    }
}
