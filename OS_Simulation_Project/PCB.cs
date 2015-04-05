using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class PCB
    {
        public int expectedCPUTime = 0;                     // service time in CPU Queues
        public int remainingCPUTime = 0;                    // remaining service time in CPU Queue
        public int expectedIOTime = 0;                      // service time required by process in IO Queues
        public int remainingIOTime = 0;                     // remaining service time in IO Queue
        public bool processState;                           // true = CPU ready, false = IO ready
        public int response = 0;                            // time from request submission to first response
        public int CPUturnaround = 0;                       // time from submission to completion of process in CPU
        public int IOturnaround = 0;                        // time from submission to completion of process in IO
        public int CPUwait = 0;                             // time process spends in CPU ready queue
        public int IOwait = 0;                              // time process spends in IO ready queue
        public int CPUarrivalTime = 0;                      // time process arrives in CPU queue
        public int IOarrivalTime = 0;                       // time process arrives in IO queue
        public string name;

        public PCB(int arrivalTime, bool state, List<int> CPU, List<int> IO)
        {
            // CPU total time is sum of individual burst times
            foreach (int i in CPU)
                this.expectedCPUTime += i;
            // IO total time is sum of individual burst times
            foreach (int j in IO)
                this.expectedIOTime += j;

            this.remainingIOTime = expectedIOTime;
            this.remainingCPUTime = expectedCPUTime;

            this.processState = state;
            this.response = -1;
            this.CPUturnaround = 0;
            this.CPUarrivalTime = arrivalTime;
            this.CPUwait = 0;
            this.IOarrivalTime = 0;
            this.IOturnaround = 0;
            this.IOwait = 0;
            this.name = "Process " + arrivalTime.ToString();
        }

        public override string ToString()
        {
            return name + "'s Stats\n" +
                "Expected CPU Time: " + expectedCPUTime.ToString() +
                "\nExpected IO Time: " + expectedIOTime.ToString() +
                "\nCPU Arrival Time: " + CPUarrivalTime.ToString() +
                "\nIO Arrival Time: " + IOarrivalTime.ToString() +
                "\nProcess State: " + processState.ToString() +
                "\nResponse: " + response.ToString() +
                "\nCPU Turnaround: " + CPUturnaround.ToString() +
                "\nIO Turnaround: " + IOturnaround.ToString() +
                "\nIO Wait: " + IOwait.ToString() +
                "\nCPU Wait: " + CPUwait.ToString();
        }
    }
}
