using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrixOfRelations = new double[,]
          { { 1, 7, 5, 9 },
            { 0, 1, 0.333, 3 },
            { 0, 0, 1, 5 },
            { 0, 0, 0, 1} };
            int lengthAlt = matrixOfRelations.GetLength(1);
            for (int i = 0; i < lengthAlt; i++)
            {
                for (int j = 0; j < lengthAlt; j++)
                {
                    if (matrixOfRelations[i,j] == 0)
                    {
                        matrixOfRelations[i, j] = 1 / matrixOfRelations[j, i];
                    }
                }
            }
            double[] alternativesScores = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                alternativesScores[i] = 1;
                for (int j = 0; j < lengthAlt; j++)
                {
                    alternativesScores[i] *= matrixOfRelations[i,j];
                }
                alternativesScores[i] = Math.Pow(alternativesScores[i], (1/(double)lengthAlt));
            }
            double SumAlternativesScores = alternativesScores.Sum();
            double[] weightsAlts = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                weightsAlts[i] = alternativesScores[i] / SumAlternativesScores;
            }
            double[] sumColumns = new double[lengthAlt];
            for (int j = 0; j < lengthAlt; j++)
            {
                for (int i = 0; i < lengthAlt; i++)
                {
                    sumColumns[j] += matrixOfRelations[i, j];
                }
            }
            double extraAlpha = 0;
            for (int i = 0; i < lengthAlt; i++)
            {
                extraAlpha += weightsAlts[i] * sumColumns[i];
            }
            double indexOfCoherence = (extraAlpha - lengthAlt) / (lengthAlt - 1);
            double randomCoherence = 1;
            if (lengthAlt == 4)
            {
                randomCoherence = 0.9;
            }
            else if (lengthAlt == 3)
            {
                randomCoherence = 0.58;
            }

            double ratio = indexOfCoherence / randomCoherence;
            if (ratio > 0.2)
            {
                Console.WriteLine("требуется уточнение матрицы парных сравнений, отношение согласовванности: " + ratio);
            }
            else
            {
                Console.WriteLine("уточнение матрицы парных сравнений не требуется, отношение согласовванности: " + ratio);
                Console.WriteLine("Веса альтернатив: ");
                foreach (var item in weightsAlts)
                {
                    Console.Write(item + " ");
                }
            }
            Console.Read();
        }
    }
}
