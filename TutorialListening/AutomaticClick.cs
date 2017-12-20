using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialListening
{
    class AutomaticClick : IAutomaticClick
    {

        #region Private Fields

        private string name;
        private string className;
        private string fieldName;
        private TimeSpan delay;
        private string actionType;
        private Dictionary<string, object> parameters;

        #endregion

        #region Public Constructor

        public AutomaticClick(string name, string className, string fieldName, TimeSpan delay, string actionType, Dictionary<string, object> dict)
        {
            this.name = name;
            this.className = className;
            this.fieldName = fieldName;
            this.delay = new TimeSpan(delay.Hours, delay.Minutes, delay.Seconds);
            this.actionType = actionType;
            this.parameters = dict;
        }

        #endregion

        #region IAutomaticClick Members

        public string Name
        {
            get { return this.name; }
        }

        public string Class
        {
            get { return this.className; }
        }

        public string Field
        {
            get { return this.fieldName; }
        }

        public TimeSpan Delay
        {
            get { return this.delay; }
        }

        public string ActionType
        {
            get { return this.actionType; }
        }

        public Dictionary<string, object> Parameters
        {
            get { return this.parameters; }
        }

        #endregion

    }
}
