using SD.Parser.Analyse;
using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using SD.Parser.ParamParser;
using SD.Parser.Util.Interface;
using System;
using System.Collections.Generic;

namespace SD.Parser.Util
{
    public static class UtilContainer
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        static UtilContainer()
        {
            Use<IAnalyse>(new AnalyseCenter());
            Use<ITypeFetch>(new TypeFetch());
            Use<IKeyWordProvider>(new KeyWordProvider());
            Use<IExpressionInfoBuilder>(new ExpressionInfoBuilder());
            Use<ParamParser.Interface.IParamRegular>(new ParamCenter());
            Use(new ExpressionRegular());
        }

        public static void UseExcuter(Func<IExcuter> create)
        {
            Use<IExcuterCreator>(new ExcuterBuilder(create));
        }

        public static void Use<TInterface>(object instance) where TInterface : class
        {
            var tInterface = typeof(TInterface);
            TInterface service = Resolve<TInterface>();
            if (service != null)
            {
                services[tInterface] = instance;
            }
            else {
                services.Add(tInterface, instance);
            }
        }

        public static void Use(object instance)
        {
            var tInterface = instance.GetType();
            var obj = Resolve(instance.GetType());
            if (obj != null)
            {
                services[tInterface] = instance;
            }
            else
            {
                services.Add(tInterface, instance);
            }
        }


        public static TInterface Resolve<TInterface>() where TInterface : class
        {
            var tInterface = typeof(TInterface);
            return Resolve(tInterface) as TInterface;
        }

        public static object Resolve(Type type)
        {
            object instance;
            if (services.TryGetValue(type, out instance))
            {
                return instance;
            }

            return null;
        }
    }
}
