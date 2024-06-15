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
