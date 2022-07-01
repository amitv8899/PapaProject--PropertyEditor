using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;


namespace PapaProject
{
    partial class PropertyFrom
    {
        //private ListView listView1;

        private System.ComponentModel.IContainer components = null;

        ///// <summary>
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
           
        }

        #region Windows Form Designer generated code

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        ///
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyFrom));
            this.FlowAllLine = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtomUpdate = new System.Windows.Forms.Button();
            this.ButtomRemarkList = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FlowAllLine
            // 
            resources.ApplyResources(this.FlowAllLine, "FlowAllLine");
            this.FlowAllLine.BackColor = System.Drawing.SystemColors.ControlLight;
            this.FlowAllLine.Name = "FlowAllLine";
            // 
            // ButtomUpdate
            // 
            resources.ApplyResources(this.ButtomUpdate, "ButtomUpdate");
            this.ButtomUpdate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtomUpdate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.ButtomUpdate.Name = "ButtomUpdate";
            this.ButtomUpdate.UseVisualStyleBackColor = false;
            this.ButtomUpdate.Click += new System.EventHandler(this.ButtomUpdate_Clicked);
            // 
            // ButtomRemarkList
            // 
            resources.ApplyResources(this.ButtomRemarkList, "ButtomRemarkList");
            this.ButtomRemarkList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtomRemarkList.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.ButtomRemarkList.Name = "ButtomRemarkList";
            this.ButtomRemarkList.UseVisualStyleBackColor = false;
            this.ButtomRemarkList.Click += new System.EventHandler(this.ButtomLabelRemark_Clicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // PropertyFrom
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.ButtomRemarkList);
            this.Controls.Add(this.ButtomUpdate);
            this.Controls.Add(this.FlowAllLine);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PropertyFrom";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlowLayoutPanel FlowAllLine;
        private Button ButtomUpdate;
        private Button ButtomRemarkList;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private OpenFileDialog openFileDialog;
    }
}