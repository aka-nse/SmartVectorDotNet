*Author: aka-nse*

# Improvement of accuracy of modulo by constant

For float constant $\alpha$ and float input $x$, we will consider the algorithm to calculate $\beta = x\ \mathrm{mod}\ \alpha$ computationally.

First, the transformation of the formula is performed from a mathematical perspective.

$$
\def\mod{\ \mathrm{mod}\ }
\begin{align*}
& x = \alpha n + \beta \hspace{1em}
\left(\mathrm{where}\ n = \left\lfloor\cfrac{x}{\alpha}\right\rfloor\right)  \\
\Rightarrow &
x\cdot\alpha^{-1} = n + \beta\cdot\alpha^{-1}  \\
\Rightarrow &
\beta =\alpha\left( x\cdot\alpha^{-1} - n \right)
\end{align*}
$$

At then, $\alpha$ is transformed such as following to present computationally.

$$
\begin{align*}
\alpha^{-1}
&= \cfrac{a_1}{2^{8}} + \cfrac{a_2}{2^{16}} + \cfrac{a_3}{2^{24}} + \cdots  \\
&= \sum_{k=1}^{\infty}a_k\cdot 2^{-8\cdot k} \hspace{2em} (a_k \in \{0, 1, 2, \cdots, 255\})
\end{align*}
$$

where $a_k$ is an integer that can be represented using 8 bits.

----

Now, we will consider the example case of $x = 1.6974669 \times 10^{37}$ and $\alpha = \frac{\pi}{2}$[^1] in IEEE 754 binary32 format.

[^1]: For this case, $\frac{\pi}{2}$ is more preferable than $\pi$ as the divisor because $\frac{\pi}{2} = 1.57 \cdots$ and therefore its exponential part is `0`.

They can be transformed as followings:

$$
\begin{align*}
1.6974669 \times 10^{37}
&= (1 + \mathrm{0x4C533D} \times 2^{-23}) \times 2^{123}\\
\left( \cfrac{\ \pi\ }{2} \right)^{-1}
&=  \cfrac{\rm 0xA2}{2^{8}} +
    \cfrac{\rm 0xF9}{2^{16}} +
    \cfrac{\rm 0x83}{2^{24}} +
    \cfrac{\rm 0x6E}{2^{32}} +
    \cfrac{\rm 0x4E}{2^{40}} +
    \cdots
\end{align*}
$$

About $\frac{\beta}{\alpha} = x\cdot \alpha^{-1} - n$,

- the integer part of $x \cdot \alpha^{-1}$ is ignorable because $n = \left\lfloor x \cdot \alpha^{-1} \right\rfloor$,
- the fractional part that exceeds 24 bits, counting from the first significant bit, is ignorable because the fractional part of binary32, including the economized bit, is 24 bits,

therefore followings transformation is available:

![./mathematicals_mod-by-const.svg](./img/mod-by-const.svg)

