using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Generated sculpture to store into a project
    /// </summary>
    [Serializable]
    public class GeneratedSculpture : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for the unique id
        /// </summary>
        protected static readonly string uniqueName = "unique";
        /// <summary>
        /// Index name for generation type
        /// </summary>
        protected static readonly string generationTypeName = "generationType";
        /// <summary>
        /// Index name for generation type
        /// </summary>
        protected static readonly string nameName = "name";
        /// <summary>
        /// Index name for second object
        /// </summary>
        protected static readonly string secondObjectName = "secondObject";
        /// <summary>
        /// Index name for destination object
        /// </summary>
        protected static readonly string destinationObjectName = "destinationObject";
        /// <summary>
        /// Index name for referenced objects
        /// </summary>
        protected static readonly string refObjectListName = "refObjects";
        /// <summary>
        /// Index name for emptyied objects
        /// </summary>
        protected static readonly string emptyObjectListName = "emptyObjects";

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">name of this generating</param>
        /// <param name="generationType">type of generating</param>
        public GeneratedSculpture(string name, string generationType)
        {
            this.Set(nameName, name);
            this.Set(generationTypeName, generationType);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="gs">generated sculpture to copy</param>
        private GeneratedSculpture(GeneratedSculpture gs)
        {
            this.Type = ExtensionMethods.CloneThis(gs.Type);
            this.Name = ExtensionMethods.CloneThis(gs.Name);
            this.SecondObject = ExtensionMethods.CloneThis(gs.SecondObject);
            this.Destination = ExtensionMethods.CloneThis(gs.Destination);
            foreach (RefObject r in gs.Objects)
            {
                this.Objects.Add(r.Clone() as RefObject);
            }
            foreach (CadreModel cm in gs.RemainerModels)
            {
                this.RemainerModels.Add(cm.Clone() as CadreModel);
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique id
        /// </summary>
        public string Unique
        {
            get { return this.Get(uniqueName); }
            set { this.Set(uniqueName, value); }
        }

        /// <summary>
        /// Gets or sets the generation type
        /// </summary>
        public string Type
        {
            get { return this.Get(generationTypeName, ""); }
            set { this.Set(generationTypeName, value); }
        }

        /// <summary>
        /// Gets or sets a name
        /// </summary>
        public string Name
        {
            get { return this.Get(nameName, ""); }
            set { this.Set(nameName, value); }
        }

        /// <summary>
        /// Second object
        /// </summary>
        protected dynamic SecondObject
        {
            get
            {
                return this.Get(secondObjectName);
            }
            set
            {
                this.Set(secondObjectName, value);
            }
        }

        /// <summary>
        /// Gets referenced objects
        /// </summary>
        public List<RefObject> Objects
        {
            get { return this.Get(refObjectListName, new List<RefObject>()); }
        }

        /// <summary>
        /// Gets remain models
        /// </summary>
        public List<CadreModel> RemainerModels
        {
            get { return this.Get(emptyObjectListName, new List<CadreModel>()); }
        }

        /// <summary>
        /// Gets or sets the destination object
        /// </summary>
        public dynamic Destination
        {
            get { return this.Get(destinationObjectName, ""); }
            set { this.Set(destinationObjectName, value); }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fill container object
        /// </summary>
        /// <param name="list">list of horizontal zone to consider</param>
        /// <param name="outputList">list of HTMLObject obtained</param>
        private void FillContainerObject(List<HorizontalZone> list, List<HTMLObject> outputList)
        {
            int indexObject = 0;
            int indexOther = 0;
            foreach (HorizontalZone h in list)
            {
                foreach (VerticalZone v in h.VerticalZones)
                {
                    CadreModel mod = this.RemainerModels[indexOther++];
                    v.CSS.BackgroundColor = new CSSColor(mod.Background);
                    v.CSS.BorderTopColor = v.CSS.BorderRightColor = v.CSS.BorderLeftColor = v.CSS.BorderBottomColor = new CSSColor(mod.Border);
                    v.CSS.ForegroundColor = new CSSColor(mod.Foreground);
                    v.CSS.Padding = new Rectangle(mod.WidthPadding, mod.WidthPadding, mod.HeightPadding, mod.HeightPadding);
                    v.CSS.Border = new Rectangle(mod.WidthBorder, mod.WidthBorder, mod.HeightBorder, mod.HeightBorder);
                    if (this.Objects[indexObject] != null)
                    {
                        this.Objects[indexObject].DirectObject.Container = v.Name;
                        outputList.Add(this.Objects[indexObject].DirectObject);
                    }
                    ++indexObject;
                }
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Generate content
        /// </summary>
        /// <param name="list">list of sized rectangle</param>
        /// <param name="g">granne to generate</param>
        internal void GenerateContent(List<SizedRectangle> list, Granne g)
        {
            if (this.Type == RefObject.MasterPage)
            {
                MasterPage mp = this.Destination;
                list.Add(g.ConvertToDrawingRectangle());
            }
            else if (this.Type == RefObject.Page)
            {
                MasterPage mp = this.SecondObject;
                list.Add(g.ConvertToDrawingRectangle());
            }
            else if (this.Type == RefObject.MasterObject)
            {
                MasterObject mo = this.Destination;
                list.Add(g.ConvertToDrawingRectangle());
            }
            else if (this.Type == RefObject.Tool)
            {
                MasterObject mo = this.SecondObject;
                list.Add(g.ConvertToDrawingRectangle());
            }
        }

        /// <summary>
        /// Generate the content
        /// </summary>
        /// <param name="list">list of sized rectangle</param>
        internal void GenerateContent(List<SizedRectangle> list)
        {
            if (this.Type == RefObject.MasterPage)
            {
                MasterPage mp = this.Destination;
                mp.HorizontalZones.Clear();
                mp.MakeZones(list);
                this.FillContainerObject(mp.HorizontalZones, mp.Objects);
                SizeCompute.ComputeMasterPage(Project.CurrentProject, mp);
            }
            else if (this.Type == RefObject.Page)
            {
                Page p = this.Destination;
                MasterPage mp = this.SecondObject;
                p.MasterPageName = mp.Name;
                mp.HorizontalZones.Clear();
                mp.MakeZones(list);
                this.FillContainerObject(mp.HorizontalZones, mp.Objects);
                SizeCompute.ComputePage(Project.CurrentProject, p);
            }
            else if (this.Type == RefObject.MasterObject)
            {
                MasterObject mo = this.Destination;
                mo.HorizontalZones.Clear();
                mo.MakeZones(list);
                this.FillContainerObject(mo.HorizontalZones, mo.Objects);
                SizeCompute.ComputeMasterObject(Project.CurrentProject, mo);
            }
            else if (this.Type == RefObject.Tool)
            {
                HTMLObject obj = this.Destination;
                MasterObject mo = this.SecondObject;
                mo.HorizontalZones.Clear();
                mo.MakeZones(list);
                this.FillContainerObject(mo.HorizontalZones, mo.Objects);
                SizeCompute.ComputeHTMLObject(Project.CurrentProject, obj);
            }
        }

        /// <summary>
        /// Create a destination object and store its into the project
        /// </summary>
        /// <param name="proj">project</param>
        /// <param name="width">width value</param>
        /// <param name="height">height value</param>
        /// <param name="h">horizontal count</param>
        /// <param name="v">vertical count</param>
        public void CreateDestination(Library.Project proj, uint width, uint height, uint h, uint v)
        {
            if (this.Destination == null)
            {
                if (this.Type == RefObject.MasterPage)
                {
                    MasterPage mp = new MasterPage();
                    mp.Name = this.Name;
                    mp.ConstraintHeight = EnumConstraint.FIXED;
                    mp.Height = height;
                    mp.ConstraintWidth = EnumConstraint.FIXED;
                    mp.Width = width;
                    mp.CountColumns = h;
                    mp.CountLines = v;
                    proj.MasterPages.Add(mp);
                    this.Destination = mp;
                }
                else if (this.Type == RefObject.Page)
                {
                    MasterPage mp = new Library.MasterPage();
                    Page p = new Library.Page();
                    if (Library.Project.AddPage(proj, p, this.Name))
                    {
                        mp.Name = "MasterPage_" + System.IO.Path.GetFileNameWithoutExtension(p.Name);
                        mp.ConstraintHeight = EnumConstraint.FIXED;
                        mp.Height = height;
                        mp.ConstraintWidth = EnumConstraint.FIXED;
                        mp.Width = width;
                        mp.CountColumns = h;
                        mp.CountLines = v;
                        proj.MasterPages.Add(mp);
                        p.MasterPageName = mp.Name;
                        p.ConstraintHeight = EnumConstraint.FIXED;
                        p.Height = height;
                        p.ConstraintWidth = EnumConstraint.FIXED;
                        p.Width = width;
                        this.SecondObject = mp;
                        this.Destination = p;
                    }
                    else
                    {
                        throw new ArgumentException(Localization.Strings.GetString("ExceptionPageNotCreated"));
                    }
                }
                else if (this.Type == RefObject.MasterObject)
                {
                    MasterObject mo = new MasterObject();
                    mo.Title = this.Name;
                    mo.ConstraintHeight = EnumConstraint.FIXED;
                    mo.Height = height;
                    mo.ConstraintWidth = EnumConstraint.FIXED;
                    mo.Width = width;
                    mo.CountColumns = h;
                    mo.CountLines = v;
                    proj.MasterObjects.Add(mo);
                    this.Destination = mo;
                }
                else if (this.Type == RefObject.Tool)
                {
                    MasterObject mo = new Library.MasterObject();
                    HTMLTool t = new HTMLTool();
                    if (Library.Project.AddTool(proj, t, this.Name))
                    {
                        mo.Name = "MasterObject_" + t.Title;
                        mo.ConstraintHeight = EnumConstraint.FIXED;
                        mo.Height = height;
                        mo.ConstraintWidth = EnumConstraint.FIXED;
                        mo.Width = width;
                        mo.CountColumns = h;
                        mo.CountLines = v;

                        t.ConstraintHeight = EnumConstraint.FIXED;
                        t.ConstraintWidth = EnumConstraint.FIXED;
                        t.Width = width;
                        t.Height = height;
                        this.SecondObject = mo;
                        this.Destination = t;
                    }
                    else
                    {
                        throw new ArgumentException(Localization.Strings.GetString("ExceptionToolNotCreated"));
                    }
                }
            }
            else
            {
                if (this.Type == RefObject.MasterPage)
                {
                    MasterPage mp = this.Destination;
                    mp.Objects.Clear();
                    mp.ConstraintHeight = EnumConstraint.FIXED;
                    mp.Height = height;
                    mp.ConstraintWidth = EnumConstraint.FIXED;
                    mp.Width = width;
                    mp.CountColumns = h;
                    mp.CountLines = v;
                }
                else if (this.Type == RefObject.Page)
                {
                    Page p = this.Destination;
                    MasterPage mp = this.SecondObject;
                    mp.Objects.ForEach(a =>
                    {
                        if (a.IsMasterObject)
                        {
                            Project.CurrentProject.MasterObjects.ForEach(z =>
                            {
                                if (z.Name == a.MasterObjectName)
                                {
                                    z.Objects.ForEach(k =>
                                    {
                                        if (Project.CurrentProject.Instances.Contains(k))
                                            Project.CurrentProject.Instances.Remove(k);
                                    });
                                    z.Objects.Clear();
                                }
                            });
                        }
                        if (Project.CurrentProject.Instances.Contains(a))
                            Project.CurrentProject.Instances.Remove(a);
                    });
                    mp.Objects.Clear();
                    p.Objects.ForEach(a =>
                    {
                        if (a.IsMasterObject)
                        {
                            Project.CurrentProject.MasterObjects.ForEach(z =>
                            {
                                if (z.Name == a.MasterObjectName)
                                {
                                    z.Objects.ForEach(k =>
                                    {
                                        if (Project.CurrentProject.Instances.Contains(k))
                                            Project.CurrentProject.Instances.Remove(k);
                                    });
                                    z.Objects.Clear();
                                }
                            });
                        }
                        if (Project.CurrentProject.Instances.Contains(a))
                            Project.CurrentProject.Instances.Remove(a);
                    });
                    p.Objects.Clear();
                    mp.ConstraintHeight = EnumConstraint.FIXED;
                    mp.Height = height;
                    mp.ConstraintWidth = EnumConstraint.FIXED;
                    mp.Width = width;
                    mp.CountColumns = h;
                    mp.CountLines = v;
                    p.ConstraintHeight = EnumConstraint.FIXED;
                    p.Height = height;
                    p.ConstraintWidth = EnumConstraint.FIXED;
                    p.Width = width;
                }
                else if (this.Type == RefObject.MasterObject)
                {
                    MasterObject mo = this.Destination;
                    mo.Objects.ForEach(a => {
                        if (Project.CurrentProject.Instances.Contains(a))
                            Project.CurrentProject.Instances.Remove(a);
                    });
                    mo.Objects.Clear();
                    mo.ConstraintHeight = EnumConstraint.FIXED;
                    mo.Height = height;
                    mo.ConstraintWidth = EnumConstraint.FIXED;
                    mo.Width = width;
                    mo.CountColumns = h;
                    mo.CountLines = v;
                }
                else if (this.Type == RefObject.Tool)
                {
                    HTMLTool t = this.Destination;
                    MasterObject mo = this.SecondObject;
                    mo.Objects.ForEach(a =>
                    {
                        if (Project.CurrentProject.Instances.Contains(a))
                            Project.CurrentProject.Instances.Remove(a);
                    });
                    mo.Objects.Clear();
                    Project.CurrentProject.Tools.Remove(t);
                    mo.ConstraintHeight = EnumConstraint.FIXED;
                    mo.Height = height;
                    mo.ConstraintWidth = EnumConstraint.FIXED;
                    mo.Width = width;
                    mo.CountColumns = h;
                    mo.CountLines = v;

                    t.ConstraintHeight = EnumConstraint.FIXED;
                    t.ConstraintWidth = EnumConstraint.FIXED;
                    t.Width = width;
                    t.Height = height;
                }
            }
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            GeneratedSculpture newObject = new GeneratedSculpture(this);
            return newObject;
        }

        #endregion

    }
}
