using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Models
{
    public class Expression
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public string Data { get; set; }

        public TypeMode Mode { get; set; }

        public IEnumerable<Expression> Releateds { get; set; }
    }
}
