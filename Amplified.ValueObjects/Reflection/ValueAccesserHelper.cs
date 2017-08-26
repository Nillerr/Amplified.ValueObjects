using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Amplified.ValueObjects.Reflection
{
    internal static class ValueAccesserHelper
    {
        public static ValueAccesser CreateValueAccesser(Type type, Type valueType, Type interfaceType)
        {
            var interfaceMap = type.GetTypeInfo().GetRuntimeInterfaceMap(interfaceType);
            var valueGetter = interfaceMap.TargetMethods.Single();
            
            // (object instance)
            var parameter = Expression.Parameter(typeof(object), "instance");
            // ((TValueValue) instance)
            var instance = Expression.Convert(parameter, type);
            // ((TValueObject) value).get_Value()
            var accesser = Expression.Call(instance, valueGetter);
            // (object) ((TValueObject) value).get_Value(); 
            var result = Expression.Convert(accesser, typeof(object));
            // (object instance) => (object) ((TValueObject) instance).Value;
            var lambda = Expression.Lambda<ValueAccesser>(result, parameter);
            return lambda.Compile();            
        }
    }
}