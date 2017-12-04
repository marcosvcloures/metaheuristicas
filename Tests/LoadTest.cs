using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class LoadTest : Instance
    {
        [TestMethod]
        public void TestCities()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities.Count, 4);
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities[2].x, 1.0);
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities[2].y, 7.0);
        }

        [TestMethod]
        public void TestItens()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Items.Count, 5);
            Assert.AreEqual(Heuristics.HeuristicsBase.Items[2].profit, 100);
            Assert.AreEqual(Heuristics.HeuristicsBase.Items[2].weight, 3);
        }

        [TestMethod]
        public void TestCityItens()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities[2].itens.Count, 3);
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities[2].itens[2].city.id, 3);
        }

        [TestMethod]
        public void TestWeightType()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.WeightType, Heuristics.HeuristicsBase.WeightTypeEnum.CEIL_2D);
        }

        [TestMethod]
        public void TestV_max()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.V_max, 1);
        }

        [TestMethod]
        public void TestV_min()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.V_min, 0.1);
        }

        [TestMethod]
        public void TestT()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.T, 75);
        }

        [TestMethod]
        public void TestW()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.W, 3);
        }

        [TestMethod]
        public void TestM()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.M, 5);
        }

        [TestMethod]
        public void TestN()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.N, 4);
        }

        [TestMethod]
        public void TestDataType()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.DataType, Heuristics.HeuristicsBase.DataTypeEnum.UNCORRELATED);
        }

        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Name, "ex4-ThOP");
        }

        [TestMethod]
        public void TestV()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.V, 0.9 / 3);
        }

        [TestMethod]
        public void TestDistances()
        {
            int[,] dists =
            {
                {0, 5, 6, 8},
                {5, 0, 8, 6},
                {6, 8, 0, 5},
                {8, 6, 5, 0}
            };

            for(int i = 0; i < Heuristics.HeuristicsBase.N; i++)
                for (int j = 0; j < Heuristics.HeuristicsBase.N; j++)
                    Assert.AreEqual(Heuristics.HeuristicsBase.Distances[i,j], dists[i, j]);
        }
    }
}
