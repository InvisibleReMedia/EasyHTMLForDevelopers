using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public class SculpturePanel : Panel
    {
        #region Private Constants
        private readonly float[] scale = new float[] { 1/16f, 1/8f, 1/4f, 1/2f, 1f, 2f, 4f, 8f, 16f };
        #endregion

        #region Private Fields
        private float ratio;
        private event EventHandler ratioChanged;
        #endregion

        #region Default Constructor
        public SculpturePanel()
        {
            this.ratio = 1f;
        }
        #endregion

        #region Public Properties
        public float Ratio
        {
            get { return this.ratio; }
        }
        #endregion

        #region Public Events
        public event EventHandler RatioChanged
        {
            add { this.ratioChanged += new EventHandler(value); }
            remove { this.ratioChanged -= new EventHandler(value); }
        }
        #endregion

        #region Public Methods
        public void Scale(int ratio)
        {
            base.Scale(new SizeF(this.ratio, this.ratio));
            if (this.ratioChanged != null)
                this.ratioChanged(this, new EventArgs());
            this.ratio = this.scale[ratio];
            float f = 1 / this.ratio;
            base.Scale(new SizeF(f, f));
            if (this.ratioChanged != null)
                this.ratioChanged(this, new EventArgs());
            this.Invalidate(true);
        }
        #endregion
    }
}
