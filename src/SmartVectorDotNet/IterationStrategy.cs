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
    /// Even if returns <c>false</c>, the operation may not be breaked according to iteration strategy.
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
    /// Even if returns <c>false</c>, the operation may not be breaked according to iteration strategy.
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
    private readonly struct ActionBody(Action<int> invoke) : IIterationBody
    {
        public bool Invoke(int i)
        {
            invoke(i);
            return true;
        }
    }

    private readonly struct ActionBody<T>(Action<T> invoke) : IIterationBody<T>
    {
        public bool Invoke(T item)
        {
            invoke(item);
            return true;
        }
    }

    private readonly struct FuncBody(Func<int, bool> invoke) : IIterationBody
    {
        public bool Invoke(int i) => invoke(i);
    }

    private readonly struct FuncBody<T>(Func<T, bool> invoke) : IIterationBody<T>
    {
        public bool Invoke(T item) => invoke(item);
    }

    private class DefaultIterationStrategy : IIterationStrategy
    {
        /// <inheritdoc />
        public void For<TBody>(int fromInclusive, int toExclusive, TBody body)
            where TBody : struct, IIterationBody
        {
            for (var i = fromInclusive; i < toExclusive; ++i)
            {
                if (!body.Invoke(i))
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
                if (body.Invoke(item))
                {
                    break;
                }
            }
        }
    }

    private class ParallelIterationStrategy(ParallelOptions options) : IIterationStrategy
    {
        public void For<TBody>(int fromInclusive, int toExclusive, TBody body)
            where TBody : struct, IIterationBody
        {
            Parallel.For(fromInclusive, toExclusive, options, (i, state) =>
            {
                if (!body.Invoke(i))
                {
                    state.Break();
                }
            });
        }

        public void ForEach<T, TBody>(IEnumerable<T> source, TBody body)
            where TBody : struct, IIterationBody<T>
        {
            Parallel.ForEach(source, options, (item, state) =>
            {
                if (!body.Invoke(item))
                {
                    state.Break();
                }
            });
        }
    }


    /// <summary>
    /// Gets a default iteration strategy instance which uses sequential loop.
    /// </summary>
    public static IIterationStrategy Default { get; } = new DefaultIterationStrategy();

    /// <summary>
    /// Does for loop.
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="fromInclusive"></param>
    /// <param name="toExclusive"></param>
    /// <param name="body"></param>
    public static void For(this IIterationStrategy strategy, int fromInclusive, int toExclusive, Action<int> body)
        => strategy.For(fromInclusive, toExclusive, new ActionBody(body));

    /// <summary>
    /// Does for loop.
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="fromInclusive"></param>
    /// <param name="toExclusive"></param>
    /// <param name="body"></param>
    public static void For(this IIterationStrategy strategy, int fromInclusive, int toExclusive, Func<int, bool> body)
        => strategy.For(fromInclusive, toExclusive, new FuncBody(body));

    /// <summary>
    /// Does foreach loop.
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="source"></param>
    /// <param name="body"></param>
    public static void ForEach<T>(this IIterationStrategy strategy, IEnumerable<T> source, Action<T> body)
        => strategy.ForEach(source, new ActionBody<T>(body));

    /// <summary>
    /// Does foreach loop.
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="source"></param>
    /// <param name="body"></param>
    public static void ForEach<T>(this IIterationStrategy strategy, IEnumerable<T> source, Func<T, bool> body)
        => strategy.ForEach(source, new FuncBody<T>(body));
}
