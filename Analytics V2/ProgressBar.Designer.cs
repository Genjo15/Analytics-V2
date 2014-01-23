namespace Analytics_V2
{
    partial class ProgressBar
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
            this.ProgressBarGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.SplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.SplitContainer2 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.Bar = new System.Windows.Forms.ProgressBar();
            this.ExpandMinimizeButton = new System.Windows.Forms.Button();
            this.ProcessSummaryRichTextBox = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarGroupBox.Panel)).BeginInit();
            this.ProgressBarGroupBox.Panel.SuspendLayout();
            this.ProgressBarGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel2)).BeginInit();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel1)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel2)).BeginInit();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBarGroupBox
            // 
            this.ProgressBarGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgressBarGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ProgressBarGroupBox.Name = "ProgressBarGroupBox";
            this.ProgressBarGroupBox.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // ProgressBarGroupBox.Panel
            // 
            this.ProgressBarGroupBox.Panel.Controls.Add(this.SplitContainer1);
            this.ProgressBarGroupBox.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.ProgressBarGroupBox.Size = new System.Drawing.Size(646, 251);
            this.ProgressBarGroupBox.TabIndex = 0;
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer1.Location = new System.Drawing.Point(5, 5);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.SplitContainer1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.SplitContainer2);
            this.SplitContainer1.Panel1MinSize = 0;
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ProcessSummaryRichTextBox);
            this.SplitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.SplitContainer1.Panel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.SplitContainer1.Size = new System.Drawing.Size(632, 217);
            this.SplitContainer1.SplitterDistance = 20;
            this.SplitContainer1.SplitterWidth = 0;
            this.SplitContainer1.TabIndex = 0;
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.Bar);
            this.SplitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.SplitContainer2.Panel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.ExpandMinimizeButton);
            this.SplitContainer2.Panel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.SplitContainer2.Panel2MinSize = 20;
            this.SplitContainer2.Size = new System.Drawing.Size(632, 20);
            this.SplitContainer2.SplitterDistance = 612;
            this.SplitContainer2.SplitterWidth = 0;
            this.SplitContainer2.TabIndex = 0;
            // 
            // Bar
            // 
            this.Bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Bar.Location = new System.Drawing.Point(0, 0);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(609, 20);
            this.Bar.Step = 1;
            this.Bar.TabIndex = 0;
            // 
            // ExpandMinimizeButton
            // 
            this.ExpandMinimizeButton.BackColor = System.Drawing.SystemColors.Control;
            this.ExpandMinimizeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExpandMinimizeButton.Image = global::Analytics_V2.Properties.Resources.ArrowDown;
            this.ExpandMinimizeButton.Location = new System.Drawing.Point(0, 0);
            this.ExpandMinimizeButton.Name = "ExpandMinimizeButton";
            this.ExpandMinimizeButton.Size = new System.Drawing.Size(20, 20);
            this.ExpandMinimizeButton.TabIndex = 0;
            this.ExpandMinimizeButton.UseVisualStyleBackColor = false;
            this.ExpandMinimizeButton.Click += new System.EventHandler(this.ExpandMinimizeButton_Click);
            // 
            // ProcessSummaryRichTextBox
            // 
            this.ProcessSummaryRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessSummaryRichTextBox.Location = new System.Drawing.Point(1, 3);
            this.ProcessSummaryRichTextBox.Name = "ProcessSummaryRichTextBox";
            this.ProcessSummaryRichTextBox.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.ProcessSummaryRichTextBox.ReadOnly = true;
            this.ProcessSummaryRichTextBox.Size = new System.Drawing.Size(630, 193);
            this.ProcessSummaryRichTextBox.TabIndex = 0;
            this.ProcessSummaryRichTextBox.Text = "";
            // 
            // ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProgressBarGroupBox);
            this.Name = "ProgressBar";
            this.Size = new System.Drawing.Size(646, 251);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarGroupBox.Panel)).EndInit();
            this.ProgressBarGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarGroupBox)).EndInit();
            this.ProgressBarGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel1)).EndInit();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1.Panel2)).EndInit();
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel1)).EndInit();
            this.SplitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel2)).EndInit();
            this.SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox ProgressBarGroupBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer2;
        private System.Windows.Forms.ProgressBar Bar;
        private System.Windows.Forms.Button ExpandMinimizeButton;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox ProcessSummaryRichTextBox;

    }
}
