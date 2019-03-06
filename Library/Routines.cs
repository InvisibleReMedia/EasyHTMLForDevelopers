using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CommonDirectories;

namespace Library
{
    static class Routines
    {
        public static string PrintTipSize(string objName, string container, ConstraintSize cs)
        {
            string output = String.Empty;
            if (!String.IsNullOrEmpty(objName))
            {
                output = objName + "_" + container + ":";
            }
            else
            {
                output = container + ":";
            }
            if (cs.constraintWidth == EnumConstraint.AUTO)
            {
                output += "auto" + " x ";
            }
            else
            {
                output += cs.widthString + " x ";
            }
            if (cs.constraintHeight == EnumConstraint.AUTO)
            {
                output += "auto";
            }
            else
            {
                output += cs.heightString;
            }
            return output;
        }

        public static void SetCSSBodyPart(CodeCSS css, ConstraintSize cs)
        {
            // CSS du tag BODY
            if (css.Body.AllKeys.Contains("width"))
                css.Body.Remove("width");
            switch (cs.constraintWidth)
            {
                case EnumConstraint.AUTO:
                    break;
                case EnumConstraint.RELATIVE:
                    break;
                default:
                    if (!String.IsNullOrEmpty(cs.widthString))
                        css.Body.Add("width", cs.widthString);
                    break;
            }
            if (css.Body.AllKeys.Contains("height"))
                css.Body.Remove("height");
            switch (cs.constraintHeight)
            {
                case EnumConstraint.AUTO:
                    break;
                case EnumConstraint.RELATIVE:
                    break;
                default:
                    if (!String.IsNullOrEmpty(cs.heightString))
                        css.Body.Add("height", cs.heightString);
                    break;
            }
        }

        public static void SetCSSPart(CodeCSS css, ConstraintSize cs)
        {
            // CSS du tag BODY
            if (css.Body.AllKeys.Contains("width"))
                css.Body.Remove("width");
            switch (cs.constraintWidth)
            {
                case EnumConstraint.AUTO:
                    break;
                default:
                    if (!String.IsNullOrEmpty(cs.widthString))
                        css.Body.Add("width", cs.widthString);
                    break;
            }
            if (css.Body.AllKeys.Contains("height"))
                css.Body.Remove("height");
            switch (cs.constraintHeight)
            {
                case EnumConstraint.AUTO:
                    break;
                default:
                    if (!String.IsNullOrEmpty(cs.heightString))
                        css.Body.Add("height", cs.heightString);
                    break;
            }
        }

        private static void WriteCSSToFile(StringBuilder css, DesignPage config)
        {
            if (config.cssOnFile)
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "file.css", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(css.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                CommonDirectories.ConfigDirectories.AddFile(Project.CurrentProject.Title, 
                                                            config.cssFile,
                                                            AppDomain.CurrentDomain.BaseDirectory + "file.css");
            }
        }

        private static void WriteCSSToProductionFile(StringBuilder css, DesignPage config)
        {
            if (config.cssOnFile)
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "file.css", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(css.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                CommonDirectories.ConfigDirectories.AddFile(Project.CurrentProject.Title,
                                                            config.cssFile,
                                                            AppDomain.CurrentDomain.BaseDirectory + "file.css");
            }
        }

        private static void WriteJavaScriptToFile(StringBuilder javascript, DesignPage config)
        {
            if (config.javascriptOnFile)
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "file.js", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(javascript.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                CommonDirectories.ConfigDirectories.AddFile(Project.CurrentProject.Title, config.javascriptFile, AppDomain.CurrentDomain.BaseDirectory + "file.js");
            }
        }

        private static void WriteJavaScriptToProductionFile(StringBuilder javascript, DesignPage config)
        {
            if (config.javascriptOnFile)
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + config.javascriptFile, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(javascript.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                CommonDirectories.ConfigDirectories.AddFile(Project.CurrentProject.Title, config.javascriptFile, AppDomain.CurrentDomain.BaseDirectory + "file.js");
            }
        }

        public static void WriteGlobalContainer(OutputHTML html, List<HTMLObject> objects, Page refPage, MasterPage masterRefPage, ParentConstraint parent)
        {
            OutputHTML contents = new OutputHTML();
            html.HTML.Append("<div style='display:none' id='suppress'></div>");
            html.HTML.Append("<div width='100%' style='text-align:left'>");
            foreach (HTMLObject obj in objects)
            {
                if (obj.Container == "globalContainer")
                {
                    html.HTML.Append(obj.Title + " (" + obj.Name + ")");
                    html.HTML.Append("<input type='image' src='ehd_minus.png' onclick='var suppress = document.getElementById(\"suppress\"); suppress.objectName=\"" + obj.Name + "\"; suppress.click();'>");
                    html.HTML.Append("<br/>");
                }
            }
            foreach (HTMLObject obj in objects)
            {
                if (obj.Container == "globalContainer")
                {
                    OutputHTML zone = obj.GenerateDesign(refPage, masterRefPage, parent);
                    contents.HTML.Append(zone.HTML.ToString());
                    contents.CSS.Append(zone.CSS.ToString());
                    contents.JavaScript.Append(zone.JavaScript.ToString());
                    contents.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                }
            }

            html.HTML.Append("<br/><input type='image' src='ehd_plus.png' onclick='var callback = document.getElementById(\"callback\"); callback.container=\"globalContainer\"; callback.click();'/>");
            html.HTML.Append("</div>");
            html.HTML.Append(contents.HTML.ToString());
            html.CSS.Append(contents.CSS.ToString());
            html.JavaScript.Append(contents.JavaScript.ToString());
            html.JavaScriptOnLoad.Append(contents.JavaScriptOnLoad.ToString());
        }

