using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Hat of a Project
    /// </summary>
    public class ProjectCap : IProjectElement
    {

        #region Fields

        /// <summary>
        /// title
        /// </summary>
        private string title;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="title">title</param>
        public ProjectCap(string title)
        {
            this.title = title;
        }

        #endregion

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string TypeName
        {
            get { return "ProjectCap"; }
        }

        /// <summary>
        /// Gets the element title
        /// </summary>
        public string ElementTitle
        {
            get { return this.title; }
            set { this.title = value; }
        }
    }
}
