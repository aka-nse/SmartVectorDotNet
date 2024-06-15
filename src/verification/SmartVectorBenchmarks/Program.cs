using BenchmarkDotNet.Running;
using SmartVectorBenchmarks;

Console.WriteLine("Hello, Benchmark!");


BenchmarkRunner.Run<PopulationBenchmarkContext>();
// BenchmarkRunner.Run<VectorLoopBenchmark>();

// BenchmarkRunner.Run<FluentIfBenchmarkContext>();
// LogarithmBenchmarkContext.RunBenchmark();
// ExponentialBenchmarkContext.RunBenchmark();
// TrigonometricBenchmarkContext.RunBenchmark();
// AtanBenchmarkContext.RunBenchmark();
