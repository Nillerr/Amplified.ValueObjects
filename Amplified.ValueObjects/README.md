# Amplified.ValueObjects

Provides types and reflection information for implementing and working with single-value _[ValueObjects](https://martinfowler.com/bliki/ValueObject.html)_.

## Installation

The library is available on [NuGet](https://www.nuget.org/packages/Amplified.CSharp).

```
Install-Package Amplified.ValueObjects
```

### See also

 - [Amplified.ValueObjects.Newtonsoft.Json](https://www.github.com/nillerr/Amplified.ValueObjects.Newtonsoft.Json)

## Usage

__ValueObject Type definition__

Value objects implement `IValueObject<T>` to signal that the specified object is a ValueObject.

```c#
public struct UserId : IValueObject<Guid>
{
    private readonly Guid _value;
    
    public UserId(Guid value)
    {
        _value = value;
    }
    
    Guid IValueObject<Guid>.Value => _value;
}
```

A aalue object should also implement `IEquatable<TValueObject>` to adhere to correct value object semantics.

```c#
public struct UserId : IValueObject<Guid>, IEquatable<UserId> 
{
    Guid IValueObject<Guid>.Value { get; }
    
    bool Equals(object other);
    bool Equals(UserId other);
    
    bool operator ==(UserId left, UserId right);
    bool operator !=(UserId left, UserId right);
}
```

Value objects may implement `IComparable<TValueObject>` when it makes sense. An example of this is the value object `PageNumber`.

```c#
public struct PageNumber : IValueObject<int>, IEquatable<PageNumber>, IComparable<PageNumber>
{
    int IValueObject<int>.Value { get; }
    
    bool Equals(object other);
    bool Equals(PageNumber other);
    
    bool operator ==(PageNumber left, PageNumber right);
    bool operator !=(PageNumber left, PageNumber right);
    
    int CompareTo(PageNumber other);
    
    int operator >(PageNumber left, PageNumber right);
    int operator <(PageNumber left, PageNumber right);
}
```

We recommend hiding the value behind the explicit implementation of `IValueObject<T>`, and exposing it using explicit type conversion operators.

```c#
public struct UserId : IValueObject<Guid>
{
    Guid IValueObject<Guid>.Value { get; }
    
    explicit operator Guid(UserId value) => value.Value;
    explicit operator Guid?(UserId? value) => value?.Value;
}
```

### Reflection

Value object types supports a set of additional extension method:
```c#
public static bool IsValueObject(this Type type);
public static ValueObjectTypeInfo GetValueObjectTypeInfo(this Type type);
```

Calling `IsValueObject` on any type is allowed, while calling `GetValueObjectTypeInfo` on non-ValueObject types, will result in an exception being thrown. You should always call `IsValueObject` before `GetValueObjectTypeInfo`, if you do not for sure that the type represents a ValueObject. The type information returned by either of these is cached, so the overhead is neglible. 

#### ValueObjectTypeInfo

This object provides type information and useful methods associated with value objects.

__Example:__ _Creating a new instance of a value object type_
```c#
public TValueObject CreateValueObject<TValueObject, T>(T argument) where TValueObject : IValueObject<T>
{
    var typeInfo = typeof(TValueObject).GetValueObjectTypeInfo();
    var valueObject = typeInfo.Create(argument);
    return (TValueObject) valueObject;
}
```

__Example:__ _Accessing the value of value object_
```c#
public object GetValueObjectValue(object instance)
{
    var typeInfo = instance.GetType().GetValueObjectTypeInfo();
    var value = typeInfo.GetValue(instance);
    return (T) value;
}
```

## License

MIT License

Copyright (c) 2017 Nicklas Jensen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.