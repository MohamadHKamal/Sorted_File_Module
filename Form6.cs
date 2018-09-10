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
    public partial class Form6 : Form
    {
        Thread th;
        string Filename = ""; //FileD.txt
        string num_attributes = "";
        string FileName = ""; //File.txt
        int RRN=0;
        bool flage = false;
        
        bool Flag = false;

        public Form6()
        {
            InitializeComponent();
            
        }

        private void Form6_Load(object sender, EventArgs e){}
        
        System.Windows.Forms.TextBox AddNewTextBox(int a, string name)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = a * 28;
            txt.Left = 220;
            txt.Text = name;
            txt.Name = "txt";
            return txt;
        }
        System.Windows.Forms.Label AddNewlabel(int a, string name)
        {
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            this.Controls.Add(label);
            label.Top = a * 28;
            label.Left = 80;
            label.Text = name;
            label.Name = "label" + a;
            return label;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
             * this button for search by binary search algorithm 
             */
            try
            {
                flage = true;
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
                if (string.IsNullOrEmpty(textBox1.Text.ToString()))
                {
                    flage = false;
                    MessageBox.Show("Invaild !!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                }
                else
                {
                    RRN = Convert.ToInt32(textBox1.Text.ToString());
                    if (Table.BinarySearch(RRN) && Flag == false)
                    {
                        for (int i = 0; i < Table.Columns; i++)
                        {
                            AddNewlabel(i + 1, Table.Attributes[i]);
                            AddNewTextBox(i + 1, Table.Record[0][i]);
                        }
                        Flag = true;
                    }
                    else if (Table.BinarySearch(RRN) && Flag == true)
                    {
                        int i = 0;
                        foreach (Control x in Controls)
                        {
                            if (x.Name.Contains("txt"))
                            {
                                x.Text = Table.Record[0][i];
                                i++;
                            }
                        }
                    }
                    else
                    {
                        flage = false;
                        MessageBox.Show("Record Is Not Exits !!"); 
                    }
                    textBox1.Clear();
                }
            }
            catch
            {
                flage = false;
                MessageBox.Show("File is Empty !!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
              * For Delete a record 
           */
            if (flage == true)
            {
                try
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
                    Table.ReadFromXml();
                    string[][] T;

                    if (Table.Rows > 1)
                    {
                        T = new String[Table.Rows - 1][];

                        for (int i = 0; i < Table.Rows - 1; i++)
                        {
                            T[i] = new string[Table.Columns];
                        }
                        Table.row_is_1 = false;

                    }

                    else
                    {
                        T = new String[1][];
                        for (int i = 0; i < Table.Rows; i++)
                        {
                            T[i] = new string[Table.Columns];
                        }
                        Table.row_is_1 = true;

                    }


                    int j = 0;

                    for (int i = 0; i < Table.Rows; i++)
                    {
                        if (Convert.ToInt32(Table.table[i][0]) == RRN)
                        {
                            //delete
                            continue;
                        }
                        else
                        {
                            for (int k = 0; k < Table.Columns; k++)
                                T[j][k] = Table.table[i][k];
                            j++;
                        }
                    }
                    if (Table.Rows > 1)
                        Table.Rows -= 1;
                    Table.table = new String[Table.Rows][];
                    for (int i = 0; i < Table.Rows; i++)
                    {
                        Table.table[i] = new string[Table.Columns];
                    }

                    for (int i = 0; i < Table.Rows; i++)
                    {
                        for (int o = 0; o < Table.Columns; o++)
                        {
                            Table.table[i][o] = T[i][o];
                        }
                    }


                    for (int i = 0; i < Table.Rows; i++)
                    {
                        Table.WriteToXml_Agian(i, 'd');
                    }


                    foreach (Control R in Controls)
                    {
                        if (R.Name.Contains("txt"))
                        {
                            R.ResetText();
                        }

                    }
                    MessageBox.Show("Deleted Is Done !!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("File Is Empty !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("You Must Press Button Search to display the record in text box", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    
        }

        private void button2_Click(object sender, EventArgs e)
        {

            /*
              * This Button For Updata
              */
            if (flage == true)
            {
                try
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
                    Table.ReadFromXml();

                    /////////////////////////////////////////////////////

                    int n = 0;
                    int n1 = 0;
                    for (int i = 0; i < Table.Rows; i++)
                    {
                        if (Convert.ToInt32(Table.table[i][0]) == RRN)
                        {
                            n = i;
                            break;
                        }
                    }


                    foreach (Control x in Controls)
                    {
                        if (x.Name.Contains("txt"))
                        {
                            Table.Record[0][n1] = x.Text.ToString();
                            n1++;
                        }

                    }

                    for (int j = 0; j < Table.Columns; j++)
                        Table.table[n][j] = Table.Record[0][j];

                    for (int i = 0; i < Table.Rows; i++)
                        Table.WriteToXml_Agian(i, 'u');


                    MessageBox.Show("Succesfuly updated !! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("RRN Will Not Be Changed For Ever it is constant ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("File Is Empty !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
                MessageBox.Show("You Must Press Button Search to display the record in text box","",MessageBoxButtons.OK,MessageBoxIcon.Information);

            
            
        }

        private void button4_Click(object sender, EventArgs e)
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
