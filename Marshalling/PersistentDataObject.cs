using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;


namespace Marshalling
{
    /// <summary>
    /// Classe contenant la séquence
    /// de sérialisation/déserialisation
    /// </summary>
    [Serializable]
    public class PersistentDataObject
    {

        #region Fields

        /// <summary>
        /// Field to store data information to serialize
        /// </summary>
        private Dictionary<string, dynamic> dict;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected PersistentDataObject()
        {
            this.dict = new Dictionary<string, dynamic>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Data to be used
        /// </summary>
        protected Dictionary<string, dynamic> Data
        {
            get
            {
                return this.dict;
            }
        }

        /// <summary>
        /// Gets all direct keys
        /// </summary>
        public IEnumerable<string> Keys
        {
            get
            {
                return this.Data.Keys;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all property names
        /// </summary>
        /// <returns>properties</returns>
        public virtual string[] GetProperties()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a property
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>value</returns>
        public virtual IMarshalling GetProperty(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Test if a key exists
        /// </summary>
        /// <param name="name">key name</param>
        /// <returns>true if exists</returns>
        public bool Exists(string name)
        {
            return this.Data.ContainsKey(name);
        }

        /// <summary>
        /// Set a value if exists
        /// </summary>
        /// <param name="name">value name</param>
        /// <param name="a">function</param>
        /// <returns>true if succeeded</returns>
        public bool Get(string name, Action<string, IMarshalling> a)
        {
            if (this.Exists(name))
            {
                a(name, this.Get(name));
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Sets a value into the dictionary
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <param name="value">value of field</param>
        public void Set(string name, dynamic value)
        {
            if (this.Data.ContainsKey(name))
            {
                this.Data[name] = value;
            }
            else
            {
                this.Data.Add(name, value);
            }
        }

        /// <summary>
        /// Gets a value from the dictionary
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <param name="init">default value</param>
        /// <returns>value</returns>
        public dynamic Get(string name, dynamic init = null)
        {
            if (!this.Data.ContainsKey(name))
            {
                this.Data.Add(name, init);
            }
            return this.Data[name];
        }

        /// <summary>
        /// Remove a key
        /// </summary>
        /// <param name="name">key name</param>
        public void Remove(string name)
        {
            this.Data.Remove(name);
        }

        /// <summary>
        /// Converts all to string
        /// </summary>
        /// <returns>string tabular</returns>
        public virtual IEnumerable<string> ToTabularString(uint depth)
        {
            foreach (string key in this.dict.Keys)
            {
                var content = this.dict[key];
                if (content != null)
                {
                    if (content is IMarshalling)
                    {
                        string tabs = "\t";
                        foreach (string s in content.ToTabularString(depth + 1))
                        {
                            if (s.StartsWith("name:"))
                                yield return tabs + s.Trim('\r','\n') + " (" + content.GetType().Name + ")" + Environment.NewLine;
                            else
                                yield return tabs + s.Trim('\r', '\n') + Environment.NewLine;
                        }
                    }
                    else
                    {
                        yield return key + ":" + content.ToString() + Environment.NewLine;
                    }
                }
                else
                {
                    depth = 0;
                    yield return key + ":" + "null" + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// Converts all to string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string output = string.Empty;
            uint d;
            foreach (string s in this.ToTabularString(0))
            {
                output += s.Trim('\n', '\r') + Environment.NewLine;
            }
            return output;
        }

        /// <summary>
        /// Implements bindings
        /// </summary>
        /// <param name="m">object to bind</param>
        public virtual void Bind(IMarshalling m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Conversion to elements
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <returns>enumeration of T</returns>
        public IEnumerable<T> Conversion<T>() where T : class
        {
            return (from x in this.Data.ToList() where x.Key != "name" select x.Value as T);
        }

        /// <summary>
        /// Copy this into a new object
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="clone">switch for cloning</param>
        /// <returns>new object</returns>
        public T Copy<T>(bool clone) where T : PersistentDataObject, new()
        {
            T x = new T();
            foreach (string s in this.Keys)
            {
                var content = this.Data[s];
                if (clone && content is ICloneable)
                    x.Set(s, content.Clone());
                else
                    x.Set(s, content);
            }
            return x;
        }


        /// <summary>
        /// Copy this into a new object
        /// select what you copy
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="clone">switch for cloning</param>
        /// <param name="names">list of wanted copy</param>
        /// <returns></returns>
        public T Copy<T>(bool clone, params string[] names) where T : PersistentDataObject, new()
        {
            T x = new T();
            foreach (string s in names)
            {
                var content = this.Data[s];
                if (this.Exists(s))
                    if (clone && content is ICloneable)
                        x.Set(s, content.Clone());
                    else
                        x.Set(s, content);
            }
            return x;
        }

        /// <summary>
        /// Copy this into an existing object
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="clone">switch for cloning</param>
        /// <param name="obj">object</param>
        public void Copy<T>(bool clone, T obj) where T : PersistentDataObject
        {
            foreach (string s in this.Keys)
            {
                var content = this.Data[s];
                if (clone && content is ICloneable)
                    obj.Set(s, content.Clone());
                else
                    obj.Set(s, content);
            }
        }

        /// <summary>
        /// Copy this into an existing object
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="clone">switch for cloning</param>
        /// <param name="obj">object</param>
        /// <param name="names">name list</param>
        public void Copy<T>(bool clone, T obj, params string[] names) where T : PersistentDataObject
        {
            foreach (string s in names)
            {
                var content = this.Data[s];
                if (clone && content is ICloneable)
                    obj.Set(s, content.Clone());
                else
                    obj.Set(s, content);
            }
        }

        /// <summary>
        /// Creates all keys that does not exists into obj
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="clone">switch for cloning</param>
        /// <param name="obj">existing object</param>
        /// <param name="f">mapping function</param>
        public void Mapping<T>(bool clone, T obj, Func<string, string> f) where T : PersistentDataObject
        {
            foreach (string s in this.Keys)
            {
                var content = this.Data[s];
                if (!obj.Exists(f(s)))
                    if (clone && content is ICloneable)
                        obj.Set(f(s), content.Clone());
                    else
                    obj.Set(f(s), content);
            }
        }

        /// <summary>
        /// Creates selected keys that does not exists into obj
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="clone">switch for cloning</param>
        /// <param name="obj">existing object</param>
        /// <param name="f">mapping fonctions</param>
        /// <param name="names">name list</param>
        public void Mapping<T>(bool clone, T obj, Func<string, string> f, params string[] names) where T : PersistentDataObject
        {
            foreach (string s in names)
            {
                var content = this.Data[s];
                if (!obj.Exists(f(s)))
                    if (clone && content is ICloneable)
                        obj.Set(f(s), content.Clone());
                    else
                        obj.Set(f(s), content);
            }
        }

        /// <summary>
        /// Format with replacement content
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns></returns>
        public string Format(string input)
        {
            string output = string.Empty;
            Regex r = new Regex(@"%([a-zA-Z_0-9]+)|([^%]*)", RegexOptions.Multiline);
            foreach (Match m in r.Matches(input))
            {
                if (m.Groups[1].Success)
                {
                    if (this.Exists(m.Groups[1].Value))
                    {
                        output += this.Get(m.Groups[1].Value).Value;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else if (m.Groups[2].Success)
                {
                    output += m.Groups[2].Value;
                }
            }
            return output;
        }

        /// <summary>
        /// Lecture d'un document
        /// </summary>
        /// <param name="file">information de fichier</param>
        /// <param name="result">objet obtenu</param>
        /// <returns>true if success</returns>
        public static bool Load(FileInfo file, out PersistentDataObject result)
        {
            result = null;
            if (file.Exists)
            {
                PersistentDataObject p;
                BinaryFormatter bf = new BinaryFormatter();
                Object o;

                try
                {
                    using(Stream s = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        o = bf.Deserialize(s);
                        s.Close();
                    }

                }
                catch (SerializationException)
                {
                    return false;
                }

                if (o != null)
                {
                    p = o as PersistentDataObject;
                    if (p != null)
                    {
                        result = p;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sauvegarde d'un document
        /// </summary>
        /// <param name="file">information de fichier</param>
        /// <param name="data">objet à sérialiser</param>
        /// <param name="errorReason">error reason</param>
        /// <returns>true if success</returns>
        public static bool Save(FileInfo file, PersistentDataObject data, out string errorReason)
        {
            using(FileStream f = new FileStream(file.FullName, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();

                try
                {
                    bf.Serialize(f, data);
                    f.Close();
                    errorReason = string.Empty;
                    return true;
                }
                catch (SerializationException e)
                {
                    errorReason = e.Message;
                    return false;
                }

            }
        }

        #endregion

    }
}
