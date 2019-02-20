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
                if (this.Exists("CountColumn"))
                {
                    return this.Get("CountColumn").Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Count of line
        /// </summary>
        public int CountLine
        {
            get
            {
                if (this.Exists("CountLine"))
                {
                    return this.Get("CountLine").Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// first column
        /// </summary>
        public int ColumnStart
        {
            get
            {
                if (this.Exists("ColumnStart"))
                {
                    return this.Get("ColumnStart").Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// First line
        /// </summary>
        public int LineStart
        {
            get
            {
                if (this.Exists("LineStart"))
                {
                    return this.Get("LineStart").Value;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
