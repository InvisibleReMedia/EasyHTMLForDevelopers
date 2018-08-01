using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Event list
    /// </summary>
    [Serializable]
    public class Events : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for notificationList
        /// </summary>
        private static readonly string notificationListName = "notifications";
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Events()
        {
            this.Set(notificationListName, new List<INotify>());
        }

        #endregion

        #region Properties

        /// <summary>
        /// List of events
        /// </summary>
        private List<INotify> List
        {
            get { return this.Get(notificationListName, new List<INotify>()); }
        }

        /// <summary>
        /// Gets the count of this list events
        /// </summary>
        public int Count
        {
            get { return this.List.Count; }
        }

        /// <summary>
        /// Gets the events
        /// </summary>
        public IEnumerable<INotify> Items
        {
            get { return this.List; }
        }

        /// <summary>
        /// Gets or sets the event
        /// </summary>
        /// <param name="evName">event name</param>
        /// <returns></returns>
        public INotify this[string evName]
        {
            get
            {
                INotify n = this.List.Find(x => x.NotificationName == evName);
                if (n == null)
                {
                    throw new KeyNotFoundException();
                }
                return n;
            }
            set
            {
                int n = this.List.FindIndex(x => x.NotificationName == evName);
                if (n != -1)
                {
                    this.List[n] = value;
                }
                else
                {
                    this.Add(value);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new event
        /// </summary>
        /// <param name="n">event</param>
        public void Add(INotify n)
        {
            this.List.Add(n);
        }

        /// <summary>
        /// Remove un element
        /// </summary>
        /// <param name="n"></param>
        public void Remove(INotify n)
        {
            List<INotify> list = this.List;
            INotify e = list.Find(x => x.GetHashCode() == n.GetHashCode());
            if (e != null)
            {
                list.Remove(e);
            }
        }

        /// <summary>
        /// To HTML String
        /// </summary>
        /// <returns>string</returns>
        public string ToHTMLString()
        {
            string output = string.Empty;
            foreach (INotify f in this.List)
            {
                if (!f.IsServerSide)
                {
                    if (!String.IsNullOrEmpty(output)) output += " ";
                    output += f.NotificationName + ":'" + f.Catch(this, new EventArgs()).Replace("\\", "\\\\").Replace("'", "\'") + "'";
                }
            }
            return output;
        }

        /// <summary>
        /// To server String
        /// </summary>
        /// <returns>string</returns>
        public string ToServerString()
        {
            string output = string.Empty;
            foreach (INotify f in this.List)
            {
                if (f.IsServerSide)
                {
                    if (!String.IsNullOrEmpty(output)) output += Environment.NewLine;
                    output += f.NotificationName + ":'" + f.Catch(this, new EventArgs()).Replace("\\", "\\\\").Replace("'", "\'") + "'";
                }
            }
            return output;
        }

        /// <summary>
        /// Convert html events to string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return this.ToHTMLString();
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Events newObj = new Events();
            foreach(INotify n in this.List)
            {
                newObj.Add((INotify)n.Clone());
            }
            return newObj;
        }

        #endregion

    }
}
