using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace sorted_File_module_poject_4
{
    class DynamicTable
    {
        public List<string> Attributes;
        public string[][] table;
        public string[][] table2;
        public string[][] Record;
        public string TableName;
        public bool row_is_1;
        public int Columns, Rows;
        int f = 0;
       
        int state_File = 0;
        public DynamicTable(int Columns, string name)
        {
            Attributes = new List<string>();
            Attributes.Add("RRN");
            
            //Write_In_Attributes();
            table = new string[1][];
            this.Columns = Columns + 1;
            Record = new string[1][];
            Record[0] = new string[this.Columns];
            table[0] = new string[this.Columns];

            TableName = name;
           
        }
        public void WriteToXml()
        {
            if (!File.Exists(TableName))
            {
                
                XmlWriter Xw = XmlWriter.Create(TableName);
                Xw.WriteStartDocument();
                Xw.WriteStartElement("Table");
                Xw.WriteAttributeString("Name", TableName);
                Xw.WriteStartElement("Record");

                Xw.WriteStartElement("RRN");
                Xw.WriteString(Convert.ToString(0));
                Xw.WriteEndElement();

                for (int i = 1; i < Columns; i++)
                {
                    Xw.WriteStartElement(Attributes[i]);
                    Xw.WriteString(table[0][i]);
                    Xw.WriteEndElement();

                }


                Xw.WriteEndElement();
                Xw.WriteEndElement();
                Xw.WriteEndDocument();
                Xw.Close();

            }
            else
            {
                XmlDocument Doc = new XmlDocument();
                XmlElement Record = Doc.CreateElement("Record");
                XmlElement Node;

                XmlDocument Doc1 = new XmlDocument();
                Doc1.Load(TableName);
                XmlNodeList MYList = Doc1.GetElementsByTagName("Record");

                Node = Doc.CreateElement(Attributes[0]);
                Node.InnerText = Convert.ToString(MYList.Count);
                Record.AppendChild(Node);

                for (int i = 1; i < Columns; i++)
                {
                    Node = Doc.CreateElement(Attributes[i]);
                    Node.InnerText = table[0][i];
                    Record.AppendChild(Node);

                }

                Doc.Load(TableName);
                XmlElement Root = Doc.DocumentElement;
                Root.AppendChild(Record);
                Doc.Save(TableName);
            }
        }

        public void WriteToXml_Agian(int j,char x)
        {

            if (f == 0)
            {
                
                XmlWriter Xw = XmlWriter.Create(TableName);
               
                Xw.WriteStartDocument();
                Xw.WriteStartElement("Table");
                Xw.WriteAttributeString("Name", TableName);

                if (row_is_1!=true || x=='u')
                {
                    f++;
                    Xw.WriteStartElement("Record");
                    Xw.WriteStartElement("RRN");
                    Xw.WriteString(Convert.ToString(0));
                    Xw.WriteEndElement();

                    for (int i = 1; i < Columns; i++)
                    {

                        Xw.WriteStartElement(Attributes[i]);
                        Xw.WriteString(table[j][i]);
                        Xw.WriteEndElement();


                    }
                    Xw.WriteEndElement();
                }
               
                Xw.WriteEndElement();

                Xw.WriteEndDocument();
                Xw.Close();
            }



            else
            {
                XmlDocument Doc = new XmlDocument();
                XmlElement Record = Doc.CreateElement("Record");
                XmlElement Node;

                XmlDocument Doc1 = new XmlDocument();
                Doc1.Load(TableName);
                XmlNodeList MYList = Doc1.GetElementsByTagName("Record");

                Node = Doc.CreateElement(Attributes[0]); //RNN
                Node.InnerText = Convert.ToString(MYList.Count);
                Record.AppendChild(Node);

                for (int i = 1; i < Columns; i++)
                {

                    Node = Doc.CreateElement(Attributes[i]);
                    Node.InnerText = table[j][i];
                    Record.AppendChild(Node);

                }




                Doc.Load(TableName);
                XmlElement Root = Doc.DocumentElement;
                Root.AppendChild(Record);
                Doc.Save(TableName);
            }
            
        }
        public void ReadFromXml()
        {
            Attributes.Clear();
            XmlDocument Doc = new XmlDocument();
            Doc.Load(TableName);
            XmlNodeList MYList = Doc.GetElementsByTagName("Record");
            Rows = MYList.Count;
            table = new string[MYList.Count][];
            for (int i = 0; i < MYList.Count; i++)
                table[i] = new string[Columns];
            XmlNodeList Node;
            for (int i = 0; i < MYList.Count; i++)
            {
                Node = MYList[i].ChildNodes;
                for (int j = 0; j < Columns; j++)
                {
                    Attributes.Add(Node[j].Name);
                    table[i][j] = Node[j].InnerText;
                }

            }
        }
         void Read()
        {
            try
            {
                XmlDocument Doc = new XmlDocument();
                Doc.Load(TableName);
                //Attributes.Clear();
                XmlNodeList MYList = Doc.GetElementsByTagName("Record");
                Rows = MYList.Count;
                table2 = new string[MYList.Count][];
                for (int i = 0; i < MYList.Count; i++)
                    table2[i] = new string[Columns];
                XmlNodeList Node;
                for (int i = 0; i < MYList.Count; i++)
                {
                    Node = MYList[i].ChildNodes;
                    for (int j = 0; j < Columns; j++)
                    {
                        Attributes.Add(Node[j].Name);
                        table2[i][j] = Node[j].InnerText;
                    }

                }
                state_File = 0;

            }
             catch
            {
                state_File = 1;
            }
        }
       public bool Is_Exist()
        {
            Read();
            int c = 0;
            if (state_File == 1)
                return false;
            else
            {
                for (int i = 0; i < Rows; i++)
                {
                    if (c == Columns - 1)
                        break;
                    for (int j = 1; j < Columns; j++)
                    {
                        if (table[0][j].ToLower() == table2[i][j].ToLower())
                            c++;
                    }

                    if(c<Columns-1)
                    c = 0;
                }
                if (c == Columns - 1)
                    return true;
            }

                return false;
        }
        public bool BinarySearch(int RRN)
        {
            ReadFromXml();
            int middle_index = Convert.ToInt32(Rows / 2);
            if (RRN < 0 || RRN > Convert.ToInt32(table[Rows - 1][0]))
                return false;
            if (RRN == Convert.ToInt32(table[middle_index][0]))
            {
                for (int i = 0; i < Columns; i++)
                    Record[0][i] = table[middle_index][i];
            }
            else if (RRN > Convert.ToInt32(table[middle_index][0]))
            {
                for (int i = middle_index; i < Rows; i++)
                {
                    if (RRN == Convert.ToInt32(table[i][0]))
                    {
                        for (int j = 0; j < Columns; j++)
                        {
                            Record[0][j] = table[i][j];
                        }
                        break;
                    }
                }
            }
            else if (RRN < Convert.ToInt32(table[middle_index][0]))
            {
                for (int i = 0; i <= middle_index; i++)
                {
                    if (RRN == Convert.ToInt32(table[i][0]))
                    {
                        for (int j = 0; j < Columns; j++)
                        {
                            Record[0][j] = table[i][j];
                        }
                        break;
                    }
                }
            }

            return true;
        }

        public void Write_In_Attributes(string Filename)
        {
            FileStream Data = new FileStream(Filename, FileMode.Open);
            StreamReader sr = new StreamReader(Data);
            sr.ReadLine();
            sr.ReadLine();
            while (sr.Peek() != -1)
            {
                Attributes.Add(sr.ReadLine().ToString());
            }
            sr.Close();
        }

    }
}