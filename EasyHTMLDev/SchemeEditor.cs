using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace EasyHTMLDev
{
    public partial class SchemeEditor : UserControl
    {
        private List<Library.CodeCSS> cssList;
        private List<int> colors;
        private event EventHandler schemeEditorChanged;
        private int localeComponentId;

        public SchemeEditor()
        {
            InitializeComponent();
            this.cssList = new List<Library.CodeCSS>();
            this.colors = new List<int>();
            this.RegisterControls(ref this.localeComponentId);
        }

        public int[] Colors
        {
            get { return this.colors.ToArray(); }
        }

        public event EventHandler SchemeEditorChanged
        {
            add { this.schemeEditorChanged += new EventHandler(value); }
            remove { this.schemeEditorChanged -= new EventHandler(value); }
        }

        private void txtCSS_Validating(object sender, CancelEventArgs e)
        {
            string errorText;
            bool isSuccess = Library.CSSValidation.CSSValidate(this.txtCSS.Text, false, this.cssList, out errorText);
            if (!isSuccess)
            {
                this.epCSS.SetError(this.txtCSS, errorText);
                this.btnAdd.Enabled = false;
            }
            else
            {
                this.epCSS.Clear();
                this.txtCSS.Text = String.Empty;
                foreach (Library.CodeCSS c in this.cssList)
                {
                    this.txtCSS.Text += c.GenerateCSS(true, true) + Environment.NewLine;
                }
                this.btnAdd.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.colors.Clear();
            foreach (Library.CodeCSS c in cssList)
            {
                try
                {
                    Library.CSSColor col = c.ForegroundColor;
                    this.colors.Add(col.Color.ToArgb());
                }
                catch { }
            }
            this.schemeEditorChanged(sender, e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