        public static bool WriteProductionGlobalContainer(string moName, string moId, OutputHTML html, List<HTMLObject> objects, Page refPage, MasterPage masterRefPage, ParentConstraint parent, ConstraintSize cs)
        {
            bool notEmpty = false;
            ConstraintSize globalContainerCs = new ConstraintSize(cs.constraintWidth, (cs.width > 0) ? cs.width - 1 : 0, cs.forcedWidth, cs.constraintHeight, (cs.height > 0) ? cs.height - 1 : 0, cs.forcedHeight);

            html.HTML.Append("<div");
            html.HTML.Append(" style='position:absolute'");
            html.HTML.Append(" name='globalContainer_" + moName + "'");
            html.HTML.Append(" id='globalContainer_" + moId + "'");
            if (!String.IsNullOrEmpty(globalContainerCs.attributeWidth))
                html.HTML.Append(" " + globalContainerCs.attributeWidth);
            if (!String.IsNullOrEmpty(globalContainerCs.attributeHeight))
                html.HTML.Append(" " + globalContainerCs.attributeHeight);
            html.HTML.Append(">");

            foreach (HTMLObject obj in objects)
            {
                if (obj.Container == "globalContainer")
                {
                    OutputHTML zone = obj.GenerateProduction(refPage, masterRefPage, parent);
                    html.HTML.Append(zone.HTML.ToString());
                    html.CSS.Append(zone.CSS.ToString());
                    html.JavaScript.Append(zone.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                    notEmpty = true;
                }
            }
            html.HTML.Append("</div>");
            return notEmpty;
        }

        public static bool WriteProductionGlobalContainer(string moName, string moId, OutputHTML html, List<HTMLObject> objects, Page refPage, MasterPage masterRefPage, List<MasterObject> masterObjects, ParentConstraint parent, ConstraintSize cs)
        {
            bool notEmpty = false;
            ConstraintSize globalContainerCs = new ConstraintSize(cs.constraintWidth, (cs.width > 0) ? cs.width - 1 : 0, cs.forcedWidth, cs.constraintHeight, (cs.height > 0) ? cs.height - 1 : 0, cs.forcedHeight);

            html.HTML.Append("<div");
            html.HTML.Append(" style='margin-top:auto;margin-bottom:auto;margin-left:0;margin-right:auto'");
            html.HTML.Append(" name='globalContainer_" + moName + "'");
            html.HTML.Append(" id='globalContainer_" + moId + "'");
            if (!String.IsNullOrEmpty(globalContainerCs.attributeWidth))
                html.HTML.Append(" " + globalContainerCs.attributeWidth);
            if (!String.IsNullOrEmpty(globalContainerCs.attributeHeight))
                html.HTML.Append(" " + globalContainerCs.attributeHeight);
            html.HTML.Append(">");

            foreach (HTMLObject obj in objects)
            {
                if (obj.Container == "globalContainer")
                {
                    OutputHTML zone = obj.GenerateProduction(refPage, masterRefPage, masterObjects, parent);
                    html.HTML.Append(zone.HTML.ToString());
                    html.CSS.Append(zone.CSS.ToString());
                    html.JavaScript.Append(zone.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                    notEmpty = true;
                }
            }
            html.HTML.Append("</div>");
            return notEmpty;
        }

        public static void WriteGlobalContainer(OutputHTML html, List<HTMLObject> objects, Page refPage, List<MasterObject> masterObjects, ParentConstraint parent)
        {
            OutputHTML contents = new OutputHTML();
            html.HTML.Append("<div style='display:none' id='suppress'></div>");
            html.HTML.Append("<div width='100%' style='text-align:left'>");
            foreach (HTMLObject obj in objects)
            {
                if (obj.Container == "globalContainer")
                {
                    html.HTML.Append(obj.Title + " (" + obj.Name + ")");
                    html.HTML.Append("<input type='image' src='ehd_minus.png' onclick='var suppress = document.getElementById(\"suppress\"); suppress.objectName=\"" + obj.Name + "\"; suppress.click();'>");
                    html.HTML.Append("<br/>");
                }
            }
            foreach (HTMLObject obj in objects)
            {
                if (obj.Container == "globalContainer")
                {
                    OutputHTML zone = obj.GenerateDesign(refPage, masterObjects, parent);
                    contents.HTML.Append(zone.HTML.ToString());
                    contents.CSS.Append(zone.CSS.ToString());
                    contents.JavaScript.Append(zone.JavaScript.ToString());
                    contents.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                }
            }
            html.HTML.Append("<br/><input type='image' src='ehd_plus.png' onclick='var callback = document.getElementById(\"callback\"); callback.container=\"globalContainer\"; callback.click();'/>");
            html.HTML.Append("</div>");
            html.HTML.Append(contents.HTML.ToString());
            html.CSS.Append(contents.CSS.ToString());
            html.JavaScript.Append(contents.JavaScript.ToString());
            html.JavaScriptOnLoad.Append(contents.JavaScriptOnLoad.ToString());
        }

        public static OutputHTML GenerateDesignPageDIV(Page refPage, MasterPage master, DesignPage pageConfig)
        {
            Project.InitializeTraceCounter();
            string myId = "master" + Project.IncrementedTraceCounter.ToString();
            CodeCSS body = new CodeCSS(master.CSS);
            CodeCSS myCss = new CodeCSS();
            // create specific output page
            refPage.SpecificOutput = new OutputHTML();
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + ConfigDirectories.GetBuildFolder(Project.CurrentProject.Title) + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            outputPage.HTML.Append(master.Meta);
            OutputHTML html = new OutputHTML();
            myCss.Ids = "#" + myId;
            body.Ids = "body";


            // compute size
            ConstraintSize cs = new ConstraintSize(refPage.ConstraintWidth, refPage.Width, refPage.Width, refPage.ConstraintHeight, refPage.Height, refPage.Height);
            Routines.SetCSSBodyPart(body, cs);
            // prepare parent
            ParentConstraint parent = Routines.ComputeMasterPageInPage(refPage, master);

            // recompute size
            cs = new ConstraintSize(parent.constraintWidth, parent.precedingWidth, parent.maximumWidth, parent.constraintHeight, parent.precedingHeight, parent.maximumHeight);
            Routines.SetCSSPart(myCss, cs);

            OutputHTML global = new OutputHTML();
            if (pageConfig.includeContainer)
            {
                Routines.WriteGlobalContainer(global, pageConfig.subObjects, refPage, master, parent);
                html.CSS.Append(global.CSS.ToString());
                html.JavaScript.Append(global.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
            }

            html.CSS.Append(body.GenerateCSS(true, true, true));
            html.CSS.Append(pageConfig.cssList.GenerateCSSWithoutPrincipal("body", true, true));
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(pageConfig.javascriptPart.GeneratedCode);
            html.JavaScriptOnLoad.Append(pageConfig.onload.GeneratedCode);

            html.HTML.Append("<div");
            html.HTML.Append(" id='" + myId + "'");
            html.HTML.Append(" name='" + master.Name + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                html.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                html.HTML.Append(" " + cs.attributeHeight);
            html.HTML.Append(">");

            foreach (HorizontalZone hz in pageConfig.zones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, master, parent);
                html.HTML.Append(hzone.HTML.ToString());
                html.CSS.Append(hzone.CSS.ToString());
                html.JavaScript.Append(hzone.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            html.HTML.Append("</div>");
            if (!pageConfig.cssOnFile)
            {
                outputPage.HTML.Append("<style>");
                outputPage.HTML.Append(html.CSS.ToString());
                outputPage.HTML.Append("</style>");
            }
            else
            {
                outputPage.HTML.Append("<link rel='stylesheet' type='text/css' href='" + pageConfig.cssFile + "'>");
                Routines.WriteCSSToFile(html.CSS, pageConfig);
            }
            // write specific output page
            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(refPage.SpecificOutput.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='javascript' type='text/javascript'>");
            outputPage.HTML.Append("function callback(obj) {");
            outputPage.HTML.Append("var call = document.getElementById('callback'); call.setAttribute('container', obj.getAttribute('name')); call.click();");
            outputPage.HTML.Append("}");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            // write specific output page
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append("</script>");
            if (!pageConfig.javascriptOnFile)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
                outputPage.HTML.Append(html.JavaScript.ToString());
                outputPage.HTML.Append("</script>");
            }
            else
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + pageConfig.javascriptFile + "'>");
                Routines.WriteJavaScriptToFile(html.JavaScript, pageConfig);
                outputPage.HTML.Append("</script>");
            }
            // write specific output page
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();'>");
            if (pageConfig.includeContainer)
                outputPage.HTML.Append(global.HTML.ToString());
            Routines.SetDIVDisposition(outputPage.HTML, refPage.Disposition, html.HTML);
            outputPage.HTML.Append("<div id='callback' style='display:none'></div>");
            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static OutputHTML GenerateProductionPageDIV(Page refPage, MasterPage master, DesignPage pageConfig)
        {
            Project.InitializeTraceCounter();
            CodeCSS body = new CodeCSS(master.CSS);
            string myId = "master" + Project.IncrementedTraceCounter.ToString();
            CodeCSS myCss = new CodeCSS();
            // create specific output page
            refPage.SpecificOutput = new OutputHTML();
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + Project.CurrentProject.Configuration.Replace("#(BASE_HREF)") + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            outputPage.HTML.Append(master.Meta);
            OutputHTML html = new OutputHTML();
            myCss.Ids = "#" + myId;
            body.Ids = "body";


            // compute size
            ConstraintSize cs = new ConstraintSize(refPage.ConstraintWidth, refPage.Width, refPage.Width, refPage.ConstraintHeight, refPage.Height, refPage.Height);
            Routines.SetCSSBodyPart(body, cs);
            // prepare parent
            ParentConstraint parent = Routines.ComputeMasterPageInPage(refPage, master);

            // recompute size
            cs = new ConstraintSize(parent.constraintWidth, parent.precedingWidth, parent.maximumWidth, parent.constraintHeight, parent.precedingHeight, parent.maximumHeight);
            Routines.SetCSSPart(myCss, cs);

            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = false;
            if (pageConfig.includeContainer)
            {
                hasGlobalContainer = Routines.WriteProductionGlobalContainer(refPage.Name, myId, global, pageConfig.subObjects, refPage, master, parent, cs);
                if (hasGlobalContainer)
                {
                    html.CSS.Append(global.CSS.ToString());
                    html.JavaScript.Append(global.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
                }
            }

            html.CSS.Append(body.GenerateCSS(true, true, true));
            html.CSS.Append(master.CSSList.GenerateCSSWithoutPrincipal("body", true, true));
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(pageConfig.javascriptPart.GeneratedCode);
            html.JavaScriptOnLoad.Append(pageConfig.onload.GeneratedCode);

            html.HTML.Append("<div");
            html.HTML.Append(" id='" + myId + "'");
            html.HTML.Append(" name='" + master.Name + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                html.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                html.HTML.Append(" " + cs.attributeHeight);
            html.HTML.Append(">");

            foreach (HorizontalZone hz in pageConfig.zones)
            {
                OutputHTML hzone = hz.GenerateProductionDIV(refPage, master, parent);
                html.HTML.Append(hzone.HTML.ToString());
                html.CSS.Append(hzone.CSS.ToString());
                html.JavaScript.Append(hzone.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            html.HTML.Append("</div>");

            if (!pageConfig.cssOnFile)
            {
                outputPage.HTML.Append("<style>");
                outputPage.HTML.Append(html.CSS.ToString());
                outputPage.HTML.Append("</style>");
            }
            else
            {
                outputPage.HTML.Append("<link rel='stylesheet' type='text/css' href='" + pageConfig.cssFile + "'>");
                Routines.WriteCSSToProductionFile(html.CSS, pageConfig);
            }
            // write specific output page
            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(refPage.SpecificOutput.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='javascript' type='text/javascript'>");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            // write specific output page
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append("</script>");
            if (!pageConfig.javascriptOnFile)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
                outputPage.HTML.Append(html.JavaScript.ToString());
                outputPage.HTML.Append("</script>");
            }
            else
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + pageConfig.javascriptFile + "'>");
                Routines.WriteJavaScriptToProductionFile(html.JavaScript, pageConfig);
                outputPage.HTML.Append("</script>");
            }
            // write specific output page
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();'>");
            if (hasGlobalContainer)
            {
                outputPage.HTML.Append(global.HTML.ToString());
            }
            Routines.SetDIVDisposition(outputPage.HTML, refPage.Disposition, html.HTML);
            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static OutputHTML GenerateDesignPageDIV(Page refPage, MasterObject master, DesignPage pageConfig)
        {
            Project.InitializeTraceCounter();
            string myId = "master" + Project.IncrementedTraceCounter.ToString();
            CodeCSS myCss = new CodeCSS(master.CSS);
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + ConfigDirectories.GetBuildFolder(Project.CurrentProject.Title) + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            OutputHTML html = new OutputHTML();
            myCss.Ids = "#" + myId;

            // compute size
            ConstraintSize cs = new ConstraintSize(pageConfig.constraintWidth, pageConfig.width, 500, pageConfig.constraintHeight, pageConfig.height, 500);

            // compute border
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.CSS, master.CountLines, master.CountColumns);

            // prepare parent
            ParentConstraint parent = new ParentConstraint("", cs.width, cs.height, cs.constraintWidth, cs.constraintHeight, pageConfig.width, pageConfig.height, bc);
            ParentConstraint computed = Routines.ComputeMasterObject(parent, master);
            // recompute size
            cs = new ConstraintSize(computed.constraintWidth, computed.precedingWidth, computed.maximumWidth, computed.constraintHeight, computed.precedingHeight, computed.maximumHeight);
            Routines.SetCSSPart(myCss, cs);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(master);
            OutputHTML global =new OutputHTML();
            if (pageConfig.includeContainer) {
                Routines.WriteGlobalContainer(global, pageConfig.subObjects, refPage, list, parent);
                html.CSS.Append(global.CSS.ToString());
                html.JavaScript.Append(global.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
            }

            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.CSS.Append(master.CSSList.GenerateCSSWithoutPrincipal("body", true, true));
            html.JavaScript.Append(pageConfig.javascriptPart.GeneratedCode);
            html.JavaScriptOnLoad.Append(pageConfig.onload.GeneratedCode);
            html.HTML.Append(master.HTMLBefore);

            html.HTML.Append("<div");
            html.HTML.Append(" id='" + myId + "'");
            html.HTML.Append(" name='" + master.Name + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                html.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                html.HTML.Append(" " + cs.attributeHeight);
            html.HTML.Append(">");

            foreach (HorizontalZone hz in pageConfig.zones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, list, computed);
                html.HTML.Append(hzone.HTML.ToString());
                html.CSS.Append(hzone.CSS.ToString());
                html.JavaScript.Append(hzone.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            html.HTML.Append("</div>");
            html.HTML.Append(master.HTMLAfter);

            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(html.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='javascript' type='text/javascript'>");
            outputPage.HTML.Append("function callback(obj) {");
            outputPage.HTML.Append("var call = document.getElementById('callback'); call.setAttribute('container', obj.getAttribute('name')); call.click();");
            outputPage.HTML.Append("}");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            if (pageConfig.includeContainer)
                outputPage.HTML.Append(global.HTML.ToString());
            outputPage.HTML.Append(html.HTML.ToString());
            outputPage.HTML.Append("<div id='callback' style='display:none'></div>");
            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static OutputHTML GenerateDesignPageTable(Page refPage, MasterPage master, DesignPage pageConfig)
        {
            Project.InitializeTraceCounter();
            CodeCSS myCss = new CodeCSS(master.CSS);
            CodeCSS body = new CodeCSS(master.CSS);
            // create specific output page
            refPage.SpecificOutput = new OutputHTML();
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + ConfigDirectories.GetBuildFolder(Project.CurrentProject.Title) + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            outputPage.HTML.Append(master.Meta);
            OutputHTML html = new OutputHTML();

            // compute size
            ConstraintSize cs = new ConstraintSize(refPage.ConstraintWidth, refPage.Width, refPage.Width, refPage.ConstraintHeight, refPage.Height, refPage.Height);
            Routines.SetCSSPart(myCss, cs);

            // compute borders
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.CSS, master.CountLines, master.CountColumns);

            // prepare parent
            ParentConstraint parent = new ParentConstraint("", cs.width, cs.height, cs.constraintWidth, cs.constraintHeight, master.Width, master.Height, bc);

            OutputHTML global = new OutputHTML();
            if (pageConfig.includeContainer)
            {
                Routines.WriteGlobalContainer(global, pageConfig.subObjects, refPage, master, parent);
                html.CSS.Append(global.CSS.ToString());
                html.JavaScript.Append(global.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
            }

            html.CSS.Append(body.GenerateCSS(true, true, true));
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.CSS.Append(pageConfig.cssList.GenerateCSS(true, true));
            html.JavaScript.Append(pageConfig.javascriptPart.GeneratedCode);
            html.JavaScriptOnLoad.Append(pageConfig.onload.GeneratedCode);

            html.HTML.Append("<table");
            html.HTML.Append(" " + Routines.SetTableDisposition(refPage.Disposition));
            html.HTML.Append(" name='globalTable'");
            html.HTML.Append(" id='" + master.Name + "'");
            html.HTML.Append(" border='0' cellspacing='0' cellpadding='0'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                html.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                html.HTML.Append(" " + cs.attributeHeight);
            html.HTML.Append(">");

            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = pageConfig.zones.Count;
            for (int index = pageConfig.zones.Count - 1; index >= 0; --index)
            {
                if (pageConfig.zones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = pageConfig.zones[index];
                if (index + 1 < pageConfig.zones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, master, parent);
                    html.HTML.Append(hzone.HTML.ToString());
                    html.CSS.Append(hzone.CSS.ToString());
                    html.JavaScript.Append(hzone.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            html.HTML.Append("</table>");
            if (!pageConfig.cssOnFile)
            {
                outputPage.HTML.Append("<style>");
                outputPage.HTML.Append(html.CSS.ToString());
                outputPage.HTML.Append("</style>");
            }
            else
            {
                outputPage.HTML.Append("<link rel='stylesheet' type='text/css' href='" + pageConfig.cssFile + "'>");
                Routines.WriteCSSToFile(html.CSS, pageConfig);
            }
            // specific output page
            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(refPage.SpecificOutput.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append("function callback(obj) {");
            outputPage.HTML.Append("var call = document.getElementById('callback'); call.setAttribute('container', obj.getAttribute('name')); call.click();");
            outputPage.HTML.Append("}");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            // specific outputpage
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append("</script>");
            if (!pageConfig.javascriptOnFile)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
                outputPage.HTML.Append(html.JavaScript.ToString());
                outputPage.HTML.Append("</script>");
            }
            else
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + pageConfig.javascriptFile + "'>");
                Routines.WriteJavaScriptToFile(html.JavaScript, pageConfig);
                outputPage.HTML.Append("</script>");
            }
            // specific output page
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            if (pageConfig.includeContainer)
                outputPage.HTML.Append(global.HTML.ToString());
            outputPage.HTML.Append(html.HTML.ToString());
            outputPage.HTML.Append("<div id='callback' style='display:none'></div>");
            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static OutputHTML GenerateProductionPageTable(Page refPage, MasterPage master, DesignPage pageConfig)
        {
            Project.InitializeTraceCounter();
            string myId = "master" + Project.IncrementedTraceCounter.ToString();
            CodeCSS body = new CodeCSS(master.CSS);
            CodeCSS myCss = new CodeCSS();
            // create specific output page
            refPage.SpecificOutput = new OutputHTML();
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + Project.CurrentProject.Configuration.Replace("#(BASE_HREF)") + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            outputPage.HTML.Append(master.Meta);
            OutputHTML html = new OutputHTML();
            myCss.Ids = "#" + myId;
            body.Ids = "body";

            // compute size
            ConstraintSize cs = new ConstraintSize(refPage.ConstraintWidth, refPage.Width, refPage.Width, refPage.ConstraintHeight, refPage.Height, refPage.Height);
            Routines.SetCSSBodyPart(body, cs);

            // compute borders
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.CSS, master.CountLines, master.CountColumns);

            // prepare parent
            ParentConstraint parent = new ParentConstraint("", cs.width, cs.height, cs.constraintWidth, cs.constraintHeight, master.Width, master.Height, bc);

            // recompute size
            cs = new ConstraintSize(parent.constraintWidth, parent.precedingWidth, parent.maximumWidth, parent.constraintHeight, parent.precedingHeight, parent.maximumHeight);
            Routines.SetCSSPart(myCss, cs);

            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = false;
            if (pageConfig.includeContainer)
            {
                hasGlobalContainer = Routines.WriteProductionGlobalContainer(refPage.Name, myId, global, pageConfig.subObjects, refPage, master, parent, cs);
                if (hasGlobalContainer)
                {
                    html.CSS.Append(global.CSS.ToString());
                    html.JavaScript.Append(global.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
                }
            }

            html.CSS.Append(body.GenerateCSS(true, true, true));
            html.CSS.Append(pageConfig.cssList.GenerateCSSWithoutPrincipal("body", true, true));
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(pageConfig.javascriptPart.GeneratedCode);
            html.JavaScriptOnLoad.Append(pageConfig.onload.GeneratedCode);

            html.HTML.Append("<table");
            html.HTML.Append(" " + Routines.SetTableDisposition(refPage.Disposition));
            html.HTML.Append(" name='globalTable'");
            html.HTML.Append(" id='" + master.Name + "'");
            html.HTML.Append(" border='0' cellspacing='0' cellpadding='0'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                html.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                html.HTML.Append(" " + cs.attributeHeight);
            html.HTML.Append(">");

            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = pageConfig.zones.Count;
            for (int index = pageConfig.zones.Count - 1; index >= 0; --index)
            {
                if (pageConfig.zones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = pageConfig.zones[index];
                if (index + 1 < pageConfig.zones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateProductionTable(refPage, master, parent);
                    html.HTML.Append(hzone.HTML.ToString());
                    html.CSS.Append(hzone.CSS.ToString());
                    html.JavaScript.Append(hzone.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            html.HTML.Append("</table>");
            if (!pageConfig.cssOnFile)
            {
                outputPage.HTML.Append("<style>");
                outputPage.HTML.Append(html.CSS.ToString());
                outputPage.HTML.Append("</style>");
            }
            else
            {
                outputPage.HTML.Append("<link rel='stylesheet' type='text/css' href='" + pageConfig.cssFile + "'>");
                Routines.WriteCSSToProductionFile(html.CSS, pageConfig);
            }
            // specific output page
            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(refPage.SpecificOutput.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            // specific outputpage
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append("</script>");
            if (!pageConfig.javascriptOnFile)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
                outputPage.HTML.Append(html.JavaScript.ToString());
                outputPage.HTML.Append("</script>");
            }
            else
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + pageConfig.javascriptFile + "'>");
                Routines.WriteJavaScriptToProductionFile(html.JavaScript, pageConfig);
                outputPage.HTML.Append("</script>");
            }
            // specific output page
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append(refPage.SpecificOutput.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            if (hasGlobalContainer)
                outputPage.HTML.Append(global.HTML.ToString());
            Routines.SetDIVDisposition(outputPage.HTML, refPage.Disposition, html.HTML);

            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static OutputHTML GenerateDesignPageTable(Page refPage, MasterObject master, DesignPage pageConfig)
        {
            Project.InitializeTraceCounter();
            string myId = "master" + Project.IncrementedTraceCounter.ToString();
            CodeCSS myCss = new CodeCSS(master.CSS);
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + ConfigDirectories.GetBuildFolder(Project.CurrentProject.Title) + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            OutputHTML html = new OutputHTML();
            myCss.Ids = "#" + myId;

            // compute size
            ConstraintSize cs = new ConstraintSize(pageConfig.constraintWidth, pageConfig.width, 500, pageConfig.constraintHeight, pageConfig.height, 500);

            // compute borders
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.CSS, master.CountLines, master.CountColumns);

            // prepare parent
            ParentConstraint parent = new ParentConstraint("", cs.width, cs.height, cs.constraintWidth, cs.constraintHeight, pageConfig.width, pageConfig.height, bc);
            ParentConstraint computed = Routines.ComputeMasterObject(parent, master);
            // recompute size
            cs = new ConstraintSize(computed.constraintWidth, computed.precedingWidth, computed.maximumWidth, computed.constraintHeight, computed.precedingHeight, computed.maximumHeight);
            Routines.SetCSSPart(myCss, cs);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(master);

            OutputHTML global = new OutputHTML();
            if (pageConfig.includeContainer)
            {
                Routines.WriteGlobalContainer(global, pageConfig.subObjects, refPage, list, computed);
                html.CSS.Append(global.CSS.ToString());
                html.JavaScript.Append(global.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
            }

            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.CSS.Append(pageConfig.cssList.GenerateCSSWithoutPrincipal(master.Id, true, true));
            html.JavaScript.Append(pageConfig.javascriptPart.GeneratedCode);
            html.JavaScriptOnLoad.Append(pageConfig.onload.GeneratedCode);

            html.HTML.Append(master.HTMLBefore);

            html.HTML.Append("<table");
            html.HTML.Append(" " + Routines.SetTableDisposition(refPage.Disposition));
            html.HTML.Append(" name='globalTable'");
            html.HTML.Append(" id='" + myId + "'");
            html.HTML.Append(" border='0' cellspacing='0' cellpadding='0'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                html.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                html.HTML.Append(" " + cs.attributeHeight);
            html.HTML.Append(">");

            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = pageConfig.zones.Count;
            for (int index = pageConfig.zones.Count - 1; index >= 0; --index)
            {
                if (pageConfig.zones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = pageConfig.zones[index];
                if (index + 1 < pageConfig.zones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, list, computed);
                    html.HTML.Append(hzone.HTML.ToString());
                    html.CSS.Append(hzone.CSS.ToString());
                    html.JavaScript.Append(hzone.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            html.HTML.Append("</table>");
            html.HTML.Append(master.HTMLAfter);

            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(html.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append("function callback(obj) {");
            outputPage.HTML.Append("var call = document.getElementById('callback'); call.setAttribute('container', obj.getAttribute('name')); call.click();");
            outputPage.HTML.Append("}");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            if (pageConfig.includeContainer)
                outputPage.HTML.Append(global.HTML.ToString());
            outputPage.HTML.Append(html.HTML.ToString());
            outputPage.HTML.Append("<div id='callback' style='display:none'></div>");
            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static OutputHTML GenerateDesignTool(HTMLTool tool)
        {
            Project.InitializeTraceCounter();
            string myId = "tool" + Project.IncrementedTraceCounter.ToString();
            CodeCSS myCss = new CodeCSS(tool.CSS);
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + ConfigDirectories.GetBuildFolder(Project.CurrentProject.Title) + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            OutputHTML html = new OutputHTML();

            // compute size
            myCss.Ids = "#" + myId;
            ConstraintSize cs = new ConstraintSize(tool.ConstraintWidth, tool.Width, 500, tool.ConstraintHeight, tool.Height, 500);
            Routines.SetCSSPart(myCss, cs);

            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.CSS.Append(tool.CSSList.GenerateCSSWithoutPrincipal(tool.Id, true, true));
            html.CSS.Append(tool.CSSOutput(true));
            html.JavaScript.Append(tool.JavaScript.GeneratedCode);
            html.JavaScriptOnLoad.Append(tool.JavaScriptOnLoad.GeneratedCode);
            html.HTML.Append(tool.GeneratedHTML);
            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(html.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();'>");

            outputPage.HTML.Append("<div");
            outputPage.HTML.Append(" id='" + myId + "'");
            outputPage.HTML.Append(" name='" + myId + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                outputPage.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                outputPage.HTML.Append(" " + cs.attributeHeight);
            outputPage.HTML.Append(">");

            outputPage.HTML.Append(html.HTML.ToString());

            outputPage.HTML.Append("</div>");
            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static OutputHTML GenerateDesignAny(OutputHTML html)
        {
            Project.InitializeTraceCounter();
            string myId = "obj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML output = new OutputHTML();
            output.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            output.HTML.Append("<head>");
            output.HTML.Append("<base href='" + ConfigDirectories.GetBuildFolder(Project.CurrentProject.Title) + "'>");
            output.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            output.HTML.Append("<style>");
            output.HTML.Append(html.CSS.ToString());
            output.HTML.Append("</style>");
            output.HTML.Append("<body>");
            output.HTML.Append(html.HTML.ToString());
            output.HTML.Append("</body>");
            output.HTML.Append("</html>");
            return output;
        }

        public static OutputHTML GenerateDesignObject(HTMLObject obj)
        {
            Project.InitializeTraceCounter();
            string myId = "obj" + Project.IncrementedTraceCounter.ToString();
            CodeCSS myCss = new CodeCSS(obj.CSS);
            OutputHTML outputPage = new OutputHTML();
            outputPage.HTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html lang='fr'>");
            outputPage.HTML.Append("<head>");
            outputPage.HTML.Append("<base href='" + ConfigDirectories.GetBuildFolder(Project.CurrentProject.Title) + "'>");
            outputPage.HTML.Append(@"<meta name='generator' content='EasyHTML For Developers'/><meta http-equiv='content-type' content='text/html; charset=utf-8' />" + Environment.NewLine);
            OutputHTML html = new OutputHTML();

            // compute size
            myCss.Ids = "#" + myId;
            ConstraintSize cs = new ConstraintSize(obj.ConstraintWidth, obj.Width, 500, obj.ConstraintHeight, obj.Height, 500);
            Routines.SetCSSPart(myCss, cs);

            // prepare parent
            BorderConstraint bcMaster = new BorderConstraint(0, 0, 1, 1);
            BorderConstraint bcHoriz = new BorderConstraint(bcMaster, 0, 0, 1);
            BorderConstraint bcVert = new BorderConstraint(bcHoriz, 0, 0, 1, 1);
            ParentConstraint parent = new ParentConstraint(obj.Name, cs.width, cs.height, cs.constraintWidth, cs.constraintHeight, obj.Width, obj.Height, bcVert);
            ParentConstraint newInfos = Routines.ComputeObject(parent, obj);
            //Routines.SetObjectDisposition(parent, myCss, newInfos);

            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.CSS.Append(obj.CSSList.GenerateCSSWithoutPrincipal(obj.Id, true, true));
            html.JavaScript.Append(obj.JavaScript.GeneratedCode);
            html.JavaScriptOnLoad.Append(obj.JavaScriptOnLoad.GeneratedCode);
            html.HTML.Append(obj.GeneratedHTML);
            outputPage.HTML.Append("<style>");
            outputPage.HTML.Append(html.CSS.ToString());
            outputPage.HTML.Append("</style>");
            foreach (string url in Project.CurrentProject.JavascriptUrls)
            {
                outputPage.HTML.Append("<script language='JavaScript' type='text/javascript' src='" + url + "'></script>");
            }
            foreach (string url in Project.CurrentProject.CSSUrls)
            {
                outputPage.HTML.Append("<link href='" + url + "' rel='stylesheet'>");
            }
            outputPage.HTML.Append("<script language='JavaScript' type='text/javascript'>");
            outputPage.HTML.Append("function initialize() {" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScriptOnLoad.ToString());
            outputPage.HTML.Append("}" + Environment.NewLine);
            outputPage.HTML.Append(html.JavaScript.ToString());
            outputPage.HTML.Append("</script>");
            outputPage.HTML.Append("</head>");
            outputPage.HTML.Append("<body onload='initialize();'>");

            outputPage.HTML.Append("<div");
            outputPage.HTML.Append(" id='" + myId + "'");
            outputPage.HTML.Append(" name='" + myId + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                outputPage.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                outputPage.HTML.Append(" " + cs.attributeHeight);
            outputPage.HTML.Append(">");

            outputPage.HTML.Append(html.HTML.ToString());

            outputPage.HTML.Append("</div>");
            outputPage.HTML.Append("</body>");
            outputPage.HTML.Append("</html>");
            return outputPage;
        }

        public static ParentConstraint ComputeMasterPageInPage(Page page, MasterPage master)
        {
            uint width = 0, height = 0;
            EnumConstraint constraintWidth = EnumConstraint.AUTO, constraintHeight = EnumConstraint.AUTO;
            uint totalWidth = 0, totalHeight = 0;
            switch (page.ConstraintWidth)
            {
                case EnumConstraint.AUTO:
                    switch (master.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = master.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = master.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = page.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = master.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.Width;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (master.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = master.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = master.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = page.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = master.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.Width;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (master.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = master.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = master.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = 100;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = master.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.Width;
                            break;
                    }
                    break;
            }
            switch (page.ConstraintHeight)
            {
                case EnumConstraint.AUTO:
                    switch (master.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = master.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = master.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = page.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = master.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.Height;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (master.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = master.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = master.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = page.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = master.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.Height;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (master.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = master.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = master.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = 100;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = master.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.Height;
                            break;
                    }
                    break;
            }
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.CSS, master.CountLines, master.CountColumns);
            ParentConstraint newInfos = new ParentConstraint("", width, height, constraintWidth, constraintHeight, totalWidth, totalHeight, bc);
            switch (master.ConstraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingWidth = bc.ComputeBorderWidthForMaster(newInfos.precedingWidth);
                    break;
            }
            switch (master.ConstraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingHeight = bc.ComputeBorderHeightForMaster(newInfos.precedingHeight);
                    break;
            }
            return newInfos;
        }

        public static ParentConstraint ComputeHorizontalZone(ParentConstraint master, HorizontalZone zone)
        {
            uint width = 0, height = 0;
            EnumConstraint constraintWidth = EnumConstraint.AUTO, constraintHeight = EnumConstraint.AUTO;
            uint totalWidth = 0, totalHeight = 0;

            switch (master.constraintWidth)
            {
                case EnumConstraint.AUTO:
                    switch (zone.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = master.precedingWidth;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = master.maximumWidth;
                            break;
                        case EnumConstraint.FIXED:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = master.maximumWidth;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterWidth = master.border.RemoveBorderWidthForMaster(master.precedingWidth);
                                width = (uint)Math.Ceiling((zone.Width / (double)master.maximumWidth) * masterWidth);
                                constraintWidth = EnumConstraint.AUTO;
                                totalWidth = master.maximumWidth;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.maximumWidth;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (zone.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = master.precedingWidth;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = master.maximumWidth;
                            break;
                        case EnumConstraint.FIXED:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = master.maximumWidth;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterWidth = master.border.RemoveBorderWidthForMaster(master.precedingWidth);
                                width = (uint)Math.Ceiling((zone.Width / (double)master.maximumWidth) * masterWidth);
                                constraintWidth = EnumConstraint.AUTO;
                                totalWidth = master.maximumWidth;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.maximumWidth;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (zone.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = master.precedingWidth;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = master.maximumWidth;
                            break;
                        case EnumConstraint.FIXED:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = master.maximumWidth;
                            break;
                        case EnumConstraint.FORCED:
                            width = 100;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.maximumWidth;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = master.maximumWidth;
                            break;
                    }
                    break;
            }
            switch (master.constraintHeight)
            {
                case EnumConstraint.AUTO:
                    switch (zone.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = master.precedingHeight;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = master.maximumHeight;
                            break;
                        case EnumConstraint.FIXED:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = master.maximumHeight;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterHeight = master.border.RemoveBorderHeightForMaster(master.precedingHeight);
                                height = (uint)Math.Ceiling((zone.Height / (double)master.maximumHeight) * masterHeight);
                                constraintHeight = EnumConstraint.AUTO;
                                totalHeight = master.maximumHeight;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.maximumHeight;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (zone.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = master.precedingHeight;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = master.maximumHeight;
                            break;
                        case EnumConstraint.FIXED:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = master.maximumHeight;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterHeight = master.border.RemoveBorderHeightForMaster(master.precedingHeight);
                                height = (uint)Math.Ceiling((zone.Height / (double)master.maximumHeight) * masterHeight);
                                constraintHeight = EnumConstraint.AUTO;
                                totalHeight = master.maximumHeight;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.maximumHeight;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (zone.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = master.precedingHeight;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = master.maximumHeight;
                            break;
                        case EnumConstraint.FIXED:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = master.maximumHeight;
                            break;
                        case EnumConstraint.FORCED:
                            height = 100;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.maximumHeight;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = master.maximumHeight;
                            break;
                    }
                    break;
            }
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.border, zone.CSS, (uint)zone.CountLines);
            //bc.ChangeTotalCountColumns(zone.TotalCountColumns);
            ParentConstraint newInfos = new ParentConstraint(master.objectName, width, height, constraintWidth, constraintHeight, totalWidth, totalHeight, bc);
            switch (zone.ConstraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingWidth = bc.ComputeBorderWidthForHorizontalZone(newInfos.precedingWidth);
                    break;
            }
            switch (zone.ConstraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingHeight = bc.ComputeBorderHeightForHorizontalZone(newInfos.precedingHeight);
                    break;
            }
            return newInfos;
        }

        public static ParentConstraint ComputeVerticalZone(ParentConstraint master, VerticalZone zone)
        {
            uint width = 0, height = 0;
            EnumConstraint constraintWidth = EnumConstraint.AUTO, constraintHeight = EnumConstraint.AUTO;
            uint totalWidth = 0, totalHeight = 0;
            switch (master.constraintWidth)
            {
                case EnumConstraint.AUTO:
                    switch (zone.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = zone.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = zone.Width;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterWidth = master.border.RemoveBorderWidthForHorizontalZone(master.precedingWidth);
                                width = (uint)Math.Ceiling((zone.Width / (double)master.maximumWidth) * masterWidth);
                                constraintWidth = EnumConstraint.FIXED;
                                totalWidth = zone.Width;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = zone.Width;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (zone.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = zone.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = zone.Width;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterWidth = master.border.RemoveBorderWidthForHorizontalZone(master.precedingWidth);
                                width = (uint)Math.Ceiling((zone.Width / (double)master.maximumWidth) * masterWidth);
                                constraintWidth = EnumConstraint.FIXED;
                                totalWidth = zone.Width;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = zone.Width;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (zone.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = zone.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = zone.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = 100;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = zone.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = zone.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = zone.Width;
                            break;
                    }
                    break;
            }
            switch (master.constraintHeight)
            {
                case EnumConstraint.AUTO:
                    switch (zone.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = zone.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = zone.Height;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterHeight = master.border.RemoveBorderHeightForHorizontalZone(master.precedingHeight);
                                height = (uint)Math.Ceiling((zone.Height / (double)master.maximumHeight) * masterHeight);
                                constraintHeight = EnumConstraint.FIXED;
                                totalHeight = zone.Height;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = zone.Height;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (zone.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = zone.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = zone.Height;
                            break;
                        case EnumConstraint.FORCED:
                            {
                                uint masterHeight = master.border.RemoveBorderHeightForHorizontalZone(master.precedingHeight);
                                height = (uint)Math.Ceiling((zone.Height / (double)master.maximumHeight) * masterHeight);
                                constraintHeight = EnumConstraint.FIXED;
                                totalHeight = zone.Height;
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = zone.Height;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (zone.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = zone.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = zone.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = 100;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = zone.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = zone.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = zone.Height;
                            break;
                    }
                    break;
            }
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.border, zone.CSS, (uint)zone.CountLines, (uint)zone.CountColumns);
            ParentConstraint newInfos = new ParentConstraint(master.objectName, width, height, constraintWidth, constraintHeight, totalWidth, totalHeight, zone.Disposition, bc);
            switch (zone.ConstraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingWidth = bc.ComputerBorderWidthForVerticalZone(newInfos.precedingWidth);
                    break;
            }
            switch (zone.ConstraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingHeight = bc.ComputeBorderHeightForVerticalZone(newInfos.precedingHeight);
                    break;
            }
            return newInfos;
        }

        public static ParentConstraint ComputeObject(ParentConstraint master, HTMLObject objet)
        {
            uint width = 0, height = 0;
            EnumConstraint constraintWidth = EnumConstraint.AUTO, constraintHeight = EnumConstraint.AUTO;
            uint totalWidth = 0, totalHeight = 0;
            switch (master.constraintWidth)
            {
                case EnumConstraint.AUTO:
                    switch (objet.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = master.precedingWidth;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (objet.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = master.precedingWidth;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (objet.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = 100;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                    }
                    break;
            }
            switch (master.constraintHeight)
            {
                case EnumConstraint.AUTO:
                    switch (objet.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = master.precedingHeight;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (objet.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = master.precedingHeight;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (objet.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = 100;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                    }
                    break;
            }
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(master.border);
            ParentConstraint newInfos = new ParentConstraint(objet.Name, width, height, constraintWidth, constraintHeight, totalWidth, totalHeight, bc);
            switch (objet.ConstraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingWidth = bc.ComputeBorderWidthForObject(newInfos.precedingWidth);
                    break;
            }
            switch(objet.ConstraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingHeight = bc.ComputerBorderHeightForObject(newInfos.precedingHeight);
                    break;
            }
            return newInfos;
        }

        public static ParentConstraint ComputeMasterObject(ParentConstraint master, MasterObject objet)
        {
            uint width = 0, height = 0;
            EnumConstraint constraintWidth = EnumConstraint.AUTO, constraintHeight = EnumConstraint.AUTO;
            uint totalWidth = 0, totalHeight = 0;
            switch (master.constraintWidth)
            {
                case EnumConstraint.AUTO:
                    switch (objet.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = (uint)Math.Ceiling((objet.Width / (double)master.maximumWidth) * master.precedingWidth);
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (objet.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = (uint)Math.Ceiling((objet.Width / (double)master.maximumWidth) * master.precedingWidth);
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (objet.ConstraintWidth)
                    {
                        case EnumConstraint.AUTO:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.AUTO;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FIXED:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.FIXED;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.FORCED:
                            width = 100;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                        case EnumConstraint.RELATIVE:
                            width = objet.Width;
                            constraintWidth = EnumConstraint.RELATIVE;
                            totalWidth = objet.Width;
                            break;
                    }
                    break;
                case EnumConstraint.FORCED:
                    width = master.precedingWidth;
                    constraintWidth = EnumConstraint.FIXED;
                    totalWidth = objet.Width;
                    break;
            }
            switch (master.constraintHeight)
            {
                case EnumConstraint.AUTO:
                    switch (objet.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = (uint)Math.Ceiling((objet.Height / (double)master.maximumHeight) * master.precedingHeight);
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                    }
                    break;
                case EnumConstraint.FIXED:
                    switch (objet.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = (uint)Math.Ceiling((objet.Height / (double)master.maximumHeight) * master.precedingHeight);
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (objet.ConstraintHeight)
                    {
                        case EnumConstraint.AUTO:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.AUTO;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FIXED:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.FIXED;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.FORCED:
                            height = 100;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                        case EnumConstraint.RELATIVE:
                            height = objet.Height;
                            constraintHeight = EnumConstraint.RELATIVE;
                            totalHeight = objet.Height;
                            break;
                    }
                    break;
                case EnumConstraint.FORCED:
                    height = master.precedingHeight;
                    constraintHeight = EnumConstraint.FIXED;
                    totalHeight = objet.Height;
                    break;
            }
            BorderConstraint bc = BorderConstraint.CreateBorderConstraint(objet.CSS, objet.CountLines, objet.CountColumns);
            ParentConstraint newInfos = new ParentConstraint(master.objectName, width, height, constraintWidth, constraintHeight, totalWidth, totalHeight, bc);
            switch (objet.ConstraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingWidth = bc.ComputeBorderWidthForMaster(newInfos.precedingWidth);
                    break;
            }
            switch (objet.ConstraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    newInfos.precedingHeight = bc.ComputeBorderHeightForMaster(newInfos.precedingHeight);
                    break;
            }
            return newInfos;
        }

        public static string ComputeStyle(int width, int height)
        {
            string output = String.Empty;
            string widthString = width.ToString() + "px";
            string heightString = height.ToString() + "px";
            output += "width:" + widthString + ";";
            output += "height:" + heightString + ";";
            output = "style='" + output + "'";
            return output;
        }

        private static string ComputeCenterTop(ParentConstraint parent, ParentConstraint newInfos)
        {
            string topString = String.Empty;
            uint top = 0;
            switch (parent.constraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                    switch (newInfos.constraintHeight)
                    {
                        case EnumConstraint.AUTO:
                        case EnumConstraint.FIXED:
                            top = (parent.precedingHeight - newInfos.precedingHeight) / 2;
                            topString = top.ToString() + "px";
                            break;
                        case EnumConstraint.RELATIVE:
                            uint size = (uint)(parent.precedingHeight * newInfos.precedingHeight / 100.0);
                            top = (parent.precedingHeight - size) / 2;
                            topString = top.ToString() + "%";
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (newInfos.constraintHeight)
                    {
                        case EnumConstraint.AUTO:
                        case EnumConstraint.FIXED:
                            {
                                uint size = (uint)((newInfos.precedingHeight / (double)newInfos.maximumHeight) * 100);
                                uint topPercent = size / 2;
                                top = (uint)((topPercent / 100.0) * parent.maximumHeight);
                                topString = top.ToString() + "%";
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            {
                                uint topPercent = (parent.precedingHeight - newInfos.precedingHeight) / 2;
                                top = (uint)((topPercent / 100.0) * newInfos.maximumHeight);
                                topString = top.ToString() + "%";
                            }
                            break;
                    }
                    break;
            }
            return topString;
        }

        private static string ComputeCenterLeft(ParentConstraint parent, ParentConstraint newInfos)
        {
            string leftString = String.Empty;
            uint left = 0;
            switch (parent.constraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                    switch (newInfos.constraintWidth)
                    {
                        case EnumConstraint.AUTO:
                        case EnumConstraint.FIXED:
                            left = (parent.precedingWidth - newInfos.precedingWidth) / 2;
                            leftString = left.ToString() + "px";
                            break;
                        case EnumConstraint.RELATIVE:
                            uint size = (uint)(parent.precedingWidth * newInfos.precedingWidth / 100.0);
                            left = (parent.precedingWidth - size) / 2;
                            leftString = left.ToString() + "%";
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    switch (newInfos.constraintWidth)
                    {
                        case EnumConstraint.AUTO:
                        case EnumConstraint.FIXED:
                            {
                                uint size = (uint)((newInfos.precedingWidth / (double)newInfos.maximumWidth) * 100);
                                uint leftPercent = size / 2;
                                left = (uint)((leftPercent / 100.0) * parent.maximumWidth);
                                leftString = left.ToString() + "%";
                            }
                            break;
                        case EnumConstraint.RELATIVE:
                            {
                                uint leftPercent = (parent.precedingWidth - newInfos.precedingWidth) / 2;
                                left = (uint)((leftPercent / 100.0) * newInfos.maximumWidth);
                                leftString = left.ToString() + "%";
                            }
                            break;
                    }
                    break;
            }
            return leftString;
        }

        public static string ComputeBottom(ParentConstraint parent)
        {
            string bottomString = String.Empty;
            uint bottom = 0;
            switch (parent.constraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                    bottom = parent.precedingHeight;
                    bottomString = bottom.ToString() + "px";
                    break;
                case EnumConstraint.RELATIVE:
                    bottom = (uint)((parent.precedingHeight / 100.0) * parent.maximumHeight);
                    bottomString = bottom.ToString() + "%";
                    break;
            }
            return bottomString;
        }

        public static string ComputeRight(ParentConstraint parent)
        {
            string rightString = String.Empty;
            uint right = 0;
            switch (parent.constraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                    right = parent.precedingWidth;
                    rightString = right.ToString() + "px";
                    break;
                case EnumConstraint.RELATIVE:
                    right = (uint)((parent.precedingWidth / 100.0) * parent.maximumWidth);
                    rightString = right.ToString() + "%";
                    break;
            }
            return rightString;
        }

        public static void SetObjectDisposition(ParentConstraint parent, CodeCSS css, ParentConstraint newInfos)
        {
            //string centerTop = Routines.ComputeCenterTop(parent, newInfos);
            //string centerLeft = Routines.ComputeCenterLeft(parent, newInfos);
            //string bottom = Routines.ComputeBottom(parent);
            //string right = Routines.ComputeRight(parent);
            //string top = String.Empty;
            //string left = String.Empty;
            if (css.Body.AllKeys.Contains("margin-left"))
                css.Body.Remove("margin-left");
            if (css.Body.AllKeys.Contains("margin-right"))
                css.Body.Remove("margin-right");
            if (css.Body.AllKeys.Contains("margin-top"))
                css.Body.Remove("margin-bottom");
            css.Margin = new Rectangle(0, 0, 0, 0);
            switch (parent.disposition)
            {
                case Disposition.CENTER:
                    css.Body.Add("margin-top", "auto");
                    css.Body.Add("margin-bottom", "auto");
                    css.Body.Add("margin-left", "auto");
                    css.Body.Add("margin-right", "auto");
                    break;
                case Disposition.CENTER_BOTTOM:
                    css.Body.Add("margin-top", "auto");
                    css.Body.Add("margin-left", "auto");
                    css.Body.Add("margin-right", "auto");
                    break;
                case Disposition.CENTER_TOP:
                    css.Body.Add("margin-bottom", "auto");
                    css.Body.Add("margin-left", "auto");
                    css.Body.Add("margin-right", "auto");
                    break;
                case Disposition.LEFT_BOTTOM:
                    css.Body.Add("margin-top", "auto");
                    css.Body.Add("margin-right", "auto");
                    break;
                case Disposition.LEFT_MIDDLE:
                    css.Body.Add("margin-top", "auto");
                    css.Body.Add("margin-bottom", "auto");
                    css.Body.Add("margin-right", "auto");
                    break;
                case Disposition.LEFT_TOP:
                    css.Body.Add("margin-bottom", "auto");
                    css.Body.Add("margin-right", "auto");
                    break;
                case Disposition.RIGHT_BOTTOM:
                    css.Body.Add("margin-top", "auto");
                    css.Body.Add("margin-left", "auto");
                    break;
                case Disposition.RIGHT_MIDDLE:
                    css.Body.Add("margin-top", "auto");
                    css.Body.Add("margin-bottom", "auto");
                    css.Body.Add("margin-left", "auto");
                    break;
                case Disposition.RIGHT_TOP:
                    css.Body.Add("margin-bottom", "auto");
                    css.Body.Add("margin-left", "auto");
                    break;
            }
        }

        public static void SetDIVDisposition(StringBuilder output, Disposition d, StringBuilder zone)
        {
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                output.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' height='100%'><tr>");
                switch (d)
                {
                    case Disposition.CENTER:
                        output.Append("<td align='center' valign='middle'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.CENTER_BOTTOM:
                        output.Append("<td align='center' valign='bottom'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.CENTER_TOP:
                        output.Append("<td align='center' valign='top'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.LEFT_BOTTOM:
                        output.Append("<td align='left' valign='bottom'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.LEFT_MIDDLE:
                        output.Append("<td align='left' valign='middle'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.LEFT_TOP:
                        output.Append("<td align='left' valign='top'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.RIGHT_BOTTOM:
                        output.Append("<td align='right' valign='bottom'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.RIGHT_MIDDLE:
                        output.Append("<td align='right' valign='middle'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                    case Disposition.RIGHT_TOP:
                        output.Append("<td align='right' valign='top'>");
                        output.Append(zone.ToString());
                        output.Append("</td>");
                        break;
                }
                output.Append("</tr></table>");
            }
            else
            {
                output.Append(zone.ToString());
            }
        }

        public static string SetTableDisposition(Disposition d)
        {
            string output = String.Empty;
            switch (d)
            {
                case Disposition.CENTER:
                    output = "align='center' valign='middle'";
                    break;
                case Disposition.CENTER_BOTTOM:
                    output = "align='center' valign='bottom'";
                    break;
                case Disposition.CENTER_TOP:
                    output = "align='center' valign='top'";
                    break;
                case Disposition.LEFT_BOTTOM:
                    output = "align='left' valign='bottom'";
                    break;
                case Disposition.LEFT_MIDDLE:
                    output = "align='left' valign='middle'";
                    break;
                case Disposition.LEFT_TOP:
                    output = "align='left' valign='top'";
                    break;
                case Disposition.RIGHT_BOTTOM:
                    output = "align='right' valign='bottom'";
                    break;
                case Disposition.RIGHT_MIDDLE:
                    output = "align='right' valign='middle'";
                    break;
                case Disposition.RIGHT_TOP:
                    output = "align='right' valign='top'";
                    break;
            }
            return output;
        }

        public static int CompareLength(EnumConstraint parentConstraint, EnumConstraint childConstraint, int parentLength, int childLength, int maximumLength)
        {
            int result = 0;
            switch (childConstraint)
            {
                case EnumConstraint.AUTO:
                    break;
                case EnumConstraint.FIXED:
                    result = childLength;
                    break;
                case EnumConstraint.FORCED:
                    switch (parentConstraint)
                    {
                        case EnumConstraint.AUTO:
                            break;
                        case EnumConstraint.FIXED:
                            result = (int)((childLength / (double)maximumLength) * parentLength);
                            break;
                        case EnumConstraint.FORCED:
                            result = (int)((childLength / (double)maximumLength) * parentLength);
                            break;
                        case EnumConstraint.RELATIVE:
                            result = (int)(maximumLength * (100 / (double)parentLength));
                            break;
                    }
                    break;
                case EnumConstraint.RELATIVE:
                    result = childLength;
                    break;
            }
            return result;
        }

        public static void MoveConstraint(ParentConstraint current, uint width, uint height, EnumConstraint constraintWidth, EnumConstraint constraintHeight)
        {
/*            switch (constraintWidth)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    current.maximumWidth = width;
                    break;
                case EnumConstraint.RELATIVE:
                    current.maximumWidth = (uint)(current.precedingWidth * width / 100.0);
                    break;
            }
            switch (constraintHeight)
            {
                case EnumConstraint.AUTO:
                case EnumConstraint.FIXED:
                case EnumConstraint.FORCED:
                    current.maximumHeight = height;
                    break;
                case EnumConstraint.RELATIVE:
                    current.maximumHeight = (uint)(current.precedingHeight * height / 100.0);
                    break;
            }*/
        }
    }
}
