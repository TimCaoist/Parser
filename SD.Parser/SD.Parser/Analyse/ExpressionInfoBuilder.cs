using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using System.Collections.Generic;

namespace SD.Parser.Analyse
{
    public class ExpressionInfoBuilder : IExpressionInfoBuilder
    {
        public ExpressionInfo Build(string expression, string paramStr, IKeyWordProvider provider)
        {
            ParamMode mode;
            if (paramStr.StartsWith(provider.ExpressionString))
            {
                mode = ParamMode.Expression;
            }
            else if (paramStr.StartsWith(provider.FuncString))
            {
                mode = ParamMode.Func;
            }
            else {
                mode = ParamMode.Param;
            }

            return Build(expression, mode, paramStr, provider);
        }

        protected ExpressionInfo Build(string expression, ParamMode mode, string paramStr, IKeyWordProvider provider)
        {
            ExpressionInfo expressionInfo;
            switch (mode)
            {
                case ParamMode.Expression:
                    {
                        var relatedExpressionInfo = new RelatedExpressionInfo();
                        var strArry = paramStr.Replace(provider.ExpressionString, string.Empty).Split(provider.ExpressionSplitChar);
                        relatedExpressionInfo.InvokeName = strArry[0];
                        if (strArry.Length == 2)
                        {
                            relatedExpressionInfo.ReturnTypeName = strArry[1];
                        }

                        expressionInfo = relatedExpressionInfo;
                    }
                   
                    break;
                case ParamMode.Func:
                    {
                        var funcExpressionInfo = new FuncExpressionInfo();
                        var strArry = paramStr.Replace(provider.FuncString, string.Empty).Split(provider.ExpressionSplitChar);
                        funcExpressionInfo.Name = strArry[0];
                        if (strArry.Length > 1)
                        {
                            funcExpressionInfo.InvokeName = strArry[1];
                        }

                        expressionInfo = funcExpressionInfo;
                    }
                   
                    break;
                default:
                    expressionInfo = new ExpressionInfo();
                    break;
            }

            expressionInfo.Mode = mode;
            expressionInfo.Str = paramStr;

            return expressionInfo;
        }
    }
}
