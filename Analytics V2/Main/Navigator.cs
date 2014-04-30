using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using System.IO;
using System.Xml.Linq;
using System.Xml;


namespace Analytics_V2
{
    public partial class Navigator : UserControl
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private List<KryptonHeaderGroup> _ProcessHeaderGroupList; // List of KryptonHeaderGroup.
        private KryptonGroupBox _WarningGroupBox;                 // Groupbox containing warning.
        private XMLLoader.ButtonHelp _ButtonHelp;                 // Button helm (used when initializing a XMLLoader).
        
        
        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Navigator(XMLLoader.ButtonHelp buttonHelp)
        {
            _ButtonHelp = buttonHelp;

            InitializeComponent();
            _ProcessHeaderGroupList = new List<KryptonHeaderGroup>();
            CreateWarningGroupBox();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            NavigatorControl.Button.ContextButton.Click += ContextButton_Click;
            ContextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
            ContextMenuStrip.Items.Add("Summary");
            switchButtonSpec.Click += switchButtonSpec_Click;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*******************************************************\
         * Create the GroupBox containing the warning message. *
        \*******************************************************/

        public void CreateWarningGroupBox()
        {
            RichTextBox WarningRichTextBox = new RichTextBox();

            _WarningGroupBox = new KryptonGroupBox();
            _WarningGroupBox.Name = "WarningGroupBox";
            _WarningGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            _WarningGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            _WarningGroupBox.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            _WarningGroupBox.Panel.Controls.Add(WarningRichTextBox);
            _WarningGroupBox.Size = new System.Drawing.Size(180, 99);
            _WarningGroupBox.Values.Heading = "/!\\ Warning";
            
            WarningRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            WarningRichTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            WarningRichTextBox.ForeColor = System.Drawing.Color.Red;
            WarningRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            WarningRichTextBox.Location = new System.Drawing.Point(0, 0);
            WarningRichTextBox.Multiline = true;
            WarningRichTextBox.Name = "WarningTextBox";
            WarningRichTextBox.ReadOnly = true;
        }


        /**************************************************************\
         * Display Config Summary                                     *
         *   - The Name of the config (KryptonHeader).                *
         *   - Each process used in the config (KryptonHeaderGroup).  *
         *   - The big play button.                                   *
        \**************************************************************/

        public void DisplayConfigSummary(String name, List<Process>processList, String warning)
        {
            _ProcessHeaderGroupList = new List<KryptonHeaderGroup>();
            this.SummarySplitContainer1.Panel1.Controls.Clear();

            // Display the name of the config
            KryptonHeader header = new KryptonHeader();
            header.Dock = System.Windows.Forms.DockStyle.Top;
            header.Text = name;
            header.Values.Description = null;
            header.Values.Image = null;

            // Display each process
            foreach (Process element in processList)
            {
                KryptonHeaderGroup headerGroup = new KryptonHeaderGroup();
                ButtonSpecHeaderGroup buttonSpecHeaderGroup = new ButtonSpecHeaderGroup();
                headerGroup.Dock = System.Windows.Forms.DockStyle.Top;
                headerGroup.HeaderPositionSecondary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
                headerGroup.ValuesPrimary.Image = null;
                headerGroup.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
                headerGroup.ValuesSecondary.Heading = "Comments";
                headerGroup.Text = element.Get_Name() + " - ID : " + element.Get_OrderId();
                buttonSpecHeaderGroup.Tag = headerGroup;
                headerGroup.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] { buttonSpecHeaderGroup });
                headerGroup.ButtonSpecs[0].Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonExpand;
                headerGroup.Size = new System.Drawing.Size(150, 23);
                
                KryptonRichTextBox richTextBox = new KryptonRichTextBox();
                richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
                richTextBox.ReadOnly = true;
                richTextBox.Text = element.Get_Comment(); 

                headerGroup.Panel.Controls.Add(richTextBox);
                _ProcessHeaderGroupList.Add(headerGroup);
     
            }

