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
    public partial class DefaultInput : UserControl
    {

        public DefaultInput()
        {
            InitializeComponent();

        }
        private void comboBoxMainInput_DropDwon(object sender, EventArgs e)
        {
            this.richTextBoxExtendInput.Visible = true;
            this.richTextBoxExtendInput.Focus();
        }
        private void comboBoxMainInput_DropDownClosed(object sender, EventArgs e)
        {
            this.richTextBoxExtendInput.Visible = false;
        }


    }
}
