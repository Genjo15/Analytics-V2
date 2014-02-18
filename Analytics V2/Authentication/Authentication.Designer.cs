namespace Analytics_V2
{
    partial class ConnectionScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionScreen));
            this.HeaderGroup = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.CancelButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SaveConnectionCheckBox = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ConnectButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.PasswordTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.LoginTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.PasswordLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.LoginLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup.Panel)).BeginInit();
            this.HeaderGroup.Panel.SuspendLayout();
            this.HeaderGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderGroup
            // 
            this.HeaderGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderGroup.HeaderPositionPrimary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.HeaderGroup.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockActive;
            this.HeaderGroup.HeaderVisibleSecondary = false;
            this.HeaderGroup.Location = new System.Drawing.Point(0, 0);
            this.HeaderGroup.Name = "HeaderGroup";
            // 
            // HeaderGroup.Panel
            // 
            this.HeaderGroup.Panel.Controls.Add(this.CancelButton);
            this.HeaderGroup.Panel.Controls.Add(this.SaveConnectionCheckBox);
            this.HeaderGroup.Panel.Controls.Add(this.ConnectButton);
            this.HeaderGroup.Panel.Controls.Add(this.PasswordTextBox);
            this.HeaderGroup.Panel.Controls.Add(this.LoginTextBox);
            this.HeaderGroup.Panel.Controls.Add(this.PasswordLabel);
            this.HeaderGroup.Panel.Controls.Add(this.LoginLabel);
            this.HeaderGroup.Size = new System.Drawing.Size(356, 106);
            this.HeaderGroup.TabIndex = 0;
            this.HeaderGroup.ValuesPrimary.Heading = "Connection";
            this.HeaderGroup.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("HeaderGroup.ValuesPrimary.Image")));
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(255, 67);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(47, 24);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Values.Text = "cancel";
            // 
            // SaveConnectionCheckBox
            // 
            this.SaveConnectionCheckBox.Location = new System.Drawing.Point(44, 71);
            this.SaveConnectionCheckBox.Name = "SaveConnectionCheckBox";
            this.SaveConnectionCheckBox.Size = new System.Drawing.Size(104, 20);
            this.SaveConnectionCheckBox.TabIndex = 5;
            this.SaveConnectionCheckBox.Values.Text = "Remember me";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(244, 17);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(72, 39);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Values.Text = "Connect";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(107, 40);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(120, 20);
            this.PasswordTextBox.TabIndex = 3;
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(107, 14);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(120, 20);
            this.LoginTextBox.TabIndex = 2;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Location = new System.Drawing.Point(11, 40);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(68, 20);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Values.Text = "Password : ";
            // 
            // LoginLabel
            // 
            this.LoginLabel.Location = new System.Drawing.Point(32, 14);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(47, 20);
            this.LoginLabel.TabIndex = 0;
            this.LoginLabel.Values.Text = "Login : ";
            // 
            // ConnectionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HeaderGroup);
            this.Name = "ConnectionScreen";
            this.Size = new System.Drawing.Size(356, 106);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup.Panel)).EndInit();
            this.HeaderGroup.Panel.ResumeLayout(false);
            this.HeaderGroup.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup)).EndInit();
            this.HeaderGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup HeaderGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel LoginLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel PasswordLabel;
        internal ComponentFactory.Krypton.Toolkit.KryptonCheckBox SaveConnectionCheckBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton ConnectButton;
        internal ComponentFactory.Krypton.Toolkit.KryptonTextBox PasswordTextBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonTextBox LoginTextBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonButton CancelButton;
    }
}
