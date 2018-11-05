using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SD.Parser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SD.Parser.Excuter.Excuter
{
    public static class CodeBuild
    {
        private const string CodePlaceHolder = "[code]";

        private const string ParamPlaceHolder = "[param]";

        private const string ParamSplitKey = ",";

        private const string ClassPlaceHolder = "[classname]";

        private const string ParamFormat = "var {0} = ({1})datas[{2}];";

        private readonly static IEnumerable<string> locations;

        static CodeBuild()
        {
            locations = AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic && !string.IsNullOrEmpty(x.Location)).Select(x => x.Location).ToArray();
        }

        public static Type Build(string templeFileName, string expression, string className, IEnumerable<ParamInfo> paramInfos)
        {
            var tempFile = BuildCode(className, templeFileName, expression, paramInfos);
            var type = BuildType(tempFile, className);
            return type;
        }

    public static string BuildCode(string className, string templeFileName, string expression, IEnumerable<ParamInfo> paramInfos)
        {
            var tempFile = File.ReadAllText(templeFileName);
            tempFile = tempFile.Replace(CodePlaceHolder, expression);
            tempFile = tempFile.Replace(ClassPlaceHolder, className);

            var paramStr = new StringBuilder();
            for (var i = 0; i < paramInfos.Count(); i++)
            {
                var paramInfo = paramInfos.ElementAt(i);
                paramStr.AppendFormat(ParamFormat, paramInfo.Name, paramInfo.TypeName, i);
            }
            
            tempFile = tempFile.Replace(ParamPlaceHolder, paramStr.ToString());
            return tempFile;
        }

        public static Type BuildType(string codeText, string className)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(codeText);
            var type = CompileType(className, new[] { syntaxTree });
            return type;
        }

        private static Type CompileType(string originalClassName, IEnumerable<SyntaxTree> syntaxTrees)
        {
            // 指定编译选项。
            var assemblyName = $"{originalClassName}.g";
            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddReferences(locations.Select(x => MetadataReference.CreateFromFile(x)));

            // 编译到内存流中。
            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);
                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());
                    return assembly.GetTypes().First(x => x.Name == originalClassName);
                }

                throw new ArgumentException(string.Join(string.Intern(";"), result.Diagnostics.Select(d => d.ToString())));
            }
        }
    }
}
