using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarshallingTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Data d = new Data();
            Console.WriteLine(d.ToString());

            Marshalling.IMarshalling list = Marshalling.MarshallingList.CreateMarshalling("test", () =>
            {
                return new List<dynamic>() { 100, 1000, "etc", 1.2 };
            });
            Console.WriteLine(list.ToString());
            Marshalling.IMarshalling list2 = Marshalling.MarshallingList.CreateMarshalling("test", () =>
            {
                return new List<dynamic>() { 10, list };
            });
            Console.WriteLine(list2.ToString());

            Marshalling.IMarshalling hash = Marshalling.MarshallingHash.CreateMarshalling("test2", () =>
            {
                return new Dictionary<string, dynamic>()
                {
                    { "a", "1"}, { "b", 3}, { "c", list}, {"d", list2}
                };
            });

            Marshalling.IMarshalling hash2 = Marshalling.MarshallingHash.CreateMarshalling("test3", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "x", list }, { "y", hash }
                };
            });

            Console.WriteLine(hash.ToString());

            Console.WriteLine(hash2.ToString());

            Console.WriteLine(list.Format("%0 oui %1 non %2 et %3"));
            Console.ReadKey();

        }
    }
}
