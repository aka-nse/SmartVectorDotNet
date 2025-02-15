using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

internal static class Guard
{
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNull<T>(T valueShouldBeNotNull, [CallerArgumentExpression(nameof(valueShouldBeNotNull))] string parameterName = null!)
    {
        if (valueShouldBeNotNull is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNullOrEmpty(string valueShouldBeNotNullAndNotEmpty, [CallerArgumentExpression(nameof(valueShouldBeNotNullAndNotEmpty))] string parameterName = null!)
    {
        if (string.IsNullOrEmpty(valueShouldBeNotNullAndNotEmpty))
        {
            throw new ArgumentException("Value cannot be null or empty.", parameterName);
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNullOrWhiteSpace(string valueShouldBeNotNullAndNotWhiteSpace, [CallerArgumentExpression(nameof(valueShouldBeNotNullAndNotWhiteSpace))] string parameterName = null!)
    {
        if (string.IsNullOrWhiteSpace(valueShouldBeNotNullAndNotWhiteSpace))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", parameterName);
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(int valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(long valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(float valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(double valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNegative(decimal valueShouldBeNotNegative, [CallerArgumentExpression(nameof(valueShouldBeNotNegative))] string parameterName = null!)
    {
        if (valueShouldBeNotNegative < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotZero(int valueShouldBeNotZero, [CallerArgumentExpression(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotZero(long valueShouldBeNotZero, [CallerArgumentExpression(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotZero(float valueShouldBeNotZero, [CallerArgumentExpression(nameof(valueShouldBeNotZero))] string parameterName = null!)
    {
        if (valueShouldBeNotZero == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Value cannot be zero.");
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidRange(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new ArgumentOutOfRangeException(message);
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidArgument(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new ArgumentException(message);
        }
    }

    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidState(bool shouldBe, string message)
    {
        if (!shouldBe)
        {
            throw new InvalidOperationException(message);
        }
    }
}
