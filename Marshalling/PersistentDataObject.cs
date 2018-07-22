using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


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
