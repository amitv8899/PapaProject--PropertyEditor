using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaProject
{
     class  Table
     {
        private StringBuilder[,] m_ProperitiesTable;
        private List<string> m_ListOfSystemRemarkForLabel = new List<string> (new string[] {string.Empty, string.Empty, string.Empty });
        private int m_NumberOfLines;
        private int m_NumberOfColunms = 6;
        private int m_CurrentLine = 0;
        private bool m_PartOfPropertyVal = false;
     


        public int IntinitializeNewTableFromFile(int LinesFromFile)
        {
            int res = 1;
            m_NumberOfLines = LinesFromFile;
            if (m_NumberOfLines != 0) 
            {
                m_ProperitiesTable = new StringBuilder[m_NumberOfLines+1, m_NumberOfColunms];

                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Type]=new StringBuilder(eColumns.Type.ToString());// remark or spacese
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Label]=new StringBuilder(eColumns.Label.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Value]= new StringBuilder(eColumns.Value.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Range]= new StringBuilder(eColumns.Range.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.FieldType]= new StringBuilder(eColumns.FieldType.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Note]= new StringBuilder(eColumns.Note.ToString());
                m_CurrentLine++;
                res = 0;
            }

            for (int i = 1; i <= m_NumberOfLines; i++) // Intinitialize empty table
            {
                for (int j = 0; j < m_NumberOfColunms; j++)
                {
                    m_ProperitiesTable[i, j] = new StringBuilder(string.Empty);
                }
            }

            return res;
        }
        public bool IsTableEmpty() 
        {
            bool ToReturn = true;
            if (m_ProperitiesTable != null )
            {
                if (m_ProperitiesTable[1, (int)eColumns.Type].ToString() != string.Empty)
                {

                    ToReturn = false;
                }
            }
            return ToReturn;
        }
        private StringBuilder MakeSpaces(int LenghtStr, string Str, int Max)
        {
            StringBuilder res = new StringBuilder();

            if (LenghtStr > Max)
            {
                res.AppendFormat("{0}...", Str.Substring(0, Max - 3));
            }
            else
            {
                res.Append(Str);
                res.Insert(LenghtStr, " ", Max - LenghtStr);
            }

            res.Append("|");
            return res;
        }
        public void PrintTable()
        {
            StringBuilder Table = new StringBuilder();
            for (int i = 0; i <= m_NumberOfLines; i++)
            {
                if(m_ProperitiesTable != null)
                {
                    if (m_ProperitiesTable[i, 0].ToString() != string.Empty)
                    {
                        for (int j = 0; j < m_NumberOfColunms; j++)
                        {
                            Table.Append(MakeSpaces(m_ProperitiesTable[i, j].Length, m_ProperitiesTable[i, j].ToString(), 15));
                        }
                        Table.Append(System.Environment.NewLine);
                    }
                    else
                        break;
                }
               
              

            }
            Console.WriteLine(Table);
        }
        private bool CheckIsLineRemark(string Line)
        {
           
            bool res = false;
            if (Line != string.Empty && Line != "")// this line is remark 
            {
                string FirstCharInStr = Line[0].ToString();
                if (FirstCharInStr == Constants.Remark1 || FirstCharInStr == Constants.Remark2)// Symbol of remark or System Remark in the file!
                {
                    if (!IsLabelRemark(Line) && !IsSystemRemark(Line)) 
                    {
                        res = true;
                    }
                }
            }
            return res;
        }
        private bool CheckIsLineAllSpaceses(string Line)
        {
            bool res = true;

            foreach(char ch in Line)
            {
                if (ch != ' ') 
                {
                    res = false;
                    break;
                }
            }
            if (Line == string.Empty)
            {
                res = true;
            }
            return res;
        }
        private bool CheckIsLineContainEqual(string Line, bool PartOfPropertyVal)
        {
            bool res = false;

            if (PartOfPropertyVal == false)// shoule countain =
            {
                foreach (char ch in Line) 
                {
                    if (ch == '=' && Line[0] != '=')// the line contaim =  and it is not the first char
                    {
                        res = true;
                        break; // no need to run until the end
                    }
                }
            }

            return res;
        }
        private int InsertNewRemark(string Line, int Row)
        {
            eTypes TypeLine = eTypes.Remark;
            ChangeTypeInTable(Row, TypeLine.ToString());
            ChangeLabelInTable(Row, Line);
            ChangeValueInTable(Row, string.Empty);
            ChangeRngeInTable(Row, string.Empty);
            ChangeFeildTypeInTable(Row, string.Empty);
            ChangeNoteInTable(Row, string.Empty);

            m_CurrentLine++;// be ready to next insert
            return 0;
        }
        private int InsertNewSystemRemark(string Line, int Row)
        {
            eTypes TypeLine = eTypes.SystemRemark;
            ChangeTypeInTable(Row, TypeLine.ToString());
            ChangeLabelInTable(Row, Line);
            ChangeValueInTable(Row, GetValFromSystemRemark(Line));
            ChangeRngeInTable(Row, string.Empty);
            ChangeFeildTypeInTable(Row, string.Empty);
            ChangeNoteInTable(Row, string.Empty);

            m_CurrentLine++;// be ready to next insert
            return 0;
        }
        private string GetValFromSystemRemark(string Line)
        {
            StringBuilder ValRes = new StringBuilder(string.Empty); ;
            bool NeedCopy = false;

            foreach(char ch in Line)
            {
                if (ch == ')') // before we copy ) we need to stop 
                {
                    NeedCopy = false;
                    break;
                }
                if (NeedCopy == true)
                {
                    ValRes.Append(ch);
                }
                if (ch == '(') // need to copy the next ch 
                {
                    NeedCopy = true;
                }
                 
            }
            
            return ValRes.ToString();
        }
        private int InsertNewSpacese(string Line, int Row)
        {
            ChangeTypeInTable(Row, eTypes.Spaces.ToString());
            ChangeLabelInTable(Row, string.Empty);
            ChangeValueInTable(Row, Line);//empty string = 0 spaces
            ChangeRngeInTable(Row, string.Empty);
            ChangeFeildTypeInTable(Row, string.Empty);
            ChangeNoteInTable(Row, string.Empty);

            m_CurrentLine++;// be ready to next insert
            return 0;
        }
        private int InsertNewLabel(string Line,int row)
        {
            
            string LabelRemarkOrNull = IsLineLabelRamark(Line);
            eTypes TypeLine = eTypes.Label;
            if (LabelRemarkOrNull != null)// this line is not label and its remark label 
            {
                TypeLine = eTypes.LabelRemark;
            }
            else
                LabelRemarkOrNull = Line;// LabelRemarkOrNull is null so now is not
            ChangeTypeInTable(row, TypeLine.ToString());
            ChangeLabelInTable(row, GetLabelFromLine(LabelRemarkOrNull, out int EqualIndex));
            if (EqualIndex == 0) 
            {
                return 1;// no valid Line!, need to exit this funcation! send error!
            }
            m_PartOfPropertyVal = CheckIsPartOfPropertyVal(LabelRemarkOrNull);
            LabelRemarkOrNull = LabelRemarkOrNull.Substring(EqualIndex + 1);// get the rest of the line without the label 

            FillLabelRemark(out List<StringBuilder> ListOfRemark);
            if (m_PartOfPropertyVal == false)
            {
                m_CurrentLine++;// be ready to next insert (line)
            }
            ChangeValueInTable(row, LabelRemarkOrNull);// get val without = .
            ChangeRngeInTable(row, ListOfRemark[(int)eSystemRemark.FiledRange].ToString());
            ChangeFeildTypeInTable(row, ListOfRemark[(int)eSystemRemark.FieldType].ToString());
            ChangeNoteInTable(row, ListOfRemark[(int)eSystemRemark.Caption].ToString());

          
            return 0;
        }
        private string IsLineLabelRamark(string Line)// if true this function return new line without labelRemaek if false return null
        {
            StringBuilder First4char = new StringBuilder();
            
            if (IsLabelRemark(Line))
                return Line.Remove(0,1);// remove # or !
            else
                return null;
        }
        private void FillLabelRemark(out List<StringBuilder> ListOfRemark)// infinite loop !!!!!
        {
            ListOfRemark = new List<StringBuilder>();//[0]= Range, [1]= tyep, [2] = caption and others
            ListOfRemark.Add(new StringBuilder(string.Empty));
            ListOfRemark.Add(new StringBuilder(string.Empty));
            ListOfRemark.Add(new StringBuilder(string.Empty));
            int CounterUp = m_CurrentLine-1;
            bool KeepGoing = true;
            int Index = -1;
            while (KeepGoing)
            {
                if (CounterUp == 0 || m_ProperitiesTable[CounterUp, (int)eColumns.Type].ToString() != eTypes.SystemRemark.ToString())
                {
                    KeepGoing = false;
                    break;
                }
               

                Index = CheckIndexOfStrAndReturn(m_ProperitiesTable[CounterUp, (int)eColumns.Label].ToString());
                if(Index != -1)
                {
                    ListOfRemark[Index] =new StringBuilder(m_ProperitiesTable[CounterUp, (int)eColumns.Value].ToString());
                }
                //else send problem!

                CounterUp--;
            }
        }
        private bool IsSystemRemark(string Remark)
        {
            return Remark.ToString().Contains(Constants.SystemRemark);// system remark!!!
        }
        private bool IsLabelRemark(string Remark)
        {
            return (Remark.ToString().Contains(Constants.Remark1)|| Remark.ToString().Contains(Constants.Remark2)) && Remark.ToString().Contains("=");// Label remark!!!
        }
        private int CheckIndexOfStrAndReturn(string StrToCheck)
        {
            int res=-1;
            bool StartCopy = false;
            
            if (StrToCheck.StartsWith(Constants.SystemFieldRange)) 
            {
                res = (int)eSystemRemark.FiledRange;
            }
            else if (StrToCheck.StartsWith(Constants.SystemFieldType))
            {
                res = (int)eSystemRemark.FieldType;
            }
            else if (StrToCheck.StartsWith(Constants.SystemCaption)) 
            {
                res = (int)eSystemRemark.Caption;
            }
            
            return res;
        }
        private string GetLabelFromLine(string Line, out int EqualIndex) // isolate label from Line
        {
            EqualIndex=0;
            StringBuilder CurrentLabel = new StringBuilder();
            foreach(char ch in Line)
            {
                if (ch != '=' && ch != ':')
                {
                    CurrentLabel.Append(ch);
                    EqualIndex++;
                }
                else
                {
                    break;
                }
            }
            if (EqualIndex == Line.Length)// There is no = or : in the line need to send error! 
            {
                EqualIndex = 0;// 0 = not valid index for Line , not valid line in the text
            }
            return CurrentLabel.ToString();
        }
        private bool CheckIsPartOfPropertyVal(string Line)
        {
          return (Line[(Line.Length) - 1] == '\\') && (Line[(Line.Length) - 2] != '\\');
        }
        private int InsertGoOnProperty(string Line, int Row)
        {
            int res = 0;
                if (!CheckIsPartOfPropertyVal( Line)) 
                 m_PartOfPropertyVal = false;//the last val of this label

            m_ProperitiesTable[Row, (int)eColumns.Value].Append(Line);
            if (!m_PartOfPropertyVal) 
            {
                m_CurrentLine++; 
            }

            return res;
        }
        public  int InsertLineToTable(string Line)
        {
            bool NeedNextCheck = true;
            int res = 0;
            if (m_PartOfPropertyVal == true) 
            {
                res = InsertGoOnProperty(Line,m_CurrentLine);
                NeedNextCheck = false;
            }
            if (NeedNextCheck && CheckIsLineRemark(Line))
            {
               res = InsertNewRemark(Line,m_CurrentLine);
                NeedNextCheck = false;
                // insert as remark
            }
            if (IsSystemRemark(Line) && NeedNextCheck)
            {
                res = InsertNewSystemRemark(Line, m_CurrentLine);
                NeedNextCheck = false;
            }
            if (NeedNextCheck && CheckIsLineAllSpaceses(Line))
            {
                res = InsertNewSpacese(Line, m_CurrentLine);
                NeedNextCheck = false;
                // insret as spaces
            }
            if (NeedNextCheck) 
            {
                res = InsertNewLabel(Line,m_CurrentLine);// handle both label and labelremark
                // insert per col
            }
            return res;
        }
        public int ChangeTypeInTable(int Row, string NewType)
        {
            m_ProperitiesTable[Row, (int)eColumns.Type] = new StringBuilder(NewType);
            return 0;
        }
        public string GetTypeFronTable(int Row)
        {
            return m_ProperitiesTable[Row, (int)eColumns.Type].ToString();
        }
        public int ChangeLabelInTable(int Row, string NewLabel)
        {
            m_ProperitiesTable[Row, (int)eColumns.Label] = new StringBuilder(NewLabel.Trim());
            return 0;
        }
        public string GetLabelFronTable(int Row)
        {
            return m_ProperitiesTable[Row, (int)eColumns.Label].ToString();
        }
        public string GetValueFronTable(int Row)
        {
            return m_ProperitiesTable[Row, (int)eColumns.Value].ToString();
        }
        public int ChangeRngeInTable(int Row, string NewFieldType)
        {
            m_ProperitiesTable[Row, (int)eColumns.Range] = new StringBuilder(NewFieldType);
            return 1;
        }
        public int ChangeFeildTypeInTable(int Row, string NewLowerRange)
        {
         
            m_ProperitiesTable[Row, (int)eColumns.FieldType] = new StringBuilder(NewLowerRange);

            return 1;
        }
        public string GetRangeFronTable(int Row)
        {

            StringBuilder ResRange = new StringBuilder();
            ResRange.Append(m_ProperitiesTable[Row, (int)eColumns.Range]);
            ResRange.Append(" to ");
            ResRange.Append(m_ProperitiesTable[Row, (int)eColumns.FieldType]);


            return ResRange.ToString();
        }
        public int ChangeNoteInTable(int Row, string NewVNote)
        {
            m_ProperitiesTable[Row, (int)eColumns.Note] = new StringBuilder(NewVNote);
            return 0;
        }
        public string GetNoteFronTable(int Row)
        {

            return m_ProperitiesTable[Row, (int)eColumns.Note].ToString();
        }
        public int CurrentLine
        { 
            get { return this.m_CurrentLine; }
            
        }
        public int NumberOfColunms
       {
            get { return this.m_NumberOfColunms; }
           
       }
        public StringBuilder[] LineTable(int LineNumber)
        {
            
            StringBuilder[] LineToReturn = new StringBuilder[m_NumberOfColunms];
            if (LineNumber >= m_CurrentLine || LineNumber == 0)
            {
                return null;
            }

            for (int i = 0; i < m_NumberOfColunms; i++)
            {
                LineToReturn[i] = this.m_ProperitiesTable[LineNumber, i];

            }
            return LineToReturn;    
        }
        public string LineTableInOneStr(int LineNumber)
        {
            StringBuilder LineToReturn = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            

            if (LineNumber >= m_CurrentLine || LineNumber <= 0 || m_ProperitiesTable[LineNumber, (int)eColumns.Type].ToString()== string.Empty)
            {
                return null;
            }
            if (m_ProperitiesTable[LineNumber, (int)eColumns.Type].ToString() == eTypes.LabelRemark.ToString())
            {

                LineToReturn.Append(Constants.Remark1);


            }
            LineToReturn.Append(m_ProperitiesTable[LineNumber, (int)eColumns.Label]);
            if ((m_ProperitiesTable[LineNumber, (int)eColumns.Label].ToString() == eTypes.LabelRemark.ToString()) || (m_ProperitiesTable[LineNumber, (int)eColumns.Type].ToString() == eTypes.Label.ToString()))// need to add =
            {
                LineToReturn.Append("=");
                int NumberIndex = 0;
                foreach (char ch in m_ProperitiesTable[LineNumber, (int)eColumns.Value].ToString())
                {

                    temp.Append(ch);

                    if (ch == '\\')
                    {
                        if (IsLinePartOfPropertyVal(NumberIndex, m_ProperitiesTable[LineNumber, (int)eColumns.Value].ToString()))
                        {
                            temp.Append(System.Environment.NewLine);
                        }


                    }
                    NumberIndex++;
                }
                LineToReturn.Append(temp);

            }



            return LineToReturn.ToString();
        }
        private bool IsLinePartOfPropertyVal(int IndexToCheck, string StrToCheck)
        {
            bool ToReturn = false;

            if (StrToCheck[IndexToCheck + 1] != '\\' && StrToCheck[IndexToCheck - 1] != '\\')
            {
                ToReturn = true;
            }
            return ToReturn;
        }

        //// Function Property From
        public int ChangeValueInTable(int Row, string NewVal)
        {

            m_ProperitiesTable[Row, (int)eColumns.Value] = new StringBuilder(NewVal.Trim());

            return 0;
        }
        public int MakeLabelRemark(int Row, bool IsChecked)
        {
            StringBuilder NewLabel = new StringBuilder();
            if (IsChecked == true) 
            {
                m_ProperitiesTable[Row, (int)eColumns.Type] = new StringBuilder(eTypes.LabelRemark.ToString());
              
            }
            else// IsChecked == false -> make it back to label
            {
                m_ProperitiesTable[Row, (int)eColumns.Type] = new StringBuilder(eTypes.Label.ToString());
               
            }
            return 0;
        }
}
}
