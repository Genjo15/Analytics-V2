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
            this.ConsistencyCheckingButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.ConsistencyCheckingTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.ConsistencyPathLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.XMLTemplateButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.XMLTemplateTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.XMLTemplateLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.HCButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.HCTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.HCLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.PersonnalPathButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.PersonnalPathTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.PersonnalPathLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DefaultEditionModeLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.CreationRadioButton = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.XMLRadioButton = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
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
            this.GroupBox.Panel.Controls.Add(this.XMLRadioButton);
            this.GroupBox.Panel.Controls.Add(this.CreationRadioButton);
            this.GroupBox.Panel.Controls.Add(this.DefaultEditionModeLabel);
            this.GroupBox.Panel.Controls.Add(this.ConsistencyCheckingButton);
            this.GroupBox.Panel.Controls.Add(this.ConsistencyCheckingTextBox);
            this.GroupBox.Panel.Controls.Add(this.ConsistencyPathLabel);
            this.GroupBox.Panel.Controls.Add(this.XMLTemplateButton);
            this.GroupBox.Panel.Controls.Add(this.XMLTemplateTextBox);
            this.GroupBox.Panel.Controls.Add(this.XMLTemplateLabel);
            this.GroupBox.Panel.Controls.Add(this.HCButton);
            this.GroupBox.Panel.Controls.Add(this.HCTextBox);
            this.GroupBox.Panel.Controls.Add(this.HCLabel);
            this.GroupBox.Panel.Controls.Add(this.PersonnalPathButton);
            this.GroupBox.Panel.Controls.Add(this.PersonnalPathTextBox);
            this.GroupBox.Panel.Controls.Add(this.PersonnalPathLabel);
            this.GroupBox.Size = new System.Drawing.Size(633, 329);
            this.GroupBox.TabIndex = 0;
            // 
            // ConsistencyCheckingButton
            // 
            this.ConsistencyCheckingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsistencyCheckingButton.Location = new System.Drawing.Point(579, 240);
            this.ConsistencyCheckingButton.Name = "ConsistencyCheckingButton";
            this.ConsistencyCheckingButton.Size = new System.Drawing.Size(24, 27);
            this.ConsistencyCheckingButton.TabIndex = 11;
            this.ConsistencyCheckingButton.Values.Image = global::Analytics_V2.Properties.Resources.Edit;
            this.ConsistencyCheckingButton.Values.Text = "";
            this.ConsistencyCheckingButton.Click += new System.EventHandler(this.ConsistencyCheckingButton_Click);
            // 
            // ConsistencyCheckingTextBox
            // 
            this.ConsistencyCheckingTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsistencyCheckingTextBox.Location = new System.Drawing.Point(24, 244);
            this.ConsistencyCheckingTextBox.Name = "ConsistencyCheckingTextBox";
            this.ConsistencyCheckingTextBox.ReadOnly = true;
            this.ConsistencyCheckingTextBox.Size = new System.Drawing.Size(535, 20);
            this.ConsistencyCheckingTextBox.TabIndex = 10;
            this.ConsistencyCheckingTextBox.Text = "kryptonTextBox1";
            // 
            // ConsistencyPathLabel
            // 
            this.ConsistencyPathLabel.Location = new System.Drawing.Point(23, 218);
            this.ConsistencyPathLabel.Name = "ConsistencyPathLabel";
            this.ConsistencyPathLabel.Size = new System.Drawing.Size(173, 20);
            this.ConsistencyPathLabel.TabIndex = 9;
            this.ConsistencyPathLabel.Values.Text = "Consistency Checking Folder :";
            // 
            // XMLTemplateButton
            // 
            this.XMLTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.XMLTemplateButton.Location = new System.Drawing.Point(579, 175);
            this.XMLTemplateButton.Name = "XMLTemplateButton";
            this.XMLTemplateButton.Size = new System.Drawing.Size(24, 27);
            this.XMLTemplateButton.TabIndex = 8;
            this.XMLTemplateButton.Values.Image = global::Analytics_V2.Properties.Resources.Edit;
            this.XMLTemplateButton.Values.Text = "";
            this.XMLTemplateButton.Click += new System.EventHandler(this.XMLTemplateButton_Click);
            // 
            // XMLTemplateTextBox
            // 
            this.XMLTemplateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.XMLTemplateTextBox.Location = new System.Drawing.Point(24, 179);
            this.XMLTemplateTextBox.Name = "XMLTemplateTextBox";
            this.XMLTemplateTextBox.ReadOnly = true;
            this.XMLTemplateTextBox.Size = new System.Drawing.Size(535, 20);
            this.XMLTemplateTextBox.TabIndex = 7;
            this.XMLTemplateTextBox.Text = "kryptonTextBox1";
            // 
            // XMLTemplateLabel
            // 
            this.XMLTemplateLabel.Location = new System.Drawing.Point(23, 153);
            this.XMLTemplateLabel.Name = "XMLTemplateLabel";
            this.XMLTemplateLabel.Size = new System.Drawing.Size(282, 20);
            this.XMLTemplateLabel.TabIndex = 6;
            this.XMLTemplateLabel.Values.Text = "XML Interpretation Template (for creation mode) :";
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
            this.HCTextBox.Location = new System.Drawing.Point(24, 111);
            this.HCTextBox.Name = "HCTextBox";
            this.HCTextBox.ReadOnly = true;
            this.HCTextBox.Size = new System.Drawing.Size(535, 20);
            this.HCTextBox.TabIndex = 4;
            this.HCTextBox.Text = "kryptonTextBox1";
            // 
            // HCLabel
            // 
            this.HCLabel.Location = new System.Drawing.Point(23, 85);
            this.HCLabel.Name = "HCLabel";
            this.HCLabel.Size = new System.Drawing.Size(152, 20);
            this.HCLabel.TabIndex = 3;
            this.HCLabel.Values.Text = "Configuration File for HC :";
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
            this.PersonnalPathTextBox.Location = new System.Drawing.Point(24, 44);
            this.PersonnalPathTextBox.Name = "PersonnalPathTextBox";
            this.PersonnalPathTextBox.ReadOnly = true;
            this.PersonnalPathTextBox.Size = new System.Drawing.Size(535, 20);
            this.PersonnalPathTextBox.TabIndex = 1;
            this.PersonnalPathTextBox.Text = "kryptonTextBox1";
            // 
            // PersonnalPathLabel
            // 
            this.PersonnalPathLabel.Location = new System.Drawing.Point(23, 18);
            this.PersonnalPathLabel.Name = "PersonnalPathLabel";
            this.PersonnalPathLabel.Size = new System.Drawing.Size(108, 20);
            this.PersonnalPathLabel.TabIndex = 0;
            this.PersonnalPathLabel.Values.Text = "Personnal Folder :";
            // 
            // DefaultEditionModeLabel
            // 
            this.DefaultEditionModeLabel.Location = new System.Drawing.Point(24, 281);
            this.DefaultEditionModeLabel.Name = "DefaultEditionModeLabel";
            this.DefaultEditionModeLabel.Size = new System.Drawing.Size(149, 20);
            this.DefaultEditionModeLabel.TabIndex = 12;
            this.DefaultEditionModeLabel.Values.Text = "Edition Mode By Default :";
            // 
            // CreationRadioButton
            // 
            this.CreationRadioButton.Location = new System.Drawing.Point(210, 281);
            this.CreationRadioButton.Name = "CreationRadioButton";
            this.CreationRadioButton.Size = new System.Drawing.Size(104, 20);
            this.CreationRadioButton.TabIndex = 13;
            this.CreationRadioButton.Values.Text = "Creation Mode";
            this.CreationRadioButton.Click += new System.EventHandler(this.CreationRadioButton_Click);
            // 
            // XMLRadioButton
            // 
            this.XMLRadioButton.Location = new System.Drawing.Point(350, 281);
            this.XMLRadioButton.Name = "XMLRadioButton";
            this.XMLRadioButton.Size = new System.Drawing.Size(82, 20);
            this.XMLRadioButton.TabIndex = 14;
            this.XMLRadioButton.Values.Text = "XML Mode";
            this.XMLRadioButton.Click += new System.EventHandler(this.XMLRadioButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBox);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(633, 329);
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
        private ComponentFactory.Krypton.Toolkit.KryptonButton XMLTemplateButton;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox XMLTemplateTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel XMLTemplateLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton ConsistencyCheckingButton;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox ConsistencyCheckingTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel ConsistencyPathLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton XMLRadioButton;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton CreationRadioButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel DefaultEditionModeLabel;
    }
}
