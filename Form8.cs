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
    
    public partial class Form8 : Form
    {
        Thread th;
        string Filename = ""; //FileD.txt
        string num_attributes = "";
        string FileName = ""; //File.txt

        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
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
            for(int i=0;i<Table.Columns;i++)
            {
                comboBox1.Items.Add(Table.Attributes[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool empty = false;

            try
            {
                string word = comboBox1.SelectedItem.ToString();
                string value = textBox1.Text.ToString();
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("It Is Not Vaild !!");
                    empty = true;
                }
                bool flag = false;
                int index = 0;

                //this must be change to file 
                DynamicTable a = new DynamicTable(Convert.ToInt32(num_attributes),FileName);
                //a.Write_In_Attributes(Filename);
                a.ReadFromXml();
                for (int i = 0; i < a.Columns; i++)
                {
                    if (word==a.Attributes[i].ToString())
                    {
                        index = i;
                        break;
                    }
                }
                for (int i = 0; i < a.Rows; i++)
                {
                    if (a.table[i][index] == value)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    Data.Rows.Clear();
                    if (Data.Columns.Count == 0)
                    {
                        for (int i = 0; i < a.Columns; i++)
                            Data.Columns.Add(a.Attributes[i], a.Attributes[i]);
                    }
                    for (int i = 0; i < a.Rows; i++)
                    {
                        if (value == a.table[i][index])
                            Data.Rows.Add(a.table[i]);
                    }
                }
                else if (flag == false && empty == false)
                    MessageBox.Show("Record Not Exist !! ");
            }
            catch
            {
                MessageBox.Show("It Is Not Vaild !!");
            }
        }

        private void Data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform4);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void opennewform4(object obj)
        {
            Application.Run(new Form5());
        }
    }
}
