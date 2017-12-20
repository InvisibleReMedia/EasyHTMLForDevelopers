using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class GeneratedSculpture : ICloneable
    {
        #region Public Static Constants
        public static readonly string Page = "page";
        public static readonly string MasterPage = "masterPage";
        public static readonly string MasterObject = "masterObject";
        public static readonly string Tool = "tool";
        #endregion

        #region Private Fields
        private string generationType;
        private string name;
        private dynamic secondObject;
        private dynamic destinationObject;
        private List<RefObject> objects;
        private List<CadreModel> emptyObjects;
        #endregion

        #region Public Constructor
        public GeneratedSculpture(string name, string generationType)
        {
            this.name = name;
            this.generationType = generationType;
            this.objects = new List<RefObject>();
            this.emptyObjects = new List<CadreModel>();
        }

        private GeneratedSculpture(GeneratedSculpture gs)
        {
            this.generationType = ExtensionMethods.CloneThis(gs.generationType);
            this.name = ExtensionMethods.CloneThis(gs.name);
            this.secondObject = ExtensionMethods.CloneThis(gs.secondObject);
            this.destinationObject = ExtensionMethods.CloneThis(gs.destinationObject);
            this.objects = new List<RefObject>();
            this.emptyObjects = new List<CadreModel>();
            foreach (RefObject r in gs.objects)
            {
                this.objects.Add(r.Clone() as RefObject);
            }
            foreach (CadreModel cm in gs.emptyObjects)
            {
                this.emptyObjects.Add(cm.Clone() as CadreModel);
            }
        }
        #endregion

        #region Public Properties
        public string Type
        {
            get { return this.generationType; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public List<RefObject> Objects
        {
            get { return this.objects; }
        }

        public List<CadreModel> RemainerModels
        {
            get { return this.emptyObjects; }
        }

        public dynamic Destination
        {
            get { return this.destinationObject; }
            set { this.destinationObject = value; }
        }
        #endregion

        #region Private Methods
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
                    v.CSS.Padding = new Rectangle(mod.HorizontalPadding, mod.HorizontalPadding, mod.VerticalPadding, mod.VerticalPadding);
                    v.CSS.Border = new Rectangle(mod.HorizontalBorder, mod.HorizontalBorder, mod.VerticalBorder, mod.VerticalBorder);
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
        internal void GenerateContent(List<SizedRectangle> list, Granne g)
        {
            if (this.generationType == GeneratedSculpture.MasterPage)
            {
                MasterPage mp = this.destinationObject;
                list.Add(g.ConvertToDrawingRectangle());
            } else if (this.generationType == GeneratedSculpture.Page)
            {
                MasterPage mp = this.secondObject;
                list.Add(g.ConvertToDrawingRectangle());
            }
            else if (this.generationType == GeneratedSculpture.MasterObject)
            {
                MasterObject mo = this.destinationObject;
                list.Add(g.ConvertToDrawingRectangle());
            }
            else if (this.generationType == GeneratedSculpture.Tool)
            {
                MasterObject mo = this.secondObject;
                list.Add(g.ConvertToDrawingRectangle());
            }
        }

        internal void GenerateContent(List<SizedRectangle> list)
        {
            if (this.generationType == GeneratedSculpture.MasterPage)
            {
                MasterPage mp = this.destinationObject;
                mp.HorizontalZones.Clear();
                mp.MakeZones(list);
                this.FillContainerObject(mp.HorizontalZones, mp.Objects);
                SizeCompute.ComputeMasterPage(Project.CurrentProject, mp);
            }
            else if (this.generationType == GeneratedSculpture.Page)
            {
                Page p = this.destinationObject;
                MasterPage mp = this.secondObject;
                p.MasterPageName = mp.Name;
                mp.HorizontalZones.Clear();
                mp.MakeZones(list);
                this.FillContainerObject(mp.HorizontalZones, mp.Objects);
                SizeCompute.ComputePage(Project.CurrentProject, p);
            }
            else if (this.generationType == GeneratedSculpture.MasterObject)
            {
                MasterObject mo = this.destinationObject;
                mo.HorizontalZones.Clear();
                mo.MakeZones(list);
                this.FillContainerObject(mo.HorizontalZones, mo.Objects);
                SizeCompute.ComputeMasterObject(Project.CurrentProject, mo);
            }
            else if (this.generationType == GeneratedSculpture.Tool)
            {
                HTMLObject obj = this.destinationObject;
                MasterObject mo = this.secondObject;
                obj.MasterObjectName = mo.Name;
                mo.HorizontalZones.Clear();
                mo.MakeZones(list);
                this.FillContainerObject(mo.HorizontalZones, mo.Objects);
                SizeCompute.ComputeHTMLObject(Project.CurrentProject, obj);
            }
        }

        public void CreateDestination(Library.Project proj, uint width, uint height, uint h, uint v)
        {
            if (this.destinationObject == null)
            {
                if (this.generationType == GeneratedSculpture.MasterPage)
                {
                    MasterPage mp = new MasterPage();
                    mp.Name = this.name;
                    mp.ConstraintHeight = EnumConstraint.FIXED;
                    mp.Height = height;
                    mp.ConstraintWidth = EnumConstraint.FIXED;
                    mp.Width = width;
                    mp.CountColumns = h;
                    mp.CountLines = v;
                    proj.MasterPages.Add(mp);
                    this.destinationObject = mp;
                }
                else if (this.generationType == GeneratedSculpture.Page)
                {
                    MasterPage mp = new Library.MasterPage();
                    Page p = new Library.Page();
                    if (Library.Project.AddPage(proj, p, this.name))
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
                        this.secondObject = mp;
                        this.destinationObject = p;
                    }
                    else
                    {
                        throw new ArgumentException(Localization.Strings.GetString("ExceptionPageNotCreated"));
                    }
                }
                else if (this.generationType == GeneratedSculpture.MasterObject)
                {
                    MasterObject mo = new MasterObject();
                    mo.Title = this.name;
                    mo.ConstraintHeight = EnumConstraint.FIXED;
                    mo.Height = height;
                    mo.ConstraintWidth = EnumConstraint.FIXED;
                    mo.Width = width;
                    mo.CountColumns = h;
                    mo.CountLines = v;
                    proj.MasterObjects.Add(mo);
                    this.destinationObject = mo;
                }
                else if (this.generationType == GeneratedSculpture.Tool)
                {
                    MasterObject mo = new Library.MasterObject();
                    HTMLTool t = new HTMLTool();
                    if (Library.Project.AddTool(proj, t, this.name))
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
                        this.secondObject = mo;
                        this.destinationObject = t;
                    }
                    else
                    {
                        throw new ArgumentException(Localization.Strings.GetString("ExceptionToolNotCreated"));
                    }
                }
            }
            else
            {
                if (this.generationType == GeneratedSculpture.MasterPage)
                {
                    MasterPage mp = this.destinationObject;
                    mp.Objects.Clear();
                    mp.ConstraintHeight = EnumConstraint.FIXED;
                    mp.Height = height;
                    mp.ConstraintWidth = EnumConstraint.FIXED;
                    mp.Width = width;
                    mp.CountColumns = h;
                    mp.CountLines = v;
                }
                else if (this.generationType == GeneratedSculpture.Page)
                {
                    Page p = this.destinationObject;
                    MasterPage mp = this.secondObject;
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
                else if (this.generationType == GeneratedSculpture.MasterObject)
                {
                    MasterObject mo = this.destinationObject;
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
                else if (this.generationType == GeneratedSculpture.Tool)
                {
                    HTMLTool t = this.destinationObject;
                    MasterObject mo = this.secondObject;
                    mo.Objects.ForEach(a =>
                    {
                        if (Project.CurrentProject.Instances.Contains(a))
                            Project.CurrentProject.Instances.Remove(a);
                    });
                    mo.Objects.Clear();
                    Project.CurrentProject.Tools.Find(t.Path).Tools.Remove(t);
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
        #endregion

        public object Clone()
        {
            GeneratedSculpture newObject = new GeneratedSculpture(this);
            return newObject;
        }
    }
}
