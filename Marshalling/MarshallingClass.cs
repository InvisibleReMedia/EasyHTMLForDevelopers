using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marshalling
{

    /// <summary>
    /// Marshall content type
    /// </summary>
    public enum MarshallingType
    {
        VALUE,
        LIST,
        HASH
    }

    /// <summary>
    /// Marshalling base class
    /// </summary>
    public abstract class MarshallingClass : PersistentDataObject, IMarshalling
    {

        #region Fields

        /// <summary>
        /// Index name for name
        /// </summary>
        protected static readonly string nameName = "name";
        /// <summary>
        /// Index name for value
        /// </summary>
        protected static readonly string valueName = "value";
        /// <summary>
        /// Index name for list
        /// </summary>
        protected static readonly string listName = "list";
        /// <summary>
        /// Index name for hash
        /// </summary>
        protected static readonly string hashName = "hash";
        /// <summary>
        /// Index name for value
        /// </summary>
        protected static readonly string formattingName = "formatting";
        /// <summary>
        /// Index name for validation rule
        /// </summary>
        protected static readonly string validationRuleName = "validationRule";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor wih an empty value
        /// </summary>
        /// <param name="name">name of value</param>
        protected MarshallingClass(string name)
        {
            this.Set(nameName, name);
            this.Set(valueName, "");
            this.Set(formattingName, "");
        }

        /// <summary>
        /// Constructor for a dynamic value
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in string</param>
        protected MarshallingClass(string name, dynamic value)
        {
            this.Set(nameName, name);
            this.Set(valueName, value);
            this.Set(formattingName, "");
        }

        /// <summary>
        /// Constructor for a dynamic value
        /// with formatting
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in string</param>
        /// <param name="format">formatting</param>
        protected MarshallingClass(string name, dynamic value, string format)
        {
            this.Set(nameName, name);
            this.Set(valueName, value);
            this.Set(formattingName, format);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of this value
        /// </summary>
        public string Name
        {
            get
            {
                return this.Get(nameName);
            }
            set
            {
                this.Set(nameName, value);
            }
        }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public virtual dynamic Value
        {
            get
            {
                return this.Get(valueName);
            }
            set
            {
                if (this.IsValid(value))
                {
                    this.Set(valueName, value);
                }
                else
                {
                    throw new InvalidCastException(String.Format("The value '{0}' of '{1}' doesn't match the validation rule", value.ToString(), this.Name));
                }
            }
        }

        /// <summary>
        /// Gets values
        /// </summary>
        public virtual List<IMarshalling> Values
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets values
        /// </summary>
        public virtual Dictionary<string, IMarshalling> Entries
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets formatting
        /// </summary>
        public string Formatting
        {
            get
            {
                return this.Get("Formatting", string.Empty);
            }
            set
            {
                this.Set("Formatting", value);
            }
        }

        /// <summary>
        /// Gets count (of Marshalling objects)
        /// </summary>
        public virtual int Count
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets HashKeys
        /// </summary>
        public virtual IEnumerable<string> HashKeys
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets value in index
        /// </summary>
        /// <param name="index">index position</param>
        /// <returns>value</returns>
        public virtual IMarshalling this[int index]
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets value key
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns>value</returns>
        public virtual IMarshalling this[string key]
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected abstract bool IsValid(dynamic value);

        /// <summary>
        /// Iterate all elements
        /// </summary>
        /// <param name="a">function</param>
        public virtual void Iterate(Action<int, IMarshalling> a)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Iterate all elements
        /// </summary>
        /// <param name="a">function</param>
        public virtual void Iterate(Action<string, IMarshalling> a)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Add a new element into hash
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="d">element</param>
        public virtual void Add(string name, dynamic d)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Add a new element into hash
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="d">element</param>
        public virtual void Add(string name, IMarshalling d)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Add a new element into list
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="d">element</param>
        public virtual void Add(IMarshalling d)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Export to hash
        /// </summary>
        /// <returns>hash</returns>
        public MarshallingHash ExportToHash()
        {
            MarshallingHash hash = new MarshallingHash(this.Name, this.Entries);
            return hash;
        }

        /// <summary>
        /// Export to list
        /// </summary>
        /// <returns>list</returns>
        public MarshallingList ExportToList()
        {
            MarshallingList list = new MarshallingList(this.Name, this.Values);
            return list;
        }

        /// <summary>
        /// Get hash model
        /// </summary>
        /// <typeparam name="T">destination object</typeparam>
        /// <param name="name">name to test</param>
        /// <returns>hash of T</returns>
        public T TransformHash<T>(string name) where T : IMarshalling, new()
        {
            if (this.Name == name)
            {
                T t = new T();
                t.Name = name;
                foreach(KeyValuePair<string, IMarshalling> kv in this.Entries)
                {
                    t.Add(kv.Key, kv.Value);
                }
                return t;
            }
            else if (this.Exists(name))
            {
                dynamic value = this.Get(name);
                if (value is MarshallingHash)
                {
                    T t = new T();
                    t.Name = name;
                    foreach (KeyValuePair<string, IMarshalling> kv in this.Entries)
                    {
                        t.Add(kv.Key, kv.Value);
                    }
                    return t;
                }
                else
                {
                    throw new ArgumentException(String.Format("Key {0} is not MarshallingHash", name));
                }
            }
            else
            {
                return new T();
            }
        }

        /// <summary>
        /// Get list model
        /// </summary>
        /// <typeparam name="T">destination object</typeparam>
        /// <param name="name">name to test</param>
        /// <returns>enumerable of T</returns>
        public T TransformList<T>(string name) where T : IMarshalling, new()
        {
            if (this.Name == name)
            {
                T t = new T();
                t.Name = name;
                foreach (IMarshalling m in this.Values)
                {
                    t.Add(m);
                }
                return t;
            }
            else if (this.Exists(name))
            {
                dynamic value = this.Get(name);
                if (value is MarshallingList)
                {
                    T t = new T();
                    t.Name = name;
                    foreach (IMarshalling m in this.Values)
                    {
                        t.Add(m);
                    }
                    return t;
                }
                else
                {
                    throw new ArgumentException(String.Format("Key {0} is not MarshallingList", name));
                }
            }
            else
            {
                return new T();
            }
        }

        /// <summary>
        /// Conversion to elements list
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <returns>enumeration of T</returns>
        public IEnumerable<T> ConversionToList<T>() where T : class, IMarshalling
        {
            return this.Values.Cast<T>();
        }

        /// <summary>
        /// Conversion to elements list
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <returns>enumeration of T</returns>
        public Dictionary<string, T> ConversionToHash<T>() where T : class, IMarshalling
        {
            return (from x in this.Entries select new KeyValuePair<string, T>(x.Key, x.Value as T)).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Conversion to elements list
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <param name="name">name</param>
        /// <returns>enumeration of T</returns>
        public IEnumerable<T> ConversionToList<T>(string name) where T : class, IMarshalling
        {
            return this.Extract(this, name).ConversionToList<T>();
        }

        /// <summary>
        /// Conversion to elements list
        /// </summary>
        /// <typeparam name="T">type of elements</typeparam>
        /// <param name="name">name</param>
        /// <returns>enumeration of T</returns>
        public Dictionary<string, T> ConversionToHash<T>(string name) where T : class, IMarshalling
        {
            return this.Extract(this, name).ConversionToHash<T>();
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
                if (kv.Key.IsEquivalentTo(input.GetType()))
                {
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
            if (this.Name == names[0])
            {
                this.Mapping(obj, map, names.Skip(1).ToArray());
            }
            else
            {
                foreach (string s in names)
                {
                    var content = this.Data[s];
                    Conversion(s, content.GetType(), obj, content, map);
                }
            }
        }

        /// <summary>
        /// Get value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="name">name</param>
        /// <param name="zero">value returned if null</param>
        /// <returns>value</returns>
        public virtual T GetValue<T>(string name, T zero)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Set value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="name">name</param>
        /// <param name="zero">value returned if null</param>
        /// <returns>value</returns>
        public virtual void SetValue<T>(string name, T value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Extract a name from a source
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="name">name to find</param>
        /// <returns>null if not exists</returns>
        public dynamic Extract(IMarshalling source, string name)
        {
            if (source.Name == name)
                return source;
            else if (source is MarshallingHash)
                return source.Entries.First(x => x.Key == name);
            else if (source is MarshallingList)
                return source.Values.First(x => x.Name == name);
            else
                return source.Value;
        }

        /// <summary>
        /// Select a specific element in tree
        /// </summary>
        /// <param name="sequence">tree sequence</param>
        /// <returns>resulting object</returns>
        public IMarshalling Extract(IMarshalling source, string name, params string[] sequence)
        {
            dynamic content = Extract(source, name);
            foreach (string s in sequence)
            {
                dynamic subContent = Extract(content, name);
                if (subContent != null)
                {
                    content = subContent;
                }
                else
                    new ArgumentException(String.Format("Key {0} is not found", s));
            }
            return content;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Import an object
        /// </summary>
        /// <param name="hash">from hash</param>
        /// <returns>master object</returns>
        public static T Import<T>(MarshallingHash hash) where T : MarshallingHash, new()
        {
            T t = new T();
            t.Set("name", hash.Name);
            hash.Copy(false, t);
            return t;
        }

        /// <summary>
        /// Import an object
        /// </summary>
        /// <param name="hash">from hash</param>
        /// <returns>master object</returns>
        public static T Import<T>(MarshallingList list) where T : MarshallingList, new()
        {
            T t = new T();
            t.Set("name", list.Name);
            list.Copy(false, t);
            return t;
        }

        /// <summary>
        /// Create marshalling
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        /// <returns>marshalling</returns>
        public static MarshallingClass CreateMarshalling(string name, dynamic value)
        {
            if (value is bool)
            {
                return new MarshallingBoolValue(name, value);
            }
            else if (value is int)
            {
                return new MarshallingIntValue(name, value);
            }
            else if (value is double)
            {
                return new MarshallingDoubleValue(name, value);
            }
            else if (value is string)
            {
                return new MarshallingRegexValue(name, value);
            }
            else if (value is Dictionary<string, IMarshalling>)
            {
                return new MarshallingHash(name, value);
            }
            else if (value is IEnumerable<IMarshalling>)
            {
                return new MarshallingList(name, value);
            }
            else
            {
                return new MarshallingObjectValue(name, value);
            }
        }

        #endregion

    }
}
