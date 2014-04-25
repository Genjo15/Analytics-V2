namespace Analytics_V2
{
    partial class BatchUC
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
            this.ToolStripGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SplitContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.Navigator = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.Page1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.HeaderGroup = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.BackButtonSpec = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.SaveButtonSpec = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.SplitContainer2 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.BatchNameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.BatchNameLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.BatchElementsGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.Page2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox.Panel)).BeginInit();
            this.ToolStripGroupBox.Panel.SuspendLayout();
            this.ToolStripGroupBox.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel1)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel2)).BeginInit();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Navigator)).BeginInit();
            this.Navigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Page1)).BeginInit();
            this.Page1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup.Panel)).BeginInit();
            this.HeaderGroup.Panel.SuspendLayout();
            this.HeaderGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel1)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel2)).BeginInit();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BatchElementsGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BatchElementsGroupBox.Panel)).BeginInit();
            this.BatchElementsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Page2)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStripGroupBox
            // 
            this.ToolStripGroupBox.CaptionEdge = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.ToolStripGroupBox.CaptionVisible = false;
            this.ToolStripGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolStripGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ToolStripGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.ToolStripGroupBox.Name = "ToolStripGroupBox";
            // 
            // ToolStripGroupBox.Panel
            // 
            this.ToolStripGroupBox.Panel.Controls.Add(this.ToolStrip);
            this.ToolStripGroupBox.Size = new System.Drawing.Size(565, 23);
            this.ToolStripGroupBox.TabIndex = 0;
            // 
            // ToolStrip
            // 
            this.ToolStrip.AutoSize = false;
            this.ToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.ImageScalingSize = new System.Drawing.Size(19, 17);
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToolStripButton,
            this.RemoveToolStripButton});
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip.Size = new System.Drawing.Size(561, 19);
            this.ToolStrip.TabIndex = 1;
            this.ToolStrip.Text = "toolStrip1";
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
            this.AddToolStripButton.ToolTipText = "Add Batch";
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
            this.RemoveToolStripButton.ToolTipText = "Delete Batch";
            // 
            // SplitContainer
            // 
            this.SplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.ToolStripGroupBox);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.Navigator);
            this.SplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.SplitContainer.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.SplitContainer.Size = new System.Drawing.Size(923, 550);
            this.SplitContainer.SplitterDistance = 565;
            this.SplitContainer.TabIndex = 1;
            // 
            // Navigator
            // 
            this.Navigator.Bar.BarOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.Navigator.Bar.ItemOrientation = ComponentFactory.Krypton.Toolkit.ButtonOrientation.FixedRight;
            this.Navigator.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.Navigator.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.Navigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Navigator.Location = new System.Drawing.Point(5, 0);
            this.Navigator.Name = "Navigator";
            this.Navigator.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.Page1,
            this.Page2});
            this.Navigator.SelectedIndex = 0;
            this.Navigator.Size = new System.Drawing.Size(348, 550);
            this.Navigator.TabIndex = 4;
            this.Navigator.Text = "kryptonNavigator1";
            // 
            // Page1
            // 
            this.Page1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.Page1.Controls.Add(this.HeaderGroup);
            this.Page1.Flags = 65534;
            this.Page1.LastVisibleSet = true;
            this.Page1.MinimumSize = new System.Drawing.Size(50, 50);
            this.Page1.Name = "Page1";
            this.Page1.Padding = new System.Windows.Forms.Padding(3);
            this.Page1.Size = new System.Drawing.Size(321, 548);
            this.Page1.Text = "Single Config";
            this.Page1.ToolTipTitle = "Page ToolTip";
            this.Page1.UniqueName = "AFD972E913314623EB943B66BE8F533C";
            // 
            // HeaderGroup
            // 
            this.HeaderGroup.AllowButtonSpecToolTips = true;
            this.HeaderGroup.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.BackButtonSpec,
            this.SaveButtonSpec});
            this.HeaderGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderGroup.HeaderVisibleSecondary = false;
            this.HeaderGroup.Location = new System.Drawing.Point(3, 3);
            this.HeaderGroup.Name = "HeaderGroup";
            // 
            // HeaderGroup.Panel
            // 
            this.HeaderGroup.Panel.Controls.Add(this.SplitContainer2);
            this.HeaderGroup.Size = new System.Drawing.Size(315, 542);
            this.HeaderGroup.TabIndex = 0;
            this.HeaderGroup.ValuesPrimary.Image = null;
            // 
            // BackButtonSpec
            // 
            this.BackButtonSpec.Image = global::Analytics_V2.Properties.Resources.Back;
            this.BackButtonSpec.ToolTipBody = "Cancel";
            this.BackButtonSpec.ToolTipTitle = "Cancel";
            this.BackButtonSpec.UniqueName = "DC36B74989304ACCEC9FC058E75D9F9D";
            this.BackButtonSpec.Click += new System.EventHandler(this.BackButtonSpec_Click);
            // 
            // SaveButtonSpec
            // 
            this.SaveButtonSpec.Image = global::Analytics_V2.Properties.Resources.Save;
            this.SaveButtonSpec.UniqueName = "19009E59FBA149C9C08209CBBC61CE79";
            this.SaveButtonSpec.Click += new System.EventHandler(this.SaveButtonSpec_Click);
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            this.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.BatchNameTextBox);
            this.SplitContainer2.Panel1.Controls.Add(this.BatchNameLabel);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.AllowDrop = true;
            this.SplitContainer2.Panel2.AutoScroll = true;
            this.SplitContainer2.Panel2.Controls.Add(this.BatchElementsGroupBox);
            this.SplitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.SplitContainer2.Size = new System.Drawing.Size(313, 506);
            this.SplitContainer2.SplitterDistance = 41;
            this.SplitContainer2.SplitterWidth = 0;
            this.SplitContainer2.TabIndex = 0;
            // 
            // BatchNameTextBox
            // 
            this.BatchNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchNameTextBox.Location = new System.Drawing.Point(100, 12);
            this.BatchNameTextBox.Name = "BatchNameTextBox";
            this.BatchNameTextBox.Size = new System.Drawing.Size(198, 20);
            this.BatchNameTextBox.TabIndex = 1;
            // 
            // BatchNameLabel
            // 
            this.BatchNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchNameLabel.Location = new System.Drawing.Point(11, 12);
            this.BatchNameLabel.MaximumSize = new System.Drawing.Size(0, 20);
            this.BatchNameLabel.Name = "BatchNameLabel";
            this.BatchNameLabel.Size = new System.Drawing.Size(83, 20);
            this.BatchNameLabel.TabIndex = 2;
            this.BatchNameLabel.Values.Text = "Batch Name :";
            // 
            // BatchElementsGroupBox
            // 
            this.BatchElementsGroupBox.CaptionVisible = false;
            this.BatchElementsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BatchElementsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.BatchElementsGroupBox.Name = "BatchElementsGroupBox";
            // 
            // BatchElementsGroupBox.Panel
            // 
            this.BatchElementsGroupBox.Panel.AllowDrop = true;
            this.BatchElementsGroupBox.Panel.AutoScroll = true;
            this.BatchElementsGroupBox.Panel.Margin = new System.Windows.Forms.Padding(3);
            this.BatchElementsGroupBox.Panel.Padding = new System.Windows.Forms.Padding(3);
            this.BatchElementsGroupBox.Size = new System.Drawing.Size(307, 459);
            this.BatchElementsGroupBox.TabIndex = 0;
            // 
            // Page2
            // 
            this.Page2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.Page2.Flags = 65534;
            this.Page2.LastVisibleSet = true;
            this.Page2.MinimumSize = new System.Drawing.Size(50, 50);
            this.Page2.Name = "Page2";
            this.Page2.Size = new System.Drawing.Size(267, 548);
            this.Page2.Text = "Multiple configs";
            this.Page2.ToolTipTitle = "Page ToolTip";
            this.Page2.UniqueName = "7C897595D2CC4F6D65B1D43DE722856E";
            // 
            // BatchUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SplitContainer);
            this.Name = "BatchUC";
            this.Size = new System.Drawing.Size(923, 550);
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox.Panel)).EndInit();
            this.ToolStripGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ToolStripGroupBox)).EndInit();
            this.ToolStripGroupBox.ResumeLayout(false);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel1)).EndInit();
            this.SplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer.Panel2)).EndInit();
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Navigator)).EndInit();
            this.Navigator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Page1)).EndInit();
            this.Page1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup.Panel)).EndInit();
            this.HeaderGroup.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderGroup)).EndInit();
            this.HeaderGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel1)).EndInit();
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2.Panel2)).EndInit();
            this.SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BatchElementsGroupBox.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BatchElementsGroupBox)).EndInit();
            this.BatchElementsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Page2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox ToolStripGroupBox;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton AddToolStripButton;
        private System.Windows.Forms.ToolStripButton RemoveToolStripButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel BatchNameLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox BatchNameTextBox;
        internal ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator Navigator;
        private ComponentFactory.Krypton.Navigator.KryptonPage Page1;
        private ComponentFactory.Krypton.Navigator.KryptonPage Page2;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SplitContainer2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup HeaderGroup;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BackButtonSpec;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup SaveButtonSpec;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox BatchElementsGroupBox;
    }
}
