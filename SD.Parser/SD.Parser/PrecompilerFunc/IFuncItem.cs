using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.PrecompilerFunc
{
    public interface IFuncItem
    {
        string KeyWord { get; }

        string Regular(string format, int index, int inculdeLength, int endIndex);
    }
}
