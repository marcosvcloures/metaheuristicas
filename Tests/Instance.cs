using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public abstract class Instance
    {
        [TestInitialize]
        public void LoadInstance()
        {
            Heuristics.HeuristicsBase.Load(Resources.instance);

            Heuristics.HeuristicsBase.GreedySolution();
        }
    }
}
