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
    public partial class SpecificTools : UserControl
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private List<String> _SpecificToolsList;

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public SpecificTools()
        { 
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            _SpecificToolsList = new List<string>();
            FillSpecificToolsList();
            this.SpecificToolsListBox.DataSource = _SpecificToolsList;
            
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /***********************\
         * Fill list of tools. *
        \***********************/

        private void FillSpecificToolsList()
        {
            _SpecificToolsList.Add("Consistency Checking *BETA*");
            _SpecificToolsList.Add("Consistency Checking - PROGRAMMES");
            _SpecificToolsList.Add("Consistency Checking - QH");
            _SpecificToolsList.Add("Traitements QH");
            _SpecificToolsList.Add("Datachecking");
            _SpecificToolsList.Add("Data Inspector");
            _SpecificToolsList.Add("Gestion de fichiers");
            _SpecificToolsList.Add("XML Editor (beta)");
        }

        public void LaunchSpecificTool(String tool)
        {
            switch (tool)
            {
                case "Consistency Checking *BETA*":
                    try
                    {
                        Consistency.Consistency consistencyCheckingBetaProcess = new Consistency.Consistency(Properties.Settings.Default.consistency_checking_path);
                        consistencyCheckingBetaProcess.StartPosition = FormStartPosition.CenterScreen;
                        consistencyCheckingBetaProcess.Show();
                    }
                    catch
                    {
                        KryptonMessageBox.Show("Unable to launch Consistency Checking, please verify your path.", "Error when launching Consistency Checking",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                    break;
                case "Consistency Checking - PROGRAMMES":
                    try
                    {
                        Konsistency.Konsistency consistencyCheckingProgramsProcess = new Konsistency.Konsistency();
                        consistencyCheckingProgramsProcess.Konsis_PRGLoader();
                    }
                    catch
                    {
                        KryptonMessageBox.Show("Error! Unable to launch Consistency Checking, please check your export file.", "Error when launching Consistency Checking",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                    break;

                case "Consistency Checking - QH":
                    try
                    {
                        Konsistency.Konsistency consistencyCheckingTimebandsProcess = new Konsistency.Konsistency();
                        consistencyCheckingTimebandsProcess.Konsis_QHLoader();
                    }
                    catch
                    {
                        KryptonMessageBox.Show("Error! Unable to launch Consistency Checking, please check your export file.", "Error when launching Consistency Checking",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                    break;

                case "Traitements QH": Format_QH.Format_QH formatQHProcess = new Format_QH.Format_QH();
                                       formatQHProcess.StartPosition = FormStartPosition.CenterScreen;
                                       formatQHProcess.Show();
                                       break;

                case "Datachecking": DataChecking.DataCheckingForm dataCheckingProcess = new DataChecking.DataCheckingForm();
                                     dataCheckingProcess.StartPosition = FormStartPosition.CenterScreen;
                                     dataCheckingProcess.Show();
                                     break;

                case "Data Inspector": Calcul.calcul calculProcess = new Calcul.calcul();
                                       calculProcess.StartPosition = FormStartPosition.CenterScreen;
                                       calculProcess.Show();
                                       break;

                case "Gestion de fichiers": mybreakerII.mybreakerII myBreakerIIProcess = new mybreakerII.mybreakerII();
                                            myBreakerIIProcess.StartPosition = FormStartPosition.CenterScreen;
                                            myBreakerIIProcess.Show();
                                            break;

                case "Productions Mediaset": 
                                             break;

                case "XML Editor (beta)": XMLEditor.Editor XMLEditorProcess = new XMLEditor.Editor();
                                          XMLEditorProcess.RunControl();
                                          break;
            }
        }

        #endregion



    }
}
