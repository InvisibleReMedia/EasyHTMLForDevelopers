using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialListening
{
    public class Tutorial : ITutorial
    {
        #region Public Delegate
        public delegate void PerformInteractiveWork(string className, string fieldName, string actionType);
        #endregion

        #region Private Fields

        private Exception lastEx;
        private string mediaFilename;
        private bool isMediaLoaded;
        private bool isMediaPlaying;
        private List<IAutomaticClick> clicks;
        private System.Media.SoundPlayer player;
        private System.Threading.Thread t;
        private System.Threading.SynchronizationContext syncContext;
        private System.Threading.AutoResetEvent autoEvt;
        private Tutorial.PerformInteractiveWork work;

        #endregion

        #region Public Constructor

        public Tutorial(string mediaFilename, Tutorial.PerformInteractiveWork work)
        {
            this.lastEx = null;
            this.isMediaLoaded = false;
            this.isMediaPlaying = false;
            this.syncContext = System.Threading.SynchronizationContext.Current;
            this.autoEvt = new System.Threading.AutoResetEvent(false);
            this.mediaFilename = mediaFilename;
            this.clicks = new List<IAutomaticClick>();
            this.work = work;
        }

        #endregion

        #region Private Methods

        private void timerElapsed(object obj) {
            IAutomaticClick click = obj as IAutomaticClick;
            this.syncContext.Post(new System.Threading.SendOrPostCallback(o =>
            {
                this.work(click.Class, click.Field, click.ActionType);
            }), click);
            this.autoEvt.Set();
        }

        private void SimulateClicks()
        {
            foreach (IAutomaticClick click in this.clicks)
            {
                using (System.Threading.Timer ti = new System.Threading.Timer(new System.Threading.TimerCallback(this.timerElapsed), click, click.Delay, TimeSpan.Zero))
                {
                    this.autoEvt.WaitOne();
                }
            }
        }
        
        #endregion

        #region ITutorial Members

        public Exception LastException
        {
            get { return this.lastEx; }
        }

        public string MediaFilename
        {
            get
            {
                return this.mediaFilename;
            }
            set
            {
                this.mediaFilename = value;
            }
        }

        public bool IsMediaLoaded
        {
            get { return this.isMediaLoaded; }
        }

        public bool IsMediaPlaying
        {
            get { return this.isMediaPlaying; }
        }

        public void Play()
        {
            if (!String.IsNullOrEmpty(this.mediaFilename))
            {
                this.player = new System.Media.SoundPlayer(this.mediaFilename);
                this.isMediaLoaded = true;
            }
            if (this.t == null)
            {
                this.t = new System.Threading.Thread(new System.Threading.ThreadStart(this.SimulateClicks));
            }
            this.player.Play();
            this.isMediaPlaying = true;
            this.t.Start();
        }

        public void Stop()
        {
            if (this.player != null)
            {
                this.player.Stop();
                this.isMediaPlaying = false;
                this.player.Dispose();
                this.player = null;
                this.isMediaLoaded = false;
            }

            if (this.t != null)
            {
                this.t.Abort();
                this.t = null;
            }
        }

        public List<IAutomaticClick> Clicks
        {
            get { return this.clicks; }
        }

        public void Load(string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a perform action request
        /// </summary>
        /// <param name="title">title of action</param>
        /// <param name="formName">name of the form</param>
        /// <param name="fieldName">field name</param>
        /// <param name="delay">delayed time</param>
        /// <param name="actionType">action type</param>
        /// <param name="dict">parameters</param>
        public void AddClick(string title, string formName, string fieldName, TimeSpan delay, string actionType, Dictionary<string, object> dict)
        {
            this.clicks.Add(new AutomaticClick(title, formName, fieldName, delay, actionType, dict));
        }

        #endregion
    }
}
