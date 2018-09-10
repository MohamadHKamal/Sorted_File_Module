using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
namespace sorted_File_module_poject_4
{
    public partial class Form9 : Form
    {
        Thread th;
        string Filename = ""; //FileD.txt
        string num_attributes = "";
        string FileName = "";
        string[][] table;
        string[] sort;
        int size = 0;
        int x = 0;
        public Form9()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
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
                ////////////display//////////////////////
                sort=new string[Table.Columns];
                DataTable T = new DataTable();
                for (int i = 0; i < Table.Columns; i++)
                {
                    T.Columns.Add(Table.Attributes[i]);
                    comboBox1.Items.Add(Table.Attributes[i]);
                }
                DataRow row;
                for (int i = 0; i < Table.Rows; i++)
                {
                    row = T.NewRow();
                    for (int j = 0; j < Table.Columns; j++)
                        row[Table.Attributes[j]] = Table.table[i][j];
                    T.Rows.Add(row);
                }
                dataGridView1.DataSource = T;
            }
            catch
            {
                MessageBox.Show("File Is Empty !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int sorted = 0;
                int writeagin = 0;
                string swape = "";
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



                if (comboBox1.Text == "RRN")
                {
                    DataTable T = new DataTable();
                    for (int i = 0; i < Table.Columns; i++)
                        T.Columns.Add(Table.Attributes[i]);


                    DataRow row;
                    for (int i = 0; i < Table.Rows; i++)
                    {
                        row = T.NewRow();
                        for (int j = 0; j < Table.Columns; j++)
                            row[Table.Attributes[j]] = Table.table[i][j];
                        T.Rows.Add(row);
                    }
                    dataGridView1.DataSource = T;
                }
                else
                {
                    for (int i = 0; i < Table.Columns;i++)
                    {
                        if(sort[i]==comboBox1.Text)
                        {
                            sorted++;
                            break;
                        }
                    }
                    if (sorted == 0)
                    {
                        sort[size] = comboBox1.Text;
                        size++;
                        try
                        {
                            for (int i = 0; i < Table.Columns; i++)
                            {
                                if (Table.Attributes[i] == comboBox1.Text)
                                {
                                    x = i;
                                    break;
                                }
                            }
                            for (int i = 0; i < Table.Rows - 1; i++)
                            {
                                for (int j = i + 1; j < Table.Rows; j++)
                                {
                                    if (Convert.ToInt32(Table.table[i][x]) > Convert.ToInt32(Table.table[j][x]))
                                    {
                                        for (int n = 0; n < Table.Columns; n++)
                                        {
                                            swape = Table.table[i][n];
                                            Table.table[i][n] = Table.table[j][n];
                                            Table.table[j][n] = swape;
                                        }
                                    }

                                }

                            }
                        }
                        catch
                        {
                            sorted = 0;
                            List<string> myList = new List<string>();
                            for (int n = 0; n < Table.Rows; n++)
                                myList.Add(Table.table[n][x]);
                            myList.Sort();
                            foreach (string name in myList)
                            {
                                for (int y = 0; y < Table.Rows; y++)
                                {
                                    if (name == Table.table[y][x] && y != sorted)
                                    {
                                        for (int n = 0; n < Table.Columns; n++)
                                        {
                                            swape = Table.table[y][n];
                                            Table.table[y][n] = Table.table[sorted][n];
                                            Table.table[sorted][n] = swape;
                                        }
                                    }
                                }
                                sorted++;
                            }
                        }




                        ////////////////////////////////////////////   
                        for (int g = 0; g < Table.Rows; g++)
                        {

                            if (writeagin == 0)
                            {
                                XmlWriter Xw = XmlWriter.Create(comboBox1.Text + ".xml");
                                Xw.WriteStartDocument();
                                Xw.WriteStartElement("Table");
                                Xw.WriteAttributeString("Name", comboBox1.Text + ".xml");
                                Xw.WriteStartElement("Record");

                                for (int i = 0; i < Table.Columns; i++)
                                {
                                    Xw.WriteStartElement(Table.Attributes[i]);
                                    Xw.WriteString(Table.table[0][i]);
                                    Xw.WriteEndElement();
                                }
                                Xw.WriteEndElement();
                                Xw.WriteEndElement();
                                Xw.WriteEndDocument();
                                Xw.Close();
                                writeagin++;
                            }
                            else
                            {
                                XmlDocument Doc = new XmlDocument();
                                XmlElement Record = Doc.CreateElement("Record");
                                XmlElement Node;

                                XmlDocument Doc1 = new XmlDocument();
                                Doc1.Load(comboBox1.Text + ".xml");
                                XmlNodeList MYList = Doc1.GetElementsByTagName("Record");

                                for (int i = 0; i < Table.Columns; i++)
                                {
                                    Node = Doc.CreateElement(Table.Attributes[i]);
                                    Node.InnerText = Table.table[g][i];
                                    Record.AppendChild(Node);
                                }

                                Doc.Load(comboBox1.Text + ".xml");
                                XmlElement Root = Doc.DocumentElement;
                                Root.AppendChild(Record);
                                Doc.Save(comboBox1.Text + ".xml");

                            }
                        }



                    }



                    XmlDocument z = new XmlDocument();
                    z.Load(comboBox1.Text + ".xml");
                    XmlNodeList MY_List = z.GetElementsByTagName("Record");
                    XmlNodeList No_de;

                    table = new string[MY_List.Count][];
                    for (int i = 0; i < MY_List.Count; i++)
                        table[i] = new string[Table.Columns];
                    for (int i = 0; i < Table.Rows; i++)
                    {
                        No_de = MY_List[i].ChildNodes;
                        for (int j = 0; j < Table.Columns; j++)
                        {


                            table[i][j] = No_de[j].InnerText;
                        }
                    }


                    DataTable T = new DataTable();
                    for (int i = 0; i < Table.Columns; i++)
                    {
                        T.Columns.Add(Table.Attributes[i]);
                    }
                    DataRow row;
                    for (int i = 0; i < Table.Rows; i++)
                    {
                        row = T.NewRow();
                        for (int j = 0; j < Table.Columns; j++)
                            row[Table.Attributes[j]] = table[i][j];
                        T.Rows.Add(row);
                    }
                    dataGridView1.DataSource = T;


                }
                MessageBox.Show("Operation is done", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch
            {
                MessageBox.Show("File Is Empty !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
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

