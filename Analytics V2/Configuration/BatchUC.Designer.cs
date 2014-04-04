namespace Analytics_V2
{
    partial class BatchUC
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
            this.ToolStripGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SplitContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.BatchNameLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.BatchNameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.BatchCreationHeaderGroup = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.CreateButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BackButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox.Panel)).BeginInit();
            this.ToolStripGroupBox.Panel.SuspendLayout();
            this.ToolStripGroupBox.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel1)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel2)).BeginInit();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BatchCreationHeaderGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BatchCreationHeaderGroup.Panel)).BeginInit();
            this.BatchCreationHeaderGroup.Panel.SuspendLayout();
            this.BatchCreationHeaderGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStripGroupBox
            // 
            this.ToolStripGroupBox.CaptionEdge = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.ToolStripGroupBox.CaptionVisible = false;
            this.ToolStripGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolStripGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ToolStripGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.ToolStripGroupBox.Name = "ToolStripGroupBox";
            // 
            // ToolStripGroupBox.Panel
            // 
            this.ToolStripGroupBox.Panel.Controls.Add(this.ToolStrip);
            this.ToolStripGroupBox.Size = new System.Drawing.Size(617, 23);
            this.ToolStripGroupBox.TabIndex = 0;
            // 
            // ToolStrip
            // 
            this.ToolStrip.AutoSize = false;
            this.ToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.ImageScalingSize = new System.Drawing.Size(19, 17);
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToolStripButton,
            this.RemoveToolStripButton});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip.Size = new System.Drawing.Size(613, 19);
            this.ToolStrip.TabIndex = 1;
            this.ToolStrip.Text = "toolStrip1";
            // 
            // AddToolStripButton
            // 
            this.AddToolStripButton.AutoSize = false;
            this.AddToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddToolStripButton.Image = global::Analytics_V2.Properties.Resources.Add;
            this.AddToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddToolStripButton.Name = "AddToolStripButton";
            this.AddToolStripButton.Size = new System.Drawing.Size(19, 17);
            this.AddToolStripButton.Text = "toolStripButton1";
            this.AddToolStripButton.ToolTipText = "Add Batch";
            this.AddToolStripButton.Click += new System.EventHandler(this.AddToolStripButton_Click);
            // 
            // RemoveToolStripButton
            // 
            this.RemoveToolStripButton.AutoSize = false;
            this.RemoveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveToolStripButton.Image = global::Analytics_V2.Properties.Resources.Remove;
            this.RemoveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveToolStripButton.Name = "RemoveToolStripButton";
            this.RemoveToolStripButton.Size = new System.Drawing.Size(19, 17);
            this.RemoveToolStripButton.Text = "toolStripButton2";
            this.RemoveToolStripButton.ToolTipText = "Delete Batch";
            // 
            // SplitContainer
            // 
            this.SplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.ToolStripGroupBox);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.BatchCreationHeaderGroup);
            this.SplitContainer.Panel2.Controls.Add(this.kryptonButton2);
            this.SplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.SplitContainer.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.SplitContainer.Size = new System.Drawing.Size(923, 373);
            this.SplitContainer.SplitterDistance = 617;
            this.SplitContainer.TabIndex = 1;
            // 
            // BatchNameLabel
            // 
            this.BatchNameLabel.Location = new System.Drawing.Point(7, 20);
            this.BatchNameLabel.Name = "BatchNameLabel";
            this.BatchNameLabel.Size = new System.Drawing.Size(83, 20);
            this.BatchNameLabel.TabIndex = 2;
            this.BatchNameLabel.Values.Text = "Batch Name :";
            // 
            // BatchNameTextBox
            // 
            this.BatchNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchNameTextBox.Location = new System.Drawing.Point(96, 20);
            this.BatchNameTextBox.Name = "BatchNameTextBox";
            this.BatchNameTextBox.Size = new System.Drawing.Size(177, 20);
            this.BatchNameTextBox.TabIndex = 1;
            // 
            // BatchCreationHeaderGroup
            // 
            this.BatchCreationHeaderGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BatchCreationHeaderGroup.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.BatchCreationHeaderGroup.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlGroupBox;
            this.BatchCreationHeaderGroup.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockActive;
            this.BatchCreationHeaderGroup.HeaderVisibleSecondary = false;
            this.BatchCreationHeaderGroup.Location = new System.Drawing.Point(5, 0);
            this.BatchCreationHeaderGroup.Name = "BatchCreationHeaderGroup";
            // 
            // BatchCreationHeaderGroup.Panel
            // 
            this.BatchCreationHeaderGroup.Panel.Controls.Add(this.CreateButton);
            this.BatchCreationHeaderGroup.Panel.Controls.Add(this.BatchNameTextBox);
            this.BatchCreationHeaderGroup.Panel.Controls.Add(this.BatchNameLabel);
            this.BatchCreationHeaderGroup.Panel.Controls.Add(this.BackButton);
            this.BatchCreationHeaderGroup.Size = new System.Drawing.Size(296, 373);
            this.BatchCreationHeaderGroup.TabIndex = 4;
            this.BatchCreationHeaderGroup.ValuesPrimary.Heading = "BATCH CREATION";
            this.BatchCreationHeaderGroup.ValuesPrimary.Image = null;
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateButton.Location = new System.Drawing.Point(60, 314);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(220, 27);
            this.CreateButton.TabIndex = 0;
            this.CreateButton.Values.Text = "CREATE ";
            // 
            // BackButton
            // 
            this.BackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BackButton.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
            this.BackButton.Location = new System.Drawing.Point(7, 314);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(47, 25);
            this.BackButton.TabIndex = 4;
            this.BackButton.Values.Image = global::Analytics_V2.Properties.Resources.Back;
            this.BackButton.Values.Text = "";
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
            this.kryptonButton2.Location = new System.Drawing.Point(54, 118);
            this.kryptonButton2.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(148, 107);
            this.kryptonButton2.TabIndex = 3;
            this.kryptonButton2.Values.Text = "";
            // 
            // BatchUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SplitContainer);
            this.Name = "BatchUC";
            this.Size = new System.Drawing.Size(923, 373);
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox.Panel)).EndInit();
            this.ToolStripGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox)).EndInit();
            this.ToolStripGroupBox.ResumeLayout(false);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel1)).EndInit();
            this.SplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel2)).EndInit();
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BatchCreationHeaderGroup.Panel)).EndInit();
            this.BatchCreationHeaderGroup.Panel.ResumeLayout(false);
            this.BatchCreationHeaderGroup.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BatchCreationHeaderGroup)).EndInit();
            this.BatchCreationHeaderGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox ToolStripGroupBox;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton AddToolStripButton;
        private System.Windows.Forms.ToolStripButton RemoveToolStripButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton CreateButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel BatchNameLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox BatchNameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BackButton;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup BatchCreationHeaderGroup;
        internal ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer;
    }
}
