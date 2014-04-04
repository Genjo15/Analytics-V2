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
            this.StopButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.ExpandMinimize = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Bar = new System.Windows.Forms.ProgressBar();
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
            this.SuspendLayout();
            // 
            // ProgressBarGroupBox
            // 
            this.ProgressBarGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgressBarGroupBox.Location = new System.Drawing.Point(5, 0);
            this.ProgressBarGroupBox.Name = "ProgressBarGroupBox";
            this.ProgressBarGroupBox.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // ProgressBarGroupBox.Panel
            // 
            this.ProgressBarGroupBox.Panel.Controls.Add(this.SplitContainer1);
            this.ProgressBarGroupBox.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.ProgressBarGroupBox.Size = new System.Drawing.Size(636, 251);
            this.ProgressBarGroupBox.StateCommon.Back.Color1 = System.Drawing.SystemColors.ControlLight;
            this.ProgressBarGroupBox.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
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
            this.SplitContainer1.Panel1.Controls.Add(this.StopButton);
            this.SplitContainer1.Panel1.Controls.Add(this.ExpandMinimize);
            this.SplitContainer1.Panel1.Controls.Add(this.Bar);
            this.SplitContainer1.Panel1MinSize = 13;
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ProcessSummaryRichTextBox);
            this.SplitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.SplitContainer1.Panel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.SplitContainer1.Size = new System.Drawing.Size(622, 217);
            this.SplitContainer1.SplitterDistance = 13;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 0;
            // 
            // StopButton
            // 
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.StopButton.Location = new System.Drawing.Point(592, 0);
            this.StopButton.Name = "StopButton";
            this.StopButton.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.StopButton.Size = new System.Drawing.Size(15, 13);
            this.StopButton.StateCommon.Back.Image = global::Analytics_V2.Properties.Resources.Stop;
            this.StopButton.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.StopButton.TabIndex = 2;
            this.StopButton.Values.Image = global::Analytics_V2.Properties.Resources.Stop;
            this.StopButton.Values.ImageTransparentColor = System.Drawing.SystemColors.ControlLight;
            this.StopButton.Values.Text = "";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ExpandMinimize
            // 
            this.ExpandMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExpandMinimize.Location = new System.Drawing.Point(607, 0);
            this.ExpandMinimize.Name = "ExpandMinimize";
            this.ExpandMinimize.Size = new System.Drawing.Size(15, 13);
            this.ExpandMinimize.StateCommon.Back.Image = global::Analytics_V2.Properties.Resources.Arrow_Down;
            this.ExpandMinimize.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.ExpandMinimize.TabIndex = 1;
            this.ExpandMinimize.Values.Text = "";
            this.ExpandMinimize.Click += new System.EventHandler(this.ExpandMinimizeButton_Click);
            // 
            // Bar
            // 
            this.Bar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Bar.Location = new System.Drawing.Point(0, 0);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(590, 13);
            this.Bar.Step = 1;
            this.Bar.TabIndex = 0;
            // 
            // ProcessSummaryRichTextBox
            // 
            this.ProcessSummaryRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessSummaryRichTextBox.Location = new System.Drawing.Point(1, 3);
            this.ProcessSummaryRichTextBox.Name = "ProcessSummaryRichTextBox";
            this.ProcessSummaryRichTextBox.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.ProcessSummaryRichTextBox.ReadOnly = true;
            this.ProcessSummaryRichTextBox.Size = new System.Drawing.Size(625, 190);
            this.ProcessSummaryRichTextBox.TabIndex = 0;
            this.ProcessSummaryRichTextBox.Text = "";
            // 
            // ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProgressBarGroupBox);
            this.Name = "ProgressBar";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
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
            this.ResumeLayout(false);

        }

        #endregion

        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox ProgressBarGroupBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer1;
        private System.Windows.Forms.ProgressBar Bar;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox ProcessSummaryRichTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton ExpandMinimize;
        private ComponentFactory.Krypton.Toolkit.KryptonButton StopButton;

    }
}
