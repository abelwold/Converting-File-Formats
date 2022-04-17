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
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable(); //object table created from the DataTable class
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // open button.
        private void button1_Click(object sender, EventArgs e)
        {

            Open openfile = new Open(); //Object created from Open Class

            
            openfile.openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // filters supports .txt files and all files format
            openfile.openFileDialog1.FilterIndex = 1;


            //Open files and displays data in the dataGridView
            if (openfile.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openfile.OpenFile(this.dataGridView1);  //method wit parameter of dataGridView to display data on the dataGridView

            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {

            SaveCsv CsvConvert = new SaveCsv(); //object CsvConvert created from SaveCsv Class 
            if (dataGridView1.Rows.Count > 0)
            {

                CsvConvert.sfd.Filter = "CSV (*.csv)|*.csv"; // Filters .csv file extentions only
                CsvConvert.sfd.FileName = "Output.csv"; // initiate to save fileName as Output.csv
                bool fileError = false;

                //handles converting and saving as Csv
                if (CsvConvert.sfd.ShowDialog() == DialogResult.OK)
                {
                    //check if file exists. if it does it delete if not it will import other than error.
                    if (File.Exists(CsvConvert.sfd.FileName))
                    {
                        try
                        {
                            File.Delete(CsvConvert.sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            CsvConvert.ConvertToCsv(this.dataGridView1);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                //Display message box when no data is present in the dataGridView
                MessageBox.Show("No Record To Save !!!", "Info");
            }

        }
        //Savexlm button
        private void button3_Click(object sender, EventArgs e)
        {
            SaveXlm ConvertXlm = new SaveXlm(); //Object ConvertXlm created from SaveXlm class

            if (dataGridView1.Rows.Count > 0)
            {
                DataTable dt = ConvertXlm.GetDataTableFromDGV(dataGridView1);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XML|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ds.Tables[0].WriteXml(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    MessageBox.Show("Data Saved Successfully !!!", "Info");
                }

                ConvertXlm.GetDataTableFromDGV(dataGridView1);
            }
            else
            {
                //Display message box when no data is present in the dataGridView
                MessageBox.Show("No Record To Save !!!", "Info");
            }
            System.Windows.Forms.Application.Exit();
        }


    }


}

