using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class EvalTest : Instance
    {
        [TestMethod]
        public void TestSolutionA()
        {
            var result = Heuristics.HeuristicsBase.Eval(new List<int>(new int[] { 1, 2 }), new HashSet<int>(new int[] { 0, 3 }));

            Assert.AreEqual(result.Item1, 60.0);
            Assert.AreEqual(result.Item2, 75.0);
            Assert.AreEqual(result.Item3, false);
        }
    }
}
