using SD.Parser.Analyse.Models;
using System.Collections.Generic;

namespace SD.Parser.ParamParser
{
    public class ParamCenter : Interface.IParamRegular
    {
        public string Regular(string expression, IEnumerable<ExpressionInfo> expressionInfos)
        {
            foreach (var expressionInfo in expressionInfos)
            {
                var regular = Create(expressionInfo);
                if (regular == null)
                {
                    continue;
                }

                expression = regular.Regular(expression, expressionInfo);
            }

            var keyWordProvider = Util.UtilContainer.Resolve<Analyse.Interface.IKeyWordProvider>();
            return expression.Replace(keyWordProvider.Param, string.Empty);
        }

        public Interface.IItemRegular Create(ExpressionInfo expressionInfo)
        {
            switch (expressionInfo.Mode)
            {
                case ParamMode.Expression:
                    return Util.UtilContainer.Resolve<ExpressionRegular>();
            }

            return null;
        }
    }
}
