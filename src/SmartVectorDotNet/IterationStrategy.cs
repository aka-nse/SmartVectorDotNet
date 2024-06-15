namespace SmartVectorDotNet;


/// <summary>
/// Loop body part for <see cref="IIterationStrategy"/>.
/// This type is intended to apply generic value type optimization.
/// This type should be implemented on struct.
/// </summary>
public interface IIterationBody
{
    /// <summary>
    /// On each iteration, this method will be called.
    /// </summary>
    /// <returns>
    /// <c>true</c> if should be continued; otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Even if returns <c>false</c>, the operation may not be aborted according to iteration strategy.
    /// </remarks>
    public bool Invoke(int i);
}

/// <summary>
/// Loop body part for <see cref="IIterationStrategy"/>.
/// This type is intended to apply generic value type optimization.
/// This type should be implemented on struct.
/// </summary>
public interface IIterationBody<T>
{
    /// <summary>
    /// On each iteration, this method will be called.
    /// </summary>
    /// <returns>
    /// <c>true</c> if should be continued; otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Even if returns <c>false</c>, the operation may not be aborted according to iteration strategy.
    /// </remarks>
    public bool Invoke(T item);
}


/// <summary>
/// Provides abstraction of iteration strategy.
/// </summary>
public interface IIterationStrategy
{
    /// <summary>
    /// Does for loop.
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="fromInclusive"></param>
    /// <param name="toExclusive"></param>
    /// <param name="body"></param>
    void For<TBody>(int fromInclusive, int toExclusive, TBody body)
        where TBody : struct, IIterationBody;

    /// <summary>
    /// Does foreach loop.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="source"></param>
    /// <param name="body"></param>
    void ForEach<T, TBody>(IEnumerable<T> source, TBody body)
        where TBody : struct, IIterationBody<T>;
}


/// <summary>
/// Provides abstraction of iteration strategy.
/// </summary>
public static class IterationStrategy
{
    private readonly struct DelegateBody : IIterationBody
    {
        private readonly Func<int, bool> _Invoke;

        public DelegateBody(Func<int, bool> invoke) => _Invoke = invoke;

        public bool Invoke(int i) => _Invoke(i);
    }

    private readonly struct DelegateBody<T> : IIterationBody<T>
    {
        private readonly Func<T, bool> _Invoke;

        public DelegateBody(Func<T, bool> invoke) => _Invoke = invoke;

        public bool Invoke(T i) => _Invoke(i);
    }

    /// <summary>
    /// Gets a default iteration strategy instance.
    /// </summary>
    public static SimpleIterationStrategy Default { get; } = new SimpleIterationStrategy();

    /// <summary>
    /// Determines the specified strategy is a default or not.
    /// </summary>
    /// <typeparam name="TStrategy"></typeparam>
    /// <returns></returns>
    public static bool IsDefault<TStrategy>()
        where TStrategy : struct, IIterationStrategy
        => typeof(TStrategy) == typeof(SimpleIterationStrategy);

    /// <summary>
    /// Does for loop.
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="fromInclusive"></param>
    /// <param name="toExclusive"></param>
    /// <param name="body"></param>
    public static void For(this IIterationStrategy strategy, int fromInclusive, int toExclusive, Func<int, bool> body)
        => strategy.For(fromInclusive, toExclusive, new DelegateBody(body));

    /// <summary>
    /// Does foreach loop.
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="source"></param>
    /// <param name="body"></param>
    public static void ForEach<T>(this IIterationStrategy strategy, IEnumerable<T> source, Func<T, bool> body)
        => strategy.ForEach(source, new DelegateBody<T>(body));
}

/// <summary>
/// The implementation of <see cref="IIterationStrategy"/> which uses sequential loop.
/// </summary>
public readonly struct SimpleIterationStrategy : IIterationStrategy
{
    /// <inheritdoc />
    public void For<TBody>(int fromInclusive, int toExclusive, TBody body)
        where TBody : struct, IIterationBody
    {
        for (var i = fromInclusive; i < toExclusive; ++i)
        {
            body.Invoke(i);
        }
    }

    /// <inheritdoc />
    public void ForEach<T, TBody>(IEnumerable<T> source, TBody body)
        where TBody : struct, IIterationBody<T>
    {
        foreach (var item in source)
        {
            body.Invoke(item);
        }
    }
}


/// <summary>
/// The implementation of <see cref="IIterationStrategy"/> which uses sequential loop which can be aborted.
/// </summary>
public readonly struct AbortableIterationStrategy : IIterationStrategy
{
    /// <inheritdoc />
    public void For<TBody>(int fromInclusive, int toExclusive, TBody body)
        where TBody : struct, IIterationBody
    {
        for (var i = fromInclusive; i < toExclusive; ++i)
        {
            if(!body.Invoke(i))
            {
                break;
            }
        }
    }

    /// <inheritdoc />
    public void ForEach<T, TBody>(IEnumerable<T> source, TBody body)
        where TBody : struct, IIterationBody<T>
    {
        foreach (var item in source)
        {
            if(body.Invoke(item))
            {
                break;
            }
        }
    }
}
