using System;
using System.Collections.Generic;
using System.Text;
using SD.Parser.Models;

namespace SD.Parser.Util.Interface
{
    public interface IExcuter
    {
        TResult Execute<TResult>(string name, string expression, object paramDatas, IEnumerable<ParamInfo> paramInfos);

        void RegisterGlobalVariable(string name, object val);

        void RegisterType(params Type[] type);

        void RegisterStaticMember(params Type[] type);
    }
}
