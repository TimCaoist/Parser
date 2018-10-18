using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;

namespace SD.Parser.Models
{
    public class TypeDict : Dictionary<string, Type>
    {
        public Type FetchType(string typeName)
        {
            Type type;
            if (this.TryGetValue(typeName, out type))
            {
                return type;
            }

            ITypeFetch typeFetch = Util.UtilContainer.Resolve<ITypeFetch>();
            type = typeFetch.FetchType(typeName);
            this.Add(typeName, type);
            return type;
        }
    }
}
