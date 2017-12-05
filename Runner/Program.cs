using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Heuristics;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "-f")
            // if(args.Length == 0)
            {
                var directories = Directory.GetDirectories(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\\instances");

                foreach (var it in directories)
                {
                    var files = Directory.GetFiles(it);

                    foreach (var file in files)
                    {
                        Heuristics.HeuristicsBase.Load(new StreamReader(file).ReadToEnd());

                        var sol = Heuristics.HeuristicsBase.GreedySolution();

                        var vns = new VNS(sol);

                        sol = vns.Run();

                        Console.WriteLine(Heuristics.HeuristicsBase.Eval(sol).Item1);
                    }
                }
            }
            else
            {
                var entrada = "";
                int key;

                while ((key = Console.Read()) != -1)
                    entrada += Char.ConvertFromUtf32(key);

                HeuristicsBase.Load(entrada);

                //var sol = new VNS().Run();
                var sol = new GRASP().Run();

                Console.WriteLine(Heuristics.HeuristicsBase.Eval(sol).Item1);
            }
        }
    }
}
