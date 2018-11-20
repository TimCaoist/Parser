using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.PrecompilerFunc
{
    public class SumItem : MathItem
    {
        private const string SUMStr = "sum";

        public override string KeyWord => SUMStr;
    }
}
