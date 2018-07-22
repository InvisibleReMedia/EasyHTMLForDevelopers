using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Class implements Hexadecimal value
    /// </summary>
    public class Hexadecimal
    {

        #region Private Fields

        /// <summary>
        /// code hexa
        /// </summary>
        private int code;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="input">input string in hexadecimal digits</param>
        public Hexadecimal(string input)
        {
            try
            {
                this.code = Hexadecimal.ToHexadecimal(input);
            }
            catch (ArgumentException e)
            {
                throw new FormatException(Localization.Strings.GetString("ExceptionNotHexadecimal"), e);
            }
            catch (OverflowException e)
            {
                throw new Exception(Localization.Strings.GetString("ExceptionHexadecimalOverflow"), e);
            }
        }

        /// <summary>
        /// Constructor with an int input value
        /// </summary>
        /// <param name="val">input value</param>
        public Hexadecimal(int val)
        {
            this.code = val;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the int value
        /// </summary>
        public int Value
        {
            get { return this.code; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the hexadecimal digit of a value
        /// </summary>
        /// <param name="val">value from 0 to 15</param>
        /// <returns></returns>
        public char HexadecimalDigitToChar(int val)
        {
            const string output = "0123456789ABCDEF";
            return output[val];
        }

        #endregion

        #region Overriden Public Methods

        /// <summary>
        /// Converts the int value into a string representation
        /// </summary>
        /// <param name="radix">maximum number of digits</param>
        /// <returns></returns>
        public string ToString(int radix)
        {
            string output = String.Empty;
            int val = this.code;
            while (val > 0)
            {
                output = this.HexadecimalDigitToChar(val % 16) + output;
                val /= 16;
            }
            output = output.PadLeft(radix, '0');
            return output;
        }
        #endregion

        #region Public Static Methods

        /// <summary>
        /// Converts an int into an hexadecimal string
        /// with a radix as the maximum number of digits
        /// </summary>
        /// <param name="val">value</param>
        /// <param name="radix">maximum number of digits</param>
        /// <returns>hexadecimal value</returns>
        public static string ToString(int val, int radix)
        {
            return new Hexadecimal(val).ToString(radix);
        }

        /// <summary>
        /// Converts an hexadecimal digit into an int value
        /// </summary>
        /// <param name="input">input hexadecimal char</param>
        /// <returns>int value</returns>
        public static int ToHexadecimalDigit(char input)
        {
            char c = Char.ToLower(input);
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            else if (c >= 'a' && c <= 'f')
            {
                return c - 'a' + 10;
            }
            else
            {
                throw new ArgumentException(Localization.Strings.GetString("ExceptionNotHexadecimalChar"));
            }
        }

        /// <summary>
        /// Converts an hexadecimal string into an int
        /// </summary>
        /// <param name="input">input hexadecimal string</param>
        /// <returns>int</returns>
        public static int ToHexadecimal(string input)
        {
            int output = 0;
            foreach (char c in input)
            {
                try
                {
                    output *= 16;
                    int digitOut = Hexadecimal.ToHexadecimalDigit(c);
                    output += digitOut;
                }
                catch (OverflowException)
                {
                    break;
                }
            }
            return output;
        }

        /// <summary>
        /// Parse an hexadecimal input string and returns
        /// an object of hexadecimal class
        /// </summary>
        /// <param name="input">hexadecimal input</param>
        /// <param name="result">object of hexadecimal class</param>
        /// <returns>true if successfull parsing</returns>
        public static bool TryParse(string input, out Hexadecimal result)
        {
            try
            {
                result = new Hexadecimal(input);
                return true;
            }
            catch
            {
                result = new Hexadecimal("0");
                return false;
            }
        }

        #endregion
    }
}
