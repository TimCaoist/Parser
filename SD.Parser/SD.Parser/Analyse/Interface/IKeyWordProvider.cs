using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Analyse.Interface
{
    public interface IKeyWordProvider
    {
        string Param { get; }

        string ExpressionString { get; }

        char[] SplitChar { get; }

        char ExpressionSplitChar { get;  }
    }
}
