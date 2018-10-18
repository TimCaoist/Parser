using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Analyse.Models
{
    public class ExpressionInfo
    {
        public ParamMode Mode { get; internal set; }
        public int StartIndex { get; internal set; }
        public string Str { get; internal set; }
    }
}
