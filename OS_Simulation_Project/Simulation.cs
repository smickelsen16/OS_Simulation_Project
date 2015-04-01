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

        public void MultiProcRoundRobin(Queue<int> rq, bool[] procArray, ProcessorAlgorithms rr, Random rand, Dictionary<int, PCB> procTab)
        {
            if (rq.Count() != 0)
            {
                for (int i = 0; i < rq.Count(); i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        if (procArray[j] == false)
                        {
                            rr.Round_Robin(2, procTab);
                            rq.Dequeue();
                        }
                    }
                }
            }
        }

        static void Main()
        {
            bool Processor1, Processor2, Processor3, Processor4, Processor5, Processor6, Processor7, Processor8 = false;
            bool[] processorArray = new bool[8] { false, false, false, false, false, false, false, false };
            Queue<int> readyQ = new Queue<int>();
            Dictionary<int, PCB> processTable = new Dictionary<int, PCB>();         // going to be text file?????????????
            Random quantum = new Random();
            double throughput;                          // amount of processes completed in a given time
            double CPU_utilization;                     // % time CPU is executing
            double systemTime = 0;                         // keep track of current time


            processTable.Add(0, new PCB(6, true, 0));
            processTable.Add(1, new PCB(8, true, 4));
            processTable.Add(2, new PCB(4, true, 7));
            processTable.Add(3, new PCB(4, true, 8));
            processTable.Add(4, new PCB(2, true, 10));
            processTable.Add(5, new PCB(7, true, 15));
            processTable.Add(6, new PCB(5, true, 17));

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
