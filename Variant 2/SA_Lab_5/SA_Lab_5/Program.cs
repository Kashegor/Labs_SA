using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Lab_5
{
    class Program
    {
        static void Main(string[] args)
        {
            //тутачки по условию мнения экспертов о критериях
            double[,] matrixOfRelationsKriteriesByExp1 = new double[,]
          { { 1, 5, 3 },
            { 0.2, 1, 3 },
            { 0.3333, 0.3333, 1 }};
            double[,] matrixOfRelationsKriteriesByExp2 = new double[,]
          { { 1, 5, 7 },
            { 0.2, 1, 0.3333 },
            { 0.14286, 3, 1 } };
            //тута инфа из таблицы
            int[,] matrixOfParametrs = new int[,] {
           //Alt
/*Parametr*/{5, 5, 7, 1, 3, 9 },
            {5, 7, 1, 7, 7, 1 },
            {5, 1, 7, 3, 1, 5 } };
            int indexRemoveAlts = 0;
            //Выбор Парето
            for (int k = 0; k < matrixOfParametrs.GetLength(1); k++)
            {
                for (int j = 0; j < matrixOfParametrs.GetLength(1); j++)
                {
                    if (j != k)
                    {

                        bool[] isRemove = new bool[matrixOfParametrs.GetLength(0)];
                        for (int i = 0; i < matrixOfParametrs.GetLength(0); i++)
                        {

                            if (matrixOfParametrs[i, k] >= matrixOfParametrs[i, j] && matrixOfParametrs[i, j] != 0)
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
                            for (int i = 0; i < matrixOfParametrs.GetLength(0); i++)
                            {
                                matrixOfParametrs[i, j] = 0;
                            }
                        }
                    }
                }
            }
            int[,] modifyedParametrs = new int[matrixOfParametrs.GetLength(0), matrixOfParametrs.GetLength(1) - indexRemoveAlts];

            for (int i = 0; i < matrixOfParametrs.GetLength(0); i++)
            {
                int jMod = 0;
                for (int j = 0; j < matrixOfParametrs.GetLength(1); j++)
                {
                    if (matrixOfParametrs[i, j] != 0)
                    {
                        modifyedParametrs[i, jMod] = matrixOfParametrs[i, j];
                    }
                    else
                    {
                        jMod--;
                    }
                    jMod++;
                }
            }

            //Вывод метода Пурето
            //Console.WriteLine("Оставшиеся значения альтернатив после очистки методом Пурето:");
            //for (int i = 0; i < modifyedParametrs.GetLength(0); i++)
            //{
            //    for (int j = 0; j < modifyedParametrs.GetLength(1); j++)
            //    {
            //        Console.Write(modifyedParametrs[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            //Основной метод
            int alternativesCount = modifyedParametrs.GetLength(1);
            double[,] relationsAlternativesByFirstKrit = new double[alternativesCount, alternativesCount];
            double[,] relationsAlternativesBySecondKrit = new double[alternativesCount, alternativesCount];
            double[,] relationsAlternativesByThirdKrit = new double[alternativesCount, alternativesCount];
            for (int i = 0; i < alternativesCount; i++)//для инициализации
            {
                for (int j = 0; j < alternativesCount; j++)
                {
                    relationsAlternativesByFirstKrit[i, j] = modifyedParametrs[0, i] / (double)modifyedParametrs[0, j];
                    relationsAlternativesBySecondKrit[i, j] = modifyedParametrs[1, i] / (double)modifyedParametrs[1, j];
                    relationsAlternativesByThirdKrit[i, j] = modifyedParametrs[2, i] / (double)modifyedParametrs[2, j];
                }
            }

            //Console.WriteLine("Вычесленная матрица зависимостей альтернатив для первого критерия:");
            //for (int i = 0; i < alternativesCount; i++)
            //{
            //    for (int j = 0; j < alternativesCount; j++)
            //    {
            //        Console.Write(relationsAlternativesByFirstKrit[i, j] + "\t\t");
            //    }
            //    Console.WriteLine();
            //}

            //получение весов критериев
            double[] kriteriesWeights = GetWeights(matrixOfRelationsKriteriesByExp1, matrixOfRelationsKriteriesByExp2);
            //Console.WriteLine("Веса критериев:");
            //for (int i = 0; i < kriteriesCount; i++)
            //{
            //    Console.Write(kriteriesWeights[i] + " ");
            //}
            //Console.WriteLine();
            //получение весов альтернатив по отдельным критериям
            double[] alternativesWeightsByFirstKrit = GetWeights(relationsAlternativesByFirstKrit);
            double[] alternativesWeightsBySecondKrit = GetWeights(relationsAlternativesBySecondKrit);
            double[] alternativesWeightsByThirdKrit = GetWeights(relationsAlternativesByThirdKrit);
            //Console.WriteLine("Веса альтернатив по первому критерию:");
            //for (int i = 0; i < alternativesCount; i++)
            //{
            //    Console.Write(alternativesWeightsByFirstKrit[i] + " ");
            //}
            //Console.WriteLine();
            //получаем ответ
            double[] globalPriors = GetGlobalPriorities(kriteriesWeights, alternativesWeightsByFirstKrit,
                alternativesWeightsBySecondKrit, alternativesWeightsByThirdKrit);
            Console.WriteLine("Глобальные приоритеты альтернатив:");
            for (int i = 0; i < alternativesCount; i++)
            {
                Console.Write(globalPriors[i] + " ");
            }
        }

        static double[] GetWeights(params double[][,] relations)
        {
            int lengthAlt = relations[0].GetLength(1);

            double[][] weightsAlts = new double[relations.Length][];
            for (int i = 0; i < relations.Length; i++)
            {
                weightsAlts[i] = new double[lengthAlt];
            }

            for (int k = 0; k < relations.Length; k++)
            {
                for (int i = 0; i < lengthAlt; i++)
                {
                    for (int j = 0; j < lengthAlt; j++)
                    {
                        if (relations[k][i, j] == 0)
                        {
                            relations[k][i, j] = 1 / relations[k][j, i];
                        }
                    }
                }
                double[] alternativesScores = new double[lengthAlt];
                for (int i = 0; i < lengthAlt; i++)
                {
                    alternativesScores[i] = 1;
                    for (int j = 0; j < lengthAlt; j++)
                    {
                        alternativesScores[i] *= relations[k][i, j];
                    }
                    alternativesScores[i] = Math.Pow(alternativesScores[i], (1 / (double)lengthAlt));
                }
                double SumAlternativesScores = alternativesScores.Sum();


                for (int i = 0; i < lengthAlt; i++)
                {
                    weightsAlts[k][i] = alternativesScores[i] / SumAlternativesScores;
                }
            }

            double[] resultWeightsAlts = new double[lengthAlt];
            for (int i = 0; i < lengthAlt; i++)
            {
                for (int k = 0; k < relations.Length; k++)
                {
                    resultWeightsAlts[i] += weightsAlts[k][i];
                }
                resultWeightsAlts[i] /= relations.Length;
            }

            return resultWeightsAlts;
        }
        
        static double[] GetGlobalPriorities(double[] kriteriesWeights, params double[][] alternativesWeightsByKrits)
        {
            double[] globalPriorities = new double[alternativesWeightsByKrits[0].Length];
            for (int i = 0; i < alternativesWeightsByKrits[0].Length; i++)
            {
                for (int k = 0; k < alternativesWeightsByKrits.Length; k++)
                {
                    globalPriorities[i] += kriteriesWeights[k] * alternativesWeightsByKrits[k][i];
                }
            }

            return globalPriorities;
        }

    }

}

