
namespace PapaProject
{
    partial class DefaultInput
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
            this.comboBoxMainInput = new System.Windows.Forms.ComboBox();
            this.richTextBoxExtendInput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // comboBoxMainInput
            // 
            this.comboBoxMainInput.DropDownHeight = 1;
            this.comboBoxMainInput.FormattingEnabled = true;
            this.comboBoxMainInput.IntegralHeight = false;
            this.comboBoxMainInput.ItemHeight = 20;
            this.comboBoxMainInput.Location = new System.Drawing.Point(0, 0);
            this.comboBoxMainInput.Name = "comboBoxMainInput";
            this.comboBoxMainInput.Size = new System.Drawing.Size(247, 28);
            this.comboBoxMainInput.TabIndex = 0;
            // 
            // richTextBoxExtendInput
            // 
            this.richTextBoxExtendInput.Location = new System.Drawing.Point(0, 34);
            this.richTextBoxExtendInput.Name = "richTextBoxExtendInput";
            this.richTextBoxExtendInput.Size = new System.Drawing.Size(247, 127);
            this.richTextBoxExtendInput.TabIndex = 1;
            this.richTextBoxExtendInput.Text = "";
            this.richTextBoxExtendInput.Visible = false;
            // 
            // DefaultInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBoxExtendInput);
            this.Controls.Add(this.comboBoxMainInput);
            this.Name = "DefaultInput";
            this.Size = new System.Drawing.Size(247, 161);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMainInput;
        private System.Windows.Forms.RichTextBox richTextBoxExtendInput;
    }
}
