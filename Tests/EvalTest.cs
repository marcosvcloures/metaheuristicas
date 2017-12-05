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
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 2, 3 }), new HashSet<int>(new int[] { 0, 3 }));

            Assert.AreEqual(result.Item1, 60);

            Assert.IsTrue(Math.Abs(result.Item3 - 75.0) < 1e-10);

            Assert.AreEqual(result.Item4, true);
        }

        [TestMethod]
        public void TestSolutionB()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 3, 2 }), new HashSet<int>(new int[] { 0, 3 }));

            Assert.AreEqual(result.Item4, false);
        }

        [TestMethod]
        public void TestSolutionC()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 3 }), new HashSet<int>(new int[] { 2 }));

            Assert.AreEqual(result.Item1, 100);

            Assert.IsTrue(Math.Abs(result.Item3 - 56.0) < 1e-10);

            Assert.AreEqual(result.Item4, true);
        }

        [TestMethod]
        public void TestSolutionD()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 3 }), new HashSet<int>(new int[] { 3, 4 }));

            Assert.AreEqual(result.Item1, 80);

            Assert.IsTrue(Math.Abs(result.Item3 - 18.5) < 1e-10);

            Assert.AreEqual(result.Item4, true);
        }

        [TestMethod]
        public void TestSimullateEval()
        {
            var evalInit = Heuristics.HeuristicsBase.Eval(new List<int>());

            var evalSimullated = Heuristics.HeuristicsBase.Eval(evalInit, null, 3);

            var evalReal = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 3 }));

            Assert.AreEqual(evalReal, evalSimullated);
        }

        [TestMethod]
        public void TestSimullateEval2()
        {
            var evalInit = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 0 }));

            var evalSimullated = Heuristics.HeuristicsBase.Eval(evalInit, 0, 3);

            var evalReal = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 0, 3 }));

            Assert.AreEqual(evalReal, evalSimullated);
        }

        [TestMethod]
        public void TestSimullateEval3()
        {
            var evalInit = Heuristics.HeuristicsBase.Eval(new List<int>());

            var evalSimullated = Heuristics.HeuristicsBase.Eval(evalInit, null, 0);
            evalSimullated = Heuristics.HeuristicsBase.Eval(evalSimullated, 0, 3);

            var evalReal = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 0, 3 }));

            Assert.AreEqual(evalReal, evalSimullated);
        }

        [TestMethod]
        public void RandomIsValid()
        {
            for (var i = 0; i < 1000; i++)
                Assert.AreEqual(Heuristics.HeuristicsBase.Eval(Heuristics.HeuristicsBase.RandomSolution()).Item4, true);
        }

        [TestMethod]
        public void GreedyIsValid()
        {
            Assert.AreEqual(Heuristics.HeuristicsBase.Eval(Heuristics.HeuristicsBase.GreedySolution()).Item4, true);

            for (var i = 0; i < 100; i++)
            {
                Assert.AreEqual(Heuristics.HeuristicsBase.Eval(Heuristics.HeuristicsBase.GreedySolution(0.5)).Item4, true);
                Assert.AreEqual(Heuristics.HeuristicsBase.Eval(Heuristics.HeuristicsBase.GreedySolution(1)).Item4, true);
            }
        }
    }
}
