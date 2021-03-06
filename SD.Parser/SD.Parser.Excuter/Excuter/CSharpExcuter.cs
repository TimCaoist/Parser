﻿using SD.Parser.Models;
using SD.Parser.ParamParser;
using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Parser.Excuter.Excuter
{
    public class CSharpExcuter : IExcuter
    {
        private readonly ICollection<ParamInfo> globalVariables = new List<ParamInfo>();

        private readonly List<string> nameSpaces = new List<string>();

        private static Dictionary<string, dynamic> expressions = new Dictionary<string, dynamic>();

        public TResult Execute<TResult>(string name, string expression, object paramDatas, IEnumerable<ParamInfo> paramInfos)
        {
            var allParamInfos = globalVariables.Union(paramInfos).Distinct().ToArray();
            var datas = allParamInfos.ToDictionary(d => d.Name, d => d.Data);
            dynamic target = null;
            if (expressions.TryGetValue(name, out target))
            {
                return target.Calculate(datas);
            }

            dynamic instance = null;
            lock (expressions)
            {
                if (expressions.TryGetValue(name, out target))
                {
                    return target.Calculate(datas);
                }

                var type = CodeBuild.Build(Config.TemplateCodePath, expression, name, allParamInfos, nameSpaces);
                instance = Activator.CreateInstance(type);
                expressions.Add(name, instance);
            }

            return (TResult)instance.Calculate(datas);
        }

        public void RegisterGlobalVariable(string name, object val)
        {
            if (globalVariables.Any(gv => gv.Name == name))
            {
                return;
            }

            globalVariables.Add(new ParamInfo {
                Name = name,
                Data = val,
                Type = val.GetType()
            });

            ParamConvert.SetParamTypeName(globalVariables);
        }

        public void RegisterStaticMember(params Type[] type)
        {
            RegisterType(type);
        }

        public void RegisterType(params Type[] type)
        {
            nameSpaces.AddRange(type.Select(t => t.Namespace));
        }
    }
}
