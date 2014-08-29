using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.IO;

namespace Analytics_V2
{
    public partial class BatchUC : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private List<BatchElement> _BatchElements;           // Graphical elements for batch (single).
        private List<BatchElementMulti> _BatchElementsMulti; // Graphical elements for batch (multi).
        private List<Batch> _BatchsList;                     // List of ALL batchs.
        private List<FTPManager.Region> _RegionsList;        // List of all regions (fetched from FTPManager).

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public BatchUC(List<FTPManager.Region> FtpRegionsList)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.BringToFront();
            SplitContainer.Panel2Collapsed = true;
            SplitContainer.Panel2.Hide();

            _BatchElements = new List<BatchElement>();
            _BatchElementsMulti = new List<BatchElementMulti>();
            _BatchsList = new List<Batch>();
            _RegionsList = FtpRegionsList;
        }

        #endregion

        /****************************************************** Methods *******************************************************/

        #region Methods

        /**********************************\
         * load batch from saved instance *
        \**********************************/

        public void LoadBatchs()
        {
            foreach (Batch element in _BatchsList)
            {
                BatchsDataGridView.AllowUserToAddRows = true;
                DataGridViewRow row = (DataGridViewRow)BatchsDataGridView.Rows[0].Clone();
                row.Cells[0].Value = element.Get_Name();
                BatchsDataGridView.Rows.Add(row);
                BatchsDataGridView.AllowUserToAddRows = false;
            }
        }

        /***************************\
         * Add Batch button click  *
        \***************************/

        private void AddToolStripButton_Click(object sender, EventArgs e)
        {
            if (SplitContainer.Panel2Collapsed)
            {
                Form form = (Form)this.Parent;
                form.Size = new System.Drawing.Size(form.Width + 300, form.Height);
            }

            SplitContainer.Panel2Collapsed = false;
            SplitContainer.Panel2.Show();

            EditToolStripButton.Enabled = false;
            RemoveToolStripButton.Enabled = false;
            AddToolStripButton.Enabled = false;
        }

        /******************************\
         * Remove Batch button click  *
         *    -> Suppress from list.  *
         *    -> Suppress graphically *
        \******************************/

        private void RemoveToolStripButton_Click(object sender, EventArgs e)
        {
            if (BatchsDataGridView.CurrentCell != null)
            {
                var result = KryptonMessageBox.Show("Do you really want to delete " + BatchsDataGridView.CurrentCell.Value + " ?", "Delete this batch",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Suppress batch from list
                    foreach (Batch element in _BatchsList)
                    {
                        if (element.Get_Name().Equals(BatchsDataGridView.CurrentCell.Value))
                        {
                            _BatchsList.Remove(element);
                            break;
                        }
                    }

                    // Suppress batch graphically
                    foreach (DataGridViewRow row in BatchsDataGridView.Rows)
                    {
                        if (row.Cells[0].Value.Equals(BatchsDataGridView.CurrentCell.Value))
                        {
                            BatchsDataGridView.Rows.Remove(row);
                        }
                    }
                }
            }
        }

        /****************************\
         * Edit Batch button click  *
        \****************************/

        private void EditToolStripButton_Click(object sender, EventArgs e)
        {
            if (BatchsDataGridView.CurrentCell != null)
            {
                TutoLabel.Visible = false;
                // Retrieve batch from list of batch
                foreach (Batch element in _BatchsList)
                {
                    if (element.Get_Name().Equals(BatchsDataGridView.CurrentCell.Value))
                    {
                        EditToolStripButton.Enabled = false;
                        AddToolStripButton.Enabled = false;
                        RemoveToolStripButton.Enabled = false;
                        if (element.Get_Type().Equals("Single"))
                            InstanciateBatchGraphical(element);
                        else if (element.Get_Type().Equals("Multi"))
                            InstanciateBatchMultiGraphical(element);
                        break;
                    }
                }
            }
        }


        /*******************************************************************\
         * Click on supress button event (for deleting a BatchElement UC)  *
         *    -> Suppress batch element UC                                 *
        \*******************************************************************/

        private void SuppressButton_Click(object sender, EventArgs e)
        {
            if (Navigator.SelectedPage.Text.Equals("Single Config"))
            {
                BatchElement elementToSuppress = ((BatchElement)((KryptonGroupBox)((KryptonGroupPanel)((KryptonButton)sender).Parent).Parent).Parent);

                foreach (BatchElement element in _BatchElements)
                {
                    if (elementToSuppress.Equals(element))
                    {
                        _BatchElements.Remove(element);
                        element.Dispose();
                        break;
                    }
                }

                if (_BatchElements.Count == 0)
                    TutoLabel.Visible = true;
            }

            else if(Navigator.SelectedPage.Text.Equals("Multiple Configs"))
            {
                //BatchElementMulti elementToSuppress = ((BatchElementMulti)((KryptonGroupBox)((KryptonGroupPanel)((KryptonButton)sender).Parent).Parent).Parent);
                BatchElementMulti elementToSuppress = ((BatchElementMulti)((KryptonButton)sender).Tag);
                foreach (BatchElementMulti element in _BatchElementsMulti)
                {
                    if (elementToSuppress.Equals(element))
                    {
                        _BatchElementsMulti.Remove(element);
                        element.Dispose();
                        break;
                    }
                }

                if (_BatchElementsMulti.Count == 0)
                    TutoLabelMulti.Visible = true;
            }
        }

        private Boolean CheckBatch()
        {
            Boolean isValid = true;

            if (Navigator.SelectedIndex == 0)
            {
                // Check if name field is correctly filled.
                if (String.IsNullOrEmpty(BatchNameTextBox.Text))
                {
                    var result = KryptonMessageBox.Show("Invalid Batch name\n\nCould not save.", "Error while saving",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation);

                    return false;
                }

                // Check if there is at least one Batch Element
                if (_BatchElements.Count == 0)
                {
                    var result = KryptonMessageBox.Show("No Batch element! Please add at least one.", "Error while saving",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation);
                    return false;
                }

                // Check if each element has 1 path set.
                foreach (BatchElement element in _BatchElements)
                {
                    if (!Directory.Exists(element.Get_TargetPath()))
                    {
                        var result = KryptonMessageBox.Show("Error with the target path of " + element.Config.Text + "\n\nCould not save.", "Error while saving",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation);

                        return false;
                    }
                }
            }

            else if (Navigator.SelectedIndex == 1)
            {

                // Check if name field is correctly filled.
                if (String.IsNullOrEmpty(BatchNameTextBoxMulti.Text))
                {
                    var result = KryptonMessageBox.Show("Invalid Batch name\n\nCould not save.", "Error while saving",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation);

                    return false;
                }

                // Check if there is at least one Batch Element
                if (_BatchElementsMulti.Count == 0)
                {
                    var result = KryptonMessageBox.Show("No Batch element! Please add at least one.", "Error while saving",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation);
                    return false;
                }

                // Check if element has 1 path set.
                foreach (BatchElementMulti element in _BatchElementsMulti)
                {
                    if (!Directory.Exists(element.Get_TargetPath()))
                    {
                        var result = KryptonMessageBox.Show("Error with the target path of " + element.Config.Text + "\n\nCould not save.", "Error while saving",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation);

                        return false;
                    }
                }
            }

            return isValid;
        }

        /****************************************************** SINGLE MODE *******************************************************/

        /***********************************\
         * Instanciate graphical elements  *
        \***********************************/

        private void InstanciateBatchGraphical(Batch batch)
        {
            // Set navigator page
            Navigator.SelectedIndex = 0;

            // Open creation section
            if (SplitContainer.Panel2Collapsed)
            {
                Form form = (Form)this.Parent;
                form.Size = new System.Drawing.Size(form.Width + 300, form.Height);
                SplitContainer.Panel2Collapsed = false;
                SplitContainer.Panel2.Show();
            }
            else
            {
                BatchNameTextBox.Text = "";
                BatchNameTextBoxMulti.Text = "";
                foreach (BatchElement element in _BatchElements)
                    element.Dispose();
                _BatchElements.Clear();
                foreach (BatchElementMulti element2 in _BatchElementsMulti)
                    element2.Dispose();
                _BatchElementsMulti.Clear();
            }

            // Add graphical elements
            if (batch.Get_Type().Equals("Single"))
            {
                BatchNameTextBox.Text = batch.Get_Name();

                foreach (KeyValuePair<string, Tuple<string, string, string>> element in batch.Get_BatchElements())
                {
                    BatchElement batchElement = new BatchElement(_RegionsList);
                    batchElement.Config.Text = element.Value.Item1.Replace(".xml", "");
                    batchElement.Set_TargetPathTooltip(element.Key);
                    batchElement.Set_TargetPath(element.Key);
                    batchElement.Set_ConfigPath(element.Value.Item2);
                    batchElement.Set_ConfigName(element.Value.Item1.Replace(".xml", ""));
                    batchElement.Set_FtpRegion(element.Value.Item3);
                    batchElement.FTPComboBox.SelectedItem = element.Value.Item3;
                    
                    batchElement.SuppressButton.Click += SuppressButton_Click;

                    // Add new element to the UC
                    BatchElementsGroupBox.Panel.Controls.Add(batchElement);
                    batchElement.Dock = DockStyle.Top;
                    batchElement.BringToFront();
                    _BatchElements.Add(batchElement);
                }
            }
        }

        /************************\
         * Return button click  *
         *    -> Clear UC       *
        \************************/

        private void BackButtonSpec_Click(object sender, EventArgs e)
        {
            var result = KryptonMessageBox.Show("Are you sure ?", "Cancel Action",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SplitContainer.Panel2Collapsed = true;
                SplitContainer.Panel2.Hide();
                Form form = (Form)this.Parent;
                form.Size = new System.Drawing.Size(form.Width - 300, form.Height);

                BatchNameTextBox.Text = "";

                foreach (BatchElement element in _BatchElements)
                    element.Dispose();
                _BatchElements.Clear();

                EditToolStripButton.Enabled = true;
                RemoveToolStripButton.Enabled = true;
                AddToolStripButton.Enabled = true;
                TutoLabel.Visible = true;
            }
        }

        /*****************************************\
         * Save new batch                        *
         *    -> Check validity                  *
         *    -> Suppress old batch if exists    *
         *    -> Add batch to the list of batchs *
         *    -> Remove old batch row if exist   *
         *    -> Add batch to the datagrid       *
         *    -> Clear Batch elements UCs        *
        \*****************************************/

        private void SaveButtonSpec_Click(object sender, EventArgs e)
        {
            // Check if each element is correctly, save if ok
            if (CheckBatch())
            {
                // Suppress old batch if exists
                foreach (Batch element in _BatchsList)
                {
                    if (element.Get_Name().Equals(BatchNameTextBox.Text))
                    {
                        _BatchsList.Remove(element);
                        break;
                    }
                }

                // Add batch to the list of batchs
                Batch batch = new Batch(BatchNameTextBox.Text, "Single");

                foreach (BatchElement element in _BatchElements)
                {
                    batch.AddBatchElement(element.Get_TargetPath(), element.Get_ConfigName(), element.Get_ConfigPath(),element.Get_FtpRegion());
                }

                _BatchsList.Add(batch);

                KryptonMessageBox.Show("Saved !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Remove old batch row if exist
                foreach (DataGridViewRow element in BatchsDataGridView.Rows)
                {
                    if (element.Cells[0].Value.Equals(BatchNameTextBox.Text))
                    {
                        BatchsDataGridView.Rows.Remove(element);
                        break;
                    }
                }

                // Add batch to the datagrid
                BatchsDataGridView.AllowUserToAddRows = true;
                DataGridViewRow row = (DataGridViewRow)BatchsDataGridView.Rows[0].Clone();
                row.Cells[0].Value = batch.Get_Name();
                BatchsDataGridView.Rows.Add(row);
                BatchsDataGridView.AllowUserToAddRows = false;

                // Clear Batch creation elements
                SplitContainer.Panel2Collapsed = true;
                SplitContainer.Panel2.Hide();
                Form form = (Form)this.Parent;
                form.Size = new System.Drawing.Size(form.Width - 300, form.Height);
                BatchNameTextBox.Text = "";
                BatchNameTextBoxMulti.Text = "";
                TutoLabel.Visible = true;
                foreach (BatchElement element in _BatchElements)
                    element.Dispose();
                _BatchElements.Clear();
                foreach (BatchElementMulti element2 in _BatchElementsMulti)
                    element2.Dispose();
                _BatchElementsMulti.Clear();

                EditToolStripButton.Enabled = true;
                AddToolStripButton.Enabled = true;
                RemoveToolStripButton.Enabled = true;
            }
        }
   

        /***************************************************\
         * Drop item event (for adding a BatchElement UC)  *
        \***************************************************/

        private void BatchElementsGroupBox_Panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.None;
        }
        
        private void BatchElementsGroupBox_Panel_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(TreeNode))) return;
        
            TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));
        
            if (node.ImageIndex == 2 && node.Text.Contains(".xml"))
            {
                BatchElement batchElement = new BatchElement(_RegionsList);
                batchElement.Config.Text = node.Text.Replace(".xml","");
                batchElement.Set_ConfigPath(node.FullPath);
                batchElement.Set_ConfigName(node.Text.Replace(".xml", ""));
                batchElement.Set_FtpRegion("-");
                batchElement.SuppressButton.Click += SuppressButton_Click;
        
                // Add new element to the UC
                BatchElementsGroupBox.Panel.Controls.Add(batchElement);
                batchElement.Dock = DockStyle.Top;
                batchElement.BringToFront();
        
                _BatchElements.Add(batchElement);
        
                TutoLabel.Visible = false;
            }
        }


        /****************************************************** MULTI MODE *******************************************************/

        private void InstanciateBatchMultiGraphical(Batch batch)
        {
            TutoLabelMulti.Visible = false;

            // Set navigator page
            Navigator.SelectedIndex = 1;

            // Open creation section
            if (SplitContainer.Panel2Collapsed)
            {
                Form form = (Form)this.Parent;
                form.Size = new System.Drawing.Size(form.Width + 300, form.Height);
                SplitContainer.Panel2Collapsed = false;
                SplitContainer.Panel2.Show();
            }
            else
            {
                BatchNameTextBox.Text = "";
                BatchNameTextBoxMulti.Text = "";
                foreach (BatchElement element in _BatchElements)
                    element.Dispose();
                _BatchElements.Clear();
                foreach (BatchElementMulti element2 in _BatchElementsMulti)
                    element2.Dispose();
                _BatchElementsMulti.Clear();
            }

            // Add graphical elements
            if (batch.Get_Type().Equals("Multi"))
            {
                BatchNameTextBoxMulti.Text = batch.Get_Name();

                foreach (KeyValuePair<Tuple<string, string>, List<Tuple<string, string>>> element in batch.Get_BatchElementsMulti())
                {
                    BatchElementMulti batchElement = new BatchElementMulti(_RegionsList);
                    batchElement.Set_TargetPathTooltip(element.Key.Item1);
                    batchElement.Set_TargetPath(element.Key.Item1);
                    batchElement.Set_FtpRegion(element.Key.Item2);
                    batchElement.FTPComboBox.Text = element.Key.Item2;
                    
                    for (int i = 0; i < element.Value.Count; i++)
                    {
                        if (i == 0)
                        {
                            batchElement.Config.Text = element.Value[0].Item1;
                            batchElement.Get_ConfigsInfo().Add(element.Value[i].Item1, element.Value[i].Item2);
                        }
                        else
                        {
                            batchElement.TutoLabel.Visible = false;
                            batchElement.Set_IdControls(batchElement.Get_IdControls() + 1);

                            batchElement.SuppressButton.Click += SuppressButton_Click;

                            ////////////////
                            // Add graphics
                            // Label "Config"

                            KryptonLabel configLabel = new KryptonLabel();
                            configLabel.Text = "Config : ";
                            configLabel.Tag = batchElement.Get_IdControls();
                            configLabel.Location = new System.Drawing.Point(35, 86 + (batchElement.Get_AdditionalConfigsLabel().Count + 1) * 20);
                            batchElement.GroupBox.Panel.Controls.Add(configLabel);

                            // Label config name
                            Label configNameLabel = new Label();
                            configNameLabel.AutoSize = true;
                            configNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                            configNameLabel.Text = element.Value[i].Item1;
                            configNameLabel.BackColor = System.Drawing.Color.Transparent;
                            configNameLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            configNameLabel.Tag = batchElement.Get_IdControls(); ;
                            configNameLabel.Location = new System.Drawing.Point(94, 86 + (batchElement.Get_AdditionalConfigs().Count + 1) * 20);
                            batchElement.GroupBox.Panel.Controls.Add(configNameLabel);

                            // mini suppress button
                            KryptonButton button = new KryptonButton();
                            button.Tag = batchElement.Get_IdControls();
                            button.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
                            button.Location = new System.Drawing.Point(10, 88 + (batchElement.Get_AdditionalConfigs().Count + 1) * 20);
                            button.Size = new System.Drawing.Size(16, 16);
                            button.Values.Image = global::Analytics_V2.Properties.Resources.Delete2;
                            button.Click += batchElement.SuppressConfigElement;
                            batchElement.GroupBox.Panel.Controls.Add(button);

                            batchElement.Get_AdditionalConfigsLabel().Add(configLabel);
                            batchElement.Get_AdditionalConfigs().Add(configNameLabel);
                            batchElement.Get_SuppressButtons().Add(button);

                            // Add config to dictionnary
                            batchElement.Get_ConfigsInfo().Add(element.Value[i].Item1, element.Value[i].Item2);

                            batchElement.Height += 20;
                        }
                    }

                    // Add new element to the UC
                    BatchElementsGroupBoxMulti.Panel.Controls.Add(batchElement);
                    batchElement.Dock = DockStyle.Top;
                    batchElement.BringToFront();

                    _BatchElementsMulti.Add(batchElement);
                }
            }
        }

        private void BackButtonSpecMulti_Click(object sender, EventArgs e)
        {
            var result = KryptonMessageBox.Show("Are you sure ?", "Cancel Action",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SplitContainer.Panel2Collapsed = true;
                SplitContainer.Panel2.Hide();
                Form form = (Form)this.Parent;
                form.Size = new System.Drawing.Size(form.Width - 300, form.Height);
;
                BatchNameTextBoxMulti.Text = "";

                foreach (BatchElementMulti element2 in _BatchElementsMulti)
                    element2.Dispose();
                _BatchElementsMulti.Clear();

                EditToolStripButton.Enabled = true;
                RemoveToolStripButton.Enabled = true;
                AddToolStripButton.Enabled = true;
                TutoLabelMulti.Visible = true;
            }
        }

        private void SaveButtonSpecMulti_Click(object sender, EventArgs e)
        {
            // Check if each element is correctly, save if ok
            if (CheckBatch())
            {
                // Suppress old batch if exists
                foreach (Batch element in _BatchsList)
                {
                    if (element.Get_Name().Equals(BatchNameTextBoxMulti.Text))
                    {
                        _BatchsList.Remove(element);
                        break;
                    }
                }

                // Add batch to the list of batchs
                Batch batch = new Batch(BatchNameTextBoxMulti.Text, "Multi");


                foreach (BatchElementMulti element in _BatchElementsMulti)
                {
                    batch.AddBatchElement(element.Get_TargetPath(),element.Get_FtpRegion(), element.Get_ConfigsInfo());
                }

                _BatchsList.Add(batch);

                KryptonMessageBox.Show("Saved !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Remove old batch row if exist
                foreach (DataGridViewRow element in BatchsDataGridView.Rows)
                {
                    if (element.Cells[0].Value.Equals(BatchNameTextBoxMulti.Text))
                    {
                        BatchsDataGridView.Rows.Remove(element);
                        break;
                    }
                }

                // Add batch to the datagrid
                BatchsDataGridView.AllowUserToAddRows = true;
                DataGridViewRow row = (DataGridViewRow)BatchsDataGridView.Rows[0].Clone();
                row.Cells[0].Value = batch.Get_Name();
                BatchsDataGridView.Rows.Add(row);
                BatchsDataGridView.AllowUserToAddRows = false;

                // Clear Batch creation elements
                SplitContainer.Panel2Collapsed = true;
                SplitContainer.Panel2.Hide();
                Form form = (Form)this.Parent;
                form.Size = new System.Drawing.Size(form.Width - 300, form.Height);
                BatchNameTextBox.Text = "";
                BatchNameTextBoxMulti.Text = "";
                TutoLabel.Visible = true;
                foreach (BatchElement element in _BatchElements)
                    element.Dispose();
                _BatchElements.Clear();
                foreach (BatchElementMulti element2 in _BatchElementsMulti)
                    element2.Dispose();
                _BatchElementsMulti.Clear();

                EditToolStripButton.Enabled = true;
                AddToolStripButton.Enabled = true;
                RemoveToolStripButton.Enabled = true;
            }
        }

        private void BatchElementsGroupBoxMulti_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.None;
        }

        private void BatchElementsGroupoxMulti_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(TreeNode))) return;

            TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (node.ImageIndex == 2 && node.Text.Contains(".xml"))
            {
                BatchElementMulti batchElement = new BatchElementMulti(_RegionsList);
                batchElement.Config.Text = node.Text.Replace(".xml", "");
                batchElement.Get_ConfigsInfo().Add(node.Text.Replace(".xml", ""), node.FullPath);
                batchElement.SuppressButton.Click += SuppressButton_Click;
                batchElement.Set_FtpRegion("-");

                // Add new element to the UC
                BatchElementsGroupBoxMulti.Panel.Controls.Add(batchElement);
                batchElement.Dock = DockStyle.Top;
                batchElement.BringToFront();

                _BatchElementsMulti.Add(batchElement);

                TutoLabelMulti.Visible = false;
            }
        }

        #endregion

        #region Accessors

        public List<Batch> Get_BatchsList()
        {
            return _BatchsList;
        }

        public void Set_BatchsList(List<Batch> batchs)
        {
            _BatchsList = batchs;
        }

        public void Set_RegionsList(List<FTPManager.Region> regionsList)
        {
            _RegionsList = regionsList;
        }

        #endregion


    }     
}
