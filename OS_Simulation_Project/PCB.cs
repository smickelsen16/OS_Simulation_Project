using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class PCB 
    {
        public double execution;                           // time spent in execution thus far
        public double expectedServiceTime;                 // service time required by process
        public double remainingServiceTime;                // keeps track of remaining service time as process goes through queue
        public bool processState;                          // true = ready, false = waiting
        public double response;                            // time from request submission to first response
        public double turnaround;                          // time from submission to completion of process
        public double wait;                                // time process spends in ready queues
        public double arrivalTime;                         // time process arrives in queue
        public string name;

        public PCB(double expSerT, bool state, double arrive)
        {
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
