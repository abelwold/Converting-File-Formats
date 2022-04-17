using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    class SaveXlm 
    {

        //Xlm converter
        public DataTable GetDataTableFromDGV(DataGridView dataGridView1) //method DataTable with parameter dataGridView1

        {
           //varaible asigned as an object from DataTable
            var dt = new DataTable();


            //assigns names to the columns of the xlm
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("time", typeof(string));
            dt.Columns.Add("speed", typeof(float));
            dt.Columns.Add("distance", typeof(float));
            dt.Columns.Add("description", typeof(string));

            //Const value of conversion rate
            const double knot = 0.868976;
            const double nautical = 0.539957;
            



            //for loop add each column
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Visible)
                {
                     dt.Columns.Add();

                   
                }
            }
            
            object[] cellValues = new object[dataGridView1.Columns.Count];
            
            //iterates through dataGrid value and converts data to its respected values.
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                double miles = double.Parse(dataGridView1.Rows[i - 1].Cells[2].Value.ToString()); //miles stores the value of speed
                double km = double.Parse(dataGridView1.Rows[i - 1].Cells[3].Value.ToString()); //km stores the value of speed
                dataGridView1.Rows[i - 1].Cells[2].Value = miles * knot; //miles converts to knots
                dataGridView1.Rows[i - 1].Cells[3].Value = km * nautical; // km now converts to nautical
                dataGridView1.Rows[i - 1].DefaultCellStyle.Format = "hh:mm tt";

            }

            


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                    
                }
                
                

                dt.Rows.Add(cellValues);
            }
            dataGridView1.Visible = false;
   
              return dt;
            
         
        }

        

    }
   
}
