using SD.Parser.Util.Interface;
using System;

namespace SD.Parser.Util
{
    public class ExcuterBuilder : IExcuterCreator
    {
        private readonly Func<IExcuter> func;

        public ExcuterBuilder(Func<IExcuter> func)
        {
            this.func = func;
        }

        public IExcuter BuildExcuter()
        {
            return func.Invoke();
        }
    }
}
