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
        // salut
        #region Variables

        private string _InputFileName;
        private string _DatamodRootPath;
        private string _DatamodLogPath;
        private string _LinedelPath;
        private string _DateformatPath;
        private string _ControlDiffPath;
        private string _DatacheckerPath;

        private int _NumberOfDays;
        private int _NumberOfTargets;


        #endregion


        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public LogsGrid(String name, List<Process> processList, string dmPath, string inputFile, int targetsNumber)
        {
            InitializeComponent();
            DataGridView.Name = name;
            _DatamodRootPath = "";
            _InputFileName = inputFile;
            _DatacheckerPath = dmPath.Replace(".txt", "_dc.log");

            _NumberOfDays = NumberOfDays(dmPath);
            _NumberOfTargets = targetsNumber;

            _DatamodRootPath = System.IO.Directory.GetParent(dmPath).FullName + '\\';

            _DatamodLogPath = dmPath.Replace(".txt", ".log");

            this.Dock = System.Windows.Forms.DockStyle.Fill;
            FillGrid(processList);

            DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        private void FillGrid(List<Process> processList)
        {
            for(int i = 0; i<processList.Count;i++)
            {
                int occurrenceCounter = 0;
                DataGridViewRow row = (DataGridViewRow)DataGridView.Rows[0].Clone();
                row.Cells[0].Value = processList[i].Get_OrderId();
                row.Cells[1].Value = processList[i].Get_Name();

                // Check how many time the process has already been called
                for (int j = 0; j < i + 1; j++)
                    if (processList[j].Get_Name().Equals(processList[i].Get_Name()))
                        occurrenceCounter++;


                AnalyzeLog(processList[i].Get_Name(), row.Cells[2], row.Cells[3], occurrenceCounter);
                DataGridView.Rows.Add(row);
            }
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

                    AnalyzeProcessLog("------------XLS2TXT------------", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodRootPath + "XLS2TXT_Process.log");

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
                        if (file.FullName.Contains(_InputFileName.Replace(".txt", "")) && file.FullName.Contains("new_ld.log"))
                            _LinedelPath = file.FullName;
                    }

                    AnalyzeProcessLog("------------ LINEDELETER ------------", listErrors, commentsCell, checkCell, occurrenceNumber, _LinedelPath);

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
                        if (file.FullName.Contains(_InputFileName.Replace(".txt", "")) && file.FullName.Contains("DATEFORMAT.log"))
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

                    AnalyzeProcessLog(" DataChecking log --------------", listErrors, commentsCell, checkCell, occurrenceNumber, _DatacheckerPath);

                    break;
                }

                case "CONTROLDIFF":
                {
                    DirectoryInfo root = new DirectoryInfo(_DatamodRootPath);
                    FileInfo[] files = root.GetFiles();

                    foreach (FileInfo file in files)
                    {
                        if (file.FullName.Contains(_InputFileName.Replace(".txt", "")) && file.FullName.Contains("CONTROLDIFF.log"))
                        {
                            _ControlDiffPath = file.FullName;
                            
                        }
                    }

                    AnalyzeControlDiff(commentsCell, checkCell,_ControlDiffPath);

                    break;
                }

                case "TOTALTVCONTROL":
                {
                    List<string> listErrors = new List<string>();
                    listErrors.Add("Alert ! Line ");
                    listErrors.Add("Error ! Line ");

                    AnalyzeProcessLog("== CONTROL TOTAL TV ==", listErrors, commentsCell, checkCell, occurrenceNumber, _DatamodLogPath);

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
            int informationCounter = 0;
            int occurrences = 0;
            Boolean readingTrigger = false;

            commentsCell.Style.BackColor = Color.Transparent;
            commentsCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            checkCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
                            if ((line.Contains(error) && error.Equals(", replaced:") && processToAnalyze.Contains("VALUECHECKER")) 
                                || (line.Contains(error) && error.Equals(" was corrected value:") && processToAnalyze.Contains("VALUECORRECTOR")) 
                                || (line.Contains(error) && error.Equals(", was deleted: ") && processToAnalyze.Contains("LINEDEL"))
                                || (line.Contains(error) && error.Equals(" - Unknown Channel Id: ") && processToAnalyze.Contains("DataChecking"))
                                || (line.Contains(error) && error.Equals(" - Unknown Typology Id: ") && processToAnalyze.Contains("DataChecking"))
                                )
                            {
                                warningCounter--;
                                informationCounter++;
                            }
                        }
                    }
                }
            

                file.Close();
            }
 
            catch (Exception ex) { Console.WriteLine(ex); }

            if (warningCounter > 0)
            {
                commentsCell.Value = "Alerts : " + warningCounter;
                checkCell.Value = "Warning";
                checkCell.Style.ForeColor = System.Drawing.Color.Orange;
                commentsCell.Style.ForeColor = System.Drawing.Color.Orange;

                if (processToAnalyze.Contains("VALUECHECKER") && informationCounter > 0)
                    commentsCell.Value = "Alerts : " + warningCounter + ". Replacements : " + informationCounter;
                else if(processToAnalyze.Contains("VALUECORRECTOR") && informationCounter > 0)
                    commentsCell.Value = "Alerts : " + warningCounter + ". Corrected values : " + informationCounter;
            }

            else if (processToAnalyze.Contains("VALUECHECKER") && informationCounter > 0)
            {
                commentsCell.Value = "Replacements : " + informationCounter;
                checkCell.Value = "INFO";
                checkCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
                commentsCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
            }

            else if (processToAnalyze.Contains("VALUECORRECTOR") && informationCounter > 0)
            {
                commentsCell.Value = "Corrected values : " + informationCounter;
                checkCell.Value = "INFO";
                checkCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
                commentsCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
            }

            else if (processToAnalyze.Contains("LINEDEL") && informationCounter > 0)
            {
                commentsCell.Value = "Lines deleted : " + informationCounter;
                checkCell.Value = "INFO";
                checkCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
                commentsCell.Style.ForeColor = System.Drawing.Color.CornflowerBlue;
            }

            else if (processToAnalyze.Contains("DataChecking") && informationCounter > 0)
            {
                commentsCell.Value = "Unknown Typology or Channel : " + informationCounter;
                checkCell.Value = "WARNING";
                checkCell.Style.ForeColor = System.Drawing.Color.Red;
                commentsCell.Style.ForeColor = System.Drawing.Color.Red;
            }

            else
            {
                commentsCell.Value = "-";
                checkCell.Value = "OK";
                checkCell.Style.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void AnalyzeControlDiff(DataGridViewCell commentsCell, DataGridViewCell checkCell, string logPath)
        {
            //KryptonRichTextBox rtb = new KryptonRichTextBox();
            int counter = 0;
            int noDiffAlerts = 0;
            int noDiffCriticalAlerts=0;
            int noDiffFormatErrors = 0;
            
            int diffAlerts = 0;
            int diffCriticalAlerts = 0;
            int diffFormatErrors = 0;

            int durationAlerts = 0;
            int durationCriticalAlerts = 0;
            int durationFormatErrors = 0;

            int totalAudienceAlerts = 0;
            int totalAudienceCriticalAlerts = 0;
            int totalAudienceFormatErrors = 0;

            int targetAudienceAlerts = 0;
            int targetAudienceFormatErrors = 0;

            int indicatorAudienceAlerts = 0;
            int indicatorAudienceCriticalAlerts = 0;
            int indicatorAudienceFormatErrors = 0;

            string currentCheck="";

            Dictionary<string, int> checkNoDiffDico = new Dictionary<string, int>();
            Dictionary<string, char> checkTotalAudienceDico = new Dictionary<string, char>();
            Dictionary<string, int> checkIndicatorAudienceDico = new Dictionary<string, int>();
            Dictionary<string, int> checkIndicatorAudienceDico2 = new Dictionary<string, int>();

            commentsCell.Style.BackColor = Color.Transparent;
            //commentsCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            checkCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            try
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(logPath);

                while ((line = file.ReadLine()) != null)
                {
                    // Define current check.
                    if (line.Contains("* CHECK NO DIFF :"))
                    {
                        currentCheck = "* CHECK NO DIFF :";
                        //rtb.AppendText("* CHECK NO DIFF :");
                    }
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
                    
                    // Analyze current check.
                    switch (currentCheck)
                    {
                        case "* CHECK NO DIFF :":
                        {
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                noDiffFormatErrors++;
                            else if (line.Contains("NO DIFF"))
                            {
                                checkTotalAudienceDico.Add(line.Replace(" : ", " ").Replace(" NO DIFF", ""),' ');

                                try
                                {
                                    checkNoDiffDico.Add(line.Split(new string[] { ":" }, StringSplitOptions.None)[1], 1);
                                }

                                catch
                                {
                                    checkNoDiffDico[line.Split(new string[] { ":" }, StringSplitOptions.None)[1]] = checkNoDiffDico[line.Split(new string[] { ":" }, StringSplitOptions.None)[1]] + 1;
                                }
                            }
                            
                            break;
                        }
                    
                        case "* CHECK DIFF :":
                        {
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                diffFormatErrors++;
                            else if (line.Contains("[Moy"))
                            {
                                diffAlerts++;
                                int average = int.Parse(line.Split(new String[] { "[Moy = ", "]" }, StringSplitOptions.None)[1]);
                                string[] splitResult = line.Split(null);
                                int diff = int.Parse(splitResult[splitResult.Length-5]);
                                    
                                if ((float)diff < (float)(average / 2) || (float)diff > ((float)average + (float)(average / 2)))
                                    diffCriticalAlerts++;
                            }
                            

                            break;
                        }
                    
                        case "* CHECK DURATION :":
                        {
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                durationFormatErrors++;
                            else if (line.Contains("[Moy"))
                            {
                                durationAlerts++;
                                string average = line.Split(new String[] { "[Moy = ", "]" }, StringSplitOptions.None)[1];
                                int minutes = int.Parse(average.Split(new char[] { ':' }, StringSplitOptions.None)[0]);
                                int seconds = int.Parse(average.Split(new char[] { ':' }, StringSplitOptions.None)[1]);
                                int averageSec = (minutes * 60) + seconds;

                                string[] splitResult = line.Split(null);
                                string duration = splitResult[splitResult.Length - 4];
                                minutes = int.Parse(duration.Split(new char[] { ':' }, StringSplitOptions.None)[0]);
                                seconds = int.Parse(duration.Split(new char[] { ':' }, StringSplitOptions.None)[1]);
                                int durationSec = (minutes * 60) + seconds;

                                if ((float)durationSec < (float)(averageSec / 2) || (float)durationSec > ((float)averageSec + (float)(averageSec / 2)))
                                    durationCriticalAlerts++;
                            }
                        }
                            
                            break;


                        case "* CHECK TOT AUD BLOCK:":
                        case "* CHECK TOT AUD :":
                        {
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                totalAudienceFormatErrors++;

                            else if (line.Contains("Alert : "))
                            {
                                try
                                {
                                    checkTotalAudienceDico.Add(line.Replace("Alert : ", "").Replace(" AUDIENCES N.A OU 0", ""), ' ');
                                    totalAudienceCriticalAlerts++;
                                }

                                catch
                                {
                                    totalAudienceAlerts++;
                                }
                            }
                            
                            break;
                        }

                        case "* CHECK TARGET AUD BLOCK:":
                        case "* CHECK TARGET AUD :":
                        {
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                targetAudienceFormatErrors++;

                            else if (line.Contains("Alert : "))
                            {
                                targetAudienceAlerts++;
                            }

                            break;
                        }

                        case "* CHECK INDICATOR AUD BLOCK:":
                        case "* CHECK INDICATOR AUD :":
                        {
                            if (line.Contains("Error ! Line ") || line.Contains("L'index se trouve en dehors des limites du tableau."))
                                indicatorAudienceFormatErrors++;
                            else if (line.Contains("Alert : "))
                            {
                                Regex rgx = new Regex("(Alert : )([0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9] )(.*)");
                                Regex rgx2 = new Regex("(.*)( AUDIENCES N.A OU 0)(.*)");
                                
                                // 1st case : For one indicator, audience NA for the whole period
                                if (rgx.IsMatch(line))
                                {
                                    Match m = rgx.Match(line);
                                    //Console.WriteLine(m.Groups[3].Value);

                                    try
                                    {
                                        checkIndicatorAudienceDico.Add(m.Groups[3].Value, 1);
                                    }

                                    catch
                                    {
                                        checkIndicatorAudienceDico[m.Groups[3].Value] = checkIndicatorAudienceDico[m.Groups[3].Value] + 1;
                                    }
                                }

                                // 2nd case : For one day, audience NA for all targets
                                if (rgx2.IsMatch(line))
                                {
                                    Match m = rgx2.Match(line);
                                    //Console.WriteLine(m.Groups[1].Value + m.Groups[2].Value);
                                    try
                                    {
                                        checkIndicatorAudienceDico2.Add(m.Groups[1].Value + m.Groups[2].Value, 1);
                                    }

                                    catch
                                    {
                                        checkIndicatorAudienceDico2[m.Groups[1].Value + m.Groups[2].Value] = checkIndicatorAudienceDico2[m.Groups[1].Value + m.Groups[2].Value] + 1;
                                    }
                                }    
                            }
   
                            break;
                        }
                    
                        default: break;
                    }
                }

                file.Close();

                // Compute & Display results

                // Check No Diff
                checkNoDiffDico.Remove("");
                foreach (KeyValuePair<string, int> element in checkNoDiffDico)
                {
                    if ((float)element.Value > (float)0.7 * (float)_NumberOfDays)
                        noDiffCriticalAlerts++;
                    noDiffAlerts = noDiffAlerts + element.Value;
                }
                commentsCell.Value = "No Diff -> Format Errors : " + noDiffFormatErrors + " - Alerts : " + noDiffAlerts + " - Crit. Alerts : " + noDiffCriticalAlerts + "\n";

                // Check Diff
                commentsCell.Value = commentsCell.Value + "Diff -> Format Errors : " + diffFormatErrors + " - Alerts : " + diffAlerts + " - Crit. Alerts : " + diffCriticalAlerts + "\n";

                // Check Duration
                commentsCell.Value = commentsCell.Value + "Duration -> Format Errors : " + durationFormatErrors + " - Alerts : " + durationAlerts + " - Crit. Alerts : " + durationCriticalAlerts + "\n";

                // Check Total Audience
                commentsCell.Value = commentsCell.Value + "Total Aud. -> Format Errors : " + totalAudienceFormatErrors + " - Alerts : " + totalAudienceAlerts + " - Crit. Alerts : " + totalAudienceCriticalAlerts + "\n";

                // Check Target Audience
                commentsCell.Value = commentsCell.Value + "Target Aud. -> Format Errors : " + targetAudienceFormatErrors + " - Alerts : " + targetAudienceAlerts + "\n";

                // Check Indicators Audience 1st case : For one indicator, audience NA for the whole period
                foreach (KeyValuePair<string, int> element in checkIndicatorAudienceDico)
                {
                    if (element.Value == _NumberOfDays)
                        indicatorAudienceCriticalAlerts++;
                    indicatorAudienceAlerts = indicatorAudienceAlerts + element.Value;
                }
                
                // Check Indicators Audience 2nd case : For one day, audience NA for all targets
                foreach (KeyValuePair<string, int> element in checkIndicatorAudienceDico2)
                {
                    if (element.Value == _NumberOfTargets)
                        indicatorAudienceCriticalAlerts++;
                }
                commentsCell.Value = commentsCell.Value + "Ind. Aud. -> Format Errors : " + indicatorAudienceFormatErrors + " - Alerts : " + indicatorAudienceAlerts + " - Crit. Alerts : " + indicatorAudienceCriticalAlerts;

            }

            catch (Exception ex) { Console.WriteLine(ex); }


        }

        /********************************************\
         * Open log when double clicking on the row *
        \********************************************/

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                try
                {
                    if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 0 && int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) < 100 && !DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("LINEDEL") && !DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("DATEFORMAT"))
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
                    else if (int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 100 && (DataGridView.Rows[e.RowIndex].Cells[1].Value.Equals("CONTROLDIFF")))
                    {
                        System.Diagnostics.Process.Start(_ControlDiffPath);

                        //var form = new Form();
                        //form.ClientSize = new System.Drawing.Size(950, 500);
                        //form.StartPosition = FormStartPosition.CenterScreen;
                        //String[] splitResult = _ControlDiffPath.Split(new string[] { "\\" }, StringSplitOptions.None);
                        //form.Controls.Add(rtb);
                        //rtb.Dock = System.Windows.Forms.DockStyle.Fill;
                        //
                        //form.Text = splitResult[splitResult.Length-1];
                        //
                        //form.Show(); // if you need modal window
                    }

                }

                catch(Exception ex)
                {
                    var result = KryptonMessageBox.Show("Unable to open file :(\n\n"+ ex, "Error opening file",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                }
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

        #endregion
    }
}
