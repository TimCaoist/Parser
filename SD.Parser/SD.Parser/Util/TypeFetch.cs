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
            var ass = asses.FirstOrDefault(a => a.DefinedTypes.Any(dt => dt.FullName == name));
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
                var type = ass.GetType(name);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
