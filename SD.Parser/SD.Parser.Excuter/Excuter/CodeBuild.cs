using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SD.Parser.Models;
using SD.Parser.Util;
using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SD.Parser.Excuter.Excuter
{
    public static class CodeBuild
    {
        private const string CodePlaceHolder = "[code]";

        private const string ParamPlaceHolder = "[param]";

        private const string ParamSplitKey = ",";

        private const string ClassPlaceHolder = "[classname]";

        private const string NameSpaceHolder = "[namespace]";

        private const string ParamFormat = "var {0} = datas.GetVal<{1}>(\"{0}\");";

        private const string NameSpaceFormat = "using {0};";

        private readonly static IEnumerable<string> locations;

        static CodeBuild()
        {
            locations = AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic && !string.IsNullOrEmpty(x.Location)).Select(x => x.Location).ToArray();
        }

        public static Type Build(string templeFileName, string expression, string className, IEnumerable<ParamInfo> paramInfos, IEnumerable<string> nameSpaces)
        {
            var tempFile = BuildCode(className, templeFileName, expression, paramInfos, nameSpaces);
            ILogger logger = UtilContainer.Resolve<ILogger>();
            logger.Info(string.Concat("code template:", tempFile));
            var type = BuildType(tempFile, className);
            return type;
        }

        public static string BuildCode(string className, string templeFileName, string expression, IEnumerable<ParamInfo> paramInfos, IEnumerable<string> nameSpacess)
        {
            var tempFile = File.ReadAllText(templeFileName);
            tempFile = tempFile.Replace(CodePlaceHolder, expression);
            tempFile = tempFile.Replace(ClassPlaceHolder, className);
            tempFile = tempFile.Replace(NameSpaceHolder, string.Join(string.Empty, nameSpacess.Distinct().Select(ns => string.Format(NameSpaceFormat, ns))));
            var paramStr = new StringBuilder();
            for (var i = 0; i < paramInfos.Count(); i++)
            {
                var paramInfo = paramInfos.ElementAt(i);
                paramStr.AppendFormat(ParamFormat, paramInfo.Name, paramInfo.TypeName);
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

                ILogger logger = UtilContainer.Resolve<ILogger>();
                logger.Warning(string.Join("\r\n", result.Diagnostics.Select(d => d.ToString())));

                throw new ArgumentException(string.Join(string.Intern(";"), result.Diagnostics.Select(d => d.ToString())));
            }
        }
    }
}
