using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Amplified.ValueObjects.Reflection
{
    public static class ValueObjectReflectionExtensions
    {
        private static readonly ConcurrentDictionary<Type, ValueObjectTypeInfo> TypeInfoCache 
            = new ConcurrentDictionary<Type, ValueObjectTypeInfo>();

        /// <summary>
        ///   <para>Checks if the current type is an implementation of <see cref="IValueObject{T}"/>.</para>
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>
        ///   <see langword="true"/> if <paramref name="type"/> is an implementation of <see cref="IValueObject{T}"/>; 
        ///   <see langword="false"/> otherwise.
        /// </returns>
        public static bool IsValueObject(this Type type)
            => TypeInfoCache.GetOrAdd(type, ResolveTypeInfo) != null;
        
        /// <summary>
        ///   <para>Returns additional metadata associated with this <see cref="IValueObject{T}"/> implementation.</para>
        /// </summary>
        /// <param name="type">The type to find additional metadata for.</param>
        /// <returns>The metadata associated with this <see cref="IValueObject{T}"/> implementation.</returns>
        /// <exception cref="ArgumentException"><paramref name="type"/> is not an implementation of <see cref="IValueObject{T}"/>.</exception>
        public static ValueObjectTypeInfo GetValueObjectTypeInfo(this Type type)
        {
            var typeInfo = TypeInfoCache.GetOrAdd(type, ResolveTypeInfo);
            if (typeInfo == null)
                throw new ArgumentException("The type " + type.FullName + " is not an instance of " + typeof(IValueObject<>).FullName, nameof(type));

            return typeInfo;
        }

        private static ValueObjectTypeInfo ResolveTypeInfo(Type type)
        {
            var interfaceType = GetValueObjectInterfaceType(type);
            if (interfaceType == null)
                return null;

            var valueType = interfaceType.GenericTypeArguments[0];
            return new ValueObjectTypeInfo(type, valueType, interfaceType);
        }

        private static Type GetValueObjectInterfaceType(Type type)
        {
            try
            {
                var valueObjectInterface = type.GetTypeInfo()
                    .GetInterfaces()
                    .Where(it => it.IsConstructedGenericType)
                    .SingleOrDefault(it => it.GetGenericTypeDefinition() == typeof(IValueObject<>));
                
                return valueObjectInterface;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("The type " + type.FullName  + " implements the generic interface " + typeof(IValueObject<>).FullName + " multiple times.", nameof(type));
            }
        }
    }
}