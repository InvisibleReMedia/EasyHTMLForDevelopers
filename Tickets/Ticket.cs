using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    public class Ticket : Marshalling.MarshallingHash
    {

        public Ticket() : base("ticket")
        {

        }

        public Ticket(string name, Dictionary<string, dynamic> data)
            : base(name, data)
        {

        }

        public override Marshalling.IMarshalling Export(string title = "")
        {
            if (this.Exists("Achats")) {
                int sum = 0;
                foreach (Achat x in this.Get("Achats").Values)
                {
                    sum += x.Get("Prix").Value;
                }
                this.Set("Total", sum);
            }
            return this;
        }

        public static Ticket Ticket1()
        {
            Dictionary<string, dynamic> d = new Dictionary<string, dynamic>() {
                { "Id", 1},
                { "Achats",
                    Marshalling.MarshallingList.CreateMarshalling("achats", () => {
                        return new List<Achat>() { Achat.Achat1(), Achat.Achat2() };
                    })
                },
                { "Total", 0 }
            };
            return new Ticket("ticket1", d);
        }

        public static Ticket Ticket2()
        {
            Dictionary<string, dynamic> d = new Dictionary<string, dynamic>() {
                { "Id", 1},
                { "Achats",
                    Marshalling.MarshallingList.CreateMarshalling("achats", () => {
                        return new List<Achat>() { Achat.Achat2(), Achat.Achat3() };
                    })
                },
                { "Total", 0 }
            };
            return new Ticket("ticket1", d);
        }
    }
}
