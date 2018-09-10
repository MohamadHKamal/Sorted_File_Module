using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace sorted_File_module_poject_4
{
    public partial class Welcome : Form
        
    {
        Thread th;
        public Welcome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            th=new Thread(opennew_form);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            
        }
        private void opennew_form(object obj)
        {
            Application.Run(new Form_2());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennewform(object obj)
        {
            Application.Run(new Form7());
        }
            

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
