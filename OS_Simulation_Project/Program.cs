using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    /// <summary>
    /// holds a dictionary mapping of PID's and PCB's
    /// the simulation will pull processes from here to run 
    /// </summary>
    class ProcessTable
    {
        ProcessTable(Dictionary<int, PCB> d) { this.dictionary = d; }
        Dictionary<int, PCB> dictionary;            // dictionary of PID's (KEY, int) and PCB's (VALUE)
    }

    /// <summary>
    /// holds all stat info for each individual process
    /// </summary>
    class PCB
    {
        public PCB(double expSerT, bool state, double arrive) {
            this.execution = 0; 
            this.expectedServiceTime = expSerT;
            this.remainingServiceTime = expSerT;
            this.processState = state; 
            this.response = -1; 
            this.turnaround = 0;
            this.wait = 0; 
            this.arrivalTime = arrive;
            this.name = "Process " + arrivalTime.ToString();
        }

        public double execution;                           // time spent in execution thus far
        public double expectedServiceTime;                 // service time required by process
        public double remainingServiceTime;                // keeps track of remaining service time as process goes through queue
        public bool processState;                          // true = ready, false = waiting
        public double response;                            // time from request submission to first response
        public double turnaround;                          // time from submission to completion of process
        public double wait;                                // time process spends in ready queues
        public double arrivalTime;                         // time process arrives in queue
        public string name;

        public override string ToString()
        {
            return name + "'s Stats\n" +
                "Expected Service Time: " + expectedServiceTime.ToString() +
                "\nArrival Time: " + arrivalTime.ToString() +
                "\nProcess State: " + processState.ToString() +
                "\nResponse: " + response.ToString() +
                "\nTurnaround: " + turnaround.ToString() +
                "\nWait: " + wait.ToString();
        }

    }

    /// <summary>
    /// where all the magic will happen
    /// </summary>
    class Simulation
    {
        //public ProcessTable table;                       // list of all processes & associated info & ID's


        static void Main()
        {
            double throughput;                          // amount of processes completed in a given time
            double CPU_utilization;                     // % time CPU is executing

            Dictionary<int, PCB> processTable = new Dictionary<int, PCB>();
            processTable.Add(0, new PCB(6, true, 0));
            processTable.Add(1, new PCB(8, true, 4));
            processTable.Add(2, new PCB(4, true, 7));
            processTable.Add(3, new PCB(4, true, 8));
            processTable.Add(4, new PCB(2, true, 10));
            processTable.Add(5, new PCB(7, true, 15));
            processTable.Add(6, new PCB(5, true, 17));

            UniprocessorAlgorithms uniSim = new UniprocessorAlgorithms();

            uniSim.First_Come_First_Served(processTable);                         // FCFS works correctly!!
            //throughput = readyQ.Count() / uniSim.systemTime;              // calculate throughput (# procs/system time)
            //Console.WriteLine("Throughput for FCFS: " + throughput.ToString() + " processes/system time\n");

            uniSim.Round_Robin(4,processTable);                                   // issues when a process arrives after quantum is over....
            //throughput = readyQ.Count() / uniSim.systemTime;            // calculate throughput (# procs/system time)
            //Console.WriteLine("Throughput for Round Robin: " + throughput.ToString() + " processes/system time\n");
        }
    }
}
