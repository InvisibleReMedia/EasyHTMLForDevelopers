using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// Bring a row in a table
    /// </summary>
    public class UXRow : UXControl
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXRow()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXRow(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the column count
        /// </summary>
        public int ColumnCount
        {
            get { return this.Get("ColumnCount").Value; }
        }

        /// <summary>
        /// Gets if this row is selectable
        /// </summary>
        public bool IsSelectable
        {
            get
            {
                if (this.Exists("IsSelectable"))
                {
                    return this.Get("IsSelectable").Value;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets if this row is clickable
        /// </summary>
        public bool IsClickable
        {
            get
            {
                if (this.Exists("IsClickable"))
                {
                    return this.Get("IsClickable").Value;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the background ground selectable
        /// </summary>
        public string BackgroundSelectable
        {
            get
            {
                if (this.Exists("Background-Selectable"))
                {
                    return this.Get("Background-Selectable").Value;
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create UXRow
        /// </summary>
        /// <param name="data">data hash</param>
        /// <param name="ui">ui properties</param>
        /// <returns>row</returns>
        public static UXRow CreateUXRow(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXRow row = new UXRow();
            row.Bind(data);
            row.Bind(ui);
            foreach (Marshalling.IMarshalling m in row.GetProperty("childs").Values)
            {
                row.Add(m.Value);
            }
            return row;
        }

        /// <summary>
        /// Create UXRow
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXRow CreateUXRow(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXRow(name, f());
        }

        #endregion

    }
}
