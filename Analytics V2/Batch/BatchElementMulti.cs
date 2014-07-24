using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Analytics_V2
{
    public partial class BatchElementMulti : UserControl
    {
        private ToolTip _SuppressButtonTooltip;
        private ToolTip _TargetPathTooltip;
        private string _TargetPath;
        private Dictionary<string, string> _ConfigsInfo; // Name/Path of config.
        private List<KryptonLabel> _AdditionalConfigsLabel;
        private List<Label> _AdditionalConfigs;
        private List<KryptonButton> _SuppressButtons;

        int _IdControls;

        /**************************************************** Constructor *****************************************************/

        #region Constructor

        public BatchElementMulti()
        {
            InitializeComponent();

            SuppressButton.Tag = this;

            _TargetPath = null;
            _ConfigsInfo = new Dictionary<string, string>();
            _AdditionalConfigsLabel = new List<KryptonLabel>();
            _AdditionalConfigs = new List<Label>();
            _SuppressButtons = new List<KryptonButton>();

            _SuppressButtonTooltip = new ToolTip();
            _SuppressButtonTooltip.SetToolTip(SuppressButton, "Delete this component.");
            _TargetPathTooltip = new ToolTip();
            _TargetPathTooltip.SetToolTip(TargetPathButton, "Not defined");

            _IdControls = 0;
        }

        #endregion

        
        /****************************************************** Methods *******************************************************/

        #region Events

        private void TargetPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

            DialogResult result = openFolderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _TargetPathTooltip.SetToolTip(TargetPathButton, "Target : " + openFolderDialog.SelectedPath);
                _TargetPath = openFolderDialog.SelectedPath;

                var result2 = KryptonMessageBox.Show("Path defined. ", "Path Defined",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
        }

        /***********************************\
         * Drag'n drop for adding configs  *
        \***********************************/

        private void GroupBox_Panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.None;
        }

        private void GroupBox_Panel_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (node.ImageIndex == 2 && node.Text.Contains(".xml"))
            {
                // If config doesn't exist in the dictionary, add config graphically/in the dictionary
                if (!_ConfigsInfo.ContainsKey(node.Text.Replace(".xml", "")))
                {
                    TutoLabel.Visible = false;
                    _IdControls++;

                    ////////////////
                    // Add graphics
                    // Label "Config"
                    KryptonLabel configLabel = new KryptonLabel();
                    configLabel.Text = "Config : ";
                    configLabel.Tag = _IdControls;
                    configLabel.Location = new System.Drawing.Point(25, 65 + (_AdditionalConfigsLabel.Count + 1) * 20);
                    GroupBox.Panel.Controls.Add(configLabel);

                    // Label config name
                    Label configNameLabel = new Label();
                    configNameLabel.AutoSize = true;
                    configNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                    configNameLabel.Text = node.Text.Replace(".xml", "");
                    configNameLabel.BackColor = System.Drawing.Color.Transparent;
                    configNameLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    configNameLabel.Tag = _IdControls;
                    configNameLabel.Location = new System.Drawing.Point(85, 65 + (_AdditionalConfigs.Count + 1) * 20);
                    GroupBox.Panel.Controls.Add(configNameLabel);

                    // mini suppress button
                    KryptonButton button = new KryptonButton();
                    button.Tag = _IdControls;
                    button.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
                    button.Location = new System.Drawing.Point(10, 67 + (_AdditionalConfigs.Count + 1) * 20);
                    button.Size = new System.Drawing.Size(16, 16);
                    button.Values.Image = global::Analytics_V2.Properties.Resources.Delete2;
                    button.Click += SuppressConfigElement;
                    GroupBox.Panel.Controls.Add(button);

                    _AdditionalConfigsLabel.Add(configLabel);
                    _AdditionalConfigs.Add(configNameLabel);
                    _SuppressButtons.Add(button);

                    // Add config to dictionnary
                    _ConfigsInfo.Add(node.Text.Replace(".xml", ""), node.FullPath);

                    this.Height += 20;
                }

                else
                {
                    var result = KryptonMessageBox.Show("Config already exists", "Error while adding config",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation);
                }
            }
        }

        /****************************\
         * Suppress config element  *
        \****************************/

        public void SuppressConfigElement(object sender, EventArgs e)
        {
            KryptonButton button = (KryptonButton)sender;

            for (int i = 0; i < _AdditionalConfigsLabel.Count; i++)
            {
                if ((int)_AdditionalConfigsLabel[i].Tag == (int)button.Tag)
                {
                    _AdditionalConfigsLabel[i].Dispose();

                    for (int j = 0; j < _AdditionalConfigsLabel.Count; j++)
                    {
                        if (j > i)
                        {
                            _AdditionalConfigsLabel[j].Location = new System.Drawing.Point(25, _AdditionalConfigsLabel[j].Location.Y - 20);
                        }
                    }

                    _AdditionalConfigsLabel.Remove(_AdditionalConfigsLabel[i]);
                    break;
                }
            }

            for (int i = 0; i < _AdditionalConfigs.Count; i++)
            {
                if ((int)_AdditionalConfigs[i].Tag == (int)button.Tag)
                {
                    _ConfigsInfo.Remove(_AdditionalConfigs[i].Text);
                    _AdditionalConfigs[i].Dispose();

                    for (int j = 0; j < _AdditionalConfigs.Count; j++)
                    {
                        if (j > i)
                        {
                            _AdditionalConfigs[j].Location = new System.Drawing.Point(85, _AdditionalConfigs[j].Location.Y - 20);
                        }
                    }

                    _AdditionalConfigs.Remove(_AdditionalConfigs[i]);
                    break;
                }
            }

            for (int i = 0; i < _SuppressButtons.Count; i++)
            {
                if ((int)_SuppressButtons[i].Tag == (int)button.Tag)
                {
                    _SuppressButtons[i].Dispose();

                    for (int j = 0; j < _SuppressButtons.Count; j++)
                    {
                        if (j > i)
                        {
                            _SuppressButtons[j].Location = new System.Drawing.Point(10, _SuppressButtons[j].Location.Y - 20);
                        }
                    }

                    _SuppressButtons.Remove(_SuppressButtons[i]);
                    break;
                }
            }

            if(_SuppressButtons.Count == 0)
                TutoLabel.Visible = true;
            this.Height -= 20;

            
        }

        #endregion

        /***************************************************** Accessors *****************************************************/

        public Dictionary<string, string> Get_ConfigsInfo()
        {
            return _ConfigsInfo;
        }

        public String Get_TargetPath()
        {
            return _TargetPath;
        }

        public void Set_TargetPath(string path)
        {
            _TargetPath = path;
        }

        public void Set_TargetPathTooltip(string path)
        {
            _TargetPathTooltip.SetToolTip(TargetPathButton, "Target : " + path);
        }

        public int Get_IdControls()
        {
            return _IdControls;
        }

        public void Set_IdControls(int i)
        {
            _IdControls = i;
        }

        public List<KryptonLabel> Get_AdditionalConfigsLabel()
        {
            return _AdditionalConfigsLabel;
        }

        public List<Label> Get_AdditionalConfigs()
        {
            return _AdditionalConfigs;
        }

        public List<KryptonButton> Get_SuppressButtons()
        {
            return _SuppressButtons;
        }
    }
}
