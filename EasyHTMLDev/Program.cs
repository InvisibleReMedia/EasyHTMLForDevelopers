using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Localization.Names.Validate();
            CommonDirectories.ConfigDirectories.CreateMyDocuments();
            try
            {
                Application.Run(new Form1());
            }
            catch(Exception ex)
            {
                MessageBox.Show("This application has ended." + ex.ToString(), "Fatal exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
