# Generic Experiment 01

[Show on Sharplab.io](https://sharplab.io/#v2:C4LghgzgtgPgAgJgIwFgBQcAMACOSB0ASgK4B2wAllAKb4DCA9lAA4UA21ATgMpcBuFAMbUIAbnRZs3ABZhOzADJgARkTKUa4tOgDaAKQrAA4tVJchACmABPZtQYAzCwBMGxZRwCUngDTYDxqbmgla29k4ObAxgwN4AuhIAzLhIAGy4CNh0ADwAKgB86ADe6Nhl2MycFHwx1CnpygwMbNgAkhC5eQCi5IbW+RaepeUjALz52DZ2jha5ntijo5NhM7k9lDaeWsNlldW19dic1A5H1GCupGzW2Lm5DNgAghB59wMUpLfYAB5DaCNjCbHU4AVVIEDADlozzyfjuDAGwOwYIhUPwz0IJwsHx+3i0APKOwqVRqwDqeHSSOOFwYVxuuSeL1yADFOEx3p8WWyoLiiYCzqDwZDoUzWUw4YiTsihWiMVicb8tugiXBkhSvo9nM5ZtgwHDsMo/gSyuM2h1sq53BwBkbjSMAPyMiwwy0eagDMDzADUjItbjdA0NtoJIDNnUi0WANr5xsdz2dLwjMQ93t9SajFiDMbKoeA0jZAHdsGYiwA5BjAbjEZjMBicMnOLrfYTMSi0wZaAC+yu0GDVaQyWWKKv76VdHCeWpc/oneuw47qhuzCwmYGwPuU+MJ/3KqsO6cn2oPc4PS53BNNa43XaAA===)

```CSharp
using System;
using System.Runtime.CompilerServices;
using SharpLab.Runtime;

[JitGeneric(typeof(double)), JitGeneric(typeof(float))]
public static class C<T>
{
    private static bool IsT<TEntity>()
        => typeof(T) == typeof(TEntity);

    private static ref readonly TTo As<TTo>(in T x)
        => ref Unsafe.As<T, TTo>(ref Unsafe.AsRef(in x));
        
    private static ref readonly T As<TFrom>(in TFrom x)
        => ref Unsafe.As<TFrom, T>(ref Unsafe.AsRef(in x));

    public static T Add(T a, T b)
        => IsT<double>()
            ? As(As<double>(a) + As<double>(b))
        : IsT<float>()
            ? As(As<float>(a) + As<float>(b))
        : throw new NotSupportedException();
}


public static class C
{
    public static double Add(double a, double b)
        => a + b;
    
    public static float Add(float a, float b)
        => a + b;
}
```

```nasm
; Core CLR 8.0.23.53103 on x86

C`1[[System.Double, System.Private.CoreLib]].get_t()
    ; Open generics cannot be JIT-compiled.
    ; However you can use attribute SharpLab.Runtime.JitGeneric to specify argument types.
    ; Example: [JitGeneric(typeof(int)), JitGeneric(typeof(string))] void M<T>() { ... }.

C`1[[System.Double, System.Private.CoreLib]].IsT()
    ; Open generics cannot be JIT-compiled.
    ; However you can use attribute SharpLab.Runtime.JitGeneric to specify argument types.
    ; Example: [JitGeneric(typeof(int)), JitGeneric(typeof(string))] void M<T>() { ... }.

C`1[[System.Double, System.Private.CoreLib]].As(Double ByRef)
    ; Open generics cannot be JIT-compiled.
    ; However you can use attribute SharpLab.Runtime.JitGeneric to specify argument types.
    ; Example: [JitGeneric(typeof(int)), JitGeneric(typeof(string))] void M<T>() { ... }.

C`1[[System.Double, System.Private.CoreLib]].Add(Double, Double)
    L0000: sub esp, 8
    L0003: vzeroupper
    L0006: vmovsd xmm0, [esp+0x14]
    L000c: vaddsd xmm0, xmm0, [esp+0xc]
    L0012: vmovsd [esp], xmm0
    L0017: fld st, qword ptr [esp]
    L001a: add esp, 8
    L001d: ret 0x10

C`1[[System.Single, System.Private.CoreLib]].IsT()
    ; Open generics cannot be JIT-compiled.
    ; However you can use attribute SharpLab.Runtime.JitGeneric to specify argument types.
    ; Example: [JitGeneric(typeof(int)), JitGeneric(typeof(string))] void M<T>() { ... }.

C`1[[System.Single, System.Private.CoreLib]].As(Single ByRef)
    ; Open generics cannot be JIT-compiled.
    ; However you can use attribute SharpLab.Runtime.JitGeneric to specify argument types.
    ; Example: [JitGeneric(typeof(int)), JitGeneric(typeof(string))] void M<T>() { ... }.

C`1[[System.Single, System.Private.CoreLib]].As(!!0 ByRef)
    ; Open generics cannot be JIT-compiled.
    ; However you can use attribute SharpLab.Runtime.JitGeneric to specify argument types.
    ; Example: [JitGeneric(typeof(int)), JitGeneric(typeof(string))] void M<T>() { ... }.

C`1[[System.Single, System.Private.CoreLib]].Add(Single, Single)
    L0000: push eax
    L0001: vzeroupper
    L0004: vmovss xmm0, [esp+0xc]
    L000a: vaddss xmm0, xmm0, [esp+8]
    L0010: vmovss [esp], xmm0
    L0015: fld st, dword ptr [esp]
    L0018: pop ecx
    L0019: ret 8

C.Add(Double, Double)
    L0000: sub esp, 8
    L0003: vzeroupper
    L0006: vmovsd xmm0, [esp+0x14]
    L000c: vaddsd xmm0, xmm0, [esp+0xc]
    L0012: vmovsd [esp], xmm0
    L0017: fld st, qword ptr [esp]
    L001a: add esp, 8
    L001d: ret 0x10

C.Add(Single, Single)
    L0000: push eax
    L0001: vzeroupper
    L0004: vmovss xmm0, [esp+0xc]
    L000a: vaddss xmm0, xmm0, [esp+8]
    L0010: vmovss [esp], xmm0
    L0015: fld st, dword ptr [esp]
    L0018: pop ecx
    L0019: ret 8

```