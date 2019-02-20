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
        /// Add a new hash into this
        /// </summary>
        /// <param name="f">function as elements</param>
        void Add(Func<IDictionary<string, dynamic>> f);
        /// <summary>
        /// Add a new list into this
        /// </summary>
        /// <param name="f">function as elements</param>
        void Add(Func<IEnumerable<dynamic>> f);
        /// <summary>
        /// Set value
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        void Set(string name, dynamic value);
        /// <summary>
        /// Set a value if exists
        /// </summary>
        /// <param name="name">value name</param>
        /// <param name="a">function</param>
        /// <returns>true if succeeded</returns>
        bool Get(string name, Action<string, IMarshalling> a);
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
        /// <summary>
        /// Conversion to elements
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <returns>enumeration of T</returns>
        IEnumerable<T> Conversion<T>() where T : class;
        /// <summary>
        /// Create a new object and copy this into
        /// </summary>
        /// <typeparam name="T">new object</typeparam>
        /// <param name="clone">switch to clone</param>
        /// <returns>new object</returns>
        T Copy<T>(bool clone) where T : PersistentDataObject, new();
        /// <summary>
        /// Create a new object and copy selected values into
        /// </summary>
        /// <typeparam name="T">new object</typeparam>
        /// <param name="clone">switch to clone</param>
        /// <param name="names">selected names</param>
        /// <returns>new object</returns>
        T Copy<T>(bool clone, params string[] names) where T : PersistentDataObject, new();
        /// <summary>
        /// Copy this into an object
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="clone">switch to clone</param>
        /// <param name="obj">object where to copy</param>
        void Copy<T>(bool clone, T obj) where T : PersistentDataObject;
        /// <summary>
        /// Copy this by selected names into an object
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="clone">switch to clone</param>
        /// <param name="obj">object where to copy</param>
        /// <param name="names">selected names</param>
        void Copy<T>(bool clone, T obj, params string[] names) where T : PersistentDataObject;
        /// <summary>
        /// Make a mapping between all names by other names into an existing object
        /// </summary>
        /// <typeparam name="T">existing object</typeparam>
        /// <param name="clone">switch to clone</param>
        /// <param name="obj">object where to copy</param>
        /// <param name="f">function to transfer names</param>
        void Mapping<T>(bool clone, T obj, Func<string, string> f) where T : PersistentDataObject;
        /// <summary>
        /// Make a mapping between selected names by other names into an existing object
        /// </summary>
        /// <typeparam name="T">existing object</typeparam>
        /// <param name="clone">switch to clone</param>
        /// <param name="obj">object where to copy</param>
        /// <param name="f">function to transfer names</param>
        /// <param name="names">selected names</param>
        void Mapping<T>(bool clone, T obj, Func<string, string> f, params string[] names) where T : PersistentDataObject;
        /// <summary>
        /// Formatting a string input that contains a name preceding a %
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>formatted text</returns>
        string Format(string input);
        /// <summary>
        /// Export function
        /// </summary>
        /// <param name="title">title of export</param>
        /// <returns>new data model</returns>
        IMarshalling Export(string title);
        /// <summary>
        /// Export to this object
        /// </summary>
        /// <typeparam name="T">object destination</typeparam>
        /// <param name="destination"></param>
        /// <param name="f">adapted function</param>
        /// <returns>new object</returns>
        T Export<F, T>(T destination, Func<F, T> f)
            where T : PersistentDataObject
            where F : PersistentDataObject;
        /// <summary>
        /// Get list model
        /// </summary>
        /// <typeparam name="T">destination object</typeparam>
        /// <param name="name">name to test</param>
        /// <returns>enumerable of T</returns>
        IEnumerable<T> TransformList<T>(string name) where T : Marshalling.PersistentDataObject;
    }
}
