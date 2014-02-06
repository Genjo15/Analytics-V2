namespace Analytics_V2
{
    partial class SpecificCountries
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
            this.SpecificCountriesGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.SpecificCountriesListBox = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SpecificCountriesGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpecificCountriesGroupBox.Panel)).BeginInit();
            this.SpecificCountriesGroupBox.Panel.SuspendLayout();
            this.SpecificCountriesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SpecificCountriesGroupBox
            // 
            this.SpecificCountriesGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.SpecificCountriesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpecificCountriesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.SpecificCountriesGroupBox.Name = "SpecificCountriesGroupBox";
            // 
            // SpecificCountriesGroupBox.Panel
            // 
            this.SpecificCountriesGroupBox.Panel.Controls.Add(this.SpecificCountriesListBox);
            this.SpecificCountriesGroupBox.Size = new System.Drawing.Size(276, 344);
            this.SpecificCountriesGroupBox.TabIndex = 0;
            this.SpecificCountriesGroupBox.Values.Heading = "Specific Countries";
            // 
            // SpecificCountriesListBox
            // 
            this.SpecificCountriesListBox.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlGroupBox;
            this.SpecificCountriesListBox.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonNavigatorStack;
            this.SpecificCountriesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpecificCountriesListBox.Location = new System.Drawing.Point(0, 0);
            this.SpecificCountriesListBox.Name = "SpecificCountriesListBox";
            this.SpecificCountriesListBox.Size = new System.Drawing.Size(272, 313);
            this.SpecificCountriesListBox.TabIndex = 0;
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2010Silver;
            // 
            // SpecificCountries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SpecificCountriesGroupBox);
            this.Name = "SpecificCountries";
            this.Size = new System.Drawing.Size(276, 344);
            ((System.ComponentModel.ISupportInitialize)(this.SpecificCountriesGroupBox.Panel)).EndInit();
            this.SpecificCountriesGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpecificCountriesGroupBox)).EndInit();
            this.SpecificCountriesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox SpecificCountriesGroupBox;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        internal ComponentFactory.Krypton.Toolkit.KryptonListBox SpecificCountriesListBox;

    }
}
