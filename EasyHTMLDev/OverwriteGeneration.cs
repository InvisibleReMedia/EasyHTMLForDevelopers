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
    public partial class OverwriteGeneration : UserControl
    {
        private int localeComponentId;

        public OverwriteGeneration()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
