﻿using System;
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

        public void MultiProcRoundRobin(ref Dictionary<int, PCB> CPU_ready_Q, ref SortedDictionary<int, PCB> COMPLETED_PROCS,
            UniprocessorAlgorithms u, int quantum, ref int time, int procNum)
        {
            int finishedCounterThing = CPU_ready_Q.Count();
            Dictionary<int, PCB> crq;
            SortedDictionary<int, PCB> cp;
            int t;
            do
            {
                crq = CPU_ready_Q;
                cp = COMPLETED_PROCS;
                t = time;
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

        public void MultiProcFCFS(ref Dictionary<int, PCB> CPU_ready_Q, ref SortedDictionary<int, PCB> COMPLETED_PROCS,
            UniprocessorAlgorithms u, ref int time, int procNum)
        {
            int finishedCounterThing = CPU_ready_Q.Count();
            int t;
            Dictionary<int, PCB> crq;
            SortedDictionary<int, PCB> cp;
            do
            {
                t = time;
                crq = CPU_ready_Q;
                cp = COMPLETED_PROCS;
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
