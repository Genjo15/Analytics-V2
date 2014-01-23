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
    public partial class FileBrowser : UserControl
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private DirectoryInfo _Root;          // Directory info of the root.
        private DirectoryInfo[] _Directories; // Array of directories.
        private FileInfo[] _Files;            // Array of files.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public FileBrowser()
        {
            InitializeComponent();

            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*****************************************\
         * Populate the tree :                   *
         *  - Clear nodes                        *
         *  - Add the root.                      *
         *  - Recursively Add Files and Folders. *
         \****************************************/

        public void PopulateTreeView()
        {
            TreeView.Nodes.Clear();

            //_Root = new DirectoryInfo(@"C:\Users\CHHIMA\Desktop\Configs Analytics");
            //_Root = new DirectoryInfo(@"C:\Users\Anthony\Desktop\Configs Analytics");
            _Root = new DirectoryInfo(@"C:\Users\CHHIMA\Desktop\Analytics");

            if (Directory.Exists(_Root.FullName))
            {
                try
                {
                    TreeView.Nodes.Add(_Root.FullName);

                    AddDirectoryToNode(_Root, TreeView.Nodes[0]); 
                    AddFileToNode(_Root, TreeView.Nodes[0]);
                    TreeView.Nodes[0].Expand();
                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }


        /************************************************\
         * Add File to a node                           *
         *  - Check all files in a directory (only xml) *
         *  - Add them to the current node.             *
         \***********************************************/

        private void AddFileToNode(DirectoryInfo directory, TreeNode InputNode)
        {
            _Files = directory.GetFiles();

            if (_Files.Length > 0)
                foreach (FileInfo file in _Files)
                {
                    if (file.Name.Split('.')[1].Equals("xml"))
                    {
                        TreeNode node = InputNode.Nodes.Add(file.Name);
                        node.ImageIndex = node.SelectedImageIndex = 2;
                    }
                }
        }


        /*************************************\
         * Add directory to a node           *
         *  - Check all files in a directory *
         *  - Add them to the current node.  *
         \************************************/

        private void AddDirectoryToNode(DirectoryInfo directory, TreeNode InputNode)
        {
              _Directories = directory.GetDirectories();
       
              if(_Directories.Length > 0)
              foreach (DirectoryInfo dir in _Directories)
              {
                   TreeNode node = InputNode.Nodes.Add(dir.Name);
                   node.ImageIndex = node.SelectedImageIndex = 1;

                   AddDirectoryToNode(dir, node);
                   AddFileToNode(dir, node);
              }
        }

        #endregion

    }
}
