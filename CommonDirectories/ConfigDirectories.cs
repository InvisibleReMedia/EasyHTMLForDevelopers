using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CommonDirectories
{
    public static class ConfigDirectories
    {
        private static string GetMyDocumentsFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EasyHTML") + Path.DirectorySeparatorChar;
        }

        public static bool RemoveTables
        {
            get { return false; }
        }

        public static string GetDocumentsFolder()
        {
            CreateMyDocuments();
            return ConfigDirectories.GetMyDocumentsFolder();
        }

        public static string GetBuildFolder(string projectName)
        {
            ConfigDirectories.CreateDirectoryProject(projectName);
            return Path.Combine(ConfigDirectories.GetMyDocumentsFolder(), projectName, "build") + Path.DirectorySeparatorChar;
        }

        public static string GetBuildFolder(string projectName, string fileName)
        {
            ConfigDirectories.CreateDirectoryProject(projectName);
            return Path.Combine(ConfigDirectories.GetMyDocumentsFolder(), projectName, "build", fileName);
        }

        public static string GetLogicalFolder(string projectName, string fileName)
        {
            return ConfigDirectories.RemoveLeadBackslash(ConfigDirectories.GetBuildFolder(projectName, fileName));
        }

        public static string GetDefaultProductionFolder(string projectName)
        {
            ConfigDirectories.CreateDirectoryProject(projectName);
            return Path.Combine(ConfigDirectories.GetMyDocumentsFolder(), projectName, "production") + Path.DirectorySeparatorChar;
        }

        public static void CreateMyDocuments()
        {
            DirectoryInfo di = new DirectoryInfo(ConfigDirectories.GetMyDocumentsFolder());
            if (!di.Exists)
            {
                di.Create();
            }
        }

        public static void CreateDirectoryProject(string projectName)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(ConfigDirectories.GetDocumentsFolder(), projectName));
            if (!di.Exists)
            {
                di.Create();
            }
            di = new DirectoryInfo(Path.Combine(ConfigDirectories.GetDocumentsFolder(), projectName, "build"));
            if (!di.Exists)
            {
                di.Create();
            }
            di = new DirectoryInfo(Path.Combine(ConfigDirectories.GetDocumentsFolder(), projectName, "production"));
            if (!di.Exists)
            {
                di.Create();
            }
            FileInfo fi = new FileInfo(Path.Combine(ConfigDirectories.GetDocumentsFolder(), projectName, "build", "ehd_ask.png"));
            if (!fi.Exists)
            {
                FileInfo src = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "ehd_ask.png");
                src.CopyTo(fi.FullName, true);
            }
            fi = new FileInfo(Path.Combine(ConfigDirectories.GetDocumentsFolder(), projectName, "build", "ehd_plus.png"));
            if (!fi.Exists)
            {
                FileInfo src = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "ehd_plus.png");
                src.CopyTo(fi.FullName, true);
            }
            fi = new FileInfo(Path.Combine(ConfigDirectories.GetDocumentsFolder(), projectName, "build", "ehd_minus.png"));
            if (!fi.Exists)
            {
                FileInfo src = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "ehd_minus.png");
                src.CopyTo(fi.FullName, true);
            }
        }

        public static void RenameDirectoryProject(string oldProjectName, string newProjectName)
        {
            DirectoryInfo di = new DirectoryInfo(ConfigDirectories.GetDocumentsFolder() + oldProjectName);
            DirectoryInfo ddest = new DirectoryInfo(ConfigDirectories.GetDocumentsFolder() + newProjectName);
            if (di.Exists && !ddest.Exists)
            {
                di.MoveTo(ConfigDirectories.GetDocumentsFolder() + newProjectName);
            }
            else
            {
                throw new IOException();
            }
            FileInfo fi = new FileInfo(ConfigDirectories.GetMyDocumentsFolder() + oldProjectName + ".bin");
            FileInfo fdest = new FileInfo(ConfigDirectories.GetMyDocumentsFolder() + newProjectName + ".bin");
            if (fi.Exists && !fdest.Exists)
            {
                fi.MoveTo(ConfigDirectories.GetDocumentsFolder() + newProjectName + ".bin");
            }
            else
            {
                throw new IOException();
            }
        }

        public static void AddFolder(string projectName, string folder)
        {
            DirectoryInfo di = new DirectoryInfo(ConfigDirectories.GetBuildFolder(projectName));
            if (!di.Exists)
            {
                di.Create();
            }
            string[] path = folder.Split('/');
            foreach (string s in path)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    DirectoryInfo subDir = new DirectoryInfo(Path.Combine(di.FullName, s));
                    if (!subDir.Exists)
                    {
                        subDir.Create();
                    }
                    di = subDir;
                }
            }
        }

        public static void AddFile(string projectName, string fileName, string srcFile)
        {
            string[] path = fileName.Split('/');
            AddFolder(projectName, String.Join("/", path, 0, path.Length - 1));
            FileInfo src = new FileInfo(srcFile);
            if (src.Exists && fileName != srcFile)
            {
                src.CopyTo(fileName, true);
            }
        }

        public static void AddProductionFolder(string projectName, string folder, string destinationDirectory)
        {
            DirectoryInfo di = new DirectoryInfo(destinationDirectory);
            if (!di.Exists)
            {
                di.Create();
            }
            string[] path = folder.Split('/');
            foreach (string s in path)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    DirectoryInfo subDir = new DirectoryInfo(Path.Combine(di.FullName, s));
                    if (!subDir.Exists)
                    {
                        subDir.Create();
                    }
                    di = subDir;
                }
            }
        }

        public static void AddProductionFile(string projectName, string fileName, string srcFile, string destinationDirectory)
        {
            string[] path = fileName.Split('/');
            AddProductionFolder(projectName, String.Join("/", path, 0, path.Length - 1), destinationDirectory);
            FileInfo fi = new FileInfo(Path.Combine(destinationDirectory, fileName));
            FileInfo src = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + srcFile);
            if (src.Exists)
            {
                src.CopyTo(fi.FullName, true);
            }
        }

        public static string RemoveLeadBackslash(string s)
        {
            string lead = s.Replace("\\", "/");
            if (lead.StartsWith("/"))
            {
                return lead.Substring(1);
            }
            else
            {
                return lead;
            }
        }

        public static string RemoveLeadSlash(string s)
        {
            string lead = s.Replace("/", "\\");
            if (lead.StartsWith("\\"))
            {
                return lead.Substring(1);
            }
            else
            {
                return lead;
            }
        }
    }
}
