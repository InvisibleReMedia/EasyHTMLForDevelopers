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
    /// 
    /// Some data are IMarshalling
    /// and others are not
    /// 
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
        /// Export this data into an another
        /// </summary>
        /// <param name="title">title</param>
        /// <returns>an another data model</returns>
        public virtual IMarshalling Export(string title = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export by a specific function
        /// and a destination object
        /// </summary>
        /// <typeparam name="T">destination type</typeparam>
        /// <param name="destination"></param>
        /// <param name="f">function</param>
        /// <returns>destination</returns>
        public T Export<F,T>(T destination, Func<F,T> f) where T : PersistentDataObject where F : PersistentDataObject
        {
            T t = f(this as F);
            t.Copy(true, destination);
            return destination;
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
        /// Export to marshalling hash
        /// </summary>
        /// <returns>marshalling hash</returns>
        public MarshallingHash ExportToHash()
        {
            MarshallingHash hash = new MarshallingHash(this.Get("name"));
            this.Copy(false, hash);
            return hash;
        }

        /// <summary>
        /// Extract a name from a source
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="name">name to find</param>
        /// <returns>null if not exists</returns>
        public dynamic Extract(IMarshalling source, string name)
        {
            if (source is MarshallingHash)
                return source.Values.First(x => x.Name == name);
            else if (source is MarshallingList)
                return source.Values.ElementAt(Convert.ToInt32(name));
            else
                return source.Value;
        }

        /// <summary>
        /// Select a specific element in tree
        /// </summary>
        /// <param name="sequence">tree sequence</param>
        /// <returns>resulting object</returns>
        public IMarshalling Extract(string name, params string[] sequence)
        {
            var content = this as IMarshalling;
            if (content != null && content.Name == name)
            {
                int index = 0;
                do
                {
                    string s = sequence[index];
                    dynamic newContent = null;
                    if (content is MarshallingList)
                    {
                        newContent = this.Extract(content, s);
                    }
                    else if (content is MarshallingValue)
                    {
                        return content;
                    }
                    else
                    {
                        newContent = this.Extract(content, s);
                    }
                    if (newContent != null)
                    {
                        content = newContent;
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} does not exist", s));
                    }
                    ++index;
                } while (index < sequence.Count());
                return content;
            }
            else
                throw new ArgumentException(String.Format("{0} does not exist", name));
        }

        /// <summary>
        /// Import an object
        /// </summary>
        /// <param name="hash">from hash</param>
        /// <returns>master object</returns>
        public static T Import<T>(MarshallingHash hash) where T : PersistentDataObject, new()
        {
            T t = new T();
            t.Set("name", hash.Name);
            hash.Copy(false, t);
            return t;
        }

        /// <summary>
        /// Get list model
        /// </summary>
        /// <typeparam name="T">destination object</typeparam>
        /// <param name="name">name to test</param>
        /// <returns>enumerable of T</returns>
        public IEnumerable<T> TransformList<T>(string name) where T : PersistentDataObject
        {
            if (this.Exists(name))
            {
                return (this.Get(name) as IMarshalling).Conversion<T>();
            }
            else
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Conversion to elements
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <returns>enumeration of T</returns>
        public IEnumerable<T> Conversion<T>() where T : class
        {
            return (from x in this.Data.ToList() where x.Value is IMarshalling && x.Key != "name" select x.Value as T);
        }

        /// <summary>
        /// Copy this into a new object
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="clone">switch for cloning</param>
        /// <returns>new object</returns>
        public T Copy<T>(bool clone) where T : PersistentDataObject, new()
        {
            return Copy<T>(clone, this.Keys.ToArray());
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
            for (int index = 0; index < names.Count(); ++index)
            {
                string s = names.ElementAt(index);
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
            Copy(clone, obj, this.Keys.ToArray());
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
            for(int index = 0; index < names.Count(); ++index)
            {
                string s = names.ElementAt(index);
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
            Mapping(clone, obj, f, this.Keys.ToArray());
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
        /// Conversion of an object as a such type
        /// </summary>
        /// <typeparam name="T">destination object type</typeparam>
        /// <param name="name">name</param>
        /// <param name="t">type of input</param>
        /// <param name="obj">destination object</param>
        /// <param name="input">input object</param>
        /// <param name="transform">transform function</param>
        /// <returns>new content</returns>
        public void Conversion(string name, Type t, IMarshalling obj, IMarshalling input, Func<MarshallingType, IMarshalling, IMarshalling, IMarshalling> transform)
        {

            Dictionary<Type, Action<IMarshalling>> SWITCH = new Dictionary<Type, Action<IMarshalling>>()
            {
                {
                    typeof(MarshallingBoolValue), x => {
                        obj.Set(name, transform(MarshallingType.VALUE, obj, x));
                    }
                },
                {
                    typeof(MarshallingDoubleValue), x => {
                        obj.Set(name, transform(MarshallingType.VALUE, obj, x));
                    }
                },
                {
                    typeof(MarshallingHash), x => {
                        IMarshalling subContent = transform(MarshallingType.HASH, obj, x);
                        obj.Set(name, subContent);
                        x.Mapping(subContent, transform);
                    }
                },
                {
                    typeof(MarshallingEnumerationValue), x => {
                        obj.Set(name, transform(MarshallingType.VALUE, obj, x));
                    }
                },
                {
                    typeof(MarshallingIntValue), x => {
                        obj.Set(name, transform(MarshallingType.VALUE, obj, x));
                    }
                },
                {
                    typeof(MarshallingList), x => {
                        IMarshalling subContent = transform(MarshallingType.LIST, obj, x);
                        obj.Set(name, subContent);
                        x.Mapping(subContent, transform);
                    }
                },
                {
                    typeof(MarshallingObjectValue), x => {
                        obj.Set(name, transform(MarshallingType.VALUE, obj, x));
                    }
                },
                {
                    typeof(MarshallingRegexValue), x => {
                        obj.Set(name, transform(MarshallingType.VALUE, obj, x));
                    }
                }
            };
            bool found = false;
            foreach (KeyValuePair<Type, Action<IMarshalling>> kv in SWITCH)
            {
                if (kv.Key.IsEquivalentTo(input.GetType())) {
                    found = true;
                    kv.Value(input);
                }
            }
            if (!found)
                throw new KeyNotFoundException(String.Format("Type {0} not found", input.GetType().Name));
        }

        /// <summary>
        /// Mapping in all tree
        /// </summary>
        /// <typeparam name="T">destination type</typeparam>
        /// <param name="obj">destination</param>
        /// <param name="map">transform function</param>
        public void Mapping(IMarshalling obj, Func<MarshallingType, IMarshalling, IMarshalling, IMarshalling> map)
        {
            Mapping(obj, map, (this as IMarshalling).HashKeys.ToArray());
        }

        /// <summary>
        /// Mapping in all tree
        /// </summary>
        /// <typeparam name="T">destination type</typeparam>
        /// <param name="obj">destination</param>
        /// <param name="map">transform function</param>
        /// <param name="names">selected names</param>
        public void Mapping(IMarshalling obj, Func<MarshallingType, IMarshalling, IMarshalling, IMarshalling> map, params string[] names)
        {
            foreach (string s in names)
            {
                var content = this.Data[s];
                Conversion(s, content.GetType(), obj, content, map);
            }
        }

        /// <summary>
        /// Format with replacement content
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns></returns>
        public virtual string Format(string input)
        {
            string output = string.Empty;
            Regex r = new Regex(@"%([a-zA-Z_0-9]+)\s|([^%]*)", RegexOptions.Multiline);
            foreach (Match m in r.Matches(input))
            {
                if (m.Groups[1].Success)
                {
                    if (this.Exists(m.Groups[1].Value))
                    {
                        var content = this.Get(m.Groups[1].Value);
                        if (content is IMarshalling)
                        {
                            output += content.Format(content.Formatting);
                        }
                        else
                        {
                            output += content.ToString();
                        }
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
        /// Get value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="name">name</param>
        /// <param name="zero">value returned if null</param>
        /// <returns>value</returns>
        public T GetValue<T>(string name, T zero)
        {
            if (this.Exists(name))
            {
                return this.Get(name).Value;
            }
            else
            {
                return zero;
            }
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
