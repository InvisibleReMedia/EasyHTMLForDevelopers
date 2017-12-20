using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class Folder : IProjectElement, ICloneable
    {
        private string name;
        private List<Folder> folders = new List<Folder>();
        private List<Page> pages = new List<Page>();
        private List<string> files = new List<string>();
        private Folder folder;

        public Folder()
        {
        }

        /// <summary>
        /// Copy constructor (object reference is already used to set ancestor)
        /// </summary>
        private Folder(string name, List<Folder> subFolders, List<Page> pages, List<string> files, Folder ancestor)
        {
            this.name = ExtensionMethods.CloneThis(name);
            foreach (Folder subFolder in subFolders)
            {
                Folder newFolder = new Folder(subFolder.name, subFolder.folders, subFolder.pages, subFolder.files, this);
                this.folders.Add(newFolder);
            }
            foreach (Page p in pages)
            {
                this.pages.Add(p.Clone() as Page);
            }
            foreach (string s in files)
            {
                this.files.Add(s.Clone() as string);
            }
            this.folder = ancestor;
        }

        public Folder(Folder ancestor)
        {
            this.folder = ancestor;
        }

        public Folder Ancestor
        {
            get { return this.folder; }
            set { this.folder = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public List<Folder> Folders
        {
            get { return this.folders; }
        }

        public List<Page> Pages
        {
            get { return this.pages; }
        }

        public List<string> Files
        {
            get { return this.files; }
        }

        public void Import(Node<IProjectElement> node)
        {
            if (node.IsSelected)
            {
                switch (node.Object.TypeName)
                {
                    case "FolderTool":
                        Folder newFolder = this.Folders.Find(a => { return a.Name == node.Object.ElementTitle; });
                        if (newFolder == null)
                        {
                            newFolder = new Folder(this);
                            newFolder.Name = node.Object.ElementTitle;
                            this.Folders.Add(newFolder);
                        }
                        else
                        {
                            newFolder = newFolder.Clone() as Folder;
                        }
                        foreach (Node<IProjectElement> subNode in node)
                        {
                            newFolder.Import(subNode);
                        }
                        break;
                    case "Page":
                        this.Pages.Add(node.Object as Page);
                        break;
                    case "File":
                        this.Files.Add(node.Object.ToString());
                        break;
                }
            }
        }

        public string TypeName
        {
            get { return "Folder"; }
        }

        public string ElementTitle
        {
            get { return this.name; }
        }

        public object Clone()
        {
            Folder nf = new Folder(this.name, this.folders, this.pages, this.files, this.folder);
            return nf;
        }
    }
}
