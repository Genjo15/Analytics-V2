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
    public partial class Administration : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private Boolean _RowAdded;

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Administration()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            _RowAdded = false;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /**********************************************\
         * Load users:                                *
         *   - Connect to the database and get users. * 
         *   - Update DatagridView                    *
        \**********************************************/

        public void LoadUsers()
        {
            AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
            DataSet dataSet = service.Get_Users();
            DataGridView.DataSource = dataSet.Tables[0];
            service.Close();

            DataGridView.Columns[0].HeaderText = "User Name";
            DataGridView.Columns[1].HeaderText = "Access Type";

        }

        /***************\
         * Delete User *
        \***************/

        private void RemoveToolStripButton_Click(object sender, EventArgs e)
        {
            if (Navigator.SelectedPage.Text.Equals("Users"))
            {
                foreach (DataGridViewRow row in DataGridView.SelectedRows)
                {
                    if (row.Cells[0].Value.ToString().Equals("user") || row.Cells[0].Value.ToString().Equals("admin") || row.Cells[0].Value.ToString().Equals("superadmin"))
                    {
                        var result = KryptonMessageBox.Show("Cannot suppress " + row.Cells[0].Value.ToString(), "Error",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Stop);
                    }

                    else
                    {
                        AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                        service.Delete_User(row.Cells[0].Value.ToString());
                        service.Close();

                        DataGridView.Rows.Remove(row);
                        var result = KryptonMessageBox.Show("User deleted !", "User deleted",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                    }
                }
            }
        }

        /**********************************************************************************\
         * Modify User                                                                    *
         *   - CellBegin Edit : begin edit (user name) OR show combobox (access type)     *
         *   - ShowComboBox : show the combobox (place it at the right place too)         *
         *   - ComboBox_SelectionChangeCommitted : When user has selected a combobox item *
         *   - DataGridView_CellValidating : When a user has edited a "user name" case    *
         *   - UpdateUser : Perform update in DB                                          *
         *                                                                                *
         * /!\ If row added, and last line modified --> Insert User                       *                                                                               
        \**********************************************************************************/

        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (DataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("user") || DataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("admin") || DataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("superadmin"))
            {
                var result = KryptonMessageBox.Show("Cannot modify " + DataGridView.Rows[e.RowIndex].Cells[0].Value, "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);

                e.Cancel = true;
            }

            else
            {
                if (DataGridView.Columns[DataGridView.CurrentCell.ColumnIndex].HeaderText.Equals("Access Type"))
                {
                    DataGridView.CurrentCell = DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[DataGridView.CurrentCell.ColumnIndex];
                    ShowComboBox(DataGridView.CurrentRow.Index, DataGridView.CurrentCell.ColumnIndex);
                    SendKeys.Send("{F4}");
                }
            }
        }

        private void ShowComboBox(int iRowIndex, int iColumnIndex)
        {
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;

            Rectangle rect = new Rectangle();
            rect = DataGridView.GetCellDisplayRectangle(iColumnIndex, iRowIndex, false);
            x = rect.X + DataGridView.Left;
            y = rect.Y + DataGridView.Top;

            width = rect.Width;
            height = rect.Height;

            ComboBox.SetBounds(x, y, width, height);
            ComboBox.Visible = true;
            ComboBox.Text = DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[DataGridView.CurrentCell.ColumnIndex].Value.ToString();
            ComboBox.Focus();
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[DataGridView.CurrentCell.ColumnIndex].Value = ComboBox.Text;
            DataGridView.Focus();
            ComboBox.Visible = false;

            if (!String.IsNullOrEmpty(DataGridView[0, DataGridView.CurrentRow.Index].Value.ToString())) // Update only if both fields are not empty.
            {
                if (_RowAdded && (DataGridView.CurrentRow.Index == DataGridView.Rows.Count - 1))
                    AddUser(DataGridView[0, DataGridView.CurrentRow.Index].Value.ToString(), DataGridView[1, DataGridView.CurrentRow.Index].Value.ToString());
                else
                    UpdateUser(DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[0].Value.ToString(), DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[0].Value.ToString());
            }
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (ComboBox.Visible)
                return;
            else 
            {
                if (!DataGridView[e.ColumnIndex, e.RowIndex].Value.Equals(e.FormattedValue) && !String.IsNullOrEmpty(e.FormattedValue.ToString()) && !String.IsNullOrEmpty(DataGridView[1, e.RowIndex].Value.ToString())) // Update only if both fields are not empty & if an update is necessary
                {
                    if (_RowAdded && (e.RowIndex == DataGridView.Rows.Count - 1))
                        AddUser(e.FormattedValue.ToString(), DataGridView[1, e.RowIndex].Value.ToString());
                    else 
                        UpdateUser(DataGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), e.FormattedValue.ToString());
                }
            }
        }

        private void UpdateUser(string oldUser, string newUser)
        {
            int accessTypeID=0;
            if (DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[1].Value.Equals("user"))
                accessTypeID = 1;
            else if (DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[1].Value.Equals("admin"))
                accessTypeID = 2;
            else if (DataGridView.Rows[DataGridView.CurrentRow.Index].Cells[1].Value.Equals("superadmin"))
                accessTypeID = 3;

            AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
            service.Update_User(oldUser, newUser, accessTypeID);
            service.Close();

            var result = KryptonMessageBox.Show("User modified !", "User modified",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }

        /***************************************************\
         * Add User                                        *
         *   AddToolStripButton_Click : Create the new Row *
         *   AddUser : Create user in DB                   *
        \***************************************************/

        private void AddToolStripButton_Click(object sender, EventArgs e)
        {
            if (Navigator.SelectedPage.Text.Equals("Users"))
            {
                if (!String.IsNullOrEmpty(DataGridView.Rows[DataGridView.Rows.Count - 1].Cells[0].Value.ToString()) && !String.IsNullOrEmpty(DataGridView.Rows[DataGridView.Rows.Count - 1].Cells[1].Value.ToString())) // Add only if last line is not empty.
                {
                    DataRow r = ((DataTable)DataGridView.DataSource).NewRow();
                    ((DataTable)DataGridView.DataSource).Rows.Add(r);
                    _RowAdded = true;
                }
            }
        }

        private void AddUser(string userName, string accessType)
        {
            int accessTypeID = 0;
            if (accessType.Equals("user"))
                accessTypeID = 1;
            else if (accessType.Equals("admin"))
                accessTypeID = 2;
            else if (accessType.Equals("superadmin"))
                accessTypeID = 3;

            try
            {
                AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                service.Add_User(userName, accessTypeID);
                service.Close();

                var result = KryptonMessageBox.Show("User added !", "User added",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                var result = KryptonMessageBox.Show("Fail to create user", "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                DataGridView.Rows.Remove(DataGridView.Rows[DataGridView.Rows.Count - 1]);
            }

            _RowAdded = false;
        }

        /******************************************\
         * When tab is changed : renamme tooltips *
        \******************************************/

        private void Navigator_SelectedPageChanged(object sender, EventArgs e)
        {
            if (Navigator.SelectedPage.Text.Equals("Users"))
            {
                AddToolStripButton.ToolTipText = "Add an user.";
                RemoveToolStripButton.ToolTipText = "Remove selected user.";
            }
        }

        #endregion
    }
}
