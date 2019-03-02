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
            //this.project.JavascriptUrls.Add("jquery-3.3.1.min.js");
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
            tool.HTML = "<div id='{0}'>{1}</div>";
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
            sb.Append("function serverSideCall(notif, data) { var p = document.getElementById('serverSideHandler'); p.setAttribute('notif', notif); p.setAttribute('data',data); p.click(); }");
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
        /// Render all CSS properties
        /// use a mapping to map ux properties and css properties
        /// </summary>
        /// <param name="control"></param>
        /// <param name="css"></param>
        public void RenderCSSProperties(UXControl control, CodeCSS css)
        {
            control.Get("Width", (s, v) => { css.Body.Add("width", v.Value.ToString() + "px"); });
            control.Get("Height", (s, v) => { css.Body.Add("height", v.Value.ToString() + "px"); });
            control.Get("BackColor", (s, v) => { css.BackgroundColor = new CSSColor(v.Value); });
            control.Get("ForeColor", (s, v) => { css.ForegroundColor = new CSSColor(v.Value); });
            control.Get("Padding", (s, v) => { css.Body.Add("padding", v.Value); });
            control.Get("Margin", (s, v) => { css.Body.Add("margin", v.Value); });
            control.Get("Border", (s, v) => { css.Body.Add("border", v.Value); });
            control.Get("Border-Spacing", (s, v) => { css.Body.Add("border-spacing", v.Value); });
            control.Get("Border-Width", (s, v) => { css.Body.Add("border-width", v.Value + "px"); });
            control.Get("Border-Height", (s, v) => { css.Body.Add("border-height", v.Value + "px"); });
            control.Get("Border-Color", (s, v) => { css.Body.Add("border-color", v.Value + "px"); });
            control.Get("Height-Minimum", (s, v) => { css.Body.Add("min-height", v.Value.ToString() + "px"); });
            control.Get("align", (s, v) => { css.Body.Add("text-align", v.Value); });
            control.Get("valign", (s, v) => { css.Body.Add("vertical-align", v.Value); });
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
            window.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    p.ConstraintWidth = c;
                }
                else
                {
                    p.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            window.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    p.ConstraintHeight = c;
                }
                else
                {
                    p.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            window.Get("Disposition", (s, v) =>
            {
                p.Disposition = Enum.Parse(typeof(Disposition), v.Value);
            });
            window.Get("Width", (x, y) => { p.Width = Convert.ToUInt32(y.Value); });
            window.Get("Height", (x, y) => { p.Height = Convert.ToUInt32(y.Value); });
            MasterPage mp = new MasterPage();
            RenderCSSProperties(window, mp.CSS);
            mp.Name = "masterPage_" + window.Name;
            mp.Width = 100;
            mp.Height = 100;
            mp.ConstraintWidth = EnumConstraint.RELATIVE;
            mp.ConstraintHeight = EnumConstraint.RELATIVE;
            mp.CountColumns = 1;
            mp.CountLines = 1;
            mp.Meta = "<meta name='viewport' content='initial-scale=1, maximum-scale=1, user-scalable=no'/>";


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
            UXTable t = new UXTable();
            UXCell c = new UXCell();
            foreach(IUXObject ux in box.Children)
            {
                c.Add(ux);
            }
            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling("content", () =>
            {
                return new Dictionary<string, dynamic>() {
                    { "ColumnCount", 1 },
                    { "LineCount", 1 },
                    { "children",
                      ChildCollection.CreateChildCollection("row", () =>
                      {
                          return new List<IUXObject>() {
                              Creation.CreateRow(1, null, c)
                          };
                      })
                    }
                };
            });
            t.Bind(hash);
            RenderControl(t);
        }

        /// <summary>
        /// Render a button
        /// </summary>
        /// <param name="button">button to render</param>
        public void RenderControl(UXButton button)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "button"));
            button.Get("Width", (s, v) =>
            {
                obj.Width = Convert.ToUInt32(v.Value);
            });
            button.Get("Height", (s, v) =>
            {
                obj.Height = Convert.ToUInt32(v.Value);
            });
            button.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintWidth = c;
                }
                else
                {
                    obj.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            button.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintHeight = c;
                }
                else
                {
                    obj.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            RenderCSSProperties(button, obj.CSS);
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, button.Id, button.Text);
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
            clickText.Get("Width", (s, v) =>
            {
                obj.Width = Convert.ToUInt32(v.Value);
            });
            clickText.Get("Height", (s, v) =>
            {
                obj.Height = Convert.ToUInt32(v.Value);
            });
            clickText.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintWidth = c;
                }
                else
                {
                    obj.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            clickText.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintHeight = c;
                }
                else
                {
                    obj.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            RenderCSSProperties(clickText, obj.CSS);
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
            selectableText.Get("Width", (s, v) =>
            {
                obj.Width = Convert.ToUInt32(v.Value);
            });
            selectableText.Get("Height", (s, v) =>
            {
                obj.Height = Convert.ToUInt32(v.Value);
            });
            selectableText.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintWidth = c;
                }
                else
                {
                    obj.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            selectableText.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintHeight = c;
                }
                else
                {
                    obj.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            RenderCSSProperties(selectableText, obj.CSS);
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
            image.Get("Width", (s, v) =>
            {
                obj.Width = Convert.ToUInt32(v.Value);
            });
            image.Get("Height", (s, v) =>
            {
                obj.Height = Convert.ToUInt32(v.Value);
            });
            image.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintWidth = c;
                }
                else
                {
                    obj.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            image.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintHeight = c;
                }
                else
                {
                    obj.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            RenderCSSProperties(image, obj.CSS);
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, image.Id, image.ImageFile, image.ImageWidth, image.ImageHeight);
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
            image.Get("Width", (s, v) =>
            {
                obj.Width = Convert.ToUInt32(v.Value);
            });
            image.Get("Height", (s, v) =>
            {
                obj.Height = Convert.ToUInt32(v.Value);
            });
            image.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintWidth = c;
                }
                else
                {
                    obj.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            image.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintHeight = c;
                }
                else
                {
                    obj.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            RenderCSSProperties(image, obj.CSS);
            obj.Container = this.currentContainer;
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
            text.Get("Width", (s, v) =>
            {
                obj.Width = Convert.ToUInt32(v.Value);
            });
            text.Get("Height", (s, v) =>
            {
                obj.Height = Convert.ToUInt32(v.Value);
            });
            text.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintWidth = c;
                }
                else
                {
                    obj.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            text.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    obj.ConstraintHeight = c;
                }
                else
                {
                    obj.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            RenderCSSProperties(text, obj.CSS);
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, text.Text);
            this.currentObject.Objects.Add(obj);
            this.project.Instances.Add(obj);
        }

        public void RenderControl(UXTree t)
        {
            RenderControl(t as UXTable);
        }

        public void RenderControl(UXTreeItem item)
        {
            HTMLObject obj = new HTMLObject(this.project.Tools.Find(x => x.Path == "html" && x.Name == "li"));
            RenderCSSProperties(item, obj.CSS);
            obj.Container = this.currentContainer;
            obj.HTML = String.Format(obj.HTML, item.Text, item.Name);
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
            RenderCSSProperties(data, mo.CSS);
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
            data.Get("Disposition", (s, x) =>
            {
                v.Disposition = Enum.Parse(typeof(Disposition), x.Value);
            });
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
            RenderCSSProperties(table, mo.CSS);
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
                if (table.Children.ElementAt(pos_line) != null)
                    RenderControl(table.Children.ElementAt(pos_line));
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
            RenderCSSProperties(row, h.CSS);
            row.Get("Width", (s, v) =>
            {
                h.Width = Convert.ToUInt32(v.Value);
            });
            row.Get("Height", (s, v) =>
            {
                h.Height = Convert.ToUInt32(v.Value);
            });
            row.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    h.ConstraintWidth = c;
                }
                else
                {
                    h.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            row.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    h.ConstraintHeight = c;
                }
                else
                {
                    h.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            h.CountLines = 1;
            this.currentObject.HorizontalZones.Add(h);
            dynamic previousObject = this.currentObject;
            string normalBackground = "Transparent";
            row.Get("BackColor", (s, v) => { normalBackground = v.Value; });
            if (row.IsSelectable)
            {
                HTMLEvent ev = new HTMLEvent("onmouseover");
                ev.Raise.Add((o, e) => {
                    return "this.style.backgroundColor = \"" + row.BackgroundSelectable + "\";";
                });
                h.Events.Add(ev);
                ev = new HTMLEvent("onmouseout");
                ev.Raise.Add((o, e) => {
                    return "this.style.backgroundColor = \"" + normalBackground + "\";";
                });
                h.Events.Add(ev);
            }
            if (row.IsClickable)
            {
                HTMLEvent ev = new HTMLEvent("onclick");
                ev.Raise.Add((o, e) =>
                {
                    return "this.style.backgroundColor = \"" + row.BackgroundClickable + "\"; serverSideCall(\"row\",\"" + row.Id + "\");";
                });
                h.Events.Add(ev);
            }
            for (int pos_column = 0; pos_column < row.ColumnCount; ++pos_column)
            {
                this.currentObject = h;
                if (row.Children.ElementAt(pos_column) != null)
                    RenderControl(row.Children.ElementAt(pos_column));
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
            cell.Get("Width", (s, x) =>
            {
                v.Width = Convert.ToUInt32(x.Value);
            });
            cell.Get("Height", (s, x) =>
            {
                v.Height = Convert.ToUInt32(x.Value);
            });
            RenderCSSProperties(cell, v.CSS);
            cell.Get("Disposition", (s,x) =>
            {
                v.Disposition = Enum.Parse(typeof(Disposition), x.Value);
            });
            cell.Get("Constraint-Width", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    v.ConstraintWidth = c;
                }
                else
                {
                    v.ConstraintWidth = EnumConstraint.AUTO;
                }
            });
            cell.Get("Constraint-Height", (x, y) =>
            {
                EnumConstraint c;
                if (Enum.TryParse<EnumConstraint>(y.Value, out c))
                {
                    v.ConstraintHeight = c;
                }
                else
                {
                    v.ConstraintHeight = EnumConstraint.AUTO;
                }
            });
            v.CountLines = 1;
            v.CountColumns = 1;
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
            else if (obj is UXTree) RenderControl(obj as UXTree);
            else if (obj is UXTable) RenderControl(obj as UXTable);
            else if (obj is UXRow) RenderControl(obj as UXRow);
            else if (obj is UXCell) RenderControl(obj as UXCell);
            else if (obj is UXWindow) RenderControl(obj as UXWindow);
            else if (obj is UXTreeItem) RenderControl(obj as UXTreeItem);
            else if (obj is BeamConnections.InteractiveBeam) RenderControl(obj as BeamConnections.InteractiveBeam);
        }

        #endregion

    }
}
