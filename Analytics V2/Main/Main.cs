using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;
using System.Xml.Linq;

namespace Analytics_V2
{
    public partial class Main : KryptonForm
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private FileBrowser _FileBrowser;             ////
        private FileBrowser _LocalFileBrowser;        //
        private Navigator _Navigator;                 //
        private SpecificCountries _SpecificCountries; // Main User Controls
        private SpecificTools _SpecificTools;         //

        private List<Config> _ConfigsList;     // List of configs.
        private List<String> _InputFiles;      // List of input files.

        private List<Thread> _PoolThreads;           // List of threads.
        private List<Launcher> _LaunchersList;       // List of launchers.
        private List<ProgressBar> _ProgressBarsList; // List of progress bars.
        private List<Log> _LogsList;                 // List of logs.
        

        private Boolean _IsCopy;          // Boolean which indicates if it's a copy or a cut.
        private String _SourcePath;       // Source path.
        private String _TargetPath;       // Target path.
        private String _PreviousNodeName; // Previous node name.
        private String _LogsPath;         // Logs path.

        private Authentication _Session;         // Authentication instance.
        private Administration _Administration;  // The Administration module UC.
        private Settings _Settings;              // The Settings module UC.
        private KryptonForm _AdministrationForm; // Form hosting the Administration UC.
        private KryptonForm _SettingsForm;       // Form hosting the Settings UC.
         

        private delegate void processOnMainThread(int[] tab);                                                     // Delegate type.
        private processOnMainThread _UpdateProgressBarDel;                                                        // Delegate for updating the progress bar.
        private delegate void processOnMainThread2(String[] tab);                                                 // Delegate type2.
        private processOnMainThread2 _UpdateRichTextBoxDel;                                                       // Delegate for updating the RTB.
        private delegate void processOnMainThread3(int i, string str, string str2, List<Process> pl, int number); // Delegate type3.
        private processOnMainThread3 _AddLogsGridViewDel;                                                         // Delegate for adding the logs grid view.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Main()
        {
            InitializeComponent();

            _FileBrowser = new FileBrowser(InitializePath());
            _LocalFileBrowser = new FileBrowser(Properties.Settings.Default.local_path);
            _Navigator = new Navigator();
            _SpecificCountries = new SpecificCountries();
            _SpecificTools = new SpecificTools();
            _Session = new Authentication("user");
            _Session.CheckSavedPUC(System.Environment.MachineName);
            _Administration = new Administration();
            _Settings = new Settings();
            InitializeInterface();

            _ConfigsList = new List<Config>();        
            _InputFiles = new List<String>();
            _PoolThreads = new List<Thread>();
            _LaunchersList = new List<Launcher>();
            _ProgressBarsList = new List<ProgressBar>();
            _LogsList = new List<Log>();

            _IsCopy = false;
            _SourcePath = null;
            _TargetPath = null;
            _PreviousNodeName = null;
            _LogsPath = null;

            _UpdateProgressBarDel = new processOnMainThread(UpdateProgressBar);
            _UpdateRichTextBoxDel = new processOnMainThread2(UpdateRichTextBox);
            _AddLogsGridViewDel = new processOnMainThread3(AddLogsGridView);
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /******************************************\
         * Initialize interface :                 *
         *  - Create Administration Form          *
         *  - Add UC File Browser.                *
         *  - Add UC Navigator.                   *
         *  - Add UC Specifics tools & countries. *
         *  - Initialize launch button.           *
         *  - Define event Handlers.              *
        \******************************************/

        private void InitializeInterface()
        {
            _Session.Dock = DockStyle.Fill;
            _AdministrationForm = new KryptonForm();
            _AdministrationForm.Text = "Administration";
            _AdministrationForm.StartPosition = FormStartPosition.CenterScreen;
            _AdministrationForm.Icon = global::Analytics_V2.Properties.Resources.Administration ;
            _AdministrationForm.Size = new System.Drawing.Size(600, 400);
            _AdministrationForm.Controls.Add(_Administration);
            _AdministrationForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HideAdministrationForm);

            _Settings.Dock = DockStyle.Fill;
            _SettingsForm = new KryptonForm();
            _SettingsForm.Text = "Settings";
            _SettingsForm.StartPosition = FormStartPosition.CenterScreen;
            _SettingsForm.Icon = global::Analytics_V2.Properties.Resources.Settings;
            _SettingsForm.Size = new System.Drawing.Size(680, 320);
            _SettingsForm.Controls.Add(_Settings);
            _SettingsForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HideSettingsForm);

            this.FileBrowserNavigator.Pages[0].Controls.Add(_FileBrowser);
            this.FileBrowserNavigator.Pages[0].Tag = _FileBrowser;
            _FileBrowser.PopulateTreeView();
            this.FileBrowserNavigator.Pages[1].Controls.Add(_LocalFileBrowser);
            this.FileBrowserNavigator.Pages[1].Tag = _LocalFileBrowser;
            _LocalFileBrowser.PopulateTreeView();

            this.MainBoardSplitContainer3.Panel1.Controls.Add(_Navigator);
            this.MainBoardSplitContainer4.Panel1.Controls.Add(_SpecificCountries);
            this.MainBoardSplitContainer4.Panel2.Controls.Add(_SpecificTools);

            MainBoardSplitContainer1.FixedPanel = FixedPanel.Panel1;

