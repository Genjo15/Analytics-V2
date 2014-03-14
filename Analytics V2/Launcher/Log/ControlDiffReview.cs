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
        private List<string> _ControlsList = new List<string>();

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
        private string _CheckChannelAndDateAudienceCriticalAlertsLogs;

        public ControlDiffReview(Dictionary<string, char> toHighlightDico, string noDiffAlerts, string noDiffCriticalAlerts, string diffAlerts, string diffCriticalAlerts, string durationAlerts, string durationCriticalAlerts, string totalAudienceAlerts, string totalAudienceCriticalAlerts, string targetAudienceAlerts, string indicatorAudienceAlerts, string indicatorAudienceCriticalAlerts, string checkChannelAndDateAudienceCriticalAlerts, Boolean isQH)
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
            _CheckChannelAndDateAudienceCriticalAlertsLogs = checkChannelAndDateAudienceCriticalAlerts;

            if (!isQH)
            {
                _ControlsList.Add("CHECK NO DIFFUSION");
                _ControlsList.Add("CHECK NUMBER OF DIFFUSIONS");
                _ControlsList.Add("CHECK DURATION");
            }
            _ControlsList.Add("CHECK TOTAL AUDIENCE");
            _ControlsList.Add("CHECK TARGET AUDIENCE");
            _ControlsList.Add("CHECK INDICATOR AUDIENCE");
            _ControlsList.Add("CHECK CHANNEL AND DATE AUDIENCE");

            foreach (string element in _ControlsList)
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

            // Append first time (for the view when the user directly open the UC)
            if (!isQH) alertsRichTextBox.Text = _NoDiffAlertsLogs;
            else alertsRichTextBox.Text = _TotalAudienceAlertsLogs;
            criticalAlertsRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            criticalAlertsRichTextBox.SelectionColor = Color.Red;
            if (!isQH && !string.IsNullOrEmpty(_NoDiffCriticalAlertsLogs))
                criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _NoDiffCriticalAlertsLogs);
            else if(isQH && !string.IsNullOrEmpty(_TotalAudienceCriticalAlertsLogs))
                criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _TotalAudienceCriticalAlertsLogs);     
        }

        private void checkDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK NO DIFFUSION"))
            {
                ReinitializeRTBs();
                if (!string.IsNullOrEmpty(_NoDiffAlertsLogs))
                    alertsRichTextBox.AppendText(_NoDiffAlertsLogs);
                if (!string.IsNullOrEmpty(_NoDiffCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _NoDiffCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK NUMBER OF DIFFUSIONS"))
            {
                ReinitializeRTBs();
                if (!string.IsNullOrEmpty(_DiffAlertsLogs))
                    alertsRichTextBox.AppendText(_DiffAlertsLogs);
                if (!string.IsNullOrEmpty(_DiffCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _DiffCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK DURATION"))
            {
                ReinitializeRTBs();
                if (!string.IsNullOrEmpty(_DurationAlertsLogs))
                    alertsRichTextBox.AppendText(_DurationAlertsLogs);
                if (!string.IsNullOrEmpty(_DurationCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _DurationCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK TOTAL AUDIENCE"))
            {
                ReinitializeRTBs();
                if (!string.IsNullOrEmpty(_TotalAudienceAlertsLogs))
                    alertsRichTextBox.AppendText(_TotalAudienceAlertsLogs);
                if (!string.IsNullOrEmpty(_TotalAudienceCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _TotalAudienceCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK TARGET AUDIENCE"))
            {
                ReinitializeRTBs();
                if (!string.IsNullOrEmpty(_TargetAudienceAlertsLogs))
                    alertsRichTextBox.AppendText(_TargetAudienceAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK INDICATOR AUDIENCE"))
            {
                ReinitializeRTBs();
                if (!string.IsNullOrEmpty(_IndicatorAudienceAlertsLogs))
                    alertsRichTextBox.AppendText(_IndicatorAudienceAlertsLogs);
                if (!string.IsNullOrEmpty(_IndicatorAudienceCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _IndicatorAudienceCriticalAlertsLogs);
            }

            else if (checkDataGridView.Rows[e.RowIndex].Cells[0].Value.Equals("CHECK CHANNEL AND DATE AUDIENCE"))
            {
                ReinitializeRTBs();
                if (!string.IsNullOrEmpty(_CheckChannelAndDateAudienceCriticalAlertsLogs))
                    criticalAlertsRichTextBox.AppendText("CRITICAL ALERTS :\n\n" + _CheckChannelAndDateAudienceCriticalAlertsLogs);
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
