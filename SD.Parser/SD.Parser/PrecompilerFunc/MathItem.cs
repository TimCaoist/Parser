using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.PrecompilerFunc
{
    public abstract class MathItem : IFuncItem
    {
        public abstract string KeyWord { get; }

        private const string MathEx = "MathEx.";

        private const string Ex = "Ex";

        public string Regular(string format, int index, int inculdeLength, int endIndex)
        {
            format = format.Replace(KeyWord, string.Concat(MathEx, KeyWord, Ex));
            return format;
        }
    }
}
