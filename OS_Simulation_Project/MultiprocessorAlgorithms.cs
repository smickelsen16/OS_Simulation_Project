using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OS_Simulation_Project
{
    class MultiprocessorAlgorithms
    {
        List<Thread> Processors = new List<Thread>();
        bool executing = true;
        public void MultiProcRoundRobin(Dictionary<int, PCB> procs, Queue<KeyValuePair<int, PCB>> rq, UniprocessorAlgorithms u, int quantum, int time, int procNum)
        {
            int i = 0;
            int finished = 0;
            Dictionary<int, PCB> queueElement;

            do
            {
                if (rq.Count() != 0)
                {
                    if (Processors.Count() < procNum)
                    {
                        Processors.Add(new Thread(() => u.Round_Robin(quantum, rq.Peek(), ref time)));
                        Processors.ElementAt(i).Start();
                        queueElement.Add(rq.Dequeue());
                        i++;
                    }
                    else
                    {
                        while (executing)
                        {
                            for (int j = 0; j < procNum; j++)
                            {
                                if (!Processors.ElementAt(j).IsAlive)
                                {
                                    if (procs.ElementAt(j).Value.remainingCPUTime != 0)
                                        rq.Enqueue(queueElement);//FIX ME
                                    else
                                        finished++;
                                    Processors.Insert(j, new Thread(() => u.Round_Robin(quantum, rq.Peek(), ref time)));
                                    Processors.ElementAt(j).Start();
                                    rq.Dequeue();
                                    executing = false;
                                    break;
                                }
                            }
                        }
                        executing = true;
                    }
                }
            } while (finished != procs.Count() - procNum);

        }

        public void MultiProcFCFS(Dictionary<int, PCB> procs, Queue<KeyValuePair<int, PCB>> rq, UniprocessorAlgorithms u, int time, int procNum)
        {
            if (rq.Count() != 0)                                        // Check that ready queue isn't empty
            {
                for (int i = 0; i < rq.Count(); i++)                    // Loop through each processes in ready queue
                {
                    if (Processors.Count() < procNum)                   // Check if too many threads have been made
                    {
                        Processors.Add(new Thread(() => u.First_Come_First_Served(procs.ElementAt(rq.Peek()), ref time)));
                        Processors.ElementAt(i).Start();
                        rq.Dequeue();                                   // Remove process from ready queue                                      // Need a way to add them back to queue when finished
                    }
                    else
                    {
                        while (executing)
                        {
                            for (int j = 0; j < procNum; j++)
                            {
                                if (!Processors.ElementAt(j).IsAlive)
                                {
                                    rq.Enqueue(procs.ElementAt(j).Key); //FIX ME
                                    Processors.Insert(j, new Thread(() => u.First_Come_First_Served(procs.ElementAt(rq.Peek()), ref time)));
                                    Processors.ElementAt(j).Start();
                                    rq.Dequeue();
                                    executing = false;
                                    break;
                                }
                            }
                        }
                        executing = true;
                    }
                }
            }
        }

        public void MultiProcSRT(Dictionary<int, PCB> procs, Queue<int> rq, UniprocessorAlgorithms u, int time, int procNum)
        {
            if (rq.Count() != 0)                                        // Check that ready queue isn't empty
            {
                for (int i = 0; i < rq.Count(); i++)                    // Loop through each processes in ready queue
                {
                    if (Processors.Count() < procNum)                   // Check if too many threads have been made
                    {
                        Processors.Add(new Thread(() => u.Shortest_Remaining_Time(procs, ref time)));
                        Processors.ElementAt(i).Start();
                        rq.Dequeue();                                   // Remove process from ready queue                                      // Need a way to add them back to queue when finished
                    }
                    else
                    {
                        while (executing)
                        {
                            for (int j = 0; j < procNum; j++)
                            {
                                if (!Processors.ElementAt(j).IsAlive)
                                {
                                    rq.Enqueue(procs.ElementAt(j).Key); //FIX ME
                                    Processors.Insert(j, new Thread(() => u.Shortest_Remaining_Time(procs, ref time)));
                                    Processors.ElementAt(j).Start();
                                    rq.Dequeue();
                                    executing = false;
                                    break;
                                }
                            }
                        }
                        executing = true;
                    }
                }
            }
        }
    }
}
