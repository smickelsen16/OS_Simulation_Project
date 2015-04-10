using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class UniprocessorAlgorithms
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantum"> time to run</param>
        /// <param name="currentProc">process being run through </param>
        /// <param name="time">current system time at start</param>
        /// <param name="CPU_ready_Q">reference to CPU ready Q in Program.cs (can be null for Multiprocessor Sim)</param>
        /// <param name="IO_Ready_Q">reference to IO ready Q in Program.cs (can be null for Multiprocessor Sim)</param>
        /// <param name="COMPLETED_PROCS">Dictionary of completed procs from Program.cs (can be null for Multiprocessor Sim)</param>
        public void Round_Robin(int quantum, KeyValuePair<int, PCB> currentProc, ref int time,
            ref Dictionary<int, PCB> CPU_ready_Q, ref Dictionary<int, PCB> IO_Ready_Q,
            ref Dictionary<int, PCB> COMPLETED_PROCS)
        {
            // make sure process has arrived at current system time
            if (time >= currentProc.Value.arrivalTime)
            {
                // calculate the response time for the current process
                if (currentProc.Value.response == -1)
                    currentProc.Value.response = time - currentProc.Value.arrivalTime;

                /************ run that process for the quantum *************/
                // check to make sure quantum isn't bigger than remaining time
                if (currentProc.Value.remainingCPUTime >= quantum)
                {
                    // subtract quantum from remainingServiceTime
                    currentProc.Value.remainingCPUTime -= quantum;
                    // update time to account for quantum
                    time += quantum;
                }
                else
                {
                    // update system time
                    time += currentProc.Value.remainingCPUTime;
                    // zero out remainingServiceTime
                    currentProc.Value.remainingCPUTime = 0;
                }

                /**********checking if process has finished**********/
                // check if current burst has finished
                if (currentProc.Value.remainingCPUTime == 0)
                {
                    // PROBLEM IS HERE!!!!
                    // remove CPU burst that just completed
                    if (currentProc.Value.CPU_bursts.Count() != 0)
                        currentProc.Value.CPU_bursts.RemoveAt(0);

                    // remove from readyQ
                    CPU_ready_Q.Remove(currentProc.Key);

                    // if a CPU burst remains, then an IO burst must also remain
                    if (currentProc.Value.CPU_bursts.Count() != 0)
                    {
                        // if still CPU bursts to run, set remainingCPUTime to next burst
                        currentProc.Value.remainingCPUTime = currentProc.Value.CPU_bursts.First();
                        // add it to IO Queue to run IO burst
                        IO_Ready_Q.Add(currentProc.Key, currentProc.Value);

                        // run IO Algorithm with first element because there should only be one element at a time
                        I_O_Algorithm(IO_Ready_Q.ElementAt(0), ref time, ref IO_Ready_Q, ref CPU_ready_Q, ref COMPLETED_PROCS);
                    }
                    // no CPU bursts remain, but an IO burst is left...
                    else if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() != 0)
                    {
                        // add it to IO Queue to run IO burst
                        IO_Ready_Q.Add(currentProc.Key, currentProc.Value);
                        // run IO Algorithm with first element because there should only be one element at a time
                        I_O_Algorithm(IO_Ready_Q.ElementAt(0), ref time, ref IO_Ready_Q, ref CPU_ready_Q, ref COMPLETED_PROCS);
                    }
                    // no CPU or IO bursts are left, process is all done
                    else if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() == 0)
                    {
                        /*STATS NOT BEING CALCULATED CORRECTLY... GETTING NEGATIVE WAIT*/
                        // turnaround is current system time - arrival time
                        currentProc.Value.turnaround = (time - currentProc.Value.arrivalTime);
                        // wait = turnaround - expSerTime
                        currentProc.Value.wait = (currentProc.Value.turnaround - (currentProc.Value.expectedIOTime + currentProc.Value.expectedCPUTime));
                        // add to COMPLETED_PROCS to be output in main
                        COMPLETED_PROCS.Add(currentProc.Key, currentProc.Value);
                    }
                }
                // if current CPU burst is not yet complete...
                else
                {
                    // remove process...
                    CPU_ready_Q.Remove(currentProc.Key);
                    // ... and add to the end of the queue, but it adds in order...
                    CPU_ready_Q.Add(currentProc.Key, currentProc.Value);
                }
                // context switch
                time += 2;
            }
            // if curent time is not equal to arrival time of process
            else
            {
                // update time to jump to point where process arrives
                time = currentProc.Value.arrivalTime;
                // recursively re-run process through RR
                Round_Robin(quantum, currentProc, ref time, ref CPU_ready_Q, ref IO_Ready_Q, ref COMPLETED_PROCS);
            }
        }



        /// <summary>
        /// Non-preemptive
        /// Runs each process to completion based on the time they arrive
        /// </summary>
        /// <param name="ReadyQueue"> list of processes to be run </param>
        public void First_Come_First_Served(KeyValuePair<int, PCB> currentProc, ref int time)
        {
            if (time >= currentProc.Value.arrivalTime)
            {
                // run the process to completion
                time += currentProc.Value.remainingCPUTime;                                                     // update system time to account for running the program 
                currentProc.Value.remainingCPUTime = 0;                                                         // process has completed

                // remove current burst
                currentProc.Value.CPU_bursts.RemoveAt(0);

                if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() == 0)
                {
                    currentProc.Value.turnaround = time - currentProc.Value.arrivalTime;                      // set turnaround time to systemTime - arrivalTime
                    currentProc.Value.wait = currentProc.Value.turnaround - (currentProc.Value.expectedCPUTime + currentProc.Value.expectedIOTime); //CPU wait is turnaround - expected service time
                    currentProc.Value.processState = false;
                    time += 2; // context switch
                    // run I/O burst
                    //I_O_Algorithm(currentProc.Value, ref time);
                }
                else
                {
                    currentProc.Value.remainingCPUTime = currentProc.Value.CPU_bursts.First();
                    // go through FCFS again or switch to IO burst if necessary
                }
            }
        }

        // how do we accrue wait time in IO queue...
        //process state must be false to get into IO Queue, when it leaves, it switches to true
        public void I_O_Algorithm(KeyValuePair<int, PCB> currProc, ref int time,
            ref Dictionary<int, PCB> IO_Queue, ref Dictionary<int, PCB> CPU_Queue,
            ref Dictionary<int, PCB> COMPLETED_PROCS)
        {
            // add IO burst time to systemTime
            time += currProc.Value.remainingIOTime;
            // finish current IO burst
            currProc.Value.remainingIOTime = 0;
            // remove from IO queue
            IO_Queue.Remove(currProc.Key);
            // remove completed burst
            currProc.Value.IO_bursts.RemoveAt(0);
            // check if IO bursts all completed; if an IO burst remains, then a CPU burst also remains
            if (currProc.Value.IO_bursts.Count() != 0)
            {
                // update the burst to next IO burst
                currProc.Value.remainingIOTime = currProc.Value.IO_bursts.ElementAt(0);
                // add back to CPU_ready_Queue
                CPU_Queue.Add(currProc.Key, currProc.Value);
                // remove from IO Queue
                IO_Queue.Remove(currProc.Key);
                // run CPU again??
            }
            // if IO bursts are done, but a CPU burst remains
            else if (currProc.Value.IO_bursts.Count() == 0 && currProc.Value.CPU_bursts.Count() != 0)
            {

            }
            // if no IO or CPU bursts remain then process is done
            else if (currProc.Value.IO_bursts.Count() == 0 && currProc.Value.CPU_bursts.Count() == 0)
            {

            }

            // add back to CPU ready queue
            // context switch
            time += 2;
        }

        /// <summary>
        /// Preemptive
        /// Completes the process with the shortest remaining time
        /// Will interrupt the current process if another process has a shorter remaining time 
        /// </summary>
        /// <param name="processes"> list of processes to be run </param>
        public void Shortest_Remaining_Time(Dictionary<int, PCB> readyQ, ref int time)
        {
            PCB currentProc = null;
            for (int i = 0; i < readyQ.Count(); i++)
            {
                if (currentProc == null)
                    currentProc = readyQ.ElementAt(i).Value;
                if (time >= currentProc.arrivalTime)
                {
                    currentProc.remainingCPUTime -= 1;              // run the process for one unit of time
                    time += 1;                                      // add 1 unit of time to systemTime

                    // check if a shorter process is out there...
                    for (int j = 1; j < readyQ.Count(); j++)
                    {
                        if (currentProc.remainingCPUTime > readyQ.ElementAt(j).Value.remainingCPUTime)
                            currentProc = readyQ.ElementAt(j).Value;
                        else
                            i--;
                    }

                    if (currentProc.remainingCPUTime == 0)
                    {
                        currentProc.turnaround = time - currentProc.arrivalTime;          // set turnaround time to systemTime - arrivalTime
                        currentProc.wait = currentProc.turnaround - currentProc.expectedCPUTime;
                        currentProc.processState = false;
                        time += 2; // context switch
                        // run I/O burst
                        //I_O_Algorithm(readyQ.ElementAt(i).Value, ref time);
                    }
                }
            }
        }
    }
}
