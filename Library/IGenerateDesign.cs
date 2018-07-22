using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Interface for the generation of page (design mode)
    /// </summary>
    interface IGenerateDesign
    {
        /// <summary>
        /// Generate a thumbnail
        /// </summary>
        /// <returns>html output</returns>
        OutputHTML GenerateThumbnail();
        /// <summary>
        /// Generate the top-level design
        /// </summary>
        /// <returns>html output</returns>
        OutputHTML GenerateDesign();
        /// <summary>
        /// Generate the next level (from a page) design
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesign(Page refPage);
        /// <summary>
        /// Generate the third level (from a page or a master page)
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate objects owned by a page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">objects owned</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate objects owned by a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">objects owned</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }

    /// <summary>
    /// Interface for the generation of page (design mode)
    /// </summary>
    interface IGenerateProduction
    {
        /// <summary>
        /// Generate the top-level design
        /// </summary>
        /// <returns>html output</returns>
        OutputHTML GenerateProduction();
        /// <summary>
        /// Generate the next level (from a page) at actual website
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProduction(Page refPage);
        /// <summary>
        /// Generate the third level (from a page or a master page) at actual website
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate objects owned by a page or a master page at actual website
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">objects owned</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }
}
