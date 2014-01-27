using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Analytics_V2
{
    public partial class ProgressBar : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private delegate void processOnProgressBar(float i);            // Delegate type 1.
        private processOnProgressBar _UpdateProgressBarDel;             // Delegate for updating the progress bar.
        private delegate void processOnRichTextBox(String a, String b); // Delegate type 2.
        private processOnRichTextBox _UpdateRichTextBoxDel;             // Delegate for updating the RTB.

        private Boolean _Expand;                                   // Indicates id groupbox expanded or not.
        System.Windows.Forms.ToolTip _MinimiseExpandButtonTooltip; // Tooltip for the MinimiseExpand Button.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public ProgressBar(String name)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Top;
            ProgressBarGroupBox.Values.Heading = name;
            _UpdateProgressBarDel = new processOnProgressBar(UpdateProgressBar);
            _UpdateRichTextBoxDel = new processOnRichTextBox(UpdateRichTextBox);

            _MinimiseExpandButtonTooltip = new ToolTip();
            _MinimiseExpandButtonTooltip.SetToolTip(ExpandMinimizeButton, "Show details");


            _Expand = false;
            this.Height = 54;
            ProgressBarGroupBox.Height = 54;
            
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*******************************\
         * Update Progress Bar or RTB. *
        \*******************************/

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
                    ProcessSummaryRichTextBox.AppendText(message +"\n");
                    break;

                case "fail":
                    ProcessSummaryRichTextBox.SelectionColor = Color.Red;
                    ProcessSummaryRichTextBox.AppendText(message + "\n");
                    break;
            }
        }

        /**********************************************\
         * Event for clicking Expand/minimize button. *
        \**********************************************/

        private void ExpandMinimizeButton_Click(object sender, EventArgs e)
        {
            if (!_Expand)
            {
                ExpandMinimizeButton.Image = global::Analytics_V2.Properties.Resources.ArrowUp;
                _MinimiseExpandButtonTooltip.SetToolTip(ExpandMinimizeButton, "Hide details");
                this.Height = 251;
                ProgressBarGroupBox.Height = 251;
                _Expand = true;
            }

            else if (_Expand)
            {
                ExpandMinimizeButton.Image = global::Analytics_V2.Properties.Resources.ArrowDown;
                _MinimiseExpandButtonTooltip.SetToolTip(ExpandMinimizeButton, "Show details");
                this.Height = 54;
                ProgressBarGroupBox.Height = 54;
                _Expand = false;
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

        #endregion

        


    }
}
