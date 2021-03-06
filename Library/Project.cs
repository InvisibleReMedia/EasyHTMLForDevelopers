﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.ComponentModel;

namespace Library
{
    /// <summary>
    /// Contains all data for a project
    /// </summary>
    [Serializable]
    public class Project : Marshalling.PersistentDataObject
    {
        #region Public Delegate

        /// <summary>
        /// Delegate for open a project
        /// </summary>
        public delegate void OpenProject();
        /// <summary>
        /// Search where is the container
        /// </summary>
        /// <param name="containers">list of all containers</param>
        /// <param name="objects">object that host a content</param>
        /// <param name="searchName">name to search</param>
        /// <param name="found">exact container if found</param>
        /// <returns>true if found</returns>
        public delegate bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found);
        #endregion

        #region Public Static Fields

        /// <summary>
        /// Name of the translation label for javascript list url
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string JavaScriptUrlName = "JavaScriptURLs";
        /// <summary>
        /// Name of the translation label for javascript list url
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string CSSUrlName = "CSSURLs";
        /// <summary>
        /// Name of the translation label for configuration
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string ConfigurationName = "Configuration";
        /// <summary>
        /// Name of the translation label for master pages
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string MasterPagesName = "MasterPages";
        /// <summary>
        /// Name of the translation label for master objects
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string MasterObjectsName = "MasterObjects";
        /// <summary>
        /// Name of the translation label for pages
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string PagesName = "Pages";
        /// <summary>
        /// Name of the translation label for tools
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string ToolsName = "Tools";
        /// <summary>
        /// Name of the translation label for files
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string FilesName = "Files";
        /// <summary>
        /// Name of the translation label for instances
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string InstancesName = "Instances";
        /// <summary>
        /// Name of the translation label for folders
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string FoldersName = "Folders";
        /// <summary>
        /// Name of the translation label for Sculpture
        /// viewed into the tree of project elements
        /// </summary>
        public static readonly string SculpturesName = "Sculptures";
        /// <summary>
        /// Index name for error switch occurred
        /// </summary>
        public static readonly string hasErrorSaveName = "hasErrorSave";
        /// <summary>
        /// Index name for error reason string
        /// </summary>
        public static readonly string errorReasonName = "errorReason";
        /// <summary>
        /// Index name for cadre model counter
        /// </summary>
        public static readonly string cadreModelCounterName = "cadreModelCounter";

        #endregion

        #region Private Fields

        /// <summary>
        /// Index name for sculpture tool image
        /// </summary>
        protected static readonly string sculptureToolImageName = "sculptureToolImage";
        /// <summary>
        /// Index name for sculpture tool text
        /// </summary>
        protected static readonly string sculptureToolTextName = "sculptureToolText";
        /// <summary>
        /// Index name for colorscheme text
        /// </summary>
        protected static readonly string colorSchemeName = "colorScheme";
        /// <summary>
        /// Index name for creation date
        /// </summary>
        protected static readonly string creationDateName = "creationDate";
        /// <summary>
        /// Index name for modification date
        /// </summary>
        protected static readonly string modificationDateName = "modificationDate";
        /// <summary>
        /// Index name for revision number
        /// </summary>
        protected static readonly string revisionName = "revision";
        /// <summary>
        /// Index name for configuration
        /// </summary>
        protected static readonly string configurationName = "configuration";
        /// <summary>
        /// Index name for counter index to name elements
        /// </summary>
        protected static readonly string counterName = "counter";
        /// <summary>
        /// Index name for title of the project
        /// </summary>
        protected static readonly string titleName = "title";
        /// <summary>
        /// Index name for list of javascript urls
        /// </summary>
        protected static readonly string javascriptUrlListName = "javascriptUrlList";
        /// <summary>
        /// Index name for list of javascript urls
        /// </summary>
        protected static readonly string cssUrlListName = "cssUrlList";
        /// <summary>
        /// Index name for master page list
        /// </summary>
        protected static readonly string masterPageListName = "masterPageList";
        /// <summary>
        /// Index name for page list
        /// </summary>
        protected static readonly string pageListName = "pageList";
        /// <summary>
        /// Index name for files
        /// </summary>
        protected static readonly string filesName = "fileList";
        /// <summary>
        /// Index name for folders
        /// </summary>
        protected static readonly string foldersName = "folders";
        /// <summary>
        /// Index name for master object list
        /// </summary>
        protected static readonly string masterObjectListName = "masterObjectList";
        /// <summary>
        /// Index name for sculpture object list
        /// </summary>
        protected static readonly string sculptureObjectListName = "sculptureObjectList";
        /// <summary>
        /// Index name for javascript model list
        /// </summary>
        protected static readonly string javascriptModelListName = "javascriptModelList";
        /// <summary>
        /// Index name for css model list
        /// </summary>
        protected static readonly string cssModelList = "cssModelList";
        /// <summary>
        /// Index name for folders tool
        /// </summary>
        protected static readonly string toolslName = "toolList";
        /// <summary>
        /// Index name for instance object list
        /// </summary>
        protected static readonly string instanceObjectListName = "instanceObjectList";
        /// <summary>
        /// Index name for a tree representation by string identifier
        /// </summary>
        protected static readonly string treeName = "tree";
        /// <summary>
        /// Index name for custom color list
        /// </summary>
        protected static readonly string customColorsListName = "customColorList";
        /// <summary>
        /// Index name for unique strings
        /// </summary>
        protected static readonly string uniqueStringsName = "uniqueStrings";
        /// <summary>
        /// Index name for unique id
        /// </summary>
        protected static readonly string uniqueIdName = "uniqueId";
        /// <summary>
        /// Index name for unique class
        /// </summary>
        protected static readonly string uniqueClassName = "uniqueClass";


