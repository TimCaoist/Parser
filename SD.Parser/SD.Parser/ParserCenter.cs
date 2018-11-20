using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using SD.Parser.Models;
using SD.Parser.ParamParser;
using SD.Parser.PrecompilerFunc;
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

        private readonly static List<Type> GLOBAL_TYPES = new List<Type>();

        private readonly static List<Type> GLOBAL_STATIC_TYPES = new List<Type>();

        static ParserCenter()
        {
            RegisterGloabalTypes(typeof(Expression), typeof(ExpressionInvoke));
        }

        public static void RegisterGloabalTypes(params Type[] types)
        {
            GLOBAL_TYPES.AddRange(types);
        }

        public static void RegisterStaticGlobalTypes(params Type[] types)
        {
            GLOBAL_STATIC_TYPES.AddRange(types);
        }

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
                Name = string.Concat("template_", Guid.NewGuid().ToString().Replace("-", string.Empty)),
                Data = expression
            }, datas);
        }

        public static TResult Evals<TResult>(Expression expression, IDictionary<string, object> datas = null)
        {
            return Evals<TResult>(expression.Data, expression.Name, expression, datas);
        }

        public static TResult Evals<TResult>(string expressionStr, string name, Expression expression, object data = null)
        {
            if (string.IsNullOrEmpty(expressionStr))
            {
                throw new ArgumentNullException(nameof(expressionStr));
            }

            var infos = Analyse(expressionStr);
            var format = expressionStr;
            if (infos.Any(i => i.Mode != ParamMode.Param))
            {
                var parser = UtilContainer.Resolve<ParamParser.Interface.IParamRegular>();
                format = parser.Regular(format, infos, expression, GLOBAL_STATIC_TYPES);
            }

            IPrecompilter precompilter = UtilContainer.Resolve<IPrecompilter>();
            if (precompilter.KeyWords.Any())
            {
                format = precompilter.Regular(format);
            }

            var excuterCreator = UtilContainer.Resolve<IExcuterCreator>();
            var context = excuterCreator.BuildExcuter();
            RegisterBasicType(context, expression);
            return context.Execute<TResult>(name, format, data, ParamConvert.GetParamInfos(data));
        }

        private static void RegisterBasicType(IExcuter context, Expression expression)
        {
            context.Register(expression);
            context.RegisterStaticMember(GLOBAL_STATIC_TYPES.ToArray());
            context.RegisterType(GLOBAL_TYPES.ToArray());
            context.RegisterGlobalVariable(GLOBAL_EPXRESSION_NAME, expression);
        }
    }
}
