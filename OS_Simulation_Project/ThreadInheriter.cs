using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OS_Simulation_Project
{
    class ThreadInheriter : Thread
    {
        public int processID;
        public Thread t;

        public ThreadInheriter(int pID) { processID = pID; }
    }
}
