using SD.Parser.Analyse.Models;
using SD.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SD.Parser.ParamParser
{
    public class ExpressionRegular : Interface.IItemRegular
    {
        private readonly static string ExpressInvoke = "SD.Parser.Util.ExpressionInvoke.ExpInvoke";

        public string Regular(string expression, ExpressionInfo expressionInfo, Expression exps, IEnumerable<Type> staticTypes)
        {
            string replace = string.Empty;
            var text = expressionInfo.Str + "(";
            RelatedExpressionInfo info = (RelatedExpressionInfo)expressionInfo;
            Regex reg;
            var v = expression.IndexOf(text, expressionInfo.StartIndex);
            if (v == -1)
            {
                reg = new Regex(expressionInfo.Str);
                replace = $"{ExpressInvoke}<{info.ReturnTypeName}>({ParserCenter.GLOBAL_EPXRESSION_NAME}, \"{info.InvokeName}\")";
                return reg.Replace(expression, replace, 1, expressionInfo.StartIndex);
            }

            replace = $"{ExpressInvoke}<{info.ReturnTypeName}>({ParserCenter.GLOBAL_EPXRESSION_NAME}, \"{info.InvokeName}\",";
            reg = new Regex(expressionInfo.Str + "\\(");
            var str = reg.Replace(expression, replace, 1, expressionInfo.StartIndex);
            return str;
        }
    }
}
