using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class FolderTool : IProjectElement, ICloneable
    {
        private string name;
        private string path;
        private List<FolderTool> folders = new List<FolderTool>();
        private List<HTMLTool> tools = new List<HTMLTool>();

        #region Default Constructor
        public FolderTool()
        {

        }
        #endregion

        #region Copy constructor
        private FolderTool(FolderTool ft)
        {
            this.name = ExtensionMethods.CloneThis(ft.name);
            this.path = ExtensionMethods.CloneThis(ft.path);
            foreach (FolderTool subFolder in ft.folders)
            {
                this.folders.Add(subFolder.Clone() as FolderTool);
            }
            foreach (HTMLTool tool in ft.tools)
            {
                this.tools.Add(tool.Clone() as HTMLTool);
            }
        }
        #endregion

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }


        public List<FolderTool> Folders
        {
            get { return this.folders; }
        }

        public List<HTMLTool> Tools
        {
            get { return this.tools; }
        }

        public void Import(string path, Node<IProjectElement> node)
        {
            if (node.IsSelected) {
                switch (node.Object.TypeName)
                {
                    case "FolderTool":
                        FolderTool newFolder = this.Folders.Find(a => { return a.Name == node.Object.ElementTitle; });
                        if (newFolder == null)
                        {
                            newFolder = new FolderTool();
                            newFolder.Path = path;
                            newFolder.Name = node.Object.ElementTitle;
                            this.Folders.Add(newFolder);
                        }
                        else
                        {
                            newFolder = newFolder.Clone() as FolderTool;
                        }
                        foreach (Node<IProjectElement> subNode in node)
                        {
                            newFolder.Import(path + System.IO.Path.AltDirectorySeparatorChar + node.Object.ElementTitle, subNode);
                        }
                        break;
                    case "HTMLTool":
                        this.Tools.Add(node.Object as HTMLTool);
                        break;
                }
            }
        }

        public void Import(string oldPath, FolderTool from)
        {
            foreach (FolderTool folder in from.Folders)
            {
                FolderTool newFolder = this.Folders.Find(a => { return a.Name == folder.Name; });
                if (newFolder == null)
                {
                    newFolder = new FolderTool();
                    newFolder.Path = oldPath;
                    newFolder.Name = folder.Name;
                    this.Folders.Add(newFolder);
                }
                newFolder.Import(oldPath + System.IO.Path.AltDirectorySeparatorChar + folder.Name, folder);
            }
            foreach (HTMLTool tool in from.Tools)
            {
                HTMLTool newTool = tool.Clone() as HTMLTool;
                this.Tools.Add(newTool);
            }
        }

        public FolderTool Find(string toolPath)
        {
            Library.FolderTool currentFolder = this;
            string[] list = toolPath.Split('/');
            IEnumerator el = list.GetEnumerator();
            string last = String.Empty;
            if (el.MoveNext())
            {
                do
                {
                    if (!String.IsNullOrEmpty(last))
                    {
                        currentFolder = currentFolder.Folders.Find(a => { return a.Name == last; });
                        if (currentFolder == null)
                        {
                            throw new ArgumentException(Localization.Strings.GetString("ExceptionPathNotExists"), "toolPath");
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
                HTMLTool tool = currentFolder.Tools.Find(a => a.Name == last);
                if (tool == null)
                    throw new ArgumentException(Localization.Strings.GetString("ExceptionToolNotExists"), "toolPath");
                else
                    return currentFolder;
            }
            else
            {
                throw new ArgumentException(Localization.Strings.GetString("ExceptionIncompletPath"), "toolPath");
            }
        }

        public string TypeName
        {
            get { return "FolderTool"; }
        }

        public string ElementTitle
        {
            get { return this.name; }
        }

        public object Clone()
        {
            FolderTool newObject = new FolderTool(this);
            return newObject;
        }
    }
}
