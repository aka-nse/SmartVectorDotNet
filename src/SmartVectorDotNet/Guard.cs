using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

/// <summary>
/// Provides a set of methods to guard against invalid arguments, states, and conditions.
/// </summary>
/// <remarks>
/// This class is set to public to support generators, and it is not recommended to use it directly from user code.
/// </remarks>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class Guard
{
    /// <summary>
    /// Ensures that the specified value is not null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="valueShouldBeNotNull"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNull<T>(T valueShouldBeNotNull, [CallerArgumentExpression(nameof(valueShouldBeNotNull))] string parameterName = null!)
    {
        if (valueShouldBeNotNull is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    /// <summary>
    /// Ensures that the specified string value is not null or empty.
    /// </summary>
    /// <param name="valueShouldBeNotNullAndNotEmpty"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNullOrEmpty(string valueShouldBeNotNullAndNotEmpty, [CallerArgumentExpression(nameof(valueShouldBeNotNullAndNotEmpty))] string parameterName = null!)
    {
        if (string.IsNullOrEmpty(valueShouldBeNotNullAndNotEmpty))
        {
            throw new ArgumentException("Value cannot be null or empty.", parameterName);
        }
    }

    /// <summary>
    /// Ensures that the specified string value is not null or whitespace.
    /// </summary>
    /// <param name="valueShouldBeNotNullAndNotWhiteSpace"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNullOrWhiteSpace(string valueShouldBeNotNullAndNotWhiteSpace, [CallerArgumentExpression(nameof(valueShouldBeNotNullAndNotWhiteSpace))] string parameterName = null!)
    {
        if (string.IsNullOrWhiteSpace(valueShouldBeNotNullAndNotWhiteSpace))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", parameterName);
        }
    }

    /// <summary>
    /// Ensures that the specified value is not negative.
    /// </summary>
    /// <param name="valueShouldBeNotNegative"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(int valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// Ensures that the specified value is not negative.
    /// </summary>
    /// <param name="valueShouldBeNotNegative"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(long valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// Ensures that the specified value is not negative.
    /// </summary>
    /// <param name="valueShouldBeNotNegative"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(float valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// Ensures that the specified value is not negative.
    /// </summary>
    /// <param name="valueShouldBeNotNegative"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(double valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// Ensures that the specified value is not negative.
    /// </summary>
    /// <param name="valueShouldBeNotNegative"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(decimal valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// Ensures that the specified value is not zero.
    /// </summary>
    /// <param name="valueShouldBeNotZero"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotZero(int valueShouldBeNotZero, [CallerArgumentExpression(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
    }

    /// <summary>
    /// Ensures that the specified value is not zero.
    /// </summary>
    /// <param name="valueShouldBeNotZero"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotZero(long valueShouldBeNotZero, [CallerArgumentExpression(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
    }

    /// <summary>
    /// Ensures that the specified value is not zero.
    /// </summary>
    /// <param name="valueShouldBeNotZero"></param>
    /// <param name="parameterName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotZero(float valueShouldBeNotZero, [CallerArgumentExpression(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
    }

    /// <summary>
    /// Ensures that the condision is satisfied, or throws <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    /// <param name="shouldBe"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidRange(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new ArgumentOutOfRangeException(message);
        }
    }

    /// <summary>
    /// Ensures that the condition is satisfied, or throws <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="shouldBe"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidArgument(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new ArgumentException(message);
        }
    }

    /// <summary>
    /// Ensures that the condition is satisfied, or throws <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <param name="shouldBe"></param>
    /// <param name="message"></param>
    /// <exception cref="InvalidOperationException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidState(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new InvalidOperationException(message);
        }
    }

    /// <summary>
    /// Ensures that the specified type is a primitive real number type (float or double).
    /// </summary>
    /// <typeparam name="TShouldBeReal"></typeparam>
    /// <exception cref="NotSupportedException"></exception>
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnlyRealSupported<TShouldBeReal>()
        where TShouldBeReal : unmanaged
    {
        if(typeof(TShouldBeReal) != typeof(float) && typeof(TShouldBeReal) != typeof(double))
        {
            throw new NotSupportedException();
        }
    }
}
