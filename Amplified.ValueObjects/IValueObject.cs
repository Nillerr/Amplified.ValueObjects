namespace Amplified.ValueObjects
{
    /// <summary>
    ///   <para>
    ///     Value objects are simple structures that wraps a single value. They are equatable to instances of the same 
    ///     type, has a ToString() method that delegates to the wrapped value, and has exactly one single argument 
    ///     constructor with accepting a parameter of <typeparamref name="T"/>
    ///   </para>
    /// </summary>
    /// <typeparam name="T">Type of value wrapped by this value object.</typeparam>
    public interface IValueObject<out T>
    {
        /// <summary>
        ///   <para>The value wrapped in the value object.</para>
        /// </summary>
        T Value { get; }
    }
}