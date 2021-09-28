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
            this.SuspendLayout();
            // 
            // FlowAllLine
            // 
            resources.ApplyResources(this.FlowAllLine, "FlowAllLine");
            this.FlowAllLine.Name = "FlowAllLine";
            // 
            // ButtomUpdate
            // 
            resources.ApplyResources(this.ButtomUpdate, "ButtomUpdate");
            this.ButtomUpdate.Name = "ButtomUpdate";
            this.ButtomUpdate.UseVisualStyleBackColor = true;
            this.ButtomUpdate.Click += new System.EventHandler(this.ButtomUpdate_Clicked);
            // 
            // ButtomRemarkList
            // 
            resources.ApplyResources(this.ButtomRemarkList, "ButtomRemarkList");
            this.ButtomRemarkList.BackColor = System.Drawing.SystemColors.Control;
            this.ButtomRemarkList.Name = "ButtomRemarkList";
            this.ButtomRemarkList.UseVisualStyleBackColor = false;
            this.ButtomRemarkList.Click += new System.EventHandler(this.ButtomLabelRemark_Clicked);
            // 
            // PropertyFrom
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButtomRemarkList);
            this.Controls.Add(this.ButtomUpdate);
            this.Controls.Add(this.FlowAllLine);
            this.Name = "PropertyFrom";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel FlowAllLine;
        private Button ButtomUpdate;
        private Button ButtomRemarkList;
    }
}