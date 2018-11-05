using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using SD.Parser.Models;
using SD.Parser.ParamParser;
using SD.Parser.Util;
using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Parser
{
    public static class ParserCenter
    {
        public const string GLOBAL_EPXRESSION_NAME = "global_epxression_name";

        public static IEnumerable<ExpressionInfo> Analyse(string expression)
        {
            var analyse = UtilContainer.Resolve<IAnalyse>();
            var infos = analyse.DoAnalyse(expression);
            return infos;
        }

        public static TResult Evals<TResult>(string expression, IDictionary<string, object> datas = null)
        {
            return Evals<TResult>(new Expression
            {
                Data = expression
            }, datas);
        }

        public static TResult Evals<TResult>(Expression expression, IDictionary<string, object> datas = null)
        {
            return Evals<TResult>(expression.Data, expression.Name, expression, datas);
        }

        public static TResult Evals<TResult>(string expressionStr, string name, Expression expression, object data = null)
        {
            if (string.IsNullOrEmpty(expression.Data))
            {
                throw new ArgumentNullException(nameof(expression.Data));
            }

            var infos = Analyse(expressionStr);
            var format = expressionStr;
            if (infos.Any())
            {
                var parser = UtilContainer.Resolve<ParamParser.Interface.IParamRegular>();
                format = parser.Regular(format, infos);
            }

            var excuterCreator = UtilContainer.Resolve<IExcuterCreator>();
            var context = excuterCreator.BuildExcuter();
            RegisterBasicType(context, expression);
            return context.Execute<TResult>(name, format, data, ParamConvert.GetParamInfos(data));
        }

        private static void RegisterBasicType(IExcuter context, Expression expression)
        {
            context.Register(expression);
            context.RegisterType(typeof(Expression));
            context.RegisterGlobalVariable(GLOBAL_EPXRESSION_NAME, expression);
            context.RegisterStaticMember(typeof(ExpressionInvoke));
        }
    }
}
