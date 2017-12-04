using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heuristics
{
    public static partial class HeuristicsBase
    {
        public static string Name { get; set; }
        public static DataTypeEnum DataType { get; set; }
        public static int N { get; set; }
        public static int M { get; set; }
        public static int W { get; set; }
        public static int T { get; set; }
        public static double V_min { get; set; }
        public static double V_max { get; set; }
        public static WeightTypeEnum WeightType { get; set; }

        public static List<City> Cities { get; set; }
        public static List<Item> Items { get; set; }

        // Greedy vars
        private static List<Item> ItemsGreedy { get; set; }
        private static double PPWDTotal { get; set; }

        // Heuristic vars
        public static Random rand { get; set; }

        public static int[,] Distances;

        public static double V;
    }
}