        /// <summary>
        /// counter for dumping element
        /// </summary>
        private static int traceCounter;
        /// <summary>
        /// Keeps a reference to the current project
        /// </summary>
        private static Project currentProject;
        [NonSerialized]
        private OpenProject openProject;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets creation date
        /// </summary>
        public DateTime CreationDate
        {
            get { return this.Get(creationDateName, DateTime.Now); }
            set { this.Set(creationDateName, value); }
        }

        /// <summary>
        /// Gets or sets modification date
        /// Is null if new project
        /// </summary>
        public DateTime ModificationDate
        {
            get { return this.Get(modificationDateName, DateTime.Now); }
            set { this.Set(modificationDateName, value); }
        }

        /// <summary>
        /// Gets or sets the revision value
        /// </summary>
        public int Revision
        {
            get { return this.Get(revisionName, 1); }
            set { this.Set(revisionName, value); }
        }

        /// <summary>
        /// Gets or sets the color scheme
        /// </summary>
        public string ColorScheme
        {
            get { return this.Get(colorSchemeName, ""); }
            set { this.Set(colorSchemeName, value); }
        }

        /// <summary>
        /// Gets the configuration object
        /// </summary>
        public Configuration Configuration
        {
            get { return this.Get(configurationName, new Configuration()); }
        }

        /// <summary>
        /// Gets the current counter and increments it immediately
        /// </summary>
        public int IncrementedCounter
        {
            get
            {
                int c = this.Get(counterName, 0);
                this.Set(counterName, c + 1);
                return c;
            }
        }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title
        {
            get { return this.Get(titleName, "new project"); }
            set {
                this.Set(titleName, value);
                this.Hierarchy.NodeValue = value;    
            }
        }

        /// <summary>
        /// Gets the list of urls of javascript
        /// </summary>
        public List<string> JavascriptUrls
        {
            get { return this.Get(javascriptUrlListName, new List<string>()); }
        }

        /// <summary>
        /// Gets the list of urls of javascript
        /// </summary>
        public List<string> CSSUrls
        {
            get { return this.Get(cssUrlListName, new List<string>()); }
        }

        /// <summary>
        /// Gets the master page tree of string and master pages
        /// </summary>
        public List<MasterPage> MasterPages
        {
            get { return this.Get(masterPageListName, new List<MasterPage>()); }
        }

        /// <summary>
        /// Gets the page list
        /// </summary>
        public List<Page> Pages
        {
            get { return this.Get(pageListName, new List<Page>()); }
        }

        /// <summary>
        /// Gets the file list
        /// </summary>
        public List<File> Files
        {
            get { return this.Get(filesName, new List<File>()); }
        }

        /// <summary>
        /// Gets master object list
        /// </summary>
        public List<MasterObject> MasterObjects
        {
            get { return this.Get(masterObjectListName, new List<MasterObject>()); }
        }

        /// <summary>
        /// Gets sculpture object list
        /// </summary>
        public List<SculptureObject> SculptureObjects
        {
            get { return this.Get(sculptureObjectListName, new List<SculptureObject>()); }
        }

        /// <summary>
        /// Gets javascript models
        /// </summary>
        public List<CodeJavaScript> JavaScriptModels
        {
            get { return this.Get(javascriptModelListName, new List<CodeJavaScript>()); }
        }

        /// <summary>
        /// Gets CSS models
        /// </summary>
        public List<CodeCSS> CSSModels
        {
            get { return this.Get(cssModelList, new List<CodeCSS>()); }
        }

