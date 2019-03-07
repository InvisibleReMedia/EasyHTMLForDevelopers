using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localization
{
    public partial class Strings : Component, ICustomTypeDescriptor
    {

        #region Inner Class

        internal class LocaleValue
        {
            #region Private Fields
            private KeyValuePair<string, string> kv;
            private object refObj;
            private List<object> parameters;
            #endregion

            #region Constructor
            public LocaleValue(string name, string value)
            {
                this.parameters = new List<object>();
                this.kv = new KeyValuePair<string, string>(name, value);
            }
            #endregion

            #region Public Properties
            public string Key
            {
                get { return this.kv.Key; }
            }

            public string Value
            {
                get { return this.kv.Value; }
            }

            public object RefObject
            {
                get { return this.refObj; }
                set { this.refObj = value; }
            }

            public List<object> Parameters
            {
                get { return this.parameters; }
            }
            #endregion

        }

        #endregion

        #region Private Static Fields
        private static Dictionary<int, Strings> _instances;
        private static Dictionary<string, string> langs;
        private static System.ComponentModel.ComponentResourceManager _resources;
        #endregion

        #region Private Fields
        private PropertyDescriptorCollection props = new PropertyDescriptorCollection(new PropertyDescriptor[] {});
        private List<LocaleValue> values = new List<LocaleValue>();
        #endregion

        #region Constructors

        public Strings()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Static Properties

        public static Dictionary<int, Strings> Instances
        {
            get
            {
                Strings.InitializeInstance();
                return Strings._instances;
            }
        }

        public static Dictionary<string, string> Languages
        {
            get
            {
                Strings.InitializeLanguages();
                return Strings.langs;
            }
        }

        #endregion

        #region Public Static Methods

        public static string GetString(string name)
        {
            string localized = name;
            try
            {
                Strings.InitializeResource();
                localized = Strings._resources.GetString(name);
                if (String.IsNullOrEmpty(localized))
                    localized = name;
            }
            catch {
                System.Diagnostics.Trace.WriteLine(String.Format("The name '{0}' does not exist", name));
            }
            return localized;
        }

        public static string SelectLanguage(string language)
        {
            Strings.InitializeLanguages();
            if (Strings.langs.ContainsKey(language))
                return Strings.langs[language];
            else
                return "en";
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public static int RelativeWindow(System.Windows.Forms.Control c, params object[] pars)
        {
            Strings.InitializeInstance();
            int winId = 1;
            if (Strings.Instances.Count > 0)
                winId = Strings.Instances.Max(a => a.Key) + 1;
            Strings.Instances.Add(winId, new Strings());
            Strings.Instances[winId].BindControls(c, pars);
            return winId;
        }

        public static void FreeWindow(ref int winId)
        {
            Strings.Instances.Remove(winId);
            winId = 0;
        }

        public static void RefreshAll()
        {
            Strings.InitializeInstance();
            foreach (int i in Strings.Instances.Keys)
            {
                Strings.Instances[i].Refresh();
            } 
        }

        #endregion
        
        #region Private Methods

        private static void InitializeLanguages()
        {
            if (Strings.langs == null)
            {
                Strings.langs = new Dictionary<string, string>();
                Strings.langs.Add("Français", "fr");
                Strings.langs.Add("English", "en");
            }
        }

        private static void InitializeResource()
        {
            if (Strings._resources == null)
            {
                Strings._resources = new System.ComponentModel.ComponentResourceManager(typeof(Strings));
            }
        }

        private static void InitializeInstance()
        {
            if (Strings._instances == null)
            {
                Strings._instances = new Dictionary<int, Strings>();
            }
        }

        private bool IsContentTextStatic(object obj)
        {
            return !this.GetName(obj).StartsWith("prv_"); 
        }

        private bool IsControlField(System.Reflection.FieldInfo fi)
        {
            return fi.FieldType.FullName.StartsWith("System.Windows.Forms");
        }

        private void SetText(object obj, string text)
        {
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("Text");
            if (pi != null)
            {
                if (!String.IsNullOrEmpty(text))
                    pi.SetValue(obj, text, new object[] { });
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private string GetText(object obj)
        {
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("Text");
            if (pi != null)
            {
                return pi.GetValue(obj, new object[] { }).ToString();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private string GetName(object obj)
        {
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("Name");
            if (pi != null)
            {
                return pi.GetValue(obj, new object[] { }).ToString();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private System.Windows.Forms.ControlBindingsCollection GetDataBindings(object obj)
        {
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("DataBindings");
            if (pi != null)
            {
                return pi.GetValue(obj, new object[] { }) as System.Windows.Forms.ControlBindingsCollection;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private IEnumerable<System.Reflection.FieldInfo> GetControls(System.Windows.Forms.Control input)
        {
            if (IsContentTextStatic(input))
            {
                Type t = input.GetType();
                System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.GetField;
                flags |= System.Reflection.BindingFlags.Public;
                flags |= System.Reflection.BindingFlags.NonPublic;
                flags |= System.Reflection.BindingFlags.Instance;
                System.Reflection.FieldInfo[] tab = t.GetFields(flags);

                foreach (System.Reflection.FieldInfo fi in tab)
                {
                    System.Diagnostics.Trace.WriteLine("property=" + fi.Name);
                }

                return (from f in tab where (f.FieldType.FullName.StartsWith("System.Windows.Forms") || (f.FieldType.FullName.StartsWith("EasyHTMLDev"))) select f).AsEnumerable();

            }
            else
            {
                return new List<System.Reflection.FieldInfo>() { };
            }
        }

        private void BindControls(object obj, params object[] pars)
        {
            try
            {
                string text = this.GetText(obj);
                string name = this.GetName(obj);

                if (!String.IsNullOrEmpty(name) && IsContentTextStatic(obj))
                {
                    foreach (object param in pars)
                    {
                        text += "," + param.ToString();
                    }
                    PropertyDescriptor newProperty = TypeDescriptor.CreateProperty(typeof(LocaleValue), "Value", typeof(string), new AmbientValueAttribute(text));
                    props.Add(newProperty);
                    LocaleValue lv = this.GetPropertyOwner(newProperty) as LocaleValue;
                    lv.RefObject = obj;
                    lv.Parameters.AddRange(pars);
                    this.values.Add(lv);
                }
            }
            catch { }
        }

        #endregion

        #region Internal Methods

        internal void OneBindControl(System.Windows.Forms.Control c, params object[] pars)
        {
            if (!this.DesignMode)
            {
                this.BindControls(c as object, pars);
            }
        }

        internal void BindControls(System.Windows.Forms.Control c, params object[] pars)
        {
            if (!this.DesignMode)
            {
                this.BindControls(c as object, pars);
                foreach (System.Reflection.FieldInfo fi in this.GetControls(c))
                {
                    object val = fi.GetValue(c);
                    System.Diagnostics.Trace.WriteLine("fi=" + fi.Name);
                    if (val != null)
                    {
                        System.Diagnostics.Trace.WriteLine("value=" + val.ToString()); 
                        this.BindControls(val);
                    } 
                    else {
                        System.Diagnostics.Trace.WriteLine(String.Format("The name '{0}' does not exist", fi.Name));
                    }


                }
                this.Refresh();
            }
        }

        internal void Refresh()
        {
            if (!this.DesignMode)
            {
                Strings.InitializeResource();
                foreach (LocaleValue lv in this.values)
                {
                    try
                    {
                        string result = String.Format(Strings._resources.GetString(lv.Key), lv.Parameters.ToArray());
                        this.SetText(lv.RefObject, result);
                    }
                    catch {
                        System.Diagnostics.Trace.WriteLine(String.Format("The name '{0}' does not exist", lv.Key));
                    }
                }
            }
        }

        #endregion


        #region ICustomTypeDescriptor

        public AttributeCollection GetAttributes()
        {
            return new AttributeCollection(new Attribute[] { });
        }

        public string GetClassName()
        {
            return "Strings";
        }

        public string GetComponentName()
        {
            return null;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return new EventDescriptorCollection(new EventDescriptor[] { });
        }

        public EventDescriptorCollection GetEvents()
        {
            return this.GetEvents(new Attribute[] { });
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return props;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return props;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            LocaleValue loc;
            IEnumerable<AmbientValueAttribute> col = pd.Attributes.OfType<AmbientValueAttribute>();
            Strings.InitializeResource();
            if (col.Count() > 0)
            {
                AmbientValueAttribute ambient = col.ElementAt(0);
                string[] tab = ((string)ambient.Value).Split(',');
                if (tab.Length > 0)  {
                    loc = new LocaleValue(tab[0], Strings._resources.GetString(tab[0]));
                    loc.Parameters.AddRange(tab.TakeWhile((s, i) => i > 0));
                }
                else
                {
                    loc = new LocaleValue(pd.Name, pd.Name);
                }
            }
            else
            {
                loc = new LocaleValue(pd.Name, pd.Name);
            }
            return loc;
        }

        #endregion

    }

}
