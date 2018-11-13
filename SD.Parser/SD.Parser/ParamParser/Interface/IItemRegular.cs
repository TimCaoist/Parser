using System;
using System.Collections.Generic;
using System.Text;
using SD.Parser.Analyse.Models;

namespace SD.Parser.ParamParser.Interface
{
    public interface IItemRegular
    {
        string Regular(string expression, ExpressionInfo expressionInfo, Models.Expression exps, IEnumerable<Type> staticTypes);
    }
}