        /// <summary>
        /// Gets folders tool
        /// </summary>
        public List<HTMLTool> Tools
        {
            get { return this.Get(toolslName, new List<HTMLTool>()); }
        }

        /// <summary>
        /// Gets all instances
        /// </summary>
        public List<HTMLObject> Instances
        {
            get { return this.Get(instanceObjectListName, new List<HTMLObject>()); }
        }

        /// <summary>
        /// Gets the hierarchy of declared elements
        /// </summary>
        public Node<string, Accessor> Hierarchy
        {
            get
            {
                return this.Get(treeName, this.ConstructHierarchy());
            }
        }

        /// <summary>
        /// Gets Custom color list
        /// </summary>
        public List<int> CustomColors
        {
            get { return this.Get(customColorsListName, new List<int>()); }
        }

        /// <summary>
        /// Gets the scultpture tool image
        /// </summary>
        public HTMLTool ToolImage
        {
            get { return this.Get(sculptureToolImageName, new HTMLTool()); }
        }

        /// <summary>
        /// Gets the sculpture tool text
        /// </summary>
        public HTMLTool ToolText
        {
            get { return this.Get(sculptureToolTextName, new HTMLTool()); }
        }

        /// <summary>
        /// Gets or sets the current project
        /// </summary>
        public static Project CurrentProject
        {
            get { return Project.currentProject; }
            set { Project.currentProject = value; }
        }

        /// <summary>
        /// Gets if the project was not saved
        /// also true if an error occurred during save
        /// </summary>
        public bool NotSaved
        {
            get { return this.Get(hasErrorSaveName, false); }
        }

        /// <summary>
        /// Gets the error reasong
        /// </summary>
        public string ErrorReason
        {
            get { return this.Get(errorReasonName, ""); }
        }

        /// <summary>
        /// Gets the trace counter
        /// </summary>
        public static int TraceCounter
        {
            get { return Project.traceCounter; }
        }

        /// <summary>
        /// Gets the trace counter and increments it immediately
        /// </summary>
        public static int IncrementedTraceCounter
        {
            get { return ++Project.traceCounter; }
        }

        /// <summary>
        /// Gets a generation of unique strings operator
        /// </summary>
        public UniqueStrings Unique
        {
            get
            {
                return this.Get(uniqueStringsName, new UniqueStrings());
            }
        }

        /// <summary>
        /// Gets or sets the unique id counter
        /// </summary>
        public int UniqueId
        {
            get { return this.Get(uniqueIdName, 0); }
            set { this.Set(uniqueIdName, value); }
        }

        /// <summary>
        /// Gets or sets the unique id counter
        /// </summary>
        public int UniqueClass
        {
            get { return this.Get(uniqueClassName, 0); }
            set { this.Set(uniqueClassName, value); }
        }

