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
        /// Gets the name of this value
        /// </summary>
        public string Name
        {
            get
            {
                return this.Get(nameName);
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

        #endregion

        #region Methods

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="value">value to test</param>
        /// <returns>true if valid</returns>
        protected abstract bool IsValid(dynamic value);

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
        /// <param name="enums">enumeration of valid values</param>
        public MarshallingIntValue(string name, long value)
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
            long val;
            return Int64.TryParse(value.ToString(), out val);
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
        /// <param name="enums">enumeration of valid values</param>
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
        /// <summary>
        /// Index name for value
        /// </summary>
        protected static readonly string valueName = "value";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for an empty hash
        /// </summary>
        /// <param name="name">name of value</param>
        public MarshallingHash(string name)
        {
            this.Set(nameName, name);
            this.Set(valueName, new List<IMarshalling>());
        }

        /// <summary>
        /// Constructor for a hash
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="elements">elements list</param>
        public MarshallingHash(string name, IEnumerable<IMarshalling> elements)
        {
            this.Set(nameName, name);
            this.Set(valueName, elements.ToList());
        }

        public MarshallingHash(string name, IDictionary<string, dynamic> keyValues)
        {
            this.Set(nameName, name);
            List<IMarshalling> values = new List<IMarshalling>();
            foreach (KeyValuePair<string, dynamic> kv in keyValues)
            {
                if (kv.Value is string)
                {
                    MarshallingRegexValue r = new MarshallingRegexValue(kv.Key, kv.Value, "^.*$");
                    values.Add(r);
                }
                else if (kv.Value is long || kv.Value is int || kv.Value is uint)
                {
                    MarshallingIntValue i = new MarshallingIntValue(kv.Key, kv.Value);
                    values.Add(i);
                }
                else if (kv.Value is double || kv.Value is float)
                {
                    MarshallingDoubleValue d = new MarshallingDoubleValue(kv.Key, kv.Value);
                    values.Add(d);
                }
            }
            this.Set(valueName, values);
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
            }
        }

        /// <summary>
        /// Gets all values
        /// </summary>
        public IEnumerable<IMarshalling> Values
        {
            get
            {
                return this.Value;
            }
        }

        /// <summary>
        /// Gets the hash
        /// </summary>
        protected List<IMarshalling> Hash
        {
            get
            {
                return this.Get(valueName);
            }
        }

        /// <summary>
        /// Gets all keys
        /// </summary>
        public IEnumerable<string> HashKeys
        {
            get
            {
                return this.Values.Select(x => x.Name);
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
                    return this.Values.Single(x => x.Name == key);
                }
                else
                {
                    return new MarshallingRegexValue(key, "", "^.*$");
                }
            }
            set
            {
                if (this.HashKeys.Contains(key))
                {
                    List<IMarshalling> newList = this.Hash.Except(this.Values.Where(x => x.Name == key)).ToList();
                    newList.Add(value);
                    this.Set(valueName, newList);
                }
                else
                {
                    this.Hash.Add(value);
                }
            }
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
        /// <summary>
        /// Index name for value
        /// </summary>
        protected static readonly string valueName = "value";

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
            this.Set(valueName, new List<IMarshalling>());
        }

        /// <summary>
        /// Constructor for a list
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="elements">elements list</param>
        public MarshallingList(string name, IEnumerable<IMarshalling> elements)
        {
            this.Set(nameName, name);
            this.Set(valueName, elements.ToList());
        }

        public MarshallingList(string name, IEnumerable<dynamic> keyValues)
        {
            this.Set(nameName, name);
            List<IMarshalling> values = new List<IMarshalling>();
            for(int index = 0; index < keyValues.Count(); ++index)
            {
                dynamic element = keyValues.ElementAt(index);
                if (element is string)
                {
                    MarshallingRegexValue r = new MarshallingRegexValue(index.ToString(), element, "^.*$");
                    values.Add(r);
                }
                else if (element is long)
                {
                    MarshallingIntValue i = new MarshallingIntValue(index.ToString(), element);
                    values.Add(i);
                }
                else if (element is double)
                {
                    MarshallingDoubleValue d = new MarshallingDoubleValue(index.ToString(), element);
                    values.Add(d);
                }
            }
            this.Set(valueName, values);
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
            }
        }

        /// <summary>
        /// Gets all values
        /// </summary>
        public IEnumerable<IMarshalling> Values
        {
            get
            {
                return this.Value;
            }
        }

        /// <summary>
        /// Gets the hash
        /// </summary>
        protected List<IMarshalling> List
        {
            get
            {
                return this.Get(valueName);
            }
        }

        /// <summary>
        /// Gets count
        /// </summary>
        public int Count
        {
            get
            {
                return this.List.Count;
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
                    return this.List.ElementAt(index);
                }
                else
                {
                    return new MarshallingRegexValue(index.ToString(), "", "^.*$");
                }
            }
            set
            {
                if (index < this.Count)
                {
                    this.List[index] = value;
                }
                else
                {
                    this.List.Add(value);
                }
            }
        }

        #endregion

    }
}
