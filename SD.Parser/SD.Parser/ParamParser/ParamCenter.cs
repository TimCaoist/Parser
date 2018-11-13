using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using SD.Parser.Models;
using SD.Parser.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Parser.ParamParser
{
    public class ParamCenter : Interface.IParamRegular
    {
        public string Regular(string expression, IEnumerable<ExpressionInfo> expressionInfos, Expression exps, IEnumerable<Type> staticTypes)
        {
            foreach (var expressionInfo in expressionInfos.OrderByDescending(e => e.StartIndex))
            {
                var regular = Create(expressionInfo);
                if (regular == null)
                {
                    continue;
                }

                expression = regular.Regular(expression, expressionInfo, exps, staticTypes);
            }

            IKeyWordProvider keyWordProvider = UtilContainer.Resolve<IKeyWordProvider>();
            return expression.Replace(keyWordProvider.Param, string.Empty);
        }

        public Interface.IItemRegular Create(ExpressionInfo expressionInfo)
        {
            switch (expressionInfo.Mode)
            {
                case ParamMode.Expression:
                    return Util.UtilContainer.Resolve<ExpressionRegular>();
                case ParamMode.Func:
                    return Util.UtilContainer.Resolve<FuncRegular>();
            }

            return null;
        }
    }
}
