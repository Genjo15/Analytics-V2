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
using System.Text.RegularExpressions;

namespace Analytics_V2
{
    public partial class LogsGrid : UserControl
    {
        /***************************************************** Variables *****************************************************/
        
        #region Variables

        private Boolean _PreProcess;        // Boolean which indicates if preprocess is enabled or not.
        private Boolean _Process;           // Boolean which indicates if process is enabled or not.
        private Boolean _Control;           // Boolean which indicates if control is enabled or not.

        private string _InputFileName;
        private string _DatamodRootPath;
        private string _DatamodLogPath;
        private string _LinedelPath;
        private string _DateformatPath;
        private string _ControlDiffPath;
        private string _DatacheckerPath;
        private string _HCPath;

        private int _NumberOfDays;
        private int _NumberOfTargets;

        private KryptonForm _ControlDiffForm;

        private int _NoDiffAlerts = 0;
        private int _NoDiffCriticalAlerts = 0;
        private int _NoDiffFormatErrors = 0; 
        private string _NoDiffAlertsLogs;
        private string _NoDiffCriticalAlertsLogs;

        private int _DiffAlerts = 0;
        private int _DiffCriticalAlerts = 0;
        private int _DiffFormatErrors = 0;
        private string _DiffAlertsLogs;
        private string _DiffCriticalAlertsLogs; 

        private int _DurationAlerts = 0;
        private int _DurationCriticalAlerts = 0; 
        private int _DurationFormatErrors = 0;
        private string _DurationAlertsLogs;
        private string _DurationCriticalAlertsLogs;

        private int _TotalAudienceAlerts = 0;
        private int _TotalAudienceCriticalAlerts = 0;
        private int _TotalAudienceFormatErrors = 0;
        private string _TotalAudienceAlertsLogs;
        private string _TotalAudienceCriticalAlertsLogs;

        private int _TargetAudienceAlerts = 0;
        private int _TargetAudienceFormatErrors = 0;
        private string _TargetAudienceAlertsLogs;

        private int _IndicatorAudienceAlerts = 0;
        private int _IndicatorAudienceCriticalAlerts = 0;
        private int _IndicatorAudienceFormatErrors = 0;
        private string _IndicatorAudienceAlertsLogs;
        private string _IndicatorAudienceCriticalAlertsLogs;

        private string _CheckChannelAndDateAudienceCriticalAlertsLogs;
        private int _CheckChannelAndDateAudienceCriticalAlerts;

        #endregion


        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public LogsGrid(String name, List<Process> processList, string dmPath, string inputFile, int targetsNumber, Boolean preProcess, Boolean process, Boolean control)
        {

            InitializeComponent();
            InitializeControlDiffForm();
            DataGridView.Name = name;
            _DatamodRootPath = "";
            _InputFileName = inputFile;
            _DatacheckerPath = Path.GetDirectoryName(dmPath) + "\\" + Path.GetFileNameWithoutExtension(dmPath) + "_dc.log";
            if (File.Exists(Path.GetDirectoryName(dmPath) + "\\" + Path.GetFileNameWithoutExtension(dmPath) + "_hc.log"))
                _HCPath = Path.GetDirectoryName(dmPath) + "\\" + Path.GetFileNameWithoutExtension(dmPath) + "_hc.log";
            else _HCPath = null;

            _PreProcess = preProcess;
            _Process = process;
            _Control = control;

            _NumberOfDays = NumberOfDays(dmPath);
            _NumberOfTargets = targetsNumber;

            _DatamodRootPath = System.IO.Directory.GetParent(dmPath).FullName + '\\';

            _DatamodLogPath = Path.GetDirectoryName(dmPath) + "\\" + Path.GetFileNameWithoutExtension(dmPath) + ".log";

            this.Dock = System.Windows.Forms.DockStyle.Fill;
            FillGrid(processList);

            DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            _NoDiffAlertsLogs = "";
            _NoDiffCriticalAlertsLogs = "";
            _DiffAlertsLogs = "";
            _DiffCriticalAlertsLogs = "";
            _DurationAlertsLogs = "";
            _DurationCriticalAlertsLogs = "";
            _TotalAudienceAlertsLogs = "";
            _TotalAudienceCriticalAlertsLogs = "";
            _TargetAudienceAlertsLogs = "";
            _IndicatorAudienceAlertsLogs = "";
            _IndicatorAudienceCriticalAlertsLogs = "";
            _CheckChannelAndDateAudienceCriticalAlertsLogs = "";
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /***********************************\
         * Fill grid:                      *
         * - Instanciate row               *
         * - Fill it by analysing cotnrol  *
         * - add it to the grid.           *
        \***********************************/

        private void FillGrid(List<Process> processList)
        {
            for (int i = 0; i < processList.Count; i++)
            {
                if ((_PreProcess && processList[i].Get_OrderId() < 0) || (_Process && processList[i].Get_OrderId() < 100 && processList[i].Get_OrderId() > 0) || (_Control && processList[i].Get_OrderId() > 100))
                {
                    int occurrenceCounter = 0;
                    DataGridViewRow row = (DataGridViewRow)DataGridView.Rows[0].Clone();
                    row.Cells[0].Value = processList[i].Get_OrderId();
                    row.Cells[1].Value = processList[i].Get_Name();

                    // Check how many time the process has already been called (for example, if there are several COLUMNS, to know which one to analyze in the log file)
                    for (int j = 0; j < i + 1; j++)
                        if (processList[j].Get_Name().Equals(processList[i].Get_Name()))
                            occurrenceCounter++;

                    AnalyzeLog(processList[i].Get_Name(), row.Cells[2], row.Cells[3], occurrenceCounter);
                    if (processList[i].Get_OrderId() > 100)
                        DataGridView.Rows.Insert(0, row);
                    else DataGridView.Rows.Add(row);
                }
            }
           
            //////////
            // For HC

            if (File.Exists(_HCPath))
            {
                DataGridViewRow row = (DataGridViewRow)DataGridView.Rows[0].Clone();
                row.Cells[1].Value = "HC";
                AnalyzeHC(row.Cells[2], row.Cells[3], _HCPath);
                DataGridView.Rows.Insert(0, row);
            }

            //DataGridView.SelectionMode.
            
            DataGridView.AllowUserToAddRows = false;
        }

        private void InitializeControlDiffForm()
        {
            _ControlDiffForm = new KryptonForm();
            _ControlDiffForm.ClientSize = new System.Drawing.Size(950, 825);
            _ControlDiffForm.StartPosition = FormStartPosition.CenterScreen;
            _ControlDiffForm.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            _ControlDiffForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HideControlDiffForm);
        }