            _FileBrowser.TreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseDown);
            _FileBrowser.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            _FileBrowser.CutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            _FileBrowser.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            _FileBrowser.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            _FileBrowser.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            _FileBrowser.DeleteToolStripMenuItem.Click += new System.EventHandler(this.SuppressToolStripButton_Click);
            _FileBrowser.RenameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
            _FileBrowser.FullCollapseToolStripMenuItem.Click += new System.EventHandler(this.FullCollapseToolStripMenu_Click);
            _FileBrowser.FullExpandToolStripMenuItem.Click += new System.EventHandler(this.FullExpandToolStripMenuItem_Click);
            _FileBrowser.NewDirectoryToolStripMenuItem.Click += new System.EventHandler(this.NewDirectoryToolStripMenuItem_Click);
            _FileBrowser.TreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_AfterLabelEdit);
            _FileBrowser.TreeView.KeyDown += new KeyEventHandler(this.TreeView_KeyDown);
            _FileBrowser.TreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            _FileBrowser.TreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            _FileBrowser.TreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            _FileBrowser.TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(KeepExpandedNode);
            _FileBrowser.TreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(RemoveExpandedNode);

            _LocalFileBrowser.TreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseDown);
            _LocalFileBrowser.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            _LocalFileBrowser.CutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            _LocalFileBrowser.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            _LocalFileBrowser.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            _LocalFileBrowser.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            _LocalFileBrowser.DeleteToolStripMenuItem.Click += new System.EventHandler(this.SuppressToolStripButton_Click);
            _LocalFileBrowser.RenameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
            _LocalFileBrowser.FullCollapseToolStripMenuItem.Click += new System.EventHandler(this.FullCollapseToolStripMenu_Click);
            _LocalFileBrowser.FullExpandToolStripMenuItem.Click += new System.EventHandler(this.FullExpandToolStripMenuItem_Click);
            _LocalFileBrowser.NewDirectoryToolStripMenuItem.Click += new System.EventHandler(this.NewDirectoryToolStripMenuItem_Click);
            _LocalFileBrowser.TreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_AfterLabelEdit);
            _LocalFileBrowser.TreeView.KeyDown += new KeyEventHandler(this.TreeView_KeyDown);
            _LocalFileBrowser.TreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            _LocalFileBrowser.TreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            _LocalFileBrowser.TreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            _LocalFileBrowser.TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(KeepExpandedNode);
            _LocalFileBrowser.TreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(RemoveExpandedNode);

            _Navigator.NavigatorControl.SelectedPageChanged += new System.EventHandler(NavigatorControl_SelectedPageChanged);
            _SpecificCountries.SpecificCountriesListBox.ListBox.DoubleClick += new System.EventHandler(SpecificCountriesListBox_DoubleClick);
            _SpecificTools.SpecificToolsListBox.ListBox.DoubleClick += new System.EventHandler(SpecificToolsListBox_DoubleClick);
            _Session.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            _Session.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            _Session.PasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(PasswordTextBox_KeyDown);

            this.StatusToolStripMenuItem.Text = "Connected as " + _Session.GetAccessType();
        }

        /***************************************\
         * Get Path of configs from WebService *
        \***************************************/

        private String InitializePath()
        {
            AnalyticsWebService.AnalyticsSoapClient request; // Webservice instance.
            string path = null;

            request = new AnalyticsWebService.AnalyticsSoapClient();

            try
            {
                request.Open();
                path = request.Get_Path("ADM");
                request.Close();
            }

            catch (Exception ex)
            {
                var result = KryptonMessageBox.Show("Path introuvable, veuillez le définir manuellement", "Path introuvable",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Exclamation);

                if (result == DialogResult.OK)
                {
                    FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
                    openFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

                    DialogResult result2 = openFolderDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        path = openFolderDialog.SelectedPath;
                    }
                }
            }
            return path;
        }

        #endregion

        #region Events

        /****************************************************************************************\
         * Event of clicking on an element of the tree :                                        *
         *  - Check (on the fly) if the xml is valid or not.                                    *
         *  - Enable/Disable buttons regarding the selected node.                               *
         *  - Check if it's a config (and not a directory) and if it exists in the config list. *
         *  - If config doesn't exist in the list, create it and add it to the list.            *
         *  - Display a summary of the current config (name, process...)                        *
        \****************************************************************************************/

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            Boolean configExists = false;
            TreeView treeView = ((TreeView)(sender));
            FileBrowser fileBrowser = ((FileBrowser)(((KryptonGroupBox)(((Panel)treeView.Parent).Parent)).Parent));

            treeView.SelectedNode = treeView.GetNodeAt(e.X, e.Y);
            
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (treeView.SelectedNode != null && treeView.SelectedNode.ImageIndex == 2)
                {
                    XmlReader ok = XmlReader.Create(treeView.SelectedNode.FullPath);

                    try
                    {
                        // Check the validity of the XML
                        while (ok.Read()) { }
                        ok.Close();

                        // Enable buttons
                        EditToolStripButton.Enabled = true;
                        SuppressToolStripButton.Enabled = true;
                        LaunchToolStripButton.Enabled = true;

                        // Check if the config already exists in the list of config
                        if (_ConfigsList.Count > 0)
                            foreach (Config element in _ConfigsList)
                                if (element.Get_Path().Equals(treeView.SelectedNode.FullPath))
                                    configExists = true;

                        // Create the config
                        if (!configExists)
                            _ConfigsList.Add(new Config(treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0], treeView.SelectedNode.FullPath));

                        // Retrieve the selected config.
                        IEnumerable<Config> retrieveConfigQuerry = from item in _ConfigsList
                                                                   where item.Get_Path().Equals(treeView.SelectedNode.FullPath)
                                                                   select item;

                        // Display the summary of the config.
                        foreach (Config set in retrieveConfigQuerry)
                            _Navigator.DisplayConfigSummary(set.Get_Name(), set.Get_ProcessList(), set.Get_Warning());

                        // Event handler for the expand/minimize arrow
                        foreach (KryptonHeaderGroup element in _Navigator.Get_ProcessHeaderGroupList())
                            element.ButtonSpecs[0].Click += new EventHandler(ProcessButtonSpec_Click);
                    }
                    catch(Exception exception)
                    {
                        ok.Close();

                        // Disable buttons
                        //EditToolStripButton.Enabled = false;
                        SuppressToolStripButton.Enabled = true;
                        LaunchToolStripButton.Enabled = false;

                        KryptonMessageBox.Show("Error !! The selected configuration is INVALID : \n\n                        " + exception.ToString(), "invalid XML",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                    }
                }

                else if (treeView.SelectedNode != null && treeView.SelectedNode.ImageIndex == 1)
                {
                    // Enable buttons
                    EditToolStripButton.Enabled = false;
                    SuppressToolStripButton.Enabled = true;
                    LaunchToolStripButton.Enabled = false;

                    // Display folder name.
                    _Navigator.DisplayFolderName(treeView.SelectedNode.Text);
                }

                else
                {
                    // Disable buttons
                    EditToolStripButton.Enabled = false;
                    SuppressToolStripButton.Enabled = false;
                    LaunchToolStripButton.Enabled = false;
                }
            }

            // Display/hide possibilities regarding if it's a file or a directory.
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (treeView.SelectedNode != null && treeView.SelectedNode.ImageIndex == 2)
                {
                    try
                    {
                        // Check the validity of the XML
                        XmlReader ok = XmlReader.Create(treeView.SelectedNode.FullPath);
                        while (ok.Read()) { }
                        ok.Close();

                        fileBrowser.RenameToolStripMenuItem.Enabled = true;
                        fileBrowser.DeleteToolStripMenuItem.Enabled = true;
                        fileBrowser.EditToolStripMenuItem.Enabled = true;
                        fileBrowser.CutToolStripMenuItem.Enabled = true;
                        fileBrowser.CopyToolStripMenuItem.Enabled = true;
                    }

                    catch (Exception exception)
                    {
                        KryptonMessageBox.Show("Error !! The selected configuration is INVALID : \n\n                        " + exception.ToString(), "invalid XML",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                    }
                }

                else if (treeView.SelectedNode != null && treeView.SelectedNode.ImageIndex == 1)
                {
                    fileBrowser.RenameToolStripMenuItem.Enabled = true;
                    fileBrowser.DeleteToolStripMenuItem.Enabled = true;
                    fileBrowser.EditToolStripMenuItem.Enabled = false;
                    fileBrowser.CutToolStripMenuItem.Enabled = false;
                    fileBrowser.CopyToolStripMenuItem.Enabled = false;
                }

                else 
                {
                    fileBrowser.RenameToolStripMenuItem.Enabled = false;
                    fileBrowser.DeleteToolStripMenuItem.Enabled = false;
                    fileBrowser.EditToolStripMenuItem.Enabled = false;
                    fileBrowser.CutToolStripMenuItem.Enabled = false;
                    fileBrowser.CopyToolStripMenuItem.Enabled = false;
                }
            }
        }

        /****************************************************************************************\
         * Event of clicking on the arrow of the headergroup (for each process)                 *
         *   - if we click on the down arrow --> expand panel                                   *
         *   - if we click on the up arrow --> minimize panel                                   *
        \****************************************************************************************/

        private void ProcessButtonSpec_Click(object sender, EventArgs e)
        {
            if (sender is ButtonSpecHeaderGroup)
            {
                ButtonSpecHeaderGroup buttonSpecHeaderGroup = sender as ButtonSpecHeaderGroup;
                KryptonHeaderGroup headerGroup = buttonSpecHeaderGroup.Tag as KryptonHeaderGroup;

                if (buttonSpecHeaderGroup.Type == ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonMinimize)
                {
                    buttonSpecHeaderGroup.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonExpand;
                    headerGroup.Size = new System.Drawing.Size(150, 23);
                    //_LaunchButton.Location = new System.Drawing.Point(_LaunchButton.Location.X, _LaunchButton.Location.Y - 72);
                }
                else if (buttonSpecHeaderGroup.Type == ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonExpand)
                {
                    buttonSpecHeaderGroup.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonMinimize;
                    headerGroup.Size = new System.Drawing.Size(150, 95);
                    //_LaunchButton.Location = new System.Drawing.Point(_LaunchButton.Location.X, _LaunchButton.Location.Y + 72);
                }
            }
        }

        /*************************************************************\
         * Event of clicking of the element refresh of the MenuStrip *
         *  - Refresh the TreeView.                                  *
        \*************************************************************/

        private void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).PopulateTreeView();
            _ConfigsList.Clear();
        }

        /*****************************************************************************\
         * Events of clicking of the elements cut/copy/paste of the ContextMenuStrip *
         *  - Copy config (then enable paste item).                                  *
         *  - Paste config (then disable paste item).                                *
        \*****************************************************************************/

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);

            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("copy"))
                    Copy(treeView);
            }

            else 
                Copy(treeView);           
        }

        private void Copy(TreeView treeView)
        {
            FileBrowser fileBrowser = ((FileBrowser)(((KryptonGroupBox)(((Panel)treeView.Parent).Parent)).Parent));
            _IsCopy = true;
            _SourcePath = treeView.SelectedNode.FullPath;
            _PreviousNodeName = treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0];
            fileBrowser.PasteToolStripMenuItem.Enabled = true;
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);

            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("cut"))
                    Cut(treeView);
            }

            else
                Cut(treeView); 
        }

        private void Cut(TreeView treeView)
        {
            FileBrowser fileBrowser = ((FileBrowser)(((KryptonGroupBox)(((Panel)treeView.Parent).Parent)).Parent));
            _IsCopy = false;
            _SourcePath = treeView.SelectedNode.FullPath;
            _PreviousNodeName = treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0];
            fileBrowser.PasteToolStripMenuItem.Enabled = true;
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);
            FileBrowser fileBrowser = ((FileBrowser)(((KryptonGroupBox)(((Panel)treeView.Parent).Parent)).Parent));
            if (treeView.SelectedNode.ImageIndex == 2)
            {
                if (File.Exists(treeView.SelectedNode.Parent.FullPath + "\\" + _PreviousNodeName + ".xml"))
                    _TargetPath = treeView.SelectedNode.Parent.FullPath + "\\" + _PreviousNodeName + "-COPY.xml";
                else _TargetPath = treeView.SelectedNode.Parent.FullPath + "\\" + _PreviousNodeName + ".xml";
            }

            else
            {
                if (File.Exists(treeView.SelectedNode.FullPath + "\\" + _PreviousNodeName + ".xml"))
                    _TargetPath = treeView.SelectedNode.FullPath + "\\" + _PreviousNodeName + "-COPY.xml";
                else _TargetPath = treeView.SelectedNode.FullPath + "\\" + _PreviousNodeName + ".xml";
            }

            Console.WriteLine("Target : " + _TargetPath);
            if(_IsCopy)
                File.Copy(_SourcePath, _TargetPath,true);
            else if(!_IsCopy)
                File.Move(_SourcePath, _TargetPath);

            fileBrowser.PopulateTreeView();
            fileBrowser.PasteToolStripMenuItem.Enabled = false;
        }


        /******************************************************************************\
         * Events of clicking of the element edit of the MenuStrip                    *
         * (or right click on a tree node --> Edit)                                   *    
         *  - Create a new tab with the content of the xml file (+ its event handler) *
         *  - Open it if it already exists                                            *
        \******************************************************************************/

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);

            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("edit"))
                    EditConfig(treeView);
            }

            else
                EditConfig(treeView);   
        }

        private void EditToolStripButton_Click(object sender, EventArgs e)
        {
            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("edit"))
                    EditConfig(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
            }

            else
                EditConfig(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
        }

        private void EditConfig(TreeView treeView)
        {
            Boolean tabExists = false;

            if (treeView.SelectedNode.ImageIndex == 2)
            {
                foreach (KryptonPage element in _Navigator.NavigatorControl.Pages)
                {
                    if (treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0].Equals(element.Text.Split(new string[] { "*" }, StringSplitOptions.None)[0]))
                        tabExists = true;
                    for (int i = 0; i < _Navigator.NavigatorControl.Pages.Count; i++)
                        if (treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0].Equals(_Navigator.NavigatorControl.Pages[i].Text.Split(new string[] { "*" }, StringSplitOptions.None)[0]))
                        {
                            _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages[i];

                            if (_Navigator.NavigatorControl.SelectedPage.Text.Contains('*'))
                                SaveToolStripButton.Enabled = true;
                            else SaveToolStripButton.Enabled = false;
                        }
                }

                if (!tabExists)
                {
                    _Navigator.AddTab(treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0], treeView.SelectedNode.FullPath);
                    _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages[_Navigator.NavigatorControl.Pages.Count - 1];
                    SaveToolStripButton.Enabled = false;
                    var childrens = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<KryptonRichTextBox>().ToList();
                    foreach (var richTextBox in childrens)
                        richTextBox.TextChanged += new EventHandler(RichTextBoxTextChanged);
                }
            }
        }

        /**************************************************************\
         * Events when the RTB is modified / When tab has changed     *
         * (Inform the user if the config has been modified or not    *
         *   - modify the label of the tab/                           *
         *   - enable/disable the save button.                        *
        \**************************************************************/

        private void RichTextBoxTextChanged(object sender, EventArgs e)
        {
            if (!_Navigator.NavigatorControl.SelectedPage.Text.Contains('*'))
                _Navigator.NavigatorControl.SelectedPage.Text = _Navigator.NavigatorControl.SelectedPage.Text + "*";
            SaveToolStripButton.Enabled = true;
        }

        private void NavigatorControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (_Navigator.NavigatorControl.SelectedPage.Text.Contains('*'))
                SaveToolStripButton.Enabled = true;
            else SaveToolStripButton.Enabled = false;
        }

        /**********************************************************************\
         * Event of clicking on the element "Save" of the MenuStrip           *
         *   - Retrieve the selected config tab (for the encoding).           *
         *   - Retrieve the encoding.                                         *
         *   - Retrieve the richtextbox text.                                 *
         *   - Do special replacements before saving (function XmlTreatment). *
         *   - Check Validity of the xml.                                     *
         *   - Create the streamwriter and save.                              *
         *   - Propose to save anyway if the xml is not valid.                *
         *   - Edit label and button.                                         *
        \**********************************************************************/

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            Encoding encoding;
            String treatedString;
            StreamWriter writer;

            // Retrieve the selected config tab (for the encoding).
            IEnumerable<Config> retrieveConfigQuerry = from item in _ConfigsList
                                                       where item.Get_Name().Equals(_Navigator.NavigatorControl.SelectedPage.Text.Split(new string[] { "*" }, StringSplitOptions.None)[0])
                                                       select item;

            // Retrieve the encoding.
            foreach (Config set in retrieveConfigQuerry)
            {
                encoding = Encoding.GetEncoding(set.Get_XmlEncoding());

                // Retrieve the richtextbox text
                var childrens = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<KryptonRichTextBox>().ToList();
                foreach (var richTextBox in childrens)
                {
                    treatedString = richTextBox.Text;

                    // Some replacement before saving (especially xml char like '"', '&', '<' and '>').
                    treatedString = XmlTreatment(treatedString);

                    XmlReader ok = XmlReader.Create(new StringReader(treatedString));
             
                    try
                    {
                        // Check Validity of the xml
                        while (ok.Read()) { }
                        ok.Close();

                        // Create the streamwriter and save
                        writer = new StreamWriter(richTextBox.Tag.ToString(), false, encoding);
                        writer.WriteLine(treatedString);
                        writer.Close();
                    }

                    catch (Exception ex)
                    {
                        ok.Close();

                        var result = KryptonMessageBox.Show("Warning !! There is an error in the xml: \n\n                        " + ex + "\n\nSave anyway?", "invalid XML",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            // Create the streamwriter and save anyway.
                            writer = new StreamWriter(richTextBox.Tag.ToString(), false, encoding);
                            writer.WriteLine(treatedString);
                            writer.Close();
                        }
                    }
                }
            }

            // Edit labels and button on the menustrip.
            _Navigator.NavigatorControl.SelectedPage.Text = _Navigator.NavigatorControl.SelectedPage.Text.Replace("*", "");
            SaveToolStripButton.Enabled = false;
        }

        private String XmlTreatment(String str)
        {
            Regex regex = new Regex("(<Field Name=\".*\" Val=\".*)(\")(.*\" Pos=\".*\" />)");   // Replace patern.
            Regex regex2 = new Regex("(<Field Name=\".*\" Type=\".*\" Val=\".*)(\")(.*\" />)"); // Linedel patern.
            Regex regex3 = new Regex("(<Field Name=\"Separator\" Val=\".*)(\")(.*\" />)");      // Column patern.
            Regex regex4 = new Regex("(<Field Name=\".*\" Val=\".*)(<)(.*\" Pos=\".*\" />)");
            Regex regex5 = new Regex("(<Field Name=\".*\" Type=\".*\" Val=\".*)(<)(.*\" />)");
            Regex regex6 = new Regex("(<Field Name=\"Separator\" Val=\".*)(<)(.*\" />)");
            Regex regex7 = new Regex("(<Field Name=\".*\" Val=\".*)(>)(.*\" Pos=\".*\" />)");
            Regex regex8 = new Regex("(<Field Name=\".*\" Type=\".*\" Val=\".*)(>)(.*\" />)");
            Regex regex9 = new Regex("(<Field Name=\"Separator\" Val=\".*)(>)(.*\" />)");

            str = str.Replace("&", "&amp;");

            while (regex.IsMatch(str))
                str = regex.Replace(str, "$1&quot;$3");
            while (regex2.IsMatch(str))
                str = regex2.Replace(str, "$1&quot;$3");
            while (regex3.IsMatch(str))
                str = regex3.Replace(str, "$1&quot;$3");
            while (regex4.IsMatch(str))
                str = regex4.Replace(str, "$1&lt;$3");
            while (regex5.IsMatch(str))
                str = regex5.Replace(str, "$1&lt;$3");
            while (regex6.IsMatch(str))
                str = regex6.Replace(str, "$1&lt;$3");
            while (regex7.IsMatch(str))
                str = regex7.Replace(str, "$1&gt;$3");
            while (regex8.IsMatch(str))
                str = regex8.Replace(str, "$1&gt;$3");
            while (regex9.IsMatch(str))
                str = regex9.Replace(str, "$1&gt;$3");

            return str;
        }

        /***************************************************************\
         * Events of clicking of the element suppress of the MenuStrip *
         * (or right click on a tree node --> Delete)                  *    
         * (or Keys Delete)                                            *
         *  - Ask confirmation then suppress the config/director       *
        \***************************************************************/

        private void SuppressToolStripButton_Click(object sender, EventArgs e)
        {
            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("suppress"))
                    Delete(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
            }

            else
                Delete(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            TreeView treeView = ((TreeView)sender);
            
            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("suppress") && (e.KeyCode == Keys.Delete))
                        Delete(treeView);
            }

            else if (e.KeyCode == Keys.Delete)
                    Delete(treeView);
        }

        private void Delete(TreeView treeView)
        {
            if (treeView.SelectedNode.ImageIndex == 2)
            {
                var result = KryptonMessageBox.Show("Do you really want to delete the following configuration?  : \n\n                        " + treeView.SelectedNode.Text + "\n\nNote that this action is IRREVERSIBLE.", "Delete this file",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    File.Delete(treeView.SelectedNode.FullPath);
                    ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).PopulateTreeView();
                }
            }

            else if (treeView.SelectedNode.ImageIndex == 1)
            {
                var result = KryptonMessageBox.Show("Do you really want to delete the following Directory?  : \n\n                        " + treeView.SelectedNode.Text + "\n\nNote that this action is IRREVERSIBLE.", "Delete this folder",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Directory.Delete(treeView.SelectedNode.FullPath, true);
                    ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).PopulateTreeView();
                }
            }
        }

        /****************************************************\
         * Events of right click on a Treenode --> Rename   *
         *  - Rename the configuration.                     *
        \****************************************************/

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);

            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("rename"))
                {
                    treeView.LabelEdit = true;
                    treeView.SelectedNode.BeginEdit();
                }
            }

            else
            {
                treeView.LabelEdit = true;
                treeView.SelectedNode.BeginEdit();
            }        
        }

        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            TreeView treeView = ((TreeView)sender);
            _SourcePath = treeView.SelectedNode.FullPath;

            treeView.LabelEdit = false;

            if (e.Label == null || e.Label.Trim().Length == 0 || File.Exists(treeView.SelectedNode.Parent.FullPath + "\\" + e.Label.Trim()) || Directory.Exists(treeView.SelectedNode.Parent.FullPath + "\\" + e.Label.Trim()))
            //if (e.Label.Trim().Length == 0)
                e.CancelEdit = true;

            else
            {
                _TargetPath = treeView.SelectedNode.Parent.FullPath + "\\" + e.Label.Trim();
                if (treeView.SelectedNode != null && treeView.SelectedNode.ImageIndex == 2)
                    File.Move(_SourcePath, _TargetPath);
                else if (treeView.SelectedNode != null && treeView.SelectedNode.ImageIndex == 1)
                    Directory.Move(_SourcePath, _TargetPath);
            }
        }

        /***********************************************************************\
         * Events of right click on a Treenode --> Full Collapse / Full Expand *
         *  - Full Collapse / Expand.                                          *
        \***********************************************************************/

        private void FullCollapseToolStripMenu_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);
            treeView.CollapseAll();
            treeView.Nodes[0].Expand();
        }

        private void FullExpandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);
            treeView.ExpandAll();
        }

        /*****************************************************************\
         * Events of clicking the element create config of the MenuStrip *
         *  - Create a new file.                                         *
        \*****************************************************************/

        private void NewFileToolStripButton_Click(object sender, EventArgs e)
        {
            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("newConfig"))
                    NewFile();
            }

            else
                NewFile();
        }

        private void NewFile()
        {
            try
            {
                //XmlDocument doc = new XmlDocument();
                string path = "";

                if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ImageIndex == 2)
                    path = ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.Parent.FullPath + "\\New Config.xml";

                else
                {
                    path = ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.FullPath + "\\New Config.xml";
                    ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.Expand();
                    ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).AddNodePath(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode);
                }

                //File.Create(path).Close();

                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "iso-8859-1", "yes");
                XmlElement rootNode = xmlDoc.CreateElement("region");
                xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
                xmlDoc.AppendChild(rootNode);

                XmlWriter writer = XmlWriter.Create(path, null);
                xmlDoc.Save(writer);
                writer.Close();

                ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).PopulateTreeView();
                ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).ScrollToCreatedItem(path);
            }

            catch (Exception ex) { Console.WriteLine(ex); }
        }

        /********************************************************\
         * Events of right click on a Treenode --> New Folder   *
         *  - Create a new folder.                              *
        \********************************************************/

        private void NewDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);

            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("newDirectory"))
                    NewDirectory(treeView);
            }

            else
                NewDirectory(treeView);
        }

        private void NewDirectory(TreeView treeView)
        {
            FileBrowser fileBrowser = ((FileBrowser)(((KryptonGroupBox)(((Panel)treeView.Parent).Parent)).Parent));

            try
            {
                DirectoryInfo directory;

                string path = "";

                if (treeView.SelectedNode.ImageIndex == 2)
                    path = treeView.SelectedNode.Parent.FullPath + "\\Nouveau Dossier";

                else //if (_FileBrowser.TreeView.SelectedNode.ImageIndex == 1)
                {
                    path = treeView.SelectedNode.FullPath + "\\Nouveau Dossier";
                    treeView.SelectedNode.Expand();
                    fileBrowser.AddNodePath(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode);
                }

                directory = Directory.CreateDirectory(path);
                fileBrowser.PopulateTreeView();
                fileBrowser.ScrollToCreatedItem(path);

                //Begin Edit on the created directory TO IMPLEMENT
                //FetchAndEditCreatedDirectory(_FileBrowser.TreeView.Nodes[0], path);
            }

            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private void FetchAndEditCreatedDirectory(TreeNode parent, string path)
        {
            foreach (TreeNode element in parent.Nodes)
            {
                if (element.FullPath.Equals(path))
                {
                    ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.LabelEdit = true;
                    element.BeginEdit();
                }
                else FetchAndEditCreatedDirectory(element, path);
            } 
        }


        /**************************\
         * Events of drag 'n drop *
        \**************************/

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            TreeView treeView = ((TreeView)sender);

            if (FileBrowserNavigator.SelectedPage.Text.Equals("Common"))
            {
                if (_Session.CheckIfAccessGranted("dragDrop"))
                    Drop(treeView, e);
            }

            else
                Drop(treeView, e);          
        }

        private void Drop(TreeView treeView, DragEventArgs e)
        {
            FileBrowser fileBrowser = ((FileBrowser)(((KryptonGroupBox)(((Panel)treeView.Parent).Parent)).Parent));

            TreeNode newNode;

            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = treeView.PointToClient(new Point(e.X, e.Y));
                TreeNode destinationNode = treeView.GetNodeAt(pt);
                newNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                _PreviousNodeName = newNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0];

                _SourcePath = newNode.FullPath;

                if (destinationNode.ImageIndex == 2)
                {
                    if (destinationNode.Parent.FullPath == newNode.Parent.FullPath)
                        _TargetPath = _SourcePath;
                    else if (File.Exists(destinationNode.Parent.FullPath + "\\" + newNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0] + ".xml"))
                        _TargetPath = destinationNode.Parent.FullPath + "\\" + newNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0] + "-COPY.xml";
                    else _TargetPath = destinationNode.Parent.FullPath + "\\" + newNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0] + ".xml";
                }

                else
                {
                    if (File.Exists(destinationNode.FullPath + "\\" + newNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0] + ".xml"))
                        _TargetPath = destinationNode.FullPath + "\\" + newNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0] + "-COPY.xml";
                    else _TargetPath = destinationNode.FullPath + "\\" + newNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0] + ".xml";
                }

                if (_TargetPath != _SourcePath)
                {
                    try
                    {
                        if (treeView.SelectedNode.ImageIndex == 2)
                            File.Move(_SourcePath, _TargetPath);
                        else if (treeView.SelectedNode.ImageIndex == 1)
                            Directory.Move(_SourcePath, _TargetPath.Replace(".xml", ""));
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
            }

            fileBrowser.PopulateTreeView();
        }

        /****************************************************************************************\
         * Events of double clicking an element of the specific countries list / Specific tools *
         *  - launch the specific process.                                                      *
        \****************************************************************************************/

        private void SpecificCountriesListBox_DoubleClick(object sender, EventArgs e)
        {
            _SpecificCountries.LaunchSpecificCountry(_SpecificCountries.SpecificCountriesListBox.SelectedItem.ToString());
        }

        private void SpecificToolsListBox_DoubleClick(object sender, EventArgs e)
        {
            _SpecificTools.LaunchSpecificTool(_SpecificTools.SpecificToolsListBox.SelectedItem.ToString());
        }

        /*********************************************************************************\
         * Event of clicking of the element Launch of the MenuStrip                      *
         * (or double click on the treenode).                                            *
         *   - Define paths (input files, output files, logs).                           *
         *   - Retrieve the selected config.                                             *
         *   - Create a launcher and associated UC, and add them to the main controls.   *
         *        - New Launcher.                                                        *
         *        - New Progress Bar.                                                    *
         *        - New Log.                                                             *
         *   - Launch thread.                                                            *
         *   - *INCLUDED* the delegate for updating Log & progress UC during the thread. *
        \*********************************************************************************/

        private void LaunchToolStripButton_Click(object sender, EventArgs e)
        {
            if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.FullPath.Contains("Recettes"))
            {
                if(_Session.CheckIfAccessGranted("launchConfigPreProd"))
                    LaunchConfig(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
            }

            else
                LaunchConfig(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (((TreeView)(sender)).SelectedNode.ImageIndex == 2)
            {
                if (((TreeView)(sender)).SelectedNode.FullPath.Contains("Recettes"))
                {
                    if(_Session.CheckIfAccessGranted("launchConfigPreProd"))
                        LaunchConfig(((TreeView)(sender)));
                }

                else
                    LaunchConfig(((TreeView)(sender)));
            }
        }

        private void LaunchConfig(TreeView treeView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Multiselect = true;
            // openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Define paths (input, output, logs)
                _InputFiles.Clear();
                foreach (String inputFile in openFileDialog.FileNames)
                {
                    _InputFiles.Add(inputFile);
                    _LogsPath = Path.GetDirectoryName(inputFile);
                }

                // Retrieve the selected config.
                IEnumerable<Config> retrieveConfigQuerry = from item in _ConfigsList
                                                           where item.Get_Path().Equals(treeView.SelectedNode.FullPath)
                                                           select item;

                // Create a launcher and associated UC.
                foreach (Config set in retrieveConfigQuerry)
                {
                    _LaunchersList.Add(new Launcher(set.Clone(), PreProcessToolStripButton.Checked, ProcessToolStripButton.Checked, ControlsToolStripButton.Checked, HeaderConsistencyToolStripButton.Checked, new List<String>(_InputFiles), _LogsPath, _UpdateProgressBarDel, _UpdateRichTextBoxDel, _AddLogsGridViewDel));

                    _ProgressBarsList.Add(new ProgressBar(set.Get_Name())); // Create a new ProgressBar Control & add it to the list of PB
                    _Navigator.ProgressGroupBox.Panel.Controls.Add(_ProgressBarsList[_ProgressBarsList.Count - 1]); // Add the created PB in the panel.
                    _LogsList.Add(new Log(set.Get_Name(), set.Get_TargetsNumber())); // Create a new Log Control & add it to the list of Logs.
                    _Navigator.LogsNavigator.Pages.Insert(0, _LogsList[_LogsList.Count - 1].Get_NavigatorTab()); // Add the log tab to the logsNavigator.
                    _Navigator.LogsNavigator.SelectedIndex = 0;
                    _LogsList[_LogsList.Count - 1].Get_NavigatorTab().ButtonSpecs[0].Click += new EventHandler(_Navigator.CloseLogsNavigatorTab);

                    _PoolThreads.Add(new Thread(() => _LaunchersList[_LaunchersList.Count - 1].Run(_LaunchersList.Count - 1)));
                    _PoolThreads[_PoolThreads.Count - 1].IsBackground = true;
                    _PoolThreads[_PoolThreads.Count - 1].Start();
                }
            }
        }

        private void UpdateProgressBar(int[] idAndProgress) // 0 is the ID of the running process and 1 is the progress.
        {
            Invoke(_ProgressBarsList[idAndProgress[0]].Get_UpdateProgressBarDel(), idAndProgress[1]);
        }

        private void UpdateRichTextBox(String[] idTypeAndMessage) // 0 is the ID of the running process  1 is the type, and 2 is the message.
        {
            Invoke(_ProgressBarsList[int.Parse(idTypeAndMessage[0])].Get_UpdateRichTextBoxDel(), idTypeAndMessage[1], idTypeAndMessage[2]);
        }

        private void AddLogsGridView(int id, string outputFile, string inputFile, List<Process> processList, int targetsNumber)
        {
            Invoke(_LogsList[id].Get_AddLogsGridViewDel(), outputFile, inputFile, processList, targetsNumber);
        }

        /**********************************************************\
         * Event of clicking of the element exit of the MenuStrip *
         *  - Close the form.                                     *
        \**********************************************************/

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /******************************************************************\
         * Event of clicking of the element "Connect as" of the MenuStrip *
         *  - Display connection screen.                                  *
         *  - Cancel button handler.                                      *
         *  - Connect button handler (perform authenticate)               *
         *       |-> if authenticated : Connected as....                  *
         *       |-> else, still connected as user                        *
        \******************************************************************/

        private void ConnectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KryptonForm connectForm = new KryptonForm();
            connectForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            connectForm.StartPosition = FormStartPosition.CenterScreen;
            connectForm.Size = new System.Drawing.Size(360, 110);
            connectForm.Controls.Add(_Session);
            connectForm.ShowDialog();  
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Form form = _Session.ParentForm;
            form.Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
                Connect();
        }

        private void Connect()
        {
            Boolean authentication = false;

            if (_Session.SaveConnectionCheckBox.Checked == false)
                authentication = _Session.Authenticate();
            else if (_Session.SaveConnectionCheckBox.Checked == true)
                authentication = _Session.AuthenticateAndRemember();

            if (authentication)
            {
                this.StatusToolStripMenuItem.Text = "Connected as " + _Session.GetAccessType();
                Form form = _Session.ParentForm;
                form.Close();
            }
        }

        /**********************************************************************************\
         * Event which occurs when the status textbox has been modified of the MenuStrip  *
         *  - Display/Hide the administration module buton access.                        *
        \**********************************************************************************/

        private void StatusToolStripMenuItem_TextChanged(object sender, EventArgs e)
        {
            if (_Session.GetAccessType().Equals("superadmin"))
            {
                ToolStripSeparator6.Visible = true;
                AdministrationToolStripButton.Visible = true;
            }

            else
            {
                ToolStripSeparator6.Visible = false;
                AdministrationToolStripButton.Visible = false;
            }
        }

        /*****************************************************************\
         * Event which occurs when the Administration button is clicked  *
         *  - Open the administration module.                            *
         *  - Also the handler when closing form                         *
        \*****************************************************************/

        private void AdministrationToolStripButton_Click(object sender, EventArgs e)
        {
            _AdministrationForm.Show();
            _Administration.LoadUsers();
        }

        private void HideAdministrationForm(object sender, FormClosingEventArgs e)
        {
            _AdministrationForm.Hide();
            e.Cancel = true;
        }

        /****************************************************************\
         * Event which occurs when the Settings button is clicked       *
         *  - Open the Settings module.                                 *
         *  - Also the handler when closing form                        *
        \****************************************************************/

        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            _SettingsForm.Show();

        }

        private void HideSettingsForm(object sender, FormClosingEventArgs e)
        {
            _SettingsForm.Hide();
            _LocalFileBrowser.SetPath(Properties.Settings.Default.local_path);
            _LocalFileBrowser.PopulateTreeView();

            e.Cancel = true;
        }

        /***************************************************************\
         * Event of resizing the form (for correcting a graphical bug) *
        \***************************************************************/

        private void Main_Resize(object sender, EventArgs e)
        {
            _Navigator.SummarySplitContainer1.SplitterDistance += 1;
            _Navigator.SummarySplitContainer1.SplitterDistance -= 1;
        }

        /******************************************************\
         * Method for keeping the nodes expanded when updated *
        \******************************************************/

        private void KeepExpandedNode(object sender, TreeViewCancelEventArgs e)
        {
            ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).AddNodePath(e.Node);
        }

        private void RemoveExpandedNode(object sender, TreeViewCancelEventArgs e)
        {
            ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).RemoveNodePath(e.Node);
        }

        #endregion
    }
}
