using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marshalling
{
    public interface IMarshalling : ICloneable
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the value
        /// </summary>
        dynamic Value { get; set; }
        /// <summary>
        /// Returns a list of string (as a tree)
        /// </summary>
        /// <param name="depth">depth tabulation</param>
        /// <returns>enumeration of string</returns>
        IEnumerable<string> ToTabularString(uint depth);
        T Copy<T>() where T : PersistentDataObject, new();
        T Copy<T>(params string[] names) where T : PersistentDataObject, new();
        void Copy<T>(T obj) where T : PersistentDataObject;
        void Copy<T>(T obj, params string[] names) where T : PersistentDataObject;
        void Mapping<T>(T obj, Func<string, string> f) where T : PersistentDataObject;
        void Mapping<T>(T obj, Func<string, string> f, params string[] names) where T : PersistentDataObject;
        string Format(string input);
    }
}
