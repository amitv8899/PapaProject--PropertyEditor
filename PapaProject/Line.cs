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
        private Control m_Input;
        public event Action<int, string> TaxBoxValChangedInvoker;
        public event Action<int, bool, string, string> CheckBoxRemarkChangeInvoker;
        public event Action<Line, bool> LineSelectedInvoker;

       
        public Line(string LabelName, string Value, string Range, string Type, string Caption, int NumberLine)
        {
            InitializeComponent();
            //Label Name
            this.LabelName.Text = LabelName;
            
            //Label Equal
            

            //this
            m_NumberLine = NumberLine;
            m_Range = Range;
            m_Type = Type;
            m_Caption = Caption;

            // Input
            CreateInputControl();
            this.m_Input.Text = Value;
            this.Controls.Add(this.m_Input);
            this.flowLayoutPanel.Controls.Add(this.m_Input);


        }
        private void  CreateInputControl()
        {

            if (m_Type == "bool")
            {
                
                ComboBox temp = new System.Windows.Forms.ComboBox();
                temp.FlatStyle = FlatStyle.Popup;
                temp.Items.Add("True");
                temp.Items.Add("False");
                m_Input = temp;


            }
            else if (m_Type == "int" && m_Range != string.Empty)
            {
                NumericUpDown temp = new System.Windows.Forms.NumericUpDown();
                temp.BorderStyle = BorderStyle.FixedSingle;
                
                GetMinAndMaxFormString(m_Range, out int Min, out int Max);
                temp.Maximum = Max;
                temp.Minimum = Min;
                m_Input = temp;

            }
            else// string or doenot have type
            {
                RichTextBox temp = new System.Windows.Forms.RichTextBox();
                temp.Multiline = true;
                m_Input = temp;
                //DefaultInput temp = new DefaultInput();
                //m_Input = temp;
            }
        }
        private int GetMinAndMaxFormString(string Range, out int Min, out int Max)
        {
            int res = 0;
            bool curIsMin = true;
            StringBuilder MinStr = new StringBuilder();
            StringBuilder MaxStr = new StringBuilder();


            foreach (char ch in Range)
            {

                if (curIsMin)
                {
                    if (ch != ',') 
                    {
                        MinStr.Append(ch);
                    }
                }
                else
                {
                    MaxStr.Append(ch);


                }
                if (ch == ',')
                {
                    curIsMin = false;

                }
            }

            if (!Int32.TryParse(MinStr.ToString(), out Min))
            {
                Min = 0;
            }
            if (!Int32.TryParse(MaxStr.ToString(), out Max))
            {
                Max = 0;
            }
            
            return res;      
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
           
            // Input
            this.m_Input.Location = new Point(LabelEqual.Right, LabelName.Top);
            this.m_Input.TextChanged += new EventHandler(TextBoxVal_Changed);
            this.m_Input.Size = new Size(182,31);

           

            // ContextMenu
            this.materialContextMenuLine.Opened += new EventHandler(MaterialContextMenuLine_Clicked);
            this.materialContextMenuLine.Opened += new EventHandler(Line_Clicked);
            //RemarkContextMenue
            this.remarkToolStroMenuItem.Click += new EventHandler(RemarkToolStroMenuItem_Clicked);
           


            //this
            this.Size = new Size(LabelName.Size.Width + LabelEqual.Size.Width + m_Input.Size.Width, m_Input.Size.Height);
           

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
            this.m_Input.BackColor = Color.LightYellow;
            this.BackColor = Color.LightYellow;

        }
        public void LineUnSelected()
        {
            this.LabelName.BackColor = Color.Empty;
            this.LabelEqual.BackColor = Color.Empty;
            this.m_Input.BackColor = Color.Empty;
            this.BackColor = Color.Empty;
           
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
            if (this.m_Input.Enabled == false)
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
                this.m_Input.Enabled = false;
                
               
            }
            else
            {
                this.m_Input.Enabled = true;
                
               
                IsBecameRemark = false;
            }

            OnChangedRemarkClicked(IsBecameRemark);
        }
        protected virtual void OnChangedRemarkClicked(bool IsChecked)
        {
            if (CheckBoxRemarkChangeInvoker != null)
            {
                this.CheckBoxRemarkChangeInvoker.Invoke(m_NumberLine, IsChecked, this.LabelName.Text,this.m_Input.Text);
            }
        }
        
        private void TextBoxVal_Changed(object sender, EventArgs e)
        {
            Control CurrentVal = (Control)sender;
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
            this.m_Input.Enabled = false;
        }
       

       
    }
}
