namespace PERF_Design
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
            this.PanelGridContainer = new System.Windows.Forms.Panel();
            this.PanelControlPanel = new System.Windows.Forms.Panel();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonNewBoard = new System.Windows.Forms.Button();
            this.CheckBoxEraseWireTool = new System.Windows.Forms.CheckBox();
            this.CheckBoxWireTool = new System.Windows.Forms.CheckBox();
            this.ButtonCancelPlacing = new System.Windows.Forms.Button();
            this.CheckBoxRotateChipTool = new System.Windows.Forms.CheckBox();
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.ButtonSaveAs = new System.Windows.Forms.Button();
            this.CheckBoxEraseChipTool = new System.Windows.Forms.CheckBox();
            this.CheckBoxChipTool = new System.Windows.Forms.CheckBox();
            this.CheckBoxEraseSolderTool = new System.Windows.Forms.CheckBox();
            this.CheckBoxSolderTool = new System.Windows.Forms.CheckBox();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PanelControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelGridContainer
            // 
            this.PanelGridContainer.BackColor = System.Drawing.SystemColors.Control;
            this.PanelGridContainer.Location = new System.Drawing.Point(0, 47);
            this.PanelGridContainer.Name = "PanelGridContainer";
            this.PanelGridContainer.Size = new System.Drawing.Size(50, 50);
            this.PanelGridContainer.TabIndex = 3;
            // 
            // PanelControlPanel
            // 
            this.PanelControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelControlPanel.Controls.Add(this.ButtonSave);
            this.PanelControlPanel.Controls.Add(this.ButtonNewBoard);
            this.PanelControlPanel.Controls.Add(this.CheckBoxEraseWireTool);
            this.PanelControlPanel.Controls.Add(this.CheckBoxWireTool);
            this.PanelControlPanel.Controls.Add(this.ButtonCancelPlacing);
            this.PanelControlPanel.Controls.Add(this.CheckBoxRotateChipTool);
            this.PanelControlPanel.Controls.Add(this.ButtonLoad);
            this.PanelControlPanel.Controls.Add(this.ButtonSaveAs);
            this.PanelControlPanel.Controls.Add(this.CheckBoxEraseChipTool);
            this.PanelControlPanel.Controls.Add(this.CheckBoxChipTool);
            this.PanelControlPanel.Controls.Add(this.CheckBoxEraseSolderTool);
            this.PanelControlPanel.Controls.Add(this.CheckBoxSolderTool);
            this.PanelControlPanel.Location = new System.Drawing.Point(-1, -1);
            this.PanelControlPanel.Name = "PanelControlPanel";
            this.PanelControlPanel.Size = new System.Drawing.Size(1125, 48);
            this.PanelControlPanel.TabIndex = 6;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(840, 12);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(81, 24);
            this.ButtonSave.TabIndex = 19;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonNewBoard
            // 
            this.ButtonNewBoard.Location = new System.Drawing.Point(753, 12);
            this.ButtonNewBoard.Name = "ButtonNewBoard";
            this.ButtonNewBoard.Size = new System.Drawing.Size(81, 24);
            this.ButtonNewBoard.TabIndex = 18;
            this.ButtonNewBoard.Text = "New";
            this.ButtonNewBoard.UseVisualStyleBackColor = true;
            this.ButtonNewBoard.Click += new System.EventHandler(this.ButtonNewBoard_Click);
            // 
            // CheckBoxEraseWireTool
            // 
            this.CheckBoxEraseWireTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxEraseWireTool.AutoCheck = false;
            this.CheckBoxEraseWireTool.Location = new System.Drawing.Point(273, 12);
            this.CheckBoxEraseWireTool.Name = "CheckBoxEraseWireTool";
            this.CheckBoxEraseWireTool.Size = new System.Drawing.Size(81, 24);
            this.CheckBoxEraseWireTool.TabIndex = 17;
            this.CheckBoxEraseWireTool.Text = "Erase Wire";
            this.CheckBoxEraseWireTool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxEraseWireTool.UseVisualStyleBackColor = true;
            this.CheckBoxEraseWireTool.Click += new System.EventHandler(this.CheckBoxEraseWireTool_Click);
            // 
            // CheckBoxWireTool
            // 
            this.CheckBoxWireTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxWireTool.AutoCheck = false;
            this.CheckBoxWireTool.Location = new System.Drawing.Point(186, 12);
            this.CheckBoxWireTool.Name = "CheckBoxWireTool";
            this.CheckBoxWireTool.Size = new System.Drawing.Size(81, 24);
            this.CheckBoxWireTool.TabIndex = 16;
            this.CheckBoxWireTool.Text = "Wire";
            this.CheckBoxWireTool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxWireTool.UseVisualStyleBackColor = true;
            this.CheckBoxWireTool.Click += new System.EventHandler(this.CheckBoxWireTool_Click);
            // 
            // ButtonCancelPlacing
            // 
            this.ButtonCancelPlacing.Enabled = false;
            this.ButtonCancelPlacing.Location = new System.Drawing.Point(621, 12);
            this.ButtonCancelPlacing.Name = "ButtonCancelPlacing";
            this.ButtonCancelPlacing.Size = new System.Drawing.Size(81, 24);
            this.ButtonCancelPlacing.TabIndex = 15;
            this.ButtonCancelPlacing.Text = "Cancel";
            this.ButtonCancelPlacing.UseVisualStyleBackColor = true;
            this.ButtonCancelPlacing.Click += new System.EventHandler(this.ButtonCancelPlacing_Click);
            // 
            // CheckBoxRotateChipTool
            // 
            this.CheckBoxRotateChipTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxRotateChipTool.AutoCheck = false;
            this.CheckBoxRotateChipTool.Location = new System.Drawing.Point(447, 12);
            this.CheckBoxRotateChipTool.Name = "CheckBoxRotateChipTool";
            this.CheckBoxRotateChipTool.Size = new System.Drawing.Size(81, 24);
            this.CheckBoxRotateChipTool.TabIndex = 14;
            this.CheckBoxRotateChipTool.Text = "Rotate Chip";
            this.CheckBoxRotateChipTool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxRotateChipTool.UseVisualStyleBackColor = true;
            this.CheckBoxRotateChipTool.Click += new System.EventHandler(this.CheckBoxRotateChipTool_Click);
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.Location = new System.Drawing.Point(1014, 12);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(81, 24);
            this.ButtonLoad.TabIndex = 13;
            this.ButtonLoad.Text = "Load";
            this.ButtonLoad.UseVisualStyleBackColor = true;
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // ButtonSaveAs
            // 
            this.ButtonSaveAs.Location = new System.Drawing.Point(927, 12);
            this.ButtonSaveAs.Name = "ButtonSaveAs";
            this.ButtonSaveAs.Size = new System.Drawing.Size(81, 24);
            this.ButtonSaveAs.TabIndex = 12;
            this.ButtonSaveAs.Text = "Save As";
            this.ButtonSaveAs.UseVisualStyleBackColor = true;
            this.ButtonSaveAs.Click += new System.EventHandler(this.ButtonSaveAs_Click);
            // 
            // CheckBoxEraseChipTool
            // 
            this.CheckBoxEraseChipTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxEraseChipTool.AutoCheck = false;
            this.CheckBoxEraseChipTool.Location = new System.Drawing.Point(534, 12);
            this.CheckBoxEraseChipTool.Name = "CheckBoxEraseChipTool";
            this.CheckBoxEraseChipTool.Size = new System.Drawing.Size(81, 24);
            this.CheckBoxEraseChipTool.TabIndex = 10;
            this.CheckBoxEraseChipTool.Text = "Erase Chip";
            this.CheckBoxEraseChipTool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxEraseChipTool.UseVisualStyleBackColor = true;
            this.CheckBoxEraseChipTool.Click += new System.EventHandler(this.CheckBoxEraseChipTool_Click);
            // 
            // CheckBoxChipTool
            // 
            this.CheckBoxChipTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxChipTool.AutoCheck = false;
            this.CheckBoxChipTool.Location = new System.Drawing.Point(360, 12);
            this.CheckBoxChipTool.Name = "CheckBoxChipTool";
            this.CheckBoxChipTool.Size = new System.Drawing.Size(81, 24);
            this.CheckBoxChipTool.TabIndex = 9;
            this.CheckBoxChipTool.Text = "Chip";
            this.CheckBoxChipTool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxChipTool.UseVisualStyleBackColor = true;
            this.CheckBoxChipTool.Click += new System.EventHandler(this.CheckBoxChipTool_Click);
            // 
            // CheckBoxEraseSolderTool
            // 
            this.CheckBoxEraseSolderTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxEraseSolderTool.AutoCheck = false;
            this.CheckBoxEraseSolderTool.Location = new System.Drawing.Point(99, 12);
            this.CheckBoxEraseSolderTool.Name = "CheckBoxEraseSolderTool";
            this.CheckBoxEraseSolderTool.Size = new System.Drawing.Size(81, 24);
            this.CheckBoxEraseSolderTool.TabIndex = 8;
            this.CheckBoxEraseSolderTool.Text = "Erase Solder";
            this.CheckBoxEraseSolderTool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxEraseSolderTool.UseVisualStyleBackColor = true;
            this.CheckBoxEraseSolderTool.Click += new System.EventHandler(this.CheckBoxEraseSolderTool_Click);
            // 
            // CheckBoxSolderTool
            // 
            this.CheckBoxSolderTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxSolderTool.AutoCheck = false;
            this.CheckBoxSolderTool.Location = new System.Drawing.Point(12, 12);
            this.CheckBoxSolderTool.Name = "CheckBoxSolderTool";
            this.CheckBoxSolderTool.Size = new System.Drawing.Size(81, 24);
            this.CheckBoxSolderTool.TabIndex = 7;
            this.CheckBoxSolderTool.Text = "Solder";
            this.CheckBoxSolderTool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxSolderTool.UseVisualStyleBackColor = true;
            this.CheckBoxSolderTool.Click += new System.EventHandler(this.CheckBoxSolderTool_Click);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "dat";
            this.SaveFileDialog.Filter = "Data Files (*.dat)|";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "dat";
            this.OpenFileDialog.Filter = "Data Files (*.dat)|";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.PanelControlPanel);
            this.Controls.Add(this.PanelGridContainer);
            this.Name = "MainForm";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MainForm_Scroll);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseWheel);
            this.PanelControlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelGridContainer;
        private System.Windows.Forms.Panel PanelControlPanel;
        private System.Windows.Forms.CheckBox CheckBoxSolderTool;
        private System.Windows.Forms.CheckBox CheckBoxEraseChipTool;
        private System.Windows.Forms.CheckBox CheckBoxChipTool;
        private System.Windows.Forms.CheckBox CheckBoxEraseSolderTool;
        private System.Windows.Forms.Button ButtonSaveAs;
        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.CheckBox CheckBoxRotateChipTool;
        private System.Windows.Forms.Button ButtonCancelPlacing;
        private System.Windows.Forms.CheckBox CheckBoxEraseWireTool;
        private System.Windows.Forms.CheckBox CheckBoxWireTool;
        private System.Windows.Forms.Button ButtonNewBoard;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button ButtonSave;
    }
}

