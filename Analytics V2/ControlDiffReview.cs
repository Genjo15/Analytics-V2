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
    public partial class ControlDiffReview : UserControl
    {
        private List<string> controlsList = new List<string>();

        private string _NoDiffAlerts;
        private string _NoDiffCriticalAlerts;

        public ControlDiffReview(Dictionary<string, char> toHighlightDico, string noDiffAlerts, string noDiffCriticalAlerts)
        {
            InitializeComponent();

            _NoDiffAlerts = noDiffAlerts;
            _NoDiffCriticalAlerts = noDiffCriticalAlerts;

            controlsList.Add("CHECK NO DIFFUSION");
            controlsList.Add("CHECK NUMBER OF DIFFUSIONS");
            controlsList.Add("CHECK DURATION");
            controlsList.Add("CHECK TOTAL AUDIENCE");
            controlsList.Add("CHECK TARGET AUDIENCE");
            controlsList.Add("CHECK INDICATOR AUDIENCE");

            foreach (string element in controlsList)
            {
                DataGridViewRow row = (DataGridViewRow)checkDataGridView.Rows[0].Clone();
                row.Cells[0].Value = element;
                if (toHighlightDico.ContainsKey(element))
                {
                    row.Cells[0].Style.ForeColor = System.Drawing.Color.Red;
                    row.Cells[0].Style.Font = new Font(checkDataGridView.Font, FontStyle.Bold);
                }
                else
                {
                    row.Cells[0].Style.ForeColor = System.Drawing.Color.Black;
                    row.Cells[0].Style.Font = new Font(checkDataGridView.Font, FontStyle.Regular);
                }

                checkDataGridView.Rows.Add(row);
            }

            checkDataGridView.AllowUserToAddRows = false;

            alertsRichTextBox.Text = _NoDiffAlerts;
            criticalAlertsRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            criticalAlertsRichTextBox.SelectionColor = Color.Red;
            if (!string.IsNullOrEmpty(_NoDiffCriticalAlerts))
                criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _NoDiffCriticalAlerts);


           
        }
   
    }
}
