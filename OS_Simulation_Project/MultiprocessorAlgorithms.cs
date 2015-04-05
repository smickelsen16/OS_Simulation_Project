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
        public void MultiProcRoundRobin(Dictionary<int, PCB> procs, Queue<int> rq, UniprocessorAlgorithms u, int quantum, int time, int procNum)
        {
            if (rq.Count() != 0)                                        // Check that ready queue isn't empty
            {
                for (int i = 0; i < rq.Count(); i++)                    // Loop through each processes in ready queue
                {
                    if (Processors.Count() < procNum)                   // Check if too many threads have been made
                    {
                        Processors.Add(new Thread(() => u.Round_Robin(quantum, procs.ElementAt(i), time)));
                        Processors.ElementAt(i).Start();
                        rq.Dequeue();                                   // Remove process from ready queue                                      // Need a way to add them back to queue when finished
                    }
                    else
                    {
                        for (int j = 0; j < procNum; j++)
                        {
                            if (!Processors.ElementAt(j).IsAlive)
                            {
                                Processors.Insert(j, new Thread(() => u.Round_Robin(quantum, procs.ElementAt(i), time)));
                                Processors.ElementAt(j).Start();
                                rq.Dequeue();
                            }
                        }
                        // Do something if they're all busy
                    }
                }
            }
        }

        public void MultiProcFCFS(Dictionary<int, PCB> procs, Queue<int> rq, UniprocessorAlgorithms u, int time, int procNum)
        {
            if (rq.Count() != 0)                                        // Check that ready queue isn't empty
            {
                for (int i = 0; i < rq.Count(); i++)                    // Loop through each processes in ready queue
                {
                    if (Processors.Count() < procNum)                   // Check if too many threads have been made
                    {
                        Processors.Add(new Thread(() => u.First_Come_First_Served(procs.ElementAt(i), time)));
                        Processors.ElementAt(i).Start();
                        rq.Dequeue();                                   // Remove process from ready queue                                      // Need a way to add them back to queue when finished
                    }
                    else
                    {
                        for (int j = 0; j < procNum; j++)
                        {
                            if (!Processors.ElementAt(j).IsAlive)
                            {
                                Processors.Insert(j, new Thread(() => u.First_Come_First_Served(procs.ElementAt(i), time)));
                                Processors.ElementAt(j).Start();
                                rq.Dequeue();
                            }
                        }
                        // Do something if they're all busy
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
                        Processors.Add(new Thread(() => u.Shortest_Remaining_Time(procs, time)));
                        Processors.ElementAt(i).Start();
                        rq.Dequeue();                                   // Remove process from ready queue                                      // Need a way to add them back to queue when finished
                    }
                    else
                    {
                        for (int j = 0; j < procNum; j++)
                        {
                            if (!Processors.ElementAt(j).IsAlive)
                            {
                                Processors.Insert(j, new Thread(() => u.Shortest_Remaining_Time(procs, time)));
                                Processors.ElementAt(j).Start();
                                rq.Dequeue();
                            }
                        }
                        // Do something if they're all busy
                    }
                }
            }
        }
    }
}
