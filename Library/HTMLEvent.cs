using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// HTML Event relation
    /// </summary>
    [Serializable]
    public class HTMLEvent : Marshalling.PersistentDataObject, INotify
    {

        #region Fields

        /// <summary>
        /// Id for Event name
        /// </summary>
        private static readonly string eventNameName = "eventName";

        /// <summary>
        /// raise list
        /// </summary>
        private static readonly string funcImpl = "functionList";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="eventName">event name</param>
        public HTMLEvent(string eventName)
        {
            this.Set(eventNameName, eventName);
            this.Set(funcImpl, new List<Func<object, EventArgs, string>>());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if this event is server side
        /// </summary>
        public bool IsServerSide
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the notification name
        /// </summary>
        public string NotificationName
        {
            get
            {
                return this.Get(eventNameName, "");
            }

            set
            {
                this.Set(eventNameName, value);
            }
        }

        /// <summary>
        /// Gets the list of actions
        /// </summary>
        public List<Func<object, EventArgs, string>> Raise
        {
            get { return this.Get(funcImpl, new List<Func<object, EventArgs, string>>()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Catch events
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        /// <returns>code</returns>
        public string Catch(object sender, EventArgs e)
        {
            string output = string.Empty;
            foreach (Func<object, EventArgs, string> a in this.Raise)
            {
                output += a(sender, e);
            }
            return output;
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            HTMLEvent ev = new HTMLEvent(this.NotificationName);
            foreach(Func<object, EventArgs, string> r in this.Raise)
            {
                ev.Raise.Add((Func<object, EventArgs, string>)r.Clone());
            }
            return ev;
        }

        #endregion
    }
}
