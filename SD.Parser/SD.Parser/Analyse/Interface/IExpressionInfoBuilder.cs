using SD.Parser.Analyse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Analyse.Interface
{
    public interface IExpressionInfoBuilder
    {
        ExpressionInfo Build(string expression, string paramStr, IKeyWordProvider provider);
    }
}
