using System;
using System.Linq;

namespace SA2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] preferences ={
            { 1, 3, 2 },
            { 2, 2, 2 },
            { 1, 2, 1 }};

            int experts = preferences.GetLength(0);
            int alterns = preferences.GetLength(1);

            double[] omega = MethodPrefer(preferences, experts, alterns);
            double W = Coordination(preferences, experts, alterns);

            Console.WriteLine("Веса альтернатив: ");
            foreach (var item in omega)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\nW = {0}", W);
            Console.Read();
        }

        private static double Coordination(int[,] preferences, int experts, int alterns)
        {
            int[] mS = new int[alterns];
            for (int j = 0; j < experts; j++)
            {
                for (int i = 0; i < alterns; i++)
                {
                    mS[j] += preferences[i, j];
                }
            }
            double S = 0;
            for (int i = 0; i < alterns; i++)
            {
                S += Math.Pow(mS[i] - experts * (alterns + 1) / 2, 2);
            }
            double W = 12 * S / Math.Pow(experts, 2) / (Math.Pow(alterns, 3) - alterns);
            return W;
        }

        private static double[] MethodPrefer(int[,] preferences, int experts, int alterns)
        {
            int[,] modifyed = new int[experts, alterns];
            for (int i = 0; i < experts; i++)
            {
                for (int j = 0; j < alterns; j++)
                {
                    modifyed[i, j] = alterns - preferences[i, j];
                }
            }
            int[] masC = new int[alterns];
            for (int j = 0; j < experts; j++)
            {
                for (int i = 0; i < alterns; i++)
                {
                    masC[j] += modifyed[i, j];
                }
            }
            double sum = masC.Sum();
            double[] omega = new double[alterns];
            for (int i = 0; i < alterns; i++)
            {
                omega[i] = masC[i] / sum;
            }
            return omega;
        }

        public static double Concent(int a1, int a2, int n)
        {
            return 1 - ((6 * Math.Pow((a1 - a2), 2)) / (n * (Math.Pow(n, 2) - 1)));
        }
    }
}
