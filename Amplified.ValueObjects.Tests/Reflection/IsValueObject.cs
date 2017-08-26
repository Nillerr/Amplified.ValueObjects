using Amplified.ValueObjects.Reflection;
using Xunit;

namespace Amplified.ValueObjects.Tests.Reflection
{
    public sealed class IsValueObject
    {
        [Fact]
        public void ReturnsTrueForValueObjects()
        {
            var source = typeof(IntValueObject);
            var result = source.IsValueObject();
            Assert.True(result);
        }
        
        [Fact]
        public void ReturnsFalseForNonValueObjects()
        {
            var source = typeof(int);
            var result = source.IsValueObject();
            Assert.False(result);
        }
    }
}