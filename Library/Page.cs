using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Library
{
    /// <summary>
    /// A page is a complete HTML Page (it generates a complete file)
    /// </summary>
    [Serializable]
    public class Page : Marshalling.PersistentDataObject, IContainer, IProjectElement, IGenerateDesign, IGenerateProduction, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for unique id
        /// </summary>
        protected static readonly string uniqueName = "unique";
        /// <summary>
        /// Index name for disposition
        /// </summary>
        protected static readonly string dispositionName = "disposition";
        /// <summary>
        /// Index name for width constraint
        /// </summary>
        protected static readonly string constraintWidthName = "constraintWidth";
        /// <summary>
        /// Index name for height constraint
        /// </summary>
        protected static readonly string constraintHeightName = "constraintHeight";
        /// <summary>
        /// Index name for automatic name
        /// </summary>
        protected static readonly string nameName = "name";
        /// <summary>
        /// Index name for related master page name
        /// </summary>
        protected static readonly string masterPageNameName = "masterPageName";
        /// <summary>
        /// Index name for width value
        /// </summary>
        protected static readonly string widthName = "width";
        /// <summary>
        /// Index name for height value
        /// </summary>
        protected static readonly string heightName = "height";
        /// <summary>
        /// Index name for its own objects
        /// </summary>
        protected static readonly string objectListName = "objects";
        /// <summary>
        /// Index name for the hosted folder
        /// </summary>
        protected static readonly string folderObjectName = "folder";
        /// <summary>
        /// Index name for events
        /// </summary>
        protected static readonly string eventsName = "events";

        [NonSerialized]
        private OutputHTML specificOutput;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Page()
        {
            this.Set(folderObjectName, null);
            this.Set(eventsName, new Events());
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="refp">input page</param>
        private Page(Page refp)
        {
            this.Disposition = refp.Disposition;
            this.ConstraintWidth = refp.ConstraintWidth;
            this.ConstraintHeight = refp.ConstraintHeight;
            this.Name = ExtensionMethods.CloneThis(refp.Name);
            this.MasterPageName = ExtensionMethods.CloneThis(refp.MasterPageName);
            this.Width = refp.Width;
            this.Height = refp.Height;
            this.Set(eventsName, refp.Events.Clone());
            foreach (HTMLObject obj in refp.Objects)
            {
                this.Objects.Add(obj.Clone() as HTMLObject);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique id
        /// </summary>
        public string Unique
        {
            get { return this.Get(uniqueName); }
            set { this.Set(uniqueName, value); }
        }

        /// <summary>
        /// Gets or sets the disposition
        /// </summary>
        public Disposition Disposition
        {
            get { return this.Get(dispositionName, new Disposition()); }
            set { this.Set(dispositionName, value); }
        }

        /// <summary>
        /// Gets or sets the disposition text
        /// </summary>
        public string DispositionText
        {
            get { return this.Disposition.ToString(); }
            set
            {
                Disposition d;
                if (Enum.TryParse(value, out d))
                    this.Disposition = d;
            }
        }

        /// <summary>
        /// Gets or sets the width constraint
        /// </summary>
        public EnumConstraint ConstraintWidth
        {
            get { return this.Get(constraintWidthName, EnumConstraint.AUTO); }
            set { this.Set(constraintWidthName, value); }
        }

        /// <summary>
        /// Gets or sets the height constraint
        /// </summary>
        public EnumConstraint ConstraintHeight
        {
            get { return this.Get(constraintHeightName, EnumConstraint.AUTO); }
            set { this.Set(constraintHeightName, value); }
        }

        /// <summary>
        /// Gets or sets the width value
        /// </summary>
        public uint Width
        {
            get { return this.Get(widthName, 0u); }
            set { this.Set(widthName, value); }
        }

        /// <summary>
        /// Gets or sets the height value
        /// </summary>
        public uint Height
        {
            get { return this.Get(heightName, 0u); }
            set { this.Set(heightName, value); }
        }

        /// <summary>
        /// Gets or sets the name of this page
        /// </summary>
        public string Name
        {
            get { return this.Get(nameName, ""); }
            set { this.Get(nameName, value); }
        }

        /// <summary>
        /// Gets or sets the master page name
        /// </summary>
        public string MasterPageName
        {
            get { return this.Get(masterPageNameName, ""); }
            set { this.Set(masterPageNameName, value); }
        }

        /// <summary>
        /// Gets or sets a specific output
        /// </summary>
        public OutputHTML SpecificOutput
        {
            get { return this.specificOutput; }
            set { this.specificOutput = value; }
        }

        /// <summary>
        /// Gets objects hosted by this page
        /// </summary>
        public List<HTMLObject> Objects
        {
            get { return this.Get(objectListName, new List<HTMLObject>()); }
        }

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string TypeName
        {
            get { return "Page"; }
        }

        /// <summary>
        /// Gets the element title
        /// </summary>
        public string ElementTitle
        {
            get { return this.Name; }
        }

        /// <summary>
        /// Gets events
        /// </summary>
        public Events Events
        {
            get
            {
                return this.Get(eventsName, new Events());
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Search a container from container list
        /// </summary>
        /// <param name="containers">all containers</param>
        /// <param name="searchName">container name to search</param>
        /// <param name="found">container</param>
        /// <returns>true if a container has found</returns>
        public bool SearchContainer(List<IContainer> containers, string searchName, out IContainer found)
        {
            List<IContent> contents = new List<IContent>(this.Objects.Cast<IContent>());
            return this.SearchContainer(containers, contents, searchName, out found);
        }

        /// <summary>
        /// Search a container from a list of containers and a list of contents
        /// </summary>
        /// <param name="containers">list of container</param>
        /// <param name="objects">list of objects</param>
        /// <param name="searchName">container name to search</param>
        /// <param name="found">container</param>
        /// <returns>true if a container has found</returns>
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

        /// <summary>
        /// Generate page for design
        /// A page is the top-level of generation
        /// so, this function works with no argument
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign()
        {
            MasterPage selectedMp = Project.CurrentProject.MasterPages.Find(mp => { return mp.Name == this.MasterPageName; });
            if (selectedMp != null)
            {
                OutputHTML output = selectedMp.GenerateDesign(this);
                return output;
            }
            else
            {
                throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterPageNotExists"), this.MasterPageName, this.Name));
            }
        }

        /// <summary>
        /// Generate page from a referenced page
        /// As a page is the top-level of generation, no other page is generated
        /// </summary>
        /// <param name="refPage"></param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate page for design
        /// A page is the top-level of generation, so any argument is not needed
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate page for design
        /// A page is the top-level of generation, so any argument is not needed
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Generate page for design
        /// A page is the top-level of generation, so any argument is not needed
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate a thumbnail of a page
        /// This function should be implemented
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateThumbnail()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Generate page for actual website
        /// A page is the top-level of generation
        /// so, this function works with no argument
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction()
        {
            MasterPage selectedMp = Project.CurrentProject.MasterPages.Find(mp => { return mp.Name == this.MasterPageName; });
            if (selectedMp != null)
            {
                OutputHTML output = selectedMp.GenerateProduction(this);
                return output;
            }
            else
            {
                throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterPageNotExists"), this.MasterPageName, this.Name));
            }
        }

        /// <summary>
        /// Generate page from a referenced page
        /// As a page is the top-level of generation, no other page is generated
        /// </summary>
        /// <param name="refPage"></param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate page for actual website
        /// A page is the top-level of generation, so any argument is not needed
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate page for actual website
        /// A page is the top-level of generation, so any argument is not needed
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            Page newPage = new Page(this);
            return newPage;
        }

        #endregion

    }
}
