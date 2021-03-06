﻿using SD.Parser.Analyse;
using SD.Parser.Analyse.Interface;
using SD.Parser.Analyse.Models;
using SD.Parser.ParamParser;
using SD.Parser.PrecompilerFunc;
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
            Use<ILogger>(new DefaultLogger());
            Use<IAnalyse>(new AnalyseCenter());
            Use<ITypeFetch>(new TypeFetch());
            Use<IKeyWordProvider>(new KeyWordProvider());
            Use<IExpressionInfoBuilder>(new ExpressionInfoBuilder());
            Use<ParamParser.Interface.IParamRegular>(new ParamCenter());
            Use(new ExpressionRegular());
            Use(new FuncRegular());
            Use<IPrecompilter>(new DefaultPrecompilter());
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

        public static void UseIfFunc()
        {
            IPrecompilter precompilter = Resolve<IPrecompilter>();
            precompilter.Items.Add(new IFFuncItem());
        }

        public static void UseMath()
        {
            IPrecompilter precompilter = Resolve<IPrecompilter>();
            precompilter.Items.Add(new SumItem());
            precompilter.Items.Add(new AvgItem());
            precompilter.Items.Add(new CountItem());
        }

        public static void UsePrecompilterFunc()
        {
            UseIfFunc();
            UseMath();
        }
    }
}
