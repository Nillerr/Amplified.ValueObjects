using System;
using System.Reflection;
using Amplified.ValueObjects.Reflection;
using Xunit;

namespace Amplified.ValueObjects.Tests.Reflection
{
    public sealed class GetValueObjectInfo
    {
        [Fact]
        public void ReturnsValueObjectTypeInfoForValueObject()
        {
            var source = typeof(StringValueObject);
            var result = source.GetValueObjectTypeInfo();
            Assert.NotNull(result);
        }

        [Fact]
        public void ThrowsExceptionForNonValueObject()
        {
            var source = typeof(object);
            Assert.Throws<ArgumentException>(() => source.GetValueObjectTypeInfo());
        }

        [Fact]
        public void ReturnsSameInstanceForSameType()
        {
            var source = typeof(StringValueObject);
            var result1 = source.GetValueObjectTypeInfo();
            var result2 = source.GetValueObjectTypeInfo();
            Assert.Same(result1, result2);
        }
    }
}