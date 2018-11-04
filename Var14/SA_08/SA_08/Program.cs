using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_08
{
    class Program
    {
        static void Main(string[] args)
        {
            // тута инфа из таблицы
            double[,] parametrs = new double[,] {
           //Alt
/*Parametr*/{ 5, 5, 7, 3, 9 },
            { 3, 7, 1, 2, 1 },
            { 5, 1, 6, 1, 5 },
            { 5, 3, 4, 1, 2 }};

            double[,] modifyedParametrs = GetPareto(parametrs);
            PrintMassive(modifyedParametrs);
            double[] wegths = GetWeightOfAlterns(modifyedParametrs);

            PrintMassive(GetRangs(modifyedParametrs));

            int alternativesCount = parametrs.GetLength(0);
            double[,] relationsAlternativesByFirstKrit = GetPairComparation(GetRangs(modifyedParametrs),0);
            PrintMassive(relationsAlternativesByFirstKrit);
            PrintMassive(GetMatrixOfLoses(relationsAlternativesByFirstKrit, wegths));
            Console.Read();
        }
        static double[,] GetMatrixOfLoses(double[,] par, double[] weigths)
        {
            double[,] newarr = new double[par.GetLength(0), par.GetLength(1)];
            for (int i = 0; i < par.GetLength(0); i++)
            {
                for (int j = 0; j < par.GetLength(1); j++)
                {
                    newarr[i,j] = weigths[i] * Math.Abs(par[i, j] - 1);
                }
            }
            return newarr;
        }
        static double[,] GetPairComparation(double[,] parametrs, int index)
        {
            int alternativesCount = parametrs.GetLength(0);
            double[,] relationsAlternativesByKrit = new double[alternativesCount, alternativesCount];
            for (int i = 0; i < alternativesCount; i++)//для инициализации
            {
                for (int j = 0; j < alternativesCount; j++)
                {
                    if (parametrs[index, i] < parametrs[index, j])
                    {
                        relationsAlternativesByKrit[i, j] = 1;
                    }
                    else if(parametrs[index, i] == parametrs[index, j])
                    {
                        relationsAlternativesByKrit[i, j] = 0;
                    }
                    else
                    {
                        relationsAlternativesByKrit[i, j] = -1;
                    }
                }
            }
            return relationsAlternativesByKrit;
        }
        static double[,] GetRangs(double[,] par)
        {
            double[,] raks = new double[par.GetLength(0), par.GetLength(1)];

            for (int i = 0; i < par.GetLength(0); i++)
            {
                int rank = 1;
                double[] slise = GetSlise(par, i);
                Dictionary<double, double> numberRank = new Dictionary<double, double>();
                Array.Sort(slise);
                Array.Reverse(slise);
                numberRank.Add(slise[0], rank);
                for (int j = 1; j < par.GetLength(1); j++)
                {
                    if (slise[j] != slise[j - 1])
                    {
                        rank++;
                    }
                    if (numberRank.ContainsKey(slise[j]))
                    {
                        continue;
                    }
                    numberRank.Add(slise[j], rank);
                }
                for (int k = 0; k < par.GetLength(1); k++)
                {
                    raks[i, k] = numberRank[par[i, k]];
                }
            }
            return raks;
        }
        static double[] GetSlise(double[,] arr, int index)
        {
            double[] slise = new double[arr.GetLength(1)];
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                slise[j] = arr[index, j];
            }
            return slise;
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
        private static void PrintMassive(double[,] massive)
        {
            for (int i = 0; i < massive.GetLength(0); i++)
            {
                for (int j = 0; j < massive.GetLength(1); j++)
                {
                    Console.Write(String.Format("{0,3}", massive[i, j]));
                }
                Console.WriteLine();
            }
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
    }

}
