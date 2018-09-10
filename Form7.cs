using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace sorted_File_module_poject_4
{
    public partial class Form7 : Form
    {
        Thread th;
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * This Button for Open an Exist File 
             */
            FileStream Data = new FileStream("FilesName.txt", FileMode.Open);
            StreamReader sr = new StreamReader(Data);
            String Filename = textBox1.Text.ToString(); // FileD.txt
            Filename+="D.txt";
            bool f =false;
            while(sr.Peek()!=-1)
            {
                if(sr.ReadLine()==Filename)
                {
                    f = true;
                    break;
                }
            }
            sr.Close();
            if (f == false)
                MessageBox.Show("File Is Not Exist Please Enter An Exist File ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                FileStream Data1 = new FileStream("FilesName.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(Data1);
                sw.WriteLine(Filename);
                sw.Close();

                this.Close();
                th = new Thread(opennew_form);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();

               
            }
            
            

        }
        private void opennew_form(object obj)
        {
            Application.Run(new Form5());
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
            Application.Run(new Welcome());
        }
        
    }
}
