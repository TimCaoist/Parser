using SD.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Parser.Util.Interface
{
    public interface ITypeFetch
    {
        Type FetchType(string name);

        IEnumerable<ParamInfo> GetParamInfos(object obj, Type type);
    }
}
