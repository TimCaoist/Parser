using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.PrecompilerFunc
{
    public class CountItem : MathItem
    {
        private const string CountStr = "count";

        public override string KeyWord => CountStr;
    }
}
