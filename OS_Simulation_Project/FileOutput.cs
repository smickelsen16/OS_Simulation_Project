using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;

namespace OS_Simulation_Project
{
    class FileOutput
    {
        public Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

        public Excel.Workbook xlWorkBook;
        public Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        public void CreateFile()
        {
            if (xlApp == null)
            {
                return;
            }

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 1] = "Process ID"; //this goes row x column, EG. [5,1] would be row 5, column 1
                xlWorkSheet.Cells[2, 2] = "ArrivalTime";
                xlWorkSheet.Cells[3, 2] = "Total CPU Service";
                xlWorkSheet.Cells[4, 2] = "Total IO Service";

                xlWorkSheet.Cells[6, 1] = "Queue 1";
                xlWorkSheet.Cells[6, 2] = "Response";
                xlWorkSheet.Cells[7, 2] = "Turnaround";
                xlWorkSheet.Cells[8, 2] = "Wait";

                xlWorkSheet.Cells[9, 1] = "Queue 2";
                xlWorkSheet.Cells[9, 2] = "Response";
                xlWorkSheet.Cells[10, 2] = "Turnaround";
                xlWorkSheet.Cells[11, 2] = "Wait";

                xlWorkSheet.Cells[12, 1] = "Queue 3";
                xlWorkSheet.Cells[12, 2] = "Response";
                xlWorkSheet.Cells[13, 2] = "Turnaround";
                xlWorkSheet.Cells[14, 2] = "Wait";

                xlWorkSheet.Cells[15, 1] = "Queue 4";
                xlWorkSheet.Cells[15, 2] = "Response";
                xlWorkSheet.Cells[16, 2] = "Turnaround";
                xlWorkSheet.Cells[17, 2] = "Wait";
           
                
                xlWorkSheet.Cells[21, 1] = "QUEUE 1"; //this goes row x column, EG. [5,1] would be row 5, column 1
                xlWorkSheet.Cells[22, 1] = "QUEUE 2";
                xlWorkSheet.Cells[23, 1] = "QUEUE 3";
                xlWorkSheet.Cells[24, 1] = "QUEUE 4";

                
                xlWorkSheet.Cells[20, 2] = "Average Response"; //this goes row x column, EG. [5,1] would be row 5, column 1
                xlWorkSheet.Cells[20, 3] = "Average Turnaround";
                xlWorkSheet.Cells[20, 4] = "Average Wait";
                xlWorkSheet.Cells[20, 5] = "CPU Utilization (%)";
                xlWorkSheet.Cells[20, 6] = "Throughput";
            

        }

        public void WriteTo(int row, int column, string value)
        {
            int PID = row;
            xlWorkSheet.Cells[row, column] = value;
        }

        public void Finish()
        {
                xlWorkBook.SaveAs("C:\\Users\\smickelsen16\\Desktop\\OS_Simulation_Project\\ExcelSheet.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }

        }
    }
}  

