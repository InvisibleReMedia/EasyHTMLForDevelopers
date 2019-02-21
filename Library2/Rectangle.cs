using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    /// <summary>
    /// Rectangle
    /// </summary>
    public class Rectangle : Marshalling.MarshallingHash
    {

        /// <summary>
        /// Constructor default
        /// </summary>
        public Rectangle()
            : base("rect")
        {

        }

        /// <summary>
        /// Count of column
        /// </summary>
        public int CountColumn
        {
            get
            {
                return this.GetValue("CountColumn", 0);
            }
        }

        /// <summary>
        /// Count of line
        /// </summary>
        public int CountLine
        {
            get
            {
                return this.GetValue("CountLine", 0);
            }
        }

        /// <summary>
        /// first column
        /// </summary>
        public int ColumnStart
        {
            get
            {
                return this.GetValue("ColumnStart", 0);
            }
        }

        /// <summary>
        /// First line
        /// </summary>
        public int LineStart
        {
            get
            {
                return this.GetValue("LineStart", 0);
            }
        }
    }
}
