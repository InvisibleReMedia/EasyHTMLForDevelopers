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
        private string currentContainer;
        private dynamic currentObject;

        #endregion

        #region Constructor

        public WebRenderer()
        {
            StringBuilder sb = new StringBuilder();

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
            tool.Name = "readOnlyText";
            tool.Id = "labelText";
            tool.HTML = "<div style='cursor:default' onselectstart='javascript:return false;' ondragstart='javascript:return false;'>{0}</div>";
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.ConstraintHeight = EnumConstraint.AUTO;
            tool.ConstraintWidth = EnumConstraint.AUTO;
            tool.Path = "html";
            tool.Name = "selectableText";
            tool.Id = "labelSelectableText";
            tool.HTML = "<div id='{0}' onmouseover='javascript:RaiseArrow(this);' indexValue='{1}' style='cursor:default' onselectstart='javascript:return false;' ondragstart='javascript:return false;'>{2}</div>";
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.ConstraintHeight = EnumConstraint.AUTO;
            tool.ConstraintWidth = EnumConstraint.AUTO;
            tool.Path = "html";
            tool.Name = "box";
            tool.Id = "boxContainer";
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.Path = "html";
            tool.Name = "default";
            tool.Id = "defaultCode";
            CodeCSS outerDiv = new CodeCSS(".outerDiv");
            outerDiv.Discret("margin-top", "auto");
            outerDiv.Discret("margin-bottom", "auto");
            outerDiv.Discret("margin-left", "auto");
            outerDiv.Discret("margin-right", "auto");
            outerDiv.Discret("width", "150px");
            outerDiv.Discret("height", "30px");
            outerDiv.Discret("padding", "5px");
            outerDiv.Discret("background-color", "#EFFAFC");
            outerDiv.Discret("border", "3px solid #3F48CC");
            outerDiv.Discret("border-radius", "25px");
            outerDiv.Discret("vertical-align", "middle");
            outerDiv.Discret("cursor", "pointer");
            tool.CSSAdditional.Add(outerDiv);
            CodeCSS innerDiv = new CodeCSS(".innerDiv");
            innerDiv.Discret("width", "auto");
            innerDiv.Discret("height", "auto");
            innerDiv.Discret("padding", "5px");
            innerDiv.Discret("font-size", "11pt");
            innerDiv.Discret("background-color", "#FDFEFE");
            innerDiv.Discret("border", "1px solid white");
            innerDiv.Discret("text-align", "center");
            tool.CSSAdditional.Add(innerDiv);
            sb = new StringBuilder();
            sb.Append("var currentIndex; function onRoll(obj) {  obj.oldBackgroundColor = obj.style.backgroundColor; ");
            sb.Append("obj.oldTextColor = obj.style.color; obj.style.backgroundColor = obj.rollBackColor; obj.style.color = obj.rollColor; }  ");
            sb.Append("function unRoll(obj) { if (obj.oldBorderColor != undefined) { obj.style.borderColor = obj.oldBorderColor; ");
            sb.Append("obj.oldBorderColor = null; } obj.style.backgroundColor = obj.oldBackgroundColor; ");
            sb.Append("obj.style.color = obj.oldTextColor; }   ");
            sb.Append("function onClickDown(obj) { obj.oldBorderColor = obj.style.borderColor; obj.style.borderColor = obj.clickBorderColor; }  ");
            sb.Append("function onClickUp(obj) { if (obj.oldBorderColor != undefined) { obj.style.borderColor = obj.oldBorderColor; ");
            sb.Append("obj.oldBorderColor = null; }  }  ");
            sb.Append("function RaiseArrow(obj) { var img; if (currentIndex) {  img = document.getElementById('imgLeft_' + currentIndex); if (img) { img.src='left.png' }; img = document.getElementById('imgRight_' + currentIndex); if (img) { img.src='right.png' }; };  img = document.getElementById('imgLeft_' + obj.indexValue); if (img) { img.src='left_on.png' }; img = document.getElementById('imgRight_' + obj.indexValue); if (img) { img.src='right_on.png' }; currentIndex = obj.indexValue; }");
            sb.Append("function LeaveArrow() { if (currentIndex) {  img = document.getElementById('imgLeft_' + currentIndex); if (img) { img.src='left.png' }; img = document.getElementById('imgRight_' + currentIndex); if (img) { img.src='right.png' }; }; currentIndex = null; }");

            sb.Append("function onImageRoll(obj) { if (obj.rollSrc != undefined) { obj.saveSrc = obj.src; obj.src = obj.rollSrc; } else { onRoll(obj); } }");
            sb.Append("  function unImageRoll(obj) { if (obj.rollSrc != undefined) { obj.src = obj.saveSrc; } else { unRoll(obj); } }");
            sb.Append("function onImageClickDown(obj) { if (obj.clickSrc != undefined) { obj.saveSrc = obj.src; obj.src = obj.clickSrc; } else { onClickDown(obj); } }  ");
            sb.Append("function onImageClickUp(obj) { if (obj.clickSrc != undefined) { obj.src = obj.saveSrc; } else { onClickUp(obj); } }  ");
            tool.JavaScript.Code = sb.ToString();
            tool.HTML = "";
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.Path = "html";
            tool.Name = "button";
            tool.Id = "buttonObject";
            sb = new StringBuilder();
            sb.Append("<table cellspacing='0' cellpadding='0' width='100%' height='100%'>");
            sb.Append("<tr><td><div onselectstart='javascript:return false;' id='{0}' class='outerDiv'>");
            sb.Append("<div class='innerDiv' rollBackColor='{2}' rollColor='{3}' clickBorderColor='{4}' onmousedown='javascript:onClickDown(this);' onmouseup='javascript:onClickUp(this);' ");
            sb.Append("onmouseover='javascript:onRoll(this);' onmouseout='javascript:unRoll(this);'>{1}</div></div></td></tr></table>");
            tool.HTML = sb.ToString();
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.Path = "html";
            tool.Name = "link";
            tool.Id = "clickableObject";
            sb = new StringBuilder();
            sb.Append("<table cellspacing='0' cellpadding='0' width='100%' height='100%'>");
            sb.Append("<tr><td><div onselectstart='javascript:return false;' id='{0}'>");
            sb.Append("<a href='javascript:void(0);' ondragstart='javascript:return false;'>{1}</a></div></td></tr></table>");
            tool.HTML = sb.ToString();
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.Path = "html";
            tool.Name = "image";
            tool.Id = "imageObject";
            sb = new StringBuilder();
            sb.Append("<table cellspacing='0' cellpadding='0' width='100%' height='100%'>");
            sb.Append("<tr><td><div onselectstart='javascript:return false;'>");
            sb.Append("<img src='{1}' width='{2}px' height='{3}px' id='{0}' ondragstart='javascript:return false;'/></div></td></tr></table>");
            tool.HTML = sb.ToString();
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.Path = "html";
            tool.Name = "clickableImage";
            tool.Id = "clickableImageObject";
            sb = new StringBuilder();
            sb.Append("<table cellspacing='0' cellpadding='0' width='100%' height='100%'>");
            sb.Append("<tr><td><div onselectstart='javascript:return false;' id='{0}'>");
            sb.Append("<img src='{1}' clickSrc='{5}' rollSrc='{6}' rollBackColor='{2}' rollColor='{3}' clickBorderColor='{4}' onmousedown='javascript:onImageClickDown(this);' onmouseup='javascript:onImageClickUp(this);' ");
            sb.Append("onmouseover='javascript:onImageRoll(this);' onmouseout='javascript:unImageRoll(this);' ondragstart='javascript:return false;'/></div></td></tr></table>");
            tool.HTML = sb.ToString();
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
            p.Width = Convert.ToUInt32(window.GetWebBrowser().DisplayRectangle.Width - 40);
            p.Height = Convert.ToUInt32(window.GetWebBrowser().DisplayRectangle.Height - 40);
            p.Disposition = window.Disposition;
            p.ConstraintWidth = EnumConstraint.FIXED;
            p.ConstraintHeight = EnumConstraint.FIXED;
            MasterPage mp = new MasterPage();
            mp.Name = "masterPage_" + window.Name;
            mp.Width = 100;
            mp.Height = 100;
            mp.ConstraintWidth = EnumConstraint.RELATIVE;
            mp.ConstraintHeight = EnumConstraint.RELATIVE;
            mp.CountColumns = 1;
            mp.CountLines = 1;

            HTMLTool def = this.project.Tools.Find(x => x.Path == "html" && x.Name == "default");
            HTMLObject obj = new HTMLObject(def);
            obj.Container = "globalContainer";
            mp.Objects.Add(obj);
            this.project.Instances.Add(obj);

            List<AreaSizedRectangle> rects = new List<AreaSizedRectangle>();
            AreaSizedRectangle sz = new AreaSizedRectangle((int)mp.Width, (int)mp.Height, 1, 1, 0, 0);
            rects.Add(sz);
            mp.MakeZones(rects);
            this.project.MasterPages.Add(mp);

            p.MasterPageName = mp.Name;
            p.Name = window.FileName;

            currentPage = p;
            currentMasterPage = mp;
            currentContainer = mp.HorizontalZones[0].VerticalZones[0].Name;
            currentObject = currentPage;
            foreach (IUXObject child in window.Children)
            {
                RenderControl(child);
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
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "box"));
            obj.Container = this.currentContainer;
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render a button
        /// </summary>
        /// <param name="button">button to render</param>
        public void RenderControl(UXButton button)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "button"));
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, button.Id, button.ButtonText, button.RollBackColor, button.RollColor, button.ClickBorderColor);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render a clickable text
        /// </summary>
        /// <param name="clickText">clickable text</param>
        public void RenderControl(UXClickableText clickText)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "link"));
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, clickText.Id, clickText.Text);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render a clickable text
        /// </summary>
        /// <param name="selectableText">selectable text</param>
        public void RenderControl(UXSelectableText selectableText)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "selectableText"));
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, selectableText.Id, selectableText.RefIndex, selectableText.Text);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render an image
        /// </summary>
        /// <param name="image">image</param>
        public void RenderControl(UXImage image)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "image"));
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, image.Id, image.ImageFile, image.Size.Width, image.Size.Height);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render an image
        /// </summary>
        /// <param name="image">image</param>
        public void RenderControl(UXClickableImage image)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "image"));
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, image.Id, image.ImageFile, image.RollBackColor, image.RollColor, image.ClickBorderColor, image.RollImageFile, image.ClickImageFile);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
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
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "readOnlyText"));
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, text.Text);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render a selectable table
        /// </summary>
        /// <param name="data"></param>
        public void RenderControl(UXViewSelectableDataTable data)
        {
            MasterObject mo = new MasterObject();
            mo.Name = data.Name + "_outer_masterObject";
            mo.Width = 100;
            mo.Height = 100;
            mo.ConstraintWidth = EnumConstraint.RELATIVE;
            mo.ConstraintHeight = EnumConstraint.RELATIVE;
            mo.CountColumns = 1;
            mo.CountLines = 1;
            mo.HTMLBefore = "<div id='" + mo.Name + "_in' onmouseout='javascript:LeaveArrow();'>";
            mo.HTMLAfter = "</div>";

            HorizontalZone h = new HorizontalZone();
            h.ConstraintWidth = EnumConstraint.RELATIVE;
            h.ConstraintHeight = EnumConstraint.RELATIVE;
            h.Width = 100;
            h.Height = 100;
            h.CountLines = 1;
            mo.HorizontalZones.Add(h);

            VerticalZone v = new VerticalZone();
            v.Width = 100;
            v.Height = 100;
            v.Disposition = Disposition.CENTER;
            v.ConstraintWidth = EnumConstraint.RELATIVE;
            v.ConstraintHeight = EnumConstraint.RELATIVE;
            v.CountLines = 1;
            v.CountColumns = 1;
            h.VerticalZones.Add(v);

            this.project.MasterObjects.Add(mo);

            HTMLObject obj = new HTMLObject(mo);
            obj.Container = this.currentContainer;
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);

            currentContainer = mo.HorizontalZones[0].VerticalZones[0].Name;
            RenderControl(data as UXViewDataTable);

        }

        /// <summary>
        /// Render a data table view
        /// </summary>
        /// <param name="data">view to render</param>
        public void RenderControl(UXViewDataTable data)
        {
            RenderControl(data as UXTable);
        }

        /// <summary>
        /// Render a table
        /// </summary>
        /// <param name="table">table to render</param>
        public void RenderControl(UXTable table)
        {
            MasterObject mo = new MasterObject();
            mo.Name = table.Name + "_masterObject";
            mo.Width = 100;
            mo.Height = 100;
            mo.ConstraintWidth = EnumConstraint.RELATIVE;
            mo.ConstraintHeight = EnumConstraint.RELATIVE;
            mo.CountColumns = table.ColumnCount;
            mo.CountLines = table.LineCount;

            for(uint pos_line = 0; pos_line < table.LineCount; ++pos_line)
            {
                UXTable.SizeArgs s = table.HorizontalCustomization[pos_line];
                if (s!= null && s.isValid) {
                    HorizontalZone h = new HorizontalZone();
                    h.ConstraintWidth = s.constraintWidth;
                    h.ConstraintHeight = s.constraintHeight;
                    h.CountLines = s.lineSize;
                    h.Height = Convert.ToUInt32(s.height);
                    h.Width = Convert.ToUInt32(s.width);
                    h.CSS.BackgroundColor = (CSSColor)s.backgroundColor.Clone();
                    h.CSS.ForegroundColor = (CSSColor)s.textColor.Clone();
                    h.CSS.BorderBottomColor = h.CSS.BorderLeftColor = h.CSS.BorderRightColor = h.CSS.BorderTopColor = (CSSColor)s.borderColor.Clone();
                    h.CSS.Border = new Rectangle(s.borderSize, s.borderSize, s.borderSize, s.borderSize);
                    h.CSS.Padding = new Rectangle(s.paddingSize, s.paddingSize, s.paddingSize, s.paddingSize);
                    mo.HorizontalZones.Add(h);
                    for (uint pos_column = 0; pos_column < table.ColumnCount; ++pos_column)
                    {
                        UXTable.SizeArgs s2 = table.VerticalCustomization[pos_line, pos_column];
                        if (s2 != null && s2.isValid)
                        {
                            VerticalZone v = new VerticalZone();
                            v.Width = Convert.ToUInt32(s2.width);
                            v.Height = Convert.ToUInt32(s2.height);
                            v.Disposition = s2.disposition;
                            v.ConstraintWidth = s2.constraintWidth;
                            v.ConstraintHeight = s2.constraintHeight;
                            v.CountLines = s2.lineSize;
                            v.CountColumns = s2.columnSize;
                            v.CSS.BackgroundColor = (CSSColor)s2.backgroundColor.Clone();
                            v.CSS.ForegroundColor = (CSSColor)s2.textColor.Clone();
                            v.CSS.BorderBottomColor = v.CSS.BorderLeftColor = v.CSS.BorderRightColor = v.CSS.BorderTopColor = (CSSColor)s2.borderColor.Clone();
                            v.CSS.Border = new Rectangle(s2.borderSize, s2.borderSize, s2.borderSize, s2.borderSize);
                            v.CSS.Padding = new Rectangle(s2.paddingSize, s2.paddingSize, s2.paddingSize, s2.paddingSize);
                            h.VerticalZones.Add(v);
                        }
                    }
                }
            }

            for (uint pos_line = 0; pos_line < mo.HorizontalZones.Count; ++pos_line)
            {
                HorizontalZone h = mo.HorizontalZones[(int)pos_line];
                for (uint pos_column = 0; pos_column < h.VerticalZones.Count; ++pos_column)
                {
                    VerticalZone v = h.VerticalZones[(int)pos_column];
                    string previousContainer = this.currentContainer;
                    this.currentContainer = v.Name;
                    if (table.VerticalCustomization[pos_line, pos_column].content != null)
                    {
                        table.Add(table.VerticalCustomization[pos_line, pos_column].content);
                        RenderControl(table.VerticalCustomization[pos_line, pos_column].content);
                    }
                    this.currentContainer = previousContainer;
                }
            }

            this.project.MasterObjects.Add(mo);

            HTMLObject obj = new HTMLObject(mo);
            obj.Container = this.currentContainer;
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
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
            else if (obj is UXSelectableText) RenderControl(obj as UXSelectableText);
            else if (obj is UXClickableText) RenderControl(obj as UXClickableText);
            else if (obj is UXClickableImage) RenderControl(obj as UXClickableImage);
            else if (obj is UXImage) RenderControl(obj as UXImage);
            else if (obj is UXFrame) RenderControl(obj as UXFrame);
            else if (obj is UXReadOnlyText) RenderControl(obj as UXReadOnlyText);
            else if (obj is UXViewSelectableDataTable) RenderControl(obj as UXViewSelectableDataTable);
            else if (obj is UXViewDataTable) RenderControl(obj as UXViewDataTable);
            else if (obj is UXTable) RenderControl(obj as UXTable);
            else if (obj is UXWindow) RenderControl(obj as UXWindow);
        }

        #endregion

    }
}
