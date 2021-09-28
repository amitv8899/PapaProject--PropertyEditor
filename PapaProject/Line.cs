using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PapaProject
{
    public partial class Line : UserControl
    {
        private ToolStripDropDown m_Popup = new ToolStripDropDown();
        private int m_NumberLine;
        private string m_Range;
        private string m_Type;
        private string m_Caption;
        public event Action<int, string> TaxBoxValChangedInvoker;
        public event Action<int, bool, string> CheckBoxRemarkChangeInvoker;
        public event Action<Line, bool> LineSelectedInvoker;
       
        public Line()
        {
            InitializeComponent();
        }

        private void Line_Load(object sender, EventArgs e)
        {
            //Label Name
            this.LabelName.MouseEnter += new EventHandler(LabelShowFullName_MouseEnter);
            this.LabelName.MouseLeave += new EventHandler(LabelShowFullName_MouseLeave);
            this.LabelName.Click += new EventHandler(Line_Clicked);

            //Equal label
            this.LabelEqual.Location = new Point(this.LabelName.Right, LabelName.Top);
            this.LabelEqual.Click += new EventHandler(Line_Clicked);
            //text box val
            this.TextBoxVal.Location = new Point(LabelEqual.Right, LabelName.Top);
            this.TextBoxVal.TextChanged += new EventHandler(TextBoxVal_Changed);
            this.TextBoxVal.Multiline = true;
            

            ////check box
            ////this.checkBoxRemark.CheckedChanged += new EventHandler(CheckBoxRemark_ChangeVal);


            // ContextMenu
            this.materialContextMenuLine.Opened += new EventHandler(MaterialContextMenuLine_Clicked);
            //RemarkContextMenue
            this.remarkToolStroMenuItem.Click += new EventHandler(RemarkToolStroMenuItem_Clicked);
           


            //this
            this.Size = new Size(LabelName.Size.Width + LabelEqual.Size.Width + TextBoxVal.Size.Width, TextBoxVal.Size.Height);


        }
        private void Line_Clicked(object sender, EventArgs e)
        {
            bool IsSelected = this.LabelName.ForeColor == Color.Black;
         
            OnSelectedLine(IsSelected);
        }
        public void LineSelected()
        {

            this.LabelName.BackColor = Color.LightYellow;
            this.LabelEqual.BackColor =  Color.LightYellow;
            this.TextBoxVal.BackColor = Color.LightYellow;
            this.BackColor = Color.LightYellow;

            //this.LabelName.ForeColor = Color.DarkRed;
            //this.LabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            //this.LabelEqual.ForeColor = Color.DarkRed;
            //this.LabelEqual.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            //this.TextBoxVal.ForeColor = Color.DarkRed;
            //this.TextBoxVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
        }
        public void LineUnSelected()
        {
            this.LabelName.BackColor = Color.Empty;
            this.LabelEqual.BackColor = Color.Empty;
            this.TextBoxVal.BackColor = Color.Empty;
            this.BackColor = Color.Empty;
            //this.LabelName.ForeColor = Color.Black;
            //this.LabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            //this.LabelEqual.ForeColor = Color.Black;
            //this.LabelEqual.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            //this.TextBoxVal.ForeColor = Color.Black;
            //this.TextBoxVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
        }
        protected virtual void OnSelectedLine(bool IsSelected)
        {
            if (LineSelectedInvoker != null)
            {
                this.LineSelectedInvoker.Invoke(this, IsSelected);
            }
        }
        private void MaterialContextMenuLine_Clicked(object sender, EventArgs e)
        {
            MaterialSkin.Controls.MaterialContextMenuStrip CurrentContextMenuStrip = (MaterialSkin.Controls.MaterialContextMenuStrip)sender;
            if (this.TextBoxVal.Enabled==false)
            {
                this.remarkToolStroMenuItem.Text = "Unremark";
            }
            else
            {
                this.remarkToolStroMenuItem.Text = "Remark";
            }
        }
     
        private void RemarkToolStroMenuItem_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            bool IsBecameRemark = true;
            if (toolStripMenuItem.Text == "Remark")// need to make line remark and the  
            {
                this.TextBoxVal.Enabled = false;
               
            }
            else
            {
                this.TextBoxVal.Enabled = true;
               
                IsBecameRemark = false;
            }

            OnChangedRemarkClicked(IsBecameRemark);
        }
        protected virtual void OnChangedRemarkClicked(bool IsChecked)
        {
            if (CheckBoxRemarkChangeInvoker != null)
            {
                this.CheckBoxRemarkChangeInvoker.Invoke(m_NumberLine, IsChecked, this.LabelName.Text);
            }
        }
        ////private void CheckBoxRemark_ChangeVal(object sender, EventArgs e)
        ////{
        ////    CheckBox CurrentCheckBox = (CheckBox)sender;
        ////    if (CurrentCheckBox.Checked == true)
        ////    {
        ////        this.TextBoxVal.Enabled = false;
        ////    }
        ////    else
        ////    {
        ////        this.TextBoxVal.Enabled = true;
        ////    }
        ////    OnChangedCheckBox(CurrentCheckBox.Checked);
        ////}
        ////protected virtual void OnChangedCheckBox(bool IsChecked)
        ////{
        ////    if (CheckBoxRemarkChangeInvoker != null)
        ////    {
        ////        this.CheckBoxRemarkChangeInvoker.Invoke(m_NumberLine, IsChecked, this.LabelName.Text);
        ////    }
        ////}
        private void TextBoxVal_Changed(object sender, EventArgs e)
        {
            RichTextBox CurrentVal = (RichTextBox)sender;
            OnChangedVal(CurrentVal.Text);
        }
        protected virtual void OnChangedVal(string Val)
        {
            if (TaxBoxValChangedInvoker != null)
            {
                this.TaxBoxValChangedInvoker.Invoke(m_NumberLine, Val);
            }
        }
        private void LabelShowFullName_MouseEnter(object sender, EventArgs e)
        {
            Label Current = (Label)sender;
            m_Popup.Margin = Padding.Empty;
            m_Popup.Padding = Padding.Empty;
            StringBuilder PopMsg = new StringBuilder();
            PopMsg.Append(Current.Text);
            string Temp;
            if (this.m_Caption != string.Empty)
            {
                
                PopMsg.AppendFormat("{0}Caption :{1}", System.Environment.NewLine,this.m_Caption);
            }
            if (this.m_Range != string.Empty)
            {
                PopMsg.AppendFormat("{0}Range :{1}", System.Environment.NewLine, this.m_Range);
            }
            if (this.m_Type != string.Empty)
            { 
                     PopMsg.AppendFormat("{0}Type :{1}", System.Environment.NewLine, this.m_Type);
            }
            m_Popup.Items.Add(PopMsg.ToString());
            Point PopLocation = new Point(MousePosition.X-10, MousePosition.Y+16);
            m_Popup.Show(PopLocation);
        }
        private void LabelShowFullName_MouseLeave(object sender, EventArgs e)
        {
            m_Popup.Items.Clear();
            m_Popup.Close();
        }
        public void MakeLineInaccessible()
        {
            this.TextBoxVal.Enabled = false;
            ////this.checkBoxRemark.Checked = true;

        }
        public void InitializeNewLine(string LabelName,string Value, string Range, string Type, string Caption, int NumberLine)
        {
            //Label Name
            this.LabelName.Text = LabelName;

            //Label Equal

            //Text box Val
            this.TextBoxVal.Text = Value;
            
            //this
            m_NumberLine = NumberLine;
            m_Range = Range;
            m_Type = Type;
            m_Caption = Caption;
        }

       
    }
}
