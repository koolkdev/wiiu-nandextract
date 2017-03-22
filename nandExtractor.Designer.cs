/*  This file is part of Wii NAND Extractor.
 *  Copyright (C) 2009 Ben Wilson
 *  
 *  Wii NAND Extractor is free software: you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License as published
 *  by the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Wii NAND Extractor is distributed in the hope that it will be
 *  useful, but WITHOUT ANY WARRANTY; without even the implied warranty
 *  of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace NAND_Extractor
{
    partial class nandExtractor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(nandExtractor));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAllFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.enterNandKeyFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileView = new System.Windows.Forms.TreeView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextExtract = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.info = new System.Windows.Forms.StatusStrip();
            this.lbl_size = new System.Windows.Forms.ToolStripStatusLabel();
            this.size = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_files = new System.Windows.Forms.ToolStripStatusLabel();
            this.files = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_stopwatch = new System.Windows.Forms.ToolStripStatusLabel();
            this.extractTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.viewIcons = new System.Windows.Forms.ImageList(this.components);
            this.menuMain.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.info.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(501, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenu,
            this.extractAllFileMenu,
            this.toolStripSeparator1,
            this.enterNandKeyFileMenu,
            this.toolStripSeparator3,
            this.aboutFileMenu,
            this.toolStripSeparator2,
            this.exitFileMenu});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(35, 20);
            this.fileMenu.Text = "&File";
            // 
            // openFileMenu
            // 
            this.openFileMenu.Image = global::NAND_Extractor.Properties.Resources.fileopen;
            this.openFileMenu.Name = "openFileMenu";
            this.openFileMenu.Size = new System.Drawing.Size(157, 22);
            this.openFileMenu.Text = "&Open";
            this.openFileMenu.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // extractAllFileMenu
            // 
            this.extractAllFileMenu.Enabled = false;
            this.extractAllFileMenu.Image = global::NAND_Extractor.Properties.Resources.decrypted;
            this.extractAllFileMenu.Name = "extractAllFileMenu";
            this.extractAllFileMenu.ShowShortcutKeys = false;
            this.extractAllFileMenu.Size = new System.Drawing.Size(157, 22);
            this.extractAllFileMenu.Text = "&Extract All";
            this.extractAllFileMenu.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // enterNandKeyFileMenu
            // 
            this.enterNandKeyFileMenu.Image = global::NAND_Extractor.Properties.Resources.password;
            this.enterNandKeyFileMenu.Name = "enterNandKeyFileMenu";
            this.enterNandKeyFileMenu.Size = new System.Drawing.Size(157, 22);
            this.enterNandKeyFileMenu.Text = "Enter &NAND Key";
            this.enterNandKeyFileMenu.Click += new System.EventHandler(this.enterNandKeyMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // aboutFileMenu
            // 
            this.aboutFileMenu.Image = global::NAND_Extractor.Properties.Resources.info;
            this.aboutFileMenu.Name = "aboutFileMenu";
            this.aboutFileMenu.Size = new System.Drawing.Size(157, 22);
            this.aboutFileMenu.Text = "&About";
            this.aboutFileMenu.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // exitFileMenu
            // 
            this.exitFileMenu.Image = global::NAND_Extractor.Properties.Resources.exit;
            this.exitFileMenu.Name = "exitFileMenu";
            this.exitFileMenu.Size = new System.Drawing.Size(157, 22);
            this.exitFileMenu.Text = "E&xit";
            this.exitFileMenu.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // fileView
            // 
            this.fileView.ContextMenuStrip = this.contextMenu;
            this.fileView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileView.HideSelection = false;
            this.fileView.ImageIndex = 2;
            this.fileView.ImageList = this.viewIcons;
            this.fileView.Location = new System.Drawing.Point(12, 49);
            this.fileView.Name = "fileView";
            this.fileView.SelectedImageIndex = 0;
            this.fileView.ShowNodeToolTips = true;
            this.fileView.Size = new System.Drawing.Size(453, 253);
            this.fileView.TabIndex = 2;
            this.fileView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fileView_MouseDown);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextExtract});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(111, 26);
            // 
            // contextExtract
            // 
            this.contextExtract.Enabled = false;
            this.contextExtract.Image = global::NAND_Extractor.Properties.Resources.decrypted;
            this.contextExtract.Name = "contextExtract";
            this.contextExtract.Size = new System.Drawing.Size(110, 22);
            this.contextExtract.Text = "Extract";
            this.contextExtract.Click += new System.EventHandler(this.contextExtract_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(0, 330);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(501, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(486, 17);
            this.status.Spring = true;
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // info
            // 
            this.info.Dock = System.Windows.Forms.DockStyle.Top;
            this.info.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_size,
            this.size,
            this.lbl_files,
            this.files,
            this.lbl_stopwatch,
            this.extractTime});
            this.info.Location = new System.Drawing.Point(0, 24);
            this.info.Name = "info";
            this.info.ShowItemToolTips = true;
            this.info.Size = new System.Drawing.Size(501, 22);
            this.info.SizingGrip = false;
            this.info.TabIndex = 4;
            // 
            // lbl_size
            // 
            this.lbl_size.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_size.Margin = new System.Windows.Forms.Padding(24, 3, 0, 2);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.Size = new System.Drawing.Size(30, 17);
            this.lbl_size.Text = "Size:";
            this.lbl_size.ToolTipText = "The total space used (in bytes).";
            // 
            // size
            // 
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(13, 17);
            this.size.Text = "0";
            this.size.ToolTipText = "The total space used (in bytes).";
            // 
            // lbl_files
            // 
            this.lbl_files.Margin = new System.Windows.Forms.Padding(24, 3, 0, 2);
            this.lbl_files.Name = "lbl_files";
            this.lbl_files.Size = new System.Drawing.Size(31, 17);
            this.lbl_files.Text = "Files:";
            this.lbl_files.ToolTipText = "Total filesystem entries (including directories).";
            // 
            // files
            // 
            this.files.Name = "files";
            this.files.Size = new System.Drawing.Size(13, 17);
            this.files.Text = "0";
            this.files.ToolTipText = "Total filesystem entries (including directories).";
            // 
            // lbl_stopwatch
            // 
            this.lbl_stopwatch.Margin = new System.Windows.Forms.Padding(48, 3, 0, 2);
            this.lbl_stopwatch.Name = "lbl_stopwatch";
            this.lbl_stopwatch.Size = new System.Drawing.Size(209, 17);
            this.lbl_stopwatch.Spring = true;
            this.lbl_stopwatch.Text = "Last extraction time:";
            this.lbl_stopwatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // extractTime
            // 
            this.extractTime.AutoSize = false;
            this.extractTime.Name = "extractTime";
            this.extractTime.Size = new System.Drawing.Size(94, 17);
            this.extractTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // viewIcons
            // 
            this.viewIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("viewIcons.ImageStream")));
            this.viewIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.viewIcons.Images.SetKeyName(0, "folder_yellow.png");
            this.viewIcons.Images.SetKeyName(1, "file.png");
            this.viewIcons.Images.SetKeyName(2, "kcmmemory.png");
            this.viewIcons.Images.SetKeyName(3, "Default Icon.ico");
            this.viewIcons.Images.SetKeyName(4, "Folder Closed.ico");
            // 
            // nandExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 352);
            this.Controls.Add(this.info);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.fileView);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "nandExtractor";
            this.Text = "Wii NAND Extractor";
            this.Resize += new System.EventHandler(this.nandExtractor_Resize);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.info.ResumeLayout(false);
            this.info.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openFileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenu;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TreeView fileView;
        private System.Windows.Forms.ToolStripMenuItem extractAllFileMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem contextExtract;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutFileMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem enterNandKeyFileMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripStatusLabel status;
        public System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.StatusStrip info;
        private System.Windows.Forms.ToolStripStatusLabel lbl_size;
        private System.Windows.Forms.ToolStripStatusLabel lbl_files;
        private System.Windows.Forms.ToolStripStatusLabel size;
        private System.Windows.Forms.ToolStripStatusLabel files;
        private System.Windows.Forms.ToolStripStatusLabel lbl_stopwatch;
        private System.Windows.Forms.ToolStripStatusLabel extractTime;
        private System.Windows.Forms.ImageList viewIcons;
    }
}