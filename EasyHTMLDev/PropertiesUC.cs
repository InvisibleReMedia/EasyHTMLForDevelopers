using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class PropertiesUC : UserControl
    {
        #region Private Fields
        private Library.CadreModel currentCadre;
        private event EventHandler newObject;
        private event EventHandler suppressObject;
        private event EventHandler openModelType;
        private event EventHandler<ColorEventArgs> openColorScheme;

        private int localeComponentId;

        #endregion

        #region Public Constructor
        public PropertiesUC()
        {
            InitializeComponent();
            string items = Localization.Strings.GetString("ScaleValue");
            string[] tab = items.Split(Environment.NewLine.ToArray());
            foreach (string s in tab)
            {
                if (!String.IsNullOrEmpty(s))
                    this.cmbScale.Items.Add(s);
            }
            this.RegisterControls(ref this.localeComponentId);
        }
        #endregion

        #region Private Methods
        private void BindDataToControl(Library.CadreModel cm)
        {
            this.cadreModelBindingSource.DataSource = cm;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (this.newObject != null)
                this.newObject(sender, e);
        }

        private void btnSuppr_Click(object sender, EventArgs e)
        {
            if (this.newObject != null)
                this.suppressObject(sender, e);
        }

        private void btnType_Click(object sender, EventArgs e)
        {
            if (this.openModelType != null)
                this.openModelType(sender, e);
        }
        #endregion

        #region Public Properties
        public Library.CadreModel CurrentObject
        {
            get { return this.currentCadre; }
            set
            {
                if (value != null)
                {
                    this.btnSuppr.Enabled = true;
                    this.grpDatas.Enabled = true;
                    this.BindDataToControl(value);
                }
                else
                {
                    this.btnSuppr.Enabled = false;
                    this.grpDatas.Enabled = false;
                }
                this.currentCadre = value;
            }
        }
        #endregion

        #region Events
        public event EventHandler OpenModelType
        {
            add { this.openModelType += new EventHandler(value); }
            remove { this.openModelType -= new EventHandler(value); }
        }

        public event EventHandler NewObject
        {
            add { this.newObject += new EventHandler(value); }
            remove { this.newObject -= new EventHandler(value); }
        }

        public event EventHandler SuppressObject
        {
            add { this.suppressObject += new EventHandler(value); }
            remove { this.suppressObject -= new EventHandler(value); }
        }

        public event EventHandler<ColorEventArgs> OpenColorScheme
        {
            add { this.openColorScheme += new EventHandler<ColorEventArgs>(value); }
            remove { this.openColorScheme -= new EventHandler<ColorEventArgs>(value); }
        }
        #endregion

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            this.openColorScheme(sender, new ColorEventArgs(this.CurrentObject.GetType().GetProperty("Background"), this.CurrentObject));
        }

        private void btnBorderColor_Click(object sender, EventArgs e)
        {
            this.openColorScheme(sender, new ColorEventArgs(this.CurrentObject.GetType().GetProperty("Border"), this.CurrentObject));
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            this.openColorScheme(sender, new ColorEventArgs(this.CurrentObject.GetType().GetProperty("Foreground"), this.CurrentObject));
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            this.UnregisterControls(ref this.localeComponentId);
        }

    }

    public class ColorEventArgs : EventArgs
    {
        #region Private Fields
        private System.Reflection.PropertyInfo c;
        private object param;
        #endregion

        #region Public Constructor
        public ColorEventArgs(System.Reflection.PropertyInfo p, object source)
        {
            this.c = p;
            this.param = source;
        }
        #endregion

        #region Public Properties
        public System.Reflection.PropertyInfo ColorProperty
        { get { return this.c; } }

        public object Source
        { get { return this.param; } }
        #endregion
    }


}
