using SD.Parser.Models;
using SD.Parser.Util;
using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SD.Parser.Util
{
    public class TypeFetch : ITypeFetch
    {
        private static IEnumerable<Assembly> Assemblies;

        public Type FetchType(string name)
        {
            var asses = Assemblies ?? UtilContainer.Resolve<IAssemblyLoader>().GetAssemblies(name);
            var ass = asses.FirstOrDefault(a => name.StartsWith(a.FullName.Split(',')[0], StringComparison.OrdinalIgnoreCase));
            if (ass != null)
            {
                var type = ass.GetType(name);
                if (type != null)
                {
                    return type;
                }
            }

            foreach (var item in asses)
            {
                if (!item.DefinedTypes.Any(dt => dt.FullName == name))
                {
                    continue;
                }

                var type = item.GetType(name);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        public IEnumerable<ParamInfo> GetParamInfos(object obj, Type type)
        {
            var properties =  type.GetRuntimeProperties();
            ICollection<ParamInfo> paramInfos = new List<ParamInfo>();
            foreach (var p in properties)
            {
                var val = p.GetValue(obj);
                paramInfos.Add(new ParamInfo
                {
                    Name = p.Name,
                    Type = val.GetType(),
                    Data = val
                });
            }

            return paramInfos;
        }

        public bool HasMethod(Type type, string name)
        {
            var methods = type.GetRuntimeMethods();
            return methods.Any(m => m.Name == name);
        }
    }
}
