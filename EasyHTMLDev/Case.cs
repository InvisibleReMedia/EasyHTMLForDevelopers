using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EasyHTMLDev
{
    class Case
    {
        private int ligne;
        private int colonne;
        private bool selected;
        private bool disabled;
        private Rectangle rect;

        public Case(int ligne, int colonne)
        {
            this.ligne = ligne;
            this.colonne = colonne;
            this.rect = new Rectangle(0, 0, 0, 0);
        }

        public Rectangle Rectangle
        {
            get { return this.rect; }
        }

        public bool IsDisabled
        {
            get
            {
                return this.disabled;
            }
        }

        public void Draw(Graphics g)
        {
            Rectangle fill = new Rectangle(this.rect.Left + 1, this.rect.Top + 1, this.rect.Width - 2, this.rect.Height - 2);
            if (this.selected)
            {
                g.FillRectangle(Brushes.Blue, fill);
            }
            else
            {
                g.FillRectangle(Brushes.White, fill);
            }
        }

        public void resise(Rectangle r)
        {
            this.rect = r;
        }

        public void Select()
        {
            if (!disabled)
                this.selected = true;
        }

        public void Unselect()
        {
            this.selected = false;
        }

        public void Disable()
        {
            this.selected = false;
            this.disabled = true;
        }
    }
}
