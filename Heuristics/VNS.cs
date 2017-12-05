using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Heuristics.HeuristicsBase;

namespace Heuristics
{
    public class VNS
    {
        private const int K = 3, MAX_IT = 1000;
        private List<int> solution;
        private Tuple<int, int, double, bool, double> eval;
        private List<int> avaliableItems;

        public VNS(List<int> solution = null)
        {
            this.solution = solution;
        }

        private bool localSearch(int nb)
        {
            switch (nb)
            {
                // item add
                case 0:
                    var possibleItems = new List<Tuple<int, double>>();

                    foreach (var it in avaliableItems)
                    {
                        var t = Eval(eval, solution.Count > 0 ? (int?)solution.Last() : null, it);

                        if (t.Item4)
                            possibleItems.Add(new Tuple<int, double>(it, t.Item1));
                    }

                    if (possibleItems.Count == 0)
                        return false;

                    possibleItems = possibleItems.OrderByDescending(p => p.Item2).ToList();

                    var tot = possibleItems.Aggregate(0.0, (agg, p) => agg + p.Item2);

                    var probs = possibleItems.Select(p => p.Item2 / tot).ToList();

                    var rd = rand.NextDouble();

                    var sum = 0.0;

                    for (var i = 0; i < possibleItems.Count; i++)
                    {
                        sum += probs[i];

                        if (sum >= rd)
                        {
                            eval = Eval(eval, solution.Count > 0 ? (int?)solution.Last() : null, possibleItems[i].Item1);

                            avaliableItems.Remove(possibleItems[i].Item1);

                            solution.Add(possibleItems[i].Item1);

                            return true;
                        }
                    }

                    return false;
                // item swap
                case 1:
                    if (avaliableItems.Count == 0)
                        return false;

                    int sPos = rand.Next(solution.Count);
                    int aPos = rand.Next(avaliableItems.Count);

                    int temp = solution[sPos];
                    solution[sPos] = avaliableItems[aPos];
                    avaliableItems[aPos] = temp;

                    var nEval = Eval(solution);

                    if (nEval.Item4 && nEval.Item1 > eval.Item1)
                    {
                        eval = nEval;
                        return true;
                    }

                    temp = solution[sPos];
                    solution[sPos] = avaliableItems[aPos];
                    avaliableItems[aPos] = temp;

                    return false;
                // 2-opt
                case 2:
                    sPos = rand.Next(solution.Count);
                    aPos = rand.Next(solution.Count);

                    temp = solution[sPos];
                    solution[sPos] = solution[aPos];
                    solution[aPos] = temp;

                    nEval = Eval(solution);

                    if (nEval.Item4 && nEval.Item1 > eval.Item1)
                    {
                        eval = nEval;
                        return true;
                    }

                    temp = solution[sPos];
                    solution[sPos] = solution[aPos];
                    solution[aPos] = temp;

                    return false;
            }

            return false;
        }

        public List<int> Run()
        {
            if (solution == null)
                solution = RandomSolution();

            int i;

            var items = new HashSet<int>();

            for (i = 0; i < M; i++)
                items.Add(i);

            foreach (var it in solution)
                items.Remove(it);

            avaliableItems = items.ToList();

            eval = Eval(solution);

            i = 0;

            while (i < K)
            {
                var improved = false;

                if (i != 0)
                {
                    for (var it = 0; it < MAX_IT; it++)
                        if (localSearch(i))
                        {
                            i = 0;
                            improved = true;
                            break;
                        }
                }
                else if (localSearch(i))
                    improved = true;

                if (!improved)
                    i++;
                else
                {
                    ImproveSolution(ref solution);
                    eval = Eval(solution);
                }
            }

            return solution;
        }
    }
}
