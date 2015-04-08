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

            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Enqueue(processTable.ElementAt(h).Value);
            
            // continuously run through the simulation until all are done
            //do
            //{
            // if the system time is the processes arrival time, it is added to the ready queue
            for (int i = 0; i < processTable.Count(); i++)
            {
                if (CPU_ready_Q.Count() != 0)
                {
                    // Run through multiple queues... collect stat information and write to file at the end, 
                    // do we need to use multithreading to run through all queues at the same time??
                    // 1st Queue
                    for (int j = 0; j < processTable.Count(); j++)
                    {
                        KeyValuePair<int, PCB> currProc = processTable.ElementAt(j);
                        uniSim.Round_Robin(2, currProc, ref systemTime);
                        uniSim.Round_Robin(4, currProc, ref systemTime);
                        uniSim.Round_Robin(6, currProc, ref systemTime);
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
                    //    uniSim.Round_Robin(6, currProc, ref systemTime);
                    //    uniSim.Round_Robin(7, currProc, ref systemTime);
                    //    uniSim.First_Come_First_Served(currProc, ref systemTime);
                    //}

                    //// 3rd Queue
                    //for (int l = 0; l < processTable.Count(); l++)
                    //{
                    //    KeyValuePair<int, PCB> currProc = processTable.ElementAt(l);
                    //    uniSim.Round_Robin(3, currProc, ref systemTime);
                    //    uniSim.Round_Robin(5, currProc, ref systemTime);
                    //    uniSim.Round_Robin(7, currProc, ref systemTime);
                    //}
                    //uniSim.Shortest_Remaining_Time(processTable, ref systemTime);

                    //// 4th Queue
                    //for (int m = 0; m < processTable.Count(); m++)
                    //{
                    //    KeyValuePair<int, PCB> currProc = processTable.ElementAt(m);
                    //    uniSim.Round_Robin(2, currProc, ref systemTime);
                    //    uniSim.Round_Robin(6, currProc, ref systemTime);
                    //}
                    //uniSim.Shortest_Remaining_Time(processTable, ref systemTime);
                }
            }
            //} while (true);
        }
    }
}
