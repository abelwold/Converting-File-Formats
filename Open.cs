using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Open : Display
    {
        // string initialization
        string delimiter = "|";
        string tablename = "File";
        string txt;
        string allData;
        string[] rows;
            
        
        public OpenFileDialog openFileDialog1 = new OpenFileDialog(); //public object OpenFileDialog1 created from OpenFileDialog class
        
        

        //dataset object created from DataSet Class
        DataSet dataset = new DataSet();


        string filename { get; set; } //filename constructor to set and get data


        /*
         OpenFile method operates by opening file.txt and displaying data in the dataGridView.
          Uses StreamReader to read files and with the dataset object tablename of the columns are created.
          If Statement to alert user with messageBox prompts if they want to import data and when data succesufly gets imported.
         */
        public void OpenFile(DataGridView dataGridView1)
        {
            if (MessageBox.Show("Are you sure you want to import the data from \n " + openFileDialog1.FileName + "?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                filename = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(filename); // sr object derived from the StreamReader Class to handle File operations.
                txt = File.ReadAllText(openFileDialog1.FileName);
                dataset.Tables.Add(tablename); //Tablenames is added to table and columns has been assigned with data names example "Date" with its data type.
                dataset.Tables[tablename].Columns.Add("Date", typeof(string));
                dataset.Tables[tablename].Columns.Add("Time", typeof(string));
                dataset.Tables[tablename].Columns.Add("Speed", typeof(double));
                dataset.Tables[tablename].Columns.Add("Distance", typeof(float));
                dataset.Tables[tablename].Columns.Add("Description", typeof(string));
                
                



                 allData = sr.ReadToEnd(); //Reads the whole context of the text file
                 rows = allData.Split("\r".ToCharArray()); // handles spliting data to charArrays


                /*
                 foreach loop to handle splitting each items after delimiter and storing in array.
                The DataGridView then displays Array of list to rows in the DataGridView
                 */
                foreach (string r in rows)
                {
                    string[] items = r.Split(delimiter.ToCharArray()); //items stores split of delimiter to charArray
                    dataset.Tables[tablename].Rows.Add(items); //Table Adds on items in a row to the table
                }
                dataGridView1.DataSource = dataset.Tables[0].DefaultView; // DataSpurces now displays information on the DataGridView for each item.
                MessageBox.Show(filename + " was successfully imported. \n ", "Success!", MessageBoxButtons.OK); //prompt of succesful import


                
            }
        }

    }
}
