using System;
using System.Collections.Generic;
using System.Linq;

namespace Heuristics
{
    public static partial class HeuristicsBase
    {
        public static Tuple<int, double, bool> Eval(List <int> items)
        {
            var objetive = items.Aggregate(0, (agg, p) => agg + Items[p].profit);
            
            double velocity = V_max, time = 0.0;

            if (items.Count > 0)
                time = Distances[0, Items[items[0]].city.id - 1] / velocity;
            else
                time = Distances[0, N - 1] / velocity;

            for (int i = 0; i < items.Count - 1; i++)
            {
                velocity -= V * Items[items[i]].weight;

                var from = Items[items[i]].city.id - 1;
                var to = Items[items[i + 1]].city.id - 1;

                time += Distances[from, to] / velocity;
            }

            if (items.Count > 0)
            {
                velocity -= V * Items[items.Last()].weight;

                time += Distances[Items[items.Last()].city.id - 1, N - 1] / velocity;
            }

            return new Tuple<int, double, bool>(objetive, time, time <= T);
        }

        public static Tuple<int, double, bool> Eval(List<int> path, HashSet<int> items)
        {
            foreach (var it in items)
                if (path.FindIndex(p => p == Items[it].city.id) == -1)
                    return new Tuple<int, double, bool>(0, 0, false);

            List<int> itensOrdered = new List<int>(items).OrderBy(p => path.FindIndex(q => q == Items[p].city.id)).ToList();

            return Eval(itensOrdered);
        }

        public static void initGreedy()
        {
            if (ItemsGreedy != null)
                return;

            ItemsGreedy = Items.OrderBy(p => p.ppwd).ToList();

            PPWDTotal = Items.Aggregate(0.0, (agg, p) => agg + p.ppwd);

            foreach (var city in Cities)
                city.itens = city.itens.OrderBy(p => p.profit / p.weight).ToList();
        }

        public static Tuple<List<int>, HashSet<int>> greedySolution(int alfa = 0)
        {
            initGreedy();

            var probItem = ItemsGreedy.Select(p => p.ppwd / PPWDTotal).ToList();
            var path = new List<int>();
            var items = new HashSet<int>();

            for (var i = 1; i < probItem.Count; i++)
                probItem[i] += probItem[i - 1];
            
            while(Eval(path, items).Item3)
            {
                
            }

            return new Tuple<List<int>, HashSet<int>> (path, items);
        }
    }
}
