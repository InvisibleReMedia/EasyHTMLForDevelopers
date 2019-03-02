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
    public class CSSList : Marshalling.PersistentDataObject
    {

        #region Fields

        /// <summary>
        /// Index name for css list
        /// </summary>
        private static readonly string cssName = "css";

        #endregion

        #region Constructor

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CSSList()
        {
            this.Set(cssName, new List<CSSProperties>());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets elements
        /// </summary>
        public IEnumerable<CSSProperties> Elements
        {
            get { return this.List; }
        }

        /// <summary>
        /// Gets the list
        /// </summary>
        private List<CSSProperties> List
        {
            get { return this.Get(cssName, new List<CSSProperties>()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new CSS Properties
        /// </summary>
        /// <param name="props"></param>
        public void AddCSS(CSSProperties props)
        {
            this.List.Add(props);
        }

        /// <summary>
        /// Remove css properties
        /// with this id
        /// </summary>
        /// <param name="id">id</param>
        public void RemoveCSS(string id)
        {
            CSSProperties css = this.List.Find(x => x.Get(CSSProperties.idName) == id);
            this.List.Remove(css);
        }

        #endregion

    }
}
