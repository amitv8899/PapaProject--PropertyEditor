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
        private List<int> m_ListOfTypes = new List<int> (new int[] {0,0,0,0,0});//0=remark,1=systemremark,2=labelremark,3=spaces,4=label
        private int m_NumberOfLines;
        private int m_NumberOfColunms = 6;
        private int m_CurrentLine;
        private bool m_PartOfPropertyVal = false;
     

        /// Genral============================================================
        public int IntinitializeNewTableFromFile(int LinesFromFile)
        {
            m_CurrentLine = 0;
            int res = 1;
            m_NumberOfLines = LinesFromFile;
            if (m_NumberOfLines != 0) 
            {
                m_ProperitiesTable = new StringBuilder[m_NumberOfLines+1, m_NumberOfColunms];

                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Type]=new StringBuilder(eColumns.Type.ToString());// remark or spacese
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Label] = new StringBuilder(eColumns.Label.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Value] = new StringBuilder(eColumns.Value.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Range] = new StringBuilder(eColumns.Range.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.FieldType] = new StringBuilder(eColumns.FieldType.ToString());
                m_ProperitiesTable[m_CurrentLine, (int)eColumns.Note] = new StringBuilder(eColumns.Note.ToString());
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


            if (LineNumber >= m_CurrentLine || LineNumber <= 0 || m_ProperitiesTable[LineNumber, (int)eColumns.Type].ToString() == string.Empty)
            {
                return null;
            }
            LineToReturn.Append(m_ProperitiesTable[LineNumber, (int)eColumns.Label]);
            if ((m_ProperitiesTable[LineNumber, (int)eColumns.Type].ToString() == eTypes.LabelRemark.ToString()) || (m_ProperitiesTable[LineNumber, (int)eColumns.Type].ToString() == eTypes.Label.ToString()))// need to add =
            {
                LineToReturn.Append("=");
                int NumberIndex = 0;
                foreach (char ch in m_ProperitiesTable[LineNumber, (int)eColumns.Value].ToString())
                {

                    temp.Append(ch);

                    if (ch == '\\')// need to add new line in the file 
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
        ///=========================================================================
        ///check====================================================================
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
        private string IsLineLabelRamark(string Line)// if true this function return new line without labelRemaek if false return null
        {
            StringBuilder First4char = new StringBuilder();

            if (IsLabelRemark(Line))
                return Line.Remove(0, 1);// remove # or !
            else
                return null;
        }
        private bool IsSystemRemark(string Remark)
        {
            return Remark.ToString().Contains(Constants.SystemRemark);// system remark!!!
        }
        private bool IsLabelRemark(string Remark)
        {
            return (Remark.ToString().Contains(Constants.Remark1) || Remark.ToString().Contains(Constants.Remark2)) && (Remark.ToString().Contains("=") || m_PartOfPropertyVal);// Label remark!!!
        }
        private int CheckIndexOfStrAndReturn(string StrToCheck)
        {
            int res = -1;


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
        private bool CheckIsPartOfPropertyVal(string Line)
        {
            return (Line[(Line.Length) - 1] == '\\') && (Line[(Line.Length) - 2] != '\\');
        }
        private void FillLabelRemark(out List<StringBuilder> ListOfRemark)
        {
            ListOfRemark = new List<StringBuilder>();//[0]= Range, [1]= tyep, [2] = caption and others
            ListOfRemark.Add(new StringBuilder(string.Empty));
            ListOfRemark.Add(new StringBuilder(string.Empty));
            ListOfRemark.Add(new StringBuilder(string.Empty));
            int CounterUp = m_CurrentLine - 1;
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
                if (Index != -1)
                {
                    ListOfRemark[Index] = new StringBuilder(m_ProperitiesTable[CounterUp, (int)eColumns.Value].ToString());
                }
                //else send problem!

                CounterUp--;
            }
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
        ///===================================================================
        /// Get =================================================================
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
        private eTypes GetLineType(string Line)
        {
            eTypes res;
            if (CheckIsLineRemark(Line))
            {
                res = eTypes.Remark;
            }
            else if (IsSystemRemark(Line))
            {
                res = eTypes.SystemRemark;
            }
            else if (CheckIsLineAllSpaceses(Line))
            {
                res = eTypes.Spaces;
            }
            else if (IsLabelRemark(Line))
            {
                res = eTypes.LabelRemark;
            }
            else
            {
                res = eTypes.Label;
            }
            return res;

        }
        public string GetTypeFronTable(int Row)
        {
            return m_ProperitiesTable[Row, (int)eColumns.Type].ToString();
        }
        public string GetLabelFronTable(int Row)
        {
            return m_ProperitiesTable[Row, (int)eColumns.Label].ToString();
        }
        public string GetValueFronTable(int Row)
        {
            return m_ProperitiesTable[Row, (int)eColumns.Value].ToString();
        }
        public string GetRangeFronTable(int Row)
        {

            StringBuilder ResRange = new StringBuilder();
            ResRange.Append(m_ProperitiesTable[Row, (int)eColumns.Range]);
            ResRange.Append(" to ");
            ResRange.Append(m_ProperitiesTable[Row, (int)eColumns.FieldType]);


            return ResRange.ToString();
        }
        public string GetNoteFronTable(int Row)
        {

            return m_ProperitiesTable[Row, (int)eColumns.Note].ToString();
        }
        ///===========================================================================
        /// insert new ====================================================================
        private int InsertGoOnProperty(string Line, int Row)
        {
            int res = 0;
                if (!CheckIsPartOfPropertyVal(Line)) 
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
            int res = 0;

            if (m_PartOfPropertyVal == true)// this line is the go on val of the last label that had been insrted 
            {
                res = InsertGoOnProperty(Line, m_CurrentLine);
            }
            else // if line is not go on val of label need to find the type and insert
            {
                eTypes LineType = GetLineType(Line);

                switch (LineType)
                {
                    case eTypes.Label:
                        InsertNewLabel(Line, m_CurrentLine);
                        break;
                    case eTypes.LabelRemark:
                        InsertNewLabelRemark(Line, m_CurrentLine);
                        break;
                    case eTypes.Remark:
                        res = InsertNewRemark(Line, m_CurrentLine);
                        break;
                    case eTypes.SystemRemark:
                        res = InsertNewSystemRemark(Line, m_CurrentLine);
                        break;
                    case eTypes.Spaces:
                        res = InsertNewSpacese(Line, m_CurrentLine);
                        break;
                    default:
                        break;
                }
            }
            return res;
        }
        private int InsertNewLabel(string Line, int row)
        {
            ChangeTypeInTable(row, eTypes.Label.ToString());
            ChangeLabelInTable(row, GetLabelFromLine(Line, out int EqualIndex));
            if (EqualIndex == 0)
            {
                return 1;// no valid Line!, need to exit this funcation! send error!
            }
            m_PartOfPropertyVal = CheckIsPartOfPropertyVal(Line);
            Line = Line.Substring(EqualIndex + 1);// get the rest of the line without the label 

            FillLabelRemark(out List<StringBuilder> ListOfRemark);
            if (m_PartOfPropertyVal == false)
            {
                m_CurrentLine++;// be ready to next insert (line)
            }
            ChangeValueInTable(row, Line);// get val without = .
            ChangeRngeInTable(row, ListOfRemark[(int)eSystemRemark.FiledRange].ToString());
            ChangeFeildTypeInTable(row, ListOfRemark[(int)eSystemRemark.FieldType].ToString());
            ChangeNoteInTable(row, ListOfRemark[(int)eSystemRemark.Caption].ToString());
            m_ListOfTypes[(int)eTypes.Label]++;

            return 0;
        }
        private int InsertNewSpacese(string Line, int Row)
        {
            ChangeTypeInTable(Row, eTypes.Spaces.ToString());
            ChangeLabelInTable(Row, string.Empty);
            ChangeValueInTable(Row, Line);//empty string = 0 spaces
            ChangeRngeInTable(Row, string.Empty);
            ChangeFeildTypeInTable(Row, string.Empty);
            ChangeNoteInTable(Row, string.Empty);
            m_ListOfTypes[(int)eTypes.Spaces]++;
            m_CurrentLine++;// be ready to next insert
            return 0;
        }
        private int InsertNewLabelRemark(string Line, int row)
        {
            int res = 0;
            ChangeTypeInTable(row, eTypes.LabelRemark.ToString());
            ChangeLabelInTable(row, GetLabelFromLine(Line, out int EqualIndex));
            if (EqualIndex == 0)
            {
                return 1;// no valid Line!, need to exit this funcation! send error!
            }
            m_PartOfPropertyVal = CheckIsPartOfPropertyVal(Line);
            Line = Line.Substring(EqualIndex + 1);// get the rest of the line without the label 
            FillLabelRemark(out List<StringBuilder> ListOfRemark);
            if (m_PartOfPropertyVal == false)
            {
                m_CurrentLine++;// be ready to next insert (line)
            }
            ChangeValueInTable(row, Line);// get val without = .
            ChangeRngeInTable(row, ListOfRemark[(int)eSystemRemark.FiledRange].ToString());
            ChangeFeildTypeInTable(row, ListOfRemark[(int)eSystemRemark.FieldType].ToString());
            ChangeNoteInTable(row, ListOfRemark[(int)eSystemRemark.Caption].ToString());
            m_ListOfTypes[(int)eTypes.LabelRemark]++;


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
            m_ListOfTypes[(int)eTypes.Remark]++;
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
            m_ListOfTypes[(int)eTypes.SystemRemark]++;
            m_CurrentLine++;// be ready to next insert
            return 0;
        }
        ///================================================================================
        /// Change in table =================================================
        public int ChangeTypeInTable(int Row, string NewType)
        {
            m_ProperitiesTable[Row, (int)eColumns.Type] = new StringBuilder(NewType);
            return 0;
        }
        public int ChangeLabelInTable(int Row, string NewLabel)
        {
            m_ProperitiesTable[Row, (int)eColumns.Label] = new StringBuilder(NewLabel.Trim());
            return 0;
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
        public int ChangeNoteInTable(int Row, string NewVNote)
        {
            m_ProperitiesTable[Row, (int)eColumns.Note] = new StringBuilder(NewVNote);
            return 0;
        }
       ///=================================================================================
       //// Function Property From ===============================================
        public int ChangeValueInTable(int Row, string NewVal)
        {

            m_ProperitiesTable[Row, (int)eColumns.Value] = new StringBuilder(NewVal.Trim());

            return 0;
        }
        public int MakeLabelRemark(int Row, bool IsChecked, string LabelName, string LabelVal)
        {
            StringBuilder NewLabel = new StringBuilder();
            if (IsChecked) 
            {
                m_ProperitiesTable[Row, (int)eColumns.Type] = new StringBuilder(eTypes.LabelRemark.ToString());
                m_ProperitiesTable[Row, (int)eColumns.Label] = new StringBuilder(LabelName);
                m_ProperitiesTable[Row, (int)eColumns.Value] = new StringBuilder(LabelVal);
                m_ListOfTypes[(int)eTypes.Label]--;
                m_ListOfTypes[(int)eTypes.LabelRemark]++;


            }
            else// IsChecked == false -> make it back to label
            {
                m_ProperitiesTable[Row, (int)eColumns.Type] = new StringBuilder(eTypes.Label.ToString());
                m_ProperitiesTable[Row, (int)eColumns.Label] = new StringBuilder(LabelName);
                m_ProperitiesTable[Row, (int)eColumns.Value] = new StringBuilder(LabelVal);
                m_ListOfTypes[(int)eTypes.Label]++;
                m_ListOfTypes[(int)eTypes.LabelRemark]--;

            }
            return 0;
        }
        ///=================================================================================
        /////// get and set====================================================================
        public int CurrentLine
        {
            get { return this.m_CurrentLine; }

        }
        public int NumberOfColunms
        {
            get { return this.m_NumberOfColunms; }

        }
        public int NumberOfLabelRemark
        {
            get { return m_ListOfTypes[(int)eTypes.LabelRemark]; }
        }
        public int NumberOfLabel
        {
            get { return m_ListOfTypes[(int)eTypes.Label]; }
        }
    }
}
