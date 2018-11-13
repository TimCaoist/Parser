using SD.Parser.Analyse.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Analyse.Models
{
    public class KeyWordProvider : IKeyWordProvider
    {
        public virtual string Param => "@";

        public virtual char[] SplitChar { get; } = new char[] { ' ', ',', ')', '(', ';' };

        public virtual string ExpressionString => "exp_";

        public virtual char ExpressionSplitChar => '_';

        public virtual string FuncString => "func_";
    }
}
