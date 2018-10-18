using SD.Parser.Models;
using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SD.Parser.Util
{
    public static class ExcuterExtensions
    {
        private readonly static TypeDict typeDict = new TypeDict();

        public static void Register(this IExcuter context, Expression expression)
        {
            var expressions = expression.Releateds;
            if (expressions == null || !expressions.Any())
            {
                return;
            }

            foreach (var ex in expressions.Where(e => e.Mode != TypeMode.Expression))
            {
                if (string.IsNullOrEmpty(ex.Data))
                {
                    continue;
                }

                Type type;
                if (typeDict.TryGetValue(ex.Data, out type) == false)
                {
                    lock (typeDict)
                    {
                        type = typeDict.FetchType(ex.Data);
                    }
                }

                if (type == null)
                {
                    continue;
                }

                switch (ex.Mode)
                {
                    case TypeMode.ReleatedType:
                        context.RegisterType(type);
                        break;
                    case TypeMode.Static:
                        context.RegisterStaticMember(type);
                        break;
                }
            }
        }
    }
}
