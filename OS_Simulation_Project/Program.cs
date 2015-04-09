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

        static Dictionary<int, PCB> CPU_ready_Q = new Dictionary<int, PCB>();
        static Dictionary<int, PCB> IO_ready_Q = new Dictionary<int, PCB>();
        static Dictionary<int, PCB> COMPLETED_PROCS = new Dictionary<int, PCB>();

        static void Main()
        {
            FileGenerate fg = new FileGenerate();
            Simulation s = new Simulation();

            Dictionary<int, PCB> processTable = s.CreateProcessTable();

            UniprocessorAlgorithms uniSim = new UniprocessorAlgorithms();

            MultiprocessorAlgorithms mulSim = new MultiprocessorAlgorithms();

            // add all processes to CPU_ready_Q since they are generated in ascending arrivalTime order
            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Add(processTable.ElementAt(h).Key, processTable.ElementAt(h).Value);

            // gonna need to run these separately at re-generate the CPU_Queue between each uniprocessor queue run...
            //do
            //{
                // Run through multiple queues... collect stat information and write to file at the end, 
                // 1st Queue
                // RR q = 6
                // RR q = 7
                // FCFS
                /////////////////////////////////////////////////////////////////
                for (int k = 0; k < CPU_ready_Q.Count(); k++)
                {
                    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                    uniSim.Round_Robin(6, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                }
                for (int k = 0; k < CPU_ready_Q.Count(); k++)
                {
                    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                    uniSim.Round_Robin(7, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                }

                Console.WriteLine("COMPLETED");
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.First_Come_First_Served(currProc, ref systemTime);
                //}
                ////////////////////////////////////////////////////////////////

                // write stats to file here

                //// 2nd Queue
                //// RR q = 2
                //// RR q = 4
                //// RR q = 6
                //// FCFS
                //////////////////////////////////////////////////////////////////
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(2, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(4, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(6, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.First_Come_First_Served(currProc, ref systemTime);
                //}
                //////////////////////////////////////////////////////////////////

                //// write stats to file here

                //// 3rd Queue
                //// RR q = 3
                //// RR q = 5
                //// RR q = 7
                //// SRT
                /////////////////////////////////////////////////////////////////
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(3, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(5, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(7, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //// give SRT the Queue of procs that havent finished yet
                //uniSim.Shortest_Remaining_Time(CPU_ready_Q, ref systemTime);
                //////////////////////////////////////////////////////////////////

                //// write stats to file here

                //// 4th Queue
                //// RR q = 2
                //// RR q = 6
                //// SRT
                ////////////////////////////////////////////////////////////////
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(2, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //for (int k = 0; k < CPU_ready_Q.Count(); k++)
                //{
                //    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                //    uniSim.Round_Robin(6, currProc, ref systemTime, ref CPU_ready_Q, ref IO_ready_Q, ref COMPLETED_PROCS);
                //}
                //// give SRT the Queue of procs that havent finished yet
                //uniSim.Shortest_Remaining_Time(CPU_ready_Q, ref systemTime);
                ////////////////////////////////////////////////////////////////

                //// write stats to file here
            //} while (CPU_ready_Q.Count() != 0 || IO_ready_Q.Count() != 0);



            /*RUN MULTIPROCESSOR SIMULATION HERE*/
        }
    }
}
