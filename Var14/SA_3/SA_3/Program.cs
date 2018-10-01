using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrixOfPreferences = {
           //alt1
   /*exp1*/ { 7, 4, 4, 3 },
            { 1, 3, 10, 4 },
            { 3, 9, 7, 4 },
            /*{ 6, 3, 1, 4 }*/ };

            double[] averageAlternatives = GetAverageAlternatives(matrixOfPreferences);
            double[] dispersionsExpert = GetDispersionOfExperts(matrixOfPreferences, averageAlternatives);
            double[] dispersionsAlternatives = GetDispersionOfAlternatives(matrixOfPreferences, averageAlternatives);
            Console.Read();
        }

        private static double[] GetDispersionOfAlternatives(int[,] marks, double[] averageAlternatives)
        {
            double[] dispersionsAlters = new double[marks.GetLength(1)];
            for (int i = 0; i < marks.GetLength(1); i++)
            {
                double sum = 0;
                for (int j = 0; j < marks.GetLength(0); j++)
                {
                    sum += marks[j, i];
                }
                dispersionsAlters[i] = (Math.Pow(marks.GetLength(0) - 1, -1) * Math.Pow((sum - averageAlternatives[i]), 2));
            }
            return dispersionsAlters;
        }

        private static double[] GetDispersionOfExperts(int[,] marks, double[] averageAlternatives)
        {
            double[] dispersionsExpert = new double[marks.GetLength(0)];
            for (int i = 0; i < marks.GetLength(0); i++)
            {
                double sum = 0;
                for (int j = 0; j < marks.GetLength(1); j++)
                {
                    sum += marks[i, j];
                }
                dispersionsExpert[i] = (Math.Pow(marks.GetLength(1) - 1, -1) * Math.Pow((sum - averageAlternatives[i]), 2));
            }
            return dispersionsExpert;
        }

        private static double[] GetAverageAlternatives(int[,] marks)
        {
            double[] averageAlternatives = new double[marks.GetLength(1)];
            for (int i = 0; i < averageAlternatives.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < marks.GetLength(0); j++)
                {
                    sum += marks[j, i];
                }
                averageAlternatives[i] = sum / marks.GetLength(0);
            }

            return averageAlternatives;
        }
    }
}
