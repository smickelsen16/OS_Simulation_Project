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
            ref Dictionary<int, PCB> CPU_ready_Q, ref Dictionary<int, PCB> COMPLETED_PROCS)
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
                    // remove burst that just finished
                    if (currentProc.Value.CPU_bursts.Count() != 0)
                        currentProc.Value.CPU_bursts.RemoveAt(0);

                    // if a CPU burst remains, then an IO burst must also remain
                    if (currentProc.Value.CPU_bursts.Count() != 0)
                    {
                        // if still CPU bursts to run, set remainingCPUTime to next burst
                        currentProc.Value.remainingCPUTime = currentProc.Value.CPU_bursts.First();

                        // run IO Algorithm 
                        I_O_Algorithm(currentProc, ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);
                    }

                    // no CPU bursts remain, but an IO burst is left...
                    else if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() != 0)
                        // run IO Algorithm with first element because there should only be one element at a time
                        I_O_Algorithm(currentProc, ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);

                    // no CPU or IO bursts are left, process is all done
                    else if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() == 0)
                    {
                        // turnaround is current system time - arrival time
                        currentProc.Value.turnaround = (time - currentProc.Value.arrivalTime);
                        // wait = turnaround - expSerTime
                        currentProc.Value.wait = currentProc.Value.turnaround - (currentProc.Value.expectedIOTime + currentProc.Value.expectedCPUTime);
                        if (!COMPLETED_PROCS.ContainsKey(currentProc.Key))
                            // add to COMPLETED_PROCS to be output in main
                            COMPLETED_PROCS.Add(currentProc.Key, currentProc.Value);
                        // remove from CPU_read_Q
                        CPU_ready_Q.Remove(currentProc.Key);
                    }
                }
                // if current CPU burst is not yet complete...
                // context switch
                time += 2;
            }
            // if curent time is not equal to arrival time of process
            else
            {
                // update time to jump to point where process arrives
                time = currentProc.Value.arrivalTime;
                // recursively re-run process through RR
                Round_Robin(quantum, currentProc, ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }
        }



        /// <summary>
        /// Non-preemptive
        /// Runs each process to completion based on the time they arrive
        /// </summary>
        /// <param name="ReadyQueue"> list of processes to be run </param>
        public void First_Come_First_Served(KeyValuePair<int, PCB> currentProc, ref int time,
            ref Dictionary<int, PCB> CPU_ready_Q, ref Dictionary<int, PCB> COMPLETED_PROCS)
        {
            if (time >= currentProc.Value.arrivalTime)
            {
                /******* run the process to completion *****/
                // update system time to account for running the program 
                time += currentProc.Value.remainingCPUTime;
                currentProc.Value.remainingCPUTime = 0;

                // remove current burst
                if (currentProc.Value.CPU_bursts.Count() != 0)
                    currentProc.Value.CPU_bursts.RemoveAt(0);

                // if CPU bursts are left then an IO burst is left
                if (currentProc.Value.CPU_bursts.Count() != 0)
                {
                    // set burst to next burst
                    currentProc.Value.remainingCPUTime = currentProc.Value.CPU_bursts.First();
                    // run IO Algorithm 
                    I_O_Algorithm(currentProc, ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);
                    // run FCFS with n

                }

                // CPU bursts are done but one IO burst remains
                else if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() != 0)
                    // run IO Algorithm with first element because there should only be one element at a time
                    I_O_Algorithm(currentProc, ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);


                // check to see if process is done
                if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() == 0)
                {
                    // calculate turnaround
                    currentProc.Value.turnaround = time - currentProc.Value.arrivalTime;
                    //CPU wait is turnaround - expected service time
                    currentProc.Value.wait = currentProc.Value.turnaround - (currentProc.Value.expectedCPUTime + currentProc.Value.expectedIOTime);

                    if (!COMPLETED_PROCS.ContainsKey(currentProc.Key))
                        // add to COMPLETED_PROCS to be output in main
                        COMPLETED_PROCS.Add(currentProc.Key, currentProc.Value);
                    // remove from CPU_read_Q
                    CPU_ready_Q.Remove(currentProc.Key);
                }
                // context switch
                time += 2;
            }
            else
            {
                // update time to jump to point where process arrives
                time = currentProc.Value.arrivalTime;
                // recursively re-run process through FCFS
                First_Come_First_Served(currentProc, ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);
            }

        }

        // how do we accrue wait time in IO queue...
        //process state must be false to get into IO Queue, when it leaves, it switches to true
        public void I_O_Algorithm(KeyValuePair<int, PCB> currProc, ref int time,
            ref Dictionary<int, PCB> CPU_Queue, ref Dictionary<int, PCB> COMPLETED_PROCS)
        {
            // add IO burst time to systemTime
            time += currProc.Value.remainingIOTime;
            // finish current IO burst
            currProc.Value.remainingIOTime = 0;
            if (currProc.Value.IO_bursts.Count() != 0)
                // remove completed burst
                currProc.Value.IO_bursts.RemoveAt(0);
            // check if IO bursts all completed; if an IO burst remains, then a CPU burst also remains
            if (currProc.Value.IO_bursts.Count() != 0)
            {
                // update the burst to next IO burst
                currProc.Value.remainingIOTime = currProc.Value.IO_bursts.ElementAt(0);
                // run CPU again??
            }
            // if IO bursts are done, but a CPU burst remains
            else if (currProc.Value.IO_bursts.Count() == 0 && currProc.Value.CPU_bursts.Count() != 0)
            {
                // do nothing
            }
            // if no IO or CPU bursts remain then process is done
            if (currProc.Value.IO_bursts.Count() == 0 && currProc.Value.CPU_bursts.Count() == 0)
            {
                // turnaround is current system time - arrival time
                currProc.Value.turnaround = (time - currProc.Value.arrivalTime);
                // wait = turnaround - expSerTime
                currProc.Value.wait = currProc.Value.turnaround - (currProc.Value.expectedIOTime + currProc.Value.expectedCPUTime);
                if (!COMPLETED_PROCS.ContainsKey(currProc.Key))
                    // add to COMPLETED_PROCS to be output in main
                    COMPLETED_PROCS.Add(currProc.Key, currProc.Value);
                // remove from CPU_read_Q
                CPU_Queue.Remove(currProc.Key);
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
        public void Shortest_Remaining_Time(ref Dictionary<int, PCB> CPU_ready_Q, ref int time,
             ref Dictionary<int, PCB> COMPLETED_PROCS)
        {
            PCB currentProc = null;
            bool newProcFound = false;
            for (int i = 0; i < CPU_ready_Q.Count(); i++)
            {
                if (currentProc == null)
                    currentProc = CPU_ready_Q.ElementAt(i).Value;

                if (time >= currentProc.arrivalTime)
                {
                    currentProc.remainingCPUTime -= 1;              // run the process for one unit of time
                    time += 1;                                      // add 1 unit of time to systemTime

                    // check if a shorter process is out there...
                    if (currentProc.remainingCPUTime != 0)
                    {
                        for (int j = 1; j < CPU_ready_Q.Count(); j++)
                        {
                            if (currentProc.remainingCPUTime > CPU_ready_Q.ElementAt(j).Value.remainingCPUTime)
                            {
                                currentProc = CPU_ready_Q.ElementAt(j).Value;
                                newProcFound = true;
                            }
                        }
                        if (newProcFound)
                            // recursively call to run again with j as the starting index
                            Shortest_Remaining_Time(ref CPU_ready_Q, ref time, ref COMPLETED_PROCS);
                    }

                    if (currentProc.remainingCPUTime == 0)
                    {
                        // remove burst that just finished
                        if (currentProc.CPU_bursts.Count() != 0)
                            currentProc.CPU_bursts.RemoveAt(0);

                        // if CPU bursts are left then an IO burst is left
                        if (currentProc.CPU_bursts.Count() != 0)
                        {
                            // set burst to next burst
                            currentProc.remainingCPUTime = currentProc.CPU_bursts.First();
                            // run IO Algorithm 
                            I_O_Algorithm(CPU_ready_Q.ElementAt(i), ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);
                            // run FCFS with n

                        }

                        // CPU bursts are done but one IO burst remains
                        else if (currentProc.CPU_bursts.Count() == 0 && currentProc.IO_bursts.Count() != 0)
                            // run IO Algorithm with first element because there should only be one element at a time
                            I_O_Algorithm(CPU_ready_Q.ElementAt(i), ref time, ref CPU_ready_Q, ref COMPLETED_PROCS);


                        // check to see if process is done
                        if (currentProc.CPU_bursts.Count() == 0 && currentProc.IO_bursts.Count() == 0)
                        {
                            // calculate turnaround
                            currentProc.turnaround = time - currentProc.arrivalTime;
                            //CPU wait is turnaround - expected service time
                            currentProc.wait = currentProc.turnaround - (currentProc.expectedCPUTime + currentProc.expectedIOTime);
                            if (!COMPLETED_PROCS.ContainsKey(CPU_ready_Q.ElementAt(i).Key))
                                // add to COMPLETED_PROCS to be output in main
                                COMPLETED_PROCS.Add(CPU_ready_Q.ElementAt(i).Key, CPU_ready_Q.ElementAt(i).Value);
                            // remove from CPU_read_Q
                            CPU_ready_Q.Remove(CPU_ready_Q.ElementAt(i).Key);
                        }
                        // context switch
                        time += 2;
                    }
                }

                else
                {
                    // update time to jump to point where process arrives
                    time = currentProc.arrivalTime;
                    // recursively re-run process through RR
                    Shortest_Remaining_Time(ref CPU_ready_Q, ref time, ref COMPLETED_PROCS);
                }
            }
        }
    }
}
