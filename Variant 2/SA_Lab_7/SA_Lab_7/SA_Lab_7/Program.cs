using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Lab_7
{
    class Program
    {
        static void Main(string[] args)
        {
            //тута инфа из таблицы
            int[,] parametrs = new int[,] {
           //Alt
/*Parametr*/{5, 5, 7, 1, 3, 9 },
            {5, 7, 1, 7, 7, 1 },
            {5, 1, 7, 3, 1, 5 } };

            int[,] modifyedParametrs = GetPareto(parametrs);

            double[,] nondimensionlessParametrs = GetNondimensionless(modifyedParametrs);
            Console.WriteLine("Безразмерные оценки:");
            PrintMassive(nondimensionlessParametrs);

            double[] minimumAlternativesMarks = GetMinimumAlternativesMarks(nondimensionlessParametrs);

            double thresholdMinimum = 0.3;
            double[,] parametrsByExpressAtsMetodic = GetAltsMoreThan(nondimensionlessParametrs, minimumAlternativesMarks, thresholdMinimum);
            Console.WriteLine("Результат экспресс-анализа при допустимом минимуме 0.3:");
            PrintMassive(parametrsByExpressAtsMetodic);
        }

        private static double[,] GetAltsMoreThan(double[,] parametrs, double[] minimumAlternativesMarks, double thresholdMinimum)
        {
            int indexRemoveAlts = 0;
            for (int j = 0; j < parametrs.GetLength(1); j++)
            {
                for (int i = 0; i < parametrs.GetLength(0); i++)
                {
                    if (minimumAlternativesMarks[j] <= thresholdMinimum)
                    {
                        for (int i1 = 0; i1 < parametrs.GetLength(0); i1++)
                        {
                            parametrs[i1, j] = 0;
                        }
                        indexRemoveAlts++;
                        break;
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

        private static double[] GetMinimumAlternativesMarks(double[,] parametrs)
        {
            double[] result = new double[parametrs.GetLength(1)];
            for (int j = 0; j < parametrs.GetLength(1); j++)
            {
                double min = parametrs[0, j];
                for (int i = 0; i < parametrs.GetLength(0); i++)
                {
                    if (parametrs[i, j] < min)
                        min = parametrs[i, j];

                }
                result[j] = min;
            }
            return result;
        }

        private static double[,] GetNondimensionless(int[,] modifyedParametrs)
        {
            double[,] result = new double[modifyedParametrs.GetLength(0), modifyedParametrs.GetLength(1)];
            for (int i = 0; i < modifyedParametrs.GetLength(0); i++)
            {
                int max = modifyedParametrs[i, 0];
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

        private static int[,] GetPareto(int[,] parametrs)
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
            int[,] modifyedParametrs = new int[parametrs.GetLength(0), parametrs.GetLength(1) - indexRemoveAlts];

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
                    Console.Write(massive[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
