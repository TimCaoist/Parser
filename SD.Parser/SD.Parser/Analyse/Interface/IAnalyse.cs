using SD.Parser.Analyse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Analyse.Interface
{
    public interface IAnalyse
    {
        IEnumerable<ExpressionInfo> DoAnalyse(string expression);
    }
}
