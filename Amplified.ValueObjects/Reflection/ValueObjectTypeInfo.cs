using System;

namespace Amplified.ValueObjects.Reflection
{
    /// <summary>
    /// Contains type information and utilities associated with a value object type.
    /// </summary>
    public sealed class ValueObjectTypeInfo
    {
        private readonly Constructor _constructor;
        private readonly ValueAccesser _valueAccesser;

        internal ValueObjectTypeInfo(Type type, Type valueType, Type interfaceType)
        {
            Type = type;
            ValueType = valueType;
            InterfaceType = interfaceType;
            _constructor = ConstructorHelper.CreateConstructor(type, valueType);
            _valueAccesser = ValueAccesserHelper.CreateValueAccesser(type, valueType, interfaceType);
        }

        /// <summary>
        /// Type of the value object represented by this type info.
        /// </summary>
        public Type Type { get; }
        
        /// <summary>
        /// Type of the wrapped value in the value object represented by this type info. 
        /// </summary>
        public Type ValueType { get; }
        
        /// <summary>
        /// Generic type of <c>IValueObject&lt;T&gt;</c> implemented by the value object represented by this type info.
        /// </summary>
        public Type InterfaceType { get; }

        /// <summary>
        /// Creates a new instance of the value object represented by this type info.
        /// </summary>
        /// <param name="argument">The value argument passed to the value object constructor.</param>
        /// <returns>The new instance of the value object.</returns>
        /// <exception cref="InvalidCastException"><paramref name="argument"/> is not assignable to <see cref="ValueType"/>.</exception>
        public object Create(object argument) => _constructor(argument);

        /// <summary>
        /// Returns the value wrapped by <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">The value object to retrieve the wrapped value from. This must be an instance of 
        /// the value object type represented by this type info.</param>
        /// <returns>The value wrapped by <paramref name="instance"/>.</returns>
        public object GetValue(object instance) => _valueAccesser(instance);
    }
}