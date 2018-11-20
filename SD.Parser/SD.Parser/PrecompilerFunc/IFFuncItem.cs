using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Parser.PrecompilerFunc
{
    public class IFFuncItem : IFuncItem
    {
        private const string IfStr = "IF";

        private const string ReturnStr = "return ";

        private const char comma = ',';

        private readonly static char[] splitCharArry = new char[] { DefaultPrecompilter.LeftBracket, DefaultPrecompilter.RightBracket, comma };

        public string KeyWord => IfStr;

        public string Regular(string format, int index, int inculdeLength, int endIndex)
        {
            var ifExpression = format.Substring(inculdeLength, endIndex - inculdeLength);
            var commaFirst = GetComma(ifExpression, 0);
            var commaSecond = GetComma(ifExpression, commaFirst + 1);

            if (commaFirst == -1 || commaSecond == -1)
            {
                throw new ArgumentException("IF语法错误。");
            }

            var strs = new string[] {
                ifExpression.Substring(0, commaFirst),
                ifExpression.Substring(commaFirst + 1, commaSecond - commaFirst - 1),
                ifExpression.Substring(commaSecond + 1)
            };
            
            var str = format.Remove(index, endIndex + 1 - index);
            return str.Insert(index, Build(strs));
        }

        private int GetComma(string expression, int startIndex)
        {
            int count = 0;
            int nextBracket = expression.IndexOfAny(splitCharArry, startIndex);
            while (nextBracket > -1)
            {
                if (expression[nextBracket] == DefaultPrecompilter.LeftBracket)
                {
                    count++;
                }
                else if (expression[nextBracket] == DefaultPrecompilter.RightBracket)
                {
                    count--;
                }
                else if (count == 0)
                {
                    return nextBracket;
                }

                nextBracket = expression.IndexOfAny(splitCharArry, nextBracket + 1);
            }

            return nextBracket;
        }

        private static string Build(IEnumerable<string> args)
        {
            var code = string.Concat("if(", args.ElementAt(0), "){ ", args.ElementAt(1).IndexOf(IfStr) > -1 ? string.Empty : ReturnStr, args.ElementAt(1), "; }else{", args.ElementAt(2).IndexOf(IfStr) > -1 ? string.Empty : ReturnStr, args.ElementAt(2), "; }");
            return code;
        }
    }
}
