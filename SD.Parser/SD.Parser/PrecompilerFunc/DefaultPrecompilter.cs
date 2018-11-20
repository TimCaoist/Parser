using System.Collections.Generic;
using System.Linq;

namespace SD.Parser.PrecompilerFunc
{
    public class DefaultPrecompilter : IPrecompilter
    {
        private string[] keywords;

        public const char LeftBracket = '(';

        public const char RightBracket = ')';

        public readonly static char[] splitCharArry = new char[] { LeftBracket, RightBracket };

        public string[] KeyWords {
            get {
                if (keywords == null)
                {
                    keywords = items.Select(item => item.KeyWord).ToArray();
                }

                return keywords;
            }
        }

        public ICollection<IFuncItem> Items {
            get {
                return items;
            }
        }

        public readonly ICollection<IFuncItem> items = new List<IFuncItem>();

        public string Regular(string format)
        {
            foreach (var item in items)
            {
                var keyWord = string.Concat(item.KeyWord, LeftBracket);
                var index = format.IndexOf(keyWord);
                while (index > -1)
                {
                    var inculdeLengthIndex = index + keyWord.Length;
                    var endIndex = GetFuncEnd(format, inculdeLengthIndex);
                    format = item.Regular(format, index, inculdeLengthIndex, endIndex);
                    index = format.IndexOf(keyWord, index + 1);
                }
            }

            return format;
        }

        private int GetFuncEnd(string format, int startIndex)
        {
            int count = 1;
            int nextBracket = startIndex - 1;
            int errorCount = 0;
            while (count > 0)
            {
                nextBracket = format.IndexOfAny(splitCharArry, nextBracket + 1);
                if (format[nextBracket] == LeftBracket)
                {
                    count++;
                }
                else {
                    count -- ;
                }

                errorCount++;
                if (errorCount > 100)
                {
                    throw new System.ArgumentException("循环体过长!");
                }
            }
           
            return nextBracket;
        }
    }
}
