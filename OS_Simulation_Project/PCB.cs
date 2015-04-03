using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class PCB 
    {
        public int expectedCPUTime;                     // service time in CPU Queues
        public int remainingCPUTime;                    // remaining service time in CPU Queue
        public int expectedIOTime;                      // service time required by process in IO Queues
        public int remainingIOTime;                     // remaining service time in IO Queue
        public bool processState;                       // true = ready, false = waiting
        public int response;                            // time from request submission to first response
        public int turnaround;                          // time from submission to completion of process
        public int wait;                                // time process spends in ready queues
        public int arrivalTime;                         // time process arrives in queue
        public string name;

        public PCB(int arrivalTime, bool state, int[] CPU, int[] IO)
        {
            // CPU total time is sum of individual burst times
            foreach (int i in CPU) 
                this.expectedCPUTime += CPU[i];
            // IO total time is sum of individual burst times
            foreach (int j in IO)
                this.expectedIOTime += IO[j];

            this.remainingIOTime = expectedIOTime;
            this.remainingCPUTime = expectedCPUTime;
       
            this.processState = state;
            this.response = -1;
            this.turnaround = 0;
            this.wait = 0;
            this.arrivalTime = arrivalTime;
            this.name = "Process " + arrivalTime.ToString();
        }

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
}
