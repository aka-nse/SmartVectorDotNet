using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SmartVectorDotNet;
using CAEAttribute = CallerArgumentExpressionAttribute;

/// <summary>
/// Provides a set of methods to guard against invalid arguments, states, and conditions.
/// </summary>
/// <remarks>
/// This class is set to public to support generators, and it is not recommended to use it directly from user code.
/// </remarks>
public static class Guard
{
    private const MethodImplOptions _inlining = MethodImplOptions.AggressiveInlining;

    #region NotNull

    /// <summary> Ensures that the specified value is not null. </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="valueShouldBeNotNull"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static T NotNull<T>([NotNull] T? valueShouldBeNotNull, [CAE(nameof(valueShouldBeNotNull))] string parameterName = null!)
    {
        if (valueShouldBeNotNull is null)
        {
            throw new ArgumentNullException(parameterName);
        }
        return valueShouldBeNotNull;
    }

    /// <inheritdoc cref="NotNull{T}(T, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotNull<T>([NotNull] T? valueShouldBeNotNull, [CAE(nameof(valueShouldBeNotNull))] string parameterName = null!) =>
        NotNull(valueShouldBeNotNull, parameterName);

    #endregion NotNull


    #region NotNullNorEmpty

    /// <summary> Ensures that the specified string value is not null or empty. </summary>
    /// <param name="valueShouldBeNotNullNorEmpty"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static string NotNullNorEmpty(string? valueShouldBeNotNullNorEmpty, [CAE(nameof(valueShouldBeNotNullNorEmpty))] string parameterName = null!)
    {
        if (string.IsNullOrEmpty(valueShouldBeNotNullNorEmpty))
        {
            throw new ArgumentException("Value cannot be null or empty.", parameterName);
        }
        return valueShouldBeNotNullNorEmpty!;
    }

    /// <inheritdoc cref="NotNullNorEmpty(string, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotNullNorEmpty([NotNull] string? valueShouldBeNotNullNorEmpty, [CAE(nameof(valueShouldBeNotNullNorEmpty))] string parameterName = null!) =>
        #pragma warning disable IDE0079
        #pragma warning disable CS8777
        NotNullNorEmpty(valueShouldBeNotNullNorEmpty, parameterName);
        #pragma warning restore CS8777
        #pragma warning restore IDE0079

    #endregion NotNullNorEmpty


    #region NotNullNorWhiteSpace

