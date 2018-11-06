using SD.Parser.Excuter.Excuter;
using SD.Parser.Util;
using SD.Parser.Util.Interface;

namespace SD.Parser.Excuter
{
    public static class Config
    {
        public static string TemplateCodePath;

        public static void Init(string codePath)
        {
            TemplateCodePath = codePath;
            IExcuter func() => new CSharpExcuter();
            UtilContainer.UseExcuter(func);
            UtilContainer.Use<IAssemblyLoader>(new AssemblyLoader());
        }
    }
}
