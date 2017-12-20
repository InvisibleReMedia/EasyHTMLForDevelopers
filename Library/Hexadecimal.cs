using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Hexadecimal
    {
        #region Private Fields
        private int code;
        #endregion

        #region Public Constructors
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

        public Hexadecimal(int val)
        {
            this.code = val;
        }
        #endregion

        #region Public Properties
        public int Value
        {
            get { return this.code; }
        }
        #endregion

        #region Public Methods

        public char HexadecimalDigitToChar(int val)
        {
            const string output = "0123456789ABCDEF";
            return output[val];
        }

        #endregion

        #region Overriden Public Methods
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
        public static string ToString(int val, int radix)
        {
            return new Hexadecimal(val).ToString(radix);
        }

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
