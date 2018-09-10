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

/*
 
 * This Form For Add New Record In the File 
 * button1 For Add
 * button2 For Transform to Form 5 
 
 */
namespace sorted_File_module_poject_4
{
    public partial class Form4 : Form
    {
        Thread th;
        string Filename = ""; //FileD.txt
        string num_attributes = "";
        string FileName=""; //File.txt
        
        public Form4()
        {
            InitializeComponent();
            
            FileStream Data = new FileStream("FilesName.txt", FileMode.Open);
            StreamReader sr = new StreamReader(Data);
            while (sr.Peek() != -1)
            {
                Filename = sr.ReadLine();
            }
            sr.Close();
            FileStream Data1 = new FileStream(Filename, FileMode.Open);
            StreamReader sr1 = new StreamReader(Data1);
            FileName = sr1.ReadLine();
             num_attributes = sr1.ReadLine();
            sr1.Close();
            DynamicTable Table = new DynamicTable(Convert.ToInt32(num_attributes), FileName);
            Table.Write_In_Attributes(Filename);
            for(int i=1;i<=Convert.ToInt32(num_attributes);i++)
            {
                AddNewlabel(i, Table.Attributes[i]);
                AddNewTextBox(i, Table.Attributes[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * this button for add 
             */
            FileStream Data = new FileStream("FilesName.txt", FileMode.Open);
            StreamReader sr = new StreamReader(Data);
            while (sr.Peek() != -1)
            {
                Filename = sr.ReadLine();
            }
            sr.Close();
            FileStream Data1 = new FileStream(Filename, FileMode.Open);
            StreamReader sr1 = new StreamReader(Data1);
            FileName = sr1.ReadLine();
            num_attributes = sr1.ReadLine();
            sr1.Close();
            DynamicTable Table = new DynamicTable(Convert.ToInt32(num_attributes), FileName);

            int c = 1;
            foreach (Control add in Controls)
            {
                if (add.Name.Contains("txt"))
                {
                    Table.table[0][c] = add.Text.ToString();
                    c++;
                }
            
            }


            if (Table.Is_Exist() == false)
            {
                Table.Write_In_Attributes(Filename);
                Table.WriteToXml();
                MessageBox.Show("Succesfuly Addedd !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Record is Existed !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            foreach (Control R in Controls)
            {
                if (R.Name.Contains("txt"))
                {
                    R.ResetText();
                }

            }
        }
        System.Windows.Forms.TextBox AddNewTextBox(int a,string name)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = a * 28;
            txt.Left = 220;
            txt.Name ="txt";
            return txt;
        }
        System.Windows.Forms.Label AddNewlabel(int a,string name)
        {
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            this.Controls.Add(label);
            label.Top = a * 28;
            label.Left = 80;
            label.Text = name;
            label.Name = "label"+a;
            return label;
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
            Application.Run(new Form5());
        }

       

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        
        
    }
}
