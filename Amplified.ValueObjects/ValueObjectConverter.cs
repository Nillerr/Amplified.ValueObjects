using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Amplified.ValueObjects.Reflection;

namespace Amplified.ValueObjects
{
    public sealed class ValueObjectConverter : TypeConverter
    {
        public ValueObjectConverter(Type type)
        {
            throw new NotImplementedException();
        }
    }
    
    public sealed class ValueObjectConverter<TValueObject, TValue> : TypeConverter
        where TValueObject : IValueObject<TValue>
    {
        public ValueObjectConverter()
        {
            ValueType = typeof(TValue);
            ValueConverter = TypeDescriptor.GetConverter(ValueType);
            ValueObjectType = typeof(TValueObject);
        }

        /// <summary>The value wrapped by the value object.</summary>
        public Type ValueType { get; }
        
        /// <summary>Type of the value object.</summary>
        public Type ValueObjectType { get; }
        
        /// <summary>Converter for <see cref="ValueType"/>.</summary>
        public TypeConverter ValueConverter { get; }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == ValueType)
                return true;
            
            if (ValueType.GetTypeInfo().IsAssignableFrom(sourceType))
                return true;

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter != null && sourceConverter.CanConvertTo(context, ValueType))
                return true;
            
            if (ValueConverter != null)
                return ValueConverter.CanConvertFrom(context, sourceType);

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var argument = ConvertToArgument(context, culture, value);
            var valueObject = CreateValueObject(argument);
            return valueObject;
        }

        private object ConvertToArgument(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sourceType = value.GetType();
            if (ValueType == sourceType)
                return value;
            
            if (ValueType.GetTypeInfo().IsAssignableFrom(sourceType))
                return value;

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter != null && sourceConverter.CanConvertTo(context, ValueType))
                return sourceConverter.ConvertTo(context, culture, value, ValueType);
            
            if (ValueConverter != null)
                return ValueConverter.ConvertFrom(context, culture, value);

            return base.ConvertFrom(context, culture, value);
        }

        private object CreateValueObject(object argument)
        {
            var typeInfo = ValueObjectType.GetValueObjectTypeInfo();
            var valueObject = typeInfo.Create(argument);
            return valueObject;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (ValueType == destinationType)
                return true;

            if (destinationType.GetTypeInfo().IsAssignableFrom(ValueType))
                return true;

            if (ValueConverter != null)
                return ValueConverter.CanConvertTo(context, destinationType);

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var valueObject = (TValueObject) value;
            if (ValueType == destinationType)
                return valueObject.Value;

            if (destinationType.GetTypeInfo().IsAssignableFrom(ValueType))
                return valueObject.Value;
            
            if (ValueConverter != null)
                return ValueConverter.ConvertTo(context, culture, valueObject.Value, destinationType);

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}