using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Element of a project that acts as a content
    /// </summary>
    public interface IContent
    {
        /// <summary>
        /// Gets or sets the container name
        /// </summary>
        string Container { get; set; }
        /// <summary>
        /// Search the container where this content resides
        /// </summary>
        /// <param name="containers">container list</param>
        /// <param name="objects">objects of content</param>
        /// <param name="searchName">container name to search</param>
        /// <param name="found">container result</param>
        /// <returns>true if a container has found</returns>
        bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found);
    }
}
