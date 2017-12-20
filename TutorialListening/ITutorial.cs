using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialListening
{
    public interface ITutorial
    {
        /// <summary>
        /// Take the last exception thrown
        /// </summary>
        Exception LastException { get; }
        /// <summary>
        /// Gets or sets the file name of the media to read
        /// </summary>
        string MediaFilename { get; set; }
        /// <summary>
        /// True if media is loaded
        /// </summary>
        bool IsMediaLoaded { get; }
        /// <summary>
        /// True if media is playing. Media is a wav file
        /// </summary>
        bool IsMediaPlaying { get; }
        /// <summary>
        /// Play the wav file
        /// </summary>
        void Play();
        /// <summary>
        /// Stop the wac file
        /// </summary>
        void Stop();
        /// <summary>
        /// List of clicks
        /// </summary>
        List<IAutomaticClick> Clicks { get; }
        /// <summary>
        /// Load the entire tutorial
        /// </summary>
        /// <param name="fileName">file name of the tutorial data</param>
        void Load(string fileName);
        /// <summary>
        /// Add a perform action request
        /// </summary>
        /// <param name="title">title of action</param>
        /// <param name="formName">name of the form</param>
        /// <param name="fieldName">field name</param>
        /// <param name="delay">delayed time</param>
        /// <param name="actionType">action type</param>
        void AddClick(string title, string formName, string fieldName, TimeSpan delay, string actionType, Dictionary<string, object> dict);
    }
}
