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
    
    public partial class Form_2 : Form
    {
        Thread th;
        public Form_2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            if (String.IsNullOrEmpty(Nametxt.Text.ToString()) || string.IsNullOrEmpty(Nametxt.Text.ToString()))
            {
                MessageBox.Show("It Isn't vaild !!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                string FileName = Nametxt.Text.ToString();
                string number = Numattr.Text.ToString();
                string FileNameD = FileName;
                FileName += ".txt";
                FileNameD += "D.txt";
                FileStream Data = new FileStream(FileNameD, FileMode.Create);
                FileStream Files = new FileStream("FilesName.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(Data);
                StreamWriter sw1 = new StreamWriter(Files);
                sw.WriteLine(FileName);
                sw.WriteLine(number);
                sw1.WriteLine(FileNameD);
                sw.Close();
                sw1.Close();
                Nametxt.Clear();
                Numattr.Clear();
                this.Close();
                th = new Thread(opennewform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }
        private void opennewform(object obj)
        {
            Application.Run(new Form3());
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form_2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennew_form);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennew_form(object obj)
        {
            Application.Run(new Welcome());
        }
        


    }
}
