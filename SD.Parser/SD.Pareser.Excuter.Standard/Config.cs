using SD.Parser.Excuter.Standard.Excuter;
using SD.Parser.Util;
using SD.Parser.Util.Interface;

namespace SD.Pareser.Excuter.Standard
{
    public static class Config
    {
        public static void Init()
        {
            IExcuter func() => new ExcuterAdpater();
            UtilContainer.UseExcuter(func);
            UtilContainer.Use<IAssemblyLoader>(new AssemblyLoader());
        }
    }
}
