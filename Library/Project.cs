using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

namespace Library
{
    [Serializable]
    public class Project
    {
        #region Public Delegate
        public delegate void OpenProject();
        public delegate bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found);
        #endregion

        #region Private Fields
        private bool hasErrorSave = false;
        private string reason;
        private HTMLTool sculptureToolImage = null;
        private HTMLTool sculptureToolText = null;
        private string colorScheme = String.Empty;
        private static int traceCounter;
        private DateTime creationDate;
        private DateTime modificationDate;
        private int revision;
        private Configuration config = new Configuration();
        private static Project currentProject;
        private int counter;
        private string title;
        private List<string> javascriptUrls = new List<string>();
        private List<MasterPage> masterPages = new List<MasterPage>();
        private Folder folders = new Folder();
        private List<MasterObject> masterObjects = new List<MasterObject>();
        private List<SculptureObject> sculptureObjects = new List<SculptureObject>();
        private List<CodeJavaScript> javascriptModels = new List<CodeJavaScript>();
        private List<CodeCSS> cssModels = new List<CodeCSS>();
        private FolderTool tools = new FolderTool();
        private List<HTMLObject> instances = new List<HTMLObject>();
        private List<int> customColors = new List<int>();
        [NonSerialized]
        private OpenProject openProject;
        #endregion

        #region Public Properties
        public DateTime CreationDate
        {
            get { return this.creationDate; }
            set { this.creationDate = value; }
        }

        public DateTime ModificationDate
        {
            get { return this.modificationDate; }
            set { this.modificationDate = value; }
        }

        public int Revision
        {
            get { return this.revision; }
            set { this.revision = value; }
        }

        public string ColorScheme
        {
            get { return this.colorScheme; }
            set { this.colorScheme = value; }
        }

        public Configuration Configuration
        {
            get { return this.config; }
        }

