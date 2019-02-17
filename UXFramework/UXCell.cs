using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// One cell in a row
    /// </summary>
    public class UXCell : UXControl
    {

        #region Default Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXCell()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXCell(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }


        #endregion

        #region Static Methods

        /// <summary>
        /// Create UXRow
        /// </summary>
        /// <param name="data">data hash</param>
        /// <param name="ui">ui properties</param>
        /// <returns>row</returns>
        public static UXCell CreateUXCell(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXCell cell = new UXCell();
            cell.Bind(data);
            cell.Bind(ui);

            if (cell.Exists("Content"))
            {
                IUXObject obj = cell["Content"].Value;
                cell.Add(obj);
            }
            return cell;
        }

        #endregion


    }
}
