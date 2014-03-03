namespace Analytics_V2
{
    partial class Settings
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
            this.GroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.PersonnalPathButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.PersonnalPathTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.PersonnalPathLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.HCButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.HCTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.HCLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox.Panel)).BeginInit();
            this.GroupBox.Panel.SuspendLayout();
            this.GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox
            // 
            this.GroupBox.CaptionVisible = false;
            this.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox.Location = new System.Drawing.Point(0, 0);
            this.GroupBox.Name = "GroupBox";
            // 
            // GroupBox.Panel
            // 
            this.GroupBox.Panel.Controls.Add(this.HCButton);
            this.GroupBox.Panel.Controls.Add(this.HCTextBox);
            this.GroupBox.Panel.Controls.Add(this.HCLabel);
            this.GroupBox.Panel.Controls.Add(this.PersonnalPathButton);
            this.GroupBox.Panel.Controls.Add(this.PersonnalPathTextBox);
            this.GroupBox.Panel.Controls.Add(this.PersonnalPathLabel);
            this.GroupBox.Size = new System.Drawing.Size(633, 321);
            this.GroupBox.TabIndex = 0;
            // 
            // PersonnalPathButton
            // 
            this.PersonnalPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PersonnalPathButton.Location = new System.Drawing.Point(577, 40);
            this.PersonnalPathButton.Name = "PersonnalPathButton";
            this.PersonnalPathButton.Size = new System.Drawing.Size(24, 27);
            this.PersonnalPathButton.TabIndex = 2;
            this.PersonnalPathButton.Values.Image = global::Analytics_V2.Properties.Resources.Edit;
            this.PersonnalPathButton.Values.Text = "";
            this.PersonnalPathButton.Click += new System.EventHandler(this.PersonnalPathButton_Click);
            // 
            // PersonnalPathTextBox
            // 
            this.PersonnalPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PersonnalPathTextBox.Location = new System.Drawing.Point(23, 44);
            this.PersonnalPathTextBox.Name = "PersonnalPathTextBox";
            this.PersonnalPathTextBox.Size = new System.Drawing.Size(535, 20);
            this.PersonnalPathTextBox.TabIndex = 1;
            this.PersonnalPathTextBox.Text = "kryptonTextBox1";
            // 
            // PersonnalPathLabel
            // 
            this.PersonnalPathLabel.Location = new System.Drawing.Point(23, 18);
            this.PersonnalPathLabel.Name = "PersonnalPathLabel";
            this.PersonnalPathLabel.Size = new System.Drawing.Size(114, 20);
            this.PersonnalPathLabel.TabIndex = 0;
            this.PersonnalPathLabel.Values.Text = "Dossier Personnel :";
            // 
            // HCButton
            // 
            this.HCButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HCButton.Location = new System.Drawing.Point(577, 107);
            this.HCButton.Name = "HCButton";
            this.HCButton.Size = new System.Drawing.Size(24, 27);
            this.HCButton.TabIndex = 5;
            this.HCButton.Values.Image = global::Analytics_V2.Properties.Resources.Edit;
            this.HCButton.Values.Text = "";
            this.HCButton.Click += new System.EventHandler(this.HCButton_Click);
            // 
            // HCTextBox
            // 
            this.HCTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HCTextBox.Location = new System.Drawing.Point(23, 111);
            this.HCTextBox.Name = "HCTextBox";
            this.HCTextBox.Size = new System.Drawing.Size(535, 20);
            this.HCTextBox.TabIndex = 4;
            this.HCTextBox.Text = "kryptonTextBox1";
            // 
            // HCLabel
            // 
            this.HCLabel.Location = new System.Drawing.Point(25, 85);
            this.HCLabel.Name = "HCLabel";
            this.HCLabel.Size = new System.Drawing.Size(259, 20);
            this.HCLabel.TabIndex = 3;
            this.HCLabel.Values.Text = "Fichier de Configuration Header Consistency :";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBox);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(633, 321);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox.Panel)).EndInit();
            this.GroupBox.Panel.ResumeLayout(false);
            this.GroupBox.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox)).EndInit();
            this.GroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox GroupBox;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox PersonnalPathTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel PersonnalPathLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton PersonnalPathButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton HCButton;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox HCTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel HCLabel;
    }
}