        public int IncrementedCounter
        {
            get { return this.counter++; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public List<string> JavascriptUrls
        {
            get { return this.javascriptUrls; }
        }

        public List<MasterPage> MasterPages
        {
            get { return this.masterPages; }
        }

        public List<Page> Pages
        {
            get { return this.folders.Pages; }
        }

        public Folder Folders
        {
            get { return this.folders; }
        }

        public List<MasterObject> MasterObjects
        {
            get { return this.masterObjects; }
        }

        public List<SculptureObject> SculptureObjects
        {
            get { return this.sculptureObjects; }
        }

        public List<CodeJavaScript> JavaScriptModels
        {
            get { return this.javascriptModels; }
        }

        public List<CodeCSS> CSSModels
        {
            get { return this.cssModels; }
        }

        public FolderTool Tools
        {
            get { return this.tools; }
        }

        public List<HTMLObject> Instances
        {
            get { return this.instances; }
        }

        public List<int> CustomColors
        {
            get { if (this.customColors == null) this.customColors = new List<int>(); return this.customColors; }
        }

        public HTMLTool ToolImage
        {
            get { return this.sculptureToolImage; }
        }

        public HTMLTool ToolText
        {
            get { return this.sculptureToolText; }
        }

        public static Project CurrentProject
        {
            get { return Project.currentProject; }
            set { Project.currentProject = value; }
        }

        public bool NotSaved
        {
            get { return this.hasErrorSave; }
        }

        public string ErrorReason
        {
            get { return this.reason; }
        }
        #endregion

        #region Public Methods
        public bool FindContainer(string searchName, out IContainer found, SearchContainer del)
        {
            List<IContainer> containers = new List<IContainer>(this.MasterPages.Cast<IContainer>());
            containers.AddRange(this.MasterObjects.Cast<IContainer>());
            return del(containers, new List<IContent>(), searchName, out found);
        }

        public void ReloadProject()
        {
            this.openProject();
        }
        #endregion

        #region Private Static Methods

        private static void ImportPages(Folder f, Tree<IProjectElement> t)
        {
            t.Push();
            foreach (Page p in f.Pages)
            {
                t.Add(p);
            }
            foreach (string s in f.Files)
            {
                t.Add(new File(s));
            }
            foreach (Folder subFolder in f.Folders)
            {
                t.Add(subFolder);
                Project.ImportPages(subFolder, t);
            }
            t.Pop();
        }

        private static void ImportTools(FolderTool f, Tree<IProjectElement> t)
        {
            t.Push();
            foreach (HTMLTool h in f.Tools)
            {
                t.Add(h);
            }
            foreach (FolderTool ft in f.Folders)
            {
                t.Add(ft);
                Project.ImportTools(ft, t);
            }
            t.Pop();
        }

        private static void ImportMasterPages(List<MasterPage> list, Tree<IProjectElement> t)
        {
            t.Push();
            foreach (MasterPage p in list)
            {
                t.Add(p);
            }
            t.Pop();
        }

        private static void ImportMasterObjects(List<MasterObject> list, Tree<IProjectElement> t)
        {
            t.Push();
            foreach (MasterObject o in list)
            {
                t.Add(o);
            }
            t.Pop();
        }

        private static void ImportSculptures(List<SculptureObject> list, Tree<IProjectElement> t)
        {
            t.Push();
            foreach (SculptureObject o in list)
            {
                t.Add(o);
            }
            t.Pop();
        }

        private static void CopyTo(Project to, Node<IProjectElement> n, List<string> needs)
        {
            if (n.Object.TypeName == "ProjectCap")
            {
                switch (n.Object.ElementTitle)
                {
                    case "MasterPages":
                        foreach (Library.Node<Library.IProjectElement> sub in n)
                        {
                            if (sub.IsSelected)
                            {
                                MasterPage p = sub.Object as MasterPage;
                                p = p.Clone() as MasterPage;
                                // recherche des objets (soit des tools, soit des master objects)
                                foreach (HTMLObject o in p.Objects)
                                {
                                    if (o.IsMasterObject)
                                    {
                                        needs.Add(o.MasterObjectName);
                                    }
                                }
                                to.MasterPages.Add(p);
                            }
                        }
                        break;
                    case "MasterObjects":
                        foreach (Library.Node<Library.IProjectElement> sub in n)
                        {
                            if (sub.IsSelected)
                            {
                                MasterObject o = sub.Object as MasterObject;
                                o = o.Clone() as MasterObject;
                                foreach (HTMLObject html in o.Objects)
                                {
                                    if (html.IsMasterObject)
                                    {
                                        needs.Add(html.MasterObjectName);
                                    }
                                }
                                to.MasterObjects.Add(o);
                            }
                        }
                        break;
                    case "Sculptures":
                        foreach (Library.Node<Library.IProjectElement> sub in n)
                        {
                            if (sub.IsSelected)
                            {
                                SculptureObject s = sub.Object as SculptureObject;
                                to.SculptureObjects.Add(s.Clone() as SculptureObject);
                            }
                        }
                        break;
                    case "Folders":
                        foreach (Library.Node<Library.IProjectElement> sub in n)
                        {
                            to.Folders.Import(sub);
                        }
                        break;
                    case "Tools":
                        foreach (Library.Node<Library.IProjectElement> sub in n)
                        {
                            to.Tools.Import("", sub);
                        }
                        break;
                }
            }
        }



        #endregion

        #region Public Static Methods

        public static void Save(Project p, string path, string fileName)
        {
            FileInfo fi = new FileInfo(Path.Combine(path, fileName));
            if (fi.Exists)
            {
                fi.CopyTo(Path.Combine(path, Path.GetFileNameWithoutExtension(fileName) + ".bak"), true);
            }
            ++p.Revision;
            BinaryFormatter bf = new BinaryFormatter();
            p.ModificationDate = DateTime.Now;
            try
            {
                using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    bf.Serialize(fs, p);
                    fs.Close();
                }
                p.hasErrorSave = false;
            }
            catch(Exception ex)
            {
                FileInfo oldFi = new FileInfo(Path.Combine(path, Path.GetFileNameWithoutExtension(fileName) + ".bak"));
                if (oldFi.Exists)
                    oldFi.CopyTo(Path.Combine(path, fileName), true);
                p.hasErrorSave = true;
                p.reason = ex.Message;
            }
        }

        public static Project Load(string path, string fileName, OpenProject del)
        {
            CadreModel.ReinitCounter(0);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Open);
            Project np = bf.Deserialize(fs) as Project;
            fs.Close();
            fs.Dispose();
            if (np == null)
            {
                throw new FormatException(String.Format(Localization.Strings.GetString("ExceptionProjectNotLoaded"), fileName));
            }
            // reinit counter CadreModel
            np.openProject = del;
            Project.CurrentProject = np;
            np.ReloadProject();
            return np;
        }

