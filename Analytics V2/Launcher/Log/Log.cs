using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics_V2
{
    public class Log
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private Boolean _PreProcess;        // Boolean which indicates if preprocess is enabled or not.
        private Boolean _Process;           // Boolean which indicates if process is enabled or not.
        private Boolean _Control;           // Boolean which indicates if control is enabled or not.

        private KryptonPage _NavigatorTab;
        private ButtonSpecAny _CloseButton;
        private KryptonSplitContainer _SplitContainer;
        private KryptonComboBox _ComboBox;
        private List<LogsGrid> _LogsGridViewList;

        private delegate void processOnLog(string outputFile, string inputFile, List<Process> pl, int number);
        private processOnLog _AddLogsGridViewDel;

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        //public Log(String tabName, Delegate del)
        public Log(String tabName, int targetsNumber, Boolean preProcess, Boolean process, Boolean control)
        {
            _PreProcess = preProcess;
            _Process = process;
            _Control = control;

            _NavigatorTab = new KryptonPage();
            _CloseButton = new ButtonSpecAny();

            _NavigatorTab.Name = tabName + "Tab";
            _NavigatorTab.Text = tabName;

            _CloseButton.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.PendantClose;
            _NavigatorTab.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            _CloseButton});

            _NavigatorTab.ButtonSpecs[0].Tag = _NavigatorTab;

            //_InitializeLogsGridViewDel = new processOnLog(InitializeLogsGridView);
            _AddLogsGridViewDel = new processOnLog(AddLogsGridView);

            _SplitContainer = new KryptonSplitContainer();
            _SplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            _SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            _SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            _SplitContainer.Location = new System.Drawing.Point(0, 0);
            _SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            _SplitContainer.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            _SplitContainer.SplitterWidth = 0;
            _SplitContainer.SplitterDistance = 49;
            _SplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(200, 15, 200, 15);
            _SplitContainer.Panel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            _SplitContainer.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            _SplitContainer.Panel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            _SplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);

            _NavigatorTab.Controls.Add(_SplitContainer);

            _ComboBox = new KryptonComboBox();
            _ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            _SplitContainer.Panel1.Controls.Add(_ComboBox);
            _ComboBox.SelectedIndexChanged += new EventHandler(DisplayLogsGridView);

            _LogsGridViewList = new List<LogsGrid>();
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        private void AddLogsGridView(string outputFile, string inputFile, List<Process> processList, int targetsNumber)
        {
            String[] splitResult = outputFile.Split(new char[] { '\\' });
            _ComboBox.Items.Add(splitResult[splitResult.Length - 1]); // datamod name.

            _LogsGridViewList.Add(new LogsGrid(splitResult[splitResult.Length - 1], processList, outputFile, inputFile,targetsNumber, _PreProcess,_Process,_Control));

            if (!_SplitContainer.Panel2.HasChildren)
            {
                _SplitContainer.Panel2.Controls.Add(_LogsGridViewList[0]);
                _ComboBox.SelectedIndex = 0;
            }
        }


        private void DisplayLogsGridView(object sender, EventArgs e)
        {
            _SplitContainer.Panel2.Controls.Clear();

            foreach (LogsGrid grid in _LogsGridViewList)
            {
                if (grid.DataGridView.Name.Equals(_ComboBox.SelectedItem))
                    _SplitContainer.Panel2.Controls.Add(grid);
                grid.DataGridView.ClearSelection();
            }
        }

        #endregion

        #region Accessors

        public KryptonPage Get_NavigatorTab()
        {
            return _NavigatorTab;
        }

        //public Delegate Get_InitializeLogsGridViewDel()
        //{
        //    return _InitializeLogsGridViewDel;
        //}

        public Delegate Get_AddLogsGridViewDel()
        {
            return _AddLogsGridViewDel;
        }

        #endregion
    }
}
