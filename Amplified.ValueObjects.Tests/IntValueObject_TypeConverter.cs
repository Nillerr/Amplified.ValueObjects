using System.ComponentModel;
using Xunit;

namespace Amplified.ValueObjects.Tests
{
    // ReSharper disable once InconsistentNaming
    public sealed class IntValueObject_TypeConverter
    {
        [Fact]
        public void GetConverterReturnsNonNull()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            Assert.NotNull(converter);
        }
        
        [Fact]
        public void SameTypeReturnsSameInstance()
        {
            var converter1 = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var converter2 = TypeDescriptor.GetConverter(typeof(IntValueObject));
            Assert.Same(converter1, converter2);
        }
        
        [Fact]
        public void GetConverterReturnsValueObjectConverter()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            Assert.IsType<ValueObjectConverter<IntValueObject, int>>(converter);
        }

        [Fact]
        public void CanConvertFromInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.CanConvertFrom(typeof(int));
            Assert.True(result);
        }

        [Fact]
        public void CanConvertFromShort()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.CanConvertFrom(typeof(short));
            Assert.True(result);
        }

        [Fact]
        public void CanConvertFromString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.CanConvertFrom(typeof(string));
            Assert.True(result);
        }

        [Fact]
        public void ConvertFromInt()
        {
            const int source = 25;
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = (IntValueObject) converter.ConvertFrom(source);
            Assert.Equal((int) result, source);
        }

        [Fact]
        public void ConvertFromShort()
        {
            const short source = 25;
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = (IntValueObject) converter.ConvertFrom(source);
            Assert.Equal((short) result, source);
        }

        [Fact]
        public void ConvertFromString()
        {
            const string source = "25";
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = (IntValueObject) converter.ConvertFrom(source);
            Assert.Equal($"{(int) result}", source);
        }

        [Fact]
        public void CanConvertToInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.CanConvertTo(typeof(int));
            Assert.True(result);
        }

        [Fact]
        public void CanConvertToLong()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.CanConvertTo(typeof(long));
            Assert.True(result);
        }

        [Fact]
        public void CanConvertToString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.CanConvertTo(typeof(string));
            Assert.True(result);
        }

        [Fact]
        public void ConvertToInt()
        {
            var source = new IntValueObject(96);
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.ConvertTo(source, typeof(int));
            Assert.Equal((int) source, result);
        }

        [Fact]
        public void ConvertToLong()
        {
            var source = new IntValueObject(96);
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.ConvertTo(source, typeof(long));
            Assert.Equal((long) source, result);
        }

        [Fact]
        public void ConvertToString()
        {
            var source = new IntValueObject(96);
            var converter = TypeDescriptor.GetConverter(typeof(IntValueObject));
            var result = converter.ConvertTo(source, typeof(string));
            Assert.Equal($"{(int) source}", result);
        }
    }
}