        public static Project Load(string path, string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Open);
            Project np = bf.Deserialize(fs) as Project;
            fs.Close();
            fs.Dispose();
            if (np == null)
            {
                throw new FormatException(String.Format(Localization.Strings.GetString("ExceptionProjectNotLoaded"), fileName));
            }
            np.openProject = Project.CurrentProject.openProject;
            Project.CurrentProject = np;
            return np;
        }

        public static Tree<IProjectElement> LoadImport(string path, string fileName)
        {
            Project np = null;
            Tree<IProjectElement> t = new Tree<IProjectElement>();
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Open))
                {
                    np = bf.Deserialize(fs) as Project;
                    fs.Close();
                }
            }
            catch { }
            if (np != null)
            {
                t.Add(new ProjectCap("MasterPages"));
                Project.ImportMasterPages(np.MasterPages, t);

                t.Add(new ProjectCap("MasterObjects"));
                Project.ImportMasterObjects(np.MasterObjects, t);

                t.Add(new ProjectCap("Folders"));
                Project.ImportPages(np.Folders, t);

                t.Add(new ProjectCap("Tools"));
                Project.ImportTools(np.Tools, t);

                t.Add(new ProjectCap("Sculptures"));
                Project.ImportSculptures(np.SculptureObjects, t);
            }
            else
            {
                throw new FormatException(String.Format(Localization.Strings.GetString("ExceptionProjectNotLoaded"), fileName));
            }
            return t;
        }

        public static void Import(Tree<IProjectElement> from, Project to, ImportType type)
        {
            Node<IProjectElement> node;
            switch (type)
            {
                case ImportType.TOOLS:
                    node = from.Root.Find(a => a.Object.ElementTitle == "Tools");
                    break;
                case ImportType.MASTERPAGES:
                    node = from.Root.Find(a => a.Object.ElementTitle == "MasterPages");
                    break;
                case ImportType.MASTEROBJECTS:
                    node = from.Root.Find(a => a.Object.ElementTitle == "MasterObjects");
                    break;
                case ImportType.SCULPTURES:
                    node = from.Root.Find(a => a.Object.ElementTitle == "Sculptures");
                    break;
                case ImportType.FILES:
                    node = from.Root.Find(a => a.Object.ElementTitle == "Folders");
                    break;
                default:
                    return;
            }
            List<string> masterObjectToAdd = new List<string>();
            Project.CopyTo(to, node, masterObjectToAdd);
            node = from.Root.Find(a => a.Object.ElementTitle == "MasterObjects");
            for (int index = 0; index < masterObjectToAdd.Count; ++index)
            {
                string name = masterObjectToAdd[index];
                Node<IProjectElement> masterObjectNode = node.Find(a => a.Object.ElementTitle == name);
                masterObjectNode.IsSelected = true;
                Project.CopyTo(to, node, masterObjectToAdd);
            }
        }

        public static bool AddFile(Project proj, string fileName)
        {
            Folder rootFolder = proj.Folders;
            Folder currentFolder = rootFolder;
            string[] list = fileName.Split('/');
            IEnumerator el = list.GetEnumerator();
            string lastItem = String.Empty;
            if (el.MoveNext())
            {
                do
                {
                    if (!String.IsNullOrEmpty(lastItem))
                    {
                        if (!currentFolder.Folders.Exists(a => { return a.Name == lastItem; }))
                        {
                            Folder newFolder = new Folder();
                            newFolder.Name = lastItem;
                            newFolder.Ancestor = currentFolder;
                            currentFolder.Folders.Add(newFolder);
                            currentFolder = newFolder;
                        }
                        else
                        {
                            currentFolder = currentFolder.Folders.Find(a => { return a.Name == lastItem; });
                        }
                    }
                    if (!String.IsNullOrEmpty((string)el.Current))
                        lastItem = (string)el.Current;
                }
                while (el.MoveNext());
            }
            if (!String.IsNullOrEmpty(lastItem))
            {
                if (!currentFolder.Files.Exists(s => s == lastItem))
                    currentFolder.Files.Add(lastItem);
                return true;
            }
            return false;
        }

        public static bool AddPage(Project proj, Page page, string fileName)
        {
            Folder rootFolder = proj.Folders;
            Folder currentFolder = rootFolder;
            string[] list = fileName.Split('/');
            IEnumerator el = list.GetEnumerator();
            string lastItem = null;
            if (el.MoveNext())
            {
                do
                {
                    if (!String.IsNullOrEmpty(lastItem))
                    {
                        if (!currentFolder.Folders.Exists(a => { return a.Name == lastItem; }))
                        {
                            Folder newFolder = new Folder();
                            newFolder.Name = lastItem;
                            newFolder.Ancestor = currentFolder;
                            currentFolder.Folders.Add(newFolder);
                            currentFolder = newFolder;
                        }
                        else
                        {
                            currentFolder = currentFolder.Folders.Find(a => { return a.Name == lastItem; });
                        }
                    }
                    if (!String.IsNullOrEmpty((string)el.Current))
                        lastItem = (string)el.Current;
                }
                while (el.MoveNext());
            }
            if (!String.IsNullOrEmpty(lastItem))
            {
                page.Ancestor = currentFolder;
                page.Name = lastItem;
                currentFolder.Pages.Add(page);
                return true;
            }
            return false;
        }

        public static bool AddTool(Project proj, HTMLTool tool, string toolName)
        {
            Library.FolderTool rootFolder = proj.Tools;
            Library.FolderTool currentFolder = rootFolder;
            string[] list = toolName.Split('/');
            IEnumerator el = list.GetEnumerator();
            string last = String.Empty;
            string oldPath = String.Empty;
            if (el.MoveNext())
            {
                do
                {
                    if (!String.IsNullOrEmpty(last))
                    {
                        if (!currentFolder.Folders.Exists(a => { return a.Name == last; }))
                        {
                            Library.FolderTool newFolder = new Library.FolderTool();
                            newFolder.Path = oldPath;
                            newFolder.Name = last;
                            if (!String.IsNullOrEmpty(oldPath))
                                oldPath = oldPath + System.IO.Path.AltDirectorySeparatorChar + last;
                            else
                                oldPath = last;
                            currentFolder.Folders.Add(newFolder);
                            currentFolder = newFolder;
                        }
                        else
                        {
                            currentFolder = currentFolder.Folders.Find(a => { return a.Name == last; });
                        }
                    }
                    if (!String.IsNullOrEmpty((string)el.Current))
                    {
                        last = (string)el.Current;
                    }
                }
                while (el.MoveNext());
            }
            if (!String.IsNullOrEmpty(last))
            {
                tool.Title = last;
                tool.Path = oldPath;
                currentFolder.Tools.Add(tool);
                return true;
            }
            return false;
        }

        public static void EnsureSculptureGeneration(Project proj)
        {
            if (proj.sculptureToolImage == null)
            {
                // créer un tool image
                HTMLTool newToolImage = new HTMLTool();
                newToolImage.ConstraintHeight = EnumConstraint.AUTO;
                newToolImage.ConstraintWidth = EnumConstraint.AUTO;
                proj.sculptureToolImage = newToolImage;
                newToolImage.CSS.Body.Add("background-position", "center");
                newToolImage.CSS.Body.Add("background-repeat", "no-repeat");
                Project.AddTool(proj, proj.sculptureToolImage, "/#sculpture/image");
            }
            if (proj.sculptureToolText == null)
            {
                // créer un tool texte
                HTMLTool newToolText = new HTMLTool();
                newToolText.ConstraintHeight = EnumConstraint.AUTO;
                newToolText.ConstraintWidth = EnumConstraint.AUTO;
                proj.sculptureToolText = newToolText;
                Project.AddTool(proj, proj.sculptureToolText, "/#sculpture/texte");
            }
        }

        public static HTMLObject InstanciateSculptureTool(Project proj, CadreModel model)
        {
            HTMLObject obj = null;
            Project.EnsureSculptureGeneration(proj);
            if (model.SelectedModelTypeObject.Type == CadreModelType.Image)
            {
                obj = new HTMLObject(proj.sculptureToolImage);
                model.CopyProperties(obj);
                obj.CSS.BackgroundImageURL = model.SelectedModelTypeObject.Content;
                proj.Instances.Add(obj);
            }
            else if (model.SelectedModelTypeObject.Type == CadreModelType.Text)
            {
                obj = new HTMLObject(proj.sculptureToolText);
                model.CopyProperties(obj);
                obj.HTML = model.SelectedModelTypeObject.Content;
                proj.Instances.Add(obj);
            }
            else if (model.SelectedModelTypeObject.Type == CadreModelType.Tool)
            {
                obj = new HTMLObject(model.SelectedModelTypeObject.DirectObject);
                model.CopyProperties(obj);
                proj.Instances.Add(obj);
            }
            else if (model.SelectedModelTypeObject.Type == CadreModelType.MasterObject)
            {
                obj = new HTMLObject();
                obj.Title = model.SelectedModelTypeObject.DirectObject.Title;
                obj.MasterObjectName = model.SelectedModelTypeObject.DirectObject;
                model.CopyProperties(obj);
                proj.Instances.Add(obj);
            }
            return obj;
        }

        public static void InitializeTraceCounter()
        {
            Project.traceCounter = 0;
        }

        public static int TraceCounter
        {
            get { return Project.traceCounter; }
        }

        public static int IncrementedTraceCounter
        {
            get { return ++Project.traceCounter; }
        }
        #endregion
    }
}
