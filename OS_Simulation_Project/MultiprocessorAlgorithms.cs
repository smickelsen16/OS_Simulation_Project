using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OS_Simulation_Project
{
    class MultiprocessorAlgorithms
    {
        //List<Thread> Processors = new List<Thread>();
        //List<Queue<KeyValuePair<int, PCB>>> queueList;
        //int wesley = 0;
        //List<ThreadInheriter> tiList;
        //bool executing = true;
        //List<Dictionary<int, PCB>> queueElement;
        //int finished = 0;

        //public void MultiProcRoundRobin(Dictionary<int, PCB> procs, 
        //    ref Dictionary<int, PCB> CPU_ready_Q, ref Dictionary<int, PCB> COMPLETED_PROCS, 
        //    Dictionary<int, PCB> rq, UniprocessorAlgorithms u, int quantum, ref int time, int procNum)
        //{
        //    int i = 0;
        //    queueElement[wesley] = new Dictionary<int,PCB>();

        //    do
        //    {
        //        if (rq.Count() != 0)
        //        {
        //            if (Processors.Count() < procNum)
        //            {
        //                tiList.Add(new ThreadInheriter(rq.First().Key));
        //                Processors.Add(tiList[i].t = new Thread(() => u.Round_Robin(quantum, rq.First(), ref time, ref CPU_ready_Q, ref COMPLETED_PROCS)));
        //                Processors.ElementAt(i).Start();
        //                //rq.Peek().Value.queueCount++;
        //                queueElement[/*rq.Peek.Value.queueCount - 1*/].Add(rq.First().Key, rq.Remove(0));
        //                i++;
        //            }
        //            else
        //            {
        //                while (executing)
        //                {
        //                    for (int j = 0; j < procNum; j++)
        //                    {
        //                        if (!Processors.ElementAt(j).IsAlive)
        //                        {
        //                            if (procs.ElementAt(j).Value.remainingCPUTime != 0)
        //                            {
        //                                for (int k = 0; k < procNum; k++)
        //                                    if (queueElement[wesley].ElementAt(k).Key == tiList[j].processID)
        //                                    {
        //                                        queueList[wesley].Enqueue(queueElement[wesley].ElementAt(k));
        //                                        queueElement[wesley].Remove(tiList[j].processID);
        //                                        if (//queueCount == 1)
        //                                            Processors.Add(new Thread(() => MultiProcRoundRobin(procs, ref CPU_ready_Q, ref COMPLETED_PROCS, queueList[wesley], u, quantum + 3, time, procNum)));
        //                                        else if (//queueCount == 2)
        //                                            Processors.Add(new Thread(() => MultiProcFCFS(procs, queueList[wesley], u, ref time, procNum)));
        //                                    }
        //                            }
        //                            else
        //                                finished++;
        //                            tiList[j] = new ThreadInheriter(rq.Peek().Key);
        //                            Processors.Insert(j, tiList[j].t = new Thread(() => u.Round_Robin(quantum, rq.Peek(), ref time)));
        //                            Processors.ElementAt(j).Start();
        //                            queueElement[wesley].Add(rq.Peek().Key, rq.Dequeue().Value);
        //                            executing = false;
        //                            break;
        //                        }
        //                    }
        //                }
        //                executing = true;
        //            }
        //        }
        //    } while (finished != procs.Count());

        //}

        //public void MultiProcFCFS(Dictionary<int, PCB> procs, Queue<KeyValuePair<int, PCB>> rq, UniprocessorAlgorithms u, int time, int procNum)
        //{
        //    if (rq.Count() != 0)                                        // Check that ready queue isn't empty
        //    {
        //        for (int i = 0; i < rq.Count(); i++)                    // Loop through each processes in ready queue
        //        {
        //            if (Processors.Count() < procNum)                   // Check if too many threads have been made
        //            {
        //                Processors.Add(new Thread(() => u.First_Come_First_Served(procs.ElementAt(rq.Peek()), ref time, ref CPU_ready_Q, ref COMPLETED_PROCS)));
        //                Processors.ElementAt(i).Start();
        //                rq.Dequeue();                                   // Remove process from ready queue                                      // Need a way to add them back to queue when finished
        //            }
        //            else
        //            {
        //                while (executing)
        //                {
        //                    for (int j = 0; j < procNum; j++)
        //                    {
        //                        if (!Processors.ElementAt(j).IsAlive)
        //                        {
        //                            rq.Enqueue(procs.ElementAt(j).Key); //FIX ME
        //                            Processors.Insert(j, new Thread(() => u.First_Come_First_Served(procs.ElementAt(rq.Peek()), ref time, ref CPU_ready_Q, ref COMPLETED_PROCS)));
        //                            Processors.ElementAt(j).Start();
        //                            rq.Dequeue();
        //                            executing = false;
        //                            break;
        //                        }
        //                    }
        //                }
        //                executing = true;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// NOT NECESSARY
        ///// </summary>
        ///// <param name="procs"></param>
        ///// <param name="rq"></param>
        ///// <param name="u"></param>
        ///// <param name="time"></param>
        ///// <param name="procNum"></param>
        //public void MultiProcSRT(Dictionary<int, PCB> procs, Queue<int> rq, UniprocessorAlgorithms u, int time, int procNum)
        //{
        //    if (rq.Count() != 0)                                        // Check that ready queue isn't empty
        //    {
        //        for (int i = 0; i < rq.Count(); i++)                    // Loop through each processes in ready queue
        //        {
        //            if (Processors.Count() < procNum)                   // Check if too many threads have been made
        //            {
        //                Processors.Add(new Thread(() => u.Shortest_Remaining_Time(procs, ref time)));
        //                Processors.ElementAt(i).Start();
        //                rq.Dequeue();                                   // Remove process from ready queue                                      // Need a way to add them back to queue when finished
        //            }
        //            else
        //            {
        //                while (executing)
        //                {
        //                    for (int j = 0; j < procNum; j++)
        //                    {
        //                        if (!Processors.ElementAt(j).IsAlive)
        //                        {
        //                            rq.Enqueue(procs.ElementAt(j).Key); //FIX ME
        //                            Processors.Insert(j, new Thread(() => u.Shortest_Remaining_Time(procs, ref time)));
        //                            Processors.ElementAt(j).Start();
        //                            rq.Dequeue();
        //                            executing = false;
        //                            break;
        //                        }
        //                    }
        //                }
        //                executing = true;
        //            }
        //        }
        //    }
        //}
    }
}
