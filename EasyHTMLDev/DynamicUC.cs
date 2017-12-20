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
    public partial class DynamicUC : UserControl, IBindingUC
    {
        public DynamicUC()
        {
            InitializeComponent();
        }

        public void ClearBindings()
        {
        }

        public void BindDataToControl(Library.CadreModelType data)
        {
        }
    }
}
