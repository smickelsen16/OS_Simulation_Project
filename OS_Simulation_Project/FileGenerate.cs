using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation_Project
{
    class FileGenerate
    {
        static Random rand = new Random();

        public void Start()
        {
            string[] lines = new string[500];
            int[] arrivalTime = new int[500];
            int filesToGen;
            for (int i = 0; i < 500; i++)
            {
                filesToGen = rand.Next(2, 10);
                lines[i] = Generate(filesToGen, i, ref arrivalTime);
            }
            //System.IO.File.WriteAllLines(@"C:\Users\James Bond\Documents\CS_Projects\OS_Sim_File_Generator\Mytext.txt", lines);
            //System.IO.File.WriteAllLines(@"/Users/TylerHarding/Documents/OS_Sim_Random_File/Mytext.txt", lines);
            System.IO.File.WriteAllLines(@"C:\Users\wesley\Desktop\Output.txt", lines);
            //System.IO.File.WriteAllLines(@"C:\Users\James Bond\Desktop\Output.txt", lines);
        }

        //Assuming that our process will always start off with a CPU burst
        //This function is going to take in a random number between 1 and 10, and then 
        //use that number to determine how many bursts the process will contain
        //EX. 5 bursts, The process will have PID, Arrivaltime, CPU burst, IOBurst, CPUBurst, IOBurst, CPUBurst
        public static string Generate(int numberOfBurstsToInclude, int pid, ref int[] ATime)
        {

            string text;
            int PID = pid;

            if (pid == 0)
            {
                ATime[pid] = 0;
            }
            else
            {
                ATime[pid] = ATime[pid - 1] + rand.Next(1, 5);
            }


            //Right now I'm making the assumption that the processes don't ever take longer than 50 units of exeuction, but this can change.
            //IOBursts are the amount of execution time that it spends doing an IO operation before switching to CPU Operation
            double IOBurst1 = rand.Next(1, 20);
            double IOBurst2 = rand.Next(1, 20);
            double IOBurst3 = rand.Next(1, 20);
            double IOBurst4 = rand.Next(1, 20);
            double IOBurst5 = rand.Next(1, 20);

            //CPUBursts are the amount of exeuction time that the process spends doing a CPU operation before switching to IO Operation
            double CPUBurst1 = rand.Next(1, 20);
            double CPUBurst2 = rand.Next(1, 20);
            double CPUBurst3 = rand.Next(1, 20);
            double CPUBurst4 = rand.Next(1, 20);
            double CPUBurst5 = rand.Next(1, 20);



            //If statements that are used to determine what string is returned
            //EX. If number of bursts to include is 1, then the string will contain the PID, ArrivalTime,
            //and Then 1 CPUburst number.
            //If NumberOfBurstsToInclude is 2, then the string will contain the PID, ArrivalTime,
            //1 CPUBurst, and 1 IOBurst
            if (numberOfBurstsToInclude == 1)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1);
                return text;
            }
            if (numberOfBurstsToInclude == 2)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1);
                return text;
            }
            if (numberOfBurstsToInclude == 3)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2);
                return text;
            }
            if (numberOfBurstsToInclude == 4)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2) + " " + Convert.ToString(IOBurst2);
                return text;
            }
            if (numberOfBurstsToInclude == 5)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2) + " " + Convert.ToString(IOBurst2)
                    + " " + Convert.ToString(CPUBurst3);
                return text;
            }
            if (numberOfBurstsToInclude == 6)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2) + " " + Convert.ToString(IOBurst2)
                    + " " + Convert.ToString(CPUBurst3) + " " + Convert.ToString(IOBurst3);
                return text;
            }
            if (numberOfBurstsToInclude == 7)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2) + " " + Convert.ToString(IOBurst2)
                    + " " + Convert.ToString(CPUBurst3) + " " + Convert.ToString(IOBurst3)
                    + " " + Convert.ToString(CPUBurst4);
                return text;
            }
            if (numberOfBurstsToInclude == 8)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2) + " " + Convert.ToString(IOBurst2)
                    + " " + Convert.ToString(CPUBurst3) + " " + Convert.ToString(IOBurst3)
                    + " " + Convert.ToString(CPUBurst4) + " " + Convert.ToString(IOBurst4);
                return text;
            }
            if (numberOfBurstsToInclude == 9)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2) + " " + Convert.ToString(IOBurst2)
                    + " " + Convert.ToString(CPUBurst3) + " " + Convert.ToString(IOBurst3)
                    + " " + Convert.ToString(CPUBurst4) + " " + Convert.ToString(IOBurst4)
                    + " " + Convert.ToString(CPUBurst5);
                return text;
            }
            if (numberOfBurstsToInclude == 10)
            {
                text = Convert.ToString(PID) + " " + Convert.ToString(ATime[pid])
                    + " " + Convert.ToString(CPUBurst1) + " " + Convert.ToString(IOBurst1)
                    + " " + Convert.ToString(CPUBurst2) + " " + Convert.ToString(IOBurst2)
                    + " " + Convert.ToString(CPUBurst3) + " " + Convert.ToString(IOBurst3)
                    + " " + Convert.ToString(CPUBurst4) + " " + Convert.ToString(IOBurst4)
                    + " " + Convert.ToString(CPUBurst5) + " " + Convert.ToString(IOBurst5);
                return text;
            }

            return "hi";
        }
    }
}
