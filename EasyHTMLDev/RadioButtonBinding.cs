using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyHTMLDev
{
    class RadioButtonBinding : ILookup<System.Enum, System.Windows.Forms.RadioButton>
    {
        private Type enumType;
        private List<System.Windows.Forms.RadioButton> radioButtons;
        private System.Windows.Forms.BindingSource bindingSource;
        private string propertyName;
        private bool resetting = false;
        private Dictionary<string, string> assocEnum = new Dictionary<string, string>() { { "FIXED", "FixedConstraints" }, { "AUTO", "AutoConstraints" }, { "RELATIVE", "RelativeConstraints" }, { "FORCED", "ForcedConstraints" } };
        private List<int> localeComponentId;

        public RadioButtonBinding(Type myEnum, System.Windows.Forms.BindingSource bs, string propertyName)
        {
            this.localeComponentId = new List<int>();
            this.enumType = myEnum;
            this.radioButtons = new List<System.Windows.Forms.RadioButton>();
            foreach (string name in System.Enum.GetNames(this.enumType))
            {
                System.Windows.Forms.RadioButton rb = new System.Windows.Forms.RadioButton();
                rb.Text = assocEnum[name];
                rb.Name = name;
                rb.AutoSize = true;
                this.radioButtons.Add(rb);
                rb.CheckedChanged += new EventHandler(rb_CheckedChanged);
                this.localeComponentId.Add(Localization.Strings.RelativeWindow(rb));
            }
            this.bindingSource = bs;
            this.propertyName = propertyName;
            this.bindingSource.DataSourceChanged += new EventHandler(bindingSource_DataSourceChanged);
        }

        internal void Close()
        {
            foreach (int id in this.localeComponentId)
            {
                int z = id;
                Localization.Strings.FreeWindow(ref z);
            }
            this.localeComponentId.RemoveAll(a => true);
        }

        string GetEnumValue(string val)
        {
            KeyValuePair<string, string> kv = assocEnum.Single((a) => Localization.Strings.GetString(a.Value) == val);
            return kv.Key;
        }

        void bindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            object obj = this.bindingSource.DataSource;
            if (obj != null)
            {
                System.Enum item = obj.GetType().GetProperty(propertyName).GetValue(obj, new object[] { }) as System.Enum;
                foreach (System.Enum value in System.Enum.GetValues(this.enumType))
                {
                    if (this.Contains(value))
                    {
                        System.Windows.Forms.RadioButton rb = this[value].First();
                        if (value.Equals(item))
                        {
                            this.resetting = true;
                            rb.Checked = true;
                            this.resetting = false;
                        }
                    }
                }
            }
        }

        void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.resetting)
            {
                System.Windows.Forms.RadioButton rb = sender as System.Windows.Forms.RadioButton;
                System.Enum val = null;
                try
                {
                    val = System.Enum.Parse(this.enumType, this.GetEnumValue(rb.Text)) as System.Enum;
                }
                catch (Exception ex)
                {
                    // cannot occurred if code is safe
                    System.Windows.Forms.MessageBox.Show("No enum value for this radio button : " + ex.ToString());
                }
                object obj = this.bindingSource.DataSource;
                obj.GetType().GetProperty(propertyName).SetValue(obj, val, new object[] { });
                this.bindingSource.CurrencyManager.Refresh();
            }
        }

        public int Count
        {
            get
            {
                return System.Enum.GetNames(this.enumType).Count();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.radioButtons.GetEnumerator();
        }

        public bool Contains(Enum key)
        {
            return System.Enum.GetNames(this.enumType).Contains(key.ToString());
        }

        public IEnumerable<System.Windows.Forms.RadioButton> this[Enum key]
        {
            get
            {
                return this.radioButtons.FindAll(a => { return this.GetEnumValue(a.Text) == key.ToString(); });
            }
        }

        IEnumerator<IGrouping<Enum, System.Windows.Forms.RadioButton>> IEnumerable<IGrouping<Enum, System.Windows.Forms.RadioButton>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void AddControlsIntoGroupBox(System.Windows.Forms.GroupBox gb, System.Windows.Forms.FlowDirection dir)
        {
            System.Windows.Forms.FlowLayoutPanel panel = new System.Windows.Forms.FlowLayoutPanel();
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.FlowDirection = dir;
            foreach (System.Windows.Forms.RadioButton rb in this.radioButtons)
            {
                panel.Controls.Add(rb);
            }
            gb.Controls.Add(panel);
        }
    }
}
