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
    public partial class Settings : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructors

        public Settings()
        {
            InitializeComponent();

            PersonnalPathTextBox.Text = Properties.Settings.Default.local_path;
            HCTextBox.Text = Properties.Settings.Default.hc_config;
            XMLTemplateTextBox.Text = Properties.Settings.Default.interpretation_template;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        // Change personnal Path
        private void PersonnalPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

            DialogResult result = openFolderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.local_path = openFolderDialog.SelectedPath;
                Properties.Settings.Default.Save();
                PersonnalPathTextBox.Text = Properties.Settings.Default.local_path;

                var result2 = KryptonMessageBox.Show("Saved !", "Saved",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // Change HC Path
        private void HCButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (String inputFile in openFileDialog.FileNames)
                    Properties.Settings.Default.hc_config = inputFile;

                Properties.Settings.Default.Save();
                HCTextBox.Text = Properties.Settings.Default.hc_config;

                var result2 = KryptonMessageBox.Show("Saved !", "Saved",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // Change XML interpretation template path
        private void XMLTemplateButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

            DialogResult result = openFolderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.interpretation_template = openFolderDialog.SelectedPath;

                Properties.Settings.Default.Save();
                XMLTemplateTextBox.Text = Properties.Settings.Default.interpretation_template;

                var result2 = KryptonMessageBox.Show("Saved !", "Saved",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        #endregion



        #region Accessors

        #endregion

    }
}
