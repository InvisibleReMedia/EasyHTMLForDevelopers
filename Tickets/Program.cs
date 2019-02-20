using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets
{
    class Program
    {
        static void Main(string[] args)
        {

            Ticket t = Ticket.Ticket1();

            Console.WriteLine(t.ToString());
            Console.WriteLine(t.Export().ToString());

            Ticket g = new Ticket();
            t.Export().Copy(true, g);

            Console.WriteLine(g.ToString());

            Gain gain = new Gain();

            g.Copy(true, gain, "Total");
            
            Console.WriteLine(gain.Total);

            Ticket.Ticket2().Export().Copy(true, gain, "Total");

            Console.WriteLine(gain.Total);

            Console.ReadKey();

        }
    }
}
