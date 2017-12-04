using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BigInstance
    {
        [TestInitialize]
        public void LoadInstance()
        {
            Heuristics.HeuristicsBase.Load(Resources.big_instance);
        }

        [TestMethod]
        public void TestCities()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities.Count, 1000);
        }

        [TestMethod]
        public void TestCity()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities[666].x, 340458.0);
            Assert.AreEqual(Heuristics.HeuristicsBase.Cities[666].y, 950903.0);
        }

        [TestMethod]
        public void TestItems()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Items.Count, 9980);
        }

        [TestMethod]
        public void TestItem()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Items[6666].city.id, 680);
            Assert.AreEqual(Heuristics.HeuristicsBase.Items[6666].profit, 700);
            Assert.AreEqual(Heuristics.HeuristicsBase.Items[6666].weight, 638);
        }
    }
}
