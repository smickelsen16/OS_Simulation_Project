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
        public void MultiProcRoundRobin(Queue<int> rq, UniprocessorAlgorithms rr, int quantum, int time, int procNum)
        {
            if (rq.Count() != 0)                                        // Check that ready queue isn't empty
            {
                for (int i = 0; i < rq.Count(); i++)                    // Loop through each processes in ready queue
                {
                    if (Processors.Count() < procNum)                   // Check if too many threads have been made
                    {
                        Processors.Add(new Thread(() => rr.Round_Robin(quantum, rq.ElementAt(i), time))); // Want to pass in ready queue, not dictionary/tuple
                        Processors.ElementAt(i).Start();
                        rq.Dequeue();                                   // Remove process from ready queue
                    }
                    else
                    {
                        for (int j = 0; j < procNum; j++)
                        {
                            if (!Processors.ElementAt(j).IsAlive)
                            {
                                Processors.Insert(j, new Thread(() => rr.Round_Robin(quantum, rq.ElementAt(i), time)));
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
