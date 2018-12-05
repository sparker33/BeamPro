namespace BeamPro
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSaveDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveToolStripSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLoadButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripZeroViewButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSolveButton = new System.Windows.Forms.ToolStripButton();
            this.componentPallette1 = new BeamPro.ComponentPallette();
            this.objectHolder1 = new BeamPro.ObjectHolder();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 494);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(697, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSaveDropDownButton,
            this.toolStripLoadButton,
            this.toolStripZeroViewButton,
            this.toolStripSolveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(697, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSaveDropDownButton
            // 
            this.toolStripSaveDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSaveDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripSaveAsMenuItem,
            this.saveToolStripSaveMenuItem});
            this.toolStripSaveDropDownButton.Image = global::BeamPro.Properties.Resources.Save;
            this.toolStripSaveDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSaveDropDownButton.Name = "toolStripSaveDropDownButton";
            this.toolStripSaveDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.toolStripSaveDropDownButton.Text = "Save...";
            // 
            // saveToolStripSaveAsMenuItem
            // 
            this.saveToolStripSaveAsMenuItem.Name = "saveToolStripSaveAsMenuItem";
            this.saveToolStripSaveAsMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripSaveAsMenuItem.Text = "Save As...";
            this.saveToolStripSaveAsMenuItem.Click += new System.EventHandler(this.saveToolStripSaveAsMenuItem_Click);
            // 
            // saveToolStripSaveMenuItem
            // 
            this.saveToolStripSaveMenuItem.Name = "saveToolStripSaveMenuItem";
            this.saveToolStripSaveMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripSaveMenuItem.Text = "Save";
            this.saveToolStripSaveMenuItem.Click += new System.EventHandler(this.saveToolStripSaveMenuItem_Click);
            // 
            // toolStripLoadButton
            // 
            this.toolStripLoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLoadButton.Image = global::BeamPro.Properties.Resources.LoadIcon;
            this.toolStripLoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLoadButton.Name = "toolStripLoadButton";
            this.toolStripLoadButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripLoadButton.Text = "Load";
            this.toolStripLoadButton.Click += new System.EventHandler(this.toolStripLoadButton_Click);
            // 
            // toolStripZeroViewButton
            // 
            this.toolStripZeroViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripZeroViewButton.Image = global::BeamPro.Properties.Resources.ResetView;
            this.toolStripZeroViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripZeroViewButton.Name = "toolStripZeroViewButton";
            this.toolStripZeroViewButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripZeroViewButton.Text = "0 View";
            this.toolStripZeroViewButton.Click += new System.EventHandler(this.toolStripZeroViewButton_Click);
            // 
            // toolStripSolveButton
            // 
            this.toolStripSolveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSolveButton.Image = global::BeamPro.Properties.Resources.SolveIcon;
            this.toolStripSolveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSolveButton.Name = "toolStripSolveButton";
            this.toolStripSolveButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripSolveButton.Text = "Solve";
            this.toolStripSolveButton.Click += new System.EventHandler(this.toolStripSolveButton_Click);
            // 
            // componentPallette1
            // 
            this.componentPallette1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.componentPallette1.Location = new System.Drawing.Point(0, 405);
            this.componentPallette1.Name = "componentPallette1";
            this.componentPallette1.Size = new System.Drawing.Size(697, 82);
            this.componentPallette1.TabIndex = 1;
            this.componentPallette1.Text = "componentPallette1";
            // 
            // objectHolder1
            // 
            this.objectHolder1.AllowDrop = true;
            this.objectHolder1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectHolder1.AutoScroll = true;
            this.objectHolder1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objectHolder1.Location = new System.Drawing.Point(0, 28);
            this.objectHolder1.Name = "objectHolder1";
            this.objectHolder1.Size = new System.Drawing.Size(697, 371);
            this.objectHolder1.TabIndex = 0;
            this.objectHolder1.Text = "objectHolder1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 516);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.componentPallette1);
            this.Controls.Add(this.objectHolder1);
            this.Name = "MainForm";
            this.Text = "BeamPro";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentPallette componentPallette1;
        private ObjectHolder objectHolder1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSaveDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripSaveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripSaveMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripLoadButton;
        private System.Windows.Forms.ToolStripButton toolStripZeroViewButton;
        private System.Windows.Forms.ToolStripButton toolStripSolveButton;
    }
}

