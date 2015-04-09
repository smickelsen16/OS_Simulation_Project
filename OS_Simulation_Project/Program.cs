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

        static void Main()
        {
            FileGenerate fg = new FileGenerate();
            Simulation s = new Simulation();

            Dictionary<int, PCB> processTable = s.CreateProcessTable();

            Queue<PCB> CPU_ready_Q = new Queue<PCB>();
            Queue<PCB> IO_ready_Q = new Queue<PCB>();

            UniprocessorAlgorithms uniSim = new UniprocessorAlgorithms();

            // add all processes to CPU_ready_Q since they are added in ascending arrivalTime order
            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Enqueue(processTable.ElementAt(h).Value);
            
            // going to have to separate this into multiple runs... CPU_ready_Q is changing each time through a queue and we need
            // to make sure the queues start with the same processes
            for (int i = 0; i < processTable.Count(); i++)
            {
                if (CPU_ready_Q.Count() != 0)
                {
                    // Run through multiple queues... collect stat information and write to file at the end, 
                    // 1st Queue
                    // RR q = 2
                    // RR q = 4
                    // RR q = 6
                    // FCFS
                    for (int j = 0; j < CPU_ready_Q.Count(); j++)
                    {
                        // need to be able to access CPU/IO_ready_Q in the processes to add/remove processes from them...
                        KeyValuePair<int, PCB> currProc = processTable.ElementAt(j);
                        uniSim.Round_Robin(2, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.Round_Robin(4, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.Round_Robin(6, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.First_Come_First_Served(currProc, ref systemTime);
                        systemTime += 2; // context switch
                    }

                    // write stats to file here

                    for (int p = 0; p < processTable.Count(); p++)
                    {
                        Console.WriteLine(processTable.ElementAt(p).Value.ToString() + "\n");
                    }

                    // 2nd Queue
                    // RR q = 6
                    // RR q = 7
                    // FCFS
                    for (int k = 0; k < processTable.Count(); k++)
                    {
                        KeyValuePair<int, PCB> currProc = processTable.ElementAt(k);
                        uniSim.Round_Robin(6, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.Round_Robin(7, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.First_Come_First_Served(currProc, ref systemTime);
                        systemTime += 2; // context switch
                    }

                    // write stats to file here

                    // 3rd Queue
                    // RR q = 3
                    // RR q = 5
                    // RR q = 7
                    // SRT
                    for (int l = 0; l < processTable.Count(); l++)
                    {
                        KeyValuePair<int, PCB> currProc = processTable.ElementAt(l);
                        uniSim.Round_Robin(3, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.Round_Robin(5, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.Round_Robin(7, currProc, ref systemTime);
                        systemTime += 2; // context switch
                    }
                    // give SRT the Queue of procs that havent finished yet
                    uniSim.Shortest_Remaining_Time(CPU_ready_Q, ref systemTime);
                    systemTime += 2; // context switch

                    // write stats to file here

                    // 4th Queue
                    // RR q = 2
                    // RR q = 6
                    // SRT
                    for (int m = 0; m < processTable.Count(); m++)
                    {
                        KeyValuePair<int, PCB> currProc = processTable.ElementAt(m);
                        uniSim.Round_Robin(2, currProc, ref systemTime);
                        systemTime += 2; // context switch
                        uniSim.Round_Robin(6, currProc, ref systemTime); 
                        systemTime += 2; // context switch
                    }
                    // give SRT the Queue of procs that havent finished yet
                    uniSim.Shortest_Remaining_Time(CPU_ready_Q, ref systemTime);
                    systemTime += 2; // context switch

                    // write stats to file here
                }
            }
        }
    }
}