            _ProcessHeaderGroupList.Reverse();
            foreach(KryptonHeaderGroup element in _ProcessHeaderGroupList)
                this.SummarySplitContainer1.Panel1.Controls.Add(element);

            var tmp = _WarningGroupBox.Panel.Controls.OfType<RichTextBox>();
            foreach (RichTextBox element in tmp)
            {
                element.Text = warning;
                if (!element.Text.Equals(""))
                    this.SummarySplitContainer1.Panel1.Controls.Add(_WarningGroupBox);
            }

            this.SummarySplitContainer1.Panel1.Controls.Add(header);
   
        }

        /************************\
         * Display Folder name  *
        \************************/

        public void DisplayFolderName(String name)
        {
            this.SummarySplitContainer1.Panel1.Controls.Clear();

            // Display the name of the folder
            KryptonHeader header = new KryptonHeader();
            header.Dock = System.Windows.Forms.DockStyle.Top;
            header.Text = name;
            header.Values.Description = null;
            header.Values.Image = null;

            this.SummarySplitContainer1.Panel1.Controls.Add(header);
        }

        /**************************************************************\
         * Create a new tab (for editing, NORMAL mode)                *
         *   - Add the component.                                     *
         *   - Display the content of the xml                         *
         *   - Manage the event when clicking on "Close".             *
        \**************************************************************/

        public void AddTab(String tabName, String path)
        {
            KryptonPage navigatorTab = new KryptonPage();
            ButtonSpecAny closeButton = new ButtonSpecAny();
            XMLLoader.XMLForm XMLLoader = new XMLLoader.XMLForm();
            
            try
            {
                GC.Collect();
                XMLLoader.Dock = DockStyle.Fill;
                XMLLoader.init(Properties.Settings.Default.interpretation_template, _ButtonHelp);
                XMLLoader.loadXML(path);
                XMLLoader.Tag = path;
                navigatorTab.Tag = true;
                navigatorTab.Controls.Add(XMLLoader);
                switchButtonSpec.ExtraText = "XML Mode";
            }

            // If no connection, New tab (XML mode)
            catch (Exception ex)
            {
                var result = KryptonMessageBox.Show("Unable to access XML Template (maybe there is no network). No Creation Mode available.\n\n\n" + ex, "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                KryptonRichTextBox richTextBox = new KryptonRichTextBox();
                richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
                richTextBox.Name = tabName + "RichTextBox";
                richTextBox.Text = "";
                richTextBox.Tag = path;
                navigatorTab.Tag = false;
                switchButtonSpec.ExtraText = "Creation Mode";
                navigatorTab.Controls.Add(richTextBox);
                DisplayXml(richTextBox);
            }
            

            navigatorTab.Name = tabName + "Tab";
            navigatorTab.Text = tabName;

            closeButton.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.PendantClose;
            navigatorTab.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            closeButton});

            navigatorTab.ButtonSpecs[0].Click += new EventHandler(CloseNavigatorTab);
            navigatorTab.ButtonSpecs[0].Tag = navigatorTab;

            NavigatorControl.Pages.Add(navigatorTab);
        }


        /**************************************************************\
         * Method for displaying a XML                                *
         *   - Display the xml with coloration.                       *
        \**************************************************************/

        private void DisplayXml(KryptonRichTextBox richTextBox)
        {
            XmlTextReader reader = new XmlTextReader(richTextBox.Tag.ToString());

            try
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.XmlDeclaration:

                            richTextBox.SelectionColor = Color.Blue;
                            richTextBox.AppendText("<?xml " + reader.Value + "?>\n");

                            break;

                        case XmlNodeType.Element:

                            String nodeName = reader.Name;
                            if (nodeName.Equals("Field"))
                                richTextBox.AppendText("    ");
                            else if (!nodeName.Equals("region"))
                                richTextBox.AppendText("  ");

                            richTextBox.SelectionColor = Color.Blue;
                            richTextBox.AppendText("<");
                            richTextBox.SelectionColor = Color.Brown;
                            richTextBox.AppendText(reader.Name);

                            for (int attIndex = 0; attIndex < reader.AttributeCount; attIndex++)
                            {
                                reader.MoveToAttribute(attIndex);
                                richTextBox.SelectionColor = Color.Brown;
                                richTextBox.AppendText(" " + reader.Name);
                                richTextBox.SelectionColor = Color.Blue;
                                richTextBox.AppendText("=\"");
                                richTextBox.SelectionColor = Color.Black;
                                richTextBox.SelectionFont = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                richTextBox.AppendText(reader.Value);
                                richTextBox.SelectionFont = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                richTextBox.SelectionColor = Color.Blue;
                                richTextBox.AppendText("\"");
                            }

                            richTextBox.SelectionColor = Color.Blue;
                            if (nodeName.Equals("region") || nodeName.Equals("Function"))
                                richTextBox.AppendText(">\n");
                            else richTextBox.AppendText(" />\n");
                            if (nodeName.Equals("Encoding_OUTPUT"))
                                richTextBox.AppendText("\n");
                            break;

                        case XmlNodeType.Comment:

                            richTextBox.SelectionColor = Color.Gray;
                            richTextBox.AppendText("  <!--" + reader.Value + "-->\n"); // To put if comment.
                            break;

                        case XmlNodeType.EndElement:

                            richTextBox.SelectionColor = Color.Blue;
                            if (reader.Name.Equals("Function"))
                                richTextBox.AppendText("  ");
                            richTextBox.AppendText("</");
                            richTextBox.SelectionColor = Color.Brown;
                            richTextBox.AppendText(reader.Name);
                            richTextBox.SelectionColor = Color.Blue;
                            richTextBox.AppendText(">\n");
                            if (reader.Name.Equals("Function"))
                                richTextBox.AppendText("\n");
                            break;
                    }
                }

                reader.Close();
            }

            catch(Exception ex)
            {
                KryptonMessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region EventMethods

        /**************************************************************\
         * Event for closing the tab                                  *
         *   - Close the tab (depending on the config).               *
         *   - Remove MenuItem from contextMenuStrip.                 *
        \**************************************************************/

        public void CloseNavigatorTab(object sender, EventArgs e)
        {
            if (sender is ButtonSpecAny)
            {
                ButtonSpecAny buttonSpec = sender as ButtonSpecAny;
                KryptonPage tab = buttonSpec.Tag as KryptonPage;

                //if (tab.Text.Contains('*'))
                //{
                //    var result = KryptonMessageBox.Show("Config unsaved. Close Anyway ?\n\n                        ", "Close Tab",
                //     MessageBoxButtons.YesNo,
                //     MessageBoxIcon.Warning);
                //
                //    if (result == DialogResult.Yes)
                //        NavigatorControl.Pages.Remove(tab);
                //}
                //
                //else
                //{
                    if ((Boolean)tab.Tag == true)
                    {
                        foreach (Control c in tab.Controls)
                        {
                            ((XMLLoader.XMLForm)c).Dispose();
                        }
                    }

                    NavigatorControl.Pages.Remove(tab);
                //}
    
                foreach(ToolStripMenuItem element in ContextMenuStrip.Items)
                {
                    if (element.Text.Equals(tab.Text))
                    {
                        ContextMenuStrip.Items.Remove(element);
                        break;
                    }
                }
            }
        }

        /**************************************************************\
         * Event for closing the log tab.                             *
         *   - Close the tab (depending on the navigator).            *
        \**************************************************************/

        public void CloseLogsNavigatorTab(object sender, EventArgs e)
        {
            if (sender is ButtonSpecAny)
            {
                ButtonSpecAny buttonSpec = sender as ButtonSpecAny;
                KryptonPage tab = buttonSpec.Tag as KryptonPage;
                LogsNavigator.Pages.Remove(tab);
            }
        }

        /**************************************************************\
         * Event for clicking on an item of the contextMenuButton     *
         *   - Open specific tab.                                     *
        \**************************************************************/

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (KryptonPage page in NavigatorControl.Pages)
            {
                if(page.Text.Equals(e.ClickedItem.Text.ToString()))
                    NavigatorControl.SelectedPage = page;
            }
        }

        /**************************************************************\
         * Event for clicking on the contextMenuButton the log tab    *
         *   - Open the contextMenuStrip.                             *
        \**************************************************************/

        private void ContextButton_Click(object sender, EventArgs e)
        {
            ContextMenuStrip.Show(Cursor.Position);
        }

        /**************************************************************\
         * Event for clicking on the switch button of the navigator : *
         *    - Switch to creation mode if it's normal mode           *
         *    - and vice & versa                                      *
        \**************************************************************/

        private void switchButtonSpec_Click(object sender, EventArgs e)
        {
            if (!NavigatorControl.SelectedPage.Text.Equals("Summary"))
            {
                // If creation mode, switch to normal mode
                if ((Boolean)NavigatorControl.SelectedPage.Tag)
                {
                    // Get path
                    string path = "";
                    var childrens = NavigatorControl.SelectedPage.Controls.OfType<XMLLoader.XMLForm>().ToList();
                    foreach (XMLLoader.XMLForm element in childrens)
                    {
                        path = (string)element.Tag;
                    }
                    
                    NavigatorControl.SelectedPage.Tag = false;
                    switchButtonSpec.ExtraText = "Creation Mode";
                    NavigatorControl.SelectedPage.Controls.Clear();
                  
                    // Set New Control
                    KryptonRichTextBox richTextBox = new KryptonRichTextBox();
                    richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
                    richTextBox.Text = "";
                    richTextBox.Tag = path;

                    NavigatorControl.SelectedPage.Controls.Add(richTextBox);
                    DisplayXml(richTextBox);
                }

                // If normal mode, switch to creation mode
                else if (!(Boolean)NavigatorControl.SelectedPage.Tag)
                {
                    // Get path
                    string path = "";
                    var childrens = NavigatorControl.SelectedPage.Controls.OfType<KryptonRichTextBox>().ToList();
                    foreach (KryptonRichTextBox element in childrens)
                    {
                        path = (string)element.Tag;
                    }

                    NavigatorControl.SelectedPage.Tag = true;
                    switchButtonSpec.ExtraText = "XML Mode";
                    NavigatorControl.SelectedPage.Controls.Clear();

                    try
                    {
                        // Set New Control
                        XMLLoader.XMLForm XMLLoader = new XMLLoader.XMLForm();
                        XMLLoader.Dock = DockStyle.Fill;
                        XMLLoader.init(Properties.Settings.Default.interpretation_template, _ButtonHelp);
                        XMLLoader.loadXML(path);
                        XMLLoader.Tag = path;
                        NavigatorControl.SelectedPage.Controls.Add(XMLLoader);
                    }

                    // If no connection, stay in XML mode
                    catch (Exception ex)
                    {
                        var result = KryptonMessageBox.Show("Unable to access XML Template (maybe there is no network). No Creation Mode available.", "No network",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        KryptonRichTextBox richTextBox = new KryptonRichTextBox();
                        richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
                        richTextBox.Text = "";
                        richTextBox.Tag = path;
                        NavigatorControl.SelectedPage.Tag = false;
                        switchButtonSpec.ExtraText = "Creation Mode";

                        NavigatorControl.SelectedPage.Controls.Add(richTextBox);
                        DisplayXml(richTextBox);
                    }
                }
            }
        }

        #endregion

        #region Accessors

        public List<KryptonHeaderGroup> Get_ProcessHeaderGroupList()
        {
            return _ProcessHeaderGroupList;
        }

        #endregion
    }
}
