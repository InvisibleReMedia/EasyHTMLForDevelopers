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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.btnContinue.Text = Localization.Strings.GetString("Continue");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(AppDomain.CurrentDomain.BaseDirectory + Localization.Strings.GetString("IntroductionWebFile"));
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
