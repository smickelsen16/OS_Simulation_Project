using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class PCB
    {
        public int expectedCPUTime = 0;                     // service time in CPU Queues (total of all bursts)
        public List<int> CPU_bursts;                        // keeps track of individual CPU burst times
        public int remainingCPUTime = 0;                    // remaining service time in CPU Queue (of 1 burst)
        public int expectedIOTime = 0;                      // service time required by process in IO Queues
        public List<int> IO_bursts;                         // keeps track of individual IO burst times (total of all bursts)
        public int remainingIOTime = 0;                     // remaining service time in IO Queue (of 1 burst)
        public bool processState;                           // true = CPU ready, false = IO ready
        public int response = 0;                            // time from request submission to first response
        public int turnaround = 0;                          // time from submission to completion of process in CPU
        
        public int wait = 0;                                // time process spends in CPU ready queue
       
        public int arrivalTime = 0;                         // time process arrives in CPU queue
        
        public string name;

        public int queueCounter;                            // counts how many queues process has been through

        public PCB(int arrivalTime, bool state, List<int> CPU, List<int> IO)
        {
            // CPU total time is sum of individual burst times
            foreach (int i in CPU)
                this.expectedCPUTime += i;
            // IO total time is sum of individual burst times
            foreach (int j in IO)
                this.expectedIOTime += j;

            CPU_bursts = CPU;
            IO_bursts = IO;

            this.remainingIOTime = IO_bursts.First();      // remaining time keeps track of the time of bursts
            this.remainingCPUTime = CPU_bursts.First();      

            this.processState = state;
            this.response = -1;
            this.turnaround = 0;
            this.arrivalTime = arrivalTime;
            this.wait = 0;
            this.name = "Process " + arrivalTime.ToString();

            this.queueCounter = 0;
        }

        public override string ToString()
        {
            return name + "'s Stats\n" +
                "Expected CPU Time: " + expectedCPUTime.ToString() +
                "\nExpected IO Time: " + expectedIOTime.ToString() +
                "\nTotal IO & CPU Service Time: " + (expectedCPUTime+expectedIOTime).ToString() +
                "\nArrival Time: " + arrivalTime.ToString() +
                "\nProcess State: " + processState.ToString() +
                "\nResponse: " + response.ToString() +
                "\nTurnaround: " + turnaround.ToString() +
                "\nWait: " + wait.ToString();
        }
    }
}
