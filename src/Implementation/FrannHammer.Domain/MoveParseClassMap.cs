using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    /// <summary>
    /// This is used to register and then return the registered <see cref="IMove"/>
    /// implementation when desired.  Mapping types to this static construct allows the api
    /// to avoid passed around the DI container in order to resolve types.
    /// 
    /// This is a much more lightweight solution than starting the pattern of passing 
    /// around the all-important DI container.
    /// </summary>
    public static class MoveParseClassMap
    {
        private static readonly IDictionary<string, Type> RegisteredTypes;

        static MoveParseClassMap()
        {
            RegisteredTypes = new Dictionary<string, Type>();
        }

        public static void RegisterType<TMoveContract, TMoveImplementation>()
            where TMoveContract : class, IMove
            where TMoveImplementation : class, IMove
        {
            string typeName = typeof(TMoveContract).Name;

            //just ignore the registration if they are attempting to register the same type to the same contract more than once.
            if (RegisteredTypes.ContainsKey(typeName) &&
                RegisteredTypes[typeName] != typeof(TMoveImplementation))
            { throw new InvalidOperationException($"Type '{typeName}' is already registered."); }

            RegisteredTypes[typeof(TMoveContract).Name] = typeof(TMoveImplementation);
        }

        public static Type GetRegisteredImplementationTypeFor<T>()
            where T : IMove
        {
            string typeName = typeof(T).Name;
            if (!RegisteredTypes.ContainsKey(typeName))
            { throw new KeyNotFoundException($"{typeName} has nothing registered!"); }

            return RegisteredTypes[typeName];
        }

        public static void ClearAllRegisteredTypes() => RegisteredTypes.Clear();
    }
}
