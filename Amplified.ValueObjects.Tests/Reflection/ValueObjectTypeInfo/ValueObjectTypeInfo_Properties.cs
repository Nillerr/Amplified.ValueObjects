using System;
using Amplified.ValueObjects.Reflection;
using Xunit;

namespace Amplified.ValueObjects.Tests.Reflection
{
    // ReSharper disable once InconsistentNaming
    public sealed class ValueObjectTypeInfo_Properties
    {
        [Fact]
        public void TypeIsValueObjectType()
        {
            var source = typeof(IntValueObject);
            var typeInfo = source.GetValueObjectTypeInfo();
            Assert.Same(source, typeInfo.Type);
        }
        
        [Fact]
        public void InterfaceTypeIsToInterfaceOfObjectType()
        {
            var expected = typeof(IValueObject<int>);
            var source = typeof(IntValueObject);
            var typeInfo = source.GetValueObjectTypeInfo();
            Assert.Same(expected, typeInfo.InterfaceType);
        }
        
        [Fact]
        public void ValueTypeIsInterfaceOfObjectType()
        {
            var expected = typeof(int);
            var source = typeof(IntValueObject);
            var typeInfo = source.GetValueObjectTypeInfo();
            Assert.Same(expected, typeInfo.ValueType);
        }
        
        [Fact]
        public void CreateReturnsConstructedInstance()
        {
            const string vaue = "FooBar";
            var expected = new StringValueObject(vaue);
            var source = typeof(StringValueObject);
            var typeInfo = source.GetValueObjectTypeInfo();
            var result = (StringValueObject) typeInfo.Create(vaue);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void CreateReturnsNewInstances()
        {
            const string value = "FooBar";
            var source = typeof(StringValueObject);
            var typeInfo = source.GetValueObjectTypeInfo();
            
            var result1 = (StringValueObject) typeInfo.Create(value);
            var result2 = (StringValueObject) typeInfo.Create(value);
            Assert.NotSame(result1, result2);
        }
        
        [Fact]
        public void CreateThrowsExceptionWithInvalidArguments()
        {
            const int invalidArgument = 52342;
            var source = typeof(StringValueObject);
            var typeInfo = source.GetValueObjectTypeInfo();
            Assert.Throws<InvalidCastException>(() => typeInfo.Create(invalidArgument));
        }

        [Fact]
        public void GetValueReturnsValue()
        {
            const int expectedValue = 2535;
            var source = new IntValueObject(expectedValue);
            var typeInfo = source.GetType().GetValueObjectTypeInfo();
            var result = typeInfo.GetValue(source);
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void GetValueOnInvalidInstanceThrowsException()
        {
            var source = new StringValueObject("FooBar");
            var typeInfo = typeof(IntValueObject).GetValueObjectTypeInfo();
            Assert.Throws<InvalidCastException>(() => typeInfo.GetValue(source));
        }
    }
}