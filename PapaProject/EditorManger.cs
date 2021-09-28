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
       
        public void StartProgram() 
        {
            m_PropertyForm = new PropertyFrom();
            m_PropertyForm.ShowDialog();

           m_PropertyForm.printethetabletoconsole();
        }
      



    }
}
