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
        /// Column size and line size
        /// </summary>
        private uint columns, lines;
        /// <summary>
        /// Colorize, borderize, dispositioning horizontal and vertical
        /// </summary>
        private SizeArgs[,] verticalCustomizer;
        private SizeArgs[] horizontalCustomizer;

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
            /// <summary>
            /// Padding size in pixels
            /// </summary>
            public int paddingSize;
            /// <summary>
            /// Object content to link with one part of the table
            /// </summary>
            public IUXObject content;

            #endregion

            #region Constructors

            /// <summary>
            /// Empty constructor
            /// </summary>
            public SizeArgs()
            {
                this.isValid = false;
            }

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
            /// Make this set valid
            /// </summary>
            public void MakeValid()
            {
                this.isValid = true;
            }

            /// <summary>
            /// Common values
            /// </summary>
            /// <param name="columnIndex">column index</param>
            /// <param name="lineIndex">line index</param>
            /// <param name="w">width in pixels</param>
            /// <param name="h">height in pixels</param>
            /// <param name="columnCount">column count</param>
            /// <param name="lineCount">line count</param>
            /// <param name="l">left corner index</param>
            /// <param name="t">top corner index</param>
            public void Common(uint columnIndex, uint lineIndex, int w, int h, int columnCount, int lineCount, int l, int t)
            {
                this.columnNumber = columnIndex;
                this.lineNumber = lineIndex;
                this.width = w;
                this.height = h;
                this.columnSize = columnCount;
                this.lineSize = lineCount;
                this.left = l;
                this.top = t;
            }

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
            /// <param name="ps">padding size</param>
            /// <param name="obj">ux object</param>
            public void Options(Disposition d, EnumConstraint w, EnumConstraint h, CSSColor b, CSSColor a, CSSColor f, int bs, int ps, IUXObject obj)
            {
                this.disposition = d;
                this.constraintWidth = w;
                this.constraintHeight = h;
                this.backgroundColor = (CSSColor)b.Clone();
                this.borderColor = (CSSColor)a.Clone();
                this.textColor = (CSSColor)f.Clone();
                this.borderSize = bs;
                this.paddingSize = ps;
                this.content = obj;
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
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the column count
        /// </summary>
        public uint ColumnCount
        {
            get { return this.columns; }
            set { this.columns = value; this.verticalCustomizer = new SizeArgs[this.lines, this.columns]; }
        }

        /// <summary>
        /// Gets the column count
        /// </summary>
        public uint LineCount
        {
            get { return this.lines; }
            set { this.lines = value; this.horizontalCustomizer = new SizeArgs[this.lines]; }
        }

        /// <summary>
        /// Horizontal customization information
        /// </summary>
        public SizeArgs[] HorizontalCustomization
        {
            get { return this.horizontalCustomizer; }
        }

        /// <summary>
        /// Vertical ustomization information
        /// </summary>
        public SizeArgs[,] VerticalCustomization
        {
            get { return this.verticalCustomizer; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set the horizontal of this table
        /// </summary>
        /// <param name="l">line size</param>
        public void SetHorizontal(uint l, Action<object, SizeArgs> a)
        {
            this.lines = l;
            this.horizontalCustomizer = new SizeArgs[this.lines];
            for (uint pos_line = 0; pos_line < this.lines; ++pos_line)
            {
                SizeArgs current = new SizeArgs();
                current.lineNumber = pos_line;
                a.Invoke(this, current);
                if (current.isValid)
                    this.HorizontalCustomization[pos_line] = current;
            }
        }

        /// <summary>
        /// Set the vertical of this table
        /// </summary>
        /// <param name="c">column size</param>
        /// <param name="l">line size</param>
        public void SetVertical(uint c, uint l, Action<object, SizeArgs> a)
        {
            this.columns = c;
            this.lines = l;
            this.verticalCustomizer = new SizeArgs[this.lines, this.columns];
            for (uint pos_line = 0; pos_line < this.lines; ++pos_line)
            {
                for(uint pos_column = 0; pos_column < this.columns; ++pos_column)
                {
                    SizeArgs current = new SizeArgs();
                    current.lineNumber = pos_line;
                    current.columnNumber = pos_column;
                    a.Invoke(this, current);
                    if (current.isValid)
                        this.VerticalCustomization[pos_line, pos_column] = current;
                }
            }
        }

        #endregion

    }
}