$$
\def\mod{\ \mathrm{mod}\ }
{\LARGE\Downarrow}\\
\begin{align*}
x\mod\alpha
&= \alpha\left(x\cdot\alpha^{-1} \mod 1\right) \\
&= \cfrac{\ \pi\ }{2}\cdot\left\{
    1.6974669 \times 10^{37} \times\left(\frac{\pi}{2}\right)^{-1}\mod 1
\right\}  \\
&= \cfrac{\ \pi\ }{2}\cdot\left[\left\{
    \texttt{0xCC533D} \times 2^{100} \times \left(
        \cfrac{\texttt{0xA2}}{2^{8}} +
        \cfrac{\texttt{0xF9}}{2^{16}} +
        \cfrac{\texttt{0x83}}{2^{24}} +
        \cfrac{\texttt{0x6E}}{2^{32}} +
        \cfrac{\texttt{0x4E}}{2^{40}} +
        \cdots
    \right)
\right\}\mod 1\right]  \\
&= \cfrac{\ \pi\ }{2}\cdot\left\{\left(
    \begin{array}{l}
        \left.\begin{array}{lrcl}
             &\texttt{0x814CAC9A} &\times& 2^{100-8}    \\
            +&\texttt{0xC6BCF655} &\times& 2^{100-16}   \\
            +&\texttt{0x688E9837} &\times& 2^{100-24}   \\
            +&\cdots              &      &              \\
            +&\texttt{0xA6CFF4CD} &\times& 2^{100-96}  \\
        \end{array} \color{#DDD}\right\}\color{#000} \textcolor{#DDD}{\rm ignorable}\\[7ex]
        \begin{array}{lrcl}
            +&\texttt{0xC38BA961} &\times& 2^{100-104}  \\
            +&\texttt{0x2980E864} &\times& 2^{100-112}  \\
            +&\texttt{0xB063DBA9} &\times& 2^{100-120}  \\
            +&\texttt{0x993E6DC0} &\times& 2^{100-128}  \\
            +&\texttt{0xAECB352F} &\times& 2^{100-136}  \\
            +&\texttt{0x4E37DD5A} &\times& 2^{100-144}  \\
            +&\texttt{0x76EC7281} &\times& 2^{100-152}  \\
        \end{array}\\
        \left.\begin{array}{lrcl}
            +&\texttt{0x7A1DBF75} &\times& 2^{100-160}  \\
            +&\cdots
        \end{array} \color{#DDD}\right\}\color{#000} \textcolor{#DDD}{\rm ignorable}
    \end{array}
\right) \mod 1\right\}  \\
&\approx \cfrac{\ \pi\ }{2}\cdot\left\{\left(
    \begin{array}{lrcl}
         &\texttt{0xC38BA961} &\times& 2^{-4}  \\
        +&\texttt{0x2980E864} &\times& 2^{-12}  \\
        +&\texttt{0xB063DBA9} &\times& 2^{-20}  \\
        +&\texttt{0x993E6DC0} &\times& 2^{-28}  \\
        +&\texttt{0xAECB352F} &\times& 2^{-36}  \\
        +&\texttt{0x4E37DD5A} &\times& 2^{-44}  \\
        +&\texttt{0x76EC7281} &\times& 2^{-52}  \\
    \end{array}
\right) \mod 1\right\}  \\
&\approx 1.5707964 \times 0.4485327 = 0.7045535
\end{align*}
$$

Therefore this logic can be implemented such as following:

```csharp
private static readonly uint[] ipip2 = [
    0xA2, 0xF9, 0x83, 0x6E, 0x4E, 0x44, 0x15, 0x29,
    0xFC, 0x27, 0x57, 0xD1, 0xF5, 0x34, 0xDD, 0xC0,
    0xDB, 0x62, 0x95, 0x99, 0x3C, 0x43, 0x90, 0x41,
    0xFE, 0x51, 0x63, 0xAB, 0xDE, 0xBB, 0xC5, 0x61,
    0xB7, 0x24, 0x6E, 0x3A, 0x42, 0x4D, 0xD2, 0xE0,
    0x06, 0x49, 0x2E, 0xEA, 0x09, 0xD1, 0x92, 0x1C,
    0xFE, 0x1D, 0xEB, 0x1C, 0xB1, 0x29, 0xA7, 0x3E,
    0xE8, 0x82, 0x35, 0xF5, 0x2E, 0xBB, 0x44, 0x84,
    0xE9, 0x9C, 0x70, 0x26, 0xB4, 0x5F, 0x7E, 0x41,
    0x39, 0x91, 0xD6, 0x39, 0x83, 0x53, 0x39, 0xF4,
    0x9C, 0x84, 0x5F, 0x8B, 0xBD, 0xF9, 0x28, 0x3B,
    0x1F, 0xF8,
];

public static uint ShiftBoth(uint x, int y)
    => y > 0
        ? x << y
        : x >> -y;

public static float ModByPiP2(float x)
{
    Decompose(x, out var sign, out var expo_, out var frac_);
    var frac = ((uint)frac_) | (1u << 23);
    int expo, start;
    expo = expo_ - 127 - 23;
    start = (expo + 7) / 8;

    // TODO: search first non-zero bit in frac * ipip2[start + (0 - 1)] << (expo - (start + 0) * 8 + 32)

    return (
        + ShiftBoth(frac * ipip2[start + (0 - 1)], expo - (start + 0) * 8 + 32)
        + ShiftBoth(frac * ipip2[start + (1 - 1)], expo - (start + 1) * 8 + 32)
        + ShiftBoth(frac * ipip2[start + (2 - 1)], expo - (start + 2) * 8 + 32)
        + ShiftBoth(frac * ipip2[start + (3 - 1)], expo - (start + 3) * 8 + 32)
        + ShiftBoth(frac * ipip2[start + (4 - 1)], expo - (start + 4) * 8 + 32)
        + ShiftBoth(frac * ipip2[start + (5 - 1)], expo - (start + 5) * 8 + 32)
        + ShiftBoth(frac * ipip2[start + (6 - 1)], expo - (start + 6) * 8 + 32)
    ) * MathF.Pow(2, -32) * (MathF.PI / 2);
}
```
