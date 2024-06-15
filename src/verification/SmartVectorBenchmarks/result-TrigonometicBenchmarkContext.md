# Benchmark result of TrigonometicBenchmarkContext

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

| N          | sin   | cos   |
|----------- |------:|------:|
| *Emulated* | 453.9 | 458.7 |
| 1          | 200.1 | 210.5 |
| 2          | 217.5 | 216.2 |
| 3          | 217.3 | 219.8 |
| 4          | 225.6 | 224.2 |
| 5          | 231.8 | 226.5 |
| 6          | 241.8 | 235.6 |
| 7          | 250.8 | 246.9 |
| 8          | 261.8 | 251.9 |
| 9          | 270.2 | 262.6 |
| 10         | 279.2 | 286.6 |
| 11         | 289.9 | 281.4 |
| 12         | 294.8 | 292.4 |
| 13         | 299.3 | 310.0 |
| 14         | 311.4 | 322.4 |
| 15         | 318.2 | 338.1 |
| 16         | 341.4 | 344.9 |
| 17         | 352.3 | 363.6 |
| 18         | 371.5 | 378.4 |
| 19         | 387.1 | 389.9 |

## precision benchmark

Mean of squared residual against *Emulated*
- N ... number of members
- Emulated ... use simple loop with `System.Math`

| N  | sin       | cos       |
|:--:|:---------:|:---------:|
|  1 | 1.126e-02 | 3.709e-02 |
|  2 | 6.134e-04 | 2.841e-03 |
|  3 | 1.949e-05 | 1.159e-04 |
|  4 | 4.077e-07 | 2.954e-06 |
|  5 | 6.052e-09 | 5.166e-08 |
|  6 | 6.715e-11 | 6.594e-10 |
|  7 | 5.782e-13 | 6.419e-12 |
|  8 | 3.987e-15 | 4.925e-14 |
|  9 | 5.649e-17 | 3.121e-16 |
| 10 | 5.509e-17 | 4.450e-17 |
| 11 | 5.509e-17 | 4.459e-17 |
| 12 | 5.509e-17 | 4.459e-17 |
| 13 | 5.509e-17 | 4.459e-17 |
| 14 | 5.509e-17 | 4.459e-17 |
| 15 | 5.509e-17 | 4.459e-17 |
| 16 | 5.509e-17 | 4.459e-17 |
| 17 | 5.509e-17 | 4.459e-17 |
| 18 | 5.509e-17 | 4.459e-17 |
| 19 | 5.509e-17 | 4.459e-17 |