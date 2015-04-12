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
        int finished = 0;

        public void MultiProcRoundRobin(ref Dictionary<int, PCB> CPU_ready_Q, ref Dictionary<int, PCB> COMPLETED_PROCS,
            UniprocessorAlgorithms u, int quantum, ref int time, int procNum)
        {
            int finishedCounterThing = CPU_ready_Q.Count();
            Dictionary<int, PCB> crq = CPU_ready_Q;
            Dictionary<int, PCB> cp = COMPLETED_PROCS;
            int t = time;

            do
            {
                if (Processors.Count() < procNum)
                {
                    Processors.Add(new Thread(() => u.Round_Robin(quantum, crq.First(), ref t, ref crq, ref cp)));
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
                                    Processors.Insert(j, new Thread(() => u.Round_Robin(quantum, crq.First(), ref t, ref crq, ref cp)));
                                    Processors.ElementAt(j).Start();
                                }
                                executing = false;
                                break;
                            }
                        }
                    }
                    executing = true;
                }
            } while (finished != finishedCounterThing);

        }

        public void MultiProcFCFS(ref Dictionary<int, PCB> CPU_ready_Q, ref Dictionary<int, PCB> COMPLETED_PROCS,
            UniprocessorAlgorithms u, ref int time, int procNum)
        {
            int finishedCounterThing = CPU_ready_Q.Count();
            int t = time;
            Dictionary<int, PCB> crq = CPU_ready_Q;
            Dictionary<int, PCB> cp = COMPLETED_PROCS;
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
            } while (finished != finishedCounterThing);
        }
    }
}
