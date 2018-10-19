using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Info[] infos =
            {
                new Info(190,1),
                new Info(100,9),
                new Info(120,5),
                new Info(90,8),
                new Info(170,2),
                new Info(200,5),
                new Info(150,10),
            };
            int M = infos.Length;

            //деление

            NormirovaniePrice(infos);
            NormirovanieMark(infos);
            ViewInfos(infos);

            ViewList(MethodMaximana(infos));

            Console.Read();
        }

        private static List<List<double>> MethodMaximana(Info[] infos)
        {
            int klastersCount = 2;
            double T = 0;
            List<double[]> res = new List<double[]>();
            List<Info> prototypes = new List<Info>();
            prototypes.Add(infos[0]);
            double[] evclidesDistances = GetEvclidesDistances(infos, prototypes[0]);
            res.Add(evclidesDistances);
            prototypes.Add(infos[1]);
            prototypes.Add(infos[IndexOf(evclidesDistances, evclidesDistances.Max())]);
            T = (GetEvclideDistance(prototypes[0].Mark, prototypes[0].Price, prototypes[1].Mark, prototypes[1].Price)) / 2;
            res.Add(GetEvclidesDistances(infos, prototypes[1]));
            res.Add(PrinadlejnostKlateru(res));
            int isEnded = IsEnded(GetMaxes(GroupByKlasters(res, klastersCount)), T);
            while (isEnded != -1)
            {
                res.RemoveAt(res.Count - 1);
                prototypes.Add(infos[isEnded]);
                klastersCount++;
                T = (GetEvclideDistance(prototypes)) / (klastersCount * (klastersCount - 1));
                res.Add(GetEvclidesDistances(infos, prototypes[isEnded]));
                res.Add(PrinadlejnostKlateru(res));
                isEnded = IsEnded(GetMaxes(GroupByKlasters(res, klastersCount)), T);
            }
            ViewResult(res);
            return GroupByKlasters(res, klastersCount);
        }

        private static double GetEvclideDistance(List<Info> prototypes)
        {
            double sum = 0;
            for (int i = 0; i < prototypes.Count; i++)
            {
                sum += Math.Pow(prototypes[i].Mark - prototypes[i].Price, 2);
            }
            return Math.Sqrt(sum);
        }

        static int IsEnded(List<double> res, double T)
        {
            for (int i = 0; i < res.Count; i++)
            {
                if (res[i] > T)
                {
                    return i;
                }
            }
            return -1;
        }

        static List<double> GetMaxes(List<List<double>> res)
        {
            List<double> maxes = new List<double>();
            for (int i = 0; i < res.Count; i++)
            {
                maxes.Add(res[i].Max());
            }
            return maxes;
        }

        static List<List<double>> GroupByKlasters(List<double[]> result, int klastersCount)
        {
            List<List<double>> clatersCount = new List<List<double>>();

            double[] maxes = new double[result.Count - 1];
            for (int j = 0; j < klastersCount; j++)
            {
                List<double> vs = new List<double>();
                for (int i = 0; i < result[result.Count - 1].Length; i++)
                {
                    for (int g = 0; g < result.Count - 1; g++)
                    {
                        if (j + 1 == result[result.Count - 1][i] && j == g)
                        {
                            vs.Add(result[g][i]);
                        }
                    }
                }
                clatersCount.Add(vs);
            }
            return clatersCount;
        }

        static double[] PrinadlejnostKlateru(List<double[]> result)
        {
            double[] prinadl = new double[result[0].Length];
            double minValue = double.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < prinadl.Length; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    if (result[j][i] < minValue)
                    {
                        minValue = result[j][i];
                        minIndex = j + 1;
                    }
                }
                prinadl[i] = minIndex;
                minIndex = 0;
                minValue = double.MaxValue;
            }
            return prinadl;
        }

        static double[] GetEvclidesDistances(Info[] infos, Info prototype)
        {
            double mark = prototype.Mark;
            double price = prototype.Price;
            double[] distances = new double[infos.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                distances[i] = GetEvclideDistance(mark, price, infos[i].Mark, infos[i].Price);
            }
            return distances;
        }

        static double GetEvclideDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - y1, 2) + Math.Pow(x2 - y2, 2));
        }

        static void NormirovanieMark(Info[] infos)
        {
            double max = infos.Max(inf => inf.Mark);
            foreach (Info info in infos)
            {
                info.Mark = info.Mark / max;
            }
        }

        static void NormirovaniePrice(Info[] infos)
        {
            double max = infos.Max(inf => inf.Price);
            foreach (Info info in infos)
            {
                info.Price = info.Price / max;
            }
        }

        static int IndexOf<T>(IEnumerable<T> arr, T value)
        {
            for (int i = 0; i < arr.ToArray().Length; i++)
            {
                if (arr.ToArray()[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }

        static void ViewList(List<List<double>> evclidesDistances)
        {
            for (int i = 0; i < evclidesDistances.Count; i++)
            {
                for (int j = 0; j < evclidesDistances[i].Count; j++)
                {
                    Console.Write(evclidesDistances[i][j] + "|\\|");
                }
                Console.WriteLine();
            }
        }

        static void ViewResult(List<double[]> evclidesDistances)
        {
            for (int i = 0; i < evclidesDistances.Count; i++)
            {
                for (int j = 0; j < evclidesDistances[i].Length; j++)
                {
                    Console.Write(evclidesDistances[i][j] + "|\\|");
                }
                Console.WriteLine();
            }
        }

        static void ViewDoubleArr<T>(IEnumerable<T> evclidesDistances)
        {
            foreach (var item in evclidesDistances)
            {
                Console.Write(item + "|||");
            }
        }

        static void ViewInfos(Info[] infos)
        {
            foreach (var item in infos)
            {
                Console.Write(item.Price + " ||| ");
                Console.Write(item.Mark);
                Console.WriteLine();
            }
        }
    }

}
