﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;
using ComponentFactory.Krypton.Toolkit;


namespace Analytics_V2
{
    partial class Launcher
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private int _ID;                // Id of the running thread.
        private int _NumberOfProcesses; // Number of processes (usefull for updating the progress bar)
        private float _Progress;        // Progress.
        private Config _Config;         // Selected config.

        private Boolean _PreProcess;        // Boolean which indicates if preprocess is enabled or not.
        private Boolean _Process;           // Boolean which indicates if process is enabled or not.
        private Boolean _Control;           // Boolean which indicates if control is enabled or not.
        private Boolean _HeaderConsistency; // Boolean which indicates if HC is enabled or not.
        private Boolean _HasTxt2Xml;        // Boolean which indicates if the function TXT2XML has been used (check in HC COntrol).

        private List<String> _InputFiles;   // List of input files.
        private List<String> _OutputFiles;  // List of output files.
        List<String> _TabData;              // Current file in list of strings.
        private int _FileCounter;           // Processed file number.
        private String _Logs;               // Logs.
        private String _LogsPath;           // Logs path.
        private String _DatamodPath;        // Datamod path.
        private String _CurrentFile;        // Current file.

        private Delegate _UpdateProgressBar;         // Delegate (for invoking the method which update the progress bar).
        private Delegate _UpdateRichTextBox;         // Delegate (for invoking the method which update the RTB).
        private Delegate _AddLogsGridView;           // Delegate (for adding the grid view with the combo box).
        private Delegate _DisplayConfigProcessTime;  // Delegate (for invoking the method which add the config process Time).
   
        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Launcher(Config config, Boolean preProcess, Boolean process, Boolean control, Boolean headerConsistency, List<String> listInputFiles, String logsPath, Delegate del, Delegate del2, Delegate del3, Delegate del4)
        {
            _Config = config;

            _PreProcess = preProcess;
            _Process = process;
            _Control = control;
            _HeaderConsistency = headerConsistency;
            _HasTxt2Xml = false;

            _Progress = 0;
            _ID = 0;

            _NumberOfProcesses = 0;
            foreach (Process p in _Config.Get_ProcessList())
            {
                if ((p.Get_OrderId() < 0 && _PreProcess) || ((p.Get_OrderId() > 0 & p.Get_OrderId() < 100) && _Process) || ((p.Get_OrderId() > 100 && _Control)))
                    _NumberOfProcesses++;
            }

            _Logs = null;

            _InputFiles = new List<String>();
            _InputFiles = listInputFiles;
            _OutputFiles = new List<String>();
            _TabData = new List<String>();
            _FileCounter = 0;
            _LogsPath = logsPath;
            _DatamodPath = null;
            _CurrentFile = null;

            _UpdateProgressBar = del;
            _UpdateRichTextBox = del2;
            _AddLogsGridView = del3;
            _DisplayConfigProcessTime = del4;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /********************************************************************\
         * Run the config (method called when a thread starts)              *
         *   - Measure the whole process time.                              *
         *   - Perform Preprocess.                                          *
         *   - Treat each input file :                                      *
         *       - Convert the current treated file into a list of strings. *
         *       - Perform Process.                                         *
         *       - Write Datamod.                                           *
         *       - Perform Controls.                                        *
         *       - Perform Header Consistency.                              *
         *       - Write logs.                                              *
         *       - Create Logs Datagrid View.                               *
         *       - Update stats                                             *
        \********************************************************************/

        internal void Run(int id)
        {
            float counter = 0;
            Stopwatch launchStopwatch = new Stopwatch();
            _ID = id;

            launchStopwatch.Start();

            UpdateProgressBar(counter);

            _Logs = _Logs + System.DateTime.Now + "\n" + _Config.Get_Name();
            
            // Perform Preprocess
            if (_PreProcess)
            {
                UpdateRichTextBox("title", "PRE-PROCESS");

                foreach (Process process in _Config.Get_ProcessList().OrderBy(x => x.Get_OrderId()))
                    if (process.Get_OrderId() < 0)
                    {
                        try
                        {
                            PreProcess(process.Get_Datatable());
                            counter++;
                            UpdateProgressBar(counter);
                        }
                        catch
                        {
                            counter++;
                            UpdateProgressBar(counter);
                            UpdateRichTextBox("fail", "FAIL");
                        }
                    }
            }

            // Treat each input file
            if (_InputFiles.Count > 0)
            {
                int numberOfInputFiles = _InputFiles.Count;
                foreach (String processedFile in _InputFiles)
                {
                    int nbLinesBefore = 0; // Number of lines before reformating.
                    int nbLinesAfter = 0;  // Number of lines after reformating.

                    Stopwatch launchStopwatch3 = new Stopwatch();
                    launchStopwatch3.Start();

                    string originalProcessedFile = processedFile.Replace("_1.txt", ".txt");
                    _FileCounter++;
                    _CurrentFile = processedFile;

                    _DatamodPath = Path.GetDirectoryName(processedFile) + "\\" + Path.GetFileNameWithoutExtension(processedFile) + "_new" + Path.GetExtension(processedFile);

                    // Convert the current treated file into a list of strings.
                    _TabData = FileToLS(processedFile);
                    nbLinesBefore = _TabData.Count;

                    // Perform process.
                    if (_Process)
                    {
                        UpdateRichTextBox("title", "PROCESS" + " (file n° " + _FileCounter + "/" + numberOfInputFiles + ") ");
                        foreach (Process process in _Config.Get_ProcessList().OrderBy(x => x.Get_OrderId()))
                            if (process.Get_OrderId() > 0 && process.Get_OrderId() < 100)
                            {
                                Process(process.Get_Datatable());
                                counter = counter + (float)((float)1 / (float)numberOfInputFiles);
                                UpdateProgressBar(counter);
                            }
                    }

                    // Write Datamod.
                    if (!String.IsNullOrEmpty(_Config.Get_CountryCode()))
                    {
                        nbLinesAfter = WriteDatamod();
                    }
                    else _DatamodPath = processedFile;

                    // Perform Controls.
                    if (_Control)
                    {
                        UpdateRichTextBox("title", "CONTROL" + " (file n° " + _FileCounter + "/" + numberOfInputFiles + ") ");
                        foreach (Process process in _Config.Get_ProcessList().OrderBy(x => x.Get_OrderId()))
                            if (process.Get_OrderId() > 100)
                            {
                                Control(process.Get_Datatable());
                                counter = counter + (float)((float)1 / (float)numberOfInputFiles);
                                UpdateProgressBar(counter);
                            }
                    }

                    // Perform Header Consistency.
                    if (_HeaderConsistency)
                        HeaderConsistency();                  

                    // Write logs.
                    WriteLogs();


                    /////////////////////////////////////////////////////////
                    // Create LogsGrid, analyze it and add it to the control.

                    Stopwatch launchStopwatch2 = new Stopwatch();
                    launchStopwatch2.Start();

                    UpdateRichTextBox("title", "*** ANALYZE LOGS ***");
                    AddLogsGridView(_DatamodPath, originalProcessedFile);
                    counter = counter + (float)((float)1 / (float)numberOfInputFiles);
                    UpdateRichTextBox("complete", "      |-->Complete!");
                    UpdateProgressBar(counter);

                    launchStopwatch2.Stop();
                    TimeSpan ts2 = launchStopwatch2.Elapsed;
                    string elapsedTime2 = String.Format("{0:00}:{1:00}", ts2.Minutes, ts2.Seconds);
                    UpdateRichTextBox("time", elapsedTime2);

                    // Update statistics
                    launchStopwatch3.Stop();
                    AnalyticsWebService.AnalyticsSoapClient query = new AnalyticsWebService.AnalyticsSoapClient();
                    query.Insert(System.Environment.UserName, _Config.Get_ConfigType(), _Config.Get_Name(), processedFile, _DatamodPath, (int)launchStopwatch3.ElapsedMilliseconds / 1000);
                    query.Close();

                    // Suppress input file if xml
                    if (processedFile.Contains(".xml"))
                        File.Delete(processedFile);

                    // Display number of lines before & after reformating
                    UpdateRichTextBox("lineCounter", "");
                    if (nbLinesBefore > 0)
                        UpdateRichTextBox("lineCounter", "Number of line before reformating : " + nbLinesBefore);
                    if (nbLinesAfter > 0)
                        UpdateRichTextBox("lineCounter", "Number of line after reformating : " + nbLinesAfter);
                }
            }

            else
            {
                UpdateRichTextBox("superfail", "GLOBAL PROCESS FAILED, sorry");
                counter = _NumberOfProcesses;
                UpdateProgressBarPut100Percent();
            }

            // Display elapsed time
            launchStopwatch.Stop();
            TimeSpan ts = launchStopwatch.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
         
            UpdateProgressBarPut100Percent();
            DisplayConfigProcessTime(elapsedTime);         
        }

        /**************\
         * PREPROCESS *
        \**************/

        private void PreProcess(DataTable dataTable)
        {
            Stopwatch launchStopwatch = new Stopwatch();         
            switch (dataTable.TableName)
            {
                case "XLS2TXT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "XLS2TXT..... ");
                    try
                    {
                        XLS2TXT.XLS2TXT xls2txtProcess = new XLS2TXT.XLS2TXT(dataTable, _InputFiles);
                        xls2txtProcess.Run();
                        _InputFiles = xls2txtProcess.FinalPaths;
                        xls2txtProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex) 
                    { 
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== XLS2TXT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "XML2TXT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "XML2TXT.....");
                    try
                    {
                        XML2TXT.XML2TXT xml2txtprocess = new XML2TXT.XML2TXT(_InputFiles);
                        xml2txtprocess.Run();
                        _InputFiles = xml2txtprocess.FinalPaths;
                        xml2txtprocess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== XML2TXT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "XMLMERGE":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "XMLMERGE.....");
                    try
                    {
                        string finalPath;
                        finalPath = Path.GetDirectoryName(_InputFiles[0]) + "\\" + "Results.xml";
                        Xml_Merge.Merge xmlmergeProcess = new Xml_Merge.Merge(dataTable, _InputFiles, finalPath);
                        xmlmergeProcess.run();
                        _Logs += "\r\n== XMLMERGE PROCESS ==\r\n";
                        foreach (String logLine in xmlmergeProcess.log)
                            _Logs += logLine + "\r\n";
                        _InputFiles.Clear();
                        _InputFiles.Add(finalPath);
                        xmlmergeProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== XML2TXT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "FILESPLIT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "FILESPLIT..... ");
                    try
                    {
                        Text.FileSplit fileSplitProcess = new Text.FileSplit(dataTable, _InputFiles, _Config.Get_DataSeparator());
                        fileSplitProcess.Run();
                        _InputFiles = fileSplitProcess.FinalPaths;
                        fileSplitProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex) 
                    { 
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== FILESPLIT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "COLUMNSCONCAT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "COLUMNSCONCAT..... ");
                    try
                    {
                        Files.FileConcat columnsConcatProcess = new Files.FileConcat(_InputFiles, dataTable, _Config.Get_DataSeparator());
                        columnsConcatProcess.Run();
                        _Logs += "\r\n== COLUMNSCONCAT PROCESS ==\r\n";
                        foreach (String logLine in columnsConcatProcess.Log)
                            _Logs += logLine + "\r\n";
                        foreach (String fileToDelete in _InputFiles)
                            File.Delete(fileToDelete);
                        _InputFiles = columnsConcatProcess.files;
                        columnsConcatProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex) 
                    { 
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== COLUMNSCONCAT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "LINESCONCAT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "LINESCONCAT..... ");
                    try
                    {
                        Files.LineConcat linesConcatProcess = new Files.LineConcat(_InputFiles, dataTable, _Config.Get_DataSeparator());
                        linesConcatProcess.Run();
                        _Logs += "\r\n== LINESCONCAT PROCESS ==\r\n";
                        foreach (String fileToDelete in _InputFiles)
                            File.Delete(fileToDelete);
                        _InputFiles = linesConcatProcess.files;
                        linesConcatProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex) 
                    { 
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== LINESCONCAT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;
            }
            GC.Collect();         
        }

        /***********\
         * PROCESS *
        \***********/

        private void Process(DataTable dataTable)
        {
            Stopwatch launchStopwatch = new Stopwatch();
            switch (dataTable.TableName)
            {
                case "REPLACE":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "REPLACE..... ");
                    try
                    {
                        Text.TextRemplacement replaceProcess = new Text.TextRemplacement(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = replaceProcess.Run();
                        _Logs += "\r\n== REPLACE PROCESS ==\r\n";
                        foreach(String logLine in replaceProcess.Log)
                            _Logs += logLine + "\r\n";
                        replaceProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex) 
                    {   ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== REPLACE ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "CUT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "CUT..... ");
                    try
                    {
                        Text.TextCut cutProcess = new Text.TextCut(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = cutProcess.Run();
                        _Logs += "\r\n== CUT PROCESS ==\r\n";
                        foreach (String logLine in cutProcess.Log)
                            _Logs += logLine + "\r\n";
                        cutProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== CUT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "COLUMNS":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "COLUMNS..... ");
                    try
                    {
                        Text.TextColumns columnsProcess = new Text.TextColumns(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = columnsProcess.Run();
                        _Logs += "\r\n== COLUMNS PROCESS ==\r\n";
                        foreach (String logLine in columnsProcess.Log)
                            _Logs += logLine + "\r\n";
                        columnsProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== COLUMNS ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "COLUMNDELETER":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "COLUMNDELETER..... ");
                    try
                    {
                        MyTextColumnsDeleter.TextColumnsDeleter columnDeleterProcess = new MyTextColumnsDeleter.TextColumnsDeleter(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = columnDeleterProcess.Run();
                        _Logs += "\r\n== COLUMNDELETER ==\r\n";
                        foreach (String logLine in columnDeleterProcess.Log)
                            _Logs += logLine + "\r\n";
                        columnDeleterProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== COLUMNDELETER ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "COLUMNMOVER":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "COLUMNMOVER..... ");
                    try
                    {
                        MyTextColumnsDeleter.ColumnMove columnMoverProcess = new MyTextColumnsDeleter.ColumnMove(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = columnMoverProcess.run();
                        foreach (String logLine in columnMoverProcess.Log)
                            _Logs += logLine + "\r\n";
                        columnMoverProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== COLUMNMOVER ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                        _Logs += "\r\n== COLUMNMOVER ==\r\n";

                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "EXPAND":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "EXPAND..... ");
                    try
                    {
                        Text.TextExpand expandProcess = new Text.TextExpand(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = expandProcess.Run();
                        _Logs += "\r\n== EXPAND PROCESS ==\r\n";
                        foreach (String logLine in expandProcess.Log)
                            _Logs += logLine + "\r\n";
                        expandProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== EXPAND ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "FILTER":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "FILTER..... ");
                    try
                    {
                        Text.TextFilter filterProcess = new Text.TextFilter(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = filterProcess.Run();
                        _Logs += "\r\n== FILTER PROCESS ==\r\n";
                        foreach (String logLine in filterProcess.Log)
                            _Logs += logLine + "\r\n";
                        filterProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== FILTER ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "LINEDEL":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "LINEDEL..... ");
                    try
                    {
                        LineDeleter.LineDeleter lineDeleterProcess = new LineDeleter.LineDeleter(_TabData, dataTable, _Config.Get_Headerlines(),_Config.Get_DataSeparator(), _CurrentFile);
                        _TabData = lineDeleterProcess.Run();
                        lineDeleterProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== LINEDEL ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "DATECONVERT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "DATECONVERT..... ");
                    try
                    {
                        Text.TextDateConvert dateConvertProcess = new Text.TextDateConvert(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = dateConvertProcess.Run();
                        _Logs += "\r\n== DATECONVERT PROCESS ==\r\n";
                        foreach (String logLine in dateConvertProcess.Log)
                            _Logs += logLine + "\r\n";
                        dateConvertProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== DATECONVERT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "TRANSLATE":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "TRANSLATE..... ");
                    try
                    {
                        Text.TextTranslate translateProcess = new Text.TextTranslate(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = translateProcess.Run();
                        _Logs += "\r\n== TRANSLATE PROCESS ==\r\n";
                        foreach (String logLine in translateProcess.Log)
                            _Logs += logLine + "\r\n";
                        translateProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== TRANSLATE ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "TRANSCRIPT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "TRANSCRIPT..... ");
                    try
                    {
                        Text.TextTranscript transcriptProcess = new Text.TextTranscript(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = transcriptProcess.Run();
                        _Logs += "\r\n== TRANSCRIPT PROCESS ==\r\n";
                        foreach (String logLine in transcriptProcess.Log)
                            _Logs += logLine + "\r\n";
                        transcriptProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== TRANSCRIPT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "DATEFORMAT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "DATEFORMAT..... ");
                    try
                    {
                        DateTime.Date dateformatProcess = new DateTime.Date(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines(), _CurrentFile);
                        _TabData = dateformatProcess.Run();
                        dateformatProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== DATEFORMAT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "TIMEFORMAT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "TIMEFORMAT..... ");
                    try
                    {
                        DateTime.TimeFormat timeformatProcess = new DateTime.TimeFormat(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = timeformatProcess.Run();
                        _Logs += "\r\n== TIMEFORMAT PROCESS ==\r\n";
                        foreach (String logLine in timeformatProcess.Log)
                            _Logs += logLine + "\r\n";
                        timeformatProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== TIMEFORMAT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "TIMECONVERT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "TIMECONVERT..... ");
                    try
                    {
                        DateTime.TimeConvert timeconvertProcess = new DateTime.TimeConvert(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = timeconvertProcess.Run();
                        _Logs += "\r\n== TIMECONVERT PROCESS ==\r\n";
                        foreach (String logLine in timeconvertProcess.Log)
                            _Logs += logLine + "\r\n";
                        timeconvertProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== TIMECONVERT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "WRITE":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "WRITE..... ");
                    try
                    {
                        Tools.Write writeProcess = new Tools.Write(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = writeProcess.Run();
                        _Logs += "\r\n== WRITE PROCESS ==\r\n";
                        foreach (String logLine in writeProcess.Log)
                            _Logs += logLine + "\r\n";
                        writeProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== WRITE ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "COPY":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "COPY..... ");
                    try
                    {
                        Text.TextCopy copyProcess = new Text.TextCopy(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = copyProcess.Run();
                        _Logs += "\r\n== COPY PROCESS ==\r\n";
                        foreach (String logLine in copyProcess.Log)
                            _Logs += logLine + "\r\n";
                        copyProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== COPY ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "CELLCOPY":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "CELLCOPY..... ");
                    try
                    {
                        Text.TextCellCopy cellcopyProcess = new Text.TextCellCopy(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = cellcopyProcess.Run();
                        _Logs += "\r\n== CELLCOPY PROCESS ==\r\n";
                        foreach (String logLine in cellcopyProcess.Log)
                            _Logs += logLine + "\r\n";
                        cellcopyProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== CELLCOPY ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "VALUECHECKER":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "VALUECHECKER..... ");
                    try
                    {
                        Controls.ControlValue valuecheckerProcess = new Controls.ControlValue(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines(), _Config.Get_TargetsNumber(), _Config.Get_DecimalSeparator());
                        _TabData = valuecheckerProcess.Run();
                        _Logs += "\r\n== VALUECHECKER PROCESS ==\r\n";
                        foreach (String logLine in valuecheckerProcess.Log)
                            _Logs += logLine + "\r\n";
                        valuecheckerProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== VALUECHECKER ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "VALUECORRECTOR":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "VALUECORRECTOR..... ");
                    try
                    {
                        Controls.CorrectValue valuecorrectorProcess = new Controls.CorrectValue(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_TargetsNumber(), _Config.Get_DecimalSeparator());
                        _TabData = valuecorrectorProcess.Run();
                        _Logs += "\r\n== VALUECORRECTOR PROCESS ==\r\n";
                        foreach (String logLine in valuecorrectorProcess.Log)
                            _Logs += logLine + "\r\n";
                        valuecorrectorProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== VALUECORRECTOR ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "QHFORMAT":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "QHFORMAT..... ");
                    try
                    {
                        QH.TotalTV qhformatProcess = new QH.TotalTV(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines(), _Config.Get_TargetsNumber());
                        _TabData = qhformatProcess.Run();
                        _Logs += "\r\n== QHFORMAT PROCESS ==\r\n";
                        foreach (String logLine in qhformatProcess.Log)
                            _Logs += logLine + "\r\n";
                        qhformatProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== QHFORMAT ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "REMOVER":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "REMOVER..... ");
                    try
                    {
                        Remover.RemDoublons removerProcess = new Remover.RemDoublons(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = removerProcess.Run();
                        _Logs += "\r\n== REMOVE DOUBLONS ==\r\nDone.";
                        removerProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== REMOVER ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "CALCUL":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "CALCUL..... ");
                    try
                    {
                        Numbers.Calculator calculProcess = new Numbers.Calculator(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = calculProcess.Run();
                        _Logs += "\r\n== CALCUL ==\r\n";
                        foreach (String logLine in calculProcess.Log)
                            _Logs += logLine + "\r\n";
                        calculProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== CALCUL ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "LEVELS":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "LEVELS..... ");
                    try
                    {
                        Levels.Levels levelsProcess = new Levels.Levels(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines());
                        _TabData = levelsProcess.Run();
                        _Logs += "\r\n== LEVELS ==\r\n";
                        foreach (String logline in levelsProcess.Log)
                            _Logs += logline + "\r\n";
                        levelsProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== LEVELS ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "DUPLICATES":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "DUPLICATES..... ");
                    try
                    {
                        Text.RemoveDuplicate duplicatesProcess = new Text.RemoveDuplicate(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = duplicatesProcess.Run();
                        _Logs += "\r\n== DUPLICATES ==\r\n";
                        foreach (String logline in duplicatesProcess.Log)
                            _Logs += logline + "\r\n";
                        duplicatesProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== DUPLICATES ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "TRANSPOSE":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "TRANSPOSE..... ");
                    try
                    {
                        Transpose.Transpose transposeProcess = new Transpose.Transpose(_TabData, dataTable, _Config.Get_DataSeparator());
                        _TabData = transposeProcess.run();
                        _Logs += "\r\n== TRANSPOSE ==\r\n";
                        foreach (String logline in transposeProcess.log)
                            _Logs += logline + "\r\n";
                        transposeProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== TRANSPOSE ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;
            }
            GC.Collect();
        }

        /***********\
         * CONTROL *
        \***********/

        private void Control(DataTable dataTable)
        {
            Stopwatch launchStopwatch = new Stopwatch();
            switch (dataTable.TableName)
            {
                case "DATACHECKER":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "DATACHECKER..... ");
                    try
                    {
                        dataTable.Rows[0].SetField("XMLPath", (string)dataTable.Rows[0]["XMLPath"].ToString().Replace("Bases completes", "Bases complètes")); // Ghetto South Korea
                        DataChecking.DataChecking datacheckingProcess = new DataChecking.DataChecking(dataTable, _DatamodPath);
                        datacheckingProcess.Run();
                        datacheckingProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== DATACHECKER ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "CONTROLDIFF": // Note that this control (special for programs) performs the renaming of the datamod
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "CONTROLDIFF..... ");
                    try
                    {
                        String fileToDeletePath = _DatamodPath.Substring(0,_DatamodPath.Length - 8) + ".txt";
                        DateTime.ControlDiff controldiffProcess = new DateTime.ControlDiff(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines(), _Config.Get_TargetsNumber(), _DatamodPath);
                        controldiffProcess.Run();
                        if (controldiffProcess.dateDebut.Equals(controldiffProcess.dateFin))
                            controldiffProcess.dateFin = "";
                        else
                            controldiffProcess.dateFin = "-" + controldiffProcess.dateFin;
                        String finalPath = Path.GetDirectoryName(_DatamodPath) + "\\" + _Config.Get_CountryCode() + "_" + controldiffProcess.dateDebut + controldiffProcess.dateFin + ".txt";
                        System.IO.File.Move(_DatamodPath, finalPath);
                        String tmpPath = Path.GetDirectoryName(_DatamodPath);
                        _DatamodPath = finalPath;

                        // Suppress input file in txt (only in case of XLS2TXT)
                        Boolean deleteOldFile = false;
                        foreach (Process process in _Config.Get_ProcessList())
                        {
                            if (process.Get_Name().Equals("XLS2TXT"))
                                deleteOldFile = true;
                        }
                        if (deleteOldFile == true)
                            File.Delete(fileToDeletePath);  // Delete the old file.

                        _OutputFiles.Add(_DatamodPath); // Add the output file to the list of output file.
                        controldiffProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== CONTROLDIFF ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "QHNUMBERS": // Note that this control (special for QH) performs the renaming of the datamod
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "QHNUMBERS..... ");
                    try
                    {
                        String fileToDeletePath = _DatamodPath.Substring(0, _DatamodPath.Length - 8) + ".txt";
                        QH.LineControl qhnumbersProcess = new QH.LineControl(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines(), _Config.Get_TargetsNumber());
                        qhnumbersProcess.Run();
                        if (qhnumbersProcess.dateDebut.Equals(qhnumbersProcess.dateFin))
                            qhnumbersProcess.dateFin = "";
                        else
                            qhnumbersProcess.dateFin = "-" + qhnumbersProcess.dateFin;
                        String finalPath = Path.GetDirectoryName(_DatamodPath) + "\\" + _Config.Get_CountryCode() + "_" + qhnumbersProcess.dateDebut + qhnumbersProcess.dateFin + ".txt";
                        System.IO.File.Move(_DatamodPath, finalPath);
                        String tmpPath = Path.GetDirectoryName(_DatamodPath);
                        _DatamodPath = finalPath;

                        // Suppress input file in txt (only in case of XLS2TXT)
                        Boolean deleteOldFile = false;                  
                        foreach (Process process in _Config.Get_ProcessList())
                        {
                            if (process.Get_Name().Equals("XLS2TXT"))
                                deleteOldFile = true;
                        }
                        if (deleteOldFile == true)
                            File.Delete(fileToDeletePath);  // Delete the old file.

                        _OutputFiles.Add(_DatamodPath); // Add the output file to the list of output file.
                        _Logs += "\r\n== CONTROL LINES QH ==\r\n";
                        foreach (String logLine in qhnumbersProcess.Log)
                            _Logs += logLine + "\r\n";
                        qhnumbersProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== QHNUMBERS ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "TOTALTVCONTROL":
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "TOTALTVCONTROL..... ");
                    try
                    {
                        QH.ControlTTV totaltvcontrolProcess = new QH.ControlTTV(_TabData, dataTable, _Config.Get_DataSeparator(), _Config.Get_Headerlines(), _Config.Get_TargetsNumber());
                        totaltvcontrolProcess.Run();
                        _Logs += "\r\n== CONTROL TOTAL TV ==\r\n";
                        foreach (String logLine in totaltvcontrolProcess.Log)
                            _Logs += logLine + "\r\n";
                        totaltvcontrolProcess = null;
                        UpdateRichTextBox("complete", "Complete!");
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== TOTALTVCONTROL ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;

                case "TXT2XML":
                    _HasTxt2Xml = true;
                    StartStopWatch(launchStopwatch);
                    UpdateRichTextBox("function", "TXT2XML..... ");
                    try
                    {
                        string finalPath = Path.GetDirectoryName(_DatamodPath) + "\\" + Path.GetFileNameWithoutExtension(_DatamodPath) + ".xml";
                        TXT2XML.TXT2XML txt2XmlProcess = new TXT2XML.TXT2XML(_TabData, dataTable, finalPath, _Config.Get_DataSeparator());
                        txt2XmlProcess.Run();
                        File.Delete(_DatamodPath);
                        _Logs += "\r\n== TXT2XML ==\r\n";
                        foreach (String logLine in txt2XmlProcess.Log)
                            _Logs += logLine + "\r\n";
                        txt2XmlProcess = null;
                    }
                    catch (Exception ex)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("== TXT2XML ==\r\n" + ex.Message, "Analytics", MessageBoxButtons.OK);
                        UpdateRichTextBox("fail", "FAIL");
                    }
                    StopStopWatch(launchStopwatch);
                    break;
            }

            GC.Collect();
        }

        /**********************\
         * HEADER CONSISTENCY *
        \**********************/

        private void HeaderConsistency()
        {
            if (!_HasTxt2Xml)
            {
                HeaderConsistency.HC headerConsistencyProcess = new global::HeaderConsistency.HC(Properties.Settings.Default.hc_config, _DatamodPath, _Config.Get_Name());
                headerConsistencyProcess.RunControl();
                GC.Collect();
            }
        }

        /****************************\
         * Update Progression & RTB *
        \****************************/

        private void UpdateProgressBar(float counter)
        {
            _Progress = (counter / (float)(_NumberOfProcesses + 1)) * 100; // + Filecounter for the processes of analyzing logs.
            if(_Progress <= 100)
                _UpdateProgressBar.DynamicInvoke(new int[] { _ID, (int)_Progress });
        }

        private void UpdateProgressBarPut100Percent()
        {
            _UpdateProgressBar.DynamicInvoke(new int[] { _ID, (int)100 });
        }

        private void UpdateRichTextBox(String type, String message)
        {
            String[] test = new String[] { _ID.ToString(), type, message };
            _UpdateRichTextBox.DynamicInvoke(new Object[]{test});
        }

        /********************************\
         * Display Process Time (total) *
        \********************************/

        private void DisplayConfigProcessTime(string time)
        {
            _DisplayConfigProcessTime.DynamicInvoke(_ID, time);
        }

        /***************************************\
         * Create datagrid view with combo box *
        \***************************************/

        private void AddLogsGridView(string outputFile, string inputFile)
        {
            _AddLogsGridView.DynamicInvoke(_ID, outputFile, inputFile, _Config.Get_ProcessList(), _Config.Get_TargetsNumber());
        }

        /**********************************************************\
         * Convert the content of a file into a list of String.   *
         *   - Create a StreamReader.                             *
         *   - Read each line of the stream and add it to the LS. *
         *   - Return the LS.                                     *
        \**********************************************************/

        private List<String> FileToLS(String file)
        {
            List<String> LS = new List<String>();

            try
            {
                StreamReader streamReader = new StreamReader(file, System.Text.Encoding.GetEncoding(_Config.Get_EncodingInput()));

                while (!streamReader.EndOfStream)
                {
                    LS.Add(streamReader.ReadLine());
                }

                streamReader.Close();
                streamReader.Dispose();
            }

            catch(Exception ex)
            {
                KryptonMessageBox.Show("Error lors de la conversion fichier - liste de Strings, verifier que le fichier n'est pas ouvert\n\n" + ex, "File To List of String error!",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Exclamation);
            }

            return LS;
        }

        /*******************************************************\
         * Write Datamod.                                      *
         *   - Create StreamWriter (+ define encoding output). *
         *   - Define input/output separator.                  *
         *   - Write datamod (+ modify separator or not).      *
         *   - Close & dispose SW.                             *
        \*******************************************************/

        private int WriteDatamod()
        {
            // Create StreamWriter (+ define encoding output).
            StreamWriter streamWriter = new StreamWriter(_DatamodPath, false, System.Text.Encoding.Default);

            if (!_Config.Get_EncodingOutput().Equals("utf-8"))
            {
                streamWriter.Dispose();
                streamWriter = new StreamWriter(_DatamodPath, false, System.Text.Encoding.GetEncoding(_Config.Get_EncodingOutput()));
            }

            // Define input/output separator.
            String outputSeparatorTmp = "";
            String inputSeparatorTmp = "";
            if (!String.IsNullOrEmpty(_Config.Get_OutputSeparator()))
            {
                if (_Config.Get_OutputSeparator().Equals("tab") || _Config.Get_OutputSeparator().Equals("\t") || _Config.Get_OutputSeparator().Equals("^t"))
                    outputSeparatorTmp = "\t";
                else outputSeparatorTmp = _Config.Get_OutputSeparator();
            }
            if (_Config.Get_DataSeparator().Equals("tab") || _Config.Get_DataSeparator().Equals("\t") || _Config.Get_DataSeparator().Equals("^t"))
                inputSeparatorTmp = "\t";
            else inputSeparatorTmp = _Config.Get_DataSeparator();

            // Write datamod (+ modify separator or not).
            int lineCounter = 0;
            if (String.IsNullOrEmpty(outputSeparatorTmp))
                foreach (String line in _TabData)
                {
                    streamWriter.WriteLine(line);
                    lineCounter++;
                }
            else 
                foreach (String line in _TabData)
                {
                    String[] splitter = line.Split(inputSeparatorTmp.ToCharArray());
                    streamWriter.WriteLine(String.Join(outputSeparatorTmp,splitter));
                    lineCounter++;
                }

            // Close & dispose SW 
            streamWriter.Close();
            streamWriter.Dispose();

            return lineCounter;
        }

        /******************************\
         * Write Logs.                *
         *   -  Create StreamWriter.  *
         *   -  Write logs.           *
         *   -  Close & dispose SW.   *
        \******************************/

        private void WriteLogs()
        {
            StreamWriter streamWriter = new StreamWriter(_DatamodPath.Substring(0, _DatamodPath.Length - 4) + ".log", true, System.Text.Encoding.Default);
            streamWriter.WriteLine(_Logs);
            streamWriter.Close();
            streamWriter.Dispose();
            _Logs = null;
        }

        /*************************************\
         * Stopwatch functions               *
         *   - Start stopwatch.              *
         *   - Stop stopwatch (& update RTB) *
        \*************************************/

        private void StartStopWatch(Stopwatch stopWatch)
        {
            stopWatch.Start();
        }

        private void StopStopWatch(Stopwatch stopWatch)
        {
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            UpdateRichTextBox("time", elapsedTime);
        }

        #endregion

        #region Accessors

        public List<String> Get_OutputList()
        {
            return _OutputFiles;
        }


        #endregion
    }
}
