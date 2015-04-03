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

        //public ProcessTable table;                       // list of all processes & associated info & ID's

        

        static void Main()
        {
            

            bool Processor1, Processor2, Processor3, Processor4, Processor5, Processor6, Processor7, Processor8 = false;
            bool[] processorArray = new bool[8] { false, false, false, false, false, false, false, false };
            Queue<int> readyQ = new Queue<int>();
            Dictionary<int, PCB> processTable = new Dictionary<int, PCB>();         // going to be text file?????????????
            Random quantum = new Random();
            double throughput;                          // amount of processes completed in a given time
            double CPU_utilization;                     // % time CPU is executing
            double systemTime = 0;                      // keep track of current time

            // reading all processes, line by line into array of strings
            string[] processes = System.IO.File.ReadAllLines(@"C:\Users\smickelsen16\Desktop\COLLEGE FILES\Spring2015\CS475W OperatingSystems\OS_Simulation_Project\Mytext.txt");

            // arrays to hold CPU & IO burst time
            int[] CPU, IO;

            // loop through the text file, separate line by line, then character by character and feed into the processTable Dictionary
            for (int i = 0; i < processes.Count(); i++ )
            {
                // split one line by spaces and assign each character to an array element
                string[] currentProc = processes[i].Split(' ');
                for (int j = 2; j < currentProc.Count(); j++)
                {
                    // if j is even, its a CPU burst time
                    if (j%2 == 0)
                        CPU[j-2] = Int32.Parse(currentProc[j]);  
                    // if j is odd, its a IO burst time
                    else
                        IO[j-2] = Int32.Parse(currentProc[j]);
                }
                // add new process to table 
                processTable.Add(Int32.Parse(currentProc[0]), new PCB(Int32.Parse(currentProc[1]), true, CPU, IO));
            }

            // if the system time is the processes arrival time, it is added to the ready queue
            for (int i = 0; i <= processTable.Count(); i++)
            {
                if (processTable.ElementAt(i).Value.arrivalTime == systemTime)
                    readyQ.Enqueue(processTable.ElementAt(i).Key);
            }

            ProcessorAlgorithms uniSim = new ProcessorAlgorithms();

            // one queue... just repeat with different order/number of RR queues??
            for (int i = 0; i < processTable.Count(); i++)
            {
                Tuple<int, PCB> currProc = new Tuple<int, PCB>(processTable.ElementAt(i).Key, processTable.ElementAt(i).Value);
                uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);                                   
                uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
            }
            uniSim.First_Come_First_Served(currProc, systemTime);
        }
    }
}
