﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class UniprocessorAlgorithms
    {
        ///// <summary>
        ///// Preemptive (?)
        ///// Runs each process for the specified time quantum and then switches to the next arrived process
        ///// </summary>
        ///// <param name="quantum"> time allocated to each process per RR cycle </param>
        ///// <param name="ReadyQueue"> list of processes to be run </param>
        //public void Round_Robin(int quantum, Dictionary<int, PCB> ReadyQueue)
        //{
        //    PCB currentProc;
        //    Dictionary<int, PCB> CompletedProcs = new Dictionary<int, PCB>();
        //    int counter = 0;
        //    do
        //    {
        //        // find the earliest arriving process 
        //        if (counter < ReadyQueue.Count())
        //            currentProc = ReadyQueue.ElementAt(counter).Value;
        //        else
        //            currentProc = ReadyQueue.First().Value;                          // if the counter is bigger than the Queue, just get the first value

        //        // set the processes response time to the systemTime - arrivalTime
        //        if (currentProc.response == -1)
        //        {
        //            if (currentProc.arrivalTime <= systemTime)                      // make sure a new process has arrived
        //                currentProc.response = systemTime - currentProc.arrivalTime;
        //            else
        //                // need to make sure this counts as CPU idle time when the system has to wait for a process to arrive... or does it run previous process again?
        //                systemTime += (currentProc.arrivalTime - systemTime);       // if no new process, update the system time to show that time has passed where the CPU was idle
        //        }

        //        // run that process for the quantum 
        //        if (currentProc.remainingServiceTime >= quantum ) {                 // check to make sure quantum isn't bigger than remaining time
        //            currentProc.remainingServiceTime -= quantum;                    // subtract quantum from remainingServiceTime
        //            systemTime += quantum;                                          //  update system time 
        //        }
        //        else { 
        //            systemTime += currentProc.remainingServiceTime;     // udate system time
        //            currentProc.remainingServiceTime = 0;                           // zero out remainingServiceTime
        //        }


        //        // check to see if process has finished
        //        if (currentProc.remainingServiceTime == 0)
        //        {
        //            currentProc.execution = systemTime;                                         // set execution time to current system time
        //            currentProc.turnaround = systemTime - currentProc.arrivalTime;              // set turnaround time to systemTime - arrivalTime
        //            currentProc.wait = currentProc.turnaround - currentProc.expectedServiceTime;                                           
        //            // write stat information to file and remove process from ReadyQueue & add to CompletedProcs
        //            if (counter < ReadyQueue.Count())             // check if counter > # procs left
        //            {
        //                CompletedProcs.Add(ReadyQueue.ElementAt(counter).Key, ReadyQueue.ElementAt(counter).Value);         // add to completed procs
        //                ReadyQueue.Remove(ReadyQueue.ElementAt(counter).Key);                                               // remove from readyQueue
        //                counter--;                                                                                          // if a proc is removed, decrement the counter
        //            }
        //            else
        //            {
        //                CompletedProcs.Add(ReadyQueue.First().Key, ReadyQueue.First().Value);                    // add to completed procs
        //                ReadyQueue.Remove(ReadyQueue.First().Key);                                               // remove from readyQueue
        //                counter--;                                                                               // if a proc is removed, decrement the counter
        //            }

        //        }

        //        counter++;                                                          // update the counter variable to get the next key/value pair

        //    } while (ReadyQueue.Count() != 0);

        //    for (int i = 0; i < CompletedProcs.Count(); i++)
        //        Console.WriteLine(CompletedProcs.ElementAt(i).Value.ToString() + "\n");

        //    Console.WriteLine("Round Robin Complete\n");
        //}

        /// <summary>
        /// Preemptive
        /// Runs for time quantum and then passes it along
        /// </summary>
        /// <param name="quantum"></param>
        /// <param name="currentProc"></param>
        /// <param name="time"></param>
       
        // do we need to pass it the Key/Value Pair or can we just give it a PCB?
        // need to add/remove prcs from CPU & IO Queues from here when necessary...
        public void Round_Robin(int quantum, KeyValuePair<int, PCB> currentProc, ref int time)
        {
            if (time >= currentProc.Value.arrivalTime)
            {
                // calculate the response time for the current process
                if (currentProc.Value.response == -1)
                {
                    currentProc.Value.response = time - currentProc.Value.arrivalTime;
                }

                // run that process for the quantum 
                if (currentProc.Value.remainingCPUTime >= quantum)                    // check to make sure quantum isn't bigger than remaining time
                {
                    currentProc.Value.remainingCPUTime -= quantum;                    // subtract quantum from remainingServiceTime
                    time += quantum;
                }
                else
                {
                    time += currentProc.Value.remainingCPUTime;                       // udate system Time
                    currentProc.Value.remainingCPUTime = 0;                           // zero out remainingServiceTime
                }

                // check if process finished and update stats accordingly; will pass to appropriate queues in Simulation.cs
                if (currentProc.Value.remainingCPUTime == 0)
                {
                    // remove from readyQ

                    // remove CPU burst that just completed
                    currentProc.Value.CPU_bursts.RemoveAt(0);

                    // check to see if CPU burst queue is empty...
                    if (currentProc.Value.CPU_bursts.Count() == 0 && currentProc.Value.IO_bursts.Count() == 0)
                    {
                        currentProc.Value.turnaround = (time - currentProc.Value.arrivalTime);        // turnaround is current system time - arrival time
                        currentProc.Value.processState = false;     // set state to false so it can go into IO Queue
                        currentProc.Value.wait = (currentProc.Value.turnaround - (currentProc.Value.expectedIOTime + currentProc.Value.expectedCPUTime));      // wait = turnaround - expSerTime
                    }
                    else
                        // if still CPU bursts to run, set remainingCPUTime to next burst
                        currentProc.Value.remainingCPUTime = currentProc.Value.CPU_bursts.First();
                    //add to back of readyQ to be run through again
                    time += 2; // context switch
                    // run I/O burst
                    I_O_Algorithm(currentProc.Value, ref time);
                }
            }
            else
            {
                time = currentProc.Value.arrivalTime;
                Round_Robin(quantum, currentProc, ref time);
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
                    currentProc.Value.wait = currentProc.Value.turnaround - (currentProc.Value.expectedCPUTime+currentProc.Value.expectedIOTime); //CPU wait is turnaround - expected service time
                    currentProc.Value.processState = false;
                    time += 2; // context switch
                    // run I/O burst
                    I_O_Algorithm(currentProc.Value, ref time);
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
        public void I_O_Algorithm(PCB currProc, ref int time)
        {
            // add IO burst time to systemTime
            time += currProc.remainingIOTime;
            currProc.remainingIOTime = 0;
            currProc.processState = true;
            time += 2; // context switch
            // remove from IO queue
            // update the burst
            // add back to CPU ready queue
        }

        /// <summary>
        /// Preemptive
        /// Completes the process with the shortest remaining time
        /// Will interrupt the current process if another process has a shorter remaining time 
        /// </summary>
        /// <param name="processes"> list of processes to be run </param>
        public void Shortest_Remaining_Time(Dictionary<int,PCB> readyQ, ref int time)
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
                        I_O_Algorithm(readyQ.ElementAt(i).Value, ref time);
                    }
                }
            }
        }
    }
}
