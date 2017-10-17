using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class EvalTest : Instance
    {
        [TestMethod]
        public void TestSolutionA()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 1, 2 }), new HashSet<int>(new int[] { 0, 3 }));

            Assert.AreEqual(result.Item1, 60);
            
            Assert.IsTrue(Math.Abs(result.Item2 - 75.0) < 1e-10);

            Assert.AreEqual(result.Item3, true);
        }

        [TestMethod]
        public void TestSolutionB()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 2, 1 }), new HashSet<int>(new int[] { 0, 3 }));
            
            Assert.AreEqual(result.Item3, false);
        }

        [TestMethod]
        public void TestSolutionC()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 2 }), new HashSet<int>(new int[] { 2 }));

            Assert.AreEqual(result.Item1, 100);

            Assert.IsTrue(Math.Abs(result.Item2 - 56.0) < 1e-10);

            Assert.AreEqual(result.Item3, true);
        }

        [TestMethod]
        public void TestSolutionD()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 2 }), new HashSet<int>(new int[] { 3, 4 }));

            Assert.AreEqual(result.Item1, 80);

            Assert.IsTrue(Math.Abs(result.Item2 - 18.5) < 1e-10);

            Assert.AreEqual(result.Item3, true);
        }
    }
}
