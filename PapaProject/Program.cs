using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PapaProject
{
    public  class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            EditorManger program = new EditorManger();
            program.StartProgram(args);
          
        }


    }
}
