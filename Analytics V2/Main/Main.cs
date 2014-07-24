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
using System.Runtime.Serialization.Formatters.Binary;

namespace Analytics_V2
{
    public partial class Main : KryptonForm
    {
        /******************************************************************** VARIABLES ***********************************************************************/

        #region VARIABLES

        private FileBrowser _FileBrowser;             ////
        private FileBrowser _LocalFileBrowser;        //
        private Navigator _Navigator;                 //
        private SpecificCountries _SpecificCountries; // Main User Controls
        private SpecificTools _SpecificTools;         //

        private List<Config> _ConfigsList;         // List of configs.
        private List<String> _InputFiles;          // List of input files.
        String[] _ConfigBeforeModif;               // Config (raw) before modif.

        private List<Thread> _PoolThreads;           // List of threads.
        private List<Launcher> _LaunchersList;       // List of launchers.
        private List<ProgressBar> _ProgressBarsList; // List of progress bars.
        private List<Log> _LogsList;                 // List of logs.

        private Boolean _IsCopy;          // Boolean which indicates if it's a copy or a cut.
        private String _SourcePath;       // Source path.
        private String _TargetPath;       // Target path.
        private String _PreviousNodeName; // Previous node name.
        private String _LogsPath;         // Logs path.

        private Authentication _Session;                  // Authentication instance.
        private Administration _Administration;           // The Administration module UC.
        private Settings _Settings;                       // The Settings module UC.
        private History _Chronicles;                      // The History module UC.
        private ConfigSummary _ConfigSummary;             // The Config summary module UC (old "Suivi Pays").
        private WaitingScreen _WaitingScreen;             // Waiting Screen UC.
        private BatchUC _Batch;                           // Batch UC.
        private FTPManager.FTPManager _FtpManager;        // Ftp Manager UC.
        private KryptonForm _AdministrationForm;          // Form hosting the Administration UC.
        private KryptonForm _SettingsForm;                // Form hosting the Settings UC.
        private KryptonForm _HistoryAndSummaryForm;       // Form hosting the Chronicles UC and the Config summary UC.
        private KryptonForm _WaitScreenForm;              // Form hosting the WaitingScreen UC.
        private KryptonForm _BatchForm;                   // Form hosting the batch UC.
        private KryptonForm _FtpManagerForm;              // Form hosting the FTP Manager.
         
        private delegate void processOnMainThread(int[] tab);                                                     // Delegate type.
        private processOnMainThread _UpdateProgressBarDel;                                                        // Delegate for updating the progress bar.
        private delegate void processOnMainThread2(String[] tab);                                                 // Delegate type2.
        private processOnMainThread2 _UpdateRichTextBoxDel;                                                       // Delegate for updating the RTB.
        private delegate void processOnMainThread3(int i, string str, string str2, List<Process> pl, int number); // Delegate type3.
        private processOnMainThread3 _AddLogsGridViewDel;                                                         // Delegate for adding the logs grid view.
        private delegate void ProcessOnMainThread4(int i, string str);                                            // Delegate type4.
        private ProcessOnMainThread4 _DisplayConfigProcessTimeDel;                                                // Delegate for displaying the process time.
        private delegate void ProcessOnMainThread5(int i);                                                        // Delegate type5.
        private ProcessOnMainThread5 _AbortThreadDel;                                                             // Delegate for aborting the specific thread.

        #endregion

        /*********************************************************** CONSTRUCTOR & INITIALIZATION *************************************************************/

        #region CONSTRUCTOR & INITIALIZATION

        public Main()
        {
            InitializeComponent();

            _FileBrowser = new FileBrowser(InitializePath());
            if (Directory.Exists(Properties.Settings.Default.local_path))
                _LocalFileBrowser = new FileBrowser(Properties.Settings.Default.local_path);
            else
                _LocalFileBrowser = new FileBrowser(@"D:\\");
            _Navigator = new Navigator(ProcessHelperButton);
            _SpecificCountries = new SpecificCountries();
            _SpecificTools = new SpecificTools();
            _Session = new Authentication("user");
            _Session.CheckSavedPUC(System.Environment.MachineName);
            _Administration = new Administration();
            _Settings = new Settings();
            _Chronicles = new History();
            _ConfigSummary = new ConfigSummary();
            _Batch = new BatchUC();
            LoadBatchs();        // Load batch objects from saved instance.
            _Batch.LoadBatchs(); // Load them graphically (rows in DGV).
            _WaitingScreen = new WaitingScreen();
            _FtpManager = new FTPManager.FTPManager();

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
            _DisplayConfigProcessTimeDel = new ProcessOnMainThread4(DisplayConfigProcessTime);
            _AbortThreadDel = new ProcessOnMainThread5(AbortThread);
        }

        /******************************************\
         * Initialize interface :                 *
         *  - Initialize Administration Form      *
         *  - Initialize Settings Form            *
         *  - Add UC File Browser (both).         *
         *  - Add UC Navigator.                   *
         *  - Add UC Specifics tools & countries. *
         *  - Initialize launch button.           *
         *  - Define event Handlers.              *
        \******************************************/

        private void InitializeInterface()
        {
            // Administration form.
            _Session.Dock = DockStyle.Fill;
            _AdministrationForm = new KryptonForm();
            _AdministrationForm.Text = "Administration";
            _AdministrationForm.StartPosition = FormStartPosition.CenterScreen;
            _AdministrationForm.Icon = global::Analytics_V2.Properties.Resources.Administration;
            _AdministrationForm.Size = new System.Drawing.Size(600, 400);
            _AdministrationForm.Controls.Add(_Administration);
            _AdministrationForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HideAdministrationForm);

            // Settings form
            _Settings.Dock = DockStyle.Fill;
            _SettingsForm = new KryptonForm();
            _SettingsForm.Text = "Settings";
            _SettingsForm.StartPosition = FormStartPosition.CenterScreen;
            _SettingsForm.Icon = global::Analytics_V2.Properties.Resources.Settings;
            _SettingsForm.Size = new System.Drawing.Size(680, 320);
            _SettingsForm.Controls.Add(_Settings);
            _SettingsForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HideSettingsForm);

