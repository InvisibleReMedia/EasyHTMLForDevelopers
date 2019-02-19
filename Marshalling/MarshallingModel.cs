using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Marshalling
{

    /// <summary>
    /// Class to value
    /// </summary>
    public abstract class MarshallingValue : PersistentDataObject, IMarshalling
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
        /// Index name for validation rule
        /// </summary>
        protected static readonly string validationRuleName = "validationRule";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor wih an empty value
        /// </summary>
        /// <param name="name">name of value</param>
        public MarshallingValue(string name)
        {
            this.Set(nameName, name);
            this.Set(valueName, "");
        }

        /// <summary>
        /// Constructor for a dynamic value
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in string</param>
        public MarshallingValue(string name, dynamic value)
        {
            this.Set(nameName, name);
            this.Set(valueName, value);
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
        public dynamic Value
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
        /// Gets or sets the value
        /// </summary>
        public IEnumerable<IMarshalling> Values
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new element into hash
        /// </summary>
        /// <param name="e">new element</param>
        public void Add(Func<IDictionary<string, dynamic>> f)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Add a new list into hash
        /// </summary>
        /// <param name="e">new element</param>
        public void Add(Func<IEnumerable<dynamic>> f)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets an enumerator
        /// </summary>
        /// <returns>enumerator</returns>
        public IEnumerator<IMarshalling> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets all property names
        /// </summary>
        /// <returns>properties</returns>
        public override string[] GetProperties()
        {
            return (from x in this.Data.ToList() where x.Value is IMarshalling select (x.Value as IMarshalling).Name).ToArray();
        }

        /// <summary>
        /// Gets a named property
        /// Shows all valid properties
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>property value</returns>
        public override IMarshalling GetProperty(string name)
        {
            string[] names = this.GetProperties();
            if (names.Contains(name))
            {
                if (this.Exists(name))
                    return this.Get(name);
                else
                    throw new ArgumentException(String.Format("Key '{0}' not found", name));
            }
            else
                throw new ArgumentException(String.Format("Key '{0}' does not exist", name));
        }

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected abstract bool IsValid(dynamic value);

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create marshalling
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static IMarshalling CreateMarshalling(Func<KeyValuePair<string, dynamic>> f)
        {
            var kv = f();
            if (kv.Value is bool)
            {
                return new MarshallingBoolValue(kv.Key, kv.Value);
            }
            else if (kv.Value is string)
            {
                return new MarshallingRegexValue(kv.Key, kv.Value, "^.*$");
            }
            else if (kv.Value is long || kv.Value is int || kv.Value is uint)
            {
                return new MarshallingIntValue(kv.Key, Convert.ToInt32(kv.Value));
            }
            else if (kv.Value is double || kv.Value is float)
            {
                return new MarshallingDoubleValue(kv.Key, kv.Value);
            }
            else if (kv.Value is IMarshalling)
            {
                return kv.Value;
            }
            else if (kv.Value is IEnumerable<dynamic>)
            {
                return new MarshallingList(kv.Key, kv.Value as IEnumerable<dynamic>);
            }
            else if (kv.Value is IDictionary<string, dynamic>)
            {
                return new MarshallingHash(kv.Key, kv.Value as IDictionary<string, dynamic>);
            }
            else
            {
                return new MarshallingObjectValue(kv.Key, kv.Value);
            }
        }

        #endregion

    }

    /// <summary>
    /// Class to enumeration string
    /// </summary>
    public class MarshallingRegexValue : MarshallingValue, IMarshalling
    {

        #region Constructor

        /// <summary>
        /// Constructor for a string value
        /// validation : regex pattern
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in string</param>
        /// <param name="regexPattern">regular expression pattern</param>
        public MarshallingRegexValue(string name, string value, string regexPattern)
            : base(name, value)
        {
            this.Set(validationRuleName, regexPattern);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets enumerations values
        /// </summary>
        protected string RegexPattern
        {
            get
            {
                return this.Get(validationRuleName);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected override bool IsValid(dynamic value)
        {
            Regex reg = new Regex(this.RegexPattern);
            MatchCollection mc = reg.Matches(value);
            bool result = true;
            foreach (Match m in mc)
            {
                result &= m.Success;
            }
            return result;
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public new object Clone()
        {
            return new MarshallingRegexValue(this.Name, this.Value, this.RegexPattern);
        }

        #endregion


    }

    /// <summary>
    /// Class to enumeration string
    /// </summary>
    public class MarshallingEnumerationValue : MarshallingValue, IMarshalling
    {

        #region Constructor

        /// <summary>
        /// Constructor for a string value
        /// validation : enumeration of values
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in string</param>
        /// <param name="enums">enumeration of valid values</param>
        public MarshallingEnumerationValue(string name, string value, params string[] enums)
            : base(name, value)
        {
            this.Set(validationRuleName, enums.ToList());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets enumerations values
        /// </summary>
        protected List<string> EnumerationValues
        {
            get
            {
                return this.Get(validationRuleName);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected override bool IsValid(dynamic value)
        {
            return this.EnumerationValues.Contains(value);
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public new object Clone()
        {
            return new MarshallingEnumerationValue(this.Name, this.Value, this.EnumerationValues.ToArray());
        }

        #endregion

    }

    /// <summary>
    /// Class to marshall bool
    /// </summary>
    public class MarshallingBoolValue : MarshallingValue, IMarshalling
    {

        #region Constructor

        /// <summary>
        /// Constructor for a int value
        /// validation : enumeration of values
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in bool</param>
        public MarshallingBoolValue(string name, bool value)
            : base(name, value)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected override bool IsValid(dynamic value)
        {
            bool val;
            return Boolean.TryParse(value.ToString(), out val);
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public new object Clone()
        {
            return new MarshallingBoolValue(this.Name, this.Value);
        }

        #endregion

    }

    /// <summary>
    /// Class to marshall int
    /// </summary>
    public class MarshallingIntValue : MarshallingValue, IMarshalling
    {

        #region Constructor

        /// <summary>
        /// Constructor for a int value
        /// validation : enumeration of values
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in int</param>
        public MarshallingIntValue(string name, int value)
            : base(name, value)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected override bool IsValid(dynamic value)
        {
            int val;
            return Int32.TryParse(value.ToString(), out val);
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public new object Clone()
        {
            return new MarshallingIntValue(this.Name, this.Value);
        }

        #endregion

    }

    /// <summary>
    /// Class to marshall double
    /// </summary>
    public class MarshallingDoubleValue : MarshallingValue, IMarshalling
    {

        #region Constructor

        /// <summary>
        /// Constructor for a double value
        /// validation : enumeration of values
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in float</param>
        public MarshallingDoubleValue(string name, double value)
            : base(name, value)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected override bool IsValid(dynamic value)
        {
            double val;
            return Double.TryParse(value.ToString(), out val);
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public new object Clone()
        {
            return new MarshallingDoubleValue(this.Name, this.Value);
        }

        #endregion

    }

    /// <summary>
    /// Class to marshall object
    /// </summary>
    public class MarshallingObjectValue : MarshallingValue, IMarshalling
    {

        #region Constructor

        /// <summary>
        /// Constructor for an object
        /// validation : always
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="value">value in object</param>
        public MarshallingObjectValue(string name, object value)
            : base(name, value)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected override bool IsValid(dynamic value)
        {
            return true;
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public new object Clone()
        {
            return new MarshallingObjectValue(this.Name, this.Value);
        }

        #endregion

    }

    /// <summary>
    /// Class to marshall hash list
    /// </summary>
    public class MarshallingHash : PersistentDataObject, IMarshalling
    {

        #region Fields

        /// <summary>
        /// Index name for name
        /// </summary>
        protected static readonly string nameName = "name";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for an empty hash
        /// </summary>
        /// <param name="name">name of value</param>
        public MarshallingHash(string name)
        {
            this.Set(nameName, name);
        }

        /// <summary>
        /// Constructor for a hash
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="elements">elements list</param>
        public MarshallingHash(string name, IEnumerable<IMarshalling> elements) :
                this(name, elements.ToDictionary(x => x.Name, x => x.Value))
        {
        }

        /// <summary>
        /// Construction for a hash
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="keyValues">dictionary</param>
        public MarshallingHash(string name, IDictionary<string, dynamic> keyValues)
        {
            this.Set(nameName, name);
            foreach (KeyValuePair<string, dynamic> kv in keyValues)
            {
                if (kv.Value is bool)
                {
                    MarshallingBoolValue b = new MarshallingBoolValue(kv.Key, kv.Value);
                    this.Set(kv.Key, b);
                }
                else if (kv.Value is string)
                {
                    MarshallingRegexValue r = new MarshallingRegexValue(kv.Key, kv.Value, "^.*$");
                    this.Set(kv.Key, r);
                }
                else if (kv.Value is long || kv.Value is int || kv.Value is uint)
                {
                    MarshallingIntValue i = new MarshallingIntValue(kv.Key, Convert.ToInt32(kv.Value));
                    this.Set(kv.Key, i);
                }
                else if (kv.Value is double || kv.Value is float)
                {
                    MarshallingDoubleValue d = new MarshallingDoubleValue(kv.Key, kv.Value);
                    this.Set(kv.Key, d);
                }
                else if (kv.Value is IMarshalling)
                {
                    this.Set(kv.Key, kv.Value);
                }
                else if (kv.Value is IEnumerable<dynamic>)
                {
                    MarshallingList ml = new MarshallingList(kv.Key, kv.Value as IEnumerable<dynamic>);
                    this.Set(kv.Key, ml);
                }
                else if (kv.Value is IDictionary<string, dynamic>)
                {
                    MarshallingHash mh = new MarshallingHash(kv.Key, kv.Value as IDictionary<string, dynamic>);
                    this.Set(kv.Key, mh);
                }
                else
                {
                    MarshallingObjectValue o = new MarshallingObjectValue(kv.Key, kv.Value);
                    this.Set(kv.Key, o);
                }
            }
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of this value
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
        public dynamic Value
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
        /// Gets all values
        /// </summary>
        public IEnumerable<IMarshalling> Values
        {
            get
            {
                return (from x in this.Data where x.Value is IMarshalling select x.Value as IMarshalling).AsEnumerable();
            }
        }

        /// <summary>
        /// Gets count (of Marshalling objects)
        /// </summary>
        public int Count
        {
            get
            {
                return this.HashKeys.Count();
            }
        }

        /// <summary>
        /// Gets all Marshalling keys
        /// </summary>
        public IEnumerable<string> HashKeys
        {
            get
            {
                return (from x in this.Data where x.Value is IMarshalling select x.Key).AsEnumerable();
            }
        }

        /// <summary>
        /// Gets or sets a value from hash
        /// given the name
        /// </summary>
        /// <param name="key">select by name</param>
        /// <returns>object</returns>
        public IMarshalling this[string key]
        {
            get
            {
                if (this.HashKeys.Contains(key))
                {
                    return this.Get(key);
                }
                else
                {
                    throw new ArgumentException(String.Format("Marshalling key '{0}' not found", key));
                }
            }
            set
            {
                this.Set(key, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an enumerator
        /// </summary>
        /// <returns>enumerator</returns>
        public IEnumerator<IMarshalling> GetEnumerator()
        {
            return this.Values.GetEnumerator();
        }

        /// <summary>
        /// Gets all property names
        /// </summary>
        /// <returns>properties</returns>
        public override string[] GetProperties()
        {
            return this.HashKeys.ToArray();
        }

        /// <summary>
        /// Gets a named property
        /// Shows all valid properties
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>property value</returns>
        public override IMarshalling GetProperty(string name)
        {
            string[] names = this.GetProperties();
            if (names.Contains(name))
            {
                if (this.Exists(name))
                    return this.Get(name);
                else
                    throw new ArgumentException(String.Format("Marshalling key '{0}' not found", name));
            }
            else
                throw new ArgumentException(String.Format("Marshalling key '{0}' does not exist", name));
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public object Clone()
        {
            List<IMarshalling> list = new List<IMarshalling>();
            foreach (IMarshalling a in this.Values)
            {
                list.Add(a.Clone() as IMarshalling);
            }
            return new MarshallingHash(this.Name, list);
        }

        /// <summary>
        /// Add a new element into hash
        /// </summary>
        /// <param name="e">new element</param>
        public void Add(Func<IDictionary<string,dynamic>> f)
        {
            Marshalling.MarshallingHash h = Marshalling.MarshallingHash.CreateMarshalling("", f);
            h.Copy(false, this);
        }

        /// <summary>
        /// Add a new list into hash
        /// </summary>
        /// <param name="e">new element</param>
        public void Add(Func<IEnumerable<dynamic>> f)
        {
            Marshalling.MarshallingList l = Marshalling.MarshallingList.CreateMarshalling("", f);
            l.Copy(false, this);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create marshalling
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static MarshallingHash CreateMarshalling(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new MarshallingHash(name, f());
        }

        #endregion

    }

    /// <summary>
    /// Class to marshall hash list
    /// </summary>
    public class MarshallingList : PersistentDataObject, IMarshalling
    {

        #region Fields

        /// <summary>
        /// Index name for name
        /// </summary>
        protected static readonly string nameName = "name";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for an empty list
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="elements">elements list</param>
        public MarshallingList(string name)
        {
            this.Set(nameName, name);
        }

        /// <summary>
        /// Construct for a list with a list of value
        /// </summary>
        /// <param name="name">name of the value</param>
        /// <param name="keyValues">values</param>
        public MarshallingList(string name, IEnumerable<dynamic> keyValues)
        {
            this.Set(nameName, name);
            for(int index = 0; index < keyValues.Count(); ++index)
            {
                dynamic element = keyValues.ElementAt(index);
                if (element is bool)
                {
                    MarshallingBoolValue b = new MarshallingBoolValue(index.ToString(), element);
                    this.Set(index.ToString(), b);
                }
                else if (element is string)
                {
                    MarshallingRegexValue r = new MarshallingRegexValue(index.ToString(), element, "^.*$");
                    this.Set(index.ToString(), r);
                }
                else if (element is long || element is int || element is uint)
                {
                    MarshallingIntValue i = new MarshallingIntValue(index.ToString(), Convert.ToInt32(element));
                    this.Set(index.ToString(), i);
                }
                else if (element is double)
                {
                    MarshallingDoubleValue d = new MarshallingDoubleValue(index.ToString(), element);
                    this.Set(index.ToString(), d);
                }
                else if (element is IMarshalling)
                {
                    this.Set(index.ToString(), element);
                }
                else if (element is IEnumerable<dynamic>)
                {
                    MarshallingList ml = new MarshallingList(index.ToString(), element as IEnumerable<dynamic>);
                    this.Set(index.ToString(), ml);
                }
                else if (element is IDictionary<string, dynamic>)
                {
                    MarshallingHash mh = new MarshallingHash(index.ToString(), element as IDictionary<string, dynamic>);
                    this.Set(index.ToString(), mh);
                }
                else
                {
                    MarshallingObjectValue o = new MarshallingObjectValue(index.ToString(), element);
                    this.Set(index.ToString(), o);
                }
            }
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
        public dynamic Value
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
        /// Gets all values
        /// </summary>
        public IEnumerable<IMarshalling> Values
        {
            get
            {
                return (from x in this.Data where x.Value is IMarshalling select x.Value as IMarshalling).AsEnumerable();
            }
        }

        /// <summary>
        /// Gets count (of Marshalling objects)
        /// </summary>
        public int Count
        {
            get
            {
                return this.HashKeys.Count();
            }
        }

        /// <summary>
        /// Gets all Marshalling keys
        /// </summary>
        public IEnumerable<string> HashKeys
        {
            get
            {
                return (from x in this.Data where x.Value is IMarshalling select x.Key).AsEnumerable();
            }
        }

        /// <summary>
        /// Gets or sets a value from hash
        /// given the name
        /// </summary>
        /// <param name="index">index position</param>
        /// <returns>object</returns>
        public IMarshalling this[int index]
        {
            get
            {
                if (index < this.Count)
                {
                    return this.Values.First(x => x.Name == index.ToString());
                }
                else
                {
                    throw new ArgumentException(String.Format("Index '{0}' not found", index));
                }
            }
            set
            {
                this.Set(index.ToString(), value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an enumerator
        /// </summary>
        /// <returns>enumerator</returns>
        public IEnumerator<IMarshalling> GetEnumerator()
        {
            return this.Values.GetEnumerator();
        }

        /// <summary>
        /// Add a new element into hash
        /// </summary>
        /// <param name="e">new element</param>
        public void Add(Func<IDictionary<string, dynamic>> f)
        {
            Marshalling.MarshallingHash h = Marshalling.MarshallingHash.CreateMarshalling("", f);
            h.Copy(false, this);
        }

        /// <summary>
        /// Add a new list into hash
        /// </summary>
        /// <param name="e">new element</param>
        public void Add(Func<IEnumerable<dynamic>> f)
        {
            Marshalling.MarshallingList l = Marshalling.MarshallingList.CreateMarshalling("", f);
            l.Copy(false, this);
        }

        /// <summary>
        /// Gets all property names
        /// </summary>
        /// <returns>properties</returns>
        public override string[] GetProperties()
        {
            return this.HashKeys.ToArray();
        }

        /// <summary>
        /// Gets a named property
        /// Shows all valid properties
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>property value</returns>
        public override IMarshalling GetProperty(string name)
        {
            string[] names = this.GetProperties();
            if (names.Contains(name))
            {
                if (this.Exists(name))
                    return this.Get(name);
                else
                    throw new ArgumentException(String.Format("Marshalling key '{0}' not found", name));
            }
            else
                throw new ArgumentException(String.Format("Marshalling key '{0}' does not exist", name));
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public object Clone()
        {
            List<IMarshalling> list = new List<IMarshalling>();
            foreach (IMarshalling a in this.Values)
            {
                list.Add(a.Clone() as IMarshalling);
            }
            return new MarshallingList(this.Name, list);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create marshalling
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static MarshallingList CreateMarshalling(string name, Func<IEnumerable<dynamic>> f)
        {
            return new MarshallingList(name, f());
        }

        #endregion

    }

}
