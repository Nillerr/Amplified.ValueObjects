using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Amplified.ValueObjects
{
    public abstract class ValueObjectFactory<TValueObject, TValue>
        where TValueObject : IValueObject<TValue>
    {
        public abstract TValueObject Create();
    }
    
    public static class ValueObjectTypeConverters
    {
        /// <summary>
        ///   <para>
        ///     Adds a <see cref="TypeConverterAttribute"/> for every public implementation of 
        ///     <see cref="IValueObject{T}"/> found in <paramref name="assembly"/> to <see cref="TypeDescriptor"/>.
        ///   </para>
        /// </summary>
        /// <param name="assembly">The assembly to find <see cref="IValueObject{T}"/> implementations in.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">
        ///   One or more types in <paramref name="assembly"/> implements <see cref="IValueObject{T}"/> multiple times.
        /// </exception>
        public static void AddAttributes(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            
            var valueObjectTypes = assembly.ExportedTypes
                .Select(type => new
                {
                    Type = type,
                    ValueObjectInterfaceType = GetValueObjectType(type)
                })
                .Where(it => it.ValueObjectInterfaceType != null);

            var valueObjectConverter = typeof(ValueObjectConverter<,>);
            
            foreach (var valueObjectType in valueObjectTypes)
            {
                var valueType = valueObjectType.ValueObjectInterfaceType.GenericTypeArguments[0];
                var converterType = valueObjectConverter.MakeGenericType(valueObjectType.Type, valueType);
                var attribute = new TypeConverterAttribute(converterType);
                TypeDescriptor.AddAttributes(valueObjectType.Type, attribute);
            }
        }

        private static Type GetValueObjectType(Type type)
        {
            try
            {
                return type.GetTypeInfo()
                    .ImplementedInterfaces
                    .Where(@interface => @interface.IsConstructedGenericType)
                    .SingleOrDefault(@interface => @interface.GetGenericTypeDefinition() == typeof(IValueObject<>));
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The type " + type.FullName + " may not implement more than one type of the generic interface " + typeof(IValueObject<>).FullName + ".");
            }
        }
    }
}