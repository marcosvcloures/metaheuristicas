using System;
using System.Collections.Generic;
using static Heuristics.HeuristicsBase;

namespace Heuristics
{
    public class GRASP
    {
        private List<int> solution;
        private Tuple<int, int, double, bool, double> bestEval;
        private double alfa;
        private const int MAX_IT = 10;

        public GRASP(double alfa = 0.1)
        {
            solution = new List<int>();
            bestEval = Eval(solution);

            this.alfa = alfa;
        }

        public List<int> Run()
        {
            for(var i = 0; i < MAX_IT; i++)
            {
                var ns = new VNS(GreedySolution(alfa)).Run();

                var ne = Eval(ns);

                if (ne.Item1 > bestEval.Item1)
                    solution = ns;
            }

            return solution;
        }
    }
}
