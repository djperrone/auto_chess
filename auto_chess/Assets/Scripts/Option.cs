
public static class Option
{
    public static Option<T> Some<T>(T value) => new Option<T>(value, true);

    public static Option<T> None<T>() => new Option<T>(default(T), false);

    public static Option<T> FromNullable<T>(T value) where T : class
        => value is null
            ? None<T>()
            : Some(value);

    public static Option<T> FromNullable<T>(T? value) where T : struct
        => value.HasValue
            ? Some(value.Value)
            : None<T>();
}

public struct Option<T>
{
    private readonly bool m_HasValue;
    private readonly T m_Value;

    public bool HasValue => m_HasValue;

    internal Option(T value, bool hasValue)
    {
        m_Value = value;
        m_HasValue = hasValue;
    }

    public bool TryGetValue(out T value)
    {
        value = m_HasValue ? m_Value : default;
        return m_HasValue;
    }

    public T GetValueUnchecked()
    {
        return m_Value;
    }
}