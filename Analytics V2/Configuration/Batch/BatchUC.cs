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
    public partial class BatchUC : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        List<BatchElement> _BatchElements;

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public BatchUC()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            SplitContainer.Panel2Collapsed = true;
            SplitContainer.Panel2.Hide();

            _BatchElements = new List<BatchElement>();

            //Test
            BatchElement test = new BatchElement();
            test.Config.Text = "1";
            BatchElementsGroupBox.Panel.Controls.Add(test);
            test.Dock = DockStyle.Top;

            BatchElement test2 = new BatchElement();
            test2.Config.Text = "2";
            BatchElementsGroupBox.Panel.Controls.Add(test2);
            test2.Dock = DockStyle.Top;
            test2.BringToFront();

            BatchElement test3 = new BatchElement();
            test3.Config.Text = "3";
            BatchElementsGroupBox.Panel.Controls.Add(test3);
            test3.Dock = DockStyle.Top;
            test3.BringToFront();

            BatchElement test4 = new BatchElement();
            test4.Config.Text = "4";
            BatchElementsGroupBox.Panel.Controls.Add(test4);
            test4.Dock = DockStyle.Top;
            test4.BringToFront();

            BatchElement test5 = new BatchElement();
            test5.Config.Text = "5";
            BatchElementsGroupBox.Panel.Controls.Add(test5);
            test5.Dock = DockStyle.Top;
            test5.BringToFront();

        }

        #endregion

        /****************************************************** Methods *******************************************************/

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
        }

        /************************\
         * Return button click  *
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

                foreach (BatchElement element in _BatchElements)
                    element.Dispose();

            }
        }

        /*******************\
         * Save new batch  *
        \*******************/

        private void SaveButtonSpec_Click(object sender, EventArgs e)
        {
            // Check if each element is correctly
        }
    }
}
