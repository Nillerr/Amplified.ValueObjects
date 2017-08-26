using System;
using System.ComponentModel;

namespace Amplified.ValueObjects.Tests
{
    [TypeConverter(typeof(ValueObjectConverter<IntValueObject, int>))]
    public struct IntValueObject : IValueObject<int>, IEquatable<IntValueObject>
    {
        private readonly int _value;
       
        public IntValueObject(int value)
        {
            _value = value;
        }

        int IValueObject<int>.Value => _value;

        public bool Equals(IntValueObject other) => _value == other._value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is IntValueObject && Equals((IntValueObject) obj);
        }

        public override int GetHashCode() => _value;

        public override string ToString() => _value.ToString();

        public static bool operator ==(IntValueObject left, IntValueObject right) => left.Equals(right);
        public static bool operator !=(IntValueObject left, IntValueObject right) => !left.Equals(right);

        public static explicit operator int(IntValueObject source) => source._value;
        public static explicit operator int?(IntValueObject? source) => source?._value;
    }
}