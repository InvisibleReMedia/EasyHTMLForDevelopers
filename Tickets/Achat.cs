using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    /// <summary>
    /// Achat
    /// </summary>
    public class Achat : Marshalling.MarshallingHash
    {

        public Achat(string name, Dictionary<string, dynamic> d)
            : base(name, d)
        {

        }

        public static Achat Achat1()
        {
            Dictionary<string, dynamic> d = new Dictionary<string, dynamic>()
            {
                { "Id", 1 },
                { "Titre", "Achat de vêtements"},
                { "Prix", 100 }
            };
            return new Achat("achat1", d);
        }

        public static Achat Achat2()
        {
            Dictionary<string, dynamic> d = new Dictionary<string, dynamic>()
            {
                { "Id", 2 },
                { "Titre", "Achat de maquillage"},
                { "Prix", 200 }
            };
            return new Achat("achat2", d);
        }

        public static Achat Achat3()
        {
            Dictionary<string, dynamic> d = new Dictionary<string, dynamic>()
            {
                { "Id", 1 },
                { "Titre", "Achat d'ordinateurs"},
                { "Prix", 10000 }
            };
            return new Achat("achat3", d);
        }

    }
}