        /**********************************************\
         * For each process, analyze it :             *
         *   - Define the errors catched in dll.      *
         *   - Analyze process.                       *
         *   - Display the result.                    *
        \**********************************************/

        private void AnalyzeLog(String name, DataGridViewCell commentsCell, DataGridViewCell checkCell, int occurrenceNumber)
        {
            switch (name)
            {
                case "XLS2TXT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Missing or wrong parameters, date format was not set.");
                    listErrors.Add("Missing parameters, time format was not set.");
                    listErrors.Add("Error in custom format: ");
                    listErrors.Add("Error in formula: ");
                    listErrors.Add("/!\\ Error: ");

                    AnalyzeProcessLog("------------ XLS2TXT ------------", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodRootPath + "XLS2TXT_Process.log");

                    break;
                }

                case "XML2TXT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("-> Error: ");

                    AnalyzeProcessLog("------------ XML2TXT ------------", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodRootPath + "XML2TXT_Process.log");

                    break;
                }

                case "XMLMERGE":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error when reading the list of path");
                    listErrors.Add("Error when parsing  datatable");
                    listErrors.Add("Entete different dans le fichier : ");
                    listErrors.Add("Error detected when parsing files ");
                    listErrors.Add("Error when creating new file.xml ");
                
                    AnalyzeProcessLog("------------ XMLMERGE ------------", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                
                    break;
                }

