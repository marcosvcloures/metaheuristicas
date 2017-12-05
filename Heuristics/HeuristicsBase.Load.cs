using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Heuristics.HeuristicsBase;

namespace Heuristics
{
    public partial class HeuristicsBase
    {
        public static void Load(string input)
        {
            int i, j;
            var data = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Name = Regex.Split(data[0], @"\s+").Last();

            var dt = Regex.Split(data[1], @"\s+").Last(p => p != "");
            DataType = dt == "uncorrelated" ? DataTypeEnum.UNCORRELATED :
                dt == "weights" ? DataTypeEnum.SIMILAR : DataTypeEnum.CORRELATED;

            N = Convert.ToInt32(Regex.Split(data[2], @"\s+").Last());
            M = Convert.ToInt32(Regex.Split(data[3], @"\s+").Last());
            W = Convert.ToInt32(Regex.Split(data[4], @"\s+").Last());
            T = Convert.ToInt32(Regex.Split(data[5], @"\s+").Last());

            V_min = Convert.ToDouble(Regex.Split(data[6], @"\s+").Last().Replace('.', ','));
            V_max = Convert.ToDouble(Regex.Split(data[7], @"\s+").Last().Replace('.', ','));

            V = (V_max - V_min) / W;

            Cities = new List<City>(N);

            for (i = 10; !data[i].StartsWith("ITEMS SECTION"); i++)
            {
                var split = Regex.Split(data[i], @"\s+");
                Cities.Add(new City
                {
                    id = Convert.ToInt32(split[0]),
                    x = Convert.ToDouble(split[1].Split(' ').Last().Replace('.', ',')),
                    y = Convert.ToDouble(split[2].Split(' ').Last().Replace('.', ','))
                });
            }

            Items = new List<Item>(M);

            for (i++; i < data.Length; i++)
            {
                var split = Regex.Split(data[i], @"\s+");

                Items.Add(new Item
                {
                    id = Convert.ToInt32(split[0]),
                    profit = Convert.ToInt32(split[1]),
                    weight = Convert.ToInt32(split[2]),
                    city = Cities[Convert.ToInt32(split[3]) - 1]
                });

                Items.Last().city.itens.Add(Items.Last());
            }

            Distances = new int[N, N];

            for (i = 0; i < N; i++)
                for (j = 0; j < N; j++)
                {
                    var dx = Cities[i].x - Cities[j].x;
                    var dy = Cities[i].y - Cities[j].y;

                    var dist = Math.Sqrt(dx * dx + dy * dy);

                    Distances[i, j] = Convert.ToInt32(Math.Ceiling(dist));

                    if (Distances[i, j] < 0)
                        throw new Exception { Source = "Overflow nas distâncias" };
                }
            
            rand = new Random();
        }
    }
}
