using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHTMLDev
{
    class LocaleKeyBinding : System.Windows.Forms.Binding
    {

        #region Public Constructor
        
        public LocaleKeyBinding(object dataSource, string Key)
            : base("Text", dataSource, Key)
        {
        }

        #endregion
    }
}
