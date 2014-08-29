using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Analytics_V2
{
    public partial class ConfigSummary : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        List<Config> _AllConfigs; // All config (prod)

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public ConfigSummary()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            _AllConfigs = new List<Config>();

            InitializeDGVGraphical();
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /********************************\
         * Initialize header graphicals *
        \********************************/

        private void InitializeDGVGraphical()
        {
            // Change font of header row.
            foreach (DataGridViewColumn column in DataGridView.Columns)
                column.HeaderCell.Style.Font = new Font(DataGridView.Font, FontStyle.Bold);

            // Define header colors
            DataGridView.Columns["Xls2txt"].HeaderCell.Style.BackColor = Color.NavajoWhite;
            DataGridView.Columns["Xml2txt"].HeaderCell.Style.BackColor = Color.NavajoWhite;
            DataGridView.Columns["Xmlmerge"].HeaderCell.Style.BackColor = Color.NavajoWhite;
            DataGridView.Columns["Filesplit"].HeaderCell.Style.BackColor = Color.NavajoWhite;
            DataGridView.Columns["Columnsconcat"].HeaderCell.Style.BackColor = Color.NavajoWhite;
            DataGridView.Columns["Linesconcat"].HeaderCell.Style.BackColor = Color.NavajoWhite;

            DataGridView.Columns["Calcul"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Cellcopy"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Columns"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["ColumnDeleter"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["ColumnMover"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Copy"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Cut"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Dateconvert"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Dateformat"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Duplicates"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Expand"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Filter"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Levels"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Linedel"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Qhformat"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Remover"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Replace"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Timeconvert"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Timeformat"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Transcript"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Translate"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Transpose"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Valuechecker"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["ValueCorrector"].HeaderCell.Style.BackColor = Color.MediumAquamarine;
            DataGridView.Columns["Write"].HeaderCell.Style.BackColor = Color.MediumAquamarine;

            DataGridView.Columns["Controldiff"].HeaderCell.Style.BackColor = Color.Thistle;
            DataGridView.Columns["Datachecker"].HeaderCell.Style.BackColor = Color.Thistle;
            DataGridView.Columns["Totaltvcontrol"].HeaderCell.Style.BackColor = Color.Thistle;
            DataGridView.Columns["Qhnumbers"].HeaderCell.Style.BackColor = Color.Thistle;
            DataGridView.Columns["txt2xml"].HeaderCell.Style.BackColor = Color.Thistle;
        }

        /*******************************\
         * Fill DataGridView (globaly) *
        \*******************************/

        public void FillDataGridView()
        {
            DataGridView.AllowUserToAddRows = true;
            FillConfigsList2();
            FillGrid();

            DataGridView.Sort(DataGridView.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            DataGridView.AllowUserToAddRows = false;
        }

        /*************************\
         * Fill list of configs: *
        \*************************/

        private void FillConfigsList()
        {
            _AllConfigs.Clear();

            // Get configs in prod
            DirectoryInfo prodPath = new DirectoryInfo(@"\\MIMAS\BUREAUTIQUE\GROUPES\Direction de l'International\POLE INFORMATIQUE\Developpement Internes\References Applications\Analytics");
            FileInfo[] files = prodPath.GetFiles();
        
            foreach (FileInfo file in files)
                if (file.FullName.Contains(".xml") && !file.FullName.Contains(".bak"))
                {
                    string[] splitResult = file.FullName.Split(new string[] { "\\" }, StringSplitOptions.None);
                    try
                    {
                        _AllConfigs.Add(new Config(file.Name.Replace(".xml", ""), file.FullName));
                    }
                    catch  { }
                }          
        }

        private void FillConfigsList2()
        {
            _AllConfigs.Clear();

            DirectoryInfo prodPath = new DirectoryInfo(@"\\MIMAS\BUREAUTIQUE\GROUPES\Direction de l'International\POLE INFORMATIQUE\Developpement Internes\References Applications\Analytics 2");
            //FileInfo[] files = prodPath.GetFiles();

            /* Treat configs in path */
            //TreatFiles(prodPath.GetFiles());

            /* Treat all other configs in sub folders */
            TreatDir(prodPath);

        }

        private void TreatFiles(FileInfo[] files)
        {
            foreach (FileInfo file in files)
                if (file.FullName.Contains(".xml") && !file.FullName.Contains(".bak"))
                {
                    string[] splitResult = file.FullName.Split(new string[] { "\\" }, StringSplitOptions.None);
                    try
                    {
                        _AllConfigs.Add(new Config(file.Name.Replace(".xml", ""), file.FullName));
                    }
                    catch { }
                }
        }

        private void TreatDir(DirectoryInfo directory)
        {
            TreatFiles(directory.GetFiles());

            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                if (!dir.Name.Contains("_RECETTES") && !dir.Name.Contains("_UTILITIES") && !dir.Name.Contains("Archived") && !dir.Name.Contains("HC") && !dir.Name.Contains("Process Templates") && !dir.Name.Contains("Configs Specifiques QH"))
                {
                    TreatDir(dir);
                }             
            }
        }


        /******************************************************\
         * Fill grid:                                         *
         *   for each config                                  *
         *      - create new row                              *
         *      - set column Config                           *
         *      - for each process in config                  *
         *             . If process present, update gridview  *
        \******************************************************/

        private void FillGrid()
        {
            DataGridView.Rows.Clear();

            foreach(Config config in _AllConfigs)
            {
                try
                {
                    DataGridViewRow row = (DataGridViewRow)DataGridView.Rows[0].Clone();
                    row.Cells[0].Value = config.Get_Name();
                    row.Cells[0].Style.Font = new Font(DataGridView.Font, FontStyle.Bold);

                    foreach (Process process in config.Get_ProcessList())
                    {
                        int i = 1;
                        if (process.Get_Name().Equals("COLUMNSCONCAT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("FILESPLIT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("LINESCONCAT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("XLS2TXT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("XML2TXT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("XMLMERGE"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("CALCUL"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("CELLCOPY"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("COLUMNS"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("COLUMNDELETER"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("COLUMNMOVER"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("COPY"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("CUT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("DATECONVERT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("DATEFORMAT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("DUPLICATES"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("EXPAND"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("FILTER"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("LEVELS"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("LINEDEL"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("QHFORMAT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("REMOVER"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("REPLACE"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("TIMECONVERT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("TIMEFORMAT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("TRANSCRIPT"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("TRANSLATE"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("TRANSPOSE"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("VALUECHECKER"))
                            CheckCell(row, i); 
                        i++;
                        if (process.Get_Name().Equals("VALUECORRECTOR"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("WRITE"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("CONTROLDIFF"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("DATACHECKER"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("QHNUMBERS"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("TOTALTVCONTROL"))
                            CheckCell(row, i);
                        i++;
                        if (process.Get_Name().Equals("TXT2XML"))
                            CheckCell(row, i);
                    }

                    DataGridView.Rows.Add(row);
                }
                catch { }
            }
        }

        private void CheckCell(DataGridViewRow row, int idColumn)
        {
            row.Cells[idColumn].Value = "X";
            row.Cells[idColumn].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            row.Cells[idColumn].Style.Font = new Font(DataGridView.Font, FontStyle.Bold);
            row.Cells[idColumn].Style.ForeColor = Color.ForestGreen;
        }

        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView.ClearSelection();
        }

        #endregion
    }
}
