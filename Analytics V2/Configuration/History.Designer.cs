namespace Analytics_V2
{
    partial class History
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PathAtModifLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BlankLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LinesDeletedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LinesAddedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.SplitContainer2 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.BeforeGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.BeforeRichTextBox = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.AfterGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.AfterRichTextBox = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel2)).BeginInit();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel1)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel2)).BeginInit();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeforeGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeforeGroupBox.Panel)).BeginInit();
            this.BeforeGroupBox.Panel.SuspendLayout();
            this.BeforeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AfterGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AfterGroupBox.Panel)).BeginInit();
            this.AfterGroupBox.Panel.SuspendLayout();
            this.AfterGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PathAtModifLabel,
            this.BlankLabel,
            this.LinesDeletedLabel,
            this.LinesAddedLabel});
            this.statusStrip1.Location = new System.Drawing.Point(3, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(642, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PathAtModifLabel
            // 
            this.PathAtModifLabel.Name = "PathAtModifLabel";
            this.PathAtModifLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // BlankLabel
            // 
            this.BlankLabel.Name = "BlankLabel";
            this.BlankLabel.Size = new System.Drawing.Size(627, 17);
            this.BlankLabel.Spring = true;
            // 
            // LinesDeletedLabel
            // 
            this.LinesDeletedLabel.Name = "LinesDeletedLabel";
            this.LinesDeletedLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // LinesAddedLabel
            // 
            this.LinesAddedLabel.Name = "LinesAddedLabel";
            this.LinesAddedLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.DataGridView);
            this.SplitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
            this.SplitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.SplitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.SplitContainer1.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.SplitContainer1.Size = new System.Drawing.Size(1030, 554);
            this.SplitContainer1.SplitterDistance = 382;
            this.SplitContainer1.SplitterWidth = 3;
            this.SplitContainer1.TabIndex = 1;
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.DataGridView.Location = new System.Drawing.Point(3, 3);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(376, 548);
            this.DataGridView.TabIndex = 1;
            this.DataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellClick);
            this.DataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridView_DataBindingComplete);
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.Location = new System.Drawing.Point(3, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.BeforeGroupBox);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.AfterGroupBox);
            this.SplitContainer2.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.SplitContainer2.Size = new System.Drawing.Size(642, 532);
            this.SplitContainer2.SplitterDistance = 318;
            this.SplitContainer2.SplitterWidth = 3;
            this.SplitContainer2.TabIndex = 1;
            // 
            // BeforeGroupBox
            // 
            this.BeforeGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.BeforeGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BeforeGroupBox.Location = new System.Drawing.Point(0, 0);
            this.BeforeGroupBox.Name = "BeforeGroupBox";
            // 
            // BeforeGroupBox.Panel
            // 
            this.BeforeGroupBox.Panel.Controls.Add(this.BeforeRichTextBox);
            this.BeforeGroupBox.Size = new System.Drawing.Size(318, 532);
            this.BeforeGroupBox.TabIndex = 0;
            this.BeforeGroupBox.Values.Heading = "Before";
            // 
            // BeforeRichTextBox
            // 
            this.BeforeRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BeforeRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.BeforeRichTextBox.Name = "BeforeRichTextBox";
            this.BeforeRichTextBox.Size = new System.Drawing.Size(314, 501);
            this.BeforeRichTextBox.TabIndex = 0;
            this.BeforeRichTextBox.Text = "kryptonRichTextBox1";
            // 
            // AfterGroupBox
            // 
            this.AfterGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.AfterGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AfterGroupBox.Location = new System.Drawing.Point(0, 0);
            this.AfterGroupBox.Name = "AfterGroupBox";
            // 
            // AfterGroupBox.Panel
            // 
            this.AfterGroupBox.Panel.Controls.Add(this.AfterRichTextBox);
            this.AfterGroupBox.Size = new System.Drawing.Size(321, 532);
            this.AfterGroupBox.TabIndex = 0;
            this.AfterGroupBox.Values.Heading = "After";
            // 
            // AfterRichTextBox
            // 
            this.AfterRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AfterRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.AfterRichTextBox.Name = "AfterRichTextBox";
            this.AfterRichTextBox.Size = new System.Drawing.Size(317, 501);
            this.AfterRichTextBox.TabIndex = 0;
            this.AfterRichTextBox.Text = "kryptonRichTextBox1";
            // 
            // Chronicles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer1);
            this.Name = "Chronicles";
            this.Size = new System.Drawing.Size(1030, 554);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel1)).EndInit();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel2)).EndInit();
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel1)).EndInit();
            this.SplitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel2)).EndInit();
            this.SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BeforeGroupBox.Panel)).EndInit();
            this.BeforeGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BeforeGroupBox)).EndInit();
            this.BeforeGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AfterGroupBox.Panel)).EndInit();
            this.AfterGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AfterGroupBox)).EndInit();
            this.AfterGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel PathAtModifLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer2;
        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox BeforeGroupBox;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox BeforeRichTextBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox AfterGroupBox;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox AfterRichTextBox;
        private System.Windows.Forms.ToolStripStatusLabel BlankLabel;
        private System.Windows.Forms.ToolStripStatusLabel LinesDeletedLabel;
        private System.Windows.Forms.ToolStripStatusLabel LinesAddedLabel;
        private System.Windows.Forms.DataGridView DataGridView;
    }
}
