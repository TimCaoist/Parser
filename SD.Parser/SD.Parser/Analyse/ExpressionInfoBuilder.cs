using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Analyse
{
    public class ExpressionInfoBuilder : IExpressionInfoBuilder
    {
        public ExpressionInfo Build(string expression, string paramStr, IKeyWordProvider provider)
        {
            var mode = paramStr.StartsWith(provider.ExpressionString) ? ParamMode.Expression : ParamMode.Param;
            return Build(expression, mode, paramStr, provider);
        }

        protected ExpressionInfo Build(string expression, ParamMode mode, string paramStr, IKeyWordProvider provider)
        {
            ExpressionInfo expressionInfo;
            switch (mode)
            {
                case ParamMode.Expression:
                    var relatedExpressionInfo = new RelatedExpressionInfo();
                    var strArry = paramStr.Replace(provider.ExpressionString, string.Empty).Split(provider.ExpressionSplitChar);
                    relatedExpressionInfo.InvokeName = strArry[0];
                    if (strArry.Length == 2)
                    {
                        relatedExpressionInfo.ReturnTypeName = strArry[1];
                    }

                    expressionInfo = relatedExpressionInfo;
                    break;
                default:
                    expressionInfo = new ExpressionInfo();
                    break;
            }

            expressionInfo.Mode = mode;
            expressionInfo.StartIndex = expression.IndexOf(paramStr);
            expressionInfo.Str = paramStr;

            return expressionInfo;
        }
    }
}
