using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;
using System.IO;
using UXFramework.BeamConnections;

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
        private MasterObject currentMasterObject;

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

            // add js link
            this.project.JavascriptUrls.Add("jquery-ui.1.12/external/jquery/jquery.js");
            this.project.JavascriptUrls.Add("jquery-ui.1.12/jquery-ui.js");

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
            tool.ConstraintHeight = EnumConstraint.AUTO;
            tool.ConstraintWidth = EnumConstraint.AUTO;
            tool.Path = "html";
            tool.Name = "li";
            tool.Id = "liContainer";
            sb = new StringBuilder();
            sb.Append("<li><span itemName='{1}' style='display:inline' onclick='javascript:onTreeItemChanged(this);'>- </span>{0}</li>");
            tool.HTML = sb.ToString();
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
            CodeCSS lineUp = new CodeCSS(".lineUp");
            lineUp.Discret("width", "auto");
            lineUp.Discret("height", "auto");
            lineUp.Discret("padding", "5px");
            lineUp.Discret("font-size", "11pt");
            lineUp.Discret("background-color", "#0000FF");
            lineUp.Discret("color", "#FFFFFF");
            tool.CSSAdditional.Add(lineUp);
            CodeCSS lineDown = new CodeCSS(".lineDown");
            lineDown.Discret("width", "auto");
            lineDown.Discret("height", "auto");
            lineDown.Discret("padding", "5px");
            lineDown.Discret("font-size", "11pt");
            lineDown.Discret("background-color", "#222222");
            lineDown.Discret("color", "#000000");
            tool.CSSAdditional.Add(lineDown);
            sb = new StringBuilder();
            sb.Append("var currentIndex; function onRoll(obj) {  obj.oldBackgroundColor = obj.style.backgroundColor; ");
            sb.Append("obj.oldTextColor = obj.style.color; obj.style.backgroundColor = obj.rollBackColor; obj.style.color = obj.rollColor; }  ");
            sb.Append("function unRoll(obj) { if (obj.oldBorderColor != undefined) { obj.style.borderColor = obj.oldBorderColor; ");
            sb.Append("obj.oldBorderColor = null; } obj.style.backgroundColor = obj.oldBackgroundColor; ");
            sb.Append("obj.style.color = obj.oldTextColor; }   ");
            sb.Append("function onClickDown(obj) { obj.oldBorderColor = obj.style.borderColor; obj.style.borderColor = obj.clickBorderColor; }  ");
            sb.Append("function onClickUp(obj) { if (obj.oldBorderColor != undefined) { obj.style.borderColor = obj.oldBorderColor; ");
            sb.Append("obj.oldBorderColor = null; }  }  ");
            sb.Append("function onSelectLine(obj) { obj.className = 'lineUp'; }  ");
            sb.Append("function onUnselectLine(obj) { obj.className = 'lineDown'; } ");
            sb.Append("function RaiseArrow(obj) { var img; if (currentIndex) {  img = document.getElementById('imgLeft_' + currentIndex); if (img) { img.src='left.png' }; img = document.getElementById('imgRight_' + currentIndex); if (img) { img.src='right.png' }; };  img = document.getElementById('imgLeft_' + obj.indexValue); if (img) { img.src='left_on.png' }; img = document.getElementById('imgRight_' + obj.indexValue); if (img) { img.src='right_on.png' }; currentIndex = obj.indexValue; }");
            sb.Append("function LeaveArrow() { if (currentIndex) {  img = document.getElementById('imgLeft_' + currentIndex); if (img) { img.src='left.png' }; img = document.getElementById('imgRight_' + currentIndex); if (img) { img.src='right.png' }; }; currentIndex = null; }");

            sb.Append("function onImageRoll(obj) { if (obj.rollSrc != undefined) { obj.saveSrc = obj.src; obj.src = obj.rollSrc; } else { onRoll(obj); } }");
            sb.Append("  function unImageRoll(obj) { if (obj.rollSrc != undefined) { obj.src = obj.saveSrc; } else { unRoll(obj); } }");
            sb.Append("function onImageClickDown(obj) { if (obj.clickSrc != undefined) { obj.saveSrc = obj.src; obj.src = obj.clickSrc; } else { onClickDown(obj); } }  ");
            sb.Append("function onImageClickUp(obj) { if (obj.clickSrc != undefined) { obj.src = obj.saveSrc; } else { onClickUp(obj); } }  ");
            sb.Append("function serverSideCall(notif, data) { var p = document.getElementById('serverSideHandler'); p.notif = notif; p.data = data; p.click(); }");
            sb.Append("function onTreeItemChanged(obj) { if (obj.innerText == '+') { obj.innerText = '- '; var i = document.getElementById(obj.itemName); i.style.display='block';} else { obj.innerText = '+'; var i = document.getElementById(obj.itemName); i.style.display='none';}  }");
            tool.JavaScript.Code = sb.ToString();
            tool.HTML = @"<div id='serverSideHandler' style='display:none'></div>";
            this.project.Tools.Add(tool);

            tool = new HTMLTool();
            tool.Path = "html";
            tool.Name = "button";
            tool.Id = "buttonObject";
            sb = new StringBuilder();
            sb.Append("<table cellspacing='0' cellpadding='0' width='100%' height='100%' style='border:1px solid red'>");
            sb.Append("<tr><td><button id='{0}' class='md-raised'>{1}</button></td></tr></table>");
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
        /// Render CSS properties from UX
        /// </summary>
        /// <param name="c">css</param>
        /// <param name="ib">ux properties</param>
        private void RenderCSSProperties(CodeCSS c, BeamConnections.InteractiveBeam ib)
        {
            //foreach (Beam b in ib.GetAllProperties())
            //{
            //    c.Body.Add(b.PropertyName, b.ReadProperty().ToString());
            //}
        }

        /// <summary>
        /// Render an interactive beam
        /// </summary>
        /// <param name="ib">interactive object</param>
        /// <returns>javascript and xml data</returns>
        private string RenderInteractiveBeam(BeamConnections.InteractiveBeam ib)
        {
            string output = "<ul style='display:none' id=''>";
            foreach (BeamConnections.Beam b in ib.GetAllProperties())
            {
                output += "<li name='" + b.PropertyName + "'>" + b.ReadProperty().ToString() + "</li>";
            }
            output += "</ul>";
            return output;
        }

        /// <summary>
        /// Render a window
        /// </summary>
        /// <param name="window">window to render</param>
        public void RenderControl(UXWindow window)
        {
            string previous;
            Projects.Activate(projectName, out previous);
            Page p = new Page();
            p.Width = (window.Properties.Width.HasValue) ? window.Properties.Width.Value : 1320;
            p.Height = (window.Properties.Height.HasValue) ? window.Properties.Height.Value : 700;
            p.Disposition = Disposition.CENTER;
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
            mp.CSS.BackgroundColor = new CSSColor((!String.IsNullOrEmpty(window.Properties.BackColor)) ? window.Properties.BackColor : "Transparent");
            mp.CSS.ForegroundColor = new CSSColor((!String.IsNullOrEmpty(window.Properties.ForeColor)) ? window.Properties.ForeColor : "Black");
            mp.CSS.Body.Add("border", (!String.IsNullOrEmpty(window.Properties.Border)) ? window.Properties.Border : "0px solid black");
            mp.CSS.Body.Add("margin", (!String.IsNullOrEmpty(window.Properties.Margin)) ? window.Properties.Margin : "0,0,0,0");
            mp.CSS.Body.Add("padding", (!String.IsNullOrEmpty(window.Properties.Padding)) ? window.Properties.Padding : "0,0,0,0");
            mp.Meta = "<meta name='viewport' content='initial-scale=1, maximum-scale=1, user-scalable=no'/>";
            this.RenderCSSProperties(mp.CSS, window.Beams);


            HTMLTool def = this.project.Tools.Find(x => x.Path == "html" && x.Name == "default");
            HTMLObject obj = new HTMLObject(def);
            obj.Container = "globalContainer";
            mp.Objects.Add(obj);
            this.project.Instances.Add(obj);

            List<AreaSizedRectangle> rects = new List<AreaSizedRectangle>();
            AreaSizedRectangle sz = new AreaSizedRectangle(0, 0, 1, 1, 0, 0);
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
            this.RenderCSSProperties(obj.CSS, box.Beams);
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
            obj.HTML = String.Format(obj.HTML, button.Id, button.ButtonText);
            obj.JavaScriptOnLoad.Code = String.Format(obj.JavaScriptOnLoad.Code, button.Id);
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
            this.RenderCSSProperties(obj.CSS, clickText.Beams);
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
            this.RenderCSSProperties(obj.CSS, selectableText.Beams);
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
            this.RenderCSSProperties(obj.CSS, image.Beams);
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
            this.RenderCSSProperties(obj.CSS, image.Beams);
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
            this.RenderCSSProperties(obj.CSS, text.Beams);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        public void RenderControl(UXTree t)
        {
            MasterObject mo = new MasterObject();
            mo.Name = t.Name + "_outer_masterObject";
            mo.Width = 100;
            mo.Height = 100;
            mo.ConstraintWidth = EnumConstraint.RELATIVE;
            mo.ConstraintHeight = EnumConstraint.RELATIVE;
            mo.CountColumns = 1;
            mo.CountLines = 1;
            mo.HTMLBefore = "<div id='" + mo.Name + "_in' onmouseout='javascript:LeaveArrow();'>";
            mo.HTMLAfter = "</div>";

            int index = 0;
            string previousContainer = this.currentContainer;
            mo.HTMLBefore = "<ul style='list-item-style:none' id='" + t.Name + "'>";
            mo.HTMLAfter = "</ul>";
            foreach (IUXObject o in t.Children)
            {
                HorizontalZone h = new HorizontalZone();
                h.ConstraintWidth = EnumConstraint.RELATIVE;
                h.ConstraintHeight = EnumConstraint.RELATIVE;
                h.Width = 100;
                h.Height = 100;
                h.CountLines = 1;
                this.RenderCSSProperties(h.CSS, t.Beams);
                mo.HorizontalZones.Add(h);

                VerticalZone v = new VerticalZone();
                v.Width = 100;
                v.Height = 100;
                v.Disposition = Disposition.LEFT_TOP;
                v.ConstraintWidth = EnumConstraint.RELATIVE;
                v.ConstraintHeight = EnumConstraint.RELATIVE;
                v.CountLines = 1;
                v.CountColumns = 1;
                this.RenderCSSProperties(v.CSS, t.Beams);
                h.VerticalZones.Add(v);
                this.currentContainer = mo.HorizontalZones[index++].VerticalZones[0].Name;
                RenderControl(o);

            }

            this.project.MasterObjects.Add(mo);

            this.currentContainer = previousContainer;
            HTMLObject obj = new HTMLObject(mo);
            obj.Container = this.currentContainer;
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);

        }

        public void RenderControl(UXTreeItem item)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "li"));
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, item.Text, item.Name);
            this.RenderCSSProperties(obj.CSS, item.Beams);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
            RenderControl(item.SubItems);
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
            this.RenderCSSProperties(h.CSS, data.Beams);
            mo.HorizontalZones.Add(h);

            VerticalZone v = new VerticalZone();
            v.Width = 100;
            v.Height = 100;
            v.Disposition = Disposition.CENTER;
            v.ConstraintWidth = EnumConstraint.RELATIVE;
            v.ConstraintHeight = EnumConstraint.RELATIVE;
            v.CountLines = 1;
            v.CountColumns = 1;
            this.RenderCSSProperties(v.CSS, data.Beams);
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
            mo.CountColumns = Convert.ToUInt32(table.ColumnCount);
            mo.CountLines = Convert.ToUInt32(table.LineCount);

            MasterObject previousMasterObject = currentMasterObject;
            this.currentMasterObject = mo;
            dynamic previousObject = this.currentObject;
            this.currentObject = mo;
            for (int pos_line = 0; pos_line < table.LineCount; ++pos_line)
            {
                if (table.Children[pos_line] != null)
                    RenderControl(table.Children[pos_line]);
            }
            this.currentObject = previousObject;
            this.currentMasterObject = previousMasterObject;

            this.project.MasterObjects.Add(mo);

            HTMLObject obj = new HTMLObject(mo);
            obj.Container = this.currentContainer;
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        /// <summary>
        /// Render a single row of a table
        /// </summary>
        /// <param name="row">row to render</param>
        public void RenderControl(UXRow row)
        {
            HorizontalZone h = new HorizontalZone();
            h.ConstraintWidth = EnumConstraint.AUTO;
            h.ConstraintHeight = EnumConstraint.FIXED;
            h.CountLines = 1;
            h.Height = 30;
            h.Width = 50;
            this.RenderCSSProperties(h.CSS, row.Beams);
            this.currentObject.HorizontalZones.Add(h);
            dynamic previousObject = this.currentObject;
            for (int pos_column = 0; pos_column < row.ColumnCount; ++pos_column)
            {
                this.currentObject = h;
                if (row.Children[pos_column] != null)
                    RenderControl(row.Children[pos_column]);
            }
            this.currentObject = previousObject;
        }

        /// <summary>
        /// Render a single cell of a table
        /// </summary>
        /// <param name="cell">cell to render</param>
        public void RenderControl(UXCell cell)
        {
            VerticalZone v = new VerticalZone();
            v.Disposition = Disposition.CENTER;
            v.ConstraintWidth = EnumConstraint.AUTO;
            v.ConstraintHeight = EnumConstraint.AUTO;
            v.CountLines = 1;
            v.CountColumns = 1;
            this.RenderCSSProperties(v.CSS, cell.Beams);
            this.currentObject.VerticalZones.Add(v);

            string previousContainer = this.currentContainer;
            dynamic previousObject = this.currentObject;
            this.currentContainer = v.Name;
            this.currentObject = this.currentMasterObject;
            foreach (IUXObject obj in cell.Children)
            {
                RenderControl(obj);
            }
            this.currentContainer = previousContainer;
            this.currentObject = previousObject;
        }

        public void RenderControl(BeamConnections.InteractiveBeam beam)
        {
            if (beam.Exists("clickEvent"))
            {
                BeamConnections.Beam b = beam.GetPropertyValue("clickEvent"); 
                HTMLEvent e = new HTMLEvent("onclick");
                e.NotificationName = "click";
                e.Raise.Add((sender, a) => { return "alert('ok');"; });
                this.currentObject.Events.Add(e);
            }
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
            else if (obj is UXRow) RenderControl(obj as UXRow);
            else if (obj is UXCell) RenderControl(obj as UXCell);
            else if (obj is UXWindow) RenderControl(obj as UXWindow);
            else if (obj is UXTree) RenderControl(obj as UXTree);
            else if (obj is UXTreeItem) RenderControl(obj as UXTreeItem);
            else if (obj is BeamConnections.InteractiveBeam) RenderControl(obj as BeamConnections.InteractiveBeam);
        }

        #endregion

    }
}
