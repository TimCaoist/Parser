using SD.Parser.Models;
using System;
using System.Collections.Generic;

using System.Text;

namespace SD.Parser.ParamParser.Interface
{
    public interface IParamRegular
    {
        string Regular(string expression, IEnumerable<Analyse.Models.ExpressionInfo> expressionInfos, Expression exps, IEnumerable<Type> staticTypes);
    }
}
