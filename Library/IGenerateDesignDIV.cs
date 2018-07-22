using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Interface for the generation of HTML DIV tag (design mode)
    /// </summary>
    interface IGenerateDesignDIV
    {
        /// <summary>
        /// Generate table on design
        /// </summary>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignDIV();
        /// <summary>
        /// Generate HTML DIV tag at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignDIV(Page refPage);
        /// <summary>
        /// Generate HTML DIV tag at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="objects">objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate HTML DIV tag at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate HTML DIV tag at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="objects">objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }

    /// <summary>
    /// Interface for the generation of HTML DIV tag (actual website)
    /// </summary>
    interface IGenerateProductionDIV
    {
        /// <summary>
        /// Generate HTML DIV tag at actual website
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProductionDIV(Page refPage);
        /// <summary>
        /// Generate HTML DIV tag at actual website
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate HTML DIV tag at actual website
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="objects">objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }
}
