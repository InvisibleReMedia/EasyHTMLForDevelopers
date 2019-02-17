using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UXFramework.BeamConnections;

namespace UXFramework
{
    /// <summary>
    /// Image clickable
    /// </summary>
    public class UXClickableImage : UXControl
    {
        #region Fields

        /// <summary>
        /// File of image
        /// </summary>
        public static readonly string imageName = "image";
        /// <summary>
        /// Id
        /// </summary>
        public static readonly string idName = "id";
        /// <summary>
        /// When roll over
        /// </summary>
        public static readonly string rollOverName = "rollover";
        /// <summary>
        /// When click
        /// </summary>
        public static readonly string clickName = "click";
        /// <summary>
        /// roll over button
        /// </summary>
        public static readonly string rollBackColorName = "rollbackColor";
        /// <summary>
        /// Roll color
        /// </summary>
        public static readonly string rollColorName = "rollColor";
        /// <summary>
        /// Click color
        /// </summary>
        public static readonly string clickBorderColorName = "clickColor";


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fileName">image file</param>
        public UXClickableImage(string id, string fileName)
        {
            this.Set(idName, id);
            this.Set(imageName, fileName);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image file
        /// </summary>
        public string ImageFile
        {
            get { return this.Get(imageName); }
            set { this.Set(imageName, value); }
        }

        /// <summary>
        /// Gets or sets the Id object
        /// </summary>
        public string Id
        {
            get { return this.Get(idName); }
            set { this.Set(idName, value); }
        }

        /// <summary>
        /// Gets or sets the roll back color
        /// </summary>
        public string RollBackColor
        {
            get { return this.Get(rollBackColorName); }
            set { this.Set(rollBackColorName, value); }
        }

        /// <summary>
        /// Gets or sets the roll color
        /// </summary>
        public string RollColor
        {
            get { return this.Get(rollColorName); }
            set { this.Set(rollColorName, value); }
        }

        /// <summary>
        /// Gets or sets the click border color
        /// </summary>
        public string ClickBorderColor
        {
            get { return this.Get(clickBorderColorName); }
            set { this.Set(clickBorderColorName, value); }
        }

        /// <summary>
        /// Gets or sets the roll image file
        /// </summary>
        public string RollImageFile
        {
            get { return this.Get(rollOverName); }
            set { this.Set(rollOverName, value); }
        }

        /// <summary>
        /// Gets or sets the click image file
        /// </summary>
        public string ClickImageFile
        {
            get { return this.Get(clickName); }
            set { this.Set(clickName, value); }
        }

        #endregion

        #region Methods

        public override void Construct(Marshalling.IMarshalling m, Marshalling.IMarshalling ui)
        {
            Marshalling.MarshallingHash hash = ui as Marshalling.MarshallingHash;
            ImageProperties ip = hash["properties"].Value;
            // enregistrement des elements
            this.Beams.SetPropertyValues(new List<KeyValuePair<string, Beam>> {
                new KeyValuePair<string, Beam>("properties", Beam.Register("properties", this, ip))
            }.ToArray());
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create clickable image
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXClickableImage CreateUXClickableImage(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXClickableImage ux = new UXClickableImage(data["Id"].Value, ui["ImageFile"].Value);
            ux.Construct(data, ui);
            return ux;
        }

        #endregion
    }
}
