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
    public partial class Form3 : Form
    {
        Thread th;
        string Filename = "";
        string num_attributes = "";

        public Form3()
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
            while (sr1.Peek() != -1)
            {
                num_attributes = sr1.ReadLine();
            }

            sr1.Close();

            for (int i = 1; i <= Convert.ToInt32(num_attributes); i++)
            {
                AddNewlabel(i);
                AddNewTextBox(i);
            }  
        }
       
         System.Windows.Forms.TextBox AddNewTextBox(int a)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = a * 28;
            txt.Left = 220;
            txt.Name = "txt" +a;
            return txt;
        }
        System.Windows.Forms.Label AddNewlabel(int a)
        {
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            this.Controls.Add(label);
            label.Top = a* 28;
            label.Left = 80;
            label.Text = "Attributes" + a;
            label.Name = "label" + a;
            return label ;
        }
      private void Form3_Load(object sender, EventArgs e)
        { }

      private void button1_Click(object sender, EventArgs e)
      {
          FileStream Data1 = new FileStream(Filename, FileMode.Append);
          StreamWriter sw = new StreamWriter(Data1);
          foreach(Control x in Controls)
          {
              if(x.Name.Contains("txt"))
              {
                  sw.WriteLine(x.Text.ToString());
              }
          }
          sw.Close();
          this.Close();
          th = new Thread(opennew_form);
          th.SetApartmentState(ApartmentState.STA);
          th.Start();

      }

      private void opennew_form(object obj)
      {
          Application.Run(new Form4());
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
            Application.Run(new Form_2());
        }
      

      

      

      

     

      
    }
}
