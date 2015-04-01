﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class ProcessorAlgorithms
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
        /// Runs each process for the specified time quantum and then switches to the next arrived process
        /// </summary>
        /// <param name="quantum"> time allocated to each process per RR cycle </param>
        /// <param name="ReadyQueue"> list of processes to be run </param>
        public void Round_Robin(int quantum, Tuple<int, PCB> currentProc, Stopwatch time)
        {
            // calculate the response time for the current process
            currentProc.Item2.response = time.ElapsedMilliseconds - currentProc.Item2.arrivalTime;

            // run that process for the quantum 
            if (currentProc.Item2.remainingServiceTime >= quantum)                    // check to make sure quantum isn't bigger than remaining time
            {
                currentProc.Item2.remainingServiceTime -= quantum;                    // subtract quantum from remainingServiceTime
                //time.ElapsedMilliseconds += quantum;                                //  how to update system time with a stopwatch??
            }
            else
            {
                //time.ElapsedMilliseconds += currentProc.remainingServiceTime;       // udate system time-- how with a stopwatch??
                currentProc.Item2.remainingServiceTime = 0;                           // zero out remainingServiceTime
            }
        }

        /// <summary>
        /// Non-preemptive
        /// Runs each process to completion based on the time they arrive
        /// </summary>
        /// <param name="ReadyQueue"> list of processes to be run </param>
        public void First_Come_First_Served(Tuple<int, PCB> currentProc, Stopwatch time)
        {
            // set the processes response time to the systemTime - arrivalTime
            currentProc.Item2.response = time.ElapsedMilliseconds - currentProc.Item2.arrivalTime;

            // run the process to completion
            //systemTime += currentProc.expectedServiceTime;                                              // update system time to account for running the program 
            currentProc.Item2.remainingServiceTime = 0;                                                   // process has completed
            currentProc.Item2.execution = time.ElapsedMilliseconds - currentProc.Item2.response;          // update currentProc execution time to systemTime - currentProc.response
            currentProc.Item2.turnaround = time.ElapsedMilliseconds - currentProc.Item2.arrivalTime;      // set turnaround time to systemTime - arrivalTime
            currentProc.Item2.wait = currentProc.Item2.response;                                          // how to calculate wait time for  each process
        }

        /// <summary>
        /// Non-Preemptive
        /// Runs & completes shortest processes first
        /// Will preempt current process if a shorter process arrives during execution
        /// </summary>
        /// <param name="ReadyQueue"> list of processes to be run </param>
        public void Shortest_Process_Next(Dictionary<int, PCB> ReadyQueue)
        {
           // same as FCFS, but runs shortest procs first
        }

        /// <summary>
        /// Preemptive
        /// Completes the process with the shortest remaining time
        /// Will interrupt the current process if another process has a shorter remaining time 
        /// </summary>
        /// <param name="processes"> list of processes to be run </param>
        public void Shortest_Remaining_Time(Tuple<int, PCB> currentProc, Stopwatch time)
        {
            // same as FCFS, but is constantly checking if a shorter process is out there...
        }
    }


}