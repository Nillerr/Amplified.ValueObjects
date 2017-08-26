using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Amplified.ValueObjects.Reflection
{
    internal static class ConstructorHelper
    {
        public static Constructor CreateConstructor(Type type, Type valueType)
        {
            var constructorInfo = type.GetTypeInfo().GetConstructor(new[] {valueType});
            if (constructorInfo == null)
                throw new ArgumentException("The type does not have a public constructor with a single parameter matching its value type: " + valueType.FullName + ".", nameof(type));

            // (object value)
            var parameter = Expression.Parameter(typeof(object), "value");
            // ((TValue) value)
            var argument = Expression.Convert(parameter, valueType);
            // new TValueObject((TValue) value)
            var constructor = Expression.New(constructorInfo, argument);
            // (object) new TValueObject((TValue) value)
            var result = Expression.Convert(constructor, typeof(object));
            // (object value) => (object) new TValueObject((TValue) value)
            var lambda = Expression.Lambda<Constructor>(result, parameter);
            return lambda.Compile();
        }
    }
}