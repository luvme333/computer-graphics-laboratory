using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_work_1
{
    public partial class Form2 : Form
    {
        public int kernelSize;
        public Form2()
        {
            InitializeComponent();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Laboratory_work_1.Form1 form = new Laboratory_work_1.Form1();
            switch (comboBox1.Text)
            {
                case "Маска 3х3": kernelSize = 3; this.Close(); break;
                case "Маска 5х5": kernelSize = 5; this.Close(); break;
                case "Маска 7х7": kernelSize = 7; this.Close(); break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
