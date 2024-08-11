# SmartVectorDotNet

SmartVectorDotNet is a library to calculate the sequence by a unified signature with SIMD.

## Concepts

This library has 3 layers:

1. `ScalarOp`/`ScalarMath`<br/>
   provides generalized and backward-compatibility-enhanced operators and `Math`/`MathF` functions.

2. `VectorOp`/`VectorMath`<br/>
   provides SIMD parallelized APIs which are corresponding with each method in `ScalarOp`/`ScalarMath`.

3. `Vectorization`<br/>
   provides span based sequential operation.
   Their APIs are declared as strategy pattern, you can select simple loop or SIMD vectorized operation.

## Usage

### Generalized `System.Numerics.Vector<T>` calculations

```CSharp
using System;
using System.Numerics;
using SmartVectorDotNet;

var x = new Vector<double>(0, 1, 2, 3);
var sin_x_pi = VectorMath.Sin(VectorMath.Multiply(x, VectorMath.Const<double>.PI));
Console.WriteLine(sin_x_pi);
```

### Simply vectirozation

```CSharp
using System;
using SmartVectorDotNet;

var x = Enumerable.Range(0, 256).Select(i => (double)i).ToArray();
var tmp = new double[x.Length];
var ans = new double[x.Length];
Vectorization.SIMD.Multiply<double>(x, Math.PI, tmp);
Vectorization.SIMD.Sin<double>(tmp, ans);
Console.WriteLine(string.Join(", ", ans));
```

## Release notes

### v0.1.0.0

- first releases
