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
    public partial class ConfigStatement : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        List<Config> _AllConfigs; // All config (prod)

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public ConfigStatement()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            _AllConfigs = new List<Config>();
            //_ConfigTreeView = new TreeView();

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
            FillConfigsList();
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
                        if (process.Get_Name().Equals("COLUMNSCONCAT"))
                            CheckCell(row, 1);
                        if (process.Get_Name().Equals("FILESPLIT"))
                            CheckCell(row, 2);
                        if (process.Get_Name().Equals("LINESCONCAT"))
                            CheckCell(row, 3);
                        if (process.Get_Name().Equals("XLS2TXT"))
                            CheckCell(row, 4);
                        if (process.Get_Name().Equals("XML2TXT"))
                            CheckCell(row, 5);
                        if (process.Get_Name().Equals("XMLMERGE"))
                            CheckCell(row, 6);
                        if (process.Get_Name().Equals("CALCUL"))
                            CheckCell(row, 7);
                        if (process.Get_Name().Equals("CELLCOPY"))
                            CheckCell(row, 8);
                        if (process.Get_Name().Equals("COLUMNS"))
                            CheckCell(row, 9);
                        if (process.Get_Name().Equals("COLUMNDELETER"))
                            CheckCell(row, 10);
                        if (process.Get_Name().Equals("COLUMNMOVER"))
                            CheckCell(row, 11);
                        if (process.Get_Name().Equals("COPY"))
                            CheckCell(row, 12);
                        if (process.Get_Name().Equals("CUT"))
                            CheckCell(row, 13);
                        if (process.Get_Name().Equals("DATECONVERT"))
                            CheckCell(row, 14);
                        if (process.Get_Name().Equals("DATEFORMAT"))
                            CheckCell(row, 15);
                        if (process.Get_Name().Equals("DUPLICATES"))
                            CheckCell(row, 16);
                        if (process.Get_Name().Equals("EXPAND"))
                            CheckCell(row, 17);
                        if (process.Get_Name().Equals("FILTER"))
                            CheckCell(row, 18);
                        if (process.Get_Name().Equals("LEVELS"))
                            CheckCell(row, 19);
                        if (process.Get_Name().Equals("LINEDEL"))
                            CheckCell(row, 20);
                        if (process.Get_Name().Equals("QHFORMAT"))
                            CheckCell(row, 21);
                        if (process.Get_Name().Equals("REMOVER"))
                            CheckCell(row, 22);
                        if (process.Get_Name().Equals("REPLACE"))
                            CheckCell(row, 23);
                        if (process.Get_Name().Equals("TIMECONVERT"))
                            CheckCell(row, 24);
                        if (process.Get_Name().Equals("TIMEFORMAT"))
                            CheckCell(row, 25);
                        if (process.Get_Name().Equals("TRANSCRIPT"))
                            CheckCell(row, 26);
                        if (process.Get_Name().Equals("TRANSLATE"))
                            CheckCell(row, 27);
                        if (process.Get_Name().Equals("TRANSPOSE"))
                            CheckCell(row, 28);
                        if (process.Get_Name().Equals("VALUECHECKER"))
                            CheckCell(row, 29);
                        if (process.Get_Name().Equals("VALUECORRECTOR"))
                            CheckCell(row, 30);
                        if (process.Get_Name().Equals("WRITE"))
                            CheckCell(row, 31);
                        if (process.Get_Name().Equals("CONTROLDIFF"))
                            CheckCell(row, 32);
                        if (process.Get_Name().Equals("DATACHECKER"))
                            CheckCell(row, 33);
                        if (process.Get_Name().Equals("QHNUMBERS"))
                            CheckCell(row, 34);
                        if (process.Get_Name().Equals("TOTALTVCONTROL"))
                            CheckCell(row, 35);
                        if (process.Get_Name().Equals("TXT2XML"))
                            CheckCell(row, 36);
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
