using System;
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
        /// <summary>
        /// Field to store data information to serialize
        /// </summary>
        private Dictionary<string, dynamic> dict;


        /// <summary>
        /// Default constructor
        /// </summary>
        protected PersistentDataObject()
        {
            this.dict = new Dictionary<string, dynamic>();
        }

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
                        foreach (string s in content.ToTabularString(depth + 1))
                        {
                            string tabs = "\t";
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
        /// Copy this into a new object
        /// </summary>
        /// <param name="t">destination</param>
        /// <returns></returns>
        public T Copy<T>() where T : PersistentDataObject, new()
        {
            T x = new T();
            foreach (string s in this.Keys)
            {
                x.Set(s, this.Data[s]);
            }
            return x;
        }

        /// <summary>
        /// Copy this into a new object
        /// </summary>
        /// <param name="t">destination</param>
        /// <returns></returns>
        public T Copy<T>(params string[] names) where T : PersistentDataObject, new()
        {
            T x = new T();
            foreach (string s in names)
            {
                if (this.Exists(s))
                    x.Set(s, this.Data[s]);
            }
            return x;
        }

        /// <summary>
        /// Copy this into an existing object
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="obj">object</param>
        public void Copy<T>(T obj) where T : PersistentDataObject
        {
            foreach (string s in this.Keys)
            {
                obj.Set(s, this.Data[s]);
            }
        }

        /// <summary>
        /// Copy this into an existing object
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="obj">object</param>
        /// <param name="names">name list</param>
        public void Copy<T>(T obj, params string[] names) where T : PersistentDataObject
        {
            foreach (string s in names)
            {
                if (this.Exists(s))
                    obj.Set(s, this.Data[s]);
            }
        }

        /// <summary>
        /// Creates all keys that does not exists into obj
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="obj">existing object</param>
        /// <param name="f">mapping function</param>
        public void Mapping<T>(T obj, Func<string, string> f) where T : PersistentDataObject
        {
            foreach (string s in this.Keys)
            {
                if (!obj.Exists(f(s)))
                    obj.Set(f(s), this.Data[s]);
            }
        }

        /// <summary>
        /// Creates selected keys that does not exists into obj
        /// </summary>
        /// <typeparam name="T">a PersistentDataObject</typeparam>
        /// <param name="obj">existing object</param>
        /// <param name="f">mapping fonctions</param>
        /// <param name="names">name list</param>
        public void Mapping<T>(T obj, Func<string, string> f, params string[] names) where T : PersistentDataObject
        {
            foreach (string s in names)
            {
                if (!obj.Exists(f(s)) && this.Exists(s))
                    obj.Set(f(s), this.Data[s]);
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
    }
}
