using System.Collections.Generic;

namespace Heuristics
{
    public static partial class HeuristicsBase
    {
        public enum DataTypeEnum { UNCORRELATED, CORRELATED, SIMILAR };
        public enum WeightTypeEnum { CEIL_2D };

        public class City
        {
            public int id;
            public double x, y;
            public List<Item> itens;

            public City()
            {
                itens = new List<Item>();
            }
        }

        public struct Item
        {
            public int id, profit, weight;
            public City city;
        }
    }
}
