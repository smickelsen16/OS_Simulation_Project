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
			xlWorkSheet.Cells[6,1] = "Queue 1";
			xlWorkSheet.Cells[9,1] = "Queue 2";
			xlWorkSheet.Cells[12,1] = "Queue 3";
			xlWorkSheet.Cells[15,1] = "Queue 4";
            
        }

        public void WriteTo(int row, int column, string value)
        {
            int PID = row;
            xlWorkSheet.Cells[row, column] = value;
        }

        public void Finish()
        {
            //xlWorkBook.SaveAs("C:\\Users\\James Bond\\Documents\\GitHub\\OS_Simulation_Project\\ExcelSheet.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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

