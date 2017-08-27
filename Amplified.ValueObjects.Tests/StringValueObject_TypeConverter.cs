using System.ComponentModel;
using System.Reflection;
using Xunit;

namespace Amplified.ValueObjects.Tests
{
    // ReSharper disable once InconsistentNaming
    public sealed class StringValueObject_TypeConverter
    {
        [Fact]
        public void GetConverterReturnsNonNull()
        {
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            Assert.NotNull(converter);
        }
        
        [Fact]
        public void SameTypeReturnsSameInstance()
        {
            var converter1 = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var converter2 = TypeDescriptor.GetConverter(typeof(StringValueObject));
            Assert.Same(converter1, converter2);
        }
        
        [Fact]
        public void GetConverterReturnsValueObjectConverter()
        {
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            Assert.IsType<ValueObjectConverter<StringValueObject, string>>(converter);
        }

        [Fact]
        public void CanConvertFromInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = converter.CanConvertFrom(typeof(int));
            Assert.True(result);
        }

        [Fact]
        public void CanConvertFromShort()
        {
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = converter.CanConvertFrom(typeof(short));
            Assert.True(result);
        }

        [Fact]
        public void CanConvertFromString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = converter.CanConvertFrom(typeof(string));
            Assert.True(result);
        }

        [Fact]
        public void ConvertFromInt()
        {
            const int source = 25;
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = (StringValueObject) converter.ConvertFrom(source);
            var parsed = int.Parse((string) result);
            Assert.Equal(parsed, source);
        }

        [Fact]
        public void ConvertFromShort()
        {
            const short source = 25;
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = (StringValueObject) converter.ConvertFrom(source);
            var parsed = short.Parse((string) result);
            Assert.Equal(parsed, source);
        }

        [Fact]
        public void ConvertFromString()
        {
            const string source = "25";
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = (StringValueObject) converter.ConvertFrom(source);
            Assert.Equal((string) result, source);
        }

        [Fact]
        public void CanConvertToString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = converter.CanConvertTo(typeof(string));
            Assert.True(result);
        }

        [Fact]
        public void ConvertToString()
        {
            const string expected = "Hello, Tests!";
            var source = new StringValueObject(expected);
            var converter = TypeDescriptor.GetConverter(typeof(StringValueObject));
            var result = converter.ConvertTo(source, typeof(string));
            Assert.Equal((string) result, expected);
        }
    }
}