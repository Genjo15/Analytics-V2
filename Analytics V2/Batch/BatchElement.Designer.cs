namespace Analytics_V2
{
    partial class BatchElement
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
            this.FTPComboBox = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.FtpLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.TargetPathButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SuppressButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Config = new System.Windows.Forms.Label();
            this.TargetPathLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ConfigSelectionLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox.Panel)).BeginInit();
            this.GroupBox.Panel.SuspendLayout();
            this.GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FTPComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox
            // 
            this.GroupBox.CaptionVisible = false;
            this.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox.Location = new System.Drawing.Point(5, 5);
            this.GroupBox.Name = "GroupBox";
            // 
            // GroupBox.Panel
            // 
            this.GroupBox.Panel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.GroupBox.Panel.Controls.Add(this.FTPComboBox);
            this.GroupBox.Panel.Controls.Add(this.FtpLabel);
            this.GroupBox.Panel.Controls.Add(this.TargetPathButton);
            this.GroupBox.Panel.Controls.Add(this.SuppressButton);
            this.GroupBox.Panel.Controls.Add(this.Config);
            this.GroupBox.Panel.Controls.Add(this.TargetPathLabel);
            this.GroupBox.Panel.Controls.Add(this.ConfigSelectionLabel);
            this.GroupBox.Size = new System.Drawing.Size(346, 102);
            this.GroupBox.TabIndex = 0;
            // 
            // FTPComboBox
            // 
            this.FTPComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FTPComboBox.DropDownWidth = 158;
            this.FTPComboBox.Location = new System.Drawing.Point(92, 11);
            this.FTPComboBox.Name = "FTPComboBox";
            this.FTPComboBox.Size = new System.Drawing.Size(200, 21);
            this.FTPComboBox.TabIndex = 7;
            this.FTPComboBox.SelectedValueChanged += new System.EventHandler(this.FTPComboBox_SelectedValueChanged);
            this.FTPComboBox.Enter += new System.EventHandler(this.FTPComboBox_Enter);
            // 
            // FtpLabel
            // 
            this.FtpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FtpLabel.Location = new System.Drawing.Point(48, 12);
            this.FtpLabel.Name = "FtpLabel";
            this.FtpLabel.Size = new System.Drawing.Size(36, 20);
            this.FtpLabel.TabIndex = 6;
            this.FtpLabel.Values.Text = "FTP :";
            // 
            // TargetPathButton
            // 
            this.TargetPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetPathButton.Location = new System.Drawing.Point(92, 66);
            this.TargetPathButton.Name = "TargetPathButton";
            this.TargetPathButton.Size = new System.Drawing.Size(238, 25);
            this.TargetPathButton.TabIndex = 5;
            this.TargetPathButton.Values.Text = "Path";
            this.TargetPathButton.Click += new System.EventHandler(this.TargetPathButton_Click);
            // 
            // SuppressButton
            // 
            this.SuppressButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SuppressButton.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
            this.SuppressButton.Location = new System.Drawing.Point(314, 2);
            this.SuppressButton.Name = "SuppressButton";
            this.SuppressButton.Size = new System.Drawing.Size(26, 26);
            this.SuppressButton.TabIndex = 4;
            this.SuppressButton.Values.Image = global::Analytics_V2.Properties.Resources.Delete;
            this.SuppressButton.Values.Text = "";
            // 
            // Config
            // 
            this.Config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Config.AutoSize = true;
            this.Config.BackColor = System.Drawing.Color.Transparent;
            this.Config.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Config.Location = new System.Drawing.Point(90, 40);
            this.Config.Name = "Config";
            this.Config.Size = new System.Drawing.Size(46, 18);
            this.Config.TabIndex = 3;
            this.Config.Text = "label1";
            // 
            // TargetPathLabel
            // 
            this.TargetPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetPathLabel.Location = new System.Drawing.Point(7, 68);
            this.TargetPathLabel.Name = "TargetPathLabel";
            this.TargetPathLabel.Size = new System.Drawing.Size(79, 20);
            this.TargetPathLabel.TabIndex = 2;
            this.TargetPathLabel.Values.Text = "Target Path :";
            // 
            // ConfigSelectionLabel
            // 
            this.ConfigSelectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigSelectionLabel.Location = new System.Drawing.Point(32, 40);
            this.ConfigSelectionLabel.Name = "ConfigSelectionLabel";
            this.ConfigSelectionLabel.Size = new System.Drawing.Size(53, 20);
            this.ConfigSelectionLabel.TabIndex = 1;
            this.ConfigSelectionLabel.Values.Text = "Config :";
            // 
            // BatchElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.GroupBox);
            this.Name = "BatchElement";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(356, 112);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox.Panel)).EndInit();
            this.GroupBox.Panel.ResumeLayout(false);
            this.GroupBox.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox)).EndInit();
            this.GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FTPComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox GroupBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel ConfigSelectionLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel TargetPathLabel;
        internal System.Windows.Forms.Label Config;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton SuppressButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel FtpLabel;
        internal ComponentFactory.Krypton.Toolkit.KryptonComboBox FTPComboBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton TargetPathButton;
    }
}
