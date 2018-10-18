using SD.Parser.Analyse.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Analyse.Models
{
    public class KeyWordProvider : IKeyWordProvider
    {

        public string Param => "@";

        public char[] SplitChar { get; } = new char[] { ' ', ',', ')', '(', ';' };

        public string ExpressionString => "exp_";

        public char ExpressionSplitChar => '_';
    }
}
