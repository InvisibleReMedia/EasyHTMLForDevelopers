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
                    { "a", "1" }, { "b", 3 }, { "c", list }, {"d", list2 }
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

            Console.WriteLine(d.ToString());

            Console.WriteLine(d.Copy<Data2>(false).ToString());

            // construction address book

            Marshalling.MarshallingList book = Marshalling.MarshallingList.CreateMarshalling("AddressBook", () =>
            {
                return new List<dynamic>() {
                    Marshalling.MarshallingHash.CreateMarshalling("my", () =>
                    {
                        return new Dictionary<string, dynamic>() {
                            { "Prenom", "Pablo"}, { "nom", "m"}, { "address", "32, rue d'argan" }, {"ville", "paris"}, {"telephone", "0600000000" }
                        };
                    }),
                    Marshalling.MarshallingHash.CreateMarshalling("my", () =>
                    {
                        return new Dictionary<string, dynamic>() {
                            { "Prenom", "Pablo"}, { "nom", "m"}, { "address", "32, rue d'argan" }, {"ville", "paris"}, {"telephone", "0600000000" }
                        };
                    }),
                    Marshalling.MarshallingHash.CreateMarshalling("my", () =>
                    {
                        return new Dictionary<string, dynamic>() {
                            { "Prenom", "Pablo"}, { "nom", "m"}, { "address", "32, rue d'argan" }, {"ville", "paris"}, {"telephone", "0600000000" }
                        };
                    }),
                    Marshalling.MarshallingHash.CreateMarshalling("my", () =>
                    {
                        return new Dictionary<string, dynamic>() {
                            { "Prenom", "Pablo"}, { "nom", "m"}, { "address", "32, rue d'argan" }, {"ville", "paris"}, {"telephone", "0600000000" }
                        };
                    })

                };

            });

            AddressBook ab = new AddressBook();
            book.Copy(false, ab);

            Console.WriteLine(ab.ToString());

            foreach (dynamic c in ab.Prenom)
            {
                Console.WriteLine(c.ToString());
            }

            foreach (dynamic c in ab.Contact)
            {
                Console.WriteLine(c.ToString());
            }

            AddressBook ab2 = AddressBook.CreateAddressBook("AddressBook2", () =>
            {
                return new List<Contact>() {
                    Contact.CreateContact("m1", () => {
                        return new Dictionary<string, dynamic>() {
                            { "Prenom", "Pablo"}, { "nom", "m"}, { "address", "32, rue d'argan" }, {"ville", "paris"}, {"telephone", "0600000000" }
                        };
                    }),
                    Contact.CreateContact("m1", () => {
                        return new Dictionary<string, dynamic>() {
                            { "Prenom", "Pablo"}, { "nom", "m"}, { "address", "32, rue d'argan" }, {"ville", "paris"}, {"telephone", "0600000000" }
                        };
                    }),
                    Contact.CreateContact("m1", () => {
                        return new Dictionary<string, dynamic>() {
                            { "Prenom", "Pablo"}, { "nom", "m"}, { "address", "32, rue d'argan" }, {"ville", "paris"}, {"telephone", "0600000000" }
                        };
                    }),
                };
            }) as AddressBook;


            foreach (dynamic c in ab2.Prenom)
            {
                Console.WriteLine(c.ToString());
            }

            foreach (dynamic c in ab2.Contact)
            {
                Console.WriteLine(c.ToString());
            }

            Console.ReadKey();

        }
    }
}
