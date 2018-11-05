using SD.Parser.Models;
using SD.Parser.Util;
using SD.Parser.Util.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SD.Parser.ParamParser
{
    public static class ParamConvert
    {
        private const string GType = "`";

        private readonly static char[] GSplitChars = new[] { '[', ','};

        private const string IEnumerableTypeName = "IEnumerable<{0}>";

        public static IEnumerable<ParamInfo> GetParamInfos(object datas)
        {
            if (datas == null)
            {
                return Enumerable.Empty<ParamInfo>();
            }

            IDictionary<string, object> d = datas as IDictionary<string, object>;
            if (d != null)
            {
                return GetDictParamInfos((IDictionary<string, object>)datas);
            }

            return GetData(datas);
        }

        private static IEnumerable<ParamInfo> GetData(object obj)
        {
            ITypeFetch typeFetch = UtilContainer.Resolve<ITypeFetch>();
            var paramInfos = typeFetch.GetParamInfos(obj, obj.GetType());
            SetParamTypeName(paramInfos);
            return paramInfos;
        }


        private static IEnumerable<ParamInfo> GetDictParamInfos(IDictionary<string, object> datas)
        {
            var paramInfos = datas.Select(d => new ParamInfo
            {
                Name = d.Key,
                Data = d.Value,
                Type = d.Value.GetType()
            }).ToArray();

            SetParamTypeName(paramInfos);
            return paramInfos;
        }

        public static void SetParamTypeName(IEnumerable<ParamInfo> paramInfos)
        {
            foreach (var item in paramInfos)
            {
                if (!string.IsNullOrEmpty(item.TypeName))
                {
                    continue;
                }

                if (item.Type.Name.Contains(GType))
                {
                    var strs = item.Type.FullName.Split(GSplitChars).Where(s => !string.IsNullOrEmpty(s));
                    item.TypeName = string.Format(IEnumerableTypeName, strs.ElementAt(1));
                }
                else if (item.Type.Name.Equals("Grouping", System.StringComparison.OrdinalIgnoreCase))
                {
                    var strs = item.Type.FullName.Split(GSplitChars).Where(s => !string.IsNullOrEmpty(s));
                    item.TypeName = string.Format(IEnumerableTypeName, strs.ElementAt(6));
                }
                else
                {
                    item.TypeName = item.Type.FullName;
                }
            }
        }
    }
}
