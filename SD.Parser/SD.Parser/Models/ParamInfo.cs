using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Models
{
    public class ParamInfo
    {
        public string Name { get; set; }

        public object Data { get; set; }

        public Type Type { get; set; }

        public string TypeName { get; set; }
    }
}
