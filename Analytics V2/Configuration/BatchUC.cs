using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Analytics_V2
{
    public partial class BatchUC : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        ToolTip _BackButtonTooltip;
        ToolTip _CreateButtonTooltip;

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public BatchUC()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            SplitContainer.Panel2Collapsed = true;
            SplitContainer.Panel2.Hide();

            _BackButtonTooltip = new ToolTip();
            _BackButtonTooltip.SetToolTip(BackButton, "Cancel");
            _CreateButtonTooltip = new ToolTip();
            _CreateButtonTooltip.SetToolTip(CreateButton, "Create batch");

        }

        #endregion

        /****************************************************** Methods *******************************************************/

        /***************************\
         * Add Batch button click  *
        \***************************/

        private void AddToolStripButton_Click(object sender, EventArgs e)
        {
            SplitContainer.Panel2Collapsed = false;
            SplitContainer.Panel2.Show();
            Form form = (Form)this.Parent;
            form.Size = new System.Drawing.Size(900, 400);
        }

        /************************\
         * Return button click  *
        \************************/

        private void BackButton_Click(object sender, EventArgs e)
        {
            SplitContainer.Panel2Collapsed = true;
            SplitContainer.Panel2.Hide();
            Form form = (Form)this.Parent;
            form.Size = new System.Drawing.Size(600, 400);
        }
    }
}
