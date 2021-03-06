﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ComponentFactory.Krypton.Toolkit;

namespace Analytics_V2
{
    public partial class FileBrowser : UserControl
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private DirectoryInfo _Root;                              // Directory info of the root.
        private DirectoryInfo[] _Directories;                     // Array of directories.
        private FileInfo[] _Files;                                // Array of files.
        private List<string> _ExpandedNodes;                      // List of all expanded nodes.
        private string _Path;                                     // Path of root.


        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public FileBrowser(string path)
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            _ExpandedNodes = new List<string>();
            _Path = path;
            TreeView.LostFocus+=TreeView_LostFocus;
            TreeView.GotFocus+=TreeView_GotFocus;
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
            //this.TreeView.BeginUpdate();
            TreeView.Nodes.Clear();        

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

            //TreeView.Sort();
            //TreeView.TreeViewNodeSorter = new NodeSorter();

            SortByName(TreeView.Nodes[0]);
            SortByType(TreeView.Nodes[0]);

            //this.TreeView.EndUpdate();
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
            try
            {
                _Files = directory.GetFiles();
            }

            catch { }

            if (_Files.Length > 0)
                foreach (FileInfo file in _Files)
                {
                    //if (!String.IsNullOrEmpty(file.Name.Split('.')[1]) && file.Name.Split('.')[1].Equals("xml"))
                    if(file.Name.Contains(".xml") && !file.Name.Contains(".bak"))
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
            try
            {
                _Directories = directory.GetDirectories().Where(di => !di.Attributes.HasFlag(FileAttributes.System))
                                                         .Where(di => !di.Attributes.HasFlag(FileAttributes.Hidden))
                                                         .ToArray();
            }

            catch { }
       
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
            for (int i = 0; i < _ExpandedNodes.Count; i++)
            {
                if (node.FullPath.Equals(_ExpandedNodes[i]))
                    _ExpandedNodes.Remove(_ExpandedNodes[i]);
            }
        }

        /******************************************************\
         * Ensure that created item is visible (scroll to it) *
        \******************************************************/

        public void ScrollToCreatedItem(string itemPath)
        {
            BrowseAndSeek(TreeView.Nodes[0], itemPath);
        }

        private void BrowseAndSeek(TreeNode parent, string itemPath)
        {
            foreach (TreeNode element in parent.Nodes)
            {
                if (element.FullPath.Equals(itemPath))
                {
                    TreeView.TopNode = element;
                    if (element.Parent != null && element.Parent != TreeView.Nodes[0])
                        TreeView.TopNode = element.Parent;
                }
                else BrowseAndSeek(element, itemPath);
            }
        }

        /*****************************\
         * Sort (by type or by name) *
        \*****************************/

        private void SortByType(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                //SortByName(n);
                SortByType(n);
            }
            try
            {
                TreeNode temp = null;
                List<TreeNode> sortedNodes = new List<TreeNode>();
                while (node.Nodes.Count > 0)
                {
                    foreach (TreeNode n in node.Nodes)
                        if (temp == null || n.ImageIndex < temp.ImageIndex)
                            temp = n;
                    node.Nodes.Remove(temp);
                    sortedNodes.Add(temp);
                    temp = null;
                }
                node.Nodes.Clear();
                foreach (TreeNode a in sortedNodes)
                    node.Nodes.Add(a);
            }
            catch { }
        }

        private void SortByName(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
                SortByName(n);
            try
            {
                TreeNode temp = null;
                List<TreeNode> sortedNodes = new List<TreeNode>();
                while (node.Nodes.Count > 0)
                {
                    foreach (TreeNode n in node.Nodes)
                        //if (temp == null || n.Text[0] < temp.Text[0])
                        //    temp = n;
                        if (temp == null || String.Compare(n.Text,temp.Text,true)<0)
                                temp = n;
                    node.Nodes.Remove(temp);
                    sortedNodes.Add(temp);
                    temp = null;
                }
                node.Nodes.Clear();
                foreach (TreeNode a in sortedNodes)
                    node.Nodes.Add(a);
            }
            catch { }
        }

        /*********************************************************\
         * Highlight selected node when TreeView get/loses focus *
        \*********************************************************/

        private void TreeView_LostFocus(object sender, EventArgs e)
        {
            //if (TreeView.SelectedNode != null && TreeView.SelectedNode.ImageIndex == 2)
            //{
            //    //TreeView.SelectedNode.BackColor = Color.IndianRed;
            //    TreeView.SelectedNode.BackColor = Color.FromKnownColor(System.Drawing.KnownColor.Highlight);
            //    TreeView.SelectedNode.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.HighlightText);
            //}
        }

        private void TreeView_GotFocus(object sender, EventArgs e)
        {
            if (TreeView.SelectedNode != null && TreeView.SelectedNode.ImageIndex == 2)
            {
                TreeView.SelectedNode.BackColor = System.Drawing.SystemColors.ControlLight;
                TreeView.SelectedNode.ForeColor = Color.Black;
            }
        }

        public void SetPath(string path)
        {
            _Path = path;
        }

        /*************************\
         * Access node by letter *
        \*************************/

        // TO IMPLEMENT

        #endregion
    }




    // TO KEEP JUST IN CASE (override the NodeSorter)
    //public class NodeSorter : System.Collections.IComparer
    //{
    //    public int Compare(object x, object y)
    //    {
    //        TreeNode node1 = (TreeNode)x;
    //        TreeNode node2 = (TreeNode)y;
    //        int result = 0;
    //        //if (node1.Level == 1)
    //        //{
    //        //    return Convert.ToInt32(node1.Tag).CompareTo(Convert.ToInt32(node2.Tag));
    //        //}
    //        //else
    //        //{
    //        //    return node1.Index.CompareTo(node2.Index);
    //        //}
    //
    //        result = Convert.ToInt32(node1.ImageIndex).CompareTo(Convert.ToInt32(node2.ImageIndex));
    //
    //
    //        return result;
    //    }
    //}
}
