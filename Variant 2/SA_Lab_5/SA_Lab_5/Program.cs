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
            { 0, 1, 3 },
            { 0, 0, 1 }};
            double[,] matrixOfRelationsKriteriesByExp2 = new double[,]
          { { 1, 5, 7 },
            { 0, 1, 0.333 },
            { 0, 0, 1 } };
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
            int[,] modifyedMatrixOfParametrs = new int[matrixOfParametrs.GetLength(0), matrixOfParametrs.GetLength(1) - indexRemoveAlts];

            for (int i = 0; i < matrixOfParametrs.GetLength(0); i++)
            {
                int jMod = 0;
                for (int j = 0; j < matrixOfParametrs.GetLength(1); j++)
                {
                    if (matrixOfParametrs[i, j] != 0)
                    {
                        modifyedMatrixOfParametrs[i, jMod] = matrixOfParametrs[i, j];
                    }
                    else
                    {
                        jMod--;
                    }
                    jMod++;
                }
            }

            for (int i = 0; i < modifyedMatrixOfParametrs.GetLength(0); i++)
            {
                for (int j = 0; j < modifyedMatrixOfParametrs.GetLength(1); j++)
                {
                    Console.Write(modifyedMatrixOfParametrs[i, j] + " ");
                }
                Console.WriteLine();
            }
            //double[,] relationsAlternativesByFirstKrit = new double[modifyedMatrixOfParametrs.GetLength(0), modifyedMatrixOfParametrs.GetLength(1)];



            //int minusRelationIndex = 1;
            //int plusRelationIndex = 1;
            //for (int j = 0; j < modifyedMatrixOfParametrs.GetLength(1); j++)
            //{
            //    if (modifyedMatrixOfParametrs[0, j] == modifyedMatrixOfParametrs[0, 0])
            //    {
            //        relationsAlternativesByFirstKrit[0, j] = 1;
            //    }
            //    else if (modifyedMatrixOfParametrs[0, 0] < modifyedMatrixOfParametrs[0, j])
            //    {
            //        minusRelationIndex += 2;
            //        relationsAlternativesByFirstKrit[0, j] = 1 / (double)minusRelationIndex;
            //    }
            //    else if (modifyedMatrixOfParametrs[0, 0] > modifyedMatrixOfParametrs[0, j])
            //    {
            //        plusRelationIndex += 2;
            //        relationsAlternativesByFirstKrit[0, j] = 1 / (double)minusRelationIndex;
            //    }
            //}

            //double[,] matrixOfRelationsAlternativesByFirstKrit = new double[1, 1];
            //double[,] matrixOfRelationsAlternativesBySecondKrit = new double[1, 1];
            //double[,] matrixOfRelationsAlternativesByThirdKrit = new double[1, 1];
            //{ { 1, 0, 0, 0, 0, 0 },
            //  { 0, 1, 0, 0, 0, 0 },
            //  { 0, 0, 1, 0, 0, 0 },
            //  { 0, 0, 0, 1, 0, 0 },
            //  { 0, 0, 0, 0, 1, 0 },
            //  { 0, 0, 0, 0, 0, 1 } };

        }

    }

}

