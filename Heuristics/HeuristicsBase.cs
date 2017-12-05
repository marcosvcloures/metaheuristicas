using System;
using System.Collections.Generic;
using System.Linq;

namespace Heuristics
{
    public static partial class HeuristicsBase
    {
        // Eval in O(n)
        public static Tuple<int, int, double, bool, double> Eval(List<int> items)
        {
            var objetive = items.Aggregate(0, (agg, p) => agg + Items[p].profit);
            var weight = items.Aggregate(0, (agg, p) => agg + Items[p].weight);

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

            return new Tuple<int, int, double, bool, double>(objetive, weight, time, time <= T && weight <= W, velocity);
        }

        // Append eval in O(1)
        public static Tuple<int, int, double, bool, double> Eval(Tuple<int, int, double, bool, double> eval, int? lastItem, int item)
        {
            var profit = eval.Item1;
            var weight = eval.Item2;
            var time = eval.Item3;
            var velocity = eval.Item5;

            if (lastItem.HasValue)
                time -= Distances[Items[lastItem.Value].city.id - 1, N - 1] / velocity;
            else
                time -= Distances[0, N - 1] / velocity;

            weight += Items[item].weight;

            if (lastItem.HasValue)
                time += Distances[Items[lastItem.Value].city.id - 1, Items[item].city.id - 1] / velocity;
            else
                time += Distances[0, Items[item].city.id - 1] / velocity;

            velocity -= V * Items[item].weight;

            velocity = Math.Max(V_min, velocity);

            time += Distances[Items[item].city.id - 1, N - 1] / velocity;

            profit += Items[item].profit;

            return new Tuple<int, int, double, bool, double>(profit, weight, time, time <= T && weight <= W, velocity);
        }

        public static Tuple<int, int, double, bool, double> Eval(List<int> path, HashSet<int> items)
        {
            List<int> map = new List<int>(N);

            for (var i = 0; i < N; i++)
                map.Add(-1);

            for (var i = 0; i < path.Count; i++)
                map[path[i] - 1] = i;

            foreach (var it in items)
                if (map[Items[it].city.id - 1] == -1)
                    return new Tuple<int, int, double, bool, double>(0, 0, 0, false, 0);

            List<int> itensOrdered = new List<int>(items).OrderBy(p => map[Items[p].city.id - 1]).ToList();

            return Eval(itensOrdered);
        }

        public static List<int> RandomSolution()
        {
            var solution = new List<int>(M);

            int i;

            for (i = 0; i < M; i++)
                solution.Add(i);

            rand.Shuffle<int>(ref solution);
            
            var eval = Eval(new List<int>());
            
            for (i = 0; i < M; i++)
            {
                eval = Eval(eval, i != 0 ? (int?)solution[i - 1] : null, solution[i]);

                if(!eval.Item4)
                    break;
            }
            
            return solution.Take(i).ToList();
        }

        public static List<int> GreedySolution(double alfa = 0)
        {
            var solution = new List<int>();
            var valid = new HashSet<int>();
            var evals = new List<Tuple<int, Double>>();

            for (var i = 0; i < M; i++)
                valid.Add(i);

            var oldEval = Eval(solution);

            while (true)
            {
                evals.Clear();

                foreach (var it in valid)
                {
                    var eval = Eval(oldEval, solution.Count > 0 ? (int?)solution.Last() : null, it);

                    if (eval.Item4)
                    {
                        switch (DataType)
                        {
                            case DataTypeEnum.CORRELATED:
                                evals.Add(new Tuple<int, double>(it, eval.Item1 / eval.Item3));
                                break;

                            case DataTypeEnum.UNCORRELATED:
                            case DataTypeEnum.SIMILAR:
                                evals.Add(new Tuple<int, double>(it, eval.Item1 / (eval.Item3 * eval.Item3)));
                                break;
                        }
                    }
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

                if (lim == evals.Count)
                    lim--;

                var newItem = evals[rand.Next(lim + 1)].Item1;

                oldEval = Eval(oldEval, solution.Count > 0 ? (int?)solution.Last() : null, newItem);

                solution.Add(newItem);

                valid.Remove(newItem);
            }
        }

        public static void ImproveSolution(ref List<int> solution)
        {
            var lastAppear = new Dictionary<int, int>();

            for (var i = 0; i < solution.Count; i++)
                lastAppear[Items[solution[i]].city.id] = i;

            solution = solution.OrderBy(p => lastAppear[Items[p].city.id]).ToList();
        }
    }
}