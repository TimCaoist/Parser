using System.Collections.Generic;
using System.Reflection;
using SD.Parser.Util.Interface;
using System.Linq;

namespace SD.Parser.Excuter.Standard
{
    public class AssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<Assembly> GetAssemblies(string typeName)
        {
            return System.AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic && !a.FullName.StartsWith("system", System.StringComparison.OrdinalIgnoreCase)).ToArray();
        }
    }
}