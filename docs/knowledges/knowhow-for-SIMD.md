*Author: aka-nse*

# Knowhow for SIMD operations

SIMD operation - a.k.a vector operation - is efficient calculation  processing based on parallelization.
But for implementations with SIMD we should consider that is different from normal scalar operations.
This document explains some knowhows for implementations with SIMD.

## Conditional branch

In most cases conditional branch (`if`, `for`, `while`, or `switch`) is not available with SIMD operation, because one vector instance can contains several elements that branch to different path each other.
Therefore usually the strategy to calculate every path and combine them at last is employed.


For example, considering about ramp function:

$$
R(x) :=
\begin{cases}
    x & (x \ge 0)  \\
    0 & (x \lt 0)  \\
\end{cases}
$$

Normally implementation of this in C# would look like following:
(In this sample intentionally does not use `Math.Max`)

```CSharp
public static double Ramp(double x)
    => x < 0 ? 0 : x;
```

For SIMD operation it should be rewritten as following:

```CSharp
public static double Ramp(Vector<double> x)
{
    var xIsLessThanZero = Vector.LessThan(x, Vector<double>.Zero);
    return Vector.ConditionalSelect(
        xIsLessThanZero,
        Vector<double>.Zero,
        x);
}
```

However, if the calculation load for each pass is heavy, it is undesirable to perform calculations many times, so you need to carefully consider at what stage branch conditions are applied.

For example, it is assumed that there is a function `SinForPositive(x)` who can calculate $\sin x$ at the domain $0 \le x$ (for the value outer the domain we can obtain meaningless value but never throw exception), let's try to implement `Sin(x)` who can do it at the domain $x \in \mathbb{R}$ with using `SinForPositive(x)`.

We can use the theorem $\sin (-x) = -\sin x$ so that like as following implementation might be came up:

```CSharp
public static Vector<double> Sin(Vector<double> x)
{
    var xIsLessThanZero = Vector.LessThan(x, Vector<double>.Zero);
    return Vector.ConditionalSelect(
        xIsLessThanZero,
        -SinForPositive(-x),
        SinForPositive(x));
}
```

But in the case `SinForPositive(x)` is heavy, following implementation might be better than:

```CSharp
public static Vector<double> Sin(Vector<double> x)
{
    var xIsLessThanZero = Vector.LessThan(x, Vector<double>.Zero);
    var sign = Vector.ConditionalSelect(
        xIsLessThanZero,
        -Vector<double>.One,
        Vector<double>.One);
    return sign * SinForPositive(sign * x);
}
```

Practical micro-benchmark is important to decide what implementation is better.
