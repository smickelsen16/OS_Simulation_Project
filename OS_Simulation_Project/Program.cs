using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class Program
    {
        static int systemTime = 0;

        static Random quantum = new Random();

        static void Main()
        {
            Stopwatch time = new Stopwatch();

            Simulation s = new Simulation();

            Dictionary<int, PCB> processTable = s.CreateProcessTable();

            Queue<PCB> CPU_ready_Q = new Queue<PCB>();
            Queue<PCB> IO_ready_Q = new Queue<PCB>();

            UniprocessorAlgorithms uniSim = new UniprocessorAlgorithms();
            // continuously run through the simulation until all are done
            //do
            //{
            // if the system time is the processes arrival time, it is added to the ready queue
            for (int i = 0; i < processTable.Count(); i++)
            {
                //if (processTable.ElementAt(i).Value.CPUarrivalTime == systemTime)
                CPU_ready_Q.Enqueue(processTable.ElementAt(i).Value);
                if (CPU_ready_Q.Count() != 0)
                {
                    // Run through multiple queues... collect stat information and write to file at the end, 
                    // do we need to use multithreading to run through all queues at the same time??
                    // 1st Queue
                    for (int j = 0; j < processTable.Count(); j++)
                    {
                        int quantum1 = quantum.Next(2, 11);
                        int quantum2 = quantum.Next(2, 11);
                        int quantum3 = quantum.Next(2, 11);
                        KeyValuePair<int, PCB> currProc = processTable.ElementAt(j);
                        uniSim.Round_Robin(quantum1, currProc, ref systemTime);
                        uniSim.Round_Robin(quantum2, currProc, ref systemTime);
                        uniSim.Round_Robin(quantum3, currProc, ref systemTime);
                        uniSim.First_Come_First_Served(currProc, ref systemTime);
                        // need to get system time to update here as well, not just in the algorithm

                    }

                    for (int p = 0; p < processTable.Count(); p++)
                    {
                        Console.WriteLine(processTable.ElementAt(p).Value.ToString() + "\n");
                    }

                    //// 2nd Queue
                    //for (int k = 0; k < processTable.Count(); k++)
                    //{
                    //    KeyValuePair<int, PCB> currProc = processTable.ElementAt(k);
                    //    uniSim.Round_Robin(quantum.Next(2, 11), currProc, ref systemTime);
                    //    uniSim.Round_Robin(quantum.Next(2, 11), currProc, ref systemTime);
                    //    uniSim.First_Come_First_Served(currProc, ref systemTime);
                    //}

                    //// 3rd Queue
                    //for (int l = 0; l < processTable.Count(); l++)
                    //{
                    //    KeyValuePair<int, PCB> currProc = processTable.ElementAt(l);
                    //    uniSim.Round_Robin(quantum.Next(2, 11), currProc, ref systemTime);
                    //    uniSim.Round_Robin(quantum.Next(2, 11), currProc, ref systemTime);
                    //    uniSim.Round_Robin(quantum.Next(2, 11), currProc, ref systemTime);
                    //}
                    //uniSim.Shortest_Remaining_Time(processTable, ref systemTime);

                    //// 4th Queue
                    //for (int m = 0; m < processTable.Count(); m++)
                    //{
                    //    KeyValuePair<int, PCB> currProc = processTable.ElementAt(m);
                    //    uniSim.Round_Robin(quantum.Next(2, 11), currProc, ref systemTime);
                    //    uniSim.Round_Robin(quantum.Next(2, 11), currProc, ref systemTime);
                    //}
                    //uniSim.Shortest_Remaining_Time(processTable, ref systemTime);
                }
            }
            //} while (true);
        }
    }
}
