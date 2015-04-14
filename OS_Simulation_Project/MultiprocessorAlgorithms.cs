using System;
using System.Collections.Concurrent;
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

        public void MultiProcRoundRobin(ref ConcurrentDictionary<int, PCB> CPU_ready_Q, ref SortedDictionary<int, PCB> COMPLETED_PROCS,
            UniprocessorAlgorithms u, int quantum, ref int time, int procNum)
        {
            int finished = 0;
            int t = time;
            ConcurrentDictionary<int, PCB> crq = CPU_ready_Q;
            SortedDictionary<int, PCB> cp = COMPLETED_PROCS;
            do
            {
                if (Processors.Count() < procNum)
                {
                    var f = crq.First();
                    Processors.Add(new Thread(() => u.Round_Robin(quantum, f, ref t, ref crq, ref cp)));
                    Processors.ElementAt(Processors.Count() - 1).Start();
                }
                else
                {
                    while (executing)
                    {
                        for (int j = 0; j < procNum; j++)
                        {
                            if (!Processors.ElementAt(j).IsAlive)
                            {
                                finished++;
                                if (CPU_ready_Q.Count != 0)
                                {
                                    var f = crq.First();
                                    Processors.Insert(j, new Thread(() => u.Round_Robin(quantum, f, ref t, ref crq, ref cp)));
                                    Processors.ElementAt(j).Start();
                                }
                                executing = false;
                                break;
                            }
                        }
                    }
                    executing = true;
                }
            } while (finished != CPU_ready_Q.Count() - procNum);

        }

        public void MultiProcFCFS(ref ConcurrentDictionary<int, PCB> CPU_ready_Q, ref SortedDictionary<int, PCB> COMPLETED_PROCS,
            UniprocessorAlgorithms u, ref int time, int procNum)
        {
            int finished = 0;
            int t = time;
            ConcurrentDictionary<int, PCB> crq = CPU_ready_Q;
            SortedDictionary<int, PCB> cp = COMPLETED_PROCS;
            do
            {
                if (Processors.Count() < procNum)
                {
                    Processors.Add(new Thread(() => u.First_Come_First_Served(crq.First(), ref t, ref crq, ref cp)));
                    Processors.ElementAt(Processors.Count() - 1).Start();
                }
                else
                {
                    while (executing)
                    {
                        for (int j = 0; j < procNum; j++)
                        {
                            if (!Processors.ElementAt(j).IsAlive)
                            {
                                finished++;
                                if (CPU_ready_Q.Count != 0)
                                {
                                    Processors.Insert(j, new Thread(() => u.First_Come_First_Served(crq.First(), ref t, ref crq, ref cp)));
                                    Processors.ElementAt(j).Start();
                                }
                                executing = false;
                                break;
                            }
                        }
                    }
                    executing = true;
                }
            } while (finished != CPU_ready_Q.Count() - procNum);
        }
    }
}
