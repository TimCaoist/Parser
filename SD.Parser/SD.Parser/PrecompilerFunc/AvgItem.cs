using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.PrecompilerFunc
{
    public class AvgItem : MathItem
    {
        private const string AVGStr = "avg";

        public override string KeyWord => AVGStr;
    }
}
