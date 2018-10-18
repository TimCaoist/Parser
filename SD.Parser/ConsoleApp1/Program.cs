using SD.Pareser.Excuter.Standard;
using SD.Parser;
using SD.Parser.Models;
using SD.Parser.Util;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace ConsoleApp1
{
    public static class Test
    {
        public static TResult ExpInvoke<TResult>(Expression expression, string expName, object data = null)
        {
            var expression1 = expression.Releateds.First(r => r.Name == expName);
            return ParserCenter.Evals<TResult>(expression1.Data, expression, data);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Config.Init();
            //var s = new
            //{
            //    sb = new string[] { "b", "a" }
            //};

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for (var i = 0; i < 1000; i++)
            //{
            //    var e = ParserCenter.Evals<int>("foreach(var item in sb.GroupBy(s => s).ToArray()){ }; return 1;", new Expression()
            //    {
            //        Data = "@exp_sdsd",
            //        Mode = TypeMode.Expression,
            //        Releateds = new Expression[]{
            //        new Expression{
            //            Mode = TypeMode.Expression,
            //            Data = "var s =  new { X = 1, Y = 2}; @exp_test_int(s);",
            //            Name = "sdsd"
            //        },
            //        new Expression{
            //            Mode = TypeMode.Expression,
            //            Data = "return X + Y;",
            //            Name = "test"
            //        }
            //    }
            //    }, s);

            //}

            //sw.Stop();
            //Console.Write(sw.ElapsedMilliseconds);

            //Console.Read();
        }
    }
}
