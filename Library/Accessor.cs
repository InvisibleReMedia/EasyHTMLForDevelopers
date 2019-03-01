using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// This class holds a data type name
    /// and a counter to select one element from a specific list
    /// with the same data type item
    /// </summary>
    [Serializable]
    public class Accessor : Marshalling.PersistentDataObject, IEquatable<Accessor>
    {

        #region Fields

        /// <summary>
        /// Index name for data type holder
        /// </summary>
        protected static readonly string dataTypeName = "dataType";
        /// <summary>
        /// Index name for the counting item from the data type list related
        /// </summary>
        protected static readonly string uniqueName = "unique";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type">data type</param>
        /// <param name="u">string</param>
        public Accessor(string type, string u)
        {
            this.Set(dataTypeName, type);
            this.Set(uniqueName, u);
        }

        /// <summary>
        /// Constructor for an accessor with a master page
        /// </summary>
        /// <param name="mp">master page list</param>
        /// <param name="u">unique name to search</param>
        public Accessor(List<MasterPage> mp, string u)
        {
            MasterPage m = mp.Find(x => x.Unique == u);
            if (m != null)
            {
                this.Set(dataTypeName, Project.MasterPagesName);
                this.Set(uniqueName, u);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Constructor for an accessor with a master object
        /// </summary>
        /// <param name="mo">master object list</param>
        /// <param name="u">unique name to search</param>
        public Accessor(List<MasterObject> mo, string u)
        {
            MasterObject m = mo.Find(x => x.Unique == u);
            if (m != null)
            {
                this.Set(dataTypeName, Project.MasterObjectsName);
                this.Set(uniqueName, u);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Constructor for an accessor with a page
        /// </summary>
        /// <param name="p">page list</param>
        /// <param name="u">unique name to search</param>
        public Accessor(List<Page> p, string u)
        {
            Page m = p.Find(x => x.Unique == u);
            if (m != null)
            {
                this.Set(dataTypeName, Project.PagesName);
                this.Set(uniqueName, u);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Constructor for an accessor with a tool
        /// </summary>
        /// <param name="t">tool list</param>
        /// <param name="u">unique name to search</param>
        public Accessor(List<HTMLTool> t, string u)
        {
            HTMLTool h = t.Find(x => x.Unique == u);
            if (h != null)
            {
                this.Set(dataTypeName, Project.ToolsName);
                this.Set(uniqueName, u);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Constructor for an accessor with an instance
        /// </summary>
        /// <param name="o">instance list</param>
        /// <param name="u">unique name to search</param>
        public Accessor(List<HTMLObject> o, string u)
        {
            HTMLObject h = o.Find(x => x.Unique == u);
            if (h != null)
            {
                this.Set(dataTypeName, Project.InstancesName);
                this.Set(uniqueName, u);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Constructor for an accessor with a file
        /// </summary>
        /// <param name="f">file list</param>
        /// <param name="u">unique name to search</param>
        public Accessor(List<File> f, string u)
        {
            File h = f.Find(x => x.Unique == u);
            if (h != null)
            {
                this.Set(dataTypeName, Project.FilesName);
                this.Set(uniqueName, u);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique name
        /// </summary>
        public string Unique
        {
            get { return this.Get(uniqueName, ""); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the requested object
        /// </summary>
        /// <param name="p">project input</param>
        /// <returns>an object into this project</returns>
        public dynamic GetObject(Project p)
        {
            string type = this.Get(dataTypeName);
            if (type == Project.MasterPagesName)
            {
                return p.MasterPages.Find(x => x.Unique == this.Get(uniqueName));
            }
            else if (type == Project.MasterObjectsName)
            {
                return p.MasterObjects.Find(x => x.Unique == this.Get(uniqueName));
            }
            else if (type == Project.PagesName)
            {
                return p.Pages.Find(x => x.Unique == this.Get(uniqueName));
            }
            else if (type == Project.ToolsName)
            {
                return p.Tools.Find(x => x.Unique == this.Get(uniqueName)); ;
            }
            else if (type == Project.InstancesName)
            {
                return p.Instances.Find(x => x.Unique == this.Get(uniqueName));
            }
            else if (type == Project.SculpturesName)
            {
                return p.SculptureObjects.Find(x => x.Unique == this.Get(uniqueName));
            }
            else if (type == Project.FilesName)
            {
                return p.Files.Find(x => x.Unique == this.Get(uniqueName)); ;
            }
            else
                return null;
        }

        /// <summary>
        /// Test equality
        /// </summary>
        /// <param name="other">with</param>
        /// <returns>true if equals</returns>
        public bool Equals(Accessor other)
        {
            return this.Unique.Equals(other.Unique);
        }

        #endregion

    }
}
