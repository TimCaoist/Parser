using SD.Parser.Models;
using System.Linq;

namespace SD.Parser.Util
{
    public static class ExpressionInvoke
    {
        public static TResult ExpInvoke<TResult>(Expression expression, string expName, object data = null)
        {
            var subExpression = expression.Releateds.First(r => r.Name == expName);
            return ParserCenter.Evals<TResult>(subExpression.Data, expression, data);
        }
    }
}