            // Chronicles form
            _HistoryAndSummaryForm = new KryptonForm();
            _HistoryAndSummaryForm.Text = "History";
            _HistoryAndSummaryForm.Icon = global::Analytics_V2.Properties.Resources.TimeMachine2;
            _HistoryAndSummaryForm.StartPosition = FormStartPosition.CenterScreen;
            _HistoryAndSummaryForm.Size = new Size(1400, 700);
            _HistoryAndSummaryForm.Controls.Add(_Chronicles);
            _HistoryAndSummaryForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HideChroniclesForm);

            // WaitingScreen form
            _WaitScreenForm = new KryptonForm();
            _WaitScreenForm.Size = new Size(380, 82);
            _WaitScreenForm.StartPosition = FormStartPosition.CenterScreen;
            _WaitScreenForm.Controls.Add(_WaitingScreen);
            _WaitScreenForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            // Batch form
            _BatchForm = new KryptonForm();
            _BatchForm.Text = "Batch Administration";
            _BatchForm.Size = new System.Drawing.Size(550, 550);
            _BatchForm.StartPosition = FormStartPosition.CenterScreen;
            _BatchForm.Icon = global::Analytics_V2.Properties.Resources.Batch2;
            _BatchForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HideBatchForm);
            _BatchForm.Controls.Add(_Batch);
            _BatchForm.TopMost = true;
            // Build batch listbox
            foreach (Batch element in _Batch.Get_BatchsList())
                BatchListBox.Items.Add(element.Get_Name());
            BatchListBox.BackColor = Color.FromArgb(227, 230, 232);

            // FTP Manager form
            _FtpManagerForm = new KryptonForm();
            _FtpManagerForm.Text = "Ftp Manager";
            _FtpManagerForm.Size = new System.Drawing.Size(1200, 800);
            _FtpManagerForm.StartPosition = FormStartPosition.CenterScreen;
            _FtpManagerForm.Icon = global::Analytics_V2.Properties.Resources.FTP3;
            _FtpManagerForm.FormClosing += HideFtpForm;
            _FtpManager.Dock = DockStyle.Fill;
            _FtpManagerForm.Controls.Add(_FtpManager);


            // Add file browsers (local + common)
            this.FileBrowserNavigator.Pages[0].Controls.Add(_FileBrowser);
            this.FileBrowserNavigator.Pages[0].Tag = _FileBrowser;
            _FileBrowser.PopulateTreeView();
            this.FileBrowserNavigator.Pages[1].Controls.Add(_LocalFileBrowser);
            this.FileBrowserNavigator.Pages[1].Tag = _LocalFileBrowser;
            _LocalFileBrowser.PopulateTreeView();

            // Add Navigator / Specific tools
            this.MainBoardSplitContainer5.Panel1.Controls.Add(_Navigator);
            this.MainBoardSplitContainer4.Panel1.Controls.Add(_SpecificCountries);
            this.MainBoardSplitContainer4.Panel2.Controls.Add(_SpecificTools);

            MainBoardSplitContainer1.FixedPanel = FixedPanel.Panel1;

            // Event handlers
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
            _FileBrowser.ViewHistoryToolStripMenuItem.Click += new System.EventHandler(this.ViewHistoryToolStripMenuItem_Click);
            _FileBrowser.OpenDirectoryToolStripMenuItem.Click += OpenDirectoryToolStripMenuItem_Click;

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
            _LocalFileBrowser.ViewHistoryToolStripMenuItem.Click += new System.EventHandler(this.ViewHistoryToolStripMenuItem_Click);
            _LocalFileBrowser.OpenDirectoryToolStripMenuItem.Click += OpenDirectoryToolStripMenuItem_Click;

            _Navigator.NavigatorControl.TabClicked += NavigatorControl_TabClick;
            _Navigator.NavigatorControl.SelectedPageChanged += NavigatorControl_SelectedPageChanged;
            _SpecificCountries.SpecificCountriesListBox.ListBox.DoubleClick += new System.EventHandler(SpecificCountriesListBox_DoubleClick);
            _SpecificTools.SpecificToolsListBox.ListBox.DoubleClick += new System.EventHandler(SpecificToolsListBox_DoubleClick);
            _Session.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            _Session.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            _Session.PasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(PasswordTextBox_KeyDown);

            // Set connection status
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

            catch
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

        /******************************************************************************************************************************************************\
         *                                                                                                                                                    *
         *                                                                  - METHODS -                                                                       *
         *                                                                                                                                                    *
        \******************************************************************************************************************************************************/     

        /**********************************************************    FILE EXPLORER TREEVIEW   ***************************************************************/

        #region FILE EXPLORER TREEVIEW

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
                        LaunchButton.Enabled = true;

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
                            element.ButtonSpecs[0].Click += new EventHandler(SummaryExpandMinimizeButton_Click);
                    }
                    catch(Exception exception)
                    {
                        ok.Close();

                        // Disable buttons
                        SuppressToolStripButton.Enabled = true;
                        LaunchButton.Enabled = false;

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
                    if(_Navigator.NavigatorControl.SelectedPage.Text.Equals("Summary"))
                        LaunchButton.Enabled = false;

                    // Display folder name.
                    _Navigator.DisplayFolderName(treeView.SelectedNode.Text);
                }

                else
                {
                    // Disable buttons
                    EditToolStripButton.Enabled = false;
                    SuppressToolStripButton.Enabled = false;
                    LaunchButton.Enabled = false;
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

        private void SummaryExpandMinimizeButton_Click(object sender, EventArgs e)
        {
            if (sender is ButtonSpecHeaderGroup)
            {
                ButtonSpecHeaderGroup buttonSpecHeaderGroup = sender as ButtonSpecHeaderGroup;
                KryptonHeaderGroup headerGroup = buttonSpecHeaderGroup.Tag as KryptonHeaderGroup;

                if (buttonSpecHeaderGroup.Type == ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonMinimize)
                {
                    buttonSpecHeaderGroup.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonExpand;
                    headerGroup.Size = new System.Drawing.Size(150, 23);
                }
                else if (buttonSpecHeaderGroup.Type == ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonExpand)
                {
                    buttonSpecHeaderGroup.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonMinimize;
                    headerGroup.Size = new System.Drawing.Size(150, 95);
                }
            }
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

            _FileBrowser.PasteToolStripMenuItem.Enabled = true;
            _LocalFileBrowser.PasteToolStripMenuItem.Enabled = true;
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
            _FileBrowser.PasteToolStripMenuItem.Enabled = true;
            _LocalFileBrowser.PasteToolStripMenuItem.Enabled = true;
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

            if(_IsCopy)
                File.Copy(_SourcePath, _TargetPath,true);
            else if (!_IsCopy)
            {
                File.Move(_SourcePath, _TargetPath);

                // Check if config in db exists, else add config
                try
                {
                    AnalyticsWebService.AnalyticsSoapClient session = new AnalyticsWebService.AnalyticsSoapClient();
                    int idConfig = session.Get_histo_id_config(_SourcePath);
                    if (idConfig == 0)
                        session.Add_histo_config(_PreviousNodeName.Replace(".xml", ""), _SourcePath);
                    session.Close();
                }
                catch { }

                // Update Config path in database
                try
                {
                    AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                    service.Update_histo_config_path(_SourcePath, _TargetPath);
                    service.Close();
                }
                catch { }
            }

            fileBrowser.PopulateTreeView();
            _FileBrowser.PasteToolStripMenuItem.Enabled = false;
            _LocalFileBrowser.PasteToolStripMenuItem.Enabled = false;
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
                        {
                            File.Move(_SourcePath, _TargetPath);

                            // Check if config in db exists, else add config
                            try
                            {
                                AnalyticsWebService.AnalyticsSoapClient session = new AnalyticsWebService.AnalyticsSoapClient();
                                int idConfig = session.Get_histo_id_config(_SourcePath);
                                if (idConfig == 0)
                                    session.Add_histo_config(_PreviousNodeName.Replace(".xml", ""), _SourcePath);
                                session.Close();
                            }
                            catch { }

                            // Update Config path in database
                            try
                            {
                                AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                                service.Update_histo_config_path(_SourcePath, _TargetPath);
                                service.Close();
                            }
                            catch { }
                        }
                        else if (treeView.SelectedNode.ImageIndex == 1)
                            Directory.Move(_SourcePath, _TargetPath.Replace(".xml", ""));
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
            }

            fileBrowser.EditToolStripMenuItem.Enabled = false;
            EditToolStripButton.Enabled = false;
            fileBrowser.PopulateTreeView();
        }

        /******************************************************************************\
         * Events of clicking of the element edit of the MenuStrip                    *
         * (or right click on a tree node --> Edit)                                   *   
         *  - Get encoding.
         *  - Save list of processes before modif.                                    *
         *  - Create a new tab with the content of the xml file (+ its event handler) *
         *  - Open it if it already exists                                            *
         *  - Add contextmenu item.                                                   *
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
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");

            // Retrieve the selected config tab (for the encoding).
            IEnumerable<Config> retrieveConfigQuerry = from item in _ConfigsList
                                                       where item.Get_Path().Equals(treeView.SelectedNode.FullPath)
                                                       select item;

            foreach (Config set in retrieveConfigQuerry)
                iso = Encoding.GetEncoding(set.Get_XmlEncoding());

            // Keep in memory config before modification
            List<string> lines = new List<string>();
            StreamReader reader = new StreamReader(treeView.SelectedNode.FullPath, iso);
            while (reader.Peek() >= 0)
            {
                lines.Add(reader.ReadLine());
            }
            reader.Close();
            _ConfigBeforeModif = lines.ToArray();


            // Open tab if it already exists 
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
                    }
                }

                if (!tabExists)
                {
                    _Navigator.ContextMenuStrip.Items.Add(treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0]);
                    _Navigator.AddTab(treeView.SelectedNode.Text.Split(new string[] { "." }, StringSplitOptions.None)[0], treeView.SelectedNode.FullPath);
                    _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages[_Navigator.NavigatorControl.Pages.Count - 1];
                }
            }
        }

        /**********************************************************************\
         * Event of clicking on the element "Save" of the MenuStrip           * 
         * CREATION MODE :                                                    * 
         *   - Call the Save function                                         * 
         *   - Update config list                                             *
         * NORMAL MODE :                                                      * 
         *   - Retrieve the selected config tab (for the encoding).           *
         *   - Retrieve the encoding.                                         *
         *   - Retrieve the richtextbox text.                                 *
         *   - Do special replacements before saving (function XmlTreatment). *
         *   - Check Validity of the xml.                                     *
         *   - Create the streamwriter and save.                              *
         *   - Propose to save anyway if the xml is not valid.                *
         *   - Edit label and button.                                         *
         *   - Update config list.                                            *
         *                                                                    *
         * - Check config modifications                                       *
        \**********************************************************************/

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            string configPath = "";

            // Retrieve the path
            var childrens = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<XMLLoader.XMLForm>().ToList();
            foreach (var element in childrens)
            {
                configPath = (string)element.Tag;
            }

            // CREATION MODE CASE
            if ((Boolean)_Navigator.NavigatorControl.SelectedPage.Tag)
            {
                var childrens2 = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<XMLLoader.XMLForm>().ToList();
                foreach (XMLLoader.XMLForm element in childrens2)
                {
                    element.saveXML((string)element.Tag);

                    var result = KryptonMessageBox.Show("Saved in :\n" + element.Tag.ToString(), "File saved.",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information);

                    // Update the config list.
                    _ConfigsList.Clear();
                    _ConfigsList.Add(new Config(_Navigator.NavigatorControl.SelectedPage.Text.Replace(".xml",""),element.Tag.ToString()));
                }             
            }

            // XML MODE CASE
            else
            {
                Encoding encoding;
                String treatedString;
                StreamWriter writer;

                // Retrieve the path
                var childrens3 = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<KryptonRichTextBox>().ToList();
                foreach (var richTextBox in childrens3)
                {
                    configPath = (string)richTextBox.Tag;
                }

                // Retrieve the selected config (for the encoding).
                IEnumerable<Config> retrieveConfigQuerry = from item in _ConfigsList
                                                           where item.Get_Path().Equals(configPath)
                                                           select item;

            
                // Retrieve the encoding.
                foreach (Config set in retrieveConfigQuerry)
                {
                    encoding = Encoding.GetEncoding(set.Get_XmlEncoding());
            
                    // Retrieve the richtextbox text
                    var childrens2 = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<KryptonRichTextBox>().ToList();
                    foreach (var richTextBox in childrens2)
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
            
                            var result = KryptonMessageBox.Show("Saved in :\n" + richTextBox.Tag.ToString(), "File saved.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        }
            
                        catch (Exception ex)
                        {
                            ok.Close();
            
                            var result2 = KryptonMessageBox.Show("Warning !! There is an error in the xml: \n\n                        " + ex + "\n\nSave anyway?", "invalid XML",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning);
            
                            if (result2 == DialogResult.Yes)
                            {
                                // Create the streamwriter and save anyway.
                                writer = new StreamWriter(richTextBox.Tag.ToString(), false, encoding);
                                writer.WriteLine(treatedString);
                                writer.Close();
                            }
                        }
                    }
                }

                // Update the config list.
                _ConfigsList.Clear();
                _ConfigsList.Add(new Config(_Navigator.NavigatorControl.SelectedPage.Text.Replace(".xml", ""), configPath));
            }

            // Update modifications in DB
            IEnumerable<Config> retrieveConfigQuerry2 = from item in _ConfigsList
                                                       where item.Get_Path().Equals(configPath)
                                                       select item;

            foreach (Config set in retrieveConfigQuerry2)
            {
                set.CheckModification(_ConfigBeforeModif);
            }

            // Update Summary tab (in case of summary tab displays the edited config
            IEnumerable<Config> retrieveConfigQuerry3 = from item in _ConfigsList
                                                        //where item.Get_Path().Equals(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.FullPath)
                                                        where item.Get_Path().Equals(configPath)
                                                        select item;
            
            foreach (Config set in retrieveConfigQuerry3)
                _Navigator.DisplayConfigSummary(set.Get_Name(), set.Get_ProcessList(), set.Get_Warning());

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
                {
                    File.Move(_SourcePath, _TargetPath);

                    // Check if config in db exists, else add config
                    try
                    {
                        AnalyticsWebService.AnalyticsSoapClient session = new AnalyticsWebService.AnalyticsSoapClient();
                        int idConfig = session.Get_histo_id_config(_SourcePath);
                        if (idConfig == 0)
                        {
                            string[] splitResult = _SourcePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                            session.Add_histo_config(splitResult[splitResult.Length-1].Replace(".xml",""), _SourcePath);
                        }
                        session.Close();
                    }
                    catch { }

                    // Update Config name in database
                    try
                    {
                        string[] splitResult = _SourcePath.Split(new string[] { "\\" }, StringSplitOptions.None);
                        string[] splitResult2 = _TargetPath.Split(new string[] { "\\" }, StringSplitOptions.None);
                        AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                        service.Update_histo_config_name(splitResult[splitResult.Length - 1].Replace(".xml", ""), splitResult2[splitResult.Length - 1].Replace(".xml", ""));
                        service.Close();
                    }
                    catch { }

                    // Consequently update Config path in database
                    try
                    {
                        AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                        service.Update_histo_config_path(_SourcePath,_TargetPath);
                        service.Close();
                    }
                    catch { }
                }
                else if (treeView.SelectedNode != null && treeView.SelectedNode.ImageIndex == 1)
                    Directory.Move(_SourcePath, _TargetPath);
            }
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

                    AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                    service.Delete_histo_configuration(treeView.SelectedNode.FullPath);
                    service.Close();

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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers XML | *.xml";
            saveFileDialog.AddExtension = true;
            if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode != null)
            {
                if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ImageIndex == 2)
                    saveFileDialog.InitialDirectory = ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.Parent.FullPath;
                else if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ImageIndex == 1)
                    saveFileDialog.InitialDirectory = ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.FullPath;
            }
            else saveFileDialog.InitialDirectory = ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.Nodes[0].FullPath;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader streamReader = new StreamReader(Properties.Settings.Default.xml_template, System.Text.Encoding.Default);
                    string text = streamReader.ReadToEnd();
                    streamReader.Close();
                    streamReader.Dispose();

                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.Default);
                    streamWriter.WriteLine(text);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }

                // If no connection, create a virgin xml
                catch 
                {
                    var result = KryptonMessageBox.Show("Unable to access file (maybe there is no network). An empty XML file will be created instead.", "No network",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.Default);
                    streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"iso-8859-1\" standalone=\"yes\"?>");
                    streamWriter.WriteLine("<region>");
                    streamWriter.WriteLine("</region>");
                    streamWriter.Close();
                    streamWriter.Dispose();
                }

                var result2 = KryptonMessageBox.Show("File Created in :\n" + saveFileDialog.FileName, "File Created.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                // Refresh TreeView
                ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).PopulateTreeView();
                ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).ScrollToCreatedItem(saveFileDialog.FileName);

                // Add Page to navigator
                _Navigator.ContextMenuStrip.Items.Add(System.IO.Path.GetFileName(saveFileDialog.FileName).Replace(".xml",""));
                _Navigator.AddTab(System.IO.Path.GetFileName(saveFileDialog.FileName).Replace(".xml", ""), saveFileDialog.FileName);
                _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages[_Navigator.NavigatorControl.Pages.Count - 1];
            }
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


        /*******************************************************************************\
         * Event which occurs when the OpenDirectory contextMenuStrip is clicked       *
         *  - Open the directory in the File Explorer                                  *
        \*******************************************************************************/

        private void OpenDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);

            if (treeView.SelectedNode.ImageIndex == 1)
            {
                try
                {
                    System.Diagnostics.Process.Start("explorer.exe", treeView.SelectedNode.FullPath);
                }
                catch (Exception ex)
                {
                    var result = KryptonMessageBox.Show("Cannot open File Explorer. Error:\n\n" + ex, "Error while opening File Explorer",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Exclamation);
                };
            }

            else if (treeView.SelectedNode.ImageIndex == 2)
            {
                try
                {
                    System.Diagnostics.Process.Start("explorer.exe", treeView.SelectedNode.Parent.FullPath);
                }
                catch (Exception ex)
                {
                    var result = KryptonMessageBox.Show("Cannot open File Explorer. Error:\n\n" + ex, "Error while opening File Explorer",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        /*****************************************************************    LAUNCHER   **********************************************************************/

        #region LAUNCHER

        /*********************************************************************************\
         * Event of clicking of the element Launch of the MenuStrip                      *
         * (or double click on the treenode).                                            *
         *   - Define if Case 1 or 2                                                     *
         *   - Define paths (input files, output files, logs).                           *
         *   - After validation, put selection page to Summary.                          *
         *   - Retrieve the selected config.                                             *
         *   - Create a launcher and associated UC, and add them to the main controls.   *
         *        - New Launcher.                                                        *
         *        - New Progress Bar.                                                    *
         *        - New Log.                                                             *
         *   - Launch thread.                                                            *
         *   - *INCLUDED* the delegate for updating Log & progress UC during the thread. *
        \*********************************************************************************/

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            if (!FileBrowserNavigator.SelectedPage.Text.Equals("Batchs"))
            {
                // CASE 1 : Selected navigator tab is Summary --> Launch selected TreeNode config.
                if (_Navigator.NavigatorControl.SelectedPage == _Navigator.NavigatorControl.Pages["Summary"])
                {
                    if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.FullPath.Contains("Recettes"))
                    {
                        if (_Session.CheckIfAccessGranted("launchConfigPreProd"))
                            LaunchConfigSelectedTreeNode(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
                    }

                    else
                    {
                        LaunchConfigSelectedTreeNode(((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView);
                    }
                }

                // CASE 2 : Selected navigator tab is not summary --> Launch selected Navigator tab config.
                else
                {
                    if ((Boolean)_Navigator.NavigatorControl.SelectedPage.Tag)
                    {
                        // Get path of config (selected navigator tab)
                        string path = "";
                        var childrens = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<XMLLoader.XMLForm>().ToList();
                        foreach (XMLLoader.XMLForm element in childrens)
                        {
                            path = (string)element.Tag;
                        }
                        // Check access.
                        if (path.Contains("Recettes"))
                        {
                            if (_Session.CheckIfAccessGranted("launchConfigPreProd"))
                                LaunchConfigSelectedNavigatorTab(path);
                        }
                        else LaunchConfigSelectedNavigatorTab(path);
                    }
                    else if (!(Boolean)_Navigator.NavigatorControl.SelectedPage.Tag)
                    {
                        // Get path of config (selected navigator tab)
                        string path = "";
                        var childrens = _Navigator.NavigatorControl.SelectedPage.Controls.OfType<KryptonRichTextBox>().ToList();
                        foreach (KryptonRichTextBox element in childrens)
                        {
                            path = (string)element.Tag;
                        }
                        // Check access.
                        if (path.Contains("Recettes"))
                        {
                            if (_Session.CheckIfAccessGranted("launchConfigPreProd"))
                                LaunchConfigSelectedNavigatorTab(path);
                        }
                        else LaunchConfigSelectedNavigatorTab(path);
                    }
                }
            }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (((TreeView)(sender)).SelectedNode.ImageIndex == 2)
            {
                if (((TreeView)(sender)).SelectedNode.FullPath.Contains("Recettes"))
                {
                    if(_Session.CheckIfAccessGranted("launchConfigPreProd"))
                        LaunchConfigSelectedTreeNode(((TreeView)(sender)));
                }

                else
                    LaunchConfigSelectedTreeNode(((TreeView)(sender)));
            }
        }

        private void LaunchConfigSelectedTreeNode(TreeView treeView)
        {         
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "CONFIG : " + treeView.SelectedNode.Text;
            openFileDialog.InitialDirectory = @"D:\";
            openFileDialog.Multiselect = true;
            // openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Put selection page to Summary. 
                _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages["Summary"];

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
                    _LaunchersList.Add(new Launcher(set.Clone(), PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked, HCButton.Checked, new List<String>(_InputFiles), _LogsPath, _UpdateProgressBarDel, _UpdateRichTextBoxDel, _AddLogsGridViewDel, _DisplayConfigProcessTimeDel));

                    _ProgressBarsList.Add(new ProgressBar(set.Get_Name(),_ProgressBarsList.Count,_AbortThreadDel)); // Create a new ProgressBar Control & add it to the list of PB
                    _Navigator.ProgressGroupBox.Panel.Controls.Add(_ProgressBarsList[_ProgressBarsList.Count - 1]); // Add the created PB in the panel.
                    _LogsList.Add(new Log(set.Get_Name(), set.Get_TargetsNumber(), PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked)); // Create a new Log Control & add it to the list of Logs.
                    _Navigator.LogsNavigator.Pages.Insert(0, _LogsList[_LogsList.Count - 1].Get_NavigatorTab()); // Add the log tab to the logsNavigator.
                    _Navigator.LogsNavigator.SelectedIndex = 0;
                    _LogsList[_LogsList.Count - 1].Get_NavigatorTab().ButtonSpecs[0].Click += new EventHandler(_Navigator.CloseLogsNavigatorTab);

                    _PoolThreads.Add(new Thread(() => _LaunchersList[_LaunchersList.Count - 1].Run(_LaunchersList.Count - 1)));
                    _PoolThreads[_PoolThreads.Count - 1].IsBackground = true;
                    _PoolThreads[_PoolThreads.Count - 1].Start();
                }
            }
        }

        private void LaunchConfigSelectedNavigatorTab(string configPath)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "CONFIG : " + _Navigator.NavigatorControl.SelectedPage.Text;
            openFileDialog.InitialDirectory = @"D:\";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Put selection page to Summary. 
                _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages["Summary"];

                // Define paths (input, output, logs)
                _InputFiles.Clear();
                foreach (String inputFile in openFileDialog.FileNames)
                {
                    _InputFiles.Add(inputFile);
                    _LogsPath = Path.GetDirectoryName(inputFile);
                }

                // Retrieve the selected config.
                IEnumerable<Config> retrieveConfigQuerry = from item in _ConfigsList
                                                           where item.Get_Path().Equals(configPath)
                                                           select item;

                // Create a launcher and associated UC.
                foreach (Config set in retrieveConfigQuerry)
                {
                    _LaunchersList.Add(new Launcher(set.Clone(), PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked, HCButton.Checked, new List<String>(_InputFiles), _LogsPath, _UpdateProgressBarDel, _UpdateRichTextBoxDel, _AddLogsGridViewDel, _DisplayConfigProcessTimeDel));

                    _ProgressBarsList.Add(new ProgressBar(set.Get_Name(),_ProgressBarsList.Count,_AbortThreadDel)); // Create a new ProgressBar Control & add it to the list of PB
                    _Navigator.ProgressGroupBox.Panel.Controls.Add(_ProgressBarsList[_ProgressBarsList.Count - 1]); // Add the created PB in the panel.
                    _LogsList.Add(new Log(set.Get_Name(), set.Get_TargetsNumber(), PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked)); // Create a new Log Control & add it to the list of Logs.
                    _Navigator.LogsNavigator.Pages.Insert(0, _LogsList[_LogsList.Count - 1].Get_NavigatorTab()); // Add the log tab to the logsNavigator.
                    _Navigator.LogsNavigator.SelectedIndex = 0;
                    _LogsList[_LogsList.Count - 1].Get_NavigatorTab().ButtonSpecs[0].Click += new EventHandler(_Navigator.CloseLogsNavigatorTab);

                    _PoolThreads.Add(new Thread(() => _LaunchersList[_LaunchersList.Count - 1].Run(_LaunchersList.Count - 1)));
                    _PoolThreads[_PoolThreads.Count - 1].IsBackground = true;
                    _PoolThreads[_PoolThreads.Count - 1].Start();
                }
            }
        }

        // Abort Thread (button stop) (delegate function)
        private void AbortThread(int id)
        {
            _PoolThreads[id].Abort();
            _ProgressBarsList[id].Visible = false;
            _LogsList[id].Get_NavigatorTab().Visible = false;
        }

        // Update Progress bar (delegate function)
        private void UpdateProgressBar(int[] idAndProgress) // 0 is the ID of the running process and 1 is the progress.
        {
            Invoke(_ProgressBarsList[idAndProgress[0]].Get_UpdateProgressBarDel(), idAndProgress[1]);
        }

        // Display Config Process Time (delegate function)
        private void DisplayConfigProcessTime(int id, string time)
        {
            Invoke(_ProgressBarsList[id].Get_DisplayConfigTimeDel(), time);
        }

        // Update RTB (delegate function)
        private void UpdateRichTextBox(String[] idTypeAndMessage) // 0 is the ID of the running process  1 is the type, and 2 is the message.
        {
            Invoke(_ProgressBarsList[int.Parse(idTypeAndMessage[0])].Get_UpdateRichTextBoxDel(), idTypeAndMessage[1], idTypeAndMessage[2]);
        }

        // Add LogsGridView (delegate function)
        private void AddLogsGridView(int id, string outputFile, string inputFile, List<Process> processList, int targetsNumber)
        {
            Invoke(_LogsList[id].Get_AddLogsGridViewDel(), outputFile, inputFile, processList, targetsNumber);
        }

        #endregion

        /**************************************************    ADMINISTRATION, LOGIN & SETTINGS MODULE   ******************************************************/

        #region ADMINISTRATION, LOGIN & SETTINGS MODULE

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
            if (e.KeyCode.Equals(Keys.Enter))
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

        #endregion

        /***************************************************************    BATCH MODULE   ********************************************************************/

        #region BATCH MODULE

        /*********************\
         * Save /Load batchs *
        \*********************/

        private void SaveBatchs()
        {
            if (!Directory.Exists(@"D:\Documents\Analytics"))
                Directory.CreateDirectory(@"D:\Documents\Analytics");
            Stream stream = File.Create(@"D:\Documents\Analytics\Batchs");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, _Batch.Get_BatchsList());
            stream.Close();
            stream.Dispose();
        }

        private void LoadBatchs()
        {
            if (File.Exists(@"D:\Documents\Analytics\Batchs"))
            {
                Stream stream = File.OpenRead(@"D:\Documents\Analytics\Batchs");
                BinaryFormatter deserializer = new BinaryFormatter();
                _Batch.Set_BatchsList((List<Batch>)deserializer.Deserialize(stream));
                stream.Close();
            }
        }

        /*********************************************\
         * Event of clicking batch listbox item :    *
         *    -> Display Batch summary               *
        \*********************************************/

        private void BatchListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display Summary page
            _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages[0];

            // Retrieve batch to summarize
            foreach (Batch element in _Batch.Get_BatchsList())
            {
                if (element.Get_Name().Equals(BatchListBox.SelectedItem.ToString()))
                {
                    _Navigator.DisplayBatchSummary(element);

                    // Event handler for the expand/minimize arrow
                    foreach (KryptonHeaderGroup element2 in _Navigator.Get_ProcessHeaderGroupList())
                        element2.ButtonSpecs[0].Click += new EventHandler(SummaryExpandMinimizeButton_Click);
                    break;
                }
            }
        }

        /****************************************************\
         * Event of double clicking batch listbox item :    *
         *    -> LAUNCH selected batch                      *
        \****************************************************/

        private void BatchListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Display Summary page
            _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages[0];

            // Retrieve batch to launch
            foreach (Batch element in _Batch.Get_BatchsList())
            {
                if (element.Get_Name().Equals(BatchListBox.SelectedItem.ToString()))
                {
                    // Launch batch, depending on if it's a single or multi batch
                    if (element.Get_Type().Equals("Single"))
                        LaunchBatchSingle(element);
                    else if (element.Get_Type().Equals("Multi"))
                        LaunchBatchMulti(element);
                    break;
                }
            }
        }

        private void LaunchBatchSingle(Batch batch)
        {
            foreach (KeyValuePair<string, Tuple<string, string>> element in batch.Get_BatchElements())
            {
                // Define paths (input, logs)
                DirectoryInfo targetPath = new DirectoryInfo(element.Key);
                FileInfo[] inputFiles = targetPath.GetFiles();

                _InputFiles.Clear();
                foreach (FileInfo file in inputFiles)
                {
                    _InputFiles.Add(file.FullName);
                    _LogsPath = file.Directory.FullName;
                }

                // Create a new config
                Config config = new Config(element.Value.Item1, element.Value.Item2);

                // Create launcher and associated UC.
                _LaunchersList.Add(new Launcher(config, PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked, HCButton.Checked, new List<String>(_InputFiles), _LogsPath, _UpdateProgressBarDel, _UpdateRichTextBoxDel, _AddLogsGridViewDel, _DisplayConfigProcessTimeDel));

                _ProgressBarsList.Add(new ProgressBar(config.Get_Name(), _ProgressBarsList.Count, _AbortThreadDel));
                _Navigator.ProgressGroupBox.Panel.Controls.Add(_ProgressBarsList[_ProgressBarsList.Count - 1]);
                _LogsList.Add(new Log(config.Get_Name(), config.Get_TargetsNumber(), PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked));
                _Navigator.LogsNavigator.Pages.Insert(0, _LogsList[_LogsList.Count - 1].Get_NavigatorTab());
                _Navigator.LogsNavigator.SelectedIndex = 0;
                _LogsList[_LogsList.Count - 1].Get_NavigatorTab().ButtonSpecs[0].Click += new EventHandler(_Navigator.CloseLogsNavigatorTab);

                _PoolThreads.Add(new Thread(() => _LaunchersList[_LaunchersList.Count - 1].Run(_LaunchersList.Count - 1)));
                _PoolThreads[_PoolThreads.Count - 1].IsBackground = true;
                _PoolThreads[_PoolThreads.Count - 1].Start();
            }
        }

        private void LaunchBatchMulti(Batch batch)
        {
            foreach (KeyValuePair<string, List<Tuple<string, string>>> element in batch.Get_BatchElementsMulti())
            {
                int configCounter = 1;

                // Define paths (input, logs)
                DirectoryInfo targetPath = new DirectoryInfo(element.Key);
                FileInfo[] inputFiles = targetPath.GetFiles();

                _InputFiles.Clear();
                foreach (FileInfo file in inputFiles)
                    _InputFiles.Add(file.FullName);

                // Create Config/Instanciate launcher for each config of each batch element
                foreach (Tuple<string, string> config in element.Value)
                {
                    // Copy all inputs in a separated directory
                    if (!Directory.Exists(targetPath + "\\" + configCounter.ToString()))
                        Directory.CreateDirectory(targetPath + "\\" + configCounter.ToString());
                    foreach (String input in _InputFiles)
                        File.Copy(input, targetPath + "\\" + configCounter.ToString() + "\\" + Path.GetFileName(input), true);

                    // Redefine paths
                    DirectoryInfo specificTargetPath = new DirectoryInfo(targetPath + "\\" + configCounter.ToString());
                    FileInfo[] specificInputFiles = specificTargetPath.GetFiles();
                    List<string> specificInputs = new List<string>();
                    string specificLogPath = specificTargetPath.FullName;

                    foreach (FileInfo file in specificInputFiles)
                        specificInputs.Add(file.FullName);

                    // Create a new config
                    Config specificConfig = new Config(config.Item1, config.Item2);

                    // Create launcher and associated UC.
                    _LaunchersList.Add(new Launcher(specificConfig, PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked, HCButton.Checked, new List<String>(specificInputs), specificLogPath, _UpdateProgressBarDel, _UpdateRichTextBoxDel, _AddLogsGridViewDel, _DisplayConfigProcessTimeDel));

                    _ProgressBarsList.Add(new ProgressBar(specificConfig.Get_Name(), _ProgressBarsList.Count, _AbortThreadDel));
                    _Navigator.ProgressGroupBox.Panel.Controls.Add(_ProgressBarsList[_ProgressBarsList.Count - 1]);
                    _LogsList.Add(new Log(specificConfig.Get_Name(), specificConfig.Get_TargetsNumber(), PreProcessButton.Checked, ProcessButton.Checked, ControlsButton.Checked));
                    _Navigator.LogsNavigator.Pages.Insert(0, _LogsList[_LogsList.Count - 1].Get_NavigatorTab());
                    _Navigator.LogsNavigator.SelectedIndex = 0;
                    _LogsList[_LogsList.Count - 1].Get_NavigatorTab().ButtonSpecs[0].Click += new EventHandler(_Navigator.CloseLogsNavigatorTab);

                    _PoolThreads.Add(new Thread(() => _LaunchersList[_LaunchersList.Count - 1].Run(_LaunchersList.Count - 1)));
                    _PoolThreads[_PoolThreads.Count - 1].IsBackground = true;
                    _PoolThreads[_PoolThreads.Count - 1].Start();

                    configCounter++;
                }


            }
        }


        /****************************************************************\
         * Event which occurs when the Batch toolstrip is clicked       *
         *  - Open the batch module                                     *
        \****************************************************************/

        private void BatchToolStripButton_Click(object sender, EventArgs e)
        {
            _BatchForm.Show();
        }

        private void HideBatchForm(object sender, FormClosingEventArgs e)
        {
            _Batch.SplitContainer.Panel2Collapsed = true;
            _Batch.SplitContainer.Panel2.Hide();
            _BatchForm.Size = new System.Drawing.Size(600, 400);
            _Batch.RemoveToolStripButton.Enabled = true;
            _Batch.AddToolStripButton.Enabled = true;
            _Batch.EditToolStripButton.Enabled = true;

            _BatchForm.Hide();
            e.Cancel = true;

            // rebuild batchs listbox
            BatchListBox.Items.Clear();
            foreach (Batch element in _Batch.Get_BatchsList())
                BatchListBox.Items.Add(element.Get_Name());
        }

        #endregion

        /*******************************************************    CONFIGURATION SUMMARY MODULE   ************************************************************/

        #region CONFIG SUMMARY MODULE

        /*********************************************************************\
         * Event which occurs when the ConfigStatement toolstrip is clicked  *
         *  - Open the Config Statement summary (old "SUIVI PAYS ANALYTICS). *
         *  - Also show & hide the waiting screen.                           *
        \*********************************************************************/

        private void ConfigStatementToolStripButton_Click(object sender, EventArgs e)
        {
            if (_Session.GetNetworkAvailable())
            {
                _WaitScreenForm.Show();

                _HistoryAndSummaryForm.Text = "Configs Summary";
                _HistoryAndSummaryForm.Icon = global::Analytics_V2.Properties.Resources.ConfigStatement2;
                _HistoryAndSummaryForm.Controls.Clear();
                _HistoryAndSummaryForm.Controls.Add(_ConfigSummary);
                _ConfigSummary.FillDataGridView();

                _WaitScreenForm.Hide();

                _HistoryAndSummaryForm.Show();
            }
        }

        #endregion

        /***************************************************************    FTP MODULE   **********************************************************************/

        #region FTP MODULE

        /*************************************************************\
         * Event which occurs when the Batch toolstrip is clicked    *
         *  - Open the batch module                                  *
         * Include hiding the module                                 *
        \*************************************************************/

        private void FTPToolStripButton_Click(object sender, EventArgs e)
        {
            _FtpManagerForm.Show();
        }

        private void HideFtpForm(object sender, FormClosingEventArgs e)
        {
            _FtpManagerForm.Hide();
            _FtpManagerForm.Controls.Clear();
            _FtpManager.Dispose();
            _FtpManager = new FTPManager.FTPManager();
            _FtpManager.Dock = DockStyle.Fill;
            _FtpManagerForm.Controls.Add(_FtpManager);

            e.Cancel = true;
        }

        #endregion

        /********************************************************    HEADER CONSISTENCY MODULE   **************************************************************/

        #region HEADER CONSISTENCY MODULE

        /*************************************************************\
         * Event which occurs when the HC toolstrip is clicked       *
         *  - Open the HC Module                                     *
        \*************************************************************/

        private void HCToolStripButton_Click(object sender, EventArgs e)
        {
            if (_Session.CheckIfAccessGranted("hc"))
            {
                try
                {
                    HeaderConsistency.HC HC = new HeaderConsistency.HC(Properties.Settings.Default.hc_config);
                    HC.Show();
                }
                catch (Exception ex)
                {
                    var result = KryptonMessageBox.Show("Cannot open the module (check if path of HC is well defined)." + ex, "Error while opening HC Module",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        /**************************************************************    HISTORY MODULE   *******************************************************************/

        #region HISTORY MODULE

        /**************************************************************************************************************************\
         * Event which occurs when the historic contextMenuStrip item / History toolstrip (whole history in this case) is clicked *
         *  - Open the config historic.                                                                                           *
         *  - Also the handler when closing form.                                                                                 *
        \**************************************************************************************************************************/

        private void HistoryToolStripButton_Click(object sender, EventArgs e)
        {
            if (_Session.GetNetworkAvailable())
            {
                _HistoryAndSummaryForm.Text = "History";
                _HistoryAndSummaryForm.Icon = global::Analytics_V2.Properties.Resources.TimeMachine2;
                _HistoryAndSummaryForm.Controls.Clear();
                _Chronicles.GetAllChronicles();
                _HistoryAndSummaryForm.Controls.Add(_Chronicles);
                _HistoryAndSummaryForm.Show();
            }
        }

        private void ViewHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = ((TreeView)((ContextMenuStrip)(((ToolStripMenuItem)(sender)).Owner)).SourceControl);
            if (_Session.GetNetworkAvailable())
            {
                _HistoryAndSummaryForm.Text = "History";
                _HistoryAndSummaryForm.Icon = global::Analytics_V2.Properties.Resources.TimeMachine2;
                _HistoryAndSummaryForm.Controls.Clear();
                _Chronicles.GetChroniclesFromSpecificConfig(treeView.SelectedNode.Text.Replace(".xml", ""));
                _HistoryAndSummaryForm.Controls.Add(_Chronicles);
                _HistoryAndSummaryForm.Show();
            }
        }

        private void HideChroniclesForm(object sender, FormClosingEventArgs e)
        {
            _HistoryAndSummaryForm.Hide();
            e.Cancel = true;
        }

        #endregion

        /************************************************************    STATISTICS MODULE   ******************************************************************/

        #region STATISTICS MODULE

        /*********************************************************************\
         * Event which occurs when the Statistics toolstrip is clicked       *
         *  - Open the statistics module                                     *
        \*********************************************************************/

        private void StatisticsToolStripButton_Click(object sender, EventArgs e)
        {
            Stats.Form1 statsForm = new Stats.Form1();
            statsForm.StartPosition = FormStartPosition.CenterScreen;
            statsForm.Show();
        }

        #endregion

        /******************************************************************    MISC  **************************************************************************/

        #region MISC

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

        /******************************************************************************************************\
         * Event of resizing the form                                                                         *
         *  - for correcting a graphical bug which occurs when the form is not focused anymore then refocused *
         *       |-> graphical display bug with the progress bars                                             *
         *                                                                                                    *
        \******************************************************************************************************/

        private void Main_Resize(object sender, EventArgs e)
        {
            try
            {
                _Navigator.SummarySplitContainer1.SplitterDistance += 1;
                _Navigator.SummarySplitContainer1.SplitterDistance -= 1;
            }
            catch { }
        
            //switch (this.WindowState)
            //{
            //    case FormWindowState.Maximized:
            //        //MessageBox.Show("J'aggrandis");
            //        break;
            //    case FormWindowState.Minimized:
            //        
            //        break;
            //    case FormWindowState.Normal:
            //        //MessageBox.Show("Retour à la normale");
            //        break;
            //    default:
            //        break;
            //}
        }

        /***************************************************************************\
         * Event of clicking tab on the navigator | when the selected tab changes  *
        \***************************************************************************/

        private void NavigatorControl_TabClick(object sender, KryptonPageEventArgs e)
        {
            if (!FileBrowserNavigator.SelectedPage.Text.Equals("Batchs"))
            {
                if (!_Navigator.NavigatorControl.SelectedPage.Text.Equals("Summary"))
                {
                    LaunchButton.Enabled = true;
                    SaveToolStripButton.Enabled = true;
                    _Navigator.switchButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
                    _Navigator.EnableDisableAllProcessesButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
                    if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode != null && ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ImageIndex == 2)
                    {
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.BackColor = System.Drawing.SystemColors.ControlLight;
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ForeColor = Color.Black;
                    }

                }
                else
                {
                    LaunchButton.Enabled = false;
                    SaveToolStripButton.Enabled = false;
                    _Navigator.switchButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
                    _Navigator.EnableDisableAllProcessesButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
                    if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode != null && ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ImageIndex == 2)
                    {
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.BackColor = Color.FromKnownColor(System.Drawing.KnownColor.Highlight);
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.HighlightText);
                        LaunchButton.Enabled = true;
                    }
                }
            }
        }

        private void NavigatorControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (!FileBrowserNavigator.SelectedPage.Text.Equals("Batchs"))
            {
                if (!_Navigator.NavigatorControl.SelectedPage.Text.Equals("Summary"))
                {
                    LaunchButton.Enabled = true;
                    SaveToolStripButton.Enabled = true;
                    _Navigator.switchButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
                    _Navigator.EnableDisableAllProcessesButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
                    if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode != null && ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ImageIndex == 2)
                    {
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.BackColor = System.Drawing.SystemColors.ControlLight;
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ForeColor = Color.Black;
                    }

                }
                else
                {
                    LaunchButton.Enabled = false;
                    SaveToolStripButton.Enabled = false;
                    _Navigator.switchButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
                    _Navigator.EnableDisableAllProcessesButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
                    if (((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode != null && ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ImageIndex == 2)
                    {
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.BackColor = Color.FromKnownColor(System.Drawing.KnownColor.Highlight);
                        ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).TreeView.SelectedNode.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.HighlightText);
                        LaunchButton.Enabled = true;
                    }
                }
            }
        }

        /*****************************************************\
         * event when fileBrowserNavigator page changes :    *
         *    -> Enable or disable suppress / edit toolstrip *
        \*****************************************************/

        private void FileBrowserNavigator_SelectedPageChanged(object sender, EventArgs e)
        {
            if (FileBrowserNavigator.SelectedPage.Text.Equals("Batchs"))
            {
                SuppressToolStripButton.Enabled = false;
                EditToolStripButton.Enabled = false;
                LaunchButton.Enabled = false;
                _Navigator.NavigatorControl.SelectedPage = _Navigator.NavigatorControl.Pages[0];
            }
            else if (!_Navigator.NavigatorControl.SelectedPage.Text.Equals("Summary"))
            {
                LaunchButton.Enabled = true;
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

        /******************************************************\
         * Method for keeping the nodes expanded when updated *
        \******************************************************/

        private void KeepExpandedNode(object sender, TreeViewCancelEventArgs e)
        {
            if (!FileBrowserNavigator.SelectedPage.Text.Equals("Batchs"))
                ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).AddNodePath(e.Node);
        }

        private void RemoveExpandedNode(object sender, TreeViewCancelEventArgs e)
        {
            if(!FileBrowserNavigator.SelectedPage.Text.Equals("Batchs"))
            ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).RemoveNodePath(e.Node);
        }

        /*************************************************************\
         * Event of clicking of the element refresh of the MenuStrip *
         *  - Refresh the TreeView.                                  *
        \*************************************************************/

        private void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            if(!FileBrowserNavigator.SelectedPage.Text.Equals("Batchs"))
                ((FileBrowser)FileBrowserNavigator.SelectedPage.Tag).PopulateTreeView();
            _ConfigsList.Clear();
        }

        /***********************************************************************\
         * Event of clicking of the element exit of the MenuStrip or the cross *
         *  - Close the form.                                                  *
        \***********************************************************************/

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBatchs();
            Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveBatchs();
            Application.Exit();
        }

        #endregion
     
    }
}

