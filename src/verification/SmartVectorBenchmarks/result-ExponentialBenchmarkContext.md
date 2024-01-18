# Benchmark result of ExponentialBenchmarkContext

## performance benchmark

Mean of duration
- unit: microsecond
- N ... number of members
- Emulated ... use simple loop with `System.Math`

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.403
  [Host]     : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2

| N          | exp   |
|----------- |------:|
| *Emulated* | 482.9 |
| 1          | 366.1 |
| 2          | 365.6 |
| 3          | 377.3 |
| 4          | 374.3 |
| 5          | 383.1 |
| 6          | 379.2 |
| 7          | 389.9 |
| 8          | 398.2 |
| 9          | 407.6 |
| 10         | 423.0 |
| 11         | 439.9 |
| 12         | 459.4 |
| 13         | 474.9 |
| 14         | 497.9 |
| 15         | 507.6 |
| 16         | 522.5 |
| 17         | 536.1 |
| 18         | 568.3 |
| 19         | 573.4 |

## precision benchmark

Mean of squared residual against *Emulated*
- N ... number of members
- Emulated ... use simple loop with `System.Math`

| N  | exp       |
|:--:|:---------:|
|  1 | 1.975e-02 |
|  2 | 1.954e-03 |
|  3 | 1.507e-04 |
|  4 | 9.511e-06 |
|  5 | 5.080e-07 |
|  6 | 2.351e-08 |
|  7 | 9.600e-10 |
|  8 | 3.507e-11 |
|  9 | 1.160e-12 |
| 10 | 3.689e-14 |
| 11 | 1.295e-14 |
| 12 | 1.282e-14 |
| 13 | 1.282e-14 |
| 14 | 1.282e-14 |
| 15 | 1.282e-14 |
| 16 | 1.282e-14 |
| 17 | 1.282e-14 |
| 18 | 1.282e-14 |
| 19 | 1.282e-14 |