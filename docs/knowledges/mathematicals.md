*Author: aka-nse*

# Implementation of mathematical functions

In this projects, some mathematical functions were implemented using the Maclaurin series expansion.

Furthermore transformations under mathematical formula were utilized to enhance accuracy and performance.

This document explains the knowledge behind these implementations.

## General strategy

### Taylor series / Maclaurin series

Taylor series at $x=a$ is transformation of arbitrary function which is constructed with infinite members polynomial function based values on n-th deriviative $f^{(n)}(a)$, and its special form of $a=0$ is as known as Maclaurin series.
(provided, several series have finite range to converge, and the range is called as radius of convergence)

$$
\begin{aligned}
f(x)
&= \sum_{n=0}^\infty\cfrac{f^{(n)}(a)}{n!}(x-a)^n
& (\mathrm{Taylor\ series\ at\ }x=a)  \\
&= \sum_{n=0}^\infty\cfrac{f^{(n)}(0)}{n!}(x)^n
& (\mathrm{Maclaurin\ series})
\end{aligned}
$$

For computation, the original function can be approximated by stopping the calculation at a finite number of terms, and the error in this case improves as the number of calculated terms increases.
Because the more terms to calculate, the longer calculation time, number of terms should be choosen considering balance of performance and accuracy.

On other hands, there is another way to improve accuracy without increasing number of terms: approximation with finite-terms Taylor series is more accurate at the closer it is to $x=a$.
Therefore, in some cases, a transformation of variable can be used to improve accuracy.
For example, periodicity of trigonometric function is well known:

$$
\sin(x + 2n\pi) = \sin x \ \ (n\in \mathbb{Z})
$$

therefore taking the modulo by $2\pi$ narrows the range calculation and improves accuracy for the case of $|x|>2\pi$.

### Newton's method

Newton's method is well known as another efficient computation strategy.

For a equation $f(x) = 0$, recursively taking the x-segment of a tangential line makes next x closer into the solution.
Its recurrence relation formula is following:

$$
x_{n+1} = x_n - \cfrac{f(x_n)}{f'(x_n)}
$$

However, its convergence speed is depend on initial value $x_1$, therefore choice of it is important.

## Trigonometric functions

Maclaurin series of trigonometric functions are following:

$$
\begin{aligned}
\sin x
&= \sum_{n=0}^\infty\cfrac{(-1)^n}{(2n+1)!}x^{2n+1}
&= x-\cfrac{x^3}{3!}+\cfrac{x^5}{5!}-\cfrac{x^7}{7!}+\cdots\\
\cos x
&= \sum_{n=0}^\infty\cfrac{(-1)^n}{(2n)!}x^{2n}
&= 1-\cfrac{x^2}{2!}+\cfrac{x^4}{4!}-\cfrac{x^6}{6!}+\cdots\\
\end{aligned}
$$

And following variable transformations are available:

$$
\begin{aligned}
\cos x
    &= \cos (x + 2n\pi)  \\
    &= \cos(-x)          \\
    &= -\cos (\pi - x)   \\
\sin x
    &= \sin (x + 2n\pi)  \\
    &= -\sin(-x)         \\
    &= \sin(\pi-x)       \\
\tan x &= \frac{\sin x}{\cos x}
\end{aligned}
$$

And for $\tan x$, because of addition theorem following conversion is available:

$$
\begin{aligned}
&\tan(x + y) = \cfrac{\tan x + \tan y}{1 - \tan x \tan y}  \\
&\Rightarrow
\tan\left(x + \cfrac{\pi}{4}\right) = \cfrac{1 + \tan x}{1 - \tan x}
\end{aligned}
$$

Therefore, argument conversion clips the domain of core implementation for trigonometric function into $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ for $\sin x, \cos x$, and $-\frac{\pi}{4} \le x \lt \frac{\pi}{4}$ for $\tan x$.

**NOTE**: Although another transformation using complementary angle formula or addition theorem can make the domain more narrower into $-\frac{\pi}{4} \le x \lt \frac{\pi}{4}$ for $\sin x, \cos x$, it is not applied for this project because function call branching on SIMD operation may be less performance than argument transformation only with addition and substraction.

## Inverse trigonometric function

Maclaurin series of $\arctan x$ function is following:

$$
\arctan x
= \sum_{n=0}^{\infty}\cfrac{(-1)^n}{2n+1}x^{2n+1}
= x - \cfrac{x^3}{3} + \cfrac{x^5}{5} - \cfrac{x^7}{7} + \cdots
$$

And following formulas are available to convert argument:

$$
\begin{aligned}
&\arctan\cfrac{1}{x} = \mathrm{sgn}\,x\cdot\cfrac{\pi}{2} - \arctan x  && (\mathrm{i})\\
&\arctan x + \arctan y = \arctan\cfrac{x + y}{1 - xy}  \\
&\Rightarrow \arctan x + \arctan 1 = \arctan x + \cfrac{\pi}{4} = \arctan\cfrac{1 + x}{1 - x}  \\
&\Rightarrow \arctan x = \arctan\cfrac{1 + x}{1 - x} - \cfrac{\pi}{4} && (\mathrm{ii})
\end{aligned}
$$

therefore the domain of Maclaurin series approximation can be limited into $0 \le x \le \sqrt{2} - 1$.

In other hands, $\arcsin x$ and $\arccos x$ can be calculated with following formulas:

$$
\begin{aligned}
\arcsin x &= 2\arctan\cfrac{x}{1 + \sqrt{1 - x^2}}  \\
\arccos x &= 2\arctan\cfrac{\sqrt{1 - x^2}}{1 + x}
\end{aligned}
$$

**NOTE**: Although Maclaurin series of $\arcsin x$ and $\arccos x$ are following:

$$
\begin{aligned}
\arcsin x
&= \sum_{n=0}^{\infty} \cfrac{(2n)!}{4^n(n!)^2(2n+1)}x^{2n+1}
&= x + \cfrac{x^3}{6} + \cfrac{3x^5}{40} + \cfrac{5x^7}{112} + \cdots \\
\arccos x
&=\cfrac{\pi}{2} - \arcsin x
\end{aligned}
$$

their convergences at $|x| \sim 1$ are slow and impractical.

## Exponential function

Maclaurin series of exponential function with neipa number as base are following:

$$
e^x
= \sum_{n=0}^{\infty}\cfrac{1}{n!}x^n
= 1 + x + \cfrac{x^2}{2} + \cfrac{x^3}{6} + \cfrac{x^4}{24} + \cdots
$$

And base conversion formula between any base $a$ and $b$ is available:

$$
b^x = a^{x\log_a b}
$$

Due to conversion between $e$ and $2$, following equation is obtained:

$$
e^x = 2^{x\log_2 e}
$$

Furthermore to define $y:=\mathrm{round}(x\log_2 e), a:= x\log_2 e - y$ enables to transform as following:

$$
e^x = 2^y \cdot 2^a = 2^y \cdot e^{a\log_e 2}
$$

Then, 

- $\log_2 e$ and $\log_e 2$ can be declared as compile time constant,
- $2^y$ can be replaced by bit shift operation because $y$ is an integer,
- $a$ satisfies $-0.5 \le a \le +0.5$,

so that high precision and performace can consist with each other.

## Logarithm function

Maclaurin series of exponential function with neipa number as base are following:

$$
\ln(1 + x)
= \sum_{n=0}^{\infty}\cfrac{(-1)^n}{n+1}x^{n+1}
= x - \cfrac{x^2}{2} + \cfrac{x^3}{3} - \cfrac{x^4}{4} + \cdots
\ (\Leftarrow -1 \lt x \le 1)
$$

In other hands, following formula is available:

$$
\ln(a \cdot b) = \log a + \log b
$$

logarithm function can be transformed into following form if $a, n :\Leftrightarrow x=a\cdot 2^n, n \in \mathbb{Z}, 1 \le |a| \lt 2$:

$$
\begin{aligned}
\ln x
=& \ln a\cdot 2^n    \\
=& \ln a + \ln 2^n   \\
=& \ln a + n\ln 2    &(\mathrm{i}) \\
=& \left(\ln\cfrac{a}{2} + \ln 2\right) + n\ln 2  &(\mathrm{ii})
\end{aligned}
$$

- $\ln 2$ can be declared as compile time constant,
- for normalized number of binary float of IEEE 754, $a$ and $n$ can be calculated by using simple bit operation,
- using $(\mathrm{i})$ form if $a \le \sqrt{2}$ and otherwise $(\mathrm{ii})$ can improve Maclaurin series approximation precision,

so that high precision and performace can consist with each other.

## Hyperbolic functions

Hyperbolic functions are defined following formulas, so that they can be calculated with elementary functions which are defined above:

$$
\begin{aligned}
\sinh x &= \cfrac{e^x - e^{-x}}{2}  \\
\cosh x &= \cfrac{e^x + e^{-x}}{2}  \\
\tanh x &= \cfrac{e^x - e^{-x}}{e^x + e^{-x}} = \cfrac{e^{2x} - 1}{e^{2x} + 1}  \\
\end{aligned}
$$

## Inverse hyperbolic functions

Inverse hyperbolic functions are defined following formulas, so that they can be calculated with elementary functions which are defined above:

$$
\begin{aligned}
\mathrm{arsinh}\,x &= \ln(x + \sqrt{x^2+1})  \\
\mathrm{arcosh}\,x &= \ln(x + \sqrt{x^2-1})  \\
\mathrm{artanh}\,x &= \cfrac{1}{2}\ln\cfrac{1+x}{1-x}  \\
\end{aligned}
$$

However, transformation of $\mathrm{arcsinh}$ and $\mathrm{arccosh}$ have computational overflow problem because of squared term.
Therefore extra transformation such as following form is preferable:

$$
\begin{aligned}
\mathrm{arsinh}\,x &= \ln(x + \sqrt{x^2+1})  \\
                   &\sim \mathrm{Sign}(x) \ln(2|x|) \sim \mathrm{Sign}(x) (2 + \ln|x|) &(x \rightarrow \pm\infty)
\end{aligned}
$$

$$
\begin{aligned}
\mathrm{arcosh}\,x &= \ln(x + \sqrt{x^2-1})  \\
                   &= \ln\left\{x\left(1 + \frac{\sqrt{(x - 1)}\sqrt{(x + 1)}}{x}\right)\right\}  \\
                   &= \ln x + \ln\left(1 + \frac{\sqrt{(x - 1)}\sqrt{(x + 1)}}{x}\right)
\end{aligned}
$$

## Cubic root

To calculate cubic root, Newton's method is appropriate instead of Taylor series.
Cubic root of $y$ can be calculated by following process:

$$
\begin{aligned}
&f(x) = x^3 - y = 0  \\
&\begin{aligned}
\Rightarrow x_{n+1}
&= x_n - \cfrac{{x_n}^3 - y}{3{x_n}^2}  \\
&= \cfrac{2}{3}x_n + \cfrac{y}{3{x_n}^2}
\end{aligned}
\end{aligned}
$$

In the case of IEEE 754 floating number, the value $a, n$ which satisfy $y = a\cdot 2^n, n \in \mathbb{Z}, 1 \le a \lt 2$ can be obtained by using only simple bit operations.

Then, $a\cdot2^{\left\lfloor\frac{n}{3}\right\rfloor}$ is usually very closer to $\sqrt[3]{y} = \sqrt[3]{a\cdot 2^n}$ than $y$, so that it is appropriate as an initial value.