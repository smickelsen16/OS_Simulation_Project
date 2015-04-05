using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class Program
    {
        static void Main()
        {
            Simulation s = new Simulation();

            Dictionary<int, PCB> processTable = s.CreateProcessTable();

            int systemTime = s.systemTime;

            //bool Processor1, Processor2, Processor3, Processor4, Processor5, Processor6, Processor7, Processor8 = false;
            //bool[] processorArray = new bool[8] { false, false, false, false, false, false, false, false };
            Queue<PCB> CPU_ready_Q = new Queue<PCB>();
            Queue<PCB> IO_ready_Q = new Queue<PCB>();
            Random quantum = new Random();

            UniprocessorAlgorithms uniSim = new UniprocessorAlgorithms();
            // continuously run through the simulation until all are done
            do
            {
                // if the system time is the processes arrival time, it is added to the ready queue
                for (int i = 0; i <= processTable.Count(); i++)
                {
                    if (processTable.ElementAt(i).Value.CPUarrivalTime == systemTime)
                        CPU_ready_Q.Enqueue(processTable.ElementAt(i).Value);
                    if (CPU_ready_Q.Count() != 0)
                    {
                        // Run through multiple queues... collect stat information and write to file at the end, 
                        // do we need to use multithreading to run through all queues at the same time??
                        // 1st Queue
                        for (int j = 0; j < processTable.Count(); j++)
                        {
                            Tuple<int, PCB> currProc = new Tuple<int, PCB>(processTable.ElementAt(j).Key, processTable.ElementAt(j).Value);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.First_Come_First_Served(currProc, systemTime);
                            // need to get system time to update here as well, not just in the algorithm
                        }

                        // 2nd Queue
                        for (int k = 0; k < processTable.Count(); k++)
                        {
                            Tuple<int, PCB> currProc = new Tuple<int, PCB>(processTable.ElementAt(k).Key, processTable.ElementAt(k).Value);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.First_Come_First_Served(currProc, systemTime);
                        }

                        // 3rd Queue
                        for (int l = 0; l < processTable.Count(); l++)
                        {
                            Tuple<int, PCB> currProc = new Tuple<int, PCB>(processTable.ElementAt(l).Key, processTable.ElementAt(l).Value);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                        }
                        uniSim.Shortest_Remaining_Time(processTable, systemTime);

                        // 4th Queue
                        for (int m = 0; m < processTable.Count(); m++)
                        {
                            Tuple<int, PCB> currProc = new Tuple<int, PCB>(processTable.ElementAt(m).Key, processTable.ElementAt(m).Value);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                            uniSim.Round_Robin(quantum.Next(2, 11), currProc, systemTime);
                        }
                        uniSim.Shortest_Remaining_Time(processTable, systemTime);
                    }
                }


            } while (true);
        }
    }
}
