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
    public partial class BatchElement : UserControl
    {
        private ToolTip _SuppressButtonTooltip;
        private ToolTip _TargetPathTooltip;
        private string _TargetPath;
        private string _ConfigPath;
        private string _ConfigName;
        private string _FtpRegion; // FTP region to connect in order to get file.

        private Timer _Timer;

        /**************************************************** Constructor *****************************************************/

        #region Constructor

        public BatchElement(List<FTPManager.Region> regions)
        {
            InitializeComponent();

            _TargetPath = null;
            _ConfigName = null;
            _ConfigPath = null;
            _FtpRegion = null;

            _SuppressButtonTooltip = new ToolTip();
            _SuppressButtonTooltip.SetToolTip(SuppressButton, "Delete this component.");
            _TargetPathTooltip = new ToolTip();
            _TargetPathTooltip.SetToolTip(TargetPathButton, "Not defined");

            _Timer = new Timer();
            _Timer.Tick += _Timer_Tick;

            /* Fill regions in ComboBox */
            regions = regions.OrderBy(o => o.Get_RegionName()).ToList();
            foreach(FTPManager.Region region in regions)
                FTPComboBox.Items.Add(region.Get_RegionName());
            FTPComboBox.Items.Insert(0, "-");


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

        private void FTPComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            _FtpRegion = FTPComboBox.Text;          
            _Timer.Enabled = true;
        }

        /* The timer is a little trick to remove the highlight (focus on another control doesn't work). */
        private void _Timer_Tick(object sender, EventArgs e)
        {
            FTPComboBox.Select(0, 0);
            _Timer.Enabled = false;
        }

        private void FTPComboBox_Enter(object sender, EventArgs e)
        {
            _Timer.Enabled = true;
        }

        #endregion

        /***************************************************** Accessors *****************************************************/

        public string Get_TargetPath()
        {
            return _TargetPath;
        }

        public string Get_FtpRegion()
        {
            return _FtpRegion;
        }

        public string Get_ConfigPath()
        {
            return _ConfigPath;
        }

        public string Get_ConfigName()
        {
            return _ConfigName;
        }

        public void Set_ConfigPath(string path)
        {
            _ConfigPath = path;
        }

        public void Set_ConfigName(string name)
        {
            _ConfigName = name;
        }

        public void Set_TargetPath(string path)
        {
            _TargetPath = path;
        }

        public void Set_TargetPathTooltip(string path)
        {
            _TargetPathTooltip.SetToolTip(TargetPathButton, "Target : " + path);
        }

        public void Set_FtpRegion(string FTPregion)
        {
            _FtpRegion = FTPregion;
        }
    }
}
