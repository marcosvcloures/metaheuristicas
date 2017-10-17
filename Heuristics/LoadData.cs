﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heuristics
{
    public class LoadData : HeuristicsBase
    {
        public static void Load(string input)
        {
            int i;
            var data = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Name = data[0].Split(' ').Last();

            DataType = data[1].Split(' ').Last() == "uncorrelated" ? DataTypeEnum.UNCORRELATED :
                data[1].Split(' ').Last() == "similar" ? DataTypeEnum.SIMILAR : DataTypeEnum.CORRELATED;

            N = Convert.ToInt32(data[2].Split(' ').Last());
            M = Convert.ToInt32(data[3].Split(' ').Last());
            W = Convert.ToInt32(data[4].Split(' ').Last());
            T = Convert.ToInt32(data[5].Split(' ').Last());

            V_min = Convert.ToDouble(data[6].Split(' ').Last().Replace('.', ','));
            V_max = Convert.ToDouble(data[7].Split(' ').Last().Replace('.', ','));

            Cities = new List<City>(N);

            for (i = 10; !data[i].StartsWith("ITEMS SECTION"); i++)
            {
                var split = data[i].Split('\t');

                Cities.Add(new City
                {
                    id = Convert.ToInt32(split[0]),
                    x = Convert.ToDouble(split[1].Split(' ').Last().Replace('.', ',')),
                    y = Convert.ToDouble(split[2].Split(' ').Last().Replace('.', ','))
                });
            }

            Itens = new List<Item>(M);

            for (i++; i < data.Length; i++)
            {
                var split = data[i].Split('\t');

                Itens.Add(new Item
                {
                    id = Convert.ToInt32(split[0]),
                    profit = Convert.ToInt32(split[1]),
                    weight = Convert.ToInt32(split[2]),
                    city = Cities[Convert.ToInt32(split[3]) - 1]
                });

                Itens.Last().city.itens.Add(Itens.Last());
            }
        }
    }
}