using Amplified.ValueObjects.Newtonsoft.Json;
using Newtonsoft.Json;
using Xunit;

namespace Amplified.ValueObjects.Tests.Newtonsoft.Json
{
    public sealed class JsonConverter
    {
        private readonly JsonSerializerSettings _settings;
        
        public JsonConverter()
        {
            _settings = new JsonSerializerSettings();
            _settings.Converters.Add(new ValueObjectJsonConverter());
        }
        
        [Fact]
        public void ConvertIntValue()
        {
            const int value = 59;
            var source = new IntValueObject(value);
            var json = JsonConvert.SerializeObject(source, _settings);
            Assert.Equal("59", json);
        }
        
        [Fact]
        public void ConvertIntValueAsProperty()
        {
            const int value = 59231;
            var source = new IntValueObject(value);
            var json = JsonConvert.SerializeObject(new { Prop = source }, _settings);
            Assert.Equal(@"{""Prop"":59231}", json);
        }
        
        [Fact]
        public void ConvertStringValue()
        {
            const string value = "fooBar";
            var source = new StringValueObject(value);
            var json = JsonConvert.SerializeObject(source, _settings);
            Assert.Equal(@"""fooBar""", json);
        }
        
        [Fact]
        public void ConvertStringValueAsProperty()
        {
            const string value = "FizzBuzz";
            var source = new StringValueObject(value);
            var json = JsonConvert.SerializeObject(new { Prop = source }, _settings);
            Assert.Equal(@"{""Prop"":""FizzBuzz""}", json);
        }
    }
}