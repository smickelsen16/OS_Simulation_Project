using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Office.Core;
//using Excel = Microsoft.Office.Interop.Excel;

namespace OS_Simulation_Project
{
    class FileOutput
    {
        public StringBuilder csv = new StringBuilder();
        public void doStuff(int PID, string pArrival)
        {
            string newLine = string.Format("{0}, {1}{2}", PID, pArrival, Environment.NewLine);
            csv.Append(newLine);
            System.IO.File.WriteAllText(@"C:\Users\James Bond\Desktop\OutputCVS.cvs", csv.ToString());
        }



        //Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //public void doStuff(string n, int row, int column)
        //{
        //    //Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //    xlApp.Workbooks.Open("C:\\Users\\James Bond\\Documents\\GitHub\\OS_Simulation_Project\\ExcelSheet.xls");
        //    if (xlApp == null)
        //    {
        //        Console.WriteLine("Excel is not properly installed!");
        //    }

        //    Excel.Workbook xlWorkBook;
        //    Excel.Worksheet xlWorkSheet;
        //    object misValue = System.Reflection.Missing.Value;

        //    xlWorkBook = xlApp.Workbooks.Add(misValue);
        //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //    xlWorkSheet.Cells[2, 1] = n; //cells work row by column, so [3,1] would be the third column, first row

            
            
            
            
        //    xlWorkBook.SaveAs("C:\\Users\\James Bond\\Documents\\GitHub\\OS_Simulation_Project\\ExcelSheet.xls",Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //    xlWorkBook.Close(true, misValue, misValue);
        //    xlApp.Quit();

        //    releaseObject(xlWorkBook);
        //    releaseObject(xlWorkSheet);
        //    releaseObject(xlApp);
            
        //}

        //private void releaseObject(object obj) 
        //{
        //    try
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        //        obj = null;
        //    }        
        //    catch(Exception ex)
        //    {
        //        obj = null;
        //        Console.WriteLine("Exception Occured while releasing object");
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}  
    }
}
