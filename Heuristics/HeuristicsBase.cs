using System;
using System.Collections.Generic;
using System.Linq;

namespace Heuristics
{
    public static partial class HeuristicsBase
    {
        public static Tuple<int, int, double, bool> Eval(List<int> items)
        {
            var objetive = items.Aggregate(0, (agg, p) => agg + Items[p].profit);
            var capacity = items.Aggregate(0, (agg, p) => agg + Items[p].weight);

            double velocity = V_max, time = 0.0;

            if (items.Count > 0)
                time = Distances[0, Items[items[0]].city.id - 1] / velocity;
            else
                time = Distances[0, N - 1] / velocity;

            for (int i = 0; i < items.Count - 1; i++)
            {
                velocity -= V * Items[items[i]].weight;

                velocity = Math.Max(V_min, velocity);

                var from = Items[items[i]].city.id - 1;
                var to = Items[items[i + 1]].city.id - 1;

                time += Distances[from, to] / velocity;
            }

            if (items.Count > 0)
            {
                velocity -= V * Items[items.Last()].weight;

                velocity = Math.Max(V_min, velocity);

                time += Distances[Items[items.Last()].city.id - 1, N - 1] / velocity;
            }

            return new Tuple<int, int, double, bool>(objetive, capacity, time, time <= T && capacity <= W);
        }

        public static Tuple<int, int, double, bool> Eval(List<int> path, HashSet<int> items)
        {
            foreach (var it in items)
                if (path.FindIndex(p => p == Items[it].city.id) == -1)
                    return new Tuple<int, int, double, bool>(0, 0, 0, false);

            List<int> itensOrdered = new List<int>(items).OrderBy(p => path.FindIndex(q => q == Items[p].city.id)).ToList();

            return Eval(itensOrdered);
        }

        public static List<int> RandomSolution()
        {
            var solution = new List<int>();
            var used = new HashSet<int>();

            while (Eval(solution).Item4 && used.Count != M)
            {
                var item = rand.Next(M);

                while (used.Contains(item))
                    item = rand.Next(M);

                solution.Add(item);
                used.Add(item);
            }

            if (used.Count != M)
                solution.Remove(solution.Last());

            return solution;
        }

        public static List<int> GreedySolution(double alfa = 0)
        {
            var solution = new List<int>();
            var valid = new HashSet<int>();
            var evals = new List<Tuple<int, Double>>();

            for (var i = 0; i < M; i++)
                valid.Add(i);

            var eval = Eval(solution);

            while (true)
            {
                evals.Clear();

                foreach (var it in valid)
                {
                    solution.Add(it);
                    eval = Eval(solution);

                    if (eval.Item4)
                    {
                        if(DataType == DataTypeEnum.UNCORRELATED)
                            evals.Add(new Tuple<int, double>(it, eval.Item1 / (eval.Item2 * eval.Item3)));
                        else if(DataType == DataTypeEnum.CORRELATED)
                            evals.Add(new Tuple<int, double>(it, eval.Item1 / eval.Item3));
                        else if(DataType == DataTypeEnum.SIMILAR)
                            evals.Add(new Tuple<int, double>(it, eval.Item1 / eval.Item2));
                    }
                    solution.Remove(solution.Last());
                }

                if (evals.Count == 0)
                    return solution;

                evals = evals.OrderByDescending(p => p.Item2).ToList();

                var tot = evals.Aggregate(0.0, (agg, p) => agg + p.Item2);

                var sum = 0.0;

                var lim = 0;

                for (; lim < evals.Count; lim++)
                {
                    sum += evals[lim].Item2 / tot;

                    if (sum >= alfa)
                        break;
                }

                solution.Add(evals[rand.Next(lim + 1)].Item1);

                valid.Remove(solution.Last());
            }
        }
    }
}