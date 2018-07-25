using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Handle multiple Projects
    /// </summary>
    public static class Projects
    {

        #region Fields

        /// <summary>
        /// Dictionary of projects
        /// </summary>
        private static Dictionary<string, Project> projects = new Dictionary<string, Project>();

        #endregion

        /// <summary>
        /// Add a new project
        /// </summary>
        /// <param name="name">project title</param>
        /// <param name="p">project object</param>
        public static void Add(string name, Project p)
        {
            projects.Add(name, p);
        }

        /// <summary>
        /// Suppress a project from list
        /// </summary>
        /// <param name="name">project name</param>
        public static void Remove(string name)
        {
            projects.Remove(name);
        }

        /// <summary>
        /// Activates project and holds previous one
        /// </summary>
        /// <param name="name">project name</param>
        /// <param name="previous">previous project</param>
        /// <returns>true if ok</returns>
        public static bool Activate(string name, out string previous)
        {
            Project p;
            if (TrySelect(name, out p))
            {

                previous = (from r in projects where r.Value.Title == Project.CurrentProject.Title select r.Key).FirstOrDefault();
                Project.CurrentProject = p;
                return true;
            }
            else
            {
                previous = "";
                return false;
            }
        }

        /// <summary>
        /// Reactivates project
        /// </summary>
        /// <param name="name">project name</param>
        /// <returns>true if ok</returns>
        public static bool Reactivate(string name)
        {
            Project p;
            if (TrySelect(name, out p))
            {
                Project.CurrentProject = p;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Try to select an existing project
        /// </summary>
        /// <param name="name">project name</param>
        /// <param name="p">project handler</param>
        /// <returns>true if handled</returns>
        public static bool TrySelect(string name, out Project p)
        {
            if (projects.ContainsKey(name))
            {
                p = projects[name];
                Project.CurrentProject = p;
                return true;
            }
            else
            {
                p = null;
                return false;
            }
        }

    }
}
