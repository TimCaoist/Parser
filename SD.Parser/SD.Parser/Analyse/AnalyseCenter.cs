using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using SD.Parser.Util;
using System.Collections.Generic;
using System.Linq;

namespace SD.Parser.Analyse
{
    public class AnalyseCenter : IAnalyse
    {
        public IEnumerable<ExpressionInfo> DoAnalyse(string expression)
        {
            IKeyWordProvider keyWordProvider = UtilContainer.Resolve<IKeyWordProvider>();
            var param = keyWordProvider.Param;
            if (expression.IndexOf(param) < 0)
            {
                return Enumerable.Empty<ExpressionInfo>();
            }

            IExpressionInfoBuilder builder = UtilContainer.Resolve<IExpressionInfoBuilder>();

            ICollection<ExpressionInfo> expressionInfos = new List<ExpressionInfo>();
            var paramStrArray = expression.Split(keyWordProvider.SplitChar).Where(p => p.StartsWith(keyWordProvider.Param)).Select(p => p.Replace(keyWordProvider.Param, string.Empty)).ToArray();
            int index = 0;
            foreach (var paramStr in paramStrArray)
            {
                var ei = builder.Build(expression, paramStr, keyWordProvider);
                ei.StartIndex = expression.IndexOf(paramStr, index);
                index = paramStr.Length + ei.StartIndex;
                expressionInfos.Add(ei);
            }
            
            return expressionInfos;
        }
    }
}
