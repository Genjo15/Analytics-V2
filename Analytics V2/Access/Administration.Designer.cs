namespace Analytics_V2
{
    partial class Administration
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
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.Navigator = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.UsersPage = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.ComboBox = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.DataGridView = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox.Panel)).BeginInit();
            this.ToolStripGroupBox.Panel.SuspendLayout();
            this.ToolStripGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Navigator)).BeginInit();
            this.Navigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsersPage)).BeginInit();
            this.UsersPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStrip
            // 
            this.ToolStrip.AutoSize = false;
            this.ToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.ToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.ImageScalingSize = new System.Drawing.Size(19, 17);
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToolStripButton,
            this.RemoveToolStripButton});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ToolStrip.Size = new System.Drawing.Size(656, 19);
            this.ToolStrip.TabIndex = 0;
            this.ToolStrip.Text = "ToolStrip";
            // 
            // AddToolStripButton
            // 
            this.AddToolStripButton.AutoSize = false;
            this.AddToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddToolStripButton.Image = global::Analytics_V2.Properties.Resources.Add;
            this.AddToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddToolStripButton.Name = "AddToolStripButton";
            this.AddToolStripButton.Size = new System.Drawing.Size(19, 17);
            this.AddToolStripButton.Text = "toolStripButton1";
            this.AddToolStripButton.ToolTipText = "Add an user.";
            this.AddToolStripButton.Click += new System.EventHandler(this.AddToolStripButton_Click);
            // 
            // RemoveToolStripButton
            // 
            this.RemoveToolStripButton.AutoSize = false;
            this.RemoveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveToolStripButton.Image = global::Analytics_V2.Properties.Resources.Remove;
            this.RemoveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveToolStripButton.Name = "RemoveToolStripButton";
            this.RemoveToolStripButton.Size = new System.Drawing.Size(19, 17);
            this.RemoveToolStripButton.Text = "toolStripButton2";
            this.RemoveToolStripButton.ToolTipText = "Remove selected user.";
            this.RemoveToolStripButton.Click += new System.EventHandler(this.RemoveToolStripButton_Click);
            // 
            // ToolStripGroupBox
            // 
            this.ToolStripGroupBox.CaptionEdge = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.ToolStripGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.ToolStripGroupBox.CaptionVisible = false;
            this.ToolStripGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolStripGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ToolStripGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.ToolStripGroupBox.Name = "ToolStripGroupBox";
            // 
            // ToolStripGroupBox.Panel
            // 
            this.ToolStripGroupBox.Panel.Controls.Add(this.ToolStrip);
            this.ToolStripGroupBox.Size = new System.Drawing.Size(660, 23);
            this.ToolStripGroupBox.TabIndex = 2;
            // 
            // Navigator
            // 
            this.Navigator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Navigator.Bar.BarOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.Navigator.Bar.ItemOrientation = ComponentFactory.Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.Navigator.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.Navigator.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.Navigator.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.Navigator.Location = new System.Drawing.Point(3, 26);
            this.Navigator.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Navigator.Name = "Navigator";
            this.Navigator.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.UsersPage});
            this.Navigator.SelectedIndex = 0;
            this.Navigator.Size = new System.Drawing.Size(654, 341);
            this.Navigator.TabIndex = 3;
            this.Navigator.Text = "kryptonNavigator1";
            this.Navigator.SelectedPageChanged += new System.EventHandler(this.Navigator_SelectedPageChanged);
            // 
            // UsersPage
            // 
            this.UsersPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.UsersPage.Controls.Add(this.ComboBox);
            this.UsersPage.Controls.Add(this.DataGridView);
            this.UsersPage.Flags = 65534;
            this.UsersPage.LastVisibleSet = true;
            this.UsersPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.UsersPage.Name = "UsersPage";
            this.UsersPage.Size = new System.Drawing.Size(608, 339);
            this.UsersPage.Text = "Users";
            this.UsersPage.ToolTipTitle = "Page ToolTip";
            this.UsersPage.UniqueName = "CBBAD44AA567420E3E9A40E94D42FB51";
            // 
            // ComboBox
            // 
            this.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox.DropDownWidth = 121;
            this.ComboBox.Items.AddRange(new object[] {
            "user",
            "admin",
            "superadmin"});
            this.ComboBox.Location = new System.Drawing.Point(244, 108);
            this.ComboBox.Name = "ComboBox";
            this.ComboBox.Size = new System.Drawing.Size(121, 21);
            this.ComboBox.TabIndex = 1;
            this.ComboBox.Visible = false;
            this.ComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView.Location = new System.Drawing.Point(0, 0);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(608, 339);
            this.DataGridView.TabIndex = 0;
            this.DataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView_CellBeginEdit);
            this.DataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridView_CellValidating);
            // 
            // Administration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Navigator);
            this.Controls.Add(this.ToolStripGroupBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Administration";
            this.Size = new System.Drawing.Size(660, 370);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox.Panel)).EndInit();
            this.ToolStripGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox)).EndInit();
            this.ToolStripGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Navigator)).EndInit();
            this.Navigator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UsersPage)).EndInit();
            this.UsersPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton AddToolStripButton;
        private System.Windows.Forms.ToolStripButton RemoveToolStripButton;
        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox ToolStripGroupBox;
        private ComponentFactory.Krypton.Navigator.KryptonPage UsersPage;
        internal ComponentFactory.Krypton.Navigator.KryptonNavigator Navigator;
        internal ComponentFactory.Krypton.Toolkit.KryptonDataGridView DataGridView;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ComboBox;

    }
}
