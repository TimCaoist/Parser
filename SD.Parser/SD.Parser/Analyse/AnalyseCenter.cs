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
            var paramStrArray = expression.Split(keyWordProvider.SplitChar).Where(s => s.StartsWith(param)).Select(s => s.Replace(param, string.Empty)).ToArray();
            return paramStrArray.Select(p => builder.Build(expression, p, keyWordProvider)).ToArray();
        }
    }
}
