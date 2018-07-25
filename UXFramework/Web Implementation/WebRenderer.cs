using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;
using System.IO;

namespace UXFramework.WebImplementation
{
    public class WebRenderer : IUXRenderer
    {

        #region Fields

        private static readonly string projectName = "webRenderer";

        /// <summary>
        /// project web implementation
        /// </summary>
        private Project project;
        /// <summary>
        /// Page in progress
        /// </summary>
        private Page currentPage;
        private MasterPage currentMasterPage;

        #endregion

        #region Constructor

        public WebRenderer()
        {
            this.project = new Project();
            Projects.Add(projectName, this.project);
            this.project.CreationDate = DateTime.Now;
            this.project.Title = projectName;
            this.project.Revision = 1;

            Projects.TrySelect(projectName, out this.project);
            // creating all master-objects and tools
            HTMLTool tool = new HTMLTool();
            tool.ConstraintHeight = EnumConstraint.AUTO;
            tool.ConstraintWidth = EnumConstraint.AUTO;
            tool.Path = "html";
            tool.Name = "ReadOnlyText";
            tool.Id = "labelText";
            this.project.Tools.Add(tool);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Render a window
        /// </summary>
        /// <param name="window">window to render</param>
        public void RenderControl(UXWindow window)
        {
            string previous;
            Projects.Activate(projectName, out previous);
            Page p = new Page();
            p.Disposition = Disposition.CENTER;
            p.ConstraintWidth = EnumConstraint.RELATIVE;
            p.ConstraintHeight = EnumConstraint.RELATIVE;
            p.Width = 100;
            p.Height = 100;
            MasterPage mp = new MasterPage();
            mp.Name = "masterPage_" + window.Name;
            mp.Width = Convert.ToUInt32(window.GetWebBrowser().DisplayRectangle.Width - 30);
            mp.Height = Convert.ToUInt32(window.GetWebBrowser().DisplayRectangle.Height - 30);
            mp.ConstraintWidth = EnumConstraint.FIXED;
            mp.ConstraintHeight = EnumConstraint.FIXED;
            mp.CountColumns = 1;
            mp.CountLines = 1;

            List<SizedRectangle> rects = new List<SizedRectangle>();
            SizedRectangle sz = new SizedRectangle((int)mp.Width, (int)mp.Height, 1, 1, 0, 0);
            rects.Add(sz);
            mp.MakeZones(rects);
            this.project.MasterPages.Add(mp);

            p.MasterPageName = mp.Name;
            p.Name = window.FileName;

            if (window.Children.Count > 1)
            {
                throw new NotSupportedException("Impossible de créer une fenêtre avec plusieurs éléments");
            }
            else
            {
                currentPage = p;
                currentMasterPage = mp;
                RenderControl(window.Children[0]);
            }

            using (FileStream fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, p.Name), FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    OutputHTML o = currentPage.GenerateProduction();
                    sw.Write(o.HTML);
                    sw.Close();
                }
                fs.Close();
            }
            Projects.Reactivate(previous);
        }

        /// <summary>
        /// Render a box
        /// </summary>
        /// <param name="box">box to render</param>
        public void RenderControl(UXBox box)
        {

        }

        /// <summary>
        /// Render a button
        /// </summary>
        /// <param name="button">button to render</param>
        public void RenderControl(UXButton button)
        {

        }

        /// <summary>
        /// Render a checkbox
        /// </summary>
        /// <param name="check">checkbox to render</param>
        public void RenderControl(UXCheck check)
        {

        }

        /// <summary>
        /// Render a drop-down list
        /// </summary>
        /// <param name="combo">drop-down list</param>
        public void RenderControl(UXCombo combo)
        {

        }

        /// <summary>
        /// Render an editable text
        /// </summary>
        /// <param name="editText">text to render</param>
        public void RenderControl(UXEditableText editText)
        {

        }

        /// <summary>
        /// Render a frame
        /// </summary>
        /// <param name="frame">frame to render</param>
        public void RenderControl(UXFrame frame)
        {

        }

        /// <summary>
        /// Render a label
        /// </summary>
        /// <param name="text">text to render</param>
        public void RenderControl(UXReadOnlyText text)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Name == "ReadOnlyText"));
            obj.Container = this.currentMasterPage.HorizontalZones[0].VerticalZones[0].Name;
            obj.HTML = text.Text;
            this.currentPage.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render a table
        /// </summary>
        /// <param name="table">table to render</param>
        public void RenderControl(UXTable table)
        {

        }

        /// <summary>
        /// Render object
        /// </summary>
        /// <param name="obj">object</param>
        public void RenderControl(IUXObject obj)
        {
            if (obj is UXBox) RenderControl(obj as UXBox);
            else if (obj is UXButton) RenderControl(obj as UXButton);
            else if (obj is UXCheck) RenderControl(obj as UXCheck);
            else if (obj is UXCombo) RenderControl(obj as UXCombo);
            else if (obj is UXEditableText) RenderControl(obj as UXEditableText);
            else if (obj is UXFrame) RenderControl(obj as UXFrame);
            else if (obj is UXReadOnlyText) RenderControl(obj as UXReadOnlyText);
            else if (obj is UXTable) RenderControl(obj as UXTable);
            else if (obj is UXWindow) RenderControl(obj as UXWindow);
        }

        #endregion

    }
}
