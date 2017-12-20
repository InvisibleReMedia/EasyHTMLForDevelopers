using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class TexteUC : UserControl, IBindingUC
    {
        public TexteUC()
        {
            InitializeComponent();
        }

        public void ClearBindings()
        {
            this.txt.DataBindings.Clear();
        }

        public void BindDataToControl(Library.CadreModelType data)
        {
            this.txt.DataBindings.Add("Text", data, "Content");
        }
    }
}
