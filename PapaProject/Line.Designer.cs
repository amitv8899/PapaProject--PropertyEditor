
namespace PapaProject
{
    partial class Line
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelName = new System.Windows.Forms.Label();
            this.LabelEqual = new System.Windows.Forms.Label();
            this.materialContextMenuLine = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.remarkToolStroMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBeforeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAfterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialContextMenuLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelName
            // 
            this.LabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelName.Location = new System.Drawing.Point(45, 7);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(93, 29);
            this.LabelName.TabIndex = 0;
            this.LabelName.Text = "label1";
            // 
            // LabelEqual
            // 
            this.LabelEqual.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelEqual.Location = new System.Drawing.Point(144, 7);
            this.LabelEqual.Name = "LabelEqual";
            this.LabelEqual.Size = new System.Drawing.Size(27, 29);
            this.LabelEqual.TabIndex = 1;
            this.LabelEqual.Text = "=";
            // 
            // materialContextMenuLine
            // 
            this.materialContextMenuLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialContextMenuLine.Depth = 0;
            this.materialContextMenuLine.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.materialContextMenuLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remarkToolStroMenuItem,
            this.addToolStripMenuItem});
            this.materialContextMenuLine.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuLine.Name = "materialContextMenuLine";
            this.materialContextMenuLine.Size = new System.Drawing.Size(144, 68);
            // 
            // remarkToolStroMenuItem
            // 
            this.remarkToolStroMenuItem.Name = "remarkToolStroMenuItem";
            this.remarkToolStroMenuItem.Size = new System.Drawing.Size(143, 32);
            this.remarkToolStroMenuItem.Text = "Remark";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBeforeToolStripMenuItem,
            this.addAfterToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(143, 32);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // addBeforeToolStripMenuItem
            // 
            this.addBeforeToolStripMenuItem.Name = "addBeforeToolStripMenuItem";
            this.addBeforeToolStripMenuItem.Size = new System.Drawing.Size(204, 34);
            this.addBeforeToolStripMenuItem.Text = "Add Before";
            // 
            // addAfterToolStripMenuItem
            // 
            this.addAfterToolStripMenuItem.Name = "addAfterToolStripMenuItem";
            this.addAfterToolStripMenuItem.Size = new System.Drawing.Size(204, 34);
            this.addAfterToolStripMenuItem.Text = "Add after";
            // 
            // Line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ContextMenuStrip = this.materialContextMenuLine;
            this.Controls.Add(this.LabelEqual);
            this.Controls.Add(this.LabelName);
            this.Name = "Line";
            this.Size = new System.Drawing.Size(590, 62);
            this.Load += new System.EventHandler(this.Line_Load);
            this.materialContextMenuLine.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Label LabelEqual;
        //private System.Windows.Forms.RichTextBox TextBoxVal;
        ////private System.Windows.Forms.CheckBox checkBoxRemark;
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuLine;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBeforeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAfterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remarkToolStroMenuItem;
    }
}
