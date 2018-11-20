using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.PrecompilerFunc
{
    public interface IPrecompilter
    {
        string[] KeyWords { get; }

        string Regular(string format);

        ICollection<IFuncItem> Items { get; }
    }
}
