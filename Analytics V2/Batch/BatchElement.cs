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

        /**************************************************** Constructor *****************************************************/

        #region Constructor

        public BatchElement()
        {
            InitializeComponent();

            _TargetPath = null;
            _ConfigName = null;
            _ConfigPath = null;

            _SuppressButtonTooltip = new ToolTip();
            _SuppressButtonTooltip.SetToolTip(SuppressButton, "Delete this component.");
            _TargetPathTooltip = new ToolTip();
            _TargetPathTooltip.SetToolTip(TargetPathButton, "Not defined");
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

        #endregion

        /***************************************************** Accessors *****************************************************/

        public string Get_TargetPath()
        {
            return _TargetPath;
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
    }
}
