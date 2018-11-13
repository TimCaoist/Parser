using System.Collections.Generic;

namespace SD.Parser.Util
{
    public static class ParamGetter
    {
        public static TResult GetVal<TResult>(this IDictionary<string, object> datas, string key)
        {
            object data;
            if (datas.TryGetValue(key, out data))
            {
                return (TResult)data;
            }

            return default(TResult);
        }
    }
}
