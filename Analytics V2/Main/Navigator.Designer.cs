namespace Analytics_V2
{
    partial class Navigator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Navigator));
            this.NavigatorControl = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.switchButtonSpec = new ComponentFactory.Krypton.Navigator.ButtonSpecNavigator();
            this.EnableDisableAllProcessesButtonSpec = new ComponentFactory.Krypton.Navigator.ButtonSpecNavigator();
            this.SummaryTab = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.SummarySplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.SummarySplitContainer2 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.ProgressGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.LogsNavigator = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorControl)).BeginInit();
            this.NavigatorControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryTab)).BeginInit();
            this.SummaryTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer1.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer1.Panel2)).BeginInit();
            this.SummarySplitContainer1.Panel2.SuspendLayout();
            this.SummarySplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer2.Panel1)).BeginInit();
            this.SummarySplitContainer2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer2.Panel2)).BeginInit();
            this.SummarySplitContainer2.Panel2.SuspendLayout();
            this.SummarySplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressGroupBox.Panel)).BeginInit();
            this.ProgressGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogsNavigator)).BeginInit();
            this.LogsNavigator.SuspendLayout();
            this.ContextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // NavigatorControl
            // 
            this.NavigatorControl.Button.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Navigator.ButtonSpecNavigator[] {
            this.switchButtonSpec,
            this.EnableDisableAllProcessesButtonSpec});
            this.NavigatorControl.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.NavigatorControl.Button.ContextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.ShowEnabled;
            this.NavigatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigatorControl.Location = new System.Drawing.Point(0, 0);
            this.NavigatorControl.Name = "NavigatorControl";
            this.NavigatorControl.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.SummaryTab});
            this.NavigatorControl.SelectedIndex = 0;
            this.NavigatorControl.Size = new System.Drawing.Size(638, 392);
            this.NavigatorControl.TabIndex = 0;
            this.NavigatorControl.Text = "kryptonNavigator1";
            // 
            // switchButtonSpec
            // 
            this.switchButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.switchButtonSpec.ExtraText = "Creation Mode";
            this.switchButtonSpec.Image = ((System.Drawing.Image)(resources.GetObject("switchButtonSpec.Image")));
            this.switchButtonSpec.UniqueName = "A177BEABA78545302FBE677A857478D9";
            // 
            // EnableDisableAllProcessesButtonSpec
            // 
            this.EnableDisableAllProcessesButtonSpec.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.EnableDisableAllProcessesButtonSpec.Image = global::Analytics_V2.Properties.Resources.ID;
            this.EnableDisableAllProcessesButtonSpec.Text = "Order IDs ON/OFF";
            this.EnableDisableAllProcessesButtonSpec.UniqueName = "02482CF2C38F499726B8DEB15AB43908";
            this.EnableDisableAllProcessesButtonSpec.Click += new System.EventHandler(this.EnableDisableAllProcessesButtonSpec_Click);
            // 
            // SummaryTab
            // 
            this.SummaryTab.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.SummaryTab.Controls.Add(this.SummarySplitContainer1);
            this.SummaryTab.Flags = 65534;
            this.SummaryTab.LastVisibleSet = true;
            this.SummaryTab.MinimumSize = new System.Drawing.Size(50, 50);
            this.SummaryTab.Name = "SummaryTab";
            this.SummaryTab.Size = new System.Drawing.Size(636, 362);
            this.SummaryTab.Text = "Summary";
            this.SummaryTab.ToolTipTitle = "Page ToolTip";
            this.SummaryTab.UniqueName = "CA00E7C3B1BE40DC098B41518D86AA39";
            // 
            // SummarySplitContainer1
            // 
            this.SummarySplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.SummarySplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SummarySplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SummarySplitContainer1.Name = "SummarySplitContainer1";
            // 
            // SummarySplitContainer1.Panel1
            // 
            this.SummarySplitContainer1.Panel1.AutoScroll = true;
            this.SummarySplitContainer1.Panel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // SummarySplitContainer1.Panel2
            // 
            this.SummarySplitContainer1.Panel2.Controls.Add(this.SummarySplitContainer2);
            this.SummarySplitContainer1.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.SummarySplitContainer1.Size = new System.Drawing.Size(636, 362);
            this.SummarySplitContainer1.SplitterDistance = 146;
            this.SummarySplitContainer1.TabIndex = 0;
            // 
            // SummarySplitContainer2
            // 
            this.SummarySplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.SummarySplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SummarySplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SummarySplitContainer2.Name = "SummarySplitContainer2";
            this.SummarySplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SummarySplitContainer2.Panel1
            // 
            this.SummarySplitContainer2.Panel1.AutoScroll = true;
            this.SummarySplitContainer2.Panel1.Controls.Add(this.ProgressGroupBox);
            this.SummarySplitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(5);
            this.SummarySplitContainer2.Panel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // SummarySplitContainer2.Panel2
            // 
            this.SummarySplitContainer2.Panel2.Controls.Add(this.LogsNavigator);
            this.SummarySplitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.SummarySplitContainer2.Panel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.SummarySplitContainer2.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.SummarySplitContainer2.Size = new System.Drawing.Size(485, 362);
            this.SummarySplitContainer2.SplitterDistance = 166;
            this.SummarySplitContainer2.TabIndex = 0;
            // 
            // ProgressGroupBox
            // 
            this.ProgressGroupBox.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.ProgressGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgressGroupBox.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.ProgressGroupBox.Location = new System.Drawing.Point(5, 5);
            this.ProgressGroupBox.Name = "ProgressGroupBox";
            this.ProgressGroupBox.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // ProgressGroupBox.Panel
            // 
            this.ProgressGroupBox.Panel.AutoScroll = true;
            this.ProgressGroupBox.Panel.ContextMenuStrip = this.ContextMenuStrip2;
            this.ProgressGroupBox.Size = new System.Drawing.Size(475, 156);
            this.ProgressGroupBox.TabIndex = 0;
            this.ProgressGroupBox.Values.Heading = "Progress";
            // 
            // LogsNavigator
            // 
            this.LogsNavigator.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.LogsNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogsNavigator.Location = new System.Drawing.Point(5, 5);
            this.LogsNavigator.Name = "LogsNavigator";
            this.LogsNavigator.PageBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.InputControlRibbon;
            this.LogsNavigator.Panel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.LogsNavigator.Size = new System.Drawing.Size(475, 181);
            this.LogsNavigator.TabIndex = 0;
            this.LogsNavigator.Text = "kryptonNavigator1";
            // 
            // ContextMenuStrip
            // 
            this.ContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ContextMenuStrip.Name = "ContextMenuStrip";
            this.ContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // ContextMenuStrip2
            // 
            this.ContextMenuStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ContextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearAllToolStripMenuItem});
            this.ContextMenuStrip2.Name = "ContextMenuStrip2";
            this.ContextMenuStrip2.Size = new System.Drawing.Size(153, 48);
            // 
            // ClearAllToolStripMenuItem
            // 
            this.ClearAllToolStripMenuItem.Name = "ClearAllToolStripMenuItem";
            this.ClearAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ClearAllToolStripMenuItem.Text = "Clear All";
            this.ClearAllToolStripMenuItem.Click += new System.EventHandler(this.ClearAllToolStripMenuItem_Click);
            // 
            // Navigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NavigatorControl);
            this.Name = "Navigator";
            this.Size = new System.Drawing.Size(638, 392);
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorControl)).EndInit();
            this.NavigatorControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummaryTab)).EndInit();
            this.SummaryTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer1.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer1.Panel2)).EndInit();
            this.SummarySplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer1)).EndInit();
            this.SummarySplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer2.Panel1)).EndInit();
            this.SummarySplitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer2.Panel2)).EndInit();
            this.SummarySplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummarySplitContainer2)).EndInit();
            this.SummarySplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressGroupBox.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressGroupBox)).EndInit();
            this.ProgressGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogsNavigator)).EndInit();
            this.LogsNavigator.ResumeLayout(false);
            this.ContextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal ComponentFactory.Krypton.Navigator.KryptonNavigator NavigatorControl;
        internal ComponentFactory.Krypton.Navigator.KryptonPage SummaryTab;
        internal ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SummarySplitContainer1;
        internal ComponentFactory.Krypton.Toolkit.KryptonSplitContainer SummarySplitContainer2;
        internal ComponentFactory.Krypton.Toolkit.KryptonGroupBox ProgressGroupBox;
        internal ComponentFactory.Krypton.Navigator.KryptonNavigator LogsNavigator;
        internal new System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
        internal ComponentFactory.Krypton.Navigator.ButtonSpecNavigator switchButtonSpec;
        internal ComponentFactory.Krypton.Navigator.ButtonSpecNavigator EnableDisableAllProcessesButtonSpec;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem ClearAllToolStripMenuItem;
    }
}
