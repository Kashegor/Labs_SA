using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * { { 1, 7, 5, 9 },
            { 0.14, 1, 0.333, 3 },
            { 0.2, 3, 1, 5 },
            { 0.11, 0.333, 0.2, 1} };
            { { 1, 7, 5, 9 },
            { 0, 1, 0.333, 3 },
            { 0, 0, 1, 5 },
            { 0, 0, 0, 1} };
 */
namespace SA_4
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] Relations = new double[,]{ 
            { 1, 7, 5, 9 },
            { 0.14, 1, 0.333, 3 },
            { 0.2, 3, 1, 5 },
            { 0.11, 0.333, 0.2, 1} };
            int lengthAltn = Relations.GetLength(1);
            double[] weightsAlts = GetWeightOfAlterns(Relations);
            double sumWeigthAndСolumn = SumWeigthAndСolumn(Relations, weightsAlts);
            double coherence = RelationshipCoherence(lengthAltn, sumWeigthAndСolumn);
            if (coherence > 0.2)
            {
                Console.WriteLine("требуется уточнение матрицы парных сравнений, отношение согласовванности: " + coherence);
            }
            else
            {
                Console.WriteLine("уточнение матрицы парных сравнений не требуется, отношение согласовванности: " + coherence);
                Console.WriteLine("Веса альтернатив: ");
                foreach (var item in weightsAlts)
                {
                    Console.Write(item + " ");
                }
            }
            Console.Read();
        }

        static double RelationshipCoherence(int lengthAlt, double extraAlpha)
        {
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
            return ratio;
        }

        static double SumWeigthAndСolumn(double[,] matrixOfRelations, double[] weightsAlts)
        {
            int lengthAlt = matrixOfRelations.GetLength(1);
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

            return extraAlpha;
        }

        static double[] GetWeightOfAlterns(double[,] matrixOfRelations)
        {
            int lengthAlt = matrixOfRelations.GetLength(1);
            double[] alternativesScores = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                alternativesScores[i] = 1;
                for (int j = 0; j < lengthAlt; j++)
                {
                    alternativesScores[i] *= matrixOfRelations[i, j];
                }
                alternativesScores[i] = Math.Pow(alternativesScores[i], (1 / (double)lengthAlt));
            }
            double SumAlternativesScores = alternativesScores.Sum();
            double[] weightsAlts = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                weightsAlts[i] = alternativesScores[i] / SumAlternativesScores;
            }

            return weightsAlts;
        }
    }
}
