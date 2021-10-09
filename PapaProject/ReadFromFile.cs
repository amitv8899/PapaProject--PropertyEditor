using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PapaProject
{
     public class ReadFromFile 
     {
        private StringBuilder m_FilePath;
        private StreamReader m_ReadFromFile;
        private StreamWriter m_WriterToFile;
        private StringBuilder m_FileName;//= new StringBuilder("txt1");
        private StringBuilder m_FileLocation;// = new StringBuilder(@"C:\Users\AMIT\Desktop\PapaProject\");
   
        public string FilePath
        {
            get { return m_FilePath.ToString(); } //  null if still file was not Loaded
            set { m_FilePath = new StringBuilder(value); }
        }

        public int SetNameFileFromUser()
        {
            if (m_FilePath == null || m_FilePath.ToString() == string.Empty)
                return 1;
            GetFileAndLocationFile(m_FilePath.ToString(), out string FileName, out string LocationName);
            FileName = FileName.Replace(".txt","");
            m_FileName = new StringBuilder(FileName);
            m_FileLocation = new StringBuilder(LocationName);

            m_FilePath = new StringBuilder(m_FilePath.ToString());
            m_ReadFromFile = new StreamReader(m_FilePath.ToString());
           
            return 0;
        }
        private int GetFileAndLocationFile(string NameFileFromUser,out string FileName, out string LocationName)
        {
            
            FileName = null;
            LocationName = null;

            if (NameFileFromUser == null || NameFileFromUser == string.Empty)
            {
                return 1;
            }

            int counterIndex = NameFileFromUser.Length - 1;

            for (int i = NameFileFromUser.Length - 1; i >= 0 ; i--)
            {
                if(NameFileFromUser[i]== '\\')
                {
                    break;
                }
                counterIndex--;
            }
            if (counterIndex == 0)
            {
                return 1;// problem

            }
            LocationName = NameFileFromUser.Substring(0, counterIndex+1);
            FileName = NameFileFromUser.Substring(counterIndex + 1);

            return 0;
        }


        public int CoutNumberProperties(out int NumberOfLines) 
        {
            NumberOfLines = 0;
            if (m_FilePath == null)// change to excaption
            {
                return 1;
            }
            if (File.Exists(m_FilePath.ToString())== false)// change to excaption
            {
                return 1;
            }
        
            NumberOfLines = File.ReadAllLines(m_FilePath.ToString()).Count();
            
            if (NumberOfLines == 0)// change to excaption
            {
                return 1;
            }

            return 0;
        }
        public string ReadNextLineFromFile()
        {
            return m_ReadFromFile.ReadLine();   
        }
        public void CloseFile()
        {
            m_ReadFromFile.Close();
        }
        public void CloseFileWriter()
        {
            m_WriterToFile.Close();
        }
        public void CreateWriter() 
        {
            m_WriterToFile = new StreamWriter(m_FilePath.ToString());
        }
        public void WriteLineToFile(string LineToWrite)// 
        {
            
            m_WriterToFile.WriteLine(LineToWrite);
          
        }
        public void DuplicateOriginalFile()
        {
           
            StringBuilder BackUpFilePath = new StringBuilder();
            BackUpFilePath.Append(m_FileLocation);
            BackUpFilePath.Append(@"\PropertyFilesBackUps");
            if (!Directory.Exists(BackUpFilePath.ToString()))
            {
                Directory.CreateDirectory(BackUpFilePath.ToString());
            }
            BackUpFilePath.Append(@"\");
            BackUpFilePath.Append(m_FileName);
            BackUpFilePath.Append("_");
            BackUpFilePath.Append(DateTime.Now.ToString("dd.MM.yyyy_hh-mm"));
            BackUpFilePath.Append(".txt");
            FileInfo fi = new FileInfo(m_FilePath.ToString());
            fi.CopyTo(BackUpFilePath.ToString(), true);
           
        }

     }
}
