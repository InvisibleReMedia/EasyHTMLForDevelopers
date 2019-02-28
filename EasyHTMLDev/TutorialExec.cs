using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    internal class TutorialExec : IDisposable
    {
        #region Private Fields

        private TutorialListening.ITutorial tuto;

        #endregion

        #region Public Constructor

        public TutorialExec()
        {
            this.Init();
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            if (this.tuto == null)
            {
                this.tuto = new TutorialListening.Tutorial((c, f, a) =>
                {
                    if (!String.IsNullOrEmpty(c))
                    {

                        List<Form> list = new List<Form>();
                        // recopie la liste (car elle est sujette à changer au cours de l'exécution)
                        foreach (Form z in Application.OpenForms)
                        {
                            list.Add(z);
                        }
                        foreach (Form z in list)
                        {
                            if (z.Name == c)
                            {
                                Type t = z.GetType();
                                object res = t.InvokeMember(f, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public, null, z, new object[] { });
                                if (res != null)
                                {
                                    if (res is Button)
                                    {
                                        Button btn = res as Button;
                                        btn.PerformClick();
                                    }
                                    else if (res is MenuItem)
                                    {
                                        MenuItem menu = res as MenuItem;
                                        menu.PerformClick();
                                    }
                                    else if (res is RadioButton)
                                    {
                                        RadioButton radio = res as RadioButton;
                                        radio.PerformClick();
                                    }
                                    else if (res is ToolStripMenuItem)
                                    {
                                        ToolStripMenuItem tool = res as ToolStripMenuItem;
                                        if (a == "Select")
                                        {
                                            tool.Select();
                                        }
                                        else if (a == "Show")
                                        {
                                            tool.DropDown.Show();
                                        }
                                        else if (a == "Click")
                                        {
                                            tool.PerformClick();
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                });
            }
        }

        #endregion

        #region Public Methods

        public void Exec()
        {
            if (this.tuto == null)
            {
                this.Init();
            }

            this.tuto.AddClick("tutorial-fr 1.wav", "", "", "", new TimeSpan(0, 0, 58), "", null);

            this.tuto.AddClick("tutorial-fr 2.wav", "T1", "Form1", "projetToolStripMenuItem", new TimeSpan(0, 0, 5), "Select", new Dictionary<string, object>()
            {
            });
            this.tuto.AddClick("", "T2", "Form1", "projetToolStripMenuItem", new TimeSpan(0, 0, 2), "Show", new Dictionary<string, object>()
            {
            });
            this.tuto.AddClick("", "T3", "Form1", "créerToolStripMenuItem", new TimeSpan(0, 0, 2), "Select", new Dictionary<string, object>()
            {
            });
            this.tuto.AddClick("", "T4", "Form1", "créerToolStripMenuItem", new TimeSpan(0, 0, 2), "Click", new Dictionary<string, object>()
            {
            });
            this.tuto.Play();
        }

        public void Abort()
        {
            this.tuto.Stop();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.tuto != null)
            {
                this.tuto.Stop();
                this.tuto = null;
            }
        }

        #endregion
    }
}
