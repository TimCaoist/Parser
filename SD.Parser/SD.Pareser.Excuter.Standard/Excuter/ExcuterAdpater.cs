using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;
using Z.Expressions;

namespace SD.Parser.Excuter.Standard.Excuter
{
    public class ExcuterAdpater : IExcuter
    {
        private readonly EvalContext context;

        private readonly static Dictionary<int, Func<object, object>> dictionary = new Dictionary<int, Func<object, object>>();

        public ExcuterAdpater()
        {
            context = new EvalContext();
        }

        public TResult Execute<TResult>(string expression, object paramDatas)
        {
            if (paramDatas == null)
            {
                return context.Execute<TResult>(expression);
            }
        

            return context.Execute<TResult>(expression, paramDatas);
        }

        public void RegisterGlobalVariable(string name, object val)
        {
            context.RegisterGlobalVariable(name, val);
        }

        public void RegisterStaticMember(params Type[] type)
        {
            context.RegisterStaticMember(type);
        }

        public void RegisterType(params Type[] type)
        {
            context.RegisterType(type);
        }
    }
}
