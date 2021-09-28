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
        private StringBuilder m_FileName = new StringBuilder("txt1");
        private StringBuilder m_FileLocation = new StringBuilder(@"C:\Users\AMIT\Desktop\PapaProject\");

        public int TakeNameFileFromUser()
        { 
          
            m_FilePath = new StringBuilder(@"C:\Users\AMIT\Desktop\PapaProject\txt1.txt");
            m_ReadFromFile = new StreamReader(m_FilePath.ToString());
            if (m_FilePath == null)
                return 1;

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
            //DateTime dateTime = new DateTime();
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
