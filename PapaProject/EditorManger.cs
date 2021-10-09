using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaProject
{
    class EditorManger
    {
        private PropertyFrom m_PropertyForm;
       
        public void StartProgram(string[] args) 
        {
            string argsStr = string.Empty;
            if (args.Length < 2) 
            {
                if (args.Length == 1)
                {
                    argsStr = args[0];
                }
                // else send empty string

                m_PropertyForm = new PropertyFrom(argsStr);
                m_PropertyForm.ShowDialog();
                m_PropertyForm.printethetabletoconsole();
            }
            else
            {
                Console.WriteLine("To much Args");
            }
        }
      



    }
}
