using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            //тута инфа из таблицы
            int[,] criteriesExpertsMarks = new int[,]
          { { 9, 7, 7 },
            { 7, 5, 9 }};

            double[,] parametrs = new double[,] {
           //Alt
/*Parametr*/{5, 5, 7, 1, 3, 9 },
            {5, 7, 1, 7, 7, 1 },
            {5, 1, 7, 3, 1, 5 } };

            double[] kriteriesWeights = GetWeights(criteriesExpertsMarks);

            double[,] modifyedParametrs = GetPareto(parametrs);

            double[,] nondimensionlessParametrs = GetNondimensionless(modifyedParametrs);
            Console.WriteLine("Безразмерные оценки:");
            PrintMassive(nondimensionlessParametrs);
            Console.WriteLine("Матрица индексов согласия:");
            double[,] acceptIndexes = GetAcceptIndexes(nondimensionlessParametrs, kriteriesWeights);
            PrintMassive(acceptIndexes);
            Console.WriteLine("Матрица индексов несогласия:");
            double[,] unacceptIndexes = GetUnacceptIndexes(nondimensionlessParametrs);
            PrintMassive(unacceptIndexes);
            double[] limitValueAcceptIndex = GetLimitValueAcceptIndex(acceptIndexes);
            double[] limitValueUnacceptIndex = GetLimitValueUnacceptIndex(unacceptIndexes);

            double thresholdValueAcceptIndex = 0.2;
            double thresholdValueUnacceptIndex = 0.5;

            Console.WriteLine("Номера альтернатив в ядре:");
            for (int i = 0; i < limitValueAcceptIndex.Length; i++)
            {
                if (limitValueAcceptIndex[i] > thresholdValueAcceptIndex && limitValueUnacceptIndex[i] < thresholdValueUnacceptIndex)
                {
                    Console.WriteLine(i + 1);
                }
            }
        }

        private static double[] GetLimitValueUnacceptIndex(double[,] unacceptIndexes)
        {
            int lenghtAlts = unacceptIndexes.GetLength(0);
            double[] limitValueUnacceptIndex = new double[lenghtAlts];
            for (int i = 0; i < lenghtAlts; i++)
            {
                for (int j = 0; j < lenghtAlts; j++)
                {
                    if (limitValueUnacceptIndex[i] < unacceptIndexes[i, j])
                    {
                        limitValueUnacceptIndex[i] = unacceptIndexes[i, j];
                    }
                }
            }
            return limitValueUnacceptIndex;
        }

        private static double[] GetLimitValueAcceptIndex(double[,] acceptIndexes)
        {
            int lenghtAlts = acceptIndexes.GetLength(0);
            double[] limitValueAcceptIndex = new double[lenghtAlts];
            for (int i = 0; i < lenghtAlts; i++)
            {
                limitValueAcceptIndex[i] = double.MaxValue;
                for (int j = 0; j < lenghtAlts; j++)
                {
                    if (limitValueAcceptIndex[i] > acceptIndexes[i, j])
                    {
                        limitValueAcceptIndex[i] = acceptIndexes[i, j];
                    }
                }
            }
            return limitValueAcceptIndex;
        }

        private static double[,] GetAcceptIndexes(double[,] nondimensionlessParametrs, double[] kriteriesWeights)
        {
            int lenghtAlts = nondimensionlessParametrs.GetLength(1);
            int lenghtKrits = nondimensionlessParametrs.GetLength(0);
            double[,] acceptIndexes = new double[lenghtAlts, lenghtAlts];

            for (int j = 0; j < lenghtAlts; j++)
            {
                for (int k = 0; k < lenghtAlts; k++)
                {
                    for (int i = 0; i < lenghtKrits; i++)
                    {
                        if (nondimensionlessParametrs[i, j] >= nondimensionlessParametrs[i, k])
                        {
                            acceptIndexes[j, k] += kriteriesWeights[i];
                        }
                    }
                }
            }

            return acceptIndexes;
        }
        private static double[,] GetUnacceptIndexes(double[,] nondimensionlessParametrs)
        {
            int lenghtAlts = nondimensionlessParametrs.GetLength(1);
            int lenghtKrits = nondimensionlessParametrs.GetLength(0);
            double[,] unacceptIndexes = new double[lenghtAlts, lenghtAlts];

            for (int j = 0; j < lenghtAlts; j++)
            {
                for (int k = 0; k < lenghtAlts; k++)
                {
                    for (int i = 0; i < lenghtKrits; i++)
                    {
                        if (nondimensionlessParametrs[i, j] <= nondimensionlessParametrs[i, k] && Math.Abs(nondimensionlessParametrs[i, k] - nondimensionlessParametrs[i, j]) > unacceptIndexes[j, k])
                        {
                            unacceptIndexes[j, k] = Math.Abs(nondimensionlessParametrs[i, k] - nondimensionlessParametrs[i, j]);
                        }
                    }
                }
            }

            return unacceptIndexes;
        }

        static double[] GetWeights(int[,] scores)
        {
            int lengthAlt = scores.GetLength(1);
            int lengthExp = scores.GetLength(0);

            int[] altScores = new int[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                altScores[i] = 0;
                for (int j = 0; j < lengthExp; j++)
                {
                    altScores[i] += scores[j, i];
                }
            }
            int sumScores = altScores.Sum();
            double[] weightsAlts = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                weightsAlts[i] = altScores[i] / (double)sumScores;
            }
            return weightsAlts;
        }

        private static double[,] GetNondimensionless(double[,] modifyedParametrs)
        {
            double[,] result = new double[modifyedParametrs.GetLength(0), modifyedParametrs.GetLength(1)];
            for (int i = 0; i < modifyedParametrs.GetLength(0); i++)
            {
                double max = modifyedParametrs[i, 0];
                for (int j = 0; j < modifyedParametrs.GetLength(1); j++)
                {
                    if (modifyedParametrs[i, j] > max)
                        max = modifyedParametrs[i, j];

                }
                for (int j = 0; j < modifyedParametrs.GetLength(1); j++)
                {
                    result[i, j] = (double)modifyedParametrs[i, j] / max;
                }
            }
            return result;
        }

        private static double[,] GetPareto(double[,] parametrs)
        {
            int indexRemoveAlts = 0;
            for (int k = 0; k < parametrs.GetLength(1); k++)
            {
                for (int j = 0; j < parametrs.GetLength(1); j++)
                {
                    if (j != k)
                    {

                        bool[] isRemove = new bool[parametrs.GetLength(0)];
                        for (int i = 0; i < parametrs.GetLength(0); i++)
                        {

                            if (parametrs[i, k] >= parametrs[i, j] && parametrs[i, j] != 0)
                            {
                                isRemove[i] = true;
                            }

                        }
                        bool notRemoveAlt = false;
                        foreach (var item in isRemove)
                        {
                            if (item == false)
                            {
                                notRemoveAlt = true;
                                break;
                            }
                        }
                        if (notRemoveAlt == false)
                        {
                            indexRemoveAlts++;
                            for (int i = 0; i < parametrs.GetLength(0); i++)
                            {
                                parametrs[i, j] = 0;
                            }
                        }
                    }
                }
            }
            double[,] modifyedParametrs = new double[parametrs.GetLength(0), parametrs.GetLength(1) - indexRemoveAlts];

            for (int i = 0; i < parametrs.GetLength(0); i++)
            {
                int jMod = 0;
                for (int j = 0; j < parametrs.GetLength(1); j++)
                {
                    if (parametrs[i, j] != 0)
                    {
                        modifyedParametrs[i, jMod] = parametrs[i, j];
                    }
                    else
                    {
                        jMod--;
                    }
                    jMod++;
                }
            }
            return modifyedParametrs;
        }

        private static void PrintMassive(double[,] massive)
        {
            for (int i = 0; i < massive.GetLength(0); i++)
            {
                for (int j = 0; j < massive.GetLength(1); j++)
                {
                    Console.Write("{0:0.##} ", massive[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
