using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heuristics
{
    public partial class HeuristicsBase
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
        public static List<Item> Itens { get; set; }

        public double Eval(int[] sol)
        {
            return 0.0;
        }
    }
}
