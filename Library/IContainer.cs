using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Interface for a container
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Gets or sets the width constraint
        /// </summary>
        EnumConstraint ConstraintWidth { get; set; }
        /// <summary>
        /// Gets or sets the height constraint
        /// </summary>
        EnumConstraint ConstraintHeight { get; set; }
        /// <summary>
        /// Gets or sets the width value
        /// </summary>
        uint Width { get; set; }
        /// <summary>
        /// Gets or sets the height value
        /// </summary>
        uint Height { get; set; }
        /// <summary>
        /// Gets or sets the name of the container
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Search the container where this container is
        /// </summary>
        /// <param name="containers">container list</param>
        /// <param name="objects">objects of content</param>
        /// <param name="searchName">container name to search</param>
        /// <param name="found">container result</param>
        /// <returns>true if a container has found</returns>
        bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found);
    }
}
