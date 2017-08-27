using System;
using Amplified.ValueObjects.Reflection;
using Newtonsoft.Json;

namespace Amplified.ValueObjects.Newtonsoft.Json
{
    public sealed class ValueObjectJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueObjectType = value.GetType().GetValueObjectTypeInfo();
            var innerValue = valueObjectType.GetValue(value);
            serializer.Serialize(writer, innerValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var valueObjectType = objectType.GetValueObjectTypeInfo();
            var valueType = valueObjectType.ValueType;
            var argument = serializer.Deserialize(reader, valueType);
            var valueObject = valueObjectType.Create(argument);
            return valueObject;
        }

        public override bool CanConvert(Type objectType) => objectType.IsValueObject();
    }
}