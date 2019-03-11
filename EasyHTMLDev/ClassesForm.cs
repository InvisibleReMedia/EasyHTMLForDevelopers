using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class ClassesForm : Form
    {

        private Library.CSSList css;

        public ClassesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets CSS
        /// </summary>
        public Library.CSSList CSSList
        {
            get { return this.css; }
            set
            {
                this.css = value;
                this.DataBind();
            }
        }

        private void DataBind()
        {
            foreach (Library.CodeCSS c in this.CSSList.List)
            {
                this.cmbIds.Items.Add(new KeyValuePair<string, Library.CodeCSS>(c.Ids, c));
            }
        }

        private void cmbIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbIds.SelectedIndex != -1) {
                KeyValuePair<string, Library.CodeCSS> kv = (KeyValuePair<string, Library.CodeCSS>)this.cmbIds.SelectedItem;
                foreach (string key in kv.Value.Keys)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewCell cell1 = new DataGridViewTextBoxCell();
                    cell1.Value = key;
                    DataGridViewCell cell2 = new DataGridViewTextBoxCell();
                    cell2.Value = kv.Value.Body[key];
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    this.dataProperties.Rows.Add(row);
                }
            }
        }

        private void cmbIds_Format(object sender, ListControlConvertEventArgs e)
        {
            KeyValuePair<string, Library.CodeCSS> kv = (KeyValuePair<string, Library.CodeCSS>)e.ListItem;
            e.Value = kv.Key;
        }
    }
}
