namespace Analytics_V2
{
    partial class BatchElementMulti
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
            this.TutoLabel = new System.Windows.Forms.Label();
            this.Config = new System.Windows.Forms.Label();
            this.ConfigSelectionLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.TargetPathButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.TargetPathLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.SuppressButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
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
            this.GroupBox.Location = new System.Drawing.Point(5, 5);
            this.GroupBox.Name = "GroupBox";
            // 
            // GroupBox.Panel
            // 
            this.GroupBox.Panel.AllowDrop = true;
            this.GroupBox.Panel.Controls.Add(this.TutoLabel);
            this.GroupBox.Panel.Controls.Add(this.Config);
            this.GroupBox.Panel.Controls.Add(this.ConfigSelectionLabel);
            this.GroupBox.Panel.Controls.Add(this.TargetPathButton);
            this.GroupBox.Panel.Controls.Add(this.TargetPathLabel);
            this.GroupBox.Panel.Controls.Add(this.SuppressButton);
            this.GroupBox.Panel.DragDrop += new System.Windows.Forms.DragEventHandler(this.GroupBox_Panel_DragDrop);
            this.GroupBox.Panel.DragOver += new System.Windows.Forms.DragEventHandler(this.GroupBox_Panel_DragOver);
            this.GroupBox.Size = new System.Drawing.Size(198, 125);
            this.GroupBox.TabIndex = 0;
            // 
            // TutoLabel
            // 
            this.TutoLabel.AutoSize = true;
            this.TutoLabel.BackColor = System.Drawing.Color.Transparent;
            this.TutoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TutoLabel.Location = new System.Drawing.Point(45, 94);
            this.TutoLabel.Name = "TutoLabel";
            this.TutoLabel.Size = new System.Drawing.Size(120, 13);
            this.TutoLabel.TabIndex = 10;
            this.TutoLabel.Text = "Drag configs here...";
            // 
            // Config
            // 
            this.Config.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Config.AutoSize = true;
            this.Config.BackColor = System.Drawing.Color.Transparent;
            this.Config.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Config.Location = new System.Drawing.Point(85, 65);
            this.Config.Name = "Config";
            this.Config.Size = new System.Drawing.Size(46, 18);
            this.Config.TabIndex = 9;
            this.Config.Text = "label1";
            // 
            // ConfigSelectionLabel
            // 
            this.ConfigSelectionLabel.Location = new System.Drawing.Point(25, 65);
            this.ConfigSelectionLabel.Name = "ConfigSelectionLabel";
            this.ConfigSelectionLabel.Size = new System.Drawing.Size(53, 20);
            this.ConfigSelectionLabel.TabIndex = 8;
            this.ConfigSelectionLabel.Values.Text = "Config :";
            // 
            // TargetPathButton
            // 
            this.TargetPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetPathButton.Location = new System.Drawing.Point(95, 29);
            this.TargetPathButton.Name = "TargetPathButton";
            this.TargetPathButton.Size = new System.Drawing.Size(90, 25);
            this.TargetPathButton.TabIndex = 7;
            this.TargetPathButton.Values.Text = "Path";
            this.TargetPathButton.Click += new System.EventHandler(this.TargetPathButton_Click);
            // 
            // TargetPathLabel
            // 
            this.TargetPathLabel.Location = new System.Drawing.Point(10, 31);
            this.TargetPathLabel.Name = "TargetPathLabel";
            this.TargetPathLabel.Size = new System.Drawing.Size(79, 20);
            this.TargetPathLabel.TabIndex = 6;
            this.TargetPathLabel.Values.Text = "Target Path :";
            // 
            // SuppressButton
            // 
            this.SuppressButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SuppressButton.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
            this.SuppressButton.Location = new System.Drawing.Point(165, 5);
            this.SuppressButton.Name = "SuppressButton";
            this.SuppressButton.Size = new System.Drawing.Size(26, 26);
            this.SuppressButton.TabIndex = 5;
            this.SuppressButton.Values.Image = global::Analytics_V2.Properties.Resources.Delete;
            this.SuppressButton.Values.Text = "";
            // 
            // BatchElementMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.GroupBox);
            this.Name = "BatchElementMulti";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(208, 135);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox.Panel)).EndInit();
            this.GroupBox.Panel.ResumeLayout(false);
            this.GroupBox.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBox)).EndInit();
            this.GroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal ComponentFactory.Krypton.Toolkit.KryptonButton SuppressButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton TargetPathButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel TargetPathLabel;
        internal System.Windows.Forms.Label Config;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel ConfigSelectionLabel;
        internal System.Windows.Forms.Label TutoLabel;
        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox GroupBox;
    }
}
