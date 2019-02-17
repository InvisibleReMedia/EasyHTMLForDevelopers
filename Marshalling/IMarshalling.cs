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
        /// Gets or sets the value
        /// </summary>
        IEnumerable<IMarshalling> Values { get; }
        /// <summary>
        /// Gets all property names
        /// </summary>
        /// <returns>properties</returns>
        string[] GetProperties();
        /// <summary>
        /// Set value
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        void Set(string name, dynamic value);
        /// <summary>
        /// Gets value
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="init">initialisation</param>
        /// <returns>value</returns>
        dynamic Get(string name, dynamic init = null);
        /// <summary>
        /// Gets a property
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>value</returns>
        IMarshalling GetProperty(string name);
        /// <summary>
        /// Gets an enumerator
        /// to enumerate elements
        /// </summary>
        /// <returns>a dynamic object</returns>
        IEnumerator<IMarshalling> GetEnumerator();
        /// <summary>
        /// Returns a list of string (as a tree)
        /// </summary>
        /// <param name="depth">depth tabulation</param>
        /// <returns>enumeration of string</returns>
        IEnumerable<string> ToTabularString(uint depth);
        T Copy<T>(bool clone) where T : PersistentDataObject, new();
        T Copy<T>(bool clone, params string[] names) where T : PersistentDataObject, new();
        void Copy<T>(bool clone, T obj) where T : PersistentDataObject;
        void Copy<T>(bool clone, T obj, params string[] names) where T : PersistentDataObject;
        void Mapping<T>(bool clone, T obj, Func<string, string> f) where T : PersistentDataObject;
        void Mapping<T>(bool clone, T obj, Func<string, string> f, params string[] names) where T : PersistentDataObject;
        string Format(string input);
    }
}
