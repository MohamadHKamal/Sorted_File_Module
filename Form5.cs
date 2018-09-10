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
    public partial class Form5 : Form
    {
        Thread th;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennewform1(object obj)
        {
            Application.Run(new Form4());
        }
            
        
        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform2);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennewform2(object obj)
        {
            Application.Run(new Form6());
        }
           
        
        /// <summary>
        /// //////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform3);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennewform3(object obj)
        {
            Application.Run(new Form8());
        }
        /////////////////////
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform4);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennewform4(object obj)
        {
            Application.Run(new Form9());
        }
        /////////////
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennew_form);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennew_form(object obj)
        {
            Application.Run(new Form7());
        }
        

        
    }
}
