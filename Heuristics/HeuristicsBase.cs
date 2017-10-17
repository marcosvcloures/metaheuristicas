using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heuristics
{
    public static partial class HeuristicsBase
    {
        public static Tuple<double, double, bool> Eval(List<int> path, HashSet<int> itens)
        {
            var objetive = itens.Aggregate(0, (agg, p) => agg + Itens[p].profit);

            var citiesItens = new Dictionary<int, List<int>>();

            foreach (var it in itens)
                if (citiesItens.ContainsKey(Itens[it].city.id - 1))
                    citiesItens[Itens[it].city.id - 1].Add(it);
                else
                    citiesItens.Add(Itens[it].city.id - 1, new List<int>(new int[] { it }));

            return new Tuple<double, double, bool>(objetive, 0.0, false);
        }
    }
}
