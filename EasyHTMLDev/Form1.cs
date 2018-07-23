using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class Form1 : Form
    {
        private TreeNode currentNodeContext;
        private int elapseTimer;
        private int localeComponentId;
        private int currentLocale;
        private TutorialExec tutoExec;

        public Form1()
        {
            InitializeComponent();
            this.currentLocale = 1;
            this.InitLanguage();
            this.RegisterControls(ref this.localeComponentId);
        }

        private string StandardTitle(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return this.Translate("NoTitle");
            }
            else
            {
                return s;
            }
        }

        private void BrowseFolder(string curPath, TreeNode node, object selectedTag, TreeNode root)
        {
        }

        private void BrowseTool(Library.HTMLTool f, TreeNode node, object selectedTag, TreeNode root)
        {
        }

        private string Translate(string key)
        {
            return Localization.Strings.GetString(key);
        }

        private void tmBlind_Tick(object sender, EventArgs e)
        {
            ++elapseTimer;
            if (this.elapseTimer % 2 == 0)
            {
                this.errorZone.BackColor = Color.Red;
                this.errorZone.ForeColor = Color.White;
            }
            else
            {
                this.errorZone.BackColor = Color.FromName(KnownColor.Control.ToString());
                this.errorZone.ForeColor = Color.Red;
            }
        }

        private void OpenProject()
        {
            object selectedTag = null;
            if (this.currentNodeContext != null)
                selectedTag = this.currentNodeContext.Tag;
            this.treeView1.Nodes.Clear();
            Project proj = Project.CurrentProject;
            if (proj != null)
            {
                if (proj.NotSaved)
                {
                    this.errorZone.Visible = true;
                    this.elapseTimer = 0;
                    this.tmBlind.Start();
                }
                else
                {
                    this.errorZone.Visible = false;
                    this.tmBlind.Stop();
                    this.tmBlind.Enabled = false;
                }
                TreeNode racine = this.treeView1.Nodes.Add(proj.Title);
                TreeNode config = racine.Nodes.Add(Translate("Configuration"));
                config.Tag = proj.Configuration;
                TreeNode urls = racine.Nodes.Add(Translate("URLJavascript"));
                urls.Tag = proj.JavascriptUrls;
                TreeNode masterPages = racine.Nodes.Add(Translate("MasterPage"));
                TreeNode masterObjects = racine.Nodes.Add(Translate("MasterObject"));
                TreeNode tools = racine.Nodes.Add(Translate("Tool"));
                TreeNode sculptures = racine.Nodes.Add(Translate("SculptureForm"));
                TreeNode instances = racine.Nodes.Add(Translate("ToolInstance"));
                TreeNode folders = racine.Nodes.Add(Translate("Folder"));
                racine.Expand();

                foreach (string s in proj.Configuration.Elements.AllKeys)
                {
                    config.Nodes.Add(this.StandardTitle(s));
                }

                foreach (string s in proj.JavascriptUrls)
                {
                    urls.Nodes.Add(this.StandardTitle(s));
                }

                var it = proj.Hierarchy.Find(Library.Project.MasterPagesName).GetNodesEnumerator();
                while(it.MoveNext())
                {
                    TreeNode masterPageNode = masterPages.Nodes.Add(this.StandardTitle(it.Current.Get("name")));
                    masterPageNode.Tag = it.Current;
                    if (selectedTag is MasterPage && selectedTag.Equals(it.Current))
                    {
                        this.treeView1.SelectedNode = masterPageNode;
                        masterPages.Expand();
                    }
                }

                foreach (MasterPage mp in proj.MasterPages)
                {
                    TreeNode masterPageNode = masterPages.Nodes.Add(this.StandardTitle(mp.Name));
                    masterPageNode.Tag = mp;
                    if (selectedTag is MasterPage && selectedTag.Equals(mp))
                    {
                        this.treeView1.SelectedNode = masterPageNode;
                        masterPages.Expand();
                    }
                    foreach (HTMLObject obj in mp.Objects)
                    {
                        TreeNode node = masterPageNode.Nodes.Add(this.StandardTitle(obj.Title));
                        node.Tag = obj;
                    }
                }

                foreach (MasterObject mo in proj.MasterObjects)
                {
                    TreeNode masterObjectNode = masterObjects.Nodes.Add(this.StandardTitle(mo.Title));
                    masterObjectNode.Tag = mo;
                    if (selectedTag is MasterObject && selectedTag.Equals(mo))
                    {
                        this.treeView1.SelectedNode = masterObjectNode;
                        masterObjects.Expand();
                    }
                    foreach (HTMLObject obj in mo.Objects)
                    {
                        TreeNode node = masterObjectNode.Nodes.Add(this.StandardTitle(obj.Title));
                        node.Tag = obj;
                    }
                }

                foreach (SculptureObject s in proj.SculptureObjects)
                {
                    TreeNode sculptureObjectNode = sculptures.Nodes.Add(this.StandardTitle(s.Title));
                    sculptureObjectNode.Tag = s;
                    if (selectedTag is SculptureObject && selectedTag.Equals(s))
                    {
                        this.treeView1.SelectedNode = sculptureObjectNode;
                        sculptures.Expand();
                    }
                }

                foreach (HTMLObject instance in proj.Instances)
                {
                    TreeNode instanceNode = instances.Nodes.Add(this.StandardTitle(instance.Title));
                    instanceNode.Tag = instance;
                    if (selectedTag is HTMLObject && selectedTag.Equals(instance))
                    {
                        this.treeView1.SelectedNode = instanceNode;
                        instances.Expand();
                    }
                }

                this.sculpterMenu.Enabled = true;
                this.masterPagesToolStripMenuItem.Enabled = true;
                this.masterObjectsToolStripMenuItem.Enabled = true;
                this.pagesToolStripMenuItem.Enabled = true;
                this.dossiers.Enabled = true;
                this.outils.Enabled = true;
                this.importer.Enabled = true;
                this.renommer.Enabled = true;
                this.fermerToolStripMenuItem.Enabled = true;
                this.generer.Enabled = true;
            }
            else
            {
                this.sculpterMenu.Enabled = false;
                this.masterPagesToolStripMenuItem.Enabled = false;
                this.masterObjectsToolStripMenuItem.Enabled = false;
                this.pagesToolStripMenuItem.Enabled = false;
                this.dossiers.Enabled = false;
                this.outils.Enabled = false;
                this.importer.Enabled = false;
                this.renommer.Enabled = false;
                this.fermerToolStripMenuItem.Enabled = false;
                this.generer.Enabled = false;
                this.errorZone.Visible = false;
            }
        }

        private void créerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open op = new Open(ConfigDirectories.GetDocumentsFolder());
            op.btnNew_Click(this, new EventArgs());
            AppDomain.CurrentDomain.SetData("fileName", op.FileName);
            this.treeView1.SelectedNode = null;
            Project.Load(ConfigDirectories.GetDocumentsFolder(), op.FileName, new Project.OpenProject(OpenProject));
            this.Text = String.Format(this.Translate("SoftwareTitleOnProject"), Project.CurrentProject.Title);
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open op = new Open(ConfigDirectories.GetDocumentsFolder());
            DialogResult dr = op.ShowDialog();
            if (dr == DialogResult.OK)
            {
                AppDomain.CurrentDomain.SetData("fileName", op.FileName);
                Project.Load(ConfigDirectories.GetDocumentsFolder(), op.FileName, new Project.OpenProject(OpenProject));
                this.Text = String.Format(this.Translate("SoftwareTitleOnProject"), Project.CurrentProject.Title);
            }
        }

        private void fermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                DialogResult dr = MessageBox.Show(Localization.Strings.GetString("CarefullNotSavedText"), Localization.Strings.GetString("CarefullNotSavedTitle"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }
            // fermer toutes les fenêtres
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            this.treeView1.Nodes.Clear();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                DialogResult dr = MessageBox.Show(Localization.Strings.GetString("CarefullNotSavedText"), Localization.Strings.GetString("CarefullNotSavedTitle"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }
            // fermer toutes les fenêtres
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            this.treeView1.Nodes.Clear();
            this.Close();
        }

        private void créerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MasterPageCreationForm form = new MasterPageCreationForm();
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Project proj = Project.CurrentProject;
                MasterPageCreationWindow window = new MasterPageCreationWindow(form.MasterPage);
                if (form.MasterPage.ConstraintWidth != EnumConstraint.RELATIVE && form.MasterPage.ConstraintWidth != EnumConstraint.AUTO)
                    window.Height = (int)form.MasterPage.Height;
                else
                    window.Height = 500;
                if (form.MasterPage.ConstraintHeight != EnumConstraint.RELATIVE && form.MasterPage.ConstraintHeight != EnumConstraint.AUTO)
                    window.Width = (int)form.MasterPage.Width;
                else
                    window.Width = 500;
                window.WindowState = FormWindowState.Normal;
                DialogResult dr2 = window.ShowDialog();
                if (dr2 == DialogResult.OK)
                {
                    Library.SizeCompute.ComputeMasterPage(proj, form.MasterPage);
                    proj.MasterPages.Add(form.MasterPage);
                    Project.Save(proj, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                    Project.CurrentProject.ReloadProject();
                }
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode currentSelection = this.treeView1.GetNodeAt(this.treeView1.PointToClient(MousePosition));
            if (currentSelection != null) this.treeView1.SelectedNode = currentSelection;
            if (this.treeView1.SelectedNode != null)
            {
                if (this.treeView1.SelectedNode.Tag != null)
                {
                    // sauvegarde selected node
                    this.currentNodeContext = this.treeView1.SelectedNode;
                    if (this.treeView1.SelectedNode.Tag is Library.MasterPage)
                    {
                        Library.MasterPage mp = this.treeView1.SelectedNode.Tag as Library.MasterPage;
                        MasterPageView window = new MasterPageView(mp.Name);
                        window.Text = String.Format(this.Translate("MasterPageTitle"), mp.Name);
                        window.FormClosed += window_FormClosed;
                        window.MdiParent = this;
                        window.MasterPage = mp;
                        window.WindowState = FormWindowState.Normal;
                        window.StartPosition = FormStartPosition.Manual;
                        window.Top = 0;
                        window.Left = 0;
                        window.Show();
                    }
                    else if (this.treeView1.SelectedNode.Tag is Library.MasterObject)
                    {
                        Library.MasterObject mo = this.treeView1.SelectedNode.Tag as Library.MasterObject;
                        MasterObjectView window = new MasterObjectView(mo.Title);
                        window.Text = String.Format(this.Translate("MasterObjectTitle"), mo.Title);
                        window.FormClosed += window_FormClosed;
                        window.MdiParent = this;
                        window.MasterObject = mo;
                        window.WindowState = FormWindowState.Normal;
                        window.StartPosition = FormStartPosition.Manual;
                        window.Top = 0;
                        window.Left = 0;
                        window.Show();
                    }
                    else if (this.treeView1.SelectedNode.Tag is Library.SculptureObject)
                    {
                        Library.SculptureObject so = this.treeView1.SelectedNode.Tag as Library.SculptureObject;
                        SculptureView window = new SculptureView();
                        window.Text = String.Format(this.Translate("SculptureTitle"), so.Title);
                        window.FormClosed += window_FormClosed;
                        window.MdiParent = this;
                        window.SculptureObject = so;
                        if (window.SculptureObject.Cadres.Count == 0)
                        {
                            window.SculptureObject.Cadres.Add(new CadreModel());
                        }
                        window.CurrentCadreModel.CurrentObject = window.SculptureObject.Cadres[0];
                        window.WindowState = FormWindowState.Normal;
                        window.StartPosition = FormStartPosition.Manual;
                        window.Top = 0;
                        window.Left = 0;
                        window.Show();
                    }
                    else if (this.treeView1.SelectedNode.Tag is Library.HTMLTool)
                    {
                        Library.HTMLTool t = this.treeView1.SelectedNode.Tag as Library.HTMLTool;
                        ToolView view = new ToolView(t.Title);
                        view.Text = String.Format(this.Translate("ToolTitle"), t.Title);
                        view.FormClosed += window_FormClosed;
                        view.MdiParent = this;
                        view.HTMLTool = t;
                        view.WindowState = FormWindowState.Normal;
                        view.StartPosition = FormStartPosition.Manual;
                        view.Top = 0;
                        view.Left = 0;
                        view.Show();
                    }
                    else if (this.treeView1.SelectedNode.Tag is Library.HTMLObject)
                    {
                        HTMLObject obj = this.treeView1.SelectedNode.Tag as Library.HTMLObject;
                        if (!String.IsNullOrEmpty(obj.MasterObjectName))
                        {
                            ObjectView view = new ObjectView(obj.Title);
                            view.Text = String.Format(this.Translate("ObjectTitle"), obj.Title);
                            view.FormClosed += window_FormClosed;
                            view.MdiParent = this;
                            view.HTMLObject = obj;
                            view.WindowState = FormWindowState.Normal;
                            view.StartPosition = FormStartPosition.Manual;
                            view.Top = 0;
                            view.Left = 0;
                            view.Show();
                        }
                        else
                        {
                            SimpleObjectView view = new SimpleObjectView(obj.Title);
                            view.Text = String.Format(this.Translate("ObjectTitle"), obj.Title);
                            view.FormClosed += window_FormClosed;
                            view.MdiParent = this;
                            view.HTMLObject = obj;
                            view.WindowState = FormWindowState.Normal;
                            view.StartPosition = FormStartPosition.Manual;
                            view.Top = 0;
                            view.Left = 0;
                            view.Show();
                        }
                    }
                    else if (this.treeView1.SelectedNode.Tag is Library.Page)
                    {
                        Page p = this.treeView1.SelectedNode.Tag as Library.Page;
                        PageView view = new PageView(p.Name);
                        view.Text = String.Format(this.Translate("PageTitle"), p.Name);
                        view.FormClosed += window_FormClosed;
                        view.MdiParent = this;
                        view.Page = p;
                        view.WindowState = FormWindowState.Normal;
                        view.StartPosition = FormStartPosition.Manual;
                        view.Top = 0;
                        view.Left = 0;
                        view.Show();
                    }
                    else if (this.treeView1.SelectedNode.Tag is Library.Configuration)
                    {
                        ConfigView cv = new ConfigView();
                        cv.FormClosed += window_FormClosed;
                        cv.Datas = (this.treeView1.SelectedNode.Tag as Library.Configuration).Elements;
                        cv.ShowDialog();
                    }
                    else if (this.treeView1.SelectedNode.Tag is List<string>)
                    {
                        JavaScriptUrls ju = new JavaScriptUrls();
                        ju.FormClosed += window_FormClosed;
                        ju.Datas = (this.treeView1.SelectedNode.Tag as List<string>);
                        ju.ShowDialog();
                    }
                }
            }
        }

        void window_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            //if (this.elapseTimer == 1)
            //{
            //    this.treeView1.SelectedNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(MousePosition));
            //    this.treeView1_DoubleClick(sender, e);
            //}
        }

        private void tdd_Tick(object sender, EventArgs e)
        {
            if (elapseTimer == 0)
            {
                this.treeView1.Focus();
                elapseTimer = 1;
                this.tdd.Interval = 20;
                this.tdd.Start();
            }
            else if (elapseTimer == 1)
            {
                this.treeView1.SelectedNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(MousePosition));
                this.currentNodeContext = this.treeView1.SelectedNode;
                elapseTimer = 2;
                this.tdd.Interval = 20;
                this.tdd.Start();
            }
            else if (elapseTimer == 2)
            {
                this.tdd.Stop();
                this.tdd.Enabled = false;
                this.elapseTimer = 0;
                if (this.treeView1.SelectedNode != null)
                    this.treeView1.DoDragDrop(this.treeView1.SelectedNode, DragDropEffects.Link);
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            this.treeView1_DoubleClick(sender, e);
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void créerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MasterObjectCreationForm creation = new MasterObjectCreationForm();
            DialogResult dr = creation.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Project proj = Project.CurrentProject;
                MasterObjectCreationWindow window = new MasterObjectCreationWindow(creation.MasterObject);
                if (creation.MasterObject.ConstraintWidth != EnumConstraint.RELATIVE && creation.MasterObject.ConstraintWidth != EnumConstraint.AUTO)
                    window.Height = (int)creation.MasterObject.Height;
                else
                    window.Height = 500;
                if (creation.MasterObject.ConstraintHeight != EnumConstraint.RELATIVE && creation.MasterObject.ConstraintHeight != EnumConstraint.AUTO)
                    window.Width = (int)creation.MasterObject.Width;
                else
                    window.Width = 500;
                window.WindowState = FormWindowState.Normal;
                DialogResult dr2 = window.ShowDialog();
                if (dr2 == DialogResult.OK)
                {
                    Library.SizeCompute.ComputeMasterObject(proj, creation.MasterObject);
                    proj.MasterObjects.Add(creation.MasterObject);
                    Project.Save(proj, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                    Project.CurrentProject.ReloadProject();
                }
            }
        }

        private void créerToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ToolCreate tc = new ToolCreate();
            DialogResult dr = tc.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Project.CurrentProject.ReloadProject();
            }
        }

        private void créerToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            FolderCreate tc = new FolderCreate();
            DialogResult dr = tc.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Project.CurrentProject.ReloadProject();
            }
        }

        private void créerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PageCreate pc = new PageCreate();
            pc.MasterPages = Project.CurrentProject.MasterPages;
            DialogResult dr = pc.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                Library.SizeCompute.ComputePage(Project.CurrentProject, pc.Page);
                Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Project.CurrentProject.ReloadProject();
            }
        }

        private void ImportTool(Library.HTMLTool f, TreeNode node)
        {
        }

        private void SelectTool(TreeNode node, Library.HTMLTool import, bool all)
        {
        }

        private void lesMasterPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Open op = new Open(ConfigDirectories.GetDocumentsFolder());
                DialogResult drOp = op.ShowDialog();
                if (drOp == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lesOutilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Open op = new Open(ConfigDirectories.GetDocumentsFolder());
                DialogResult drOp = op.ShowDialog();
                if (drOp == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lesMasterObjetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Open op = new Open(ConfigDirectories.GetDocumentsFolder());
                DialogResult drOp = op.ShowDialog();
                if (drOp == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void menuImportFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Open op = new Open(ConfigDirectories.GetDocumentsFolder());
                DialogResult drOp = op.ShowDialog();
                if (drOp == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fichiers_Click(object sender, EventArgs e)
        {
            FileImport fi = new FileImport();
            DialogResult dr = fi.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                fi.path.Text = ConfigDirectories.RemoveLeadBackslash(fi.path.Text);
                Project.AddFile(Project.CurrentProject, Path.GetDirectoryName(fi.path.Text), Path.GetFileName(fi.path.Text));
                ConfigDirectories.AddFile(Project.CurrentProject.Title, fi.path.Text, fi.ofd.FileName);
                Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Project.CurrentProject.ReloadProject();
            }
        }

        private void supprimerToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            TreeNode t = this.currentNodeContext;
            if (t != null)
            {
                if (t.Tag != null)
                {
                    DialogResult dr = MessageBox.Show(this.Translate("SuppressText"), this.Translate("SuppressTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (t.Tag is Library.MasterPage)
                        {
                            MasterPage mp = t.Tag as MasterPage;
                            foreach (HTMLObject obj in mp.Objects)
                            {
                                Project.CurrentProject.Instances.Remove(obj);
                            }
                            Project.CurrentProject.MasterPages.Remove(mp);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                        else if (t.Tag is Library.MasterObject)
                        {
                            MasterObject mo = t.Tag as MasterObject;
                            foreach (HTMLObject obj in mo.Objects)
                            {
                                Project.CurrentProject.Instances.Remove(obj);
                            }
                            Project.CurrentProject.MasterObjects.Remove(mo);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                        else if (t.Tag is Library.HTMLTool)
                        {
                            Library.HTMLTool tool = t.Tag as Library.HTMLTool;
                            Project.CurrentProject.Tools.Remove(tool);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                        else if (t.Tag is Library.HTMLObject)
                        {
                            HTMLObject obj = t.Tag as Library.HTMLObject;
                            if (obj.BelongsTo != null && obj.BelongsTo.StartsWith("mObj"))
                            {
                                // ce serait un master object
                                MasterObject mo = Project.CurrentProject.MasterObjects.Find(a => { return a.Name == obj.BelongsTo; });
                                if (mo != null)
                                {
                                    mo.Objects.Remove(obj);
                                }
                            }
                            else
                            {
                                Page p = Project.CurrentProject.Pages.Find(a => { return a.Name == obj.BelongsTo; });
                                if (p != null)
                                {
                                    p.Objects.Remove(obj);
                                }
                                else
                                {
                                    // il appartient plutot à une master page
                                    MasterPage mp = Project.CurrentProject.MasterPages.Find(a => { return a.Name == obj.BelongsTo; });
                                    if (mp != null)
                                    {
                                        mp.Objects.Remove(obj);
                                    }
                                }
                            }
                            Project.CurrentProject.Instances.Remove(obj);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                        else if (t.Tag is Library.SculptureObject)
                        {
                            SculptureObject sObject = t.Tag as SculptureObject;
                            Project.CurrentProject.SculptureObjects.Remove(sObject);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                        else if (t.Tag is Library.Page)
                        {
                        }
                        else if (t.Tag is string)
                        {
                        }
                    }
                }
            }
        }

        private void cntx_Opening(object sender, CancelEventArgs e)
        {
            this.currentNodeContext = this.treeView1.GetNodeAt(this.treeView1.PointToClient(MousePosition));
            this.treeView1.SelectedNode = this.currentNodeContext;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RenameProject rp = new RenameProject();
            rp.textBox1.Text = Project.CurrentProject.Title;
            DialogResult dr = rp.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string old = Project.CurrentProject.Title;
                string renamed = rp.textBox1.Text;
                try
                {
                    ConfigDirectories.RenameDirectoryProject(old, renamed);
                    Project.CurrentProject.Title = renamed;
                    AppDomain.CurrentDomain.SetData("fileName", renamed + ".bin");
                    Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                    this.Text = String.Format(this.Translate("SoftwareTitleOnProject"), Project.CurrentProject.Title);
                }
                catch
                {
                    MessageBox.Show(this.Translate("FileLoadErrorText"), this.Translate("FileLoadErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Project.CurrentProject.ReloadProject();
                }
            }
        }

        private void GenerateProduction(string path, Library.File f, string destinationDirectory)
        {
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            SiteGenerationForm f = new SiteGenerationForm();
            if (Library.Project.CurrentProject.Configuration.Elements.AllKeys.Contains("BASE_HREF"))
            {
                f.path.Text = Library.Project.CurrentProject.Configuration.Elements["BASE_HREF"];
            }
            DialogResult dr = f.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                Library.Project.CurrentProject.Configuration.Elements.Remove("BASE_HREF");
                Library.Project.CurrentProject.Configuration.Elements.Add("BASE_HREF", f.path.Text);
                this.GenerateProduction("", Project.CurrentProject.Files.First(), f.ffd.SelectedPath);
                // it's possible added new files
                Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Project.CurrentProject.ReloadProject();
            }
        }

        private void créerUneSculptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SculptureCreationForm creation = new SculptureCreationForm();
                DialogResult dr = creation.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Project proj = Project.CurrentProject;
                    proj.SculptureObjects.Add(creation.SculptureObject);
                    Project.Save(proj, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                    Project.CurrentProject.ReloadProject();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void supprimerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            TreeNode t = this.currentNodeContext;
            if (t != null)
            {
                if (t.Tag != null)
                {
                    if (t.Tag is Library.HTMLTool)
                    {
                        DialogResult dr = MessageBox.Show(this.Translate("SuppressText"), this.Translate("SuppressTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            Library.HTMLTool tool = t.Tag as Library.HTMLTool;
                            Project.CurrentProject.Tools.Remove(tool);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                    }
                }
            }
        }

        private void supprimerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode t = this.currentNodeContext;
            if (t != null)
            {
                if (t.Tag != null)
                {
                    if (t.Tag is Library.MasterObject)
                    {
                        DialogResult dr = MessageBox.Show(this.Translate("SuppressText"), this.Translate("SuppressTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            MasterObject mo = t.Tag as MasterObject;
                            foreach (HTMLObject obj in mo.Objects)
                            {
                                Project.CurrentProject.Instances.Remove(obj);
                            }
                            Project.CurrentProject.MasterObjects.Remove(mo);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                    }
                }
            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode t = this.currentNodeContext;
            if (t != null)
            {
                if (t.Tag != null)
                {
                    if (t.Tag is Library.MasterPage)
                    {
                        DialogResult dr = MessageBox.Show(this.Translate("SuppressText"), this.Translate("SuppressTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            MasterPage mp = t.Tag as MasterPage;
                            foreach (HTMLObject obj in mp.Objects)
                            {
                                Project.CurrentProject.Instances.Remove(obj);
                            }
                            Project.CurrentProject.MasterPages.Remove(mp);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                    }
                }
            }
        }

        private void supprimerToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TreeNode t = this.currentNodeContext;
            if (t != null)
            {
                if (t.Tag != null)
                {
                    if (t.Tag is string)
                    {
                        DialogResult dr = MessageBox.Show(this.Translate("SuppressText"), this.Translate("SuppressTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            string fileName = t.Tag as string;
                            if (!Project.CurrentProject.Files.Exists(x => x.FileName == fileName))
                            {
                                // recherche dans les dossiers
                                string[] path = fileName.TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
                            }
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                    }
                }
            }
        }

        private void supprimerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TreeNode t = this.currentNodeContext;
            if (t != null)
            {
                if (t.Tag != null)
                {
                    if (t.Tag is Library.Page)
                    {
                        DialogResult dr = MessageBox.Show(this.Translate("SuppressText"), this.Translate("SuppressTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            Page page = t.Tag as Page;
                            if (!Project.CurrentProject.Pages.Remove(page))
                            {
                            }
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                    }
                }
            }
        }

        private void supprimerToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            TreeNode t = this.currentNodeContext;
            if (t != null)
            {
                if (t.Tag != null)
                {
                    if (t.Tag is Library.SculptureObject)
                    {
                        DialogResult dr = MessageBox.Show(this.Translate("SuppressText"), this.Translate("SuppressTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            SculptureObject sObject = t.Tag as SculptureObject;
                            Project.CurrentProject.SculptureObjects.Remove(sObject);
                            Project.Save(Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                            Project.CurrentProject.ReloadProject();
                        }
                    }
                }
            }
        }

        private void cntxMenuOpen_Click(object sender, EventArgs e)
        {
            this.treeView1_DoubleClick(sender, e);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.tutoExec != null)
            {
                this.tutoExec.Abort();
                this.tutoExec = null;
            }
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void InitLanguage()
        {
            string culture = Localization.Strings.SelectLanguage(Localization.Strings.Languages.Keys.ElementAt(this.currentLocale));
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(culture);
        }

        private void languages_Click(object sender, EventArgs e)
        {
            if (this.currentLocale + 1 == Localization.Strings.Languages.Keys.Count)
            {
                this.currentLocale = 0;
            }
            else
            {
                ++this.currentLocale;
            }
            string culture = Localization.Strings.SelectLanguage(Localization.Strings.Languages.Keys.ElementAt(this.currentLocale));
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(culture);
            Localization.Strings.RefreshAll();
            this.OpenProject();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            this.elapseTimer = 0;
            this.tdd.Interval = 200;
            this.tdd.Enabled = true;
            this.tdd.Start();
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.tdd.Enabled)
            {
                this.tdd.Stop();
                this.tdd.Enabled = false;
            }
        }

        private void errorZone_Click(object sender, EventArgs e)
        {
            string fileName = AppDomain.CurrentDomain.GetData("fileName").ToString();
            Project.Load(ConfigDirectories.GetDocumentsFolder(), fileName, new Project.OpenProject(OpenProject));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tutoExec = new TutorialExec();
            this.tutoExec.Exec();
        }

        private void tutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tutoExec = new TutorialExec();
            this.tutoExec.Exec();
        }

    }
}
