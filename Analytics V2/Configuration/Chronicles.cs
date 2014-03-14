using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DataGridViewAutoFilter;

namespace Analytics_V2
{
    public partial class Chronicles : UserControl
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Chronicles()
        {
            InitializeComponent();
            DataGridView.BindingContextChanged += new EventHandler(DataGridView_BindingContextChanged);
            this.Dock = DockStyle.Fill;

            LinesAddedLabel.ForeColor = Color.DarkGreen;
            LinesDeletedLabel.ForeColor = Color.IndianRed;

            //BindingSource bs = new BindingSource();
            //bs.DataSource = DataGridView.DataSource;
            //DataGridView.DataSource = bs;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /**********************************************\
         * Get All chronicles for a specific config : *
         *    - Query on DB & bind to Datagrid View   *
        \**********************************************/

        public void GetChroniclesFromSpecificConfig(string name)
        {
            try
            {
                // Query on DB & bind to Datagrid View
                AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                DataSet dataSet = service.Get_histo_modif_per_config(name);
                //DataGridView.DataSource = dataSet.Tables[0];

                BindingSource bs = new BindingSource();
                bs.DataSource = dataSet.Tables[0];
                DataGridView.DataSource = bs;

                service.Close();          
            }

            catch (Exception ex) { KryptonMessageBox.Show(ex.ToString()); }    
        }

        /**********************************************\
         * Get All chronicles                         *
         *    - Query on DB & bind to Datagrid View   *
        \**********************************************/

        public void GetAllChronicles()
        {
            try
            {
                // Query on DB & bind to Datagrid View
                AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                DataSet dataSet = service.Get_all_histo_modif_per_config();
                //DataGridView.DataSource = dataSet.Tables[0];

                BindingSource bs = new BindingSource();
                bs.DataSource = dataSet.Tables[0];
                DataGridView.DataSource = bs;
               
                service.Close();
            }
            catch (Exception ex) { KryptonMessageBox.Show(ex.ToString()); } 

            //foreach(DataGridViewColumn col in DataGridView.Columns)
            //{
            //    col.
            //}
            
        }

        /**************************************************************\
         * Get specific chronicle of modif selected in DataGridView : *
         *    - Query on DB & bind to RTBs + status bar               *
        \**************************************************************/

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BeforeRichTextBox.Clear();
            AfterRichTextBox.Clear();

            if (e.RowIndex != -1)
            {
                try
                {
                    string pathAtModif = null;
                    int linesDeleted = 0;
                    int linesAdded = 0;
                    string before = null;
                    string after = null;
                    Boolean trigger = false;
                    Boolean appended = false;

                    // Query on DB
                    AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                    DataSet dataSet = service.Get_histo_modif_per_config_specific(int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    service.Close();

                    // Get data
                    pathAtModif = dataSet.Tables[0].Rows[0]["path_at_modif"].ToString();
                    linesDeleted = int.Parse(dataSet.Tables[0].Rows[0]["lines_deleted"].ToString());
                    linesAdded = int.Parse(dataSet.Tables[0].Rows[0]["lines_added"].ToString());
                    before = dataSet.Tables[0].Rows[0]["config_before"].ToString();
                    after = dataSet.Tables[0].Rows[0]["config_after"].ToString();
                    string[] configBefore = before.Split(new string[] { "\n" }, StringSplitOptions.None);
                    string[] configAfter = after.Split(new string[] { "\n" }, StringSplitOptions.None);

                    // Bind data to status bar.
                    if (pathAtModif.Contains("POLE INFORMATIQUE"))
                        PathAtModifLabel.Text = "Path at modif : MIMAS\\ ... " + pathAtModif.Split(new string[] { "POLE INFORMATIQUE" }, StringSplitOptions.None)[1];
                    else
                        PathAtModifLabel.Text = "Path at modif : " + pathAtModif;

                    LinesDeletedLabel.Text = "Lines deleted : " + linesDeleted;
                    LinesAddedLabel.Text = "Lines added : " + linesAdded;

                    // Bind data to RTBs

                    IEnumerable<String> onlyInOldConfig = configBefore.Except(configAfter);
                    IEnumerable<String> onlyInNewConfig = configAfter.Except(configBefore);

                    // BEFORE
                    for (int i = 0; i < configBefore.Length; i++)
                    {
                        foreach (string str in onlyInOldConfig)
                        {
                            if (configBefore[i].Contains("<Function"))
                                if (configBefore[i + 1] != null && configBefore[i + 1].Equals(str))
                                {
                                    trigger = true;
                                    //BeforeRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    //BeforeRichTextBox.SelectionBackColor = Color.IndianRed;
                                    //BeforeRichTextBox.AppendText(configBefore[i] + "\n");
                                    //appended = true;
                                }

                            if (configBefore[i].Equals(str) && !appended)
                            {
                                BeforeRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                BeforeRichTextBox.SelectionBackColor = Color.IndianRed;
                                BeforeRichTextBox.AppendText(configBefore[i] + "\n");
                                appended = true;

                            }
                        }

                        if (!appended)
                        {
                            if (trigger)
                            {
                                BeforeRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                BeforeRichTextBox.SelectionBackColor = Color.IndianRed;
                                BeforeRichTextBox.AppendText(configBefore[i] + "\n");
                                if (configBefore[i].Contains("</Function>"))
                                    trigger = false;
                            }

                            else
                            {
                                BeforeRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                BeforeRichTextBox.AppendText(configBefore[i] + "\n");
                            }
                        }
                        appended = false;
                    }

                    // AFTER
                    for (int i = 0; i < configAfter.Length; i++)
                    {
                        foreach (string str in onlyInNewConfig)
                        {
                            if (configAfter[i].Contains("<Function"))
                                if (configAfter[i + 1] != null && configAfter[i + 1].Equals(str))
                                {
                                    trigger = true;
                                    //AfterRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    //AfterRichTextBox.SelectionBackColor = Color.LightGreen;
                                    //AfterRichTextBox.AppendText(configAfter[i] + "\n");
                                    //appended = true;
                                }

                            if (configAfter[i].Equals(str) && !appended)
                            {
                                AfterRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                AfterRichTextBox.SelectionBackColor = Color.LightGreen;
                                AfterRichTextBox.AppendText(configAfter[i] + "\n");
                                appended = true;

                            }
                        }

                        if (!appended)
                        {
                            if (trigger)
                            {
                                AfterRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                AfterRichTextBox.SelectionBackColor = Color.LightGreen;
                                AfterRichTextBox.AppendText(configAfter[i] + "\n");
                                linesAdded++;
                                if (configAfter[i].Contains("</Function>"))
                                    trigger = false;
                            }
                            else
                            {
                                AfterRichTextBox.SelectionFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                AfterRichTextBox.AppendText(configAfter[i] + "\n");
                            }
                        }

                        appended = false;
                    }

                    BeforeRichTextBox.SelectionStart = 0;
                    BeforeRichTextBox.ScrollToCaret();

                    AfterRichTextBox.SelectionStart = 0;
                    AfterRichTextBox.ScrollToCaret();
                }

                catch (Exception ex) { KryptonMessageBox.Show(ex.ToString()); }
            }
        }

        /******************************\
         * Adjust display of datagrid *
        \******************************/

        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            PathAtModifLabel.Text = "";
            LinesAddedLabel.Text = "";
            LinesDeletedLabel.Text = "";
            BeforeRichTextBox.Clear();
            AfterRichTextBox.Clear();

            DataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridView.Columns[1].Width = 135;
            DataGridView.Columns[2].Width = 135;
            DataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //DataGridView1.AutoResizeColumns();
            DataGridView.Columns[2].HeaderText = "Modification Date";

            //DataGridView.Sort(DataGridView.Columns[2], System.ComponentModel.ListSortDirection.Descending);

            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                if (row.Cells[4].Value != null && row.Cells[4].Value.ToString().Equals("Prod"))
                    row.Cells[4].Style.ForeColor = System.Drawing.Color.Green;
                else if (row.Cells[4].Value != null && row.Cells[4].Value.ToString().Equals("PreProd"))
                    row.Cells[4].Style.ForeColor = System.Drawing.Color.Orange;
                else if (row.Cells[4].Value != null && row.Cells[4].Value.ToString().Equals("Other"))
                    row.Cells[4].Style = new DataGridViewCellStyle { ForeColor = Color.Purple };

                foreach (DataGridViewCell cell in row.Cells)
                {
                  cell.Style.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }

            DataGridView.ClearSelection();
            
        }


        private void DataGridView_BindingContextChanged(object sender, EventArgs e)
        {
            if (DataGridView.DataSource == null) return;

            foreach (DataGridViewColumn col in DataGridView.Columns)
            {
                col.HeaderCell = new
                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

           // DataGridView1.AutoResizeColumns();
        }

        #endregion

        






    }
}
