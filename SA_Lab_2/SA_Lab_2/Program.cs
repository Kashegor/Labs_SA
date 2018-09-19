using System;
using System.Linq;

namespace SA_Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int[,] matrixOfPreferences = new int[,]
            { { 1, 4, 2, 3 },
            { 1, 3, 2, 4 },
            { 3, 2, 1, 4 },
            { 1, 3, 2, 4 } };

            int lengthAlterns = matrixOfPreferences.GetLength(1);
            int lengthExperts = matrixOfPreferences.GetLength(0);

            int[,] modifyedMatrixOfPreferences = new int[lengthExperts, lengthAlterns];
            for (int i = 0; i < lengthExperts; i++)
            {
                for (int j = 0; j < lengthAlterns; j++)
                {
                    modifyedMatrixOfPreferences[i, j] = lengthAlterns - matrixOfPreferences[i, j];
                }
            }
            int[] masC = new int[lengthAlterns];
            for (int j = 0; j < lengthExperts; j++)
            {
                for (int i = 0; i < lengthAlterns; i++)
                {
                    masC[j] += modifyedMatrixOfPreferences[i, j];
                }
            }
            double C = masC.Sum();
            double[] masOmega = new double[lengthAlterns];
            for (int i = 0; i < lengthAlterns; i++)
            {
                masOmega[i] = masC[i] / C;
            }
            int[] masS = new int[lengthAlterns];
            for (int j = 0; j < lengthExperts; j++)
            {
                for (int i = 0; i < lengthAlterns; i++)
                {
                    masS[j] += matrixOfPreferences[i, j];
                }
            }
            double S = 0;
            for (int i = 0; i < lengthAlterns; i++)
            {
                S += Math.Pow(masS[i] - lengthExperts * (lengthAlterns + 1) / 2, 2);
            }
            double W = 12 * S / Math.Pow(lengthExperts, 2) / (Math.Pow(lengthAlterns, 3) - lengthAlterns);

            Console.WriteLine("Искомые веса альтернатив: ");
            foreach (var item in masOmega)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("W= {0}", W);
            Console.Read();
        }
    }
}
