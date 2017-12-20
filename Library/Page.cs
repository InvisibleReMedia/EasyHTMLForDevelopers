using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Library
{
    [Serializable]
    public class Page : IContainer, IProjectElement, IGenerateDesign, IGenerateProduction, ICloneable
    {
        #region Private Fields

        private Disposition disposition;
        private EnumConstraint constraintWidth, constraintHeight;
        private string name;
        private string masterPageName;
        private uint width;
        private uint height;
        private List<HTMLObject> objects = new List<HTMLObject>();
        private Folder folder;
        [NonSerialized]
        private OutputHTML specificOutput;

        #endregion

        #region Public Constructor

        public Page()
        {
            this.folder = null;
        }

        public Page(Folder ancestor)
        {
            this.folder = ancestor;
        }

        private Page(Page refp)
        {
            this.disposition = refp.disposition;
            this.constraintWidth = refp.constraintWidth;
            this.constraintHeight = refp.constraintHeight;
            this.name = ExtensionMethods.CloneThis(refp.name);
            this.masterPageName = ExtensionMethods.CloneThis(refp.masterPageName);
            this.width = refp.width;
            this.height = refp.height;
            foreach (HTMLObject obj in refp.objects)
            {
                this.objects.Add(obj.Clone() as HTMLObject);
            }
            this.folder = ExtensionMethods.CloneThis(refp.folder);
        }

        #endregion

        #region Public Properties

        public Disposition Disposition
        {
            get { return this.disposition; }
            set { this.disposition = value; }
        }

        public string DispositionText
        {
            get { return this.disposition.ToString(); }
            set
            {
                Enum.TryParse(value, out this.disposition);
            }
        }

        public EnumConstraint ConstraintWidth
        {
            get { return this.constraintWidth; }
            set { this.constraintWidth = value; }
        }

        public EnumConstraint ConstraintHeight
        {
            get { return this.constraintHeight; }
            set { this.constraintHeight = value; }
        }

        public uint Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public uint Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string MasterPageName
        {
            get { return this.masterPageName; }
            set { this.masterPageName = value; }
        }

        public OutputHTML SpecificOutput
        {
            get { return this.specificOutput; }
            set { this.specificOutput = value; }
        }

        public List<HTMLObject> Objects
        {
            get { return this.objects; }
        }

        public Folder Ancestor
        {
            get { return this.folder; }
            set { this.folder = value; }
        }

        public string Path
        {
            get
            {
                string output = String.Empty;
                Folder current = this.folder;
                while (current.Ancestor != null)
                {
                    if (!String.IsNullOrEmpty(output))
                        output = current.Name + "/" + output;
                    else
                        output = current.Name;
                    current = current.Ancestor;
                }
                return output + (!String.IsNullOrEmpty(output) ? "/" : "");
            }
        }

        #endregion

        #region Public Methods

        public bool SearchContainer(List<IContainer> containers, string searchName, out IContainer found)
        {
            List<IContent> contents = new List<IContent>(this.Objects.Cast<IContent>());
            return this.SearchContainer(containers, contents, searchName, out found);
        }

        public bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found)
        {
            found = null;
            bool done = false;
            foreach (IContainer cont in containers)
            {
                if (cont.Name == this.MasterPageName)
                {
                    done = cont.SearchContainer(containers, objects, searchName, out found);
                    if (done) break;
                }
            }
            return done;
        }

        public OutputHTML GenerateDesign()
        {
            MasterPage selectedMp = Project.CurrentProject.MasterPages.Find(mp => { return mp.Name == this.masterPageName; });
            if (selectedMp != null)
            {
                OutputHTML output = selectedMp.GenerateDesign(this);
                return output;
            }
            else
            {
                throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterPageNotExists"), this.MasterPageName, this.Path, this.Name));
            }
        }

        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }


        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateThumbnail()
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction()
        {
            MasterPage selectedMp = Project.CurrentProject.MasterPages.Find(mp => { return mp.Name == this.masterPageName; });
            if (selectedMp != null)
            {
                OutputHTML output = selectedMp.GenerateProduction(this);
                return output;
            }
            else
            {
                throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterPageNotExists"), this.MasterPageName, this.Path, this.Name));
            }
        }

        public OutputHTML GenerateProduction(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public string TypeName
        {
            get { return "Page"; }
        }

        public string ElementTitle
        {
            get { return this.name; }
        }

        public object Clone()
        {
            Page newPage = new Page(this);
            return newPage;
        }

        #endregion
    }
}
