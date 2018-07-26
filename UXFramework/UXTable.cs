using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace UXFramework
{
    /// <summary>
    /// View a table from ux
    /// </summary>
    public class UXTable : UXControl
    {

        #region Fields

        /// <summary>
        /// Contains the list of elements table
        /// </summary>
        private List<SizedRectangle> rects;
        /// <summary>
        /// Column size and line size
        /// </summary>
        private uint columns, lines;
        /// <summary>
        /// Colorize, borderize, dispositioning
        /// </summary>
        private SizeArgs[,] customizer;

        #endregion

        #region Inner Class

        public class SizeArgs
        {

            #region Public Fields

            /// <summary>
            /// Take this fields as valid
            /// </summary>
            public bool isValid;
            /// <summary>
            /// Column number zero-based index
            /// </summary>
            public uint columnNumber;
            /// <summary>
            /// Line number zero-based index
            /// </summary>
            public uint lineNumber;
            /// <summary>
            /// Width of this box in pixels
            /// </summary>
            public int width;
            /// <summary>
            /// Column size
            /// </summary>
            public int columnSize;
            /// <summary>
            /// Line size
            /// </summary>
            public int lineSize;
            /// <summary>
            /// Height of this box in pixels
            /// </summary>
            public int height;
            /// <summary>
            /// Left corner column index
            /// </summary>
            public int left;
            /// <summary>
            /// top corner line index
            /// </summary>
            public int top;
            /// <summary>
            /// Disposition
            /// </summary>
            public Disposition disposition;
            /// <summary>
            /// Constraints
            /// </summary>
            public EnumConstraint constraintWidth, constraintHeight;
            /// <summary>
            /// Colors
            /// </summary>
            public CSSColor backgroundColor, borderColor, textColor;
            /// <summary>
            /// Border size in pixels
            /// </summary>
            public int borderSize;

            #endregion

            #region Constructor

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="columnIndex">column index</param>
            /// <param name="lineIndex">line index</param>
            /// <param name="w">width in pixels</param>
            /// <param name="h">height in pixels</param>
            /// <param name="columnCount">column count</param>
            /// <param name="lineCount">line count</param>
            /// <param name="l">left corner index</param>
            /// <param name="t">top corner index</param>
            public SizeArgs(uint columnIndex, uint lineIndex, int w, int h, int columnCount, int lineCount, int l, int t)
            {
                this.isValid = false;
                this.columnNumber = columnIndex;
                this.lineNumber = lineIndex;
                this.width = w;
                this.height = h;
                this.columnSize = columnCount;
                this.lineSize = lineCount;
                this.left = l;
                this.top = t;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Options to change (graphics attributes)
            /// </summary>
            /// <param name="d">disposition</param>
            /// <param name="w">constraint on width</param>
            /// <param name="h">constraint on height</param>
            /// <param name="b">background color</param>
            /// <param name="a">border color</param>
            /// <param name="f">font color</param>
            /// <param name="bs">border size</param>
            public void Options(Disposition d, EnumConstraint w, EnumConstraint h, CSSColor b, CSSColor a, CSSColor f, int bs)
            {
                this.disposition = d;
                this.constraintWidth = w;
                this.constraintHeight = h;
                this.backgroundColor = (CSSColor)b.Clone();
                this.borderColor = (CSSColor)a.Clone();
                this.textColor = (CSSColor)f.Clone();
                this.borderSize = bs;
            }

            #endregion
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXTable()
        {
            this.rects = new List<SizedRectangle>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the rectangle list
        /// </summary>
        public List<SizedRectangle> Rectangles
        {
            get { return this.rects; }
        }

        /// <summary>
        /// Gets the column count
        /// </summary>
        public uint ColumnCount
        {
            get { return this.columns; }
        }

        /// <summary>
        /// Gets the column count
        /// </summary>
        public uint LineCount
        {
            get { return this.lines; }
        }

        /// <summary>
        /// Customization information
        /// </summary>
        public SizeArgs[,] Customization
        {
            get { return this.customizer; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set the size of this table
        /// </summary>
        /// <param name="c">column size</param>
        /// <param name="l">line size</param>
        public void SetSize(uint c, uint l, Action<object, SizeArgs> a)
        {
            this.columns = c;
            this.lines = l;
            
            SizedRectangle[,] indexes = new SizedRectangle[this.lines, this.columns];
            for (int index = 0; index < this.rects.Count; ++index)
            {
                SizedRectangle current = this.rects[index];
                indexes[current.Top, current.Left] = current;
            }
            this.rects.Clear();
            this.customizer = new SizeArgs[this.lines, this.columns];
            for (uint pos_line = 0; pos_line < this.lines; ++pos_line)
            {
                for(uint pos_column = 0; pos_column < this.columns; ++pos_column)
                {
                    SizedRectangle current = indexes[pos_line, pos_column];
                    if (current != null)
                    {
                        SizeArgs e = new SizeArgs(pos_column, pos_line, current.Width, current.Height, current.CountWidth, current.CountHeight, current.Left, current.Top);
                        a.Invoke(this, e);
                        if (e.isValid)
                        {
                            current.Width = e.width;
                            current.Height = e.height;
                            current.CountWidth = e.columnSize;
                            current.CountHeight = e.lineSize;
                            current.Left = e.left;
                            current.Top = e.top;
                            this.rects.Add(current);
                            this.customizer[pos_line, pos_column] = e;
                        }
                    }
                    else
                    {
                        SizeArgs e = new SizeArgs(pos_column, pos_line, 0, 0, 0, 0, 0, 0);
                        a.Invoke(this, e);
                        if (e.isValid)
                        {
                            current = new SizedRectangle();
                            current.Width = e.width;
                            current.Height = e.height;
                            current.CountWidth = e.columnSize;
                            current.CountHeight = e.lineSize;
                            current.Left = e.left;
                            current.Top = e.top;
                            this.rects.Add(current);
                            this.customizer[pos_line, pos_column] = e;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
