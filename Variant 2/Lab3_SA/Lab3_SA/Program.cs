using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_SA
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrixOfScores = new int[,]
            { { 10, 6, 5, 8 },
            { 7, 7, 5, 9 },
            { 8, 4, 6, 10 },
            { 3, 5, 2, 8} };

            int lengthAlt = matrixOfScores.GetLength(1);
            int lengthExp = matrixOfScores.GetLength(0);

            int[] sumScores = new int[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                sumScores[i] = 0;
                for (int j = 0; j < lengthExp; j++)
                {
                    sumScores[i] += matrixOfScores[j, i];
                }
            }
            int SumScores = sumScores.Sum();
            double[] weightsAlts = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                weightsAlts[i] = sumScores[i] / (double)SumScores;
            }

            double[] averageScores = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                averageScores[i] = sumScores[i] / (double)lengthExp;
            }
            double[] dispersionsForExperts = new double[lengthExp];
            for (int i = 0; i < lengthExp; i++)
            {
                double qvSumAverScores = 0;
                for (int j = 0; j < lengthAlt; j++)
                {
                    qvSumAverScores += Math.Pow((matrixOfScores[i, j] - averageScores[j]), 2);
                }
                dispersionsForExperts[i] = (1 / lengthAlt - 1) * qvSumAverScores;
            }
            double[] dispersionsForAlternatives = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                double qvSumAverScores = 0;
                for (int j = 0; j < lengthExp; j++)
                {
                    qvSumAverScores += Math.Pow((matrixOfScores[j, i] - averageScores[i]), 2);
                }
                dispersionsForAlternatives[i] = (1 / lengthExp - 1) * qvSumAverScores;
            }
            foreach (var item in dispersionsForExperts)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            foreach (var item in dispersionsForAlternatives)
            {
                Console.Write(item + " ");
            }
        }

    }
}
