using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    public class File : Marshalling.MarshallingHash
    {

        /// <summary>
        /// Empty constructor
        /// </summary>
        public File()
            : base("File")
        {

        }

        /// <summary>
        /// Constructor default
        /// </summary>
        /// <param name="name">name</param>
        public File(string name)
            : base(name)
        {

        }

        /// <summary>
        /// Gets extension
        /// </summary>
        public string Extension
        {
            get
            {
                return this.GetValue("Extension", "html");
            }
        }

        /// <summary>
        /// Gets file name
        /// </summary>
        public string FileName
        {
            get
            {
                return this.GetValue("FileName", string.Empty);
            }
        }

        /// <summary>
        /// Gets path
        /// </summary>
        public StringBuilder Path
        {
            get
            {
                return this.GetValue("Path", new StringBuilder());
            }
        }

        /// <summary>
        /// Export extension
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportExtension()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.Extension));
        }

        /// <summary>
        /// Export file name
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportFileName()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.FileName));
        }

        /// <summary>
        /// Export path
        /// </summary>
        /// <returns>ux cell</returns>
        public UXFramework.UXCell ExportPath()
        {
            return UXFramework.Creation.CreateCell(null, UXFramework.Creation.CreateReadOnlyText(null, "text", this.Path.ToString()));
        }

        /// <summary>
        /// Export to row
        /// </summary>
        /// <returns>ux row</returns>
        public UXFramework.UXRow ExportFileToRow()
        {
            return UXFramework.Creation.CreateRow(3, null, ExportPath(), ExportFileName(), ExportExtension());
        }

        /// <summary>
        /// Create a file
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>ok</returns>
        public static bool CreateFile(string fileName, string input)
        {
            using (FileStream fs = new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName),
                                                  FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(input);
                    sw.Close();
                }
                fs.Close();
            }
            return true;
        }

        /// <summary>
        /// Write a file
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>ok</returns>
        public static bool WriteFile(string fileName, string input)
        {
            using (FileStream fs = new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName),
                                                  FileMode.Truncate, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(input);
                    sw.Close();
                }
                fs.Close();
            }
            return true;
        }

        /// <summary>
        /// Read a file
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>ok</returns>
        public static bool ReadFile(string fileName, out string output)
        {
            using (FileStream fs = new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName),
                                                  FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sw = new StreamReader(fs))
                {
                    output = sw.ReadToEnd();
                    sw.Close();
                }
                fs.Close();
            }
            return true;
        }

        /// <summary>
        /// Rewrite the file
        /// </summary>
        /// <param name="fileName">file name to rewrite</param>
        /// <param name="f">formatter</param>
        /// <returns>ok</returns>
        public static bool RewriteFile(string fileName, Formatter f)
        {
            string content;
            if (ReadFile(fileName, out content))
            {
                string newContent = f.Replace(content);
                if (WriteFile(fileName, newContent))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
