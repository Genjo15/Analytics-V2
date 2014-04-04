using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ComponentFactory.Krypton.Toolkit;

namespace Analytics_V2
{
    public partial class ProgressBar : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private int _ID; // Control ID.

        private delegate void processOnProgressBar(float i);            // Delegate type 1.
        private processOnProgressBar _UpdateProgressBarDel;             // Delegate for updating the progress bar.
        private delegate void processOnRichTextBox(String a, String b); // Delegate type 2.
        private processOnRichTextBox _UpdateRichTextBoxDel;             // Delegate for updating the RTB.
        private delegate void processOnProgressBar2(string str);        // Delegate type 3.
        private processOnProgressBar2 _DisplayConfigProcessTimeDel;     // Delegate for adding the config process time.
        Delegate _AbortThreadDel;

        private Boolean _Expand;                                   // Indicates id groupbox expanded or not.
        System.Windows.Forms.ToolTip _MinimiseExpandButtonTooltip; // Tooltip for the MinimiseExpand Button.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public ProgressBar(String name, int id, Delegate del)
        {
            InitializeComponent();

            _ID = id;
            _AbortThreadDel = del;

            this.Dock = System.Windows.Forms.DockStyle.Top;

            ProgressBarGroupBox.Values.Heading = name;
            _UpdateProgressBarDel = new processOnProgressBar(UpdateProgressBar);
            _UpdateRichTextBoxDel = new processOnRichTextBox(UpdateRichTextBox);
            _DisplayConfigProcessTimeDel = new processOnProgressBar2(DisplayConfigProcessTime);

            _MinimiseExpandButtonTooltip = new ToolTip();
            _MinimiseExpandButtonTooltip.SetToolTip(ExpandMinimize, "Show details");
            new ToolTip().SetToolTip(StopButton, "Stop Process !");

            _Expand = false;

            this.Height = 47;
            ProgressBarGroupBox.Height = 47;
            
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /**************************************************************\
         * Update Progress Bar or RTB.                                *
         *     - The progress bar and his header                      *
         *     - Just the header (for adding the whole process time). *
         *     - The RTB.                                             *
        \**************************************************************/

        private void UpdateProgressBar(float progress)
        {
            Bar.Value = (int)progress;


            ProgressBarGroupBox.Values.Heading = ProgressBarGroupBox.Values.Heading.Split(new string[] { " - " }, StringSplitOptions.None)[0] + " - " + progress + "%";
        }

        private void UpdateRichTextBox(String type, String message)
        {
            switch (type)
            {
                case "title":
                    ProcessSummaryRichTextBox.SelectionColor = Color.Brown;
                    ProcessSummaryRichTextBox.AppendText("\n-= " + message + " =-\n\n");
                    break;

                case "function":
                    ProcessSummaryRichTextBox.SelectionColor = Color.Black;
                    ProcessSummaryRichTextBox.AppendText(message);
                    break;

                case "complete":
                    ProcessSummaryRichTextBox.SelectionColor = Color.Green;
                    ProcessSummaryRichTextBox.AppendText(message);
                    break;

                case "time":
                    ProcessSummaryRichTextBox.SelectionColor = Color.CornflowerBlue;
                    ProcessSummaryRichTextBox.AppendText(" - Time : " + message + "\n");
                    break;

                case "fail":
                    ProcessSummaryRichTextBox.SelectionColor = Color.Red;
                    ProcessSummaryRichTextBox.AppendText(message + "\n");
                    break;

            }
        }

        private void DisplayConfigProcessTime(string time)
        {
            ProgressBarGroupBox.Values.Heading = ProgressBarGroupBox.Values.Heading + " - Time : " + time;
        }

        /**********************************************\
         * Event for clicking Expand/minimize button. *
        \**********************************************/

        private void ExpandMinimizeButton_Click(object sender, EventArgs e)
        {
            if (!_Expand)
            {
                ExpandMinimize.StateCommon.Back.Image = global::Analytics_V2.Properties.Resources.Arrow_Up;
                _MinimiseExpandButtonTooltip.SetToolTip(ExpandMinimize, "Hide details");
                this.Height = 244;
                ProgressBarGroupBox.Height = 244;
                _Expand = true;
            }

            else if (_Expand)
            {
                ExpandMinimize.StateCommon.Back.Image = global::Analytics_V2.Properties.Resources.Arrow_Down;
                _MinimiseExpandButtonTooltip.SetToolTip(ExpandMinimize, "Show details");
                this.Height = 47;
               ProgressBarGroupBox.Height = 47;
                _Expand = false;
            }
        }

        /******************************************\
         * Event for clicking Stop Process button *
        \******************************************/

        private void StopButton_Click(object sender, EventArgs e)
        {
            var result = KryptonMessageBox.Show("Do you really want to stop the process?" , "Stop the process",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _AbortThreadDel.DynamicInvoke(_ID);
                //Bar.ForeColor = Color.IndianRed;
            }
        }

        #endregion

        #region Accessors

        public Delegate Get_UpdateProgressBarDel()
        {
            return _UpdateProgressBarDel;
        }

        public Delegate Get_UpdateRichTextBoxDel()
        {
            return _UpdateRichTextBoxDel;
        }

        public Delegate Get_DisplayConfigTimeDel()
        {
            return _DisplayConfigProcessTimeDel;
        }

        #endregion  
    }
}
