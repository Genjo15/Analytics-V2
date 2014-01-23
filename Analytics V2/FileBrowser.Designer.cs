namespace Analytics_V2
{
    partial class FileBrowser
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileBrowser));
            this.BrowserGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FullCollapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FullExpandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.BrowserHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            ((System.ComponentModel.ISupportInitialize)(this.BrowserGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrowserGroupBox.Panel)).BeginInit();
            this.BrowserGroupBox.Panel.SuspendLayout();
            this.BrowserGroupBox.SuspendLayout();
            this.ContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowserGroupBox
            // 
            this.BrowserGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.BrowserGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrowserGroupBox.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.BrowserGroupBox.Location = new System.Drawing.Point(0, 36);
            this.BrowserGroupBox.Name = "BrowserGroupBox";
            // 
            // BrowserGroupBox.Panel
            // 
            this.BrowserGroupBox.Panel.Controls.Add(this.TreeView);
            this.BrowserGroupBox.Size = new System.Drawing.Size(304, 272);
            this.BrowserGroupBox.TabIndex = 0;
            this.BrowserGroupBox.Values.Heading = "File Browser";
            // 
            // TreeView
            // 
            this.TreeView.AllowDrop = true;
            this.TreeView.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView.ContextMenuStrip = this.ContextMenuStrip;
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView.ImageIndex = 0;
            this.TreeView.ImageList = this.ImageList;
            this.TreeView.Location = new System.Drawing.Point(0, 0);
            this.TreeView.Name = "TreeView";
            this.TreeView.SelectedImageIndex = 0;
            this.TreeView.Size = new System.Drawing.Size(300, 250);
            this.TreeView.TabIndex = 0;
            // 
            // ContextMenuStrip
            // 
            this.ContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolStripMenuItem,
            this.ToolStripSeparator1,
            this.FullCollapseToolStripMenuItem,
            this.FullExpandToolStripMenuItem,
            this.toolStripSeparator3,
            this.CutToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.ToolStripSeparator2,
            this.DeleteToolStripMenuItem,
            this.RenameToolStripMenuItem});
            this.ContextMenuStrip.Name = "contextMenuStrip1";
            this.ContextMenuStrip.Size = new System.Drawing.Size(142, 198);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // FullCollapseToolStripMenuItem
            // 
            this.FullCollapseToolStripMenuItem.Name = "FullCollapseToolStripMenuItem";
            this.FullCollapseToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.FullCollapseToolStripMenuItem.Text = "Full Collapse";
            // 
            // FullExpandToolStripMenuItem
            // 
            this.FullExpandToolStripMenuItem.Name = "FullExpandToolStripMenuItem";
            this.FullExpandToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.FullExpandToolStripMenuItem.Text = "Full Expand";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(138, 6);
            // 
            // CutToolStripMenuItem
            // 
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.CutToolStripMenuItem.Text = "Cut";
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.CopyToolStripMenuItem.Text = "Copy";
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Enabled = false;
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.PasteToolStripMenuItem.Text = "Paste";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(138, 6);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.DeleteToolStripMenuItem.Text = "Delete";
            // 
            // RenameToolStripMenuItem
            // 
            this.RenameToolStripMenuItem.Enabled = false;
            this.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem";
            this.RenameToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.RenameToolStripMenuItem.Text = "Rename";
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "Root.ico");
            this.ImageList.Images.SetKeyName(1, "Open Folder.ico");
            this.ImageList.Images.SetKeyName(2, "File.ico");
            // 
            // BrowserHeader
            // 
            this.BrowserHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.BrowserHeader.Location = new System.Drawing.Point(0, 0);
            this.BrowserHeader.Name = "BrowserHeader";
            this.BrowserHeader.Size = new System.Drawing.Size(304, 36);
            this.BrowserHeader.TabIndex = 0;
            this.BrowserHeader.Values.Description = "";
            this.BrowserHeader.Values.Heading = "Data Process";
            this.BrowserHeader.Values.Image = global::Analytics_V2.Properties.Resources.Logo;
            // 
            // FileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.BrowserGroupBox);
            this.Controls.Add(this.BrowserHeader);
            this.Name = "FileBrowser";
            this.Size = new System.Drawing.Size(304, 308);
            ((System.ComponentModel.ISupportInitialize)(this.BrowserGroupBox.Panel)).EndInit();
            this.BrowserGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrowserGroupBox)).EndInit();
            this.BrowserGroupBox.ResumeLayout(false);
            this.ContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox BrowserGroupBox;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader BrowserHeader;
        private System.Windows.Forms.ImageList ImageList;
        internal System.Windows.Forms.TreeView TreeView;
        internal System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem;
        new internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
        internal System.Windows.Forms.ToolStripMenuItem RenameToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem FullCollapseToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FullExpandToolStripMenuItem;
    }
}
