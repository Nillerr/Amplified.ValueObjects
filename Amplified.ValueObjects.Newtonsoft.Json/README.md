# Amplified.ValueObjects.Newtonsoft.Json

Json serialization of _[ValueObjects](https://martinfowler.com/bliki/ValueObject.html)_ using `Newtonsoft.Json`.

## Installation

The library is available on [NuGet](https://www.nuget.org/packages/Amplified.ValueObjects.Newtonsoft.Json).

```
Install-Package Amplified.ValueObjects.Newtonsoft.Json
```

### See also

 - [Amplified.ValueObjects](https://www.github.com/nillerr/ValueObjects)

## Usage

Add `ValueObjectJsonConverter` to the serializer settings. 

__Using `JsonConvert`__

```c#
JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
{
    Converters = {
        new ValueObjectJsonConverter()
    }
}
```

__Using a custom `JsonSerializer`__
```c#
var serializer = JsonSerializer.Create(new JsonSerializerSettings()
{
    Converters =
    {
        new ValueObjectJsonConverter()
    }
}); 
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