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
        static SortedDictionary<int, PCB> COMPLETED_PROCS = new SortedDictionary<int, PCB>();

        

        static double throughput, CPU_utilization = 0;         // stats for whole system

        static double totalCPUTime = 0;


        static void Main()
        {
            FileGenerate fg = new FileGenerate();
            fg.Start();
            Simulation s = new Simulation();

			FileOutput FO = new FileOutput();
			FO.CreateFile();

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


            // throughput is # processes per system time
            throughput = (COMPLETED_PROCS.Count() / (double)systemTime);

            for (int i = 0; i < COMPLETED_PROCS.Count(); i++)
                // add all CPU burst times of all processes together...
                totalCPUTime += COMPLETED_PROCS.ElementAt(i).Value.expectedCPUTime;

            // % time CPU was running (not including IO bursts)
            CPU_utilization = (totalCPUTime / systemTime) * 100;

            Console.WriteLine("Throughput for Queue 1: " + Math.Round((decimal)throughput, 5).ToString());
            Console.WriteLine("CPU Utilization for Queue 1: " + Math.Round((decimal)CPU_utilization, 5).ToString() + "%");

            
            
            // write stats to file here
            for (int i = 3; i < COMPLETED_PROCS.Count() + 3; i++)
            {
				//Writeto function writes to cells in excel file, it takes 3 parameters
				//The first parameter is the row, second is column, and third is what youre writing
				FO.WriteTo (1, i, (i - 3).ToString());
				FO.WriteTo (2, i, COMPLETED_PROCS.ElementAt (i - 3).Value.arrivalTime.ToString());
				FO.WriteTo (3, i, COMPLETED_PROCS.ElementAt (i - 3).Value.expectedCPUTime.ToString());
				FO.WriteTo (4, i, COMPLETED_PROCS.ElementAt (i - 3).Value.expectedIOTime.ToString());
				FO.WriteTo (1, i, (i - 3).ToString());
                FO.WriteTo(6, i, COMPLETED_PROCS.ElementAt(i - 3).Value.response.ToString());
				FO.WriteTo (7, i, COMPLETED_PROCS.ElementAt (i - 3).Value.turnaround.ToString ());
				FO.WriteTo (8, i, COMPLETED_PROCS.ElementAt (i - 3).Value.wait.ToString ());
            }

			//writing to the end of the excel file where we will display the values below
			FO.WriteTo(6, COMPLETED_PROCS.Count()+4, "Average Response time");
			FO.WriteTo(7, COMPLETED_PROCS.Count()+4, "Average Turnaround time");
			FO.WriteTo(8, COMPLETED_PROCS.Count()+4, "Average Wait time");
			FO.WriteTo(6, COMPLETED_PROCS.Count()+6, "Cpu Utilization");
			FO.WriteTo(7, COMPLETED_PROCS.Count()+6, "Throughput");

			FO.WriteTo(9, COMPLETED_PROCS.Count()+4, "Average Response time");
			FO.WriteTo(10, COMPLETED_PROCS.Count()+4, "Average Turnaround time");
			FO.WriteTo(11, COMPLETED_PROCS.Count()+4, "Average Wait time");
			FO.WriteTo(9, COMPLETED_PROCS.Count()+6, "Cpu Utilization");
			FO.WriteTo(10, COMPLETED_PROCS.Count()+6, "Throughput");

			FO.WriteTo(12, COMPLETED_PROCS.Count()+4, "Average Response time");
			FO.WriteTo(13, COMPLETED_PROCS.Count()+4, "Average Turnaround time");
			FO.WriteTo(13, COMPLETED_PROCS.Count()+4, "Average Wait time");
			FO.WriteTo(12, COMPLETED_PROCS.Count()+6, "Cpu Utilization");
			FO.WriteTo(13, COMPLETED_PROCS.Count()+6, "Throughput");

			FO.WriteTo(15, COMPLETED_PROCS.Count()+4, "Average Response time");
			FO.WriteTo(16, COMPLETED_PROCS.Count()+4, "Average Turnaround time");
			FO.WriteTo(17, COMPLETED_PROCS.Count()+4, "Average Wait time");
			FO.WriteTo(15, COMPLETED_PROCS.Count()+6, "Cpu Utilization");
			FO.WriteTo(16, COMPLETED_PROCS.Count()+6, "Throughput");

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


            throughput = (COMPLETED_PROCS.Count() / (double)systemTime);

            // % time CPU was running (not including IO bursts)
            CPU_utilization = (totalCPUTime / systemTime) * 100;

            Console.WriteLine("Throughput for Queue 2: " + Math.Round((decimal)throughput, 5).ToString());
            Console.WriteLine("CPU Utilization for Queue 2: " + Math.Round((decimal)CPU_utilization, 5).ToString() + "%");

			for (int i = 3; i < COMPLETED_PROCS.Count() + 3; i++)
			{
				//Writeto function writes to cells in excel file, it takes 3 parameters
				//The first parameter is the row, second is column, and third is what youre writing
				FO.WriteTo(9, i, COMPLETED_PROCS.ElementAt(i - 3).Value.response.ToString());
				FO.WriteTo (10, i, COMPLETED_PROCS.ElementAt (i - 3).Value.turnaround.ToString ());
				FO.WriteTo (11, i, COMPLETED_PROCS.ElementAt (i - 3).Value.wait.ToString ());
			}

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


            throughput = (COMPLETED_PROCS.Count() / (double)systemTime);

            // % time CPU was running (not including IO bursts)
            CPU_utilization = (totalCPUTime / systemTime) * 100;

            Console.WriteLine("Throughput for Queue 3: " + Math.Round((decimal)throughput, 5).ToString());
            Console.WriteLine("CPU Utilization for Queue 3: " + Math.Round((decimal)CPU_utilization, 5).ToString() + "%");

			for (int i = 3; i < COMPLETED_PROCS.Count() + 3; i++)
			{
				//Writeto function writes to cells in excel file, it takes 3 parameters
				//The first parameter is the row, second is column, and third is what youre writing
				FO.WriteTo(12, i, COMPLETED_PROCS.ElementAt(i - 3).Value.response.ToString());
				FO.WriteTo (13, i, COMPLETED_PROCS.ElementAt (i - 3).Value.turnaround.ToString ());
				FO.WriteTo (4, i, COMPLETED_PROCS.ElementAt (i - 3).Value.wait.ToString ());
			}

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

            throughput = (COMPLETED_PROCS.Count() / (double)systemTime);

            // % time CPU was running (not including IO bursts)
            CPU_utilization = (totalCPUTime / systemTime) * 100;

            Console.WriteLine("Throughput for Queue 4: " + Math.Round((decimal)throughput, 5).ToString());
            Console.WriteLine("CPU Utilization for Queue 4: " + Math.Round((decimal)CPU_utilization, 5).ToString() + "%");

			for (int i = 3; i < COMPLETED_PROCS.Count() + 3; i++)
			{
				//Writeto function writes to cells in excel file, it takes 3 parameters
				//The first parameter is the row, second is column, and third is what youre writing
				FO.WriteTo(15, i, COMPLETED_PROCS.ElementAt(i - 3).Value.response.ToString());
				FO.WriteTo (16, i, COMPLETED_PROCS.ElementAt (i - 3).Value.turnaround.ToString ());
				FO.WriteTo (17, i, COMPLETED_PROCS.ElementAt (i - 3).Value.wait.ToString ());
			}

            //////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////

            /*RUN MULTIPROCESSOR SIMULATION HERE*/

            processTable = s.CreateProcessTable();
            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Add(processTable.ElementAt(h).Key, processTable.ElementAt(h).Value);
            COMPLETED_PROCS.Clear();
            systemTime = 0;
            
            /*MULTIPROCESSOR WITH 4 PROCESSORS*/
            mulSim.MultiProcRoundRobin(ref CPU_ready_Q, ref COMPLETED_PROCS, uniSim, 2, ref systemTime, 4);
            mulSim.MultiProcRoundRobin(ref CPU_ready_Q, ref COMPLETED_PROCS, uniSim, 4, ref systemTime, 4);
            mulSim.MultiProcRoundRobin(ref CPU_ready_Q, ref COMPLETED_PROCS, uniSim, 6, ref systemTime, 4);
            mulSim.MultiProcFCFS(ref CPU_ready_Q, ref COMPLETED_PROCS, uniSim, ref systemTime, 4);

            throughput = (COMPLETED_PROCS.Count() / (double)systemTime);

            // % time CPU was running (not including IO bursts)
            CPU_utilization = (totalCPUTime / systemTime) * 100;

            Console.WriteLine("Throughput for MultiProc 4 Proc: " + Math.Round((decimal)throughput, 5).ToString());
            Console.WriteLine("CPU Utilization for MultiProc 4 Proc: " + Math.Round((decimal)CPU_utilization, 5).ToString() + "%");

            processTable = s.CreateProcessTable();
            for (int h = 0; h < processTable.Count(); h++)
                CPU_ready_Q.Add(processTable.ElementAt(h).Key, processTable.ElementAt(h).Value);
            COMPLETED_PROCS.Clear();
            systemTime = 0;

            /*MULTIPROCESSOR WITH 8 PROCESSORS*/
            mulSim.MultiProcRoundRobin(ref CPU_ready_Q, ref COMPLETED_PROCS, uniSim, 4, ref systemTime, 8);
            mulSim.MultiProcRoundRobin(ref CPU_ready_Q, ref COMPLETED_PROCS, uniSim, 8, ref systemTime, 8);
            mulSim.MultiProcFCFS(ref CPU_ready_Q, ref COMPLETED_PROCS, uniSim, ref systemTime, 8);

            throughput = (COMPLETED_PROCS.Count() / (double)systemTime);

            // % time CPU was running (not including IO bursts)
            CPU_utilization = (totalCPUTime / systemTime) * 100;

            Console.WriteLine("Throughput for MultiProc 8 Procs: " + Math.Round((decimal)throughput, 5).ToString());
            Console.WriteLine("CPU Utilization for MultiProc 8 Procs: " + Math.Round((decimal)CPU_utilization, 5).ToString() + "%");
            
            FO.Finish();

        }
    }
}
