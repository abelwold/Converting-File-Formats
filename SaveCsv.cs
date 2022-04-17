using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{


    

    class SaveCsv
    {
       
        //Convert to CSV
        
        public SaveFileDialog sfd = new SaveFileDialog();

        public object Rows { get; private set; }
        public object DefaultCellStyle { get; private set; }


        //Const value of conversion rate
        const double knot = 0.868976;
        const double nautical = 0.539957;
        //string date_format = 

        public void ConvertToCsv(DataGridView dataGridView1)
        {
            //variables assign
            int columnCount = dataGridView1.Columns.Count;
            string columnNames = "";
            string[] outputCsv = new string[dataGridView1.Rows.Count];
           
            //for loop to columns of .txt and seprate it by "," 
            for (int i = 0; i < columnCount; i++)
            {
                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";
            }
            outputCsv[0] += columnNames; //columns stored in arrays along wih columnNames


            //for loop of rows to be stored in arraw and separeted with ","
            for (int i = 1; i < dataGridView1.Rows.Count; i++)

            {
                double miles = double.Parse(dataGridView1.Rows[i - 1].Cells[2].Value.ToString()); //miles stores the value of speed
                double km = double.Parse(dataGridView1.Rows[i - 1].Cells[3].Value.ToString()); //km stores the value of speed
                dataGridView1.Rows[i - 1].Cells[2].Value = miles * knot; //miles converts to knots
                dataGridView1.Rows[i - 1].Cells[3].Value = km * nautical; // km now converts to nautical
                //dataGridView1.Rows[i - 1].Cells[1].Style.Format = "hh:mm:ss tt";






                for (int j = 0; j < columnCount; j++)
                {
                    outputCsv[i] += dataGridView1.Rows[i-1].Cells[j].Value.ToString() + ","; //rows stored in arrays with values
                }   
            }
            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
            MessageBox.Show("Data Saved Successfully !!!", "Info"); // prompt of succesful Data Save

            System.Windows.Forms.Application.Exit();
        }


       
    }
}
