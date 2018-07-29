using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// View for a data table
    /// </summary>
    public class UXViewDataTable : UXControl
    {

        #region Fields

        /// <summary>
        /// Data list
        /// </summary>
        private Marshalling.MarshallingList list;
        /// <summary>
        /// Id
        /// </summary>
        private string id;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">id</param>
        public UXViewDataTable(string id)
        {
            this.id = id;
            this.list = new Marshalling.MarshallingList("projectList");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Bind sequence
        /// </summary>
        /// <param name="a">specific bind function</param>
        public void Bind(Action<Marshalling.MarshallingList> a)
        {
            a(list);
        }

        #endregion
    }
}
