using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    public class Gain : Marshalling.MarshallingHash
    {

        public Gain()
            : base("Gain")
        {

        }

        public Gain(string name, Dictionary<string, dynamic> d)
            : base(name, d)
        {

        }

        public int Total
        {
            get
            {
                if (this.Exists("Total"))
                    return this.Get("Total");
                else
                    return 0;
            }
        }


    }
}
