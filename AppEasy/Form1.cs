using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UXFramework.BeamConnections;

namespace AppEasy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // couleurs
            BrandIdentity.Current.Colors.Add("fond_haut", "#AF7853");
            BrandIdentity.Current.Colors.Add("line1", "White");
            BrandIdentity.Current.Colors.Add("trait2", "#B97457");
            BrandIdentity.Current.Colors.Add("fond_page", "#E2F5A7");
            BrandIdentity.Current.Colors.Add("fond_tableau", "#E5F9AB");
            BrandIdentity.Current.Colors.Add("fond_button", "#EFE4B0");
            BrandIdentity.Current.Colors.Add("pencil", "Black");
            // tailles
            BrandIdentity.Current.Sizes.Add("taille_page", new Size(1000, 440));
            BrandIdentity.Current.Sizes.Add("taille_haut", new Size(1000, 60));
            BrandIdentity.Current.Sizes.Add("taille_trait", new Size(1000, 7));
            BrandIdentity.Current.Sizes.Add("taille_trait2", new Size(1000, 3));
            // rectangle
            BrandIdentity.Current.Boxes.Add("button", new Rectangle(0, 0, 205, 43));
            BrandIdentity.Current.Boxes.Add("button_bord", new Rectangle(5, 5, 200, 38));
            BrandIdentity.Current.Boxes.Add("button_pad", new Rectangle(8, 8, 197, 35));

            SplashScreen.Splash(this.webBrowser1);
        }
    }
}
