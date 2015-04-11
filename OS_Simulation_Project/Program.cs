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
        static Dictionary<int, PCB> COMPLETED_PROCS = new Dictionary<int, PCB>();

        static double throughput, CPU_utilization = 0;         // stats for whole system


        static void Main()
        {
            FileGenerate fg = new FileGenerate();
            fg.Start();
            Simulation s = new Simulation();

            Dictionary<int, PCB> processTable = s.CreateProcessTable();

            UniprocessorAlgorithms uniSim = new UniprocessorAlgorithms();

            MultiprocessorAlgorithms mulSim = new MultiprocessorAlgorithms();

            // add all processes to CPU_ready_Q since they are generated in ascending arrivalTime order
            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Add(processTable.ElementAt(h).Key, processTable.ElementAt(h).Value);

            // 1st Queue
            // RR q = 6
            // RR q = 7
            // FCFS
            /////////////////////////////////////////////////////////////////
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                // BURSTS ARE OKAY HERE //
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(6, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(7, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }

            do
            {
                for (int k = 0; k < CPU_ready_Q.Count(); k++)
                {
                    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                    uniSim.First_Come_First_Served(currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
                }
            } while (CPU_ready_Q.Count() != 0);


            throughput = COMPLETED_PROCS.Count() / systemTime;

            Console.WriteLine("Throughput for Queue 1: " + throughput.ToString());
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////

            /*RESET READY Q FOR NEXT QUEUE*/
            // add all processes to CPU_ready_Q since they are generated in ascending arrivalTime order
            processTable = s.CreateProcessTable();

            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Add(processTable.ElementAt(h).Key, processTable.ElementAt(h).Value);
            COMPLETED_PROCS.Clear();
            systemTime = 0;

            // write stats to file here

            // 2nd Queue
            // RR q = 2
            // RR q = 4
            // RR q = 6
            // FCFS
            ////////////////////////////////////////////////////////////////
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(2, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(4, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(6, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            do
            {
                for (int k = 0; k < CPU_ready_Q.Count(); k++)
                {
                    KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                    uniSim.First_Come_First_Served(currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
                }
            } while (CPU_ready_Q.Count() != 0);


            throughput = COMPLETED_PROCS.Count() / systemTime;

            Console.WriteLine("Throughput for Queue 2: " + throughput.ToString());
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////

            ///*RESET READY Q FOR NEXT QUEUE*/
            // add all processes to CPU_ready_Q since they are generated in ascending arrivalTime order
            processTable = s.CreateProcessTable();

            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Add(processTable.ElementAt(h).Key, processTable.ElementAt(h).Value);
            COMPLETED_PROCS.Clear();
            systemTime = 0;

            // write stats to file here

            // 3rd Queue
            // RR q = 3
            // RR q = 5
            // RR q = 7
            // SRT
            ///////////////////////////////////////////////////////////////
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(3, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(5, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(7, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            // give SRT the Queue of procs that havent finished yet
            do
            {
                uniSim.Shortest_Remaining_Time(ref CPU_ready_Q, ref systemTime, ref COMPLETED_PROCS);
            } while (CPU_ready_Q.Count() != 0);


            throughput = COMPLETED_PROCS.Count() / systemTime;

            Console.WriteLine("Throughput for Queue 3: " + throughput.ToString());
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////

            /*RESET READY Q FOR NEXT QUEUE*/
            // add all processes to CPU_ready_Q since they are generated in ascending arrivalTime order

            processTable = s.CreateProcessTable();
            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Add(processTable.ElementAt(h).Key, processTable.ElementAt(h).Value);
            COMPLETED_PROCS.Clear();
            systemTime = 0;

            // write stats to file here

            // 4th Queue
            // RR q = 2
            // RR q = 6
            // SRT
            //////////////////////////////////////////////////////////////
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(2, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            for (int k = 0; k < CPU_ready_Q.Count(); k++)
            {
                KeyValuePair<int, PCB> currProc = CPU_ready_Q.ElementAt(k);
                uniSim.Round_Robin(6, currProc, ref systemTime, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
            // give SRT the Queue of procs that havent finished yet
            do
            {
                uniSim.Shortest_Remaining_Time(ref CPU_ready_Q, ref systemTime, ref COMPLETED_PROCS);
            } while (CPU_ready_Q.Count() != 0);

            throughput = COMPLETED_PROCS.Count() / systemTime;

            Console.WriteLine("Throughput for Queue 4: " + throughput.ToString());
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////

            // write stats to file here



            /*RUN MULTIPROCESSOR SIMULATION HERE*/
        }
    }
}
