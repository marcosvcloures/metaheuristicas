using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heuristics
{
    public static partial class HeuristicsBase
    {
        public static Tuple<int, double, bool> Eval(List<int> path, HashSet<int> itens)
        {
            var objetive = itens.Aggregate(0, (agg, p) => agg + Itens[p].profit);

            var citiesWeights = new int[N];

            foreach (var it in itens)
                citiesWeights[Itens[it].city.id - 1] += Itens[it].weight;

            double velocity = V_max, time = 0;

            if (path.Count > 0)
                time = Distances[0, path[0]] / velocity;
            else
                time = Distances[0, N - 1] / velocity;

            for (int i = 0; i < path.Count - 1; i++)
            {
                velocity -= V * citiesWeights[path[i]];

                time += Distances[path[i + 1], path[i]] / velocity;
            }

            if(path.Count > 0)
            {
                velocity -= V * citiesWeights[path.Last()];

                time += Distances[path.Last(), N-1] / velocity;
            }

            return new Tuple<int, double, bool>(objetive, time, time <= T);
        }
    }
}