        /// <summary>
        /// Gets or set the cadre model counter
        /// </summary>
        public int CadreModelCounter
        {
            get { return this.Get(cadreModelCounterName, 0); }
            set { this.Set(cadreModelCounterName, value); }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Construct the initial hierarchy
        /// </summary>
        /// <returns></returns>
        protected Node<string, Accessor> ConstructHierarchy()
        {
            Node<string, Accessor> node = new Node<string, Accessor>(this.Title);
            node.AddNode(new Node<string, Accessor>(Project.JavaScriptUrlName));
            node.AddNode(new Node<string, Accessor>(Project.CSSUrlName));
            node.AddNode(new Node<string, Accessor>(Project.ConfigurationName));
            node.AddNode(new Node<string, Accessor>(Project.MasterPagesName));
            node.AddNode(new Node<string, Accessor>(Project.MasterObjectsName));
            node.AddNode(new Node<string, Accessor>(Project.ToolsName));
            node.AddNode(new Node<string, Accessor>(Project.SculpturesName));
            node.AddNode(new Node<string, Accessor>(Project.InstancesName));
            node.AddNode(new Node<string, Accessor>(Project.PagesName));
            node.AddNode(new Node<string, Accessor>(Project.FilesName));
            node.AddNode(new Node<string, Accessor>(Project.FoldersName));
            return node;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fonction to find a container
        /// </summary>
        /// <param name="searchName">name of the container to search</param>
        /// <param name="found">counter found</param>
        /// <param name="del">delegate for recursive search</param>
        /// <returns>true if found</returns>
        public bool FindContainer(string searchName, out IContainer found, SearchContainer del)
        {
            List<IContainer> containers = new List<IContainer>(this.MasterPages.Cast<IContainer>());
            containers.AddRange(this.MasterObjects.Cast<IContainer>());
            return del(containers, new List<IContent>(), searchName, out found);
        }

        /// <summary>
        /// When you reload project
        /// </summary>
        public void ReloadProject()
        {
            this.openProject();
        }

        /// <summary>
        /// Add a master page
        /// given a specific path to organize project's element
        /// </summary>
        /// <param name="mp">master page</param>
        /// <param name="path">path</param>
        public void Add(MasterPage mp, string path)
        {
            this.MasterPages.Add(mp);
            string u = this.Unique.ComputeNewString();
            mp.Unique = u;
            Accessor a = new Accessor(Project.MasterPagesName, u);
            this.Hierarchy.Find(Project.MasterPagesName).Find(path.Split('/')).AddLeaf(a);

            // objects
            foreach (HTMLObject obj in mp.Objects)
            {
                this.Add(obj, "");
            }
        }

        /// <summary>
        /// Add a master object
        /// given a specific path to organize project's element
        /// </summary>
        /// <param name="mo">master object</param>
        /// <param name="path">path</param>
        public void Add(MasterObject mo, string path)
        {
            this.MasterObjects.Add(mo);
            string u = this.Unique.ComputeNewString();
            mo.Unique = u;
            Accessor a = new Accessor(Project.MasterObjectsName, u);
            this.Hierarchy.Find(Project.MasterObjectsName).Find(path.Split('/')).AddLeaf(a);

            // objects
            foreach (HTMLObject obj in mo.Objects)
            {
                this.Add(obj, "");
            }
        }

        /// <summary>
        /// Add a page
        /// given a specific path to organize project's element
        /// </summary>
        /// <param name="p">page</param>
        /// <param name="path">path</param>
        public void Add(Page p, string path)
        {
            this.Pages.Add(p);
            string u = this.Unique.ComputeNewString();
            p.Unique = u;
            Accessor a = new Accessor(Project.PagesName, u);
            string[] splitted = path.Split('/');
            p.Name = splitted.Last();
            this.Hierarchy.Find(Project.PagesName).Find(splitted.Take(splitted.Count() - 1)).AddLeaf(a);

            // folders
            this.Hierarchy.Find(Project.FoldersName).Find(splitted.Take(splitted.Count() - 1)).AddLeaf(a);
            p.Folder = String.Join("/", splitted.Take(splitted.Count() - 1).ToArray()) + "/";
            CommonDirectories.ConfigDirectories.AddFolder(this.Title, p.Folder);

            // objects
            foreach (HTMLObject obj in p.Objects)
            {
                this.Add(obj, "");
            }
        }

        /// <summary>
        /// Add a file
        /// given a specific path to organize project's element
        /// </summary>
        /// <param name="f">file</param>
        /// <param name="path">path</param>
        public void Add(File f, string path)
        {
            this.Files.Add(f);
            string u = this.Unique.ComputeNewString();
            f.Unique = u;
            f.Folder = path;
            Accessor a = new Accessor(Project.FilesName, u);
            this.Hierarchy.Find(Project.FilesName).Find(path.Split('/')).AddLeaf(a);
            this.Hierarchy.Find(Project.FoldersName).Find(path.Split('/')).AddLeaf(a);
        }

        /// <summary>
        /// Add a tool
        /// given a specific path to organize project's element
        /// </summary>
        /// <param name="t">tool</param>
        /// <param name="path">path</param>
        public void Add(HTMLTool t, string path)
        {
            this.Tools.Add(t);
            string u = this.Unique.ComputeNewString();
            t.Unique = u;
            Accessor a = new Accessor(Project.ToolsName, u);
            string[] splitted = path.Split('/');
            this.Hierarchy.Find(Project.ToolsName).Find(splitted.Take(splitted.Count() - 1)).AddLeaf(a);
            t.Path = String.Join("/", splitted.Take(splitted.Count() - 1).ToArray()) + "/";
            t.Title = splitted.Last();
        }

        /// <summary>
        /// Add an instance
        /// given a specific path to organize project's element
        /// </summary>
        /// <param name="i">instance</param>
        /// <param name="path">path</param>
        public void Add(HTMLObject i, string path)
        {
            this.Instances.Add(i);
            string u = this.Unique.ComputeNewString();
            i.Unique = u;
            Accessor a = new Accessor(Project.InstancesName, u);
            this.Hierarchy.Find(Project.InstancesName).Find(path.Split('/')).AddLeaf(a);
        }

        /// <summary>
        /// Add an instance
        /// given a specific path to organize project's element
        /// </summary>
        /// <param name="s">instance</param>
        /// <param name="path">path</param>
        public void Add(SculptureObject s, string path)
        {
            this.SculptureObjects.Add(s);
            string u = this.Unique.ComputeNewString();
            s.Unique = u;
            Accessor a = new Accessor(Project.SculpturesName, u);
            this.Hierarchy.Find(Project.SculpturesName).Find(path.Split('/')).AddLeaf(a);
        }

        /// <summary>
        /// Remove a master page
        /// </summary>
        /// <param name="mp">master page</param>
        public void Remove(MasterPage mp)
        {
            Accessor a = new Accessor(Project.MasterPagesName, mp.Unique);
            this.Hierarchy.Find(Project.MasterPagesName).Remove(a);
            this.MasterPages.Remove(mp);
            foreach (HTMLObject obj in mp.Objects)
            {
                this.Remove(obj);
            }
        }

        /// <summary>
        /// Remove a master object
        /// </summary>
        /// <param name="mo">master object</param>
        public void Remove(MasterObject mo)
        {
            Accessor a = new Accessor(Project.MasterObjectsName, mo.Unique);
            this.Hierarchy.Find(Project.MasterObjectsName).Remove(a);
            this.MasterObjects.Remove(mo);
            foreach (HTMLObject obj in mo.Objects)
            {
                this.Remove(obj);
            }
        }

        /// <summary>
        /// Remove a page
        /// </summary>
        /// <param name="p">page</param>
        public void Remove(Page p)
        {
            Accessor a = new Accessor(Project.PagesName, p.Unique);
            this.Hierarchy.Find(Project.PagesName).Remove(a);
            this.Hierarchy.Find(Project.FoldersName).Remove(a);
            this.Pages.Remove(p);
            foreach (HTMLObject obj in p.Objects)
            {
                this.Remove(obj);
            }
        }

        /// <summary>
        /// Remove a file
        /// </summary>
        /// <param name="f">file</param>
        public void Remove(File f)
        {
            Accessor a = new Accessor(Project.FilesName, f.Unique);
            this.Hierarchy.Find(Project.FilesName).Remove(a);
            this.Hierarchy.Find(Project.FoldersName).Remove(a);
            this.Files.Remove(f);
        }

        /// <summary>
        /// Remove an html tool
        /// </summary>
        /// <param name="t">html tool</param>
        public void Remove(HTMLTool t)
        {
            Accessor a = new Accessor(Project.ToolsName, t.Unique);
            this.Hierarchy.Find(Project.ToolsName).Remove(a);
            this.Tools.Remove(t);
        }

        /// <summary>
        /// Remove a html object
        /// </summary>
        /// <param name="o">htmlObject</param>
        public void Remove(HTMLObject o)
        {
            Accessor a = new Accessor(Project.InstancesName, o.Unique);
            this.Hierarchy.Find(Project.InstancesName).Remove(a);
            this.Instances.Remove(o);
            if (o.BelongsTo != null)
            {
                // ce serait un master object
                MasterObject mo = this.MasterObjects.Find(x => { return x.Name == o.BelongsTo; });
                if (mo != null)
                {
                    mo.Objects.Remove(o);
                }
                else
                {
                    Page p = this.Pages.Find(x => { return x.Name == o.BelongsTo; });
                    if (p != null)
                    {
                        p.Objects.Remove(o);
                    }
                    else
                    {
                        // il appartient plutot à un tool
                        MasterPage mp = this.MasterPages.Find(x => { return x.Name == o.BelongsTo; });
                        if (mp != null)
                        {
                            mp.Objects.Remove(o);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove a sculpture
        /// </summary>
        /// <param name="s">sculpture</param>
        public void Remove(SculptureObject s)
        {
            Accessor a = new Accessor(Project.SculpturesName, s.Unique);
            this.Hierarchy.Find(Project.SculpturesName).Remove(a);
            this.SculptureObjects.Remove(s);
        }


        #endregion

        #region Private Static Methods

        /// <summary>
        /// Import selected pages from an existing project
        /// </summary>
        /// <param name="src">project source</param>
        /// <param name="dest">project destination</param>
        private static void ImportPages(Project src, Project dest)
        {
            Tree<string, Accessor> t = new Tree<string, Accessor>(src.Hierarchy.Find(Project.PagesName));
            BindingList<KeyValuePair<IEnumerable<string>, Accessor>> b = new BindingList<KeyValuePair<IEnumerable<string>, Accessor>>();
            Node<string, Accessor> destNode = dest.Hierarchy.Find(Project.PagesName);
            t.EnumerateSelected(b);
            foreach (KeyValuePair<IEnumerable<string>, Accessor> kv in b)
            {
                Page page = kv.Value.GetObject(src);
                List<string> path = new List<string>(kv.Key.Skip(1));
                path.Add(page.ElementTitle);
                dest.Add(page, String.Join("/", path));
            }
        }

        /// <summary>
        /// Import selected tools from an existing project
        /// </summary>
        /// <param name="src">project source</param>
        /// <param name="dest">project destination</param>
        private static void ImportTools(Project src, Project dest)
        {
            Tree<string, Accessor> t = new Tree<string, Accessor>(src.Hierarchy.Find(Project.ToolsName));
            BindingList<KeyValuePair<IEnumerable<string>, Accessor>> b = new BindingList<KeyValuePair<IEnumerable<string>, Accessor>>();
            Node<string, Accessor> destNode = dest.Hierarchy.Find(Project.ToolsName);
            t.EnumerateSelected(b);
            foreach (KeyValuePair<IEnumerable<string>, Accessor> kv in b)
            {
                HTMLTool tool = kv.Value.GetObject(src);
                tool = tool.Clone() as HTMLTool;
                List<string> path = new List<string>(kv.Key.Skip(1));
                path.Add(tool.ElementTitle);
                dest.Add(tool, String.Join("/", path.ToArray()));
            }
        }

        /// <summary>
        /// Import selected master pages from an existing project
        /// </summary>
        /// <param name="src">project source</param>
        /// <param name="dest">project destination</param>
        private static void ImportMasterPages(Project src, Project dest)
        {
            Tree<string, Accessor> t = new Tree<string, Accessor>(src.Hierarchy.Find(Project.MasterPagesName));
            BindingList<KeyValuePair<IEnumerable<string>, Accessor>> b = new BindingList<KeyValuePair<IEnumerable<string>, Accessor>>();
            Node<string, Accessor> destNode = dest.Hierarchy.Find(Project.MasterPagesName);
            t.EnumerateSelected(b);
            foreach (KeyValuePair<IEnumerable<string>, Accessor> kv in b)
            {
                // recherche des objets (soit des tools, soit des master objects)
                MasterPage srcMo = kv.Value.GetObject(src);
                dest.Add(srcMo, String.Join("/", kv.Key.Skip(1).ToArray()));
            }
        }

        /// <summary>
        /// Import selected master object from an existing project
        /// </summary>
        /// <param name="src">project source</param>
        /// <param name="dest">project destination</param>
        private static void ImportMasterObjects(Project src, Project dest)
        {
            Tree<string, Accessor> t = new Tree<string, Accessor>(src.Hierarchy.Find(Project.MasterObjectsName));
            BindingList<KeyValuePair<IEnumerable<string>, Accessor>> b = new BindingList<KeyValuePair<IEnumerable<string>, Accessor>>();
            Node<string, Accessor> destNode = dest.Hierarchy.Find(Project.MasterObjectsName);
            t.EnumerateSelected(b);
            foreach (KeyValuePair<IEnumerable<string>, Accessor> kv in b)
            {
                MasterObject srcMo = kv.Value.GetObject(src);
                dest.Add(srcMo, String.Join("/", kv.Key.Skip(1).ToArray()));
            }
        }

        /// <summary>
        /// Import selected sculptures from an existing project
        /// </summary>
        /// <param name="src">project source</param>
        /// <param name="dest">project destination</param>
        private static void ImportSculptures(Project src, Project dest)
        {
            Tree<string, Accessor> t = new Tree<string, Accessor>(src.Hierarchy.Find(Project.SculpturesName));
            BindingList<KeyValuePair<IEnumerable<string>, Accessor>> b = new BindingList<KeyValuePair<IEnumerable<string>, Accessor>>();
            Node<string, Accessor> destNode = dest.Hierarchy.Find(Project.SculpturesName);
            t.EnumerateSelected(b);
            foreach (KeyValuePair<IEnumerable<string>, Accessor> kv in b)
            {
                SculptureObject srcSculpture = kv.Value.GetObject(src);
                dest.Add(srcSculpture, String.Join("/", kv.Key.Skip(1).ToArray()));
            }
        }

        /// <summary>
        /// Import selected files from an existing project
        /// </summary>
        /// <param name="src">project source</param>
        /// <param name="dest">project destination</param>
        private static void ImportFiles(Project src, Project dest)
        {
            Tree<string, Accessor> t = new Tree<string, Accessor>(src.Hierarchy.Find(Project.FilesName));
            BindingList<KeyValuePair<IEnumerable<string>, Accessor>> b = new BindingList<KeyValuePair<IEnumerable<string>, Accessor>>();
            Node<string, Accessor> destNode = dest.Hierarchy.Find(Project.FilesName);
            t.EnumerateSelected(b);
            foreach (KeyValuePair<IEnumerable<string>, Accessor> kv in b)
            {
                File f = kv.Value.GetObject(src);
                dest.Add(f, String.Join("/", kv.Key.Skip(1).ToArray()));
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Import from an another project
        /// </summary>
        /// <param name="src">source</param>
        /// <param name="dest">destination</param>
        public static void Import(Project src, Project dest)
        {
            foreach (Node<string, Accessor> node in src.Hierarchy.SubNodes)
            {
                if (node.NodeValue == Project.MasterPagesName && node.IsSelected)
                {
                    ImportMasterPages(src, dest);
                }
                else if (node.NodeValue == Project.MasterObjectsName && node.IsSelected)
                {
                    ImportMasterObjects(src, dest);
                }
                else if (node.NodeValue == Project.PagesName && node.IsSelected)
                {
                    ImportPages(src, dest);
                }
                else if (node.NodeValue == Project.ToolsName && node.IsSelected)
                {
                    ImportTools(src, dest);
                }
                else if (node.NodeValue == Project.SculpturesName && node.IsSelected)
                {
                    ImportSculptures(src, dest);
                }
                else if (node.NodeValue == Project.FilesName && node.IsSelected)
                {
                    ImportFiles(src, dest);
                }
            }
        }

        /// <summary>
        /// Save project into file
        /// </summary>
        /// <param name="p">project object to save</param>
        /// <param name="path">path of file</param>
        /// <param name="fileName">file name</param>
        public static void Save(Project p, string path, string fileName)
        {
            FileInfo fi = new FileInfo(Path.Combine(path, fileName));
            if (fi.Exists)
            {
                fi.CopyTo(Path.Combine(path, Path.GetFileNameWithoutExtension(fileName) + ".bak"), true);
            }
            ++p.Revision;
            p.ModificationDate = DateTime.Now;
            p.CadreModelCounter = CadreModel.counter;
            p.UniqueId = Attributes.uniqueId.Counter;
            p.UniqueClass = Attributes.uniqueClass.Counter;
            string errorText;
            if (Save(fi, p, out errorText))
            {
                p.Set(hasErrorSaveName, false);
            }
            else
            {
                p.Set(hasErrorSaveName, true);
                p.Set(errorReasonName, errorText);
                FileInfo oldFi = new FileInfo(Path.Combine(path, Path.GetFileNameWithoutExtension(fileName) + ".bak"));
                if (oldFi.Exists)
                    oldFi.CopyTo(Path.Combine(path, fileName), true);
            }
        }

        /// <summary>
        /// Load a project
        /// </summary>
        /// <param name="path">path of file</param>
        /// <param name="fileName">file name</param>
        /// <param name="del">delegate to open project</param>
        /// <returns>project object</returns>
        public static Project Load(string path, string fileName, OpenProject del)
        {
            FileInfo fi = new FileInfo(Path.Combine(path, fileName));
            Marshalling.PersistentDataObject obj = null;
            Load(fi, out obj);

            Project pn = obj as Project;
            if (pn == null)
            {
                throw new FormatException(String.Format(Localization.Strings.GetString("ExceptionProjectNotLoaded"), fileName));
            }
            CadreModel.ReinitCounter(pn.CadreModelCounter);
            Attributes.uniqueId.Counter = pn.UniqueId;
            Attributes.uniqueClass.Counter = pn.UniqueClass;
            pn.openProject = del;
            Project.CurrentProject = pn;
            pn.ReloadProject();
            return pn;
        }

        /// <summary>
        /// Load a project
        /// </summary>
        /// <param name="path">path of file</param>
        /// <param name="fileName">file name</param>
        /// <param name="notCurrent">do not set the current project</param>
        /// <returns>project object</returns>
        public static Project Load(string path, string fileName, bool notCurrent = false)
        {
            CadreModel.ReinitCounter(0);
            FileInfo fi = new FileInfo(Path.Combine(path, fileName));
            Marshalling.PersistentDataObject obj = null;
            Load(fi, out obj);

            Project pn = obj as Project;
            if (pn == null)
            {
                throw new FormatException(String.Format(Localization.Strings.GetString("ExceptionProjectNotLoaded"), fileName));
            }
            if (!notCurrent)
            {
                pn.openProject = Project.CurrentProject.openProject;
                Project.CurrentProject = pn;
            }
            return pn;
        }

        /// <summary>
        /// Add a file
        /// </summary>
        /// <param name="proj">project source</param>
        /// <param name="path">path for the directory of this file to copy</param>
        /// <param name="fileName">file name to add</param>
        /// <returns>true if added</returns>
        public static bool AddFile(Project proj, string path, string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            string path2 = CommonDirectories.ConfigDirectories.RemoveLeadBackslash(path);
            string f = CommonDirectories.ConfigDirectories.RemoveLeadBackslash(Path.GetFileName(fileName));
            proj.Add(new File(f), path2);
            bool result;
            try
            {
                CommonDirectories.ConfigDirectories.AddFile(proj.Title,
                                                            Path.Combine(CommonDirectories.ConfigDirectories.GetBuildFolder(proj.Title), path, f),
                                                            fileName);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="project">project</param>
        /// <param name="sourcePath">source</param>
        /// <param name="destPath">dest</param>
        public static void Copy(Project project, string sourcePath, string destPath)
        {
            CommonDirectories.ConfigDirectories.AddFolder(project.Title, destPath);
            DirectoryInfo di = new DirectoryInfo(sourcePath);
            foreach (DirectoryInfo subDir in di.GetDirectories())
            {
                CommonDirectories.ConfigDirectories.AddFolder(project.Title, Path.Combine(destPath, subDir.Name));
                Copy(project, Path.Combine(sourcePath, subDir.Name), Path.Combine(destPath, subDir.Name));
            }
            foreach (FileInfo fi in di.GetFiles())
            {
                Project.AddFile(Project.CurrentProject,
                                Path.Combine(destPath, fi.Name),
                                Path.Combine(sourcePath, fi.Name));
            }
        }

        /// <summary>
        /// Add a new Page
        /// </summary>
        /// <param name="proj">concerned project</param>
        /// <param name="p">new page</param>
        /// <param name="path">path for this page</param>
        /// <returns></returns>
        public static bool AddPage(Project proj, Page p, string path)
        {
            proj.Add(p, path);
            return true;
        }

        /// <summary>
        /// Add a new Page
        /// </summary>
        /// <param name="proj">concerned project</param>
        /// <param name="t">new tool</param>
        /// <param name="path">path for this page</param>
        /// <returns></returns>
        public static bool AddTool(Project proj, HTMLTool t, string path)
        {
            proj.Add(t, path);
            return true;
        }

        /// <summary>
        /// Check sculpture generation
        /// </summary>
        /// <param name="proj">project</param>
        public static void EnsureSculptureGeneration(Project proj)
        {
            if (proj.ToolImage == null)
            {
                // créer un tool image
                HTMLTool newToolImage = new HTMLTool();
                newToolImage.ConstraintHeight = EnumConstraint.AUTO;
                newToolImage.ConstraintWidth = EnumConstraint.AUTO;
                proj.Set(sculptureToolImageName, newToolImage);
                newToolImage.CSS.Body.Add("background-position", "center");
                newToolImage.CSS.Body.Add("background-repeat", "no-repeat");
                Project.AddTool(proj, proj.ToolImage, "/#sculpture/image");
            }
            if (proj.ToolText == null)
            {
                // créer un tool texte
                HTMLTool newToolText = new HTMLTool();
                newToolText.ConstraintHeight = EnumConstraint.AUTO;
                newToolText.ConstraintWidth = EnumConstraint.AUTO;
                proj.Set(sculptureToolTextName, newToolText);
                Project.AddTool(proj, proj.ToolText, "/#sculpture/texte");
            }
        }

        /// <summary>
        /// Generate an HtmlObject from a sculpture object
        /// </summary>
        /// <param name="proj">project source</param>
        /// <param name="model">model</param>
        /// <returns>HTMLObject constructed</returns>
        public static HTMLObject InstanciateSculptureTool(Project proj, CadreModel model)
        {
            HTMLObject obj = null;
            Project.EnsureSculptureGeneration(proj);
            if (model.SelectedModelTypeObject.Type == CadreModelType.Image)
            {
                obj = new HTMLObject(proj.ToolImage);
                model.CopyProperties(obj);
                obj.CSS.BackgroundImageURL = model.SelectedModelTypeObject.Content;
                proj.Add(obj, "Sculptures");
            }
            else if (model.SelectedModelTypeObject.Type == CadreModelType.Text)
            {
                obj = new HTMLObject(proj.ToolText);
                model.CopyProperties(obj);
                obj.HTML = model.SelectedModelTypeObject.Content;
                proj.Add(obj, "Sculptures");
            }
            else if (model.SelectedModelTypeObject.Type == CadreModelType.Tool)
            {
                obj = new HTMLObject(model.SelectedModelTypeObject.DirectObject);
                model.CopyProperties(obj);
                proj.Add(obj, "Sculptures");
            }
            else if (model.SelectedModelTypeObject.Type == CadreModelType.MasterObject)
            {
                obj = new HTMLObject(model.SelectedModelTypeObject.DirectObject);
                model.CopyProperties(obj);
                proj.Add(obj, "Sculptures");
            }
            return obj;
        }

        /// <summary>
        /// Initialize trace counter
        /// </summary>
        public static void InitializeTraceCounter()
        {
            Project.traceCounter = 0;
        }

        #endregion

    }
}
