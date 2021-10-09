using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace PapaProject
{
    public partial class PropertyFrom : Form
    {
        private List<Line> m_LineArray;
        private ReadFromFile m_Newfile;
        private Table m_PropertiesTable;
        private bool m_ChangeHadBeenMade = false;
        private bool m_NeedShowLabelRemark = false;
        private Line m_CurLineSelected = null;// if null no line is selected

       // readonly MaterialSkin.MaterialSkinManager r_MaterialSkinManager;

        public PropertyFrom(string FileNameAsArgs)
        {
            
            InitializeComponent();
            // allocate 
            m_Newfile = new ReadFromFile();
            m_PropertiesTable = new Table();
            this.Load += new System.EventHandler(this.Form_Load);
            m_Newfile.FilePath = FileNameAsArgs;


            //r_MaterialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            //r_MaterialSkinManager.EnforceBackcolorOnAllComponents = true;
            //r_MaterialSkinManager.AddFormToManage(this);
            //this.ControlBox = true;

        }
        private void Form_Load(object sender, EventArgs e)
        {
            // create table 
            CreateTableFromFile();
            //this
            this.FormClosing += new FormClosingEventHandler(FormClosed_Clicked);
            this.openToolStripMenuItem.Click += new EventHandler(OpenToolStripMenuItem_Clicked);
            // if (name of file is not empty or null )
            CreateLine();
        }
        private void OpenToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
           this.openFileDialog = new OpenFileDialog();
           if( this.openFileDialog.ShowDialog() == DialogResult.OK)
           {
                m_Newfile.FilePath = this.openFileDialog.FileName;
                if (m_LineArray.Count != 0)
                {
                    m_LineArray.Clear();
                    FlowAllLine.Controls.Clear();
                }
                CreateTableFromFile();
                CreateLine();
            }



        }
        private void CreateLine() 
        {
            if (InitializeAllLine() == 0 && m_Newfile.FilePath != string.Empty)
            {
                this.ClientSize = new Size(m_LineArray.Last<Line>().Size.Width + 20, 200);
                this.MinimumSize = new Size(m_LineArray.Last<Line>().Size.Width + 40, FlowAllLine.Height);
                
            }
            else
            {
                if (m_PropertiesTable.IsTableEmpty() && m_Newfile.FilePath != string.Empty)
                {
                    MessageBox.Show("No label or Remark Label");
                    this.Close();
                }
            }
           
        }
        public void CreateTableFromFile()
        {
            string LineFromFile;
            bool FileNotOver = true;
            if (m_Newfile.SetNameFileFromUser() == 0)
            {
                m_Newfile.CoutNumberProperties(out int NumberOfLines);
                m_PropertiesTable.IntinitializeNewTableFromFile(NumberOfLines);
                int Ok = 0;
                while (FileNotOver)
                {
                    LineFromFile = m_Newfile.ReadNextLineFromFile();
                    if (LineFromFile == null)// end of file! if line == null 
                    {
                        FileNotOver = false;
                    }
                    else
                    {
                        Ok = m_PropertiesTable.InsertLineToTable(LineFromFile);
                        if (Ok == 1) // 1 == not Ok!!
                        {
                            break;
                        }
                    }
                }
                m_Newfile.CloseFile(); // close file , table is ready
            }
           
        }
        public int InitializeAllLine() // initialize form from the table to user 
        {
            m_LineArray = new List<Line>();
            StringBuilder[] NewLine;
            bool MoreLineInTable = true;
            int IndexForLine = 1;
            int ToReturn = 0;
            while (MoreLineInTable)
            {
                NewLine = m_PropertiesTable.LineTable(IndexForLine);// if table is done or is empty so break

                if (NewLine == null)
                {
                    MoreLineInTable = false;
                    break;
                }
                if ((NewLine[0].ToString() == eTypes.Label.ToString()) || (m_NeedShowLabelRemark && (NewLine[0].ToString() == eTypes.LabelRemark.ToString())))
                {
                    string LabelName = NewLine[(int)eColumns.Label].ToString();
                    string LabelVal = NewLine[(int)eColumns.Value].ToString();
                    if (NewLine[0].ToString() == eTypes.LabelRemark.ToString())// if label remark need to remove all # in line
                    {
                        LabelName = LabelName.Replace("#","");
                        LabelVal = LabelVal.Replace("#","");

                    }
                    m_LineArray.Add(new Line(LabelName, LabelVal, NewLine[(int)eColumns.Range].ToString(), NewLine[(int)eColumns.FieldType].ToString(), NewLine[(int)eColumns.Note].ToString(), IndexForLine));
                    m_LineArray.Last<Line>().TaxBoxValChangedInvoker += UpdateTable;
                    m_LineArray.Last<Line>().CheckBoxRemarkChangeInvoker += UpdateRemarkLabel;
                    m_LineArray.Last<Line>().LineSelectedInvoker += UpdateLineSelected;
                    if (NewLine[0].ToString() == eTypes.LabelRemark.ToString())
                    {
                        m_LineArray.Last<Line>().MakeLineInaccessible();
                    }
                    if (this.FlowAllLine.Controls.Count < 0)
                    {
                        this.FlowAllLine.Controls.Clear();
                    }
                    else
                    this.FlowAllLine.Controls.Add(m_LineArray.Last<Line>());

                }
                IndexForLine++;
            }
            if (m_LineArray.Count == 0 && m_Newfile.FilePath != string.Empty)
            {

                MessageBox.Show("No Label To Show!");
                ToReturn = 1;
            }
            return ToReturn;
        }
        private void FormClosed_Clicked(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = DialogResult.Yes;
            StringBuilder message = new StringBuilder();
            message.AppendFormat("Change had been made{0} Are you sure you want to close Editor?", System.Environment.NewLine);

            if (m_ChangeHadBeenMade == true) 
            {
                result = MessageBox.Show(message.ToString(), "PropertyForm", buttons);
            }
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;//close form
                m_ChangeHadBeenMade = false; 
                m_NeedShowLabelRemark = false;
            }
            else 
            {
                e.Cancel=true;
            }
        }
        private void ButtomUpdate_Clicked(object sender, EventArgs e)
        {
            // save to file
            bool TableNotOver = true;
            string LineToFile;
            int Line = 1;
            StringBuilder message = new StringBuilder("Are you sure yow want to update file?");
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = DialogResult.Yes;
            if (m_ChangeHadBeenMade == true)
            {
                result = MessageBox.Show(message.ToString(), "PropertyForm", buttons);
            }

            if (result == DialogResult.Yes)
            {
                m_ChangeHadBeenMade = false;
                m_Newfile.DuplicateOriginalFile(); // create new back up file 
                m_Newfile.CreateWriter();
                while (TableNotOver)
                {
                    LineToFile = m_PropertiesTable.LineTableInOneStr(Line);
                    if (LineToFile == null)
                        break;
                 
                    m_Newfile.WriteLineToFile(LineToFile);
                    Line++;
                }

                string Msg = "The Changes saved!";
                MessageBox.Show(Msg);// here add the name of the file !
                m_Newfile.CloseFileWriter();
                //// delete all lines and create new one 
                m_LineArray.Clear();
                FlowAllLine.Controls.Clear();
                CreateLine();
                ///
            }
            else
                MessageBox.Show("The Changes *didn't* save!");

           
        }
        private void ButtomLabelRemark_Clicked(object sender, EventArgs e)
        {
           if (m_NeedShowLabelRemark == false)
           {
               if (m_PropertiesTable.NumberOfLabelRemark != 0)   // if there zero Remark label   
               { 
                  ButtomRemarkList.Text = "Hide Remarks";
                  m_NeedShowLabelRemark = true;
                  // show all lebel include those as remark
                  // delete all line and create new ones with label remark
                   m_LineArray.Clear();
                   FlowAllLine.Controls.Clear();
                   CreateLine();
               }
               else
               {
                    ButtomRemarkList.Text = "Show Remarks";
                    MessageBox.Show("No Remark Label To Show", "PropertyForm");
               }    
           }
           else
           {
                if (m_PropertiesTable.NumberOfLabel != 0)// if there zero label and there is remark label 
                {
                    ButtomRemarkList.Text = "Show Remarks";
                    m_NeedShowLabelRemark = false;
                    // show all lebel witout those as remark
                    // delete all line and create new ones without label remark
                    m_LineArray.Clear();
                    FlowAllLine.Controls.Clear();
                    CreateLine();
                }
                else
                {
                    ButtomRemarkList.Text = "Hide Remarks";
                    MessageBox.Show("No Label To Show", "PropertyForm");
                }
             
           }
        }

        private void UpdateTable(int Line, string NewVal)
        {
            m_PropertiesTable.ChangeValueInTable(Line, NewVal);
            m_ChangeHadBeenMade = true;
        }
        private void UpdateRemarkLabel(int LineNumber, bool Ischecked, string LabelName, string LabelVal) 
        {
            StringBuilder Msg = new StringBuilder();
            if (Ischecked == true)
            {
                Msg.AppendFormat("The Label {0} became Remark, after update ie will appaer in Remark List", LabelName);
                MessageBox.Show(Msg.ToString());
               // this.m_NumberLabelRemark++;
                //this.m_NumberLabel--;
                if (m_PropertiesTable.NumberOfLabel == 0 )
                {
                   ButtomRemarkList.Text = "  Hide Remarks  ";
                }
            }
            else
            {
                Msg.AppendFormat("The Label {0} became No Remark", LabelName);
                MessageBox.Show(Msg.ToString());
                //this.m_NumberLabelRemark--;
                //this.m_NumberLabel++;
                if (m_PropertiesTable.NumberOfLabelRemark == 0)
                {
                    ButtomRemarkList.Text = "Show Remarks";
                }
            }
            if (Ischecked)//This is a remark label need to add # in the start and in every new line (go on val) (after every \ add #)
            {

                LabelName = LabelName.Insert(0,"#");// add in the start # to mark it as remark in file 
                LabelVal = LabelVal.Replace("\\","\\#");
            }
            else
            {
                LabelVal = LabelVal.Replace("#", "");// all the # need to be deleted

            }
            int res = m_PropertiesTable.MakeLabelRemark(LineNumber, Ischecked, LabelName, LabelVal);
            if (res == 0)
            {
                m_ChangeHadBeenMade = true;
             
            }

        }
        private void UpdateLineSelected(Line CurLine, bool IsSelected)
        {
            if (m_CurLineSelected != null)
            {
                m_CurLineSelected.LineUnSelected();// make the kine as unselected one
            }

            if (m_CurLineSelected != CurLine) 
            {
                CurLine.LineSelected(); // make the line selected
                m_CurLineSelected = CurLine;
            }
            else // the line that was select is noe unselected and there is no more new line that has been selected
            {
                m_CurLineSelected = null;

            }
        }


        public void printethetabletoconsole()
        {
            m_PropertiesTable.PrintTable();// printe the table to console
        }

        
    }

} 