using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// CSS list
    /// </summary>
    [Serializable]
    public class CSSList : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for css list
        /// </summary>
        private static readonly string cssListName = "cssList";

        #endregion

        #region Constructor

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CSSList()
        {
            this.Set(cssListName, new List<CodeCSS>());
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="list">input list</param>
        public CSSList(List<CodeCSS> list)
        {
            this.Set(cssListName, (from CodeCSS c in list select c.Clone() as CodeCSS).ToList());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets elements
        /// </summary>
        public IEnumerable<CodeCSS> Elements
        {
            get { return this.List; }
        }

        /// <summary>
        /// Gets the list
        /// </summary>
        public List<CodeCSS> List
        {
            get { return this.Get(cssListName, new List<CodeCSS>()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new CSS Properties
        /// </summary>
        /// <param name="props"></param>
        public void AddCSS(CodeCSS props)
        {
            this.List.Add(props);
        }

        /// <summary>
        /// Import CSS
        /// </summary>
        /// <param name="list"></param>
        public void ImportCSS(List<CodeCSS> list)
        {
            foreach(CodeCSS c in list)
            {
                this.AddCSS(c.Clone() as CodeCSS);
            }
        }

        /// <summary>
        /// Find a css from id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>css</returns>
        public CodeCSS Find(string id)
        {
            return this.List.Find(x => x.Ids == id);
        }

        /// <summary>
        /// Generates the CSS code
        /// </summary>
        /// <param name="addDefaultKeys">add default keys</param>
        /// <param name="resolveConfig">transforms configuration keys to values</param>
        /// <returns>css string generated code output</returns>
        public string GenerateCSS(bool addDefaultKeys, bool resolveConfig = false)
        {
            string output = string.Empty;
            foreach (CodeCSS c in this.Elements)
            {
                output += c.GenerateCSS(addDefaultKeys, true, resolveConfig);
                output += Environment.NewLine + Environment.NewLine;
            }
            return output;
        }

        /// <summary>
        /// Generate CSS without principal id
        /// </summary>
        /// <param name="principalId">principal</param>
        /// <param name="addDefaultKeys">add default key</param>
        /// <param name="resolveConfig">resolve config</param>
        /// <returns>string output</returns>
        public string GenerateCSSWithoutPrincipal(string principalId, bool addDefaultKeys, bool resolveConfig = false)
        {
            string output = string.Empty;
            foreach (CodeCSS c in this.GetListWithoutPrincipal(principalId))
            {
                output += c.GenerateCSS(addDefaultKeys, true, resolveConfig);
                output += Environment.NewLine + Environment.NewLine;
            }
            return output;
        }

        /// <summary>
        /// Rename a css element
        /// </summary>
        /// <param name="currentId">current id</param>
        /// <param name="nextId">new id</param>
        public void RenamePrincipalCSS(string currentId, string nextId)
        {
            CodeCSS objCSS = this.List.Find(x => x.Ids == "#" + currentId);
            objCSS.Ids = "#" + nextId;
        }

        /// <summary>
        /// Get list without principal element
        /// </summary>
        /// <param name="principalId">principal id</param>
        /// <returns>list</returns>
        public List<CodeCSS> GetListWithoutPrincipal(string principalId)
        {
            return (from x in this.List where x.Ids != principalId select x).ToList();
        }

        /// <summary>
        /// Remove css properties
        /// with this id
        /// </summary>
        /// <param name="id">id</param>
        public void RemoveCSS(string id)
        {
            CodeCSS css = this.List.Find(x => x.Ids == id);
            this.List.Remove(css);
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public object Clone()
        {
            return new CSSList(this.List);
        }

        #endregion

    }
}
