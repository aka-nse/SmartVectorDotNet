#pragma warning disable IDE0130
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SmartVectorDotNet;

/// <summary>
/// Provides compatibility of CSharp operators for generic number types.
/// </summary>
public static partial class ScalarOp
{
    internal static TTo Reinterpret<TFrom, TTo>(in TFrom x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => Unsafe.As<TFrom, TTo>(ref Unsafe.AsRef(in x));
}
