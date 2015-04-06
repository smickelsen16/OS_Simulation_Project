using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace OS_Simulation_Project
{
    /// <summary>
    /// where all the magic will happen
    /// </summary>
    class Simulation
    {
        // Creates process table based on randomly generated text file
        public Dictionary<int, PCB> CreateProcessTable()
        {
            List<int> CPU = new List<int>();
            List<int> IO = new List<int>();

            Dictionary<int, PCB> processTable = new Dictionary<int, PCB>();

            // reading all processes, line by line into array of strings
            string[] processes = System.IO.File.ReadAllLines(@"C:\Users\Wesley\Desktop\Mytext.txt");

            // loop through the text file, separate line by line, then character by character and feed into the processTable Dictionary
            for (int i = 0; i < 20; i++)
            {
                // split one line by spaces and assign each character to an array element
                string[] currentProc = processes[i].Split(' ');

                CPU.Clear();
                IO.Clear();

                for (int j = 2; j < currentProc.Count(); j++)
                {
                    // if j is even, its a CPU burst time
                    if (j % 2 == 0)
                        CPU.Add(Int32.Parse(currentProc[j]));
                    // if j is odd, its a IO burst time
                    else
                        IO.Add(Int32.Parse(currentProc[j]));
                }
                // add new process to table 
                processTable.Add(Int32.Parse(currentProc[0]), new PCB(Int32.Parse(currentProc[1]), true, CPU, IO));

                //Console.WriteLine(processTable.ElementAt(i).Value.ToString() + "\n");
            }
            return processTable;
        }

        //public int systemTime = 0;                          // keeps track of system time

        public int throughput, CPU_utilization = 0;         // stats for whole system

    }
}