                case "FILESPLIT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error (level2) : ");
                    listErrors.Add("Error (level1) : ");

                    AnalyzeProcessLog("------------ FILESPLIT ------------", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodRootPath + "FILESPLIT_Process.log");

                    break;
                }

                case "COLUMNSCONCAT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error ! Bad RefColums");

                    AnalyzeProcessLog("== COLUMNSCONCAT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);

                    break;
                }

                case "LINESCONCAT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error while matching file masks, check file names.\n-> File: ");
                    listErrors.Add("Incorrect file mask, check file names.\n-> File: ");
                    listErrors.Add(" LineConcat Failed.");
                    listErrors.Add("Error ! Line ");
                    listErrors.Add("Error ! Bad RefColums");

                    AnalyzeProcessLog("== LINESCONCAT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);

                    break;
                }

                case "REPLACE":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not operate replacement - Error: ");
                    AnalyzeProcessLog("== REPLACE PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "CUT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not operate cut - Error: ");
                    AnalyzeProcessLog("== CUT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "COLUMNS":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not operate concatenation.");
                    AnalyzeProcessLog("== COLUMNS PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "COLUMNDELETER":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error when creating  the new table :");
                    AnalyzeProcessLog("== COLUMNDELETER PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "COLUMNMOVER":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error detected in line :");
                    AnalyzeProcessLog("== COLUMNMOVER PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "EXPAND":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not operate expand - Error: ");
                    AnalyzeProcessLog("== EXPAND PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "FILTER":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not operate filter - Error: ");
                    AnalyzeProcessLog("== FILTER PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "DATECONVERT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(" could not convert expression: ");
                    listErrors.Add(" could not operate conversion: ");
                    AnalyzeProcessLog("== DATECONVERT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "TRANSLATE":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Failed to load dictionnary, error: ");
                    listErrors.Add(" could not translate expression ");
                    listErrors.Add(" could not operate translation: ");
                    listErrors.Add("Failed to save dictionnary, error: ");
                    listErrors.Add("Failed to save dictionnary during process, error: ");
                    AnalyzeProcessLog("== TRANSLATE PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "TRANSCRIPT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(" could not transcript expression: ");
                    listErrors.Add(" could not operate transcription: ");
                    AnalyzeProcessLog("== TRANSCRIPT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "TIMEFORMAT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error ! Bad Parameter : ");
                    listErrors.Add("Error ! line ");
                    AnalyzeProcessLog("== TIMEFORMAT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "TIMECONVERT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error ! line ");
                    AnalyzeProcessLog("== TIMECONVERT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "WRITE":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error ! Bad parameters : str = ");
                    listErrors.Add("Error ! Bad Columns to treat");
                    AnalyzeProcessLog("== WRITE PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "COPY":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error, line:");
                    AnalyzeProcessLog("== COPY PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "CELLCOPY":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not find value to copy - Error: ");
                    listErrors.Add(", could not copy value - Error: ");
                    AnalyzeProcessLog("== CELLCOPY PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "VALUECHECKER":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not operate control. Checked value might not be an integer or be empty.");
                    listErrors.Add(", replaced:");
                    AnalyzeProcessLog("== VALUECHECKER PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "VALUECORRECTOR":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", could not operate correction: ");
                    listErrors.Add(" was corrected value:");
                    AnalyzeProcessLog("== VALUECORRECTOR PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "QHFORMAT":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Missing channel ");
                    listErrors.Add("Error ! Line ");
                    AnalyzeProcessLog("== QHFORMAT PROCESS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "REMOVER":
                {
                    List<string> listErrors = new List<string>();
                    AnalyzeProcessLog("== REMOVE DOUBLONS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "CALCUL":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error ! Addition failed, line ");
                    listErrors.Add("Error ! Soustraction failed, line ");
                    listErrors.Add("Error ! Multiplication failed, line ");
                    listErrors.Add("Error ! Division failed, line ");
                    listErrors.Add("Error ! Bad Columns to treat");
                    AnalyzeProcessLog("== CALCUL ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "LEVELS":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("-> Error parsing date time identifiers, message: ");
                    listErrors.Add("-> Error on channel and day identifier: ");
                    listErrors.Add("-> Warning on channel|day|start|end identifiers: ");
                    AnalyzeProcessLog("== LEVELS ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);
                    break;
                }

                case "LINEDEL":
                {
                    DirectoryInfo root = new DirectoryInfo(_DatamodRootPath);
                    FileInfo[] files = root.GetFiles();
                    List<string> listErrors = new List<string>();
                    listErrors.Add(", was deleted: ");

                    foreach (FileInfo file in files)
                    {
                        if (file.FullName.Contains(Path.GetFileNameWithoutExtension(_InputFileName)) && file.FullName.Contains("new_ld.log"))
                            _LinedelPath = file.FullName;
                    }

                    AnalyzeProcessLog("------------ LINEDELETER ------------", listErrors, commentsCell, checkCell, occurrenceNumber, _LinedelPath);

                    break;
                }

                case "DUPLICATES":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("-> Duplicated line removed index: ");

                    AnalyzeProcessLog("== DUPLICATES ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);

                    break;
                }

                case "DATEFORMAT":
                {
                    DirectoryInfo root = new DirectoryInfo(_DatamodRootPath);
                    FileInfo[] files = root.GetFiles();
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error ! line ");

                    foreach (FileInfo file in files)
                    {
                        if (file.FullName.Contains(Path.GetFileNameWithoutExtension(_InputFileName)) && file.FullName.Contains("DATEFORMAT.log"))
                            _DateformatPath = file.FullName;
                    }

                    AnalyzeProcessLog("-                 DATE FORMAT                           -", listErrors, commentsCell, checkCell, occurrenceNumber, _DateformatPath);

                    break;
                }

                case "DATACHECKER":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Unable to split typo, error: ");
                    listErrors.Add("Unable to check empties on line: ");
                    listErrors.Add("Unable to check line: ");
                    listErrors.Add(" - Field is empty.");
                    listErrors.Add(" - Unknown Channel Id: ");
                    listErrors.Add(" - Unknown Typology Id: ");

                    AnalyzeDatachecker(commentsCell, checkCell, _DatacheckerPath);

                    break;
                }

                case "CONTROLDIFF":
                {
                    DirectoryInfo root = new DirectoryInfo(_DatamodRootPath);
                    FileInfo[] files = root.GetFiles();

                    foreach (FileInfo file in files)
                    {
                        if (file.FullName.Contains(Path.GetFileNameWithoutExtension(_InputFileName)) && file.FullName.Contains("CONTROLDIFF.log"))
                        {
                            _ControlDiffPath = file.FullName;
                        }
                    }

                    AnalyzeControlDiffOrQHNumbers(commentsCell, checkCell, _ControlDiffPath, false);

                    break;
                }

                case "QHNUMBERS":
                {
                    AnalyzeControlDiffOrQHNumbers(commentsCell, checkCell, _DatamodLogPath, true);
                    break;
                }

                case "TOTALTVCONTROL":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Alert ! Line ");
                    listErrors.Add("Error ! Line ");
                    listErrors.Add("Error ! TTV Channel");

                    AnalyzeProcessLog("== CONTROL TOTAL TV ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);

                    break;
                }

                case "TXT2XML":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error on line : ");
                    listErrors.Add("Error when creating xml files");
                    listErrors.Add("Missing element in line");
                    listErrors.Add("Could not parse skipped lines indexes");

                    AnalyzeProcessLog("== TXT2XML ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);

                    break;
                }

                case "TRANSPOSE":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Error detected during the reception lines");
                    listErrors.Add("Error detected when creating the list of string ");
                    listErrors.Add("Error detected when creating column ");
                    listErrors.Add("Error detected when creating row ");
                    listErrors.Add("Error detected when creating colum name");
                    listErrors.Add("Error detected when assigning value into  the table");
                    listErrors.Add("Error detected in function find_max_colum_col");
                    listErrors.Add("Error detected in function find_repetition_col");
                    listErrors.Add("Error detected in function strlen_col");

                    AnalyzeProcessLog("== TRANSPOSE ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);

                    break;
                }

                default:
                    commentsCell.Style.BackColor = Color.Transparent;
                    break;

            }
        }

        /*************************************\
         * Analyze the process :             *
         *   - Define graphical display.     *
         *   - Open the specific log.        *
         *   - count warnings/infos.         *
         *   - display them;                 *
        \*************************************/

        private void AnalyzeProcessLog(string processToAnalyze, List<string> errorMessages, DataGridViewCell commentsCell, DataGridViewCell checkCell, int occurrenceNumber, string logPath)
        {
            int warningCounter = 0;
            int informationOrCriticalCounter = 0;
            int occurrences = 0;
            Boolean readingTrigger = false;

            commentsCell.Style.BackColor = Color.Transparent;
            commentsCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            checkCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            /* 1ST PART : FOR EACH LINE OF LOG, COUNT ERRORS/WARNINGS/CRITICAL ERRORS ACCORDING TO ANALYZED PROCESS AND ERRORMESSAGES */

            try
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(logPath);

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(processToAnalyze))
                    {
                        occurrences++;
                        if (occurrences == occurrenceNumber)
                        {
                            readingTrigger = true;
                        }
                    }

                    else if (line.Contains("PROCESS ==") && readingTrigger)
                        readingTrigger = false;

                    if (readingTrigger)
                    {
                        foreach (string error in errorMessages)
                        {
                            if (line.Contains(error))
                                warningCounter++;

                            // Add here cases of critical error
                            if ((line.Contains(error) && error.Equals(", replaced:") && processToAnalyze.Contains("VALUECHECKER")) 
                                || (line.Contains(error) && error.Equals(" was corrected value:") && processToAnalyze.Contains("VALUECORRECTOR")) 
                                || (line.Contains(error) && error.Equals(", was deleted: ") && processToAnalyze.Contains("LINEDEL"))
                                || (line.Contains(error) && error.Equals(" - Field is empty.") && processToAnalyze.Contains("DataChecking"))
                                || (line.Contains(error) && error.Equals("-> Duplicated line removed index: ") && processToAnalyze.Contains("DUPLICATES"))
                                || (line.Contains(error) && processToAnalyze.Contains("XML2TXT"))
                                || (line.Contains(error) && error.Equals("Error when creating xml files") && processToAnalyze.Contains("TXT2XML"))
                                || (line.Contains(error) && processToAnalyze.Contains("COLUMNDELETER"))
                                || (line.Contains(error) && processToAnalyze.Contains("COLUMNMOVER"))
                                || (line.Contains(error) && processToAnalyze.Contains("XMLMERGE"))
                                || (line.Contains(error) && error.Equals("Error ! TTV Channel") && processToAnalyze.Equals("== CONTROL TOTAL TV =="))
                                )
                            {
                                warningCounter--;
                                informationOrCriticalCounter++;
                            }
                        }
                    }
                }

                file.Close();
            }
 
            catch { }


            /* 2ND PART : FILL RESULTS IN CELLS (WARNINGS,CRITICAL ERRORS, INFOS), ACCORDING TO COUNTERS AND PROCESSES */

            if (warningCounter > 0)
            {
                commentsCell.Value = "Alerts : " + warningCounter;
                checkCell.Value = "Warning";
                checkCell.Style.ForeColor = System.Drawing.Color.DarkOrange;
                commentsCell.Style.ForeColor = System.Drawing.Color.DarkOrange;

                if (processToAnalyze.Contains("VALUECHECKER") && informationOrCriticalCounter > 0)
                {
                    commentsCell.Value = "Alerts : " + warningCounter + ". Replacements : " + informationOrCriticalCounter;
                }
                else if(processToAnalyze.Contains("VALUECORRECTOR") && informationOrCriticalCounter > 0)
                {
                    commentsCell.Value = "Alerts : " + warningCounter + ". Corrected values : " + informationOrCriticalCounter;
                }
                else if (processToAnalyze.Contains("TXT2XML") && informationOrCriticalCounter > 0)
                {
                    commentsCell.Value = "Alerts : " + warningCounter + "Critical errors : " + informationOrCriticalCounter;
                    checkCell.Value = "ALERT";
                    checkCell.Style.ForeColor = System.Drawing.Color.Red;
                    commentsCell.Style.ForeColor = System.Drawing.Color.Red;
                }
            }

            else if (processToAnalyze.Contains("VALUECHECKER") && informationOrCriticalCounter > 0)
            {
                commentsCell.Value = "Replacements : " + informationOrCriticalCounter;
                checkCell.Value = "INFO";
                checkCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
                commentsCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
            }

            else if (processToAnalyze.Contains("VALUECORRECTOR") && informationOrCriticalCounter > 0)
            {
                commentsCell.Value = "Corrected values : " + informationOrCriticalCounter;
                checkCell.Value = "INFO";
                checkCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
                commentsCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
            }

            else if (processToAnalyze.Contains("LINEDEL") && informationOrCriticalCounter > 0)
            {
                commentsCell.Value = "Lines deleted : " + informationOrCriticalCounter;
                checkCell.Value = "INFO";
                checkCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
                commentsCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
            }

            else if (processToAnalyze.Contains("DUPLICATES") && informationOrCriticalCounter > 0)
            {
                commentsCell.Value = "Duplicated lines removed : " + informationOrCriticalCounter;
                checkCell.Value = "INFO";
                checkCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
                commentsCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
            }

            else if (processToAnalyze.Equals(("== CONTROL TOTAL TV ==")) && informationOrCriticalCounter > 0)
            {
                commentsCell.Value = "TTV Channel is missing";
                checkCell.Value = "ALERT";
                checkCell.Style.ForeColor = System.Drawing.Color.Red;
                commentsCell.Style.ForeColor = System.Drawing.Color.Red;
            }

            else if ((processToAnalyze.Contains("XML2TXT") || processToAnalyze.Contains("TXT2XML") || processToAnalyze.Contains("COLUMNDELETER") || processToAnalyze.Contains("COLUMNMOVER") || processToAnalyze.Contains("XMLMERGE")) && informationOrCriticalCounter > 0)
            {
                commentsCell.Value = "Critical errors : " + informationOrCriticalCounter;
                checkCell.Value = "ALERT";
                checkCell.Style.ForeColor = System.Drawing.Color.Red;
                commentsCell.Style.ForeColor = System.Drawing.Color.Red;
            }

            else
            {
                commentsCell.Style.ForeColor = System.Drawing.Color.Black;
                commentsCell.Value = "-";
                checkCell.Value = "OK";
                checkCell.Style.ForeColor = System.Drawing.Color.Green;
            }
        }

        /*************************************************************\
         * Analyze DATACHECKER :                                     *
         *   - Go through the file, for each line, 4 possibilities : *
         *        . Warning                                          *
         *        . Unknown Typology                                 *
         *        . Unknown Channel                                  *
         *        . Empty Field                                      *
        \*************************************************************/

        private void AnalyzeDatachecker(DataGridViewCell commentsCell, DataGridViewCell checkCell, string DCPath)
        {
            int nbWarnings = 0;
            int nbEmptyFields = 0;
            int nbUnknownTypos = 0;
            int nbUnknownChannels = 0;

            Boolean channelTrigger = false;
            Boolean typoTrigger = false;
         
            try
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(DCPath);

                /* Read Log */
                while ((line = file.ReadLine()) != null)
                {
                    /* Detect warnings */
                    if (line.Contains("Unable to split typo, error: ")
                        || line.Contains("Unable to check empties on line: ")
                        || line.Contains("Unable to check line: "))
                        nbWarnings++;

                    /* Detect Empty lines */
                    else if (line.Contains((" - Field is empty.")))
                        nbEmptyFields++;

                    /* Detect empty channels & typos */
                    else if (channelTrigger && !String.IsNullOrEmpty(line) && !line.Contains(("Unknown genres list:")))
                        nbUnknownChannels++;
                    else if (typoTrigger && !String.IsNullOrEmpty(line))
                        nbUnknownTypos++;
                    else if (line.Contains(("Unknown channels list:")))
                        channelTrigger = true;
                    else if (line.Contains(("Unknown genres list:")))
                    {
                        channelTrigger = false;
                        typoTrigger = true;
                    }
                }

                /* Set results */
                if (nbWarnings > 0 && nbEmptyFields == 0 && nbUnknownTypos == 0 && nbUnknownChannels == 0)
                {
                    commentsCell.Value = "Alerts : " + nbWarnings;
                    checkCell.Value = "Warning";
                    checkCell.Style.ForeColor = System.Drawing.Color.DarkOrange;
                    commentsCell.Style.ForeColor = System.Drawing.Color.DarkOrange;
                }
                else if (nbWarnings == 0 && nbEmptyFields == 0 && nbUnknownTypos == 0 && nbUnknownChannels == 0)
                {
                    commentsCell.Value = "-";
                    checkCell.Value = "OK";
                    checkCell.Style.ForeColor = System.Drawing.Color.Green;
                    commentsCell.Style.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    if (nbWarnings > 0)
                        commentsCell.Value = "Warnings : " + nbWarnings + "\n";
                    if (nbEmptyFields > 0)
                        commentsCell.Value += "Empty fields : " + nbEmptyFields + ". ";
                    if (nbUnknownChannels > 0)
                        commentsCell.Value += "Unkn. Channels : " + nbUnknownChannels + ". ";
                    if (nbUnknownTypos > 0)
                        commentsCell.Value += "Unkn. Typos : " + nbUnknownTypos + ". ";

                    checkCell.Value = "ALERT";
                    commentsCell.Style.ForeColor = System.Drawing.Color.Red;
                    checkCell.Style.ForeColor = System.Drawing.Color.Red;
                }

                file.Close();
            }

            catch { }
        }

        /********************************************\
         * Analyze CONTROLDIFF/QHNUMBER :           *
         *   - Go through the file, for each line : *
         *        . Define current check            *
         *        . Perform specific check          *
         *        . Compute result                  *
         *        . Fill cells                      *
         *        . Create User Control             *
        \********************************************/

        private void AnalyzeControlDiffOrQHNumbers(DataGridViewCell commentsCell, DataGridViewCell checkCell, string logPath, Boolean isQH)
        {
            string currentCheck="";

            Dictionary<string, int> checkNoDiffDico = new Dictionary<string, int>();
            Dictionary<string, char> checkTotalAudienceDico = new Dictionary<string, char>();
            Dictionary<string, int> checkIndicatorAudienceDico = new Dictionary<string, int>();
            Dictionary<string, int> checkIndicatorAudienceDico2 = new Dictionary<string, int>();

            commentsCell.Style.BackColor = Color.Transparent;
            
            checkCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            try
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(logPath);

                try
                {

                    while ((line = file.ReadLine()) != null)
                    {
                        // Define current check.
                        if (line.Contains("* CHECK NO DIFF :"))
                            currentCheck = "* CHECK NO DIFF :";
                        else if (line.Contains("* CHECK DIFF :"))
                            currentCheck = "* CHECK DIFF :";
                        else if (line.Contains("* CHECK DURATION :"))
                            currentCheck = "* CHECK DURATION :";
                        else if (line.Contains("* CHECK TOT AUD :"))
                            currentCheck = "* CHECK TOT AUD :";
                        else if (line.Contains("* CHECK TOT AUD BLOCK:"))
                            currentCheck = "* CHECK TOT AUD BLOCK:";
                        else if (line.Contains("* CHECK TARGET AUD :"))
                            currentCheck = "* CHECK TARGET AUD :";
                        else if (line.Contains("* CHECK TARGET AUD BLOCK:"))
                            currentCheck = "* CHECK TARGET AUD BLOCK:";
                        else if (line.Contains("* CHECK INDICATOR AUD :"))
                            currentCheck = "* CHECK INDICATOR AUD :";
                        else if (line.Contains("* CHECK INDICATOR AUD BLOCK:"))
                            currentCheck = "* CHECK INDICATOR AUD BLOCK:";
                        else if (line.Contains("== CONTROL TOTAL TV =="))
                            currentCheck = "";
                        else if (line.Contains("* CHECK CHANNEL AND DATE AUD :"))
                            currentCheck = "* CHECK CHANNEL AND DATE AUD :";
                    
                        if(currentCheck.Equals( "* CHECK NO DIFF :")) 
                        {
                            _NoDiffAlertsLogs= _NoDiffAlertsLogs + line + "\n";

                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                _NoDiffFormatErrors++;

                            else if (line.Contains("NO DIFF"))
                            {
                                checkTotalAudienceDico.Add(line.Replace(" : ", " ").Replace(" NO DIFF", ""), ' ');
                                
                                // first add line in a dictionnary (which will be analyzed after)
                                if (checkNoDiffDico.ContainsKey(line.Split(new string[] { ":" }, StringSplitOptions.None)[1]))
                                    checkNoDiffDico[line.Split(new string[] { ":" }, StringSplitOptions.None)[1]] = checkNoDiffDico[line.Split(new string[] { ":" }, StringSplitOptions.None)[1]] + 1;
                                else checkNoDiffDico.Add(line.Split(new string[] { ":" }, StringSplitOptions.None)[1],1);

                            }
                        }
                    
                        else if (currentCheck.Equals("* CHECK DIFF :"))
                        {
                            _DiffAlertsLogs += line + "\n";
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                _DiffFormatErrors++;
                            else if (line.Contains("[Moy"))
                            {
                                _DiffAlerts++;
                                int average = int.Parse(line.Split(new String[] { "[Moy = ", "]" }, StringSplitOptions.None)[1]);
                                string[] splitResult = line.Split(null);
                                int diff = int.Parse(splitResult[splitResult.Length-5]);

                                /* Directly analyze the line (compare the diff with the average value) */
                                if ((float)diff < (float)(average / 2))
                                {
                                    _DiffCriticalAlerts++;
                                    _DiffCriticalAlertsLogs += line + " : nombre de diffusions trop inférieur à la moyenne!\n";
                                }
                                else if ((float)diff > ((float)average + (float)(average / 2)))
                                {
                                    _DiffCriticalAlerts++;
                                    _DiffCriticalAlertsLogs += line + " : nombre de diffusions trop supérieur à la moyenne!\n";
                                }
                            }
                        }
                    
                        else if (currentCheck.Equals("* CHECK DURATION :"))
                        {
                            _DurationAlertsLogs += line + "\n";
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                _DurationFormatErrors++;
                            else if (line.Contains("[Moy"))
                            {
                                _DurationAlerts++;
                                string average = line.Split(new String[] { "[Moy = "}, StringSplitOptions.None)[1].Replace("]","");
                                int minutes = int.Parse(average.Split(new char[] { ':' }, StringSplitOptions.None)[0]);
                                int seconds = int.Parse(average.Split(new char[] { ':' }, StringSplitOptions.None)[1]);
                                int averageSec = (minutes * 60) + seconds;

                                string[] splitResult = line.Split(null);
                                string duration = splitResult[splitResult.Length - 4];
                                minutes = int.Parse(duration.Split(new char[] { ':' }, StringSplitOptions.None)[0]);
                                seconds = int.Parse(duration.Split(new char[] { ':' }, StringSplitOptions.None)[1]);
                                int durationSec = (minutes * 60) + seconds;

                                if ((float)durationSec < (float)(averageSec / 2))
                                {
                                    _DurationCriticalAlerts++;
                                    _DurationCriticalAlertsLogs += line + " : Durée trop inférieure à la moyenne!\n";
                                }

                                else if ((float)durationSec > ((float)averageSec + (float)(averageSec / 2)))
                                {
                                    _DurationCriticalAlerts++;
                                    _DurationCriticalAlertsLogs += line + " : Durée trop supérieure à la moyenne!\n";
                                }
                            }
                        }
                           

                        else if (currentCheck.Equals("* CHECK TOT AUD BLOCK:") ||currentCheck.Equals("* CHECK TOT AUD :"))
                        {
                            _TotalAudienceAlertsLogs += line + "\n";
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                _TotalAudienceFormatErrors++;

                            else if (line.Contains("Alert : "))
                            {
                                if (checkTotalAudienceDico.ContainsKey(line.Replace("Alert : ", "").Replace(" AUDIENCES N.A OU 0", "")))
                                    _TotalAudienceAlerts++;
                                else
                                {
                                    _TotalAudienceCriticalAlertsLogs += line.Replace(" AUDIENCES N.A OU 0","") + " Audiences nulles ou égales à 0 (alors que la chaîne est diffusée)\n";
                                    _TotalAudienceCriticalAlerts++;
                                }
                            }
                        }

                        else if (currentCheck.Equals("* CHECK TARGET AUD BLOCK:") ||currentCheck.Equals("* CHECK TARGET AUD :"))
                        {
                            _TargetAudienceAlertsLogs += line + "\n";
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                _TargetAudienceFormatErrors++;

                            else if (line.Contains("Alert : "))
                            {
                                _TargetAudienceAlerts++;
                            }
                        }

                        else if (currentCheck.Equals( "* CHECK INDICATOR AUD BLOCK:") ||currentCheck.Equals("* CHECK INDICATOR AUD :"))
                        {
                            _IndicatorAudienceAlertsLogs += line + "\n";
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                _IndicatorAudienceFormatErrors++;
                            else if (line.Contains("Alert : "))
                            {
                                Regex rgx = new Regex("(Alert : )([0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9] )(.*)");
                                Regex rgx2 = new Regex("(.*)( AUDIENCES N.A OU 0)(.*)");
                                
                                // 1st case : For one indicator, audience NA for the whole period
                                if (rgx.IsMatch(line))
                                {
                                    Match m = rgx.Match(line);

                                    if (checkIndicatorAudienceDico.ContainsKey(m.Groups[3].Value))
                                        checkIndicatorAudienceDico[m.Groups[3].Value] = checkIndicatorAudienceDico[m.Groups[3].Value] + 1;
                                    else checkIndicatorAudienceDico.Add(m.Groups[3].Value,1);
                                }

                                // 2nd case : For one day, audience NA for all targets
                                if (rgx2.IsMatch(line))
                                {
                                    Match m = rgx2.Match(line);

                                    if (checkIndicatorAudienceDico2.ContainsKey(m.Groups[1].Value + m.Groups[2].Value))
                                        checkIndicatorAudienceDico2[m.Groups[1].Value + m.Groups[2].Value] = checkIndicatorAudienceDico2[m.Groups[1].Value + m.Groups[2].Value] + 1;
                                    else checkIndicatorAudienceDico2.Add(m.Groups[1].Value + m.Groups[2].Value, 1);
                                }    
                            }
                        }

                        else if (currentCheck.Equals("* CHECK CHANNEL AND DATE AUD :"))
                        {
                            if (line.Contains("Alert"))
                            {
                                _CheckChannelAndDateAudienceCriticalAlerts++;
                                _CheckChannelAndDateAudienceCriticalAlertsLogs += line + "\n";
                            }
                        }

                        else if (currentCheck.Equals(""))
                        {

                        }
                    }

                    file.Close();
                }

                catch (Exception ex) { Console.WriteLine(ex); }

                /////////////////////////////
                // Compute & Display results 

                if (!isQH)
                {
                    // Check No Diff
                    checkNoDiffDico.Remove("");
                    foreach (KeyValuePair<string, int> element in checkNoDiffDico)
                    {
                        if ((float)element.Value > (float)0.5 * (float)_NumberOfDays)
                        {
                            _NoDiffCriticalAlerts++;
                            _NoDiffCriticalAlertsLogs = _NoDiffCriticalAlertsLogs + "La chaîne " + element.Key.ToString().Replace(" NO DIFF", "") + " n'a pas été diffusée durant " + element.Value + " jours (sur les " + _NumberOfDays + ").\n";
                        }
                        _NoDiffAlerts = _NoDiffAlerts + element.Value;
                    }
                }

                // Check Indicators Audience 1st case : For one indicator, audience NA for the whole period
                foreach (KeyValuePair<string, int> element in checkIndicatorAudienceDico)
                {
                    if (element.Value == _NumberOfDays)
                    {
                        _IndicatorAudienceCriticalAlerts++;
                        _IndicatorAudienceCriticalAlertsLogs += element.Key + " : Indicateur nul/n.a pour toute la période !\n";
                    }
                    _IndicatorAudienceAlerts = _IndicatorAudienceAlerts + element.Value;
                }

                // Check Indicators Audience 2nd case : For one day, audience NA for all targets
                foreach (KeyValuePair<string, int> element in checkIndicatorAudienceDico2)
                {
                    if (element.Value == _NumberOfTargets)
                    {
                        _IndicatorAudienceCriticalAlerts++;
                        _IndicatorAudienceCriticalAlertsLogs += element.Key + " : Audience nulle/n.a pour toutes les cibles ! (une journée) \n";
                    }
                }

                int totalFormatErrors = _NoDiffFormatErrors+ _DiffFormatErrors+ _DurationFormatErrors+ _TotalAudienceFormatErrors +_TargetAudienceFormatErrors+ _IndicatorAudienceFormatErrors;
                int totalAlerts = _NoDiffAlerts + _DiffAlerts + _DurationAlerts + _TotalAudienceAlerts + _TargetAudienceAlerts + _IndicatorAudienceAlerts;
                int totalCriticalAlerts = _NoDiffCriticalAlerts + _DiffCriticalAlerts + _DurationCriticalAlerts + _TotalAudienceCriticalAlerts + _IndicatorAudienceCriticalAlerts + _CheckChannelAndDateAudienceCriticalAlerts;
                commentsCell.Value = "Total Format Error : " + totalFormatErrors + "\nTotal Alerts : " + totalAlerts + "\nTotal Critical Alerts : " + totalCriticalAlerts;

                // Fill cells

                if (totalCriticalAlerts > 0)
                {
                    checkCell.Value = "ALERT";
                    checkCell.Style.ForeColor = System.Drawing.Color.Red;
                    commentsCell.Style.ForeColor = System.Drawing.Color.Red;
                }

                else if (totalAlerts > 0)
                {
                    checkCell.Value = "Warning";
                    checkCell.Style.ForeColor = System.Drawing.Color.DarkOrange;
                    commentsCell.Style.ForeColor = System.Drawing.Color.DarkOrange;
                }

                else
                {
                    checkCell.Value = "OK";
                    checkCell.Style.ForeColor = System.Drawing.Color.Green;
                    commentsCell.Style.ForeColor = System.Drawing.Color.Green;
                }

                /////////////
                // Create UC

                // Define what has to be highlited in red
                Dictionary<string, char> criticalAlertsResume = new Dictionary<string, char>();
                if (_NoDiffCriticalAlerts > 0)
                    criticalAlertsResume.Add("CHECK NO DIFFUSION", ' ');
                if (_DiffCriticalAlerts > 0)
                    criticalAlertsResume.Add("CHECK NUMBER OF DIFFUSIONS", ' ');
                if (_DurationCriticalAlerts > 0)
                    criticalAlertsResume.Add("CHECK DURATION", ' ');
                if (_TotalAudienceCriticalAlerts > 0)
                    criticalAlertsResume.Add("CHECK TOTAL AUDIENCE", ' ');
                if (_IndicatorAudienceCriticalAlerts > 0)
                    criticalAlertsResume.Add("CHECK INDICATOR AUDIENCE", ' ');
                if (_CheckChannelAndDateAudienceCriticalAlerts > 0)
                    criticalAlertsResume.Add("CHECK CHANNEL AND DATE AUDIENCE", ' ');

                if (!isQH)
                {
                    String[] splitRes = _ControlDiffPath.Split(new string[] { "\\" }, StringSplitOptions.None);
                    _ControlDiffForm.Text = splitRes[splitRes.Length - 1];
                }
                else
                {
                    String[] splitRes = _DatamodLogPath.Split(new string[] { "\\" }, StringSplitOptions.None);
                    _ControlDiffForm.Text = splitRes[splitRes.Length - 1];
                }

                ControlDiffReview review = new ControlDiffReview(criticalAlertsResume, _NoDiffAlertsLogs, _NoDiffCriticalAlertsLogs, _DiffAlertsLogs, _DiffCriticalAlertsLogs, _DurationAlertsLogs, _DurationCriticalAlertsLogs, _TotalAudienceAlertsLogs, _TotalAudienceCriticalAlertsLogs, _TargetAudienceAlertsLogs, _IndicatorAudienceAlertsLogs, _IndicatorAudienceCriticalAlertsLogs,_CheckChannelAndDateAudienceCriticalAlertsLogs, isQH);
                _ControlDiffForm.Controls.Clear();
                _ControlDiffForm.Controls.Add(review);
                review.Dock = System.Windows.Forms.DockStyle.Fill;
            }

            catch (Exception ex) { Console.WriteLine(ex); }


        }

        /**********************************\
         * Analyze Header Consistency Log *
        \**********************************/

        private void AnalyzeHC(DataGridViewCell commentsCell, DataGridViewCell checkCell, string HCPath)
        {
            Boolean error = false;
            string line;

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(HCPath);

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("values dismatches"))
                    {
                        error = true;
                        commentsCell.Value = "Values dismatches";
                    }
                    else if (line.Contains("Country or configuration file not found"))
                    {
                        error = true;
                        commentsCell.Value = "Country or configuration file not found";
                    }
                }

                file.Close();
            }

            catch (Exception ex) { Console.WriteLine(ex); }

            if (error)
            {
                checkCell.Value = "ALERT";
                checkCell.Style.ForeColor = System.Drawing.Color.Red;
                commentsCell.Style.ForeColor = System.Drawing.Color.Red;
            }

            else
            {
                commentsCell.Value = "Values matches";
                checkCell.Value = "OK";
                checkCell.Style.ForeColor = System.Drawing.Color.Green;
                commentsCell.Style.ForeColor = System.Drawing.Color.Green;
            }
        }

        /********************************************\
         * Open log when double clicking on the row *
        \********************************************/

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("HC"))
                    System.Diagnostics.Process.Start(_HCPath);
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 0 && int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 100 && !DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("LINEDEL") && !DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("DATEFORMAT"))
                    System.Diagnostics.Process.Start(_DatamodLogPath);
                else if(int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 0 && int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 100 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("LINEDEL"))
                    System.Diagnostics.Process.Start(_LinedelPath);
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 0 && int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 100 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("DATEFORMAT"))
                    System.Diagnostics.Process.Start(_DateformatPath);
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 0 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("XLS2TXT"))
                    System.Diagnostics.Process.Start(_DatamodRootPath + "XLS2TXT_Process.log");
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 0 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("FILESPLIT"))
                    System.Diagnostics.Process.Start(_DatamodRootPath + "FILESPLIT_Process.log");
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 0 && (DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("COLUMNSCONCAT") || DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("LINESCONCAT")))
                    System.Diagnostics.Process.Start(_DatamodLogPath);
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 100 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("DATACHECKER"))
                    System.Diagnostics.Process.Start(_DatacheckerPath);
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 100 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("TOTALTVCONTROL"))
                    System.Diagnostics.Process.Start(_DatamodLogPath);
                else if ((int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 100 && (DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("CONTROLDIFF"))) || (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 100 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("QHNUMBERS")))
                    _ControlDiffForm.Show();
                else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 0 && DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("XML2TXT"))
                    System.Diagnostics.Process.Start(_DatamodRootPath + "XML2TXT_Process.log");
                else if (DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("TXT2XML") || DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("COLUMNDELETER") || DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("XMLMERGE"))
                    System.Diagnostics.Process.Start(_DatamodLogPath);
            }

            catch(Exception ex)
            {
                var result = KryptonMessageBox.Show("Unable to open file :(\n\n"+ ex, "Error opening file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /*************************\
         * Hide the user control *
        \*************************/

        private void HideControlDiffForm(object sender, FormClosingEventArgs e)
        {
            _ControlDiffForm.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        /********************************\
         * Calculate the number of days *
        \********************************/

        private int NumberOfDays(string dmPath)
        {
            int numberOfDays = 0;
            Regex rgx = new Regex("(.*)([0-9]{8})-([0-9]{8})(.txt)"); // group 1 is any char. Group 2 is begin date. Group 3 is end date. Group 4 is ".txt"

            if (rgx.IsMatch(dmPath))
            {
                Match m = rgx.Match(dmPath); // group 1 is any char. Group 2 is begin date. Group 3 is end date.

                System.DateTime start = new System.DateTime(int.Parse(m.Groups[2].Value.Substring(0, 4)), int.Parse(m.Groups[2].Value.Substring(4,2)), int.Parse(m.Groups[2].Value.Substring(6, 2)));
                System.DateTime end = new System.DateTime(int.Parse(m.Groups[3].Value.Substring(0, 4)), int.Parse(m.Groups[3].Value.Substring(4, 2)), int.Parse(m.Groups[3].Value.Substring(6, 2)));

                TimeSpan ts = end - start;
                numberOfDays = ts.Days + 1;
            }
            else numberOfDays = 1;

            return numberOfDays;
        }

        #endregion
   
        #region Accessors

        public DataGridView Get_DataGridView(){
            return DataGridView;}

        #endregion
    }
}
