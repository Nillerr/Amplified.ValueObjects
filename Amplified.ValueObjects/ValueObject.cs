using System;
using System.Collections.Generic;

namespace Amplified.ValueObjects
{
    public abstract class ValueObject<TValue, TValueObject> : IValueObject<TValue>, IEquatable<ValueObject<TValue, TValueObject>>
        where TValueObject : ValueObject<TValue, TValueObject>
    {
        protected ValueObject(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; }

        public bool Equals(ValueObject<TValue, TValueObject> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueObject<TValue, TValueObject>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(Value);
        }

        public static bool operator ==(ValueObject<TValue, TValueObject> left, ValueObject<TValue, TValueObject> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<TValue, TValueObject> left, ValueObject<TValue, TValueObject> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
