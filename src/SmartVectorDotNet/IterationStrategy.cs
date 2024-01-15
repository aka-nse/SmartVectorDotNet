namespace SmartVectorDotNet;

/// <summary>
/// Provides abstraction of iteration strategy.
/// </summary>
public interface IIterationStrategy
{
    /// <summary>
    /// Does for loop.
    /// </summary>
    /// <param name="fromInclusive"></param>
    /// <param name="toExclusive"></param>
    /// <param name="body"></param>
    void For(int fromInclusive, int toExclusive, Action<int> body);

    /// <summary>
    /// Does foreach loop.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="body"></param>
    void ForEach<T>(IEnumerable<T> source, Action<T> body);
}


/// <summary>
/// Provides abstraction of iteration strategy.
/// </summary>
public static class IterationStrategy
{
    /// <summary>
    /// Gets a default iteration strategy instance.
    /// </summary>
    public static DefaultStrategy Default { get; } = new DefaultStrategy();

    /// <summary>
    /// Determines the specified strategy is a default or not.
    /// </summary>
    /// <typeparam name="TStrategy"></typeparam>
    /// <returns></returns>
    public static bool IsDefault<TStrategy>()
        where TStrategy : struct, IIterationStrategy
        => typeof(TStrategy) == typeof(DefaultStrategy);
}

/// <summary>
/// The implementation of <see cref="IIterationStrategy"/> which uses simple loop.
/// </summary>
public readonly struct DefaultStrategy : IIterationStrategy
{
    /// <inheritdoc />
    public void For(int fromInclusive, int toExclusive, Action<int> body)
    {
        for (var i = fromInclusive; i < toExclusive; ++i) body(i);
    }

    /// <inheritdoc />
    public void ForEach<T>(IEnumerable<T> source, Action<T> body)
    {
        foreach (var item in source) body(item);
    }
}