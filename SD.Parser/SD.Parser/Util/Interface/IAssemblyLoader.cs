using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SD.Parser.Util.Interface
{
    public interface IAssemblyLoader
    {
        IEnumerable<Assembly> GetAssemblies(string typeName);
    }
}
