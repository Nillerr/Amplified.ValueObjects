using System;
using System.ComponentModel;

namespace Amplified.ValueObjects.Tests
{
    [TypeConverter(typeof(ValueObjectConverter<StringValueObject, string>))]
    public struct StringValueObject : IValueObject<string>, IEquatable<StringValueObject>
    {
        private readonly string _value;

        public StringValueObject(string value)
        {
            _value = value;
        }

        string IValueObject<string>.Value => _value;

        public bool Equals(StringValueObject other) => string.Equals(_value, other._value);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StringValueObject && Equals((StringValueObject) obj);
        }

        public override int GetHashCode() => _value != null ? _value.GetHashCode() : 0;

        public override string ToString() => _value;

        public static bool operator ==(StringValueObject left, StringValueObject right) => left.Equals(right);
        public static bool operator !=(StringValueObject left, StringValueObject right) => !left.Equals(right);

        public static explicit operator string(StringValueObject obj) => obj._value;
        public static explicit operator string(StringValueObject? obj) => obj?._value;
    }
}