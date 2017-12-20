using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialListening
{
    public interface IAutomaticClick
    {
        /// <summary>
        /// Name of this action
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Class name to call
        /// </summary>
        string Class { get; }
        /// <summary>
        /// Field name to test
        /// </summary>
        string Field { get; }
        /// <summary>
        /// Gets a delay before click
        /// </summary>
        TimeSpan Delay { get; }
        /// <summary>
        /// Type of the action
        /// click, double-click or select/focus
        /// </summary>
        string ActionType { get; }
    }
}
