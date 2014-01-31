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

        private DirectoryInfo _Root;           // Directory info of the root.
        private DirectoryInfo[] _Directories;  // Array of directories.
        private FileInfo[] _Files;             // Array of files.
        private List<string> _ExpandedNodes; // List of all expanded nodes.
        private AnalyticsWebService.AnalyticsSoapClient _Request;
        private string _Path;


        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public FileBrowser()
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            _ExpandedNodes = new List<string>();
            _Request = new AnalyticsWebService.AnalyticsSoapClient();

            try
            {
                _Request.Open();
                _Path = _Request.Get_Path("ADM");
                _Request.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
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
            //_ExpandedNodes.Clear();
            

            //_Root = new DirectoryInfo(@"C:\Users\CHHIMA\Desktop\Analytics");
            _Root = new DirectoryInfo(_Path);

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

                List<string> pathList = new List<string>();
                foreach (string element in _ExpandedNodes)
                {
                    pathList.Add(element);
                }

                ExpandNode(TreeView.Nodes[0], pathList);
            }

            TreeView.Sort();
        }

        private void ExpandNode(TreeNode parent, List<string> pathsList)
        {
            // Start recursion on all subnodes
            foreach (TreeNode child in parent.Nodes)
            {
                foreach (string savedNode in pathsList)
                {
                    if (child.FullPath.Equals(savedNode))
                        child.Expand();
                        //MessageBox.Show("To expand : " + child.FullPath);
                }

                ExpandNode(child, pathsList);
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
                        node.Name = file.Name;
                        node.ImageIndex = node.SelectedImageIndex = 2;
                    }
                }
        }

        /*************************************\
         * Add directory to a node           *
         *  - Check all files in a directory *
         *  - Add them to the current node.  *
        \*************************************/

        private void AddDirectoryToNode(DirectoryInfo directory, TreeNode InputNode)
        {
              _Directories = directory.GetDirectories();
       
              if(_Directories.Length > 0)
              foreach (DirectoryInfo dir in _Directories)
              {
                   TreeNode node = InputNode.Nodes.Add(dir.Name);
                   node.ImageIndex = node.SelectedImageIndex = 1;
                   node.Name = dir.Name;

                   AddDirectoryToNode(dir, node);
                   AddFileToNode(dir, node);
              }
        }

        /********************************************\
         * Add/Remove node to list of expanded node *
        \********************************************/

        public void AddNodePath(TreeNode node)
        {
            _ExpandedNodes.Add(node.FullPath);
        }

        public void RemoveNodePath(TreeNode node)
        {
           //foreach (string element in _ExpandedNodes)
           //{
            for (int i = 0; i < _ExpandedNodes.Count; i++)
            {
                if (node.FullPath.Equals(_ExpandedNodes[i]))
                    _ExpandedNodes.Remove(_ExpandedNodes[i]);
            }
            //}
        }

        /**************************\
         * Expand all saved nodes *
        \**************************/

        //public void ExpandNodes()
        //{
        //    foreach (TreeNode element in _ExpandedNodes)
        //    {
        //        if (element != null)
        //             element.Expand();
        //    }
        //}

        #endregion

        

    }
}
