using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarshallingTest
{

    public class Contact : Marshalling.MarshallingHash
    {

        public Contact() : base("contact") { }

        public Contact(string name, IDictionary<string, dynamic> e) : base(name, e) {}

        public override string[] GetProperties()
        {
            return new string[] { "Prenom", "nom", "address", "ville", "telephone" };
        }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static Contact CreateContact(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new Contact(name, f());
        }


    }

    public class AddressBook : Marshalling.MarshallingList
    {

        #region Constructor

        public AddressBook()
            : base("AddressBook")
        {
        }

        public AddressBook(string name, IEnumerable<dynamic> e) : base(name, e) { }

        #endregion

        #region Properties

        public IEnumerable<dynamic> Prenom
        {
            get
            {
                return from x in this.Values select x.GetProperty("Prenom").Value;
            }
        }

        public dynamic Contact
        {
            get
            {
                return this.GetProperty("1").Values;
            }
        } 

        #endregion

        /// <summary>
        /// Create marshalling
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static AddressBook CreateAddressBook(string name, Func<IEnumerable<dynamic>> f)
        {
            return new AddressBook(name, f());
        }



    }
}
