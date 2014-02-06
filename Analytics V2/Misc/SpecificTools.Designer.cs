namespace Analytics_V2
{
    partial class SpecificTools
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
            this.SpecificToolsGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.SpecificToolsListBox = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            ((System.ComponentModel.ISupportInitialize)(this.SpecificToolsGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpecificToolsGroupBox.Panel)).BeginInit();
            this.SpecificToolsGroupBox.Panel.SuspendLayout();
            this.SpecificToolsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SpecificToolsGroupBox
            // 
            this.SpecificToolsGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.SpecificToolsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpecificToolsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.SpecificToolsGroupBox.Name = "SpecificToolsGroupBox";
            // 
            // SpecificToolsGroupBox.Panel
            // 
            this.SpecificToolsGroupBox.Panel.Controls.Add(this.SpecificToolsListBox);
            this.SpecificToolsGroupBox.Size = new System.Drawing.Size(251, 247);
            this.SpecificToolsGroupBox.TabIndex = 0;
            this.SpecificToolsGroupBox.Values.Heading = "Specific Tools";
            // 
            // SpecificToolsListBox
            // 
            this.SpecificToolsListBox.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlGroupBox;
            this.SpecificToolsListBox.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonNavigatorStack;
            this.SpecificToolsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpecificToolsListBox.Location = new System.Drawing.Point(0, 0);
            this.SpecificToolsListBox.Name = "SpecificToolsListBox";
            this.SpecificToolsListBox.Size = new System.Drawing.Size(247, 216);
            this.SpecificToolsListBox.TabIndex = 0;
            // 
            // SpecificTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SpecificToolsGroupBox);
            this.Name = "SpecificTools";
            this.Size = new System.Drawing.Size(251, 247);
            ((System.ComponentModel.ISupportInitialize)(this.SpecificToolsGroupBox.Panel)).EndInit();
            this.SpecificToolsGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpecificToolsGroupBox)).EndInit();
            this.SpecificToolsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox SpecificToolsGroupBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonListBox SpecificToolsListBox;

    }
}
