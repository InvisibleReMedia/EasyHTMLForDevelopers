using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHTMLDev
{
    interface IBindingUC
    {
        void ClearBindings();
        void BindDataToControl(Library.CadreModelType data);
    }
}