    /// <summary> Ensures that the specified string value is not null or whitespace. </summary>
    /// <param name="valueShouldBeNotNullAndNotWhiteSpace"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static string NotNullNorWhiteSpace([NotNull] string? valueShouldBeNotNullAndNotWhiteSpace, [CAE(nameof(valueShouldBeNotNullAndNotWhiteSpace))] string parameterName = null!)
    {
        if (string.IsNullOrWhiteSpace(valueShouldBeNotNullAndNotWhiteSpace))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", parameterName);
        }
        #pragma warning disable IDE0079
        #pragma warning disable CS8777
        return valueShouldBeNotNullAndNotWhiteSpace!;
        #pragma warning restore CS8777
        #pragma warning restore IDE0079
    }

    /// <inheritdoc cref="NotNullNorWhiteSpace(string, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotNullNorWhiteSpace([NotNull] string? valueShouldBeNotNullAndNotWhiteSpace, [CAE(nameof(valueShouldBeNotNullAndNotWhiteSpace))] string parameterName = null!) =>
        NotNullNorWhiteSpace(valueShouldBeNotNullAndNotWhiteSpace, parameterName);

    #endregion NotNullNorWhiteSpace


    #region NotNegative

    /// <summary> Ensures that the specified value is not negative. </summary>
    /// <param name="valueShouldBeNotNegative"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static int NotNegative(int valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
        return valueShouldBeNotNegative;
    }

    /// <inheritdoc cref="NotNegative(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotNegative(int valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!) =>
        NotNegative(valueShouldBeNotNegative, parameterName);


    /// <inheritdoc cref="NotNegative(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static long NotNegative(long valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
        return valueShouldBeNotNegative;
    }

    /// <inheritdoc cref="NotNegative(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotNegative(long valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!) =>
        NotNegative(valueShouldBeNotNegative, parameterName);


    /// <inheritdoc cref="NotNegative(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static float NotNegative(float valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
        return valueShouldBeNotNegative;
    }

    /// <inheritdoc cref="NotNegative(int, string)" />
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotNegative(float valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!) =>
        NotNegative(valueShouldBeNotNegative, parameterName);


    /// <inheritdoc cref="NotNegative(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static double NotNegative(double valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
        return valueShouldBeNotNegative;
    }

    /// <inheritdoc cref="NotNegative(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotNegative(double valueShouldBeNotNegative, [CAE(nameof(valueShouldBeNotNegative))] string parameterName = null!) =>
        NotNegative(valueShouldBeNotNegative, parameterName);

    #endregion NotNegative


    #region NotZero

    /// <summary> Ensures that the specified value is not zero. </summary>
    /// <param name="valueShouldBeNotZero"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static int NotZero(int valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
        return valueShouldBeNotZero;
    }

    /// <inheritdoc cref="NotZero(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotZero(int valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!) =>
        NotZero(valueShouldBeNotZero, parameterName);


    /// <inheritdoc cref="NotZero(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static long NotZero(long valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
        return valueShouldBeNotZero;
    }

    /// <inheritdoc cref="NotZero(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotZero(long valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!) =>
        NotZero(valueShouldBeNotZero, parameterName);


    /// <inheritdoc cref="NotZero(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static float NotZero(float valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
        return valueShouldBeNotZero;
    }

    /// <inheritdoc cref="NotZero(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotZero(float valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!) =>
        NotZero(valueShouldBeNotZero, parameterName);


    /// <inheritdoc cref="NotZero(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static double NotZero(double valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
        return valueShouldBeNotZero;
    }

    /// <inheritdoc cref="NotZero(int, string)"/>
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugNotZero(double valueShouldBeNotZero, [CAE(nameof(valueShouldBeNotZero))] string parameterName = null!) =>
        NotZero(valueShouldBeNotZero, parameterName);

    #endregion NotZero


    #region ValidRange

    /// <summary> Ensures that the condition is satisfied, or throws <see cref="ArgumentOutOfRangeException"/>. </summary>
    /// <param name="shouldBe"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static void ValidRange(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new ArgumentOutOfRangeException(message);
        }
    }

    /// <inheritdoc cref="ValidRange(bool, string)" />
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugValidRange(bool shouldBe, string message) =>
        ValidRange(shouldBe, message);

    #endregion ValidRange


    #region ValidArgument

    /// <summary> Ensures that the condition is satisfied, or throws <see cref="ArgumentException"/>. </summary>
    /// <param name="shouldBe"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static void ValidArgument(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new ArgumentException(message);
        }
    }

    /// <inheritdoc cref="ValidArgument(bool, string)" />
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugValidArgument(bool shouldBe, string message) =>
        ValidArgument(shouldBe, message);

    #endregion ValidArgument


    #region ValidState

    /// <summary> Ensures that the condition is satisfied, or throws <see cref="InvalidOperationException"/>. </summary>
    /// <param name="shouldBe"></param>
    /// <param name="message"></param>
    /// <exception cref="InvalidOperationException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static void ValidState(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new InvalidOperationException(message);
        }
    }

    /// <inheritdoc cref="ValidState(bool, string)" />
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugValidState(bool shouldBe, string message) =>
        ValidState(shouldBe, message);

    #endregion ValidState


    #region OnlyRealSupported

    /// <summary> Ensures that the specified type is a primitive real number type (float or double). </summary>
    /// <typeparam name="TShouldBeReal"></typeparam>
    /// <exception cref="NotSupportedException"></exception>
    [DebuggerHidden, MethodImpl(_inlining)]
    public static void OnlyRealSupported<TShouldBeReal>()
        where TShouldBeReal : unmanaged
    {
        if(typeof(TShouldBeReal) != typeof(float) && typeof(TShouldBeReal) != typeof(double))
        {
            throw new NotSupportedException();
        }
    }

    /// <inheritdoc cref="OnlyRealSupported{TShouldBeReal}" />
    [DebuggerHidden, MethodImpl(_inlining), Conditional("DEBUG")]
    public static void DebugOnlyRealSupported<TShouldBeReal>()
        where TShouldBeReal : unmanaged
    {
        if (typeof(TShouldBeReal) != typeof(float) && typeof(TShouldBeReal) != typeof(double))
        {
            throw new NotSupportedException();
        }
    }

    #endregion OnlyRealSupported

}
