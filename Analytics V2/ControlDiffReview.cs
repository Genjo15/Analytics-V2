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

        private string _NoDiffAlertsLogs;
        private string _NoDiffCriticalAlertsLogs;
        private string _DiffAlertsLogs;
        private string _DiffCriticalAlertsLogs;
        private string _DurationAlertsLogs;
        private string _DurationCriticalAlertsLogs;
        private string _TotalAudienceAlertsLogs;
        private string _TotalAudienceCriticalAlertsLogs;
        private string _TargetAudienceAlertsLogs;
        private string _IndicatorAudienceAlertsLogs;
        private string _IndicatorAudienceCriticalAlertsLogs;

        public ControlDiffReview(Dictionary<string, char> toHighlightDico, string noDiffAlerts, string noDiffCriticalAlerts, string diffAlerts, string diffCriticalAlerts, string durationAlerts, string durationCriticalAlerts, string totalAudienceAlerts, string totalAudienceCriticalAlerts, string targetAudienceAlerts, string indicatorAudienceAlerts, string indicatorAudienceCriticalAlerts)
        {
            InitializeComponent();

            _NoDiffAlertsLogs = noDiffAlerts;
            _NoDiffCriticalAlertsLogs = noDiffCriticalAlerts;
            _DiffAlertsLogs = diffAlerts;
            _DiffCriticalAlertsLogs = diffCriticalAlerts;
            _DurationAlertsLogs = durationAlerts;
            _DurationCriticalAlertsLogs = durationCriticalAlerts;
            _TotalAudienceAlertsLogs = totalAudienceAlerts;
            _TotalAudienceCriticalAlertsLogs = totalAudienceCriticalAlerts;
            _TargetAudienceAlertsLogs = targetAudienceAlerts;
            _IndicatorAudienceAlertsLogs = indicatorAudienceAlerts;
            _IndicatorAudienceCriticalAlertsLogs = indicatorAudienceCriticalAlerts;

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

            alertsRichTextBox.Text = _NoDiffAlertsLogs;
            criticalAlertsRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            criticalAlertsRichTextBox.SelectionColor = Color.Red;
            if (!string.IsNullOrEmpty(_NoDiffCriticalAlertsLogs))
                criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _NoDiffCriticalAlertsLogs);


           
        }

        private void checkDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK NO DIFFUSION"))
            {
                ReinitializeRTBs();
                alertsRichTextBox.AppendText(_NoDiffAlertsLogs);
                if (!string.IsNullOrEmpty(_NoDiffCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _NoDiffCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK NUMBER OF DIFFUSIONS"))
            {
                ReinitializeRTBs();
                alertsRichTextBox.AppendText(_DiffAlertsLogs);
                if (!string.IsNullOrEmpty(_DiffCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _DiffCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK DURATION"))
            {
                ReinitializeRTBs();
                alertsRichTextBox.AppendText(_DurationAlertsLogs);
                if (!string.IsNullOrEmpty(_DurationCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _DurationCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK TOTAL AUDIENCE"))
            {
                ReinitializeRTBs();
                alertsRichTextBox.AppendText(_TotalAudienceAlertsLogs);
                if (!string.IsNullOrEmpty(_TotalAudienceCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _TotalAudienceCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK TARGET AUDIENCE"))
            {
                ReinitializeRTBs();
                alertsRichTextBox.AppendText(_TargetAudienceAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK INDICATOR AUDIENCE"))
            {
                ReinitializeRTBs();
                alertsRichTextBox.AppendText(_IndicatorAudienceAlertsLogs);
                if (!string.IsNullOrEmpty(_IndicatorAudienceCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _IndicatorAudienceCriticalAlertsLogs);
            }
        }

        private void ReinitializeRTBs()
        {
            alertsRichTextBox.Clear();
            criticalAlertsRichTextBox.Clear();
            criticalAlertsRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            criticalAlertsRichTextBox.SelectionColor = Color.Red;
        }
    }
}
