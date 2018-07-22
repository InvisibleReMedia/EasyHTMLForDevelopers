using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Interface for the generation of table (design mode)
    /// </summary>
    interface IGenerateDesignTable
    {
        /// <summary>
        /// Generate table at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignTable(Page refPage);
        /// <summary>
        /// Generate table at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="objects">objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate table at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate table at design
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="objects">objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }

    /// <summary>
    /// Interface for the generation of table (actual website)
    /// </summary>
    interface IGenerateProductionTable
    {
        /// <summary>
        /// Generate table at actual website
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProductionTable(Page refPage);
        /// <summary>
        /// Generate table at actual website
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        /// <summary>
        /// Generate table at actual website
        /// </summary>
        /// <param name="refPage">from a page</param>
        /// <param name="masterRefPage">from a master page</param>
        /// <param name="objects">objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }
}